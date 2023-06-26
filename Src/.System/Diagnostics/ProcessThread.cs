using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	/// <summary>Represents an operating system process thread.</summary>
	// Token: 0x020004FF RID: 1279
	[Designer("System.Diagnostics.Design.ProcessThreadDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[HostProtection(SecurityAction.LinkDemand, SelfAffectingProcessMgmt = true, SelfAffectingThreading = true)]
	public class ProcessThread : Component
	{
		// Token: 0x06003086 RID: 12422 RVA: 0x000DB61B File Offset: 0x000D981B
		internal ProcessThread(bool isRemoteMachine, ThreadInfo threadInfo)
		{
			this.isRemoteMachine = isRemoteMachine;
			this.threadInfo = threadInfo;
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets the base priority of the thread.</summary>
		/// <returns>The base priority of the thread, which the operating system computes by combining the process priority class with the priority level of the associated thread.</returns>
		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06003087 RID: 12423 RVA: 0x000DB637 File Offset: 0x000D9837
		[MonitoringDescription("ThreadBasePriority")]
		public int BasePriority
		{
			get
			{
				return this.threadInfo.basePriority;
			}
		}

		/// <summary>Gets the current priority of the thread.</summary>
		/// <returns>The current priority of the thread, which may deviate from the base priority based on how the operating system is scheduling the thread. The priority may be temporarily boosted for an active thread.</returns>
		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x000DB644 File Offset: 0x000D9844
		[MonitoringDescription("ThreadCurrentPriority")]
		public int CurrentPriority
		{
			get
			{
				return this.threadInfo.currentPriority;
			}
		}

		/// <summary>Gets the unique identifier of the thread.</summary>
		/// <returns>The unique identifier associated with a specific thread.</returns>
		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x000DB651 File Offset: 0x000D9851
		[MonitoringDescription("ThreadId")]
		public int Id
		{
			get
			{
				return this.threadInfo.threadId;
			}
		}

		/// <summary>Sets the preferred processor for this thread to run on.</summary>
		/// <returns>The preferred processor for the thread, used when the system schedules threads, to determine which processor to run the thread on.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The system could not set the thread to start on the specified processor.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x17000BE5 RID: 3045
		// (set) Token: 0x0600308A RID: 12426 RVA: 0x000DB660 File Offset: 0x000D9860
		[Browsable(false)]
		public int IdealProcessor
		{
			set
			{
				Microsoft.Win32.SafeHandles.SafeThreadHandle safeThreadHandle = null;
				try
				{
					safeThreadHandle = this.OpenThreadHandle(32);
					if (Microsoft.Win32.NativeMethods.SetThreadIdealProcessor(safeThreadHandle, value) < 0)
					{
						throw new Win32Exception();
					}
				}
				finally
				{
					ProcessThread.CloseThreadHandle(safeThreadHandle);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the operating system should temporarily boost the priority of the associated thread whenever the main window of the thread's process receives the focus.</summary>
		/// <returns>
		///   <see langword="true" /> to boost the thread's priority when the user interacts with the process's interface; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The priority boost information could not be retrieved.  
		///  -or-  
		///  The priority boost information could not be set.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x000DB6A4 File Offset: 0x000D98A4
		// (set) Token: 0x0600308C RID: 12428 RVA: 0x000DB708 File Offset: 0x000D9908
		[MonitoringDescription("ThreadPriorityBoostEnabled")]
		public bool PriorityBoostEnabled
		{
			get
			{
				if (!this.havePriorityBoostEnabled)
				{
					Microsoft.Win32.SafeHandles.SafeThreadHandle safeThreadHandle = null;
					try
					{
						safeThreadHandle = this.OpenThreadHandle(64);
						bool flag = false;
						if (!Microsoft.Win32.NativeMethods.GetThreadPriorityBoost(safeThreadHandle, out flag))
						{
							throw new Win32Exception();
						}
						this.priorityBoostEnabled = !flag;
						this.havePriorityBoostEnabled = true;
					}
					finally
					{
						ProcessThread.CloseThreadHandle(safeThreadHandle);
					}
				}
				return this.priorityBoostEnabled;
			}
			set
			{
				Microsoft.Win32.SafeHandles.SafeThreadHandle safeThreadHandle = null;
				try
				{
					safeThreadHandle = this.OpenThreadHandle(32);
					if (!Microsoft.Win32.NativeMethods.SetThreadPriorityBoost(safeThreadHandle, !value))
					{
						throw new Win32Exception();
					}
					this.priorityBoostEnabled = value;
					this.havePriorityBoostEnabled = true;
				}
				finally
				{
					ProcessThread.CloseThreadHandle(safeThreadHandle);
				}
			}
		}

		/// <summary>Gets or sets the priority level of the thread.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.ThreadPriorityLevel" /> values, specifying a range that bounds the thread's priority.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread priority level information could not be retrieved.  
		///  -or-  
		///  The thread priority level could not be set.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x000DB75C File Offset: 0x000D995C
		// (set) Token: 0x0600308E RID: 12430 RVA: 0x000DB7C0 File Offset: 0x000D99C0
		[MonitoringDescription("ThreadPriorityLevel")]
		public ThreadPriorityLevel PriorityLevel
		{
			get
			{
				if (!this.havePriorityLevel)
				{
					Microsoft.Win32.SafeHandles.SafeThreadHandle safeThreadHandle = null;
					try
					{
						safeThreadHandle = this.OpenThreadHandle(64);
						int threadPriority = Microsoft.Win32.NativeMethods.GetThreadPriority(safeThreadHandle);
						if (threadPriority == 2147483647)
						{
							throw new Win32Exception();
						}
						this.priorityLevel = (ThreadPriorityLevel)threadPriority;
						this.havePriorityLevel = true;
					}
					finally
					{
						ProcessThread.CloseThreadHandle(safeThreadHandle);
					}
				}
				return this.priorityLevel;
			}
			set
			{
				Microsoft.Win32.SafeHandles.SafeThreadHandle safeThreadHandle = null;
				try
				{
					safeThreadHandle = this.OpenThreadHandle(32);
					if (!Microsoft.Win32.NativeMethods.SetThreadPriority(safeThreadHandle, (int)value))
					{
						throw new Win32Exception();
					}
					this.priorityLevel = value;
				}
				finally
				{
					ProcessThread.CloseThreadHandle(safeThreadHandle);
				}
			}
		}

		/// <summary>Gets the amount of time that the thread has spent running code inside the operating system core.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> indicating the amount of time that the thread has spent running code inside the operating system core.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread time could not be retrieved.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x000DB808 File Offset: 0x000D9A08
		[MonitoringDescription("ThreadPrivilegedProcessorTime")]
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.GetThreadTimes().PrivilegedProcessorTime;
			}
		}

		/// <summary>Gets the memory address of the function that the operating system called that started this thread.</summary>
		/// <returns>The thread's starting address, which points to the application-defined function that the thread executes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x000DB81C File Offset: 0x000D9A1C
		[MonitoringDescription("ThreadStartAddress")]
		public IntPtr StartAddress
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.threadInfo.startAddress;
			}
		}

		/// <summary>Gets the time that the operating system started the thread.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing the time that was on the system when the operating system started the thread.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread time could not be retrieved.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x000DB830 File Offset: 0x000D9A30
		[MonitoringDescription("ThreadStartTime")]
		public DateTime StartTime
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.GetThreadTimes().StartTime;
			}
		}

		/// <summary>Gets the current state of this thread.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.ThreadState" /> that indicates the thread's execution, for example, running, waiting, or terminated.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x000DB844 File Offset: 0x000D9A44
		[MonitoringDescription("ThreadThreadState")]
		public ThreadState ThreadState
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.threadInfo.threadState;
			}
		}

		/// <summary>Gets the total amount of time that this thread has spent using the processor.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the thread has had control of the processor.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread time could not be retrieved.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x000DB858 File Offset: 0x000D9A58
		[MonitoringDescription("ThreadTotalProcessorTime")]
		public TimeSpan TotalProcessorTime
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.GetThreadTimes().TotalProcessorTime;
			}
		}

		/// <summary>Gets the amount of time that the associated thread has spent running code inside the application.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> indicating the amount of time that the thread has spent running code inside the application, as opposed to inside the operating system core.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread time could not be retrieved.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x000DB86C File Offset: 0x000D9A6C
		[MonitoringDescription("ThreadUserProcessorTime")]
		public TimeSpan UserProcessorTime
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.GetThreadTimes().UserProcessorTime;
			}
		}

		/// <summary>Gets the reason that the thread is waiting.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.ThreadWaitReason" /> representing the reason that the thread is in the wait state.</returns>
		/// <exception cref="T:System.InvalidOperationException">The thread is not in the wait state.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x000DB880 File Offset: 0x000D9A80
		[MonitoringDescription("ThreadWaitReason")]
		public ThreadWaitReason WaitReason
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				if (this.threadInfo.threadState != ThreadState.Wait)
				{
					throw new InvalidOperationException(SR.GetString("WaitReasonUnavailable"));
				}
				return this.threadInfo.threadWaitReason;
			}
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000DB8B2 File Offset: 0x000D9AB2
		private static void CloseThreadHandle(Microsoft.Win32.SafeHandles.SafeThreadHandle handle)
		{
			if (handle != null)
			{
				handle.Close();
			}
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000DB8C0 File Offset: 0x000D9AC0
		private void EnsureState(ProcessThread.State state)
		{
			if ((state & ProcessThread.State.IsLocal) != (ProcessThread.State)0 && this.isRemoteMachine)
			{
				throw new NotSupportedException(SR.GetString("NotSupportedRemoteThread"));
			}
			if ((state & ProcessThread.State.IsNt) != (ProcessThread.State)0 && Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
			}
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000DB90C File Offset: 0x000D9B0C
		private Microsoft.Win32.SafeHandles.SafeThreadHandle OpenThreadHandle(int access)
		{
			this.EnsureState(ProcessThread.State.IsLocal);
			return ProcessManager.OpenThread(this.threadInfo.threadId, access);
		}

		/// <summary>Resets the ideal processor for this thread to indicate that there is no single ideal processor. In other words, so that any processor is ideal.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The ideal processor could not be reset.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x06003099 RID: 12441 RVA: 0x000DB926 File Offset: 0x000D9B26
		public void ResetIdealProcessor()
		{
			this.IdealProcessor = 32;
		}

		/// <summary>Sets the processors on which the associated thread can run.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that points to a set of bits, each of which represents a processor that the thread can run on.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The processor affinity could not be set.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x17000BEF RID: 3055
		// (set) Token: 0x0600309A RID: 12442 RVA: 0x000DB930 File Offset: 0x000D9B30
		[Browsable(false)]
		public IntPtr ProcessorAffinity
		{
			set
			{
				Microsoft.Win32.SafeHandles.SafeThreadHandle safeThreadHandle = null;
				try
				{
					safeThreadHandle = this.OpenThreadHandle(96);
					if (Microsoft.Win32.NativeMethods.SetThreadAffinityMask(safeThreadHandle, new HandleRef(this, value)) == IntPtr.Zero)
					{
						throw new Win32Exception();
					}
				}
				finally
				{
					ProcessThread.CloseThreadHandle(safeThreadHandle);
				}
			}
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000DB980 File Offset: 0x000D9B80
		private ProcessThreadTimes GetThreadTimes()
		{
			ProcessThreadTimes processThreadTimes = new ProcessThreadTimes();
			Microsoft.Win32.SafeHandles.SafeThreadHandle safeThreadHandle = null;
			try
			{
				safeThreadHandle = this.OpenThreadHandle(64);
				if (!Microsoft.Win32.NativeMethods.GetThreadTimes(safeThreadHandle, out processThreadTimes.create, out processThreadTimes.exit, out processThreadTimes.kernel, out processThreadTimes.user))
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				ProcessThread.CloseThreadHandle(safeThreadHandle);
			}
			return processThreadTimes;
		}

		// Token: 0x0400289A RID: 10394
		private ThreadInfo threadInfo;

		// Token: 0x0400289B RID: 10395
		private bool isRemoteMachine;

		// Token: 0x0400289C RID: 10396
		private bool priorityBoostEnabled;

		// Token: 0x0400289D RID: 10397
		private bool havePriorityBoostEnabled;

		// Token: 0x0400289E RID: 10398
		private ThreadPriorityLevel priorityLevel;

		// Token: 0x0400289F RID: 10399
		private bool havePriorityLevel;

		// Token: 0x02000883 RID: 2179
		private enum State
		{
			// Token: 0x04003778 RID: 14200
			IsLocal = 2,
			// Token: 0x04003779 RID: 14201
			IsNt = 4
		}
	}
}
