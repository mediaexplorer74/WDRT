using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	/// <summary>Provides access to local and remote processes and enables you to start and stop local system processes.</summary>
	// Token: 0x020004EE RID: 1262
	[MonitoringDescription("ProcessDesc")]
	[DefaultEvent("Exited")]
	[DefaultProperty("StartInfo")]
	[Designer("System.Diagnostics.Design.ProcessDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true, Synchronization = true, ExternalProcessMgmt = true, SelfAffectingProcessMgmt = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class Process : Component
	{
		/// <summary>Occurs each time an application writes a line to its redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.</summary>
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06002F93 RID: 12179 RVA: 0x000D6BF4 File Offset: 0x000D4DF4
		// (remove) Token: 0x06002F94 RID: 12180 RVA: 0x000D6C2C File Offset: 0x000D4E2C
		[Browsable(true)]
		[MonitoringDescription("ProcessAssociated")]
		public event DataReceivedEventHandler OutputDataReceived;

		/// <summary>Occurs when an application writes to its redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</summary>
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06002F95 RID: 12181 RVA: 0x000D6C64 File Offset: 0x000D4E64
		// (remove) Token: 0x06002F96 RID: 12182 RVA: 0x000D6C9C File Offset: 0x000D4E9C
		[Browsable(true)]
		[MonitoringDescription("ProcessAssociated")]
		public event DataReceivedEventHandler ErrorDataReceived;

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Process" /> class.</summary>
		// Token: 0x06002F97 RID: 12183 RVA: 0x000D6CD1 File Offset: 0x000D4ED1
		public Process()
		{
			this.machineName = ".";
			this.outputStreamReadMode = Process.StreamReadMode.undefined;
			this.errorStreamReadMode = Process.StreamReadMode.undefined;
			this.m_processAccess = 2035711;
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000D6D00 File Offset: 0x000D4F00
		private Process(string machineName, bool isRemoteMachine, int processId, ProcessInfo processInfo)
		{
			this.processInfo = processInfo;
			this.machineName = machineName;
			this.isRemoteMachine = isRemoteMachine;
			this.processId = processId;
			this.haveProcessId = true;
			this.outputStreamReadMode = Process.StreamReadMode.undefined;
			this.errorStreamReadMode = Process.StreamReadMode.undefined;
			this.m_processAccess = 2035711;
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x000D6D50 File Offset: 0x000D4F50
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessAssociated")]
		private bool Associated
		{
			get
			{
				return this.haveProcessId || this.haveProcessHandle;
			}
		}

		/// <summary>Gets the base priority of the associated process.</summary>
		/// <returns>The base priority, which is computed from the <see cref="P:System.Diagnostics.Process.PriorityClass" /> of the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has exited.  
		///  -or-  
		///  The process has not started, so there is no process ID.</exception>
		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06002F9A RID: 12186 RVA: 0x000D6D62 File Offset: 0x000D4F62
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessBasePriority")]
		public int BasePriority
		{
			get
			{
				this.EnsureState(Process.State.HaveProcessInfo);
				return this.processInfo.basePriority;
			}
		}

		/// <summary>Gets the value that the associated process specified when it terminated.</summary>
		/// <returns>The code that the associated process specified when it terminated.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process has not exited.  
		///  -or-  
		///  The process <see cref="P:System.Diagnostics.Process.Handle" /> is not valid.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.ExitCode" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06002F9B RID: 12187 RVA: 0x000D6D76 File Offset: 0x000D4F76
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessExitCode")]
		public int ExitCode
		{
			get
			{
				this.EnsureState(Process.State.Exited);
				return this.exitCode;
			}
		}

		/// <summary>Gets a value indicating whether the associated process has been terminated.</summary>
		/// <returns>
		///   <see langword="true" /> if the operating system process referenced by the <see cref="T:System.Diagnostics.Process" /> component has terminated; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with the object.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The exit code for the process could not be retrieved.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.HasExited" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06002F9C RID: 12188 RVA: 0x000D6D88 File Offset: 0x000D4F88
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessTerminated")]
		public bool HasExited
		{
			get
			{
				if (!this.exited)
				{
					this.EnsureState(Process.State.Associated);
					Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = null;
					try
					{
						safeProcessHandle = this.GetProcessHandle(1049600, false);
						int num;
						if (safeProcessHandle.IsInvalid)
						{
							this.exited = true;
						}
						else if (Microsoft.Win32.NativeMethods.GetExitCodeProcess(safeProcessHandle, out num) && num != 259)
						{
							this.exited = true;
							this.exitCode = num;
						}
						else
						{
							if (!this.signaled)
							{
								ProcessWaitHandle processWaitHandle = null;
								try
								{
									processWaitHandle = new ProcessWaitHandle(safeProcessHandle);
									this.signaled = processWaitHandle.WaitOne(0, false);
								}
								finally
								{
									if (processWaitHandle != null)
									{
										processWaitHandle.Close();
									}
								}
							}
							if (this.signaled)
							{
								if (!Microsoft.Win32.NativeMethods.GetExitCodeProcess(safeProcessHandle, out num))
								{
									throw new Win32Exception();
								}
								this.exited = true;
								this.exitCode = num;
							}
						}
					}
					finally
					{
						this.ReleaseProcessHandle(safeProcessHandle);
					}
					if (this.exited)
					{
						this.RaiseOnExited();
					}
				}
				return this.exited;
			}
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000D6E78 File Offset: 0x000D5078
		private ProcessThreadTimes GetProcessTimes()
		{
			ProcessThreadTimes processThreadTimes = new ProcessThreadTimes();
			Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = null;
			try
			{
				int num = 1024;
				if (EnvironmentHelpers.IsWindowsVistaOrAbove())
				{
					num = 4096;
				}
				safeProcessHandle = this.GetProcessHandle(num, false);
				if (safeProcessHandle.IsInvalid)
				{
					throw new InvalidOperationException(SR.GetString("ProcessHasExited", new object[] { this.processId.ToString(CultureInfo.CurrentCulture) }));
				}
				if (!Microsoft.Win32.NativeMethods.GetProcessTimes(safeProcessHandle, out processThreadTimes.create, out processThreadTimes.exit, out processThreadTimes.kernel, out processThreadTimes.user))
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
			return processThreadTimes;
		}

		/// <summary>Gets the time that the associated process exited.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that indicates when the associated process was terminated.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.ExitTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x000D6F1C File Offset: 0x000D511C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessExitTime")]
		public DateTime ExitTime
		{
			get
			{
				if (!this.haveExitTime)
				{
					this.EnsureState((Process.State)20);
					this.exitTime = this.GetProcessTimes().ExitTime;
					this.haveExitTime = true;
				}
				return this.exitTime;
			}
		}

		/// <summary>Gets the native handle of the associated process.</summary>
		/// <returns>The handle that the operating system assigned to the associated process when the process was started. The system uses this handle to keep track of process attributes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process has not been started or has exited. The <see cref="P:System.Diagnostics.Process.Handle" /> property cannot be read because there is no process associated with this <see cref="T:System.Diagnostics.Process" /> instance.  
		///  -or-  
		///  The <see cref="T:System.Diagnostics.Process" /> instance has been attached to a running process but you do not have the necessary permissions to get a handle with full access rights.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.Handle" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x000D6F4C File Offset: 0x000D514C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessHandle")]
		public IntPtr Handle
		{
			get
			{
				this.EnsureState(Process.State.Associated);
				return this.OpenProcessHandle(this.m_processAccess).DangerousGetHandle();
			}
		}

		/// <summary>Gets the native handle to this process.</summary>
		/// <returns>The native handle to this process.</returns>
		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x000D6F67 File Offset: 0x000D5167
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Microsoft.Win32.SafeHandles.SafeProcessHandle SafeHandle
		{
			get
			{
				this.EnsureState(Process.State.Associated);
				return this.OpenProcessHandle(this.m_processAccess);
			}
		}

		/// <summary>Gets the number of handles opened by the process.</summary>
		/// <returns>The number of operating system handles the process has opened.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x000D6F7D File Offset: 0x000D517D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessHandleCount")]
		public int HandleCount
		{
			get
			{
				this.EnsureState(Process.State.HaveProcessInfo);
				return this.processInfo.handleCount;
			}
		}

		/// <summary>Gets the unique identifier for the associated process.</summary>
		/// <returns>The system-generated unique identifier of the process that is referenced by this <see cref="T:System.Diagnostics.Process" /> instance.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process's <see cref="P:System.Diagnostics.Process.Id" /> property has not been set.  
		///  -or-  
		///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x000D6F91 File Offset: 0x000D5191
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessId")]
		public int Id
		{
			get
			{
				this.EnsureState(Process.State.HaveId);
				return this.processId;
			}
		}

		/// <summary>Gets the name of the computer the associated process is running on.</summary>
		/// <returns>The name of the computer that the associated process is running on.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000D6FA0 File Offset: 0x000D51A0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessMachineName")]
		public string MachineName
		{
			get
			{
				this.EnsureState(Process.State.Associated);
				return this.machineName;
			}
		}

		/// <summary>Gets the window handle of the main window of the associated process.</summary>
		/// <returns>The system-generated window handle of the main window of the associated process.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.MainWindowHandle" /> is not defined because the process has exited.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MainWindowHandle" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x000D6FB0 File Offset: 0x000D51B0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessMainWindowHandle")]
		public IntPtr MainWindowHandle
		{
			get
			{
				if (!this.haveMainWindow)
				{
					this.EnsureState((Process.State)3);
					this.mainWindowHandle = ProcessManager.GetMainWindowHandle(this.processId);
					if (this.mainWindowHandle != (IntPtr)0)
					{
						this.haveMainWindow = true;
					}
					else
					{
						this.EnsureState(Process.State.HaveProcessInfo);
					}
				}
				return this.mainWindowHandle;
			}
		}

		/// <summary>Gets the caption of the main window of the process.</summary>
		/// <returns>The main window title of the process.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.MainWindowTitle" /> property is not defined because the process has exited.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MainWindowTitle" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06002FA5 RID: 12197 RVA: 0x000D7008 File Offset: 0x000D5208
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessMainWindowTitle")]
		public string MainWindowTitle
		{
			get
			{
				if (this.mainWindowTitle == null)
				{
					IntPtr intPtr = this.MainWindowHandle;
					if (intPtr == (IntPtr)0)
					{
						this.mainWindowTitle = string.Empty;
					}
					else
					{
						int num = Microsoft.Win32.NativeMethods.GetWindowTextLength(new HandleRef(this, intPtr)) * 2;
						StringBuilder stringBuilder = new StringBuilder(num);
						Microsoft.Win32.NativeMethods.GetWindowText(new HandleRef(this, intPtr), stringBuilder, stringBuilder.Capacity);
						this.mainWindowTitle = stringBuilder.ToString();
					}
				}
				return this.mainWindowTitle;
			}
		}

		/// <summary>Gets the main module for the associated process.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.ProcessModule" /> that was used to start the process.</returns>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MainModule" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A 32-bit process is trying to access the modules of a 64-bit process.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.  
		///  -or-  
		///  The process has exited.</exception>
		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x000D707C File Offset: 0x000D527C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessMainModule")]
		public ProcessModule MainModule
		{
			get
			{
				if (this.OperatingSystem.Platform == PlatformID.Win32NT)
				{
					this.EnsureState((Process.State)3);
					ModuleInfo firstModuleInfo = NtProcessManager.GetFirstModuleInfo(this.processId);
					return new ProcessModule(firstModuleInfo);
				}
				ProcessModuleCollection processModuleCollection = this.Modules;
				this.EnsureState(Process.State.HaveProcessInfo);
				foreach (object obj in processModuleCollection)
				{
					ProcessModule processModule = (ProcessModule)obj;
					if (processModule.moduleInfo.Id == this.processInfo.mainModuleId)
					{
						return processModule;
					}
				}
				return null;
			}
		}

		/// <summary>Gets or sets the maximum allowable working set size, in bytes, for the associated process.</summary>
		/// <returns>The maximum working set size that is allowed in memory for the process, in bytes.</returns>
		/// <exception cref="T:System.ArgumentException">The maximum working set size is invalid. It must be greater than or equal to the minimum working set size.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Working set information cannot be retrieved from the associated process resource.  
		///  -or-  
		///  The process identifier or process handle is zero because the process has not been started.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MaxWorkingSet" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.  
		///  -or-  
		///  The process has exited.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x000D7124 File Offset: 0x000D5324
		// (set) Token: 0x06002FA8 RID: 12200 RVA: 0x000D7132 File Offset: 0x000D5332
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessMaxWorkingSet")]
		public IntPtr MaxWorkingSet
		{
			get
			{
				this.EnsureWorkingSetLimits();
				return this.maxWorkingSet;
			}
			set
			{
				this.SetWorkingSetLimits(null, value);
			}
		}

		/// <summary>Gets or sets the minimum allowable working set size, in bytes, for the associated process.</summary>
		/// <returns>The minimum working set size that is required in memory for the process, in bytes.</returns>
		/// <exception cref="T:System.ArgumentException">The minimum working set size is invalid. It must be less than or equal to the maximum working set size.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Working set information cannot be retrieved from the associated process resource.  
		///  -or-  
		///  The process identifier or process handle is zero because the process has not been started.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MinWorkingSet" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.  
		///  -or-  
		///  The process has exited.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06002FA9 RID: 12201 RVA: 0x000D7141 File Offset: 0x000D5341
		// (set) Token: 0x06002FAA RID: 12202 RVA: 0x000D714F File Offset: 0x000D534F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessMinWorkingSet")]
		public IntPtr MinWorkingSet
		{
			get
			{
				this.EnsureWorkingSetLimits();
				return this.minWorkingSet;
			}
			set
			{
				this.SetWorkingSetLimits(value, null);
			}
		}

		/// <summary>Gets the modules that have been loaded by the associated process.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.ProcessModule" /> that represents the modules that have been loaded by the associated process.</returns>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.Modules" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">You are attempting to access the <see cref="P:System.Diagnostics.Process.Modules" /> property for either the system process or the idle process. These processes do not have modules.</exception>
		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x000D7160 File Offset: 0x000D5360
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessModules")]
		public ProcessModuleCollection Modules
		{
			get
			{
				if (this.modules == null)
				{
					this.EnsureState((Process.State)3);
					ModuleInfo[] moduleInfos = ProcessManager.GetModuleInfos(this.processId);
					ProcessModule[] array = new ProcessModule[moduleInfos.Length];
					for (int i = 0; i < moduleInfos.Length; i++)
					{
						array[i] = new ProcessModule(moduleInfos[i]);
					}
					ProcessModuleCollection processModuleCollection = new ProcessModuleCollection(array);
					this.modules = processModuleCollection;
				}
				return this.modules;
			}
		}

		/// <summary>Gets the amount of nonpaged system memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, the system has allocated for the associated process that cannot be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x000D71BE File Offset: 0x000D53BE
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.NonpagedSystemMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessNonpagedSystemMemorySize")]
		public int NonpagedSystemMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.poolNonpagedBytes;
			}
		}

		/// <summary>Gets the amount of nonpaged system memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of system memory, in bytes, allocated for the associated process that cannot be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06002FAD RID: 12205 RVA: 0x000D71D4 File Offset: 0x000D53D4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessNonpagedSystemMemorySize")]
		[ComVisible(false)]
		public long NonpagedSystemMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.poolNonpagedBytes;
			}
		}

		/// <summary>Gets the amount of paged memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, allocated by the associated process that can be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x000D71E9 File Offset: 0x000D53E9
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PagedMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPagedMemorySize")]
		public int PagedMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.pageFileBytes;
			}
		}

		/// <summary>Gets the amount of paged memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, allocated in the virtual memory paging file for the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x000D71FF File Offset: 0x000D53FF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPagedMemorySize")]
		[ComVisible(false)]
		public long PagedMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.pageFileBytes;
			}
		}

		/// <summary>Gets the amount of pageable system memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, the system has allocated for the associated process that can be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x000D7214 File Offset: 0x000D5414
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PagedSystemMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPagedSystemMemorySize")]
		public int PagedSystemMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.poolPagedBytes;
			}
		}

		/// <summary>Gets the amount of pageable system memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of system memory, in bytes, allocated for the associated process that can be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x000D722A File Offset: 0x000D542A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPagedSystemMemorySize")]
		[ComVisible(false)]
		public long PagedSystemMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.poolPagedBytes;
			}
		}

		/// <summary>Gets the maximum amount of memory in the virtual memory paging file, in bytes, used by the associated process.</summary>
		/// <returns>The maximum amount of memory, in bytes, allocated by the associated process that could be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x000D723F File Offset: 0x000D543F
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PeakPagedMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPeakPagedMemorySize")]
		public int PeakPagedMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.pageFileBytesPeak;
			}
		}

		/// <summary>Gets the maximum amount of memory in the virtual memory paging file, in bytes, used by the associated process.</summary>
		/// <returns>The maximum amount of memory, in bytes, allocated in the virtual memory paging file for the associated process since it was started.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x000D7255 File Offset: 0x000D5455
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPeakPagedMemorySize")]
		[ComVisible(false)]
		public long PeakPagedMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.pageFileBytesPeak;
			}
		}

		/// <summary>Gets the peak working set size for the associated process, in bytes.</summary>
		/// <returns>The maximum amount of physical memory that the associated process has required all at once, in bytes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x000D726A File Offset: 0x000D546A
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PeakWorkingSet64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPeakWorkingSet")]
		public int PeakWorkingSet
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.workingSetPeak;
			}
		}

		/// <summary>Gets the maximum amount of physical memory, in bytes, used by the associated process.</summary>
		/// <returns>The maximum amount of physical memory, in bytes, allocated for the associated process since it was started.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x000D7280 File Offset: 0x000D5480
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPeakWorkingSet")]
		[ComVisible(false)]
		public long PeakWorkingSet64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.workingSetPeak;
			}
		}

		/// <summary>Gets the maximum amount of virtual memory, in bytes, used by the associated process.</summary>
		/// <returns>The maximum amount of virtual memory, in bytes, that the associated process has requested.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x000D7295 File Offset: 0x000D5495
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PeakVirtualMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPeakVirtualMemorySize")]
		public int PeakVirtualMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.virtualBytesPeak;
			}
		}

		/// <summary>Gets the maximum amount of virtual memory, in bytes, used by the associated process.</summary>
		/// <returns>The maximum amount of virtual memory, in bytes, allocated for the associated process since it was started.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x000D72AB File Offset: 0x000D54AB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPeakVirtualMemorySize")]
		[ComVisible(false)]
		public long PeakVirtualMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.virtualBytesPeak;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000D72C0 File Offset: 0x000D54C0
		private OperatingSystem OperatingSystem
		{
			get
			{
				if (this.operatingSystem == null)
				{
					this.operatingSystem = Environment.OSVersion;
				}
				return this.operatingSystem;
			}
		}

		/// <summary>Gets or sets a value indicating whether the associated process priority should temporarily be boosted by the operating system when the main window has the focus.</summary>
		/// <returns>
		///   <see langword="true" /> if dynamic boosting of the process priority should take place for a process when it is taken out of the wait state; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Priority boost information could not be retrieved from the associated process resource.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.  
		///  -or-  
		///  The process identifier or process handle is zero. (The process has not been started.)</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.PriorityBoostEnabled" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.</exception>
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x000D72DC File Offset: 0x000D54DC
		// (set) Token: 0x06002FBA RID: 12218 RVA: 0x000D734C File Offset: 0x000D554C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPriorityBoostEnabled")]
		public bool PriorityBoostEnabled
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				if (!this.havePriorityBoostEnabled)
				{
					Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = null;
					try
					{
						safeProcessHandle = this.GetProcessHandle(1024);
						bool flag = false;
						if (!Microsoft.Win32.NativeMethods.GetProcessPriorityBoost(safeProcessHandle, out flag))
						{
							throw new Win32Exception();
						}
						this.priorityBoostEnabled = !flag;
						this.havePriorityBoostEnabled = true;
					}
					finally
					{
						this.ReleaseProcessHandle(safeProcessHandle);
					}
				}
				return this.priorityBoostEnabled;
			}
			set
			{
				this.EnsureState(Process.State.IsNt);
				Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = null;
				try
				{
					safeProcessHandle = this.GetProcessHandle(512);
					if (!Microsoft.Win32.NativeMethods.SetProcessPriorityBoost(safeProcessHandle, !value))
					{
						throw new Win32Exception();
					}
					this.priorityBoostEnabled = value;
					this.havePriorityBoostEnabled = true;
				}
				finally
				{
					this.ReleaseProcessHandle(safeProcessHandle);
				}
			}
		}

		/// <summary>Gets or sets the overall priority category for the associated process.</summary>
		/// <returns>The priority category for the associated process, from which the <see cref="P:System.Diagnostics.Process.BasePriority" /> of the process is calculated.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Process priority information could not be set or retrieved from the associated process resource.  
		///  -or-  
		///  The process identifier or process handle is zero. (The process has not been started.)</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.PriorityClass" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">You have set the <see cref="P:System.Diagnostics.Process.PriorityClass" /> to <see langword="AboveNormal" /> or <see langword="BelowNormal" /> when using Windows 98 or Windows Millennium Edition (Windows Me). These platforms do not support those values for the priority class.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Priority class cannot be set because it does not use a valid value, as defined in the <see cref="T:System.Diagnostics.ProcessPriorityClass" /> enumeration.</exception>
		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x000D73A8 File Offset: 0x000D55A8
		// (set) Token: 0x06002FBC RID: 12220 RVA: 0x000D740C File Offset: 0x000D560C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPriorityClass")]
		public ProcessPriorityClass PriorityClass
		{
			get
			{
				if (!this.havePriorityClass)
				{
					Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = null;
					try
					{
						safeProcessHandle = this.GetProcessHandle(1024);
						int num = Microsoft.Win32.NativeMethods.GetPriorityClass(safeProcessHandle);
						if (num == 0)
						{
							throw new Win32Exception();
						}
						this.priorityClass = (ProcessPriorityClass)num;
						this.havePriorityClass = true;
					}
					finally
					{
						this.ReleaseProcessHandle(safeProcessHandle);
					}
				}
				return this.priorityClass;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ProcessPriorityClass), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ProcessPriorityClass));
				}
				if ((value & (ProcessPriorityClass)49152) != (ProcessPriorityClass)0 && (this.OperatingSystem.Platform != PlatformID.Win32NT || this.OperatingSystem.Version.Major < 5))
				{
					throw new PlatformNotSupportedException(SR.GetString("PriorityClassNotSupported"), null);
				}
				Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = null;
				try
				{
					safeProcessHandle = this.GetProcessHandle(512);
					if (!Microsoft.Win32.NativeMethods.SetPriorityClass(safeProcessHandle, (int)value))
					{
						throw new Win32Exception();
					}
					this.priorityClass = value;
					this.havePriorityClass = true;
				}
				finally
				{
					this.ReleaseProcessHandle(safeProcessHandle);
				}
			}
		}

		/// <summary>Gets the amount of private memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The number of bytes allocated by the associated process that cannot be shared with other processes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x000D74C8 File Offset: 0x000D56C8
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PrivateMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPrivateMemorySize")]
		public int PrivateMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.privateBytes;
			}
		}

		/// <summary>Gets the amount of private memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, allocated for the associated process that cannot be shared with other processes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06002FBE RID: 12222 RVA: 0x000D74DE File Offset: 0x000D56DE
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPrivateMemorySize")]
		[ComVisible(false)]
		public long PrivateMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.privateBytes;
			}
		}

		/// <summary>Gets the privileged processor time for this process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the process has spent running code inside the operating system core.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.PrivilegedProcessorTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06002FBF RID: 12223 RVA: 0x000D74F3 File Offset: 0x000D56F3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPrivilegedProcessorTime")]
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().PrivilegedProcessorTime;
			}
		}

		/// <summary>Gets the name of the process.</summary>
		/// <returns>The name that the system uses to identify the process to the user.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process does not have an identifier, or no process is associated with the <see cref="T:System.Diagnostics.Process" />.  
		///  -or-  
		///  The associated process has exited.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is not on this computer.</exception>
		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x000D7508 File Offset: 0x000D5708
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessProcessName")]
		public string ProcessName
		{
			get
			{
				this.EnsureState(Process.State.HaveProcessInfo);
				string processName = this.processInfo.processName;
				if (processName.Length == 15 && ProcessManager.IsNt && ProcessManager.IsOSOlderThanXP && !this.isRemoteMachine)
				{
					try
					{
						string moduleName = this.MainModule.ModuleName;
						if (moduleName != null)
						{
							this.processInfo.processName = Path.ChangeExtension(Path.GetFileName(moduleName), null);
						}
					}
					catch (Exception)
					{
					}
				}
				return this.processInfo.processName;
			}
		}

		/// <summary>Gets or sets the processors on which the threads in this process can be scheduled to run.</summary>
		/// <returns>A bitmask representing the processors that the threads in the associated process can run on. The default depends on the number of processors on the computer. The default value is 2 n -1, where n is the number of processors.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">
		///   <see cref="P:System.Diagnostics.Process.ProcessorAffinity" /> information could not be set or retrieved from the associated process resource.  
		/// -or-  
		/// The process identifier or process handle is zero. (The process has not been started.)</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.ProcessorAffinity" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> was not available.  
		///  -or-  
		///  The process has exited.</exception>
		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000D7590 File Offset: 0x000D5790
		// (set) Token: 0x06002FC2 RID: 12226 RVA: 0x000D75F4 File Offset: 0x000D57F4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessProcessorAffinity")]
		public IntPtr ProcessorAffinity
		{
			get
			{
				if (!this.haveProcessorAffinity)
				{
					Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = null;
					try
					{
						safeProcessHandle = this.GetProcessHandle(1024);
						IntPtr intPtr;
						IntPtr intPtr2;
						if (!Microsoft.Win32.NativeMethods.GetProcessAffinityMask(safeProcessHandle, out intPtr, out intPtr2))
						{
							throw new Win32Exception();
						}
						this.processorAffinity = intPtr;
					}
					finally
					{
						this.ReleaseProcessHandle(safeProcessHandle);
					}
					this.haveProcessorAffinity = true;
				}
				return this.processorAffinity;
			}
			set
			{
				Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = null;
				try
				{
					safeProcessHandle = this.GetProcessHandle(512);
					if (!Microsoft.Win32.NativeMethods.SetProcessAffinityMask(safeProcessHandle, value))
					{
						throw new Win32Exception();
					}
					this.processorAffinity = value;
					this.haveProcessorAffinity = true;
				}
				finally
				{
					this.ReleaseProcessHandle(safeProcessHandle);
				}
			}
		}

		/// <summary>Gets a value indicating whether the user interface of the process is responding.</summary>
		/// <returns>
		///   <see langword="true" /> if the user interface of the associated process is responding to the system; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.Responding" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000D7648 File Offset: 0x000D5848
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessResponding")]
		public bool Responding
		{
			get
			{
				if (!this.haveResponding)
				{
					IntPtr intPtr = this.MainWindowHandle;
					if (intPtr == (IntPtr)0)
					{
						this.responding = true;
					}
					else
					{
						IntPtr intPtr2;
						this.responding = Microsoft.Win32.NativeMethods.SendMessageTimeout(new HandleRef(this, intPtr), 0, IntPtr.Zero, IntPtr.Zero, 2, 5000, out intPtr2) != (IntPtr)0;
					}
				}
				return this.responding;
			}
		}

		/// <summary>Gets the Terminal Services session identifier for the associated process.</summary>
		/// <returns>The Terminal Services session identifier for the associated process.</returns>
		/// <exception cref="T:System.NullReferenceException">There is no session associated with this process.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with this session identifier.  
		///  -or-  
		///  The associated process is not on this machine.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="P:System.Diagnostics.Process.SessionId" /> property is not supported on Windows 98.</exception>
		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06002FC4 RID: 12228 RVA: 0x000D76B1 File Offset: 0x000D58B1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessSessionId")]
		public int SessionId
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.sessionId;
			}
		}

		/// <summary>Gets or sets the properties to pass to the <see cref="M:System.Diagnostics.Process.Start" /> method of the <see cref="T:System.Diagnostics.Process" />.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.ProcessStartInfo" /> that represents the data with which to start the process. These arguments include the name of the executable file or document used to start the process.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value that specifies the <see cref="P:System.Diagnostics.Process.StartInfo" /> is <see langword="null" />.</exception>
		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x000D76C6 File Offset: 0x000D58C6
		// (set) Token: 0x06002FC6 RID: 12230 RVA: 0x000D76E2 File Offset: 0x000D58E2
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MonitoringDescription("ProcessStartInfo")]
		public ProcessStartInfo StartInfo
		{
			get
			{
				if (this.startInfo == null)
				{
					this.startInfo = new ProcessStartInfo(this);
				}
				return this.startInfo;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.startInfo = value;
			}
		}

		/// <summary>Gets the time that the associated process was started.</summary>
		/// <returns>An object  that indicates when the process started. An exception is thrown if the process is not running.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.StartTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has exited.  
		///  -or-  
		///  The process has not been started.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred in the call to the Windows function.</exception>
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06002FC7 RID: 12231 RVA: 0x000D76F9 File Offset: 0x000D58F9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessStartTime")]
		public DateTime StartTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().StartTime;
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls that are issued as a result of a process exit event.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> used to marshal event handler calls that are issued as a result of an <see cref="E:System.Diagnostics.Process.Exited" /> event on the process.</returns>
		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002FC8 RID: 12232 RVA: 0x000D7710 File Offset: 0x000D5910
		// (set) Token: 0x06002FC9 RID: 12233 RVA: 0x000D776A File Offset: 0x000D596A
		[Browsable(false)]
		[DefaultValue(null)]
		[MonitoringDescription("ProcessSynchronizingObject")]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				if (this.synchronizingObject == null && base.DesignMode)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						object rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is ISynchronizeInvoke)
						{
							this.synchronizingObject = (ISynchronizeInvoke)rootComponent;
						}
					}
				}
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Gets the set of threads that are running in the associated process.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.ProcessThread" /> representing the operating system threads currently running in the associated process.</returns>
		/// <exception cref="T:System.SystemException">The process does not have an <see cref="P:System.Diagnostics.Process.Id" />, or no process is associated with the <see cref="T:System.Diagnostics.Process" /> instance.  
		///  -or-  
		///  The associated process has exited.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06002FCA RID: 12234 RVA: 0x000D7774 File Offset: 0x000D5974
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessThreads")]
		public ProcessThreadCollection Threads
		{
			get
			{
				if (this.threads == null)
				{
					this.EnsureState(Process.State.HaveProcessInfo);
					int count = this.processInfo.threadInfoList.Count;
					ProcessThread[] array = new ProcessThread[count];
					for (int i = 0; i < count; i++)
					{
						array[i] = new ProcessThread(this.isRemoteMachine, (ThreadInfo)this.processInfo.threadInfoList[i]);
					}
					ProcessThreadCollection processThreadCollection = new ProcessThreadCollection(array);
					this.threads = processThreadCollection;
				}
				return this.threads;
			}
		}

		/// <summary>Gets the total processor time for this process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the associated process has spent utilizing the CPU. This value is the sum of the <see cref="P:System.Diagnostics.Process.UserProcessorTime" /> and the <see cref="P:System.Diagnostics.Process.PrivilegedProcessorTime" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.TotalProcessorTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002FCB RID: 12235 RVA: 0x000D77EC File Offset: 0x000D59EC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessTotalProcessorTime")]
		public TimeSpan TotalProcessorTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().TotalProcessorTime;
			}
		}

		/// <summary>Gets the user processor time for this process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the associated process has spent running code inside the application portion of the process (not inside the operating system core).</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.UserProcessorTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x000D7800 File Offset: 0x000D5A00
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessUserProcessorTime")]
		public TimeSpan UserProcessorTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().UserProcessorTime;
			}
		}

		/// <summary>Gets the size of the process's virtual memory, in bytes.</summary>
		/// <returns>The amount of virtual memory, in bytes, that the associated process has requested.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x000D7814 File Offset: 0x000D5A14
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.VirtualMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessVirtualMemorySize")]
		public int VirtualMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.virtualBytes;
			}
		}

		/// <summary>Gets the amount of the virtual memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of virtual memory, in bytes, allocated for the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06002FCE RID: 12238 RVA: 0x000D782A File Offset: 0x000D5A2A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessVirtualMemorySize")]
		[ComVisible(false)]
		public long VirtualMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.virtualBytes;
			}
		}

		/// <summary>Gets or sets whether the <see cref="E:System.Diagnostics.Process.Exited" /> event should be raised when the process terminates.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="E:System.Diagnostics.Process.Exited" /> event should be raised when the associated process is terminated (through either an exit or a call to <see cref="M:System.Diagnostics.Process.Kill" />); otherwise, <see langword="false" />. The default is <see langword="false" />. Note that the <see cref="E:System.Diagnostics.Process.Exited" /> event is raised even if the value of <see cref="P:System.Diagnostics.Process.EnableRaisingEvents" /> is <see langword="false" /> when the process exits during or before the user performs a <see cref="P:System.Diagnostics.Process.HasExited" /> check.</returns>
		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x000D783F File Offset: 0x000D5A3F
		// (set) Token: 0x06002FD0 RID: 12240 RVA: 0x000D7847 File Offset: 0x000D5A47
		[Browsable(false)]
		[DefaultValue(false)]
		[MonitoringDescription("ProcessEnableRaisingEvents")]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.watchForExit;
			}
			set
			{
				if (value != this.watchForExit)
				{
					if (this.Associated)
					{
						if (value)
						{
							this.OpenProcessHandle();
							this.EnsureWatchingForExit();
						}
						else
						{
							this.StopWatchingForExit();
						}
					}
					this.watchForExit = value;
				}
			}
		}

		/// <summary>Gets a stream used to write the input of the application.</summary>
		/// <returns>A <see cref="T:System.IO.StreamWriter" /> that can be used to write the standard input stream of the application.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardInput" /> stream has not been defined because <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" /> is set to <see langword="false" />.</exception>
		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x000D7879 File Offset: 0x000D5A79
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessStandardInput")]
		public StreamWriter StandardInput
		{
			get
			{
				if (this.standardInput == null)
				{
					throw new InvalidOperationException(SR.GetString("CantGetStandardIn"));
				}
				return this.standardInput;
			}
		}

		/// <summary>Gets a stream used to read the textual output of the application.</summary>
		/// <returns>A <see cref="T:System.IO.StreamReader" /> that can be used to read the standard output stream of the application.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream has not been defined for redirection; ensure <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" /> is set to <see langword="true" /> and <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is set to <see langword="false" />.  
		/// -or-
		///  The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream has been opened for asynchronous read operations with <see cref="M:System.Diagnostics.Process.BeginOutputReadLine" />.</exception>
		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x000D789C File Offset: 0x000D5A9C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessStandardOutput")]
		public StreamReader StandardOutput
		{
			get
			{
				if (this.standardOutput == null)
				{
					throw new InvalidOperationException(SR.GetString("CantGetStandardOut"));
				}
				if (this.outputStreamReadMode == Process.StreamReadMode.undefined)
				{
					this.outputStreamReadMode = Process.StreamReadMode.syncMode;
				}
				else if (this.outputStreamReadMode != Process.StreamReadMode.syncMode)
				{
					throw new InvalidOperationException(SR.GetString("CantMixSyncAsyncOperation"));
				}
				return this.standardOutput;
			}
		}

		/// <summary>Gets a stream used to read the error output of the application.</summary>
		/// <returns>A <see cref="T:System.IO.StreamReader" /> that can be used to read the standard error stream of the application.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardError" /> stream has not been defined for redirection; ensure <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> is set to <see langword="true" /> and <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is set to <see langword="false" />.  
		/// -or-
		///  The <see cref="P:System.Diagnostics.Process.StandardError" /> stream has been opened for asynchronous read operations with <see cref="M:System.Diagnostics.Process.BeginErrorReadLine" />.</exception>
		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000D78F4 File Offset: 0x000D5AF4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessStandardError")]
		public StreamReader StandardError
		{
			get
			{
				if (this.standardError == null)
				{
					throw new InvalidOperationException(SR.GetString("CantGetStandardError"));
				}
				if (this.errorStreamReadMode == Process.StreamReadMode.undefined)
				{
					this.errorStreamReadMode = Process.StreamReadMode.syncMode;
				}
				else if (this.errorStreamReadMode != Process.StreamReadMode.syncMode)
				{
					throw new InvalidOperationException(SR.GetString("CantMixSyncAsyncOperation"));
				}
				return this.standardError;
			}
		}

		/// <summary>Gets the associated process's physical memory usage, in bytes.</summary>
		/// <returns>The total amount of physical memory the associated process is using, in bytes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x000D7949 File Offset: 0x000D5B49
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.WorkingSet64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessWorkingSet")]
		public int WorkingSet
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.workingSet;
			}
		}

		/// <summary>Gets the amount of physical memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of physical memory, in bytes, allocated for the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000D795F File Offset: 0x000D5B5F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessWorkingSet")]
		[ComVisible(false)]
		public long WorkingSet64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.workingSet;
			}
		}

		/// <summary>Occurs when a process exits.</summary>
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06002FD6 RID: 12246 RVA: 0x000D7974 File Offset: 0x000D5B74
		// (remove) Token: 0x06002FD7 RID: 12247 RVA: 0x000D798D File Offset: 0x000D5B8D
		[Category("Behavior")]
		[MonitoringDescription("ProcessExited")]
		public event EventHandler Exited
		{
			add
			{
				this.onExited = (EventHandler)Delegate.Combine(this.onExited, value);
			}
			remove
			{
				this.onExited = (EventHandler)Delegate.Remove(this.onExited, value);
			}
		}

		/// <summary>Closes a process that has a user interface by sending a close message to its main window.</summary>
		/// <returns>
		///   <see langword="true" /> if the close message was successfully sent; <see langword="false" /> if the associated process does not have a main window or if the main window is disabled (for example if a modal dialog is being shown).</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has already exited.  
		///  -or-  
		///  No process is associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		// Token: 0x06002FD8 RID: 12248 RVA: 0x000D79A8 File Offset: 0x000D5BA8
		public bool CloseMainWindow()
		{
			IntPtr intPtr = this.MainWindowHandle;
			if (intPtr == (IntPtr)0)
			{
				return false;
			}
			int windowLong = Microsoft.Win32.NativeMethods.GetWindowLong(new HandleRef(this, intPtr), -16);
			if ((windowLong & 134217728) != 0)
			{
				return false;
			}
			Microsoft.Win32.NativeMethods.PostMessage(new HandleRef(this, intPtr), 16, IntPtr.Zero, IntPtr.Zero);
			return true;
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000D7A00 File Offset: 0x000D5C00
		private void ReleaseProcessHandle(Microsoft.Win32.SafeHandles.SafeProcessHandle handle)
		{
			if (handle == null)
			{
				return;
			}
			if (this.haveProcessHandle && handle == this.m_processHandle)
			{
				return;
			}
			handle.Close();
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000D7A1E File Offset: 0x000D5C1E
		private void CompletionCallback(object context, bool wasSignaled)
		{
			this.StopWatchingForExit();
			this.RaiseOnExited();
		}

		/// <summary>Release all resources used by this process.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002FDB RID: 12251 RVA: 0x000D7A2C File Offset: 0x000D5C2C
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.Close();
				}
				this.disposed = true;
				base.Dispose(disposing);
			}
		}

		/// <summary>Frees all the resources that are associated with this component.</summary>
		// Token: 0x06002FDC RID: 12252 RVA: 0x000D7A50 File Offset: 0x000D5C50
		public void Close()
		{
			if (this.Associated)
			{
				if (this.haveProcessHandle)
				{
					this.StopWatchingForExit();
					this.m_processHandle.Close();
					this.m_processHandle = null;
					this.haveProcessHandle = false;
				}
				this.haveProcessId = false;
				this.isRemoteMachine = false;
				this.machineName = ".";
				this.raisedOnExited = false;
				this.standardOutput = null;
				this.standardInput = null;
				this.standardError = null;
				this.output = null;
				this.error = null;
				this.Refresh();
			}
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000D7AD8 File Offset: 0x000D5CD8
		private void EnsureState(Process.State state)
		{
			if ((state & Process.State.IsWin2k) != (Process.State)0 && (this.OperatingSystem.Platform != PlatformID.Win32NT || this.OperatingSystem.Version.Major < 5))
			{
				throw new PlatformNotSupportedException(SR.GetString("Win2kRequired"));
			}
			if ((state & Process.State.IsNt) != (Process.State)0 && this.OperatingSystem.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
			}
			if ((state & Process.State.Associated) != (Process.State)0 && !this.Associated)
			{
				throw new InvalidOperationException(SR.GetString("NoAssociatedProcess"));
			}
			if ((state & Process.State.HaveId) != (Process.State)0 && !this.haveProcessId)
			{
				if (!this.haveProcessHandle)
				{
					this.EnsureState(Process.State.Associated);
					throw new InvalidOperationException(SR.GetString("ProcessIdRequired"));
				}
				this.SetProcessId(ProcessManager.GetProcessIdFromHandle(this.m_processHandle));
			}
			if ((state & Process.State.IsLocal) != (Process.State)0 && this.isRemoteMachine)
			{
				throw new NotSupportedException(SR.GetString("NotSupportedRemote"));
			}
			if ((state & Process.State.HaveProcessInfo) != (Process.State)0 && this.processInfo == null)
			{
				if ((state & Process.State.HaveId) == (Process.State)0)
				{
					this.EnsureState(Process.State.HaveId);
				}
				this.processInfo = ProcessManager.GetProcessInfo(this.processId, this.machineName);
				if (this.processInfo == null)
				{
					throw new InvalidOperationException(SR.GetString("NoProcessInfo"));
				}
			}
			if ((state & Process.State.Exited) != (Process.State)0)
			{
				if (!this.HasExited)
				{
					throw new InvalidOperationException(SR.GetString("WaitTillExit"));
				}
				if (!this.haveProcessHandle)
				{
					throw new InvalidOperationException(SR.GetString("NoProcessHandle"));
				}
			}
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000D7C38 File Offset: 0x000D5E38
		private void EnsureWatchingForExit()
		{
			if (!this.watchingForExit)
			{
				lock (this)
				{
					if (!this.watchingForExit)
					{
						this.watchingForExit = true;
						try
						{
							this.waitHandle = new ProcessWaitHandle(this.m_processHandle);
							this.registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(this.waitHandle, new WaitOrTimerCallback(this.CompletionCallback), null, -1, true);
						}
						catch
						{
							this.watchingForExit = false;
							throw;
						}
					}
				}
			}
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000D7CD0 File Offset: 0x000D5ED0
		private void EnsureWorkingSetLimits()
		{
			this.EnsureState(Process.State.IsNt);
			if (!this.haveWorkingSetLimits)
			{
				Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = null;
				try
				{
					safeProcessHandle = this.GetProcessHandle(1024);
					IntPtr intPtr;
					IntPtr intPtr2;
					if (!Microsoft.Win32.NativeMethods.GetProcessWorkingSetSize(safeProcessHandle, out intPtr, out intPtr2))
					{
						throw new Win32Exception();
					}
					this.minWorkingSet = intPtr;
					this.maxWorkingSet = intPtr2;
					this.haveWorkingSetLimits = true;
				}
				finally
				{
					this.ReleaseProcessHandle(safeProcessHandle);
				}
			}
		}

		/// <summary>Puts a <see cref="T:System.Diagnostics.Process" /> component in state to interact with operating system processes that run in a special mode by enabling the native property <see langword="SeDebugPrivilege" /> on the current thread.</summary>
		// Token: 0x06002FE0 RID: 12256 RVA: 0x000D7D3C File Offset: 0x000D5F3C
		public static void EnterDebugMode()
		{
			if (ProcessManager.IsNt)
			{
				Process.SetPrivilege("SeDebugPrivilege", 2);
			}
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000D7D50 File Offset: 0x000D5F50
		private static void SetPrivilege(string privilegeName, int attrib)
		{
			IntPtr intPtr = (IntPtr)0;
			Microsoft.Win32.NativeMethods.LUID luid = default(Microsoft.Win32.NativeMethods.LUID);
			IntPtr currentProcess = Microsoft.Win32.NativeMethods.GetCurrentProcess();
			if (!Microsoft.Win32.NativeMethods.OpenProcessToken(new HandleRef(null, currentProcess), 32, out intPtr))
			{
				throw new Win32Exception();
			}
			try
			{
				if (!Microsoft.Win32.NativeMethods.LookupPrivilegeValue(null, privilegeName, out luid))
				{
					throw new Win32Exception();
				}
				Microsoft.Win32.NativeMethods.TokenPrivileges tokenPrivileges = new Microsoft.Win32.NativeMethods.TokenPrivileges();
				tokenPrivileges.Luid = luid;
				tokenPrivileges.Attributes = attrib;
				Microsoft.Win32.NativeMethods.AdjustTokenPrivileges(new HandleRef(null, intPtr), false, tokenPrivileges, 0, IntPtr.Zero, IntPtr.Zero);
				if (Marshal.GetLastWin32Error() != 0)
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				SafeNativeMethods.CloseHandle(intPtr);
			}
		}

		/// <summary>Takes a <see cref="T:System.Diagnostics.Process" /> component out of the state that lets it interact with operating system processes that run in a special mode.</summary>
		// Token: 0x06002FE2 RID: 12258 RVA: 0x000D7DF4 File Offset: 0x000D5FF4
		public static void LeaveDebugMode()
		{
			if (ProcessManager.IsNt)
			{
				Process.SetPrivilege("SeDebugPrivilege", 0);
			}
		}

		/// <summary>Returns a new <see cref="T:System.Diagnostics.Process" /> component, given a process identifier and the name of a computer on the network.</summary>
		/// <param name="processId">The system-unique identifier of a process resource.</param>
		/// <param name="machineName">The name of a computer on the network.</param>
		/// <returns>A <see cref="T:System.Diagnostics.Process" /> component that is associated with a remote process resource identified by the <paramref name="processId" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The process specified by the <paramref name="processId" /> parameter is not running. The identifier might be expired.  
		///  -or-  
		///  The <paramref name="machineName" /> parameter syntax is invalid. The name might have length zero (0).</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="machineName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process was not started by this object.</exception>
		// Token: 0x06002FE3 RID: 12259 RVA: 0x000D7E08 File Offset: 0x000D6008
		public static Process GetProcessById(int processId, string machineName)
		{
			if (!ProcessManager.IsProcessRunning(processId, machineName))
			{
				throw new ArgumentException(SR.GetString("MissingProccess", new object[] { processId.ToString(CultureInfo.CurrentCulture) }));
			}
			return new Process(machineName, ProcessManager.IsRemoteMachine(machineName), processId, null);
		}

		/// <summary>Returns a new <see cref="T:System.Diagnostics.Process" /> component, given the identifier of a process on the local computer.</summary>
		/// <param name="processId">The system-unique identifier of a process resource.</param>
		/// <returns>A <see cref="T:System.Diagnostics.Process" /> component that is associated with the local process resource identified by the <paramref name="processId" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The process specified by the <paramref name="processId" /> parameter is not running. The identifier might be expired.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process was not started by this object.</exception>
		// Token: 0x06002FE4 RID: 12260 RVA: 0x000D7E46 File Offset: 0x000D6046
		public static Process GetProcessById(int processId)
		{
			return Process.GetProcessById(processId, ".");
		}

		/// <summary>Creates an array of new <see cref="T:System.Diagnostics.Process" /> components and associates them with all the process resources on the local computer that share the specified process name.</summary>
		/// <param name="processName">The friendly name of the process.</param>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents the process resources running the specified application or file.</returns>
		/// <exception cref="T:System.InvalidOperationException">There are problems accessing the performance counter API's used to get process information. This exception is specific to Windows NT, Windows 2000, and Windows XP.</exception>
		// Token: 0x06002FE5 RID: 12261 RVA: 0x000D7E53 File Offset: 0x000D6053
		public static Process[] GetProcessesByName(string processName)
		{
			return Process.GetProcessesByName(processName, ".");
		}

		/// <summary>Creates an array of new <see cref="T:System.Diagnostics.Process" /> components and associates them with all the process resources on a remote computer that share the specified process name.</summary>
		/// <param name="processName">The friendly name of the process.</param>
		/// <param name="machineName">The name of a computer on the network.</param>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents the process resources running the specified application or file.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter syntax is invalid. It might have length zero (0).</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="machineName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system platform does not support this operation on remote computers.</exception>
		/// <exception cref="T:System.InvalidOperationException">The attempt to connect to <paramref name="machineName" /> has failed.
		///  -or- 
		/// There are problems accessing the performance counter API's used to get process information. This exception is specific to Windows NT, Windows 2000, and Windows XP.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A problem occurred accessing an underlying system API.</exception>
		// Token: 0x06002FE6 RID: 12262 RVA: 0x000D7E60 File Offset: 0x000D6060
		public static Process[] GetProcessesByName(string processName, string machineName)
		{
			if (processName == null)
			{
				processName = string.Empty;
			}
			Process[] processes = Process.GetProcesses(machineName);
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < processes.Length; i++)
			{
				if (string.Equals(processName, processes[i].ProcessName, StringComparison.OrdinalIgnoreCase))
				{
					arrayList.Add(processes[i]);
				}
				else
				{
					processes[i].Dispose();
				}
			}
			Process[] array = new Process[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		/// <summary>Creates a new <see cref="T:System.Diagnostics.Process" /> component for each process resource on the local computer.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents all the process resources running on the local computer.</returns>
		// Token: 0x06002FE7 RID: 12263 RVA: 0x000D7ECC File Offset: 0x000D60CC
		public static Process[] GetProcesses()
		{
			return Process.GetProcesses(".");
		}

		/// <summary>Creates a new <see cref="T:System.Diagnostics.Process" /> component for each process resource on the specified computer.</summary>
		/// <param name="machineName">The computer from which to read the list of processes.</param>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents all the process resources running on the specified computer.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter syntax is invalid. It might have length zero (0).</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="machineName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system platform does not support this operation on remote computers.</exception>
		/// <exception cref="T:System.InvalidOperationException">There are problems accessing the performance counter API's used to get process information. This exception is specific to Windows NT, Windows 2000, and Windows XP.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A problem occurred accessing an underlying system API.</exception>
		// Token: 0x06002FE8 RID: 12264 RVA: 0x000D7ED8 File Offset: 0x000D60D8
		public static Process[] GetProcesses(string machineName)
		{
			bool flag = ProcessManager.IsRemoteMachine(machineName);
			ProcessInfo[] processInfos = ProcessManager.GetProcessInfos(machineName);
			Process[] array = new Process[processInfos.Length];
			for (int i = 0; i < processInfos.Length; i++)
			{
				ProcessInfo processInfo = processInfos[i];
				array[i] = new Process(machineName, flag, processInfo.processId, processInfo);
			}
			return array;
		}

		/// <summary>Gets a new <see cref="T:System.Diagnostics.Process" /> component and associates it with the currently active process.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component associated with the process resource that is running the calling application.</returns>
		// Token: 0x06002FE9 RID: 12265 RVA: 0x000D7F23 File Offset: 0x000D6123
		public static Process GetCurrentProcess()
		{
			return new Process(".", false, Microsoft.Win32.NativeMethods.GetCurrentProcessId(), null);
		}

		/// <summary>Raises the <see cref="E:System.Diagnostics.Process.Exited" /> event.</summary>
		// Token: 0x06002FEA RID: 12266 RVA: 0x000D7F38 File Offset: 0x000D6138
		protected void OnExited()
		{
			EventHandler eventHandler = this.onExited;
			if (eventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(eventHandler, new object[]
					{
						this,
						EventArgs.Empty
					});
					return;
				}
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000D7F90 File Offset: 0x000D6190
		private Microsoft.Win32.SafeHandles.SafeProcessHandle GetProcessHandle(int access, bool throwIfExited)
		{
			if (this.haveProcessHandle)
			{
				if (throwIfExited)
				{
					ProcessWaitHandle processWaitHandle = null;
					try
					{
						processWaitHandle = new ProcessWaitHandle(this.m_processHandle);
						if (processWaitHandle.WaitOne(0, false))
						{
							if (this.haveProcessId)
							{
								throw new InvalidOperationException(SR.GetString("ProcessHasExited", new object[] { this.processId.ToString(CultureInfo.CurrentCulture) }));
							}
							throw new InvalidOperationException(SR.GetString("ProcessHasExitedNoId"));
						}
					}
					finally
					{
						if (processWaitHandle != null)
						{
							processWaitHandle.Close();
						}
					}
				}
				return this.m_processHandle;
			}
			this.EnsureState((Process.State)3);
			Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = Microsoft.Win32.SafeHandles.SafeProcessHandle.InvalidHandle;
			safeProcessHandle = ProcessManager.OpenProcess(this.processId, access, throwIfExited);
			if (throwIfExited && (access & 1024) != 0 && Microsoft.Win32.NativeMethods.GetExitCodeProcess(safeProcessHandle, out this.exitCode) && this.exitCode != 259)
			{
				throw new InvalidOperationException(SR.GetString("ProcessHasExited", new object[] { this.processId.ToString(CultureInfo.CurrentCulture) }));
			}
			return safeProcessHandle;
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000D8090 File Offset: 0x000D6290
		private Microsoft.Win32.SafeHandles.SafeProcessHandle GetProcessHandle(int access)
		{
			return this.GetProcessHandle(access, true);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000D809A File Offset: 0x000D629A
		private Microsoft.Win32.SafeHandles.SafeProcessHandle OpenProcessHandle()
		{
			return this.OpenProcessHandle(2035711);
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000D80A7 File Offset: 0x000D62A7
		private Microsoft.Win32.SafeHandles.SafeProcessHandle OpenProcessHandle(int access)
		{
			if (!this.haveProcessHandle)
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.SetProcessHandle(this.GetProcessHandle(access));
			}
			return this.m_processHandle;
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000D80E0 File Offset: 0x000D62E0
		private void RaiseOnExited()
		{
			if (!this.raisedOnExited)
			{
				lock (this)
				{
					if (!this.raisedOnExited)
					{
						this.raisedOnExited = true;
						this.OnExited();
					}
				}
			}
		}

		/// <summary>Discards any information about the associated process that has been cached inside the process component.</summary>
		// Token: 0x06002FF0 RID: 12272 RVA: 0x000D8134 File Offset: 0x000D6334
		public void Refresh()
		{
			this.processInfo = null;
			this.threads = null;
			this.modules = null;
			this.mainWindowTitle = null;
			this.exited = false;
			this.signaled = false;
			this.haveMainWindow = false;
			this.haveWorkingSetLimits = false;
			this.haveProcessorAffinity = false;
			this.havePriorityClass = false;
			this.haveExitTime = false;
			this.haveResponding = false;
			this.havePriorityBoostEnabled = false;
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000D819C File Offset: 0x000D639C
		private void SetProcessHandle(Microsoft.Win32.SafeHandles.SafeProcessHandle processHandle)
		{
			this.m_processHandle = processHandle;
			this.haveProcessHandle = true;
			if (this.watchForExit)
			{
				this.EnsureWatchingForExit();
			}
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000D81BA File Offset: 0x000D63BA
		private void SetProcessId(int processId)
		{
			this.processId = processId;
			this.haveProcessId = true;
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000D81CC File Offset: 0x000D63CC
		private void SetWorkingSetLimits(object newMin, object newMax)
		{
			this.EnsureState(Process.State.IsNt);
			Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = null;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1280);
				IntPtr intPtr;
				IntPtr intPtr2;
				if (!Microsoft.Win32.NativeMethods.GetProcessWorkingSetSize(safeProcessHandle, out intPtr, out intPtr2))
				{
					throw new Win32Exception();
				}
				if (newMin != null)
				{
					intPtr = (IntPtr)newMin;
				}
				if (newMax != null)
				{
					intPtr2 = (IntPtr)newMax;
				}
				if ((long)intPtr > (long)intPtr2)
				{
					if (newMin != null)
					{
						throw new ArgumentException(SR.GetString("BadMinWorkset"));
					}
					throw new ArgumentException(SR.GetString("BadMaxWorkset"));
				}
				else
				{
					if (!Microsoft.Win32.NativeMethods.SetProcessWorkingSetSize(safeProcessHandle, intPtr, intPtr2))
					{
						throw new Win32Exception();
					}
					if (!Microsoft.Win32.NativeMethods.GetProcessWorkingSetSize(safeProcessHandle, out intPtr, out intPtr2))
					{
						throw new Win32Exception();
					}
					this.minWorkingSet = intPtr;
					this.maxWorkingSet = intPtr2;
					this.haveWorkingSetLimits = true;
				}
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
		}

		/// <summary>Starts (or reuses) the process resource that is specified by the <see cref="P:System.Diagnostics.Process.StartInfo" /> property of this <see cref="T:System.Diagnostics.Process" /> component and associates it with the component.</summary>
		/// <returns>
		///   <see langword="true" /> if a process resource is started; <see langword="false" /> if no new process resource is started (for example, if an existing process is reused).</returns>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified in the <see cref="T:System.Diagnostics.Process" /> component's <see cref="P:System.Diagnostics.Process.StartInfo" />.
		///  -or-
		///  The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> member of the <see cref="P:System.Diagnostics.Process.StartInfo" /> property is <see langword="true" /> while <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" />, <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" />, or <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> is <see langword="true" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Method not supported on operating systems without shell support such as Nano Server (.NET Core only).</exception>
		// Token: 0x06002FF4 RID: 12276 RVA: 0x000D8298 File Offset: 0x000D6498
		public bool Start()
		{
			this.Close();
			ProcessStartInfo processStartInfo = this.StartInfo;
			if (processStartInfo.FileName.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("FileNameMissing"));
			}
			if (processStartInfo.UseShellExecute)
			{
				return this.StartWithShellExecuteEx(processStartInfo);
			}
			return this.StartWithCreateProcess(processStartInfo);
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000D82E8 File Offset: 0x000D64E8
		private static void CreatePipeWithSecurityAttributes(out SafeFileHandle hReadPipe, out SafeFileHandle hWritePipe, Microsoft.Win32.NativeMethods.SECURITY_ATTRIBUTES lpPipeAttributes, int nSize)
		{
			bool flag = Microsoft.Win32.NativeMethods.CreatePipe(out hReadPipe, out hWritePipe, lpPipeAttributes, nSize);
			if (!flag || hReadPipe.IsInvalid || hWritePipe.IsInvalid)
			{
				throw new Win32Exception();
			}
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000D831C File Offset: 0x000D651C
		private void CreatePipe(out SafeFileHandle parentHandle, out SafeFileHandle childHandle, bool parentInputs)
		{
			Microsoft.Win32.NativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new Microsoft.Win32.NativeMethods.SECURITY_ATTRIBUTES();
			security_ATTRIBUTES.bInheritHandle = true;
			SafeFileHandle safeFileHandle = null;
			try
			{
				if (parentInputs)
				{
					Process.CreatePipeWithSecurityAttributes(out childHandle, out safeFileHandle, security_ATTRIBUTES, 0);
				}
				else
				{
					Process.CreatePipeWithSecurityAttributes(out safeFileHandle, out childHandle, security_ATTRIBUTES, 0);
				}
				if (!Microsoft.Win32.NativeMethods.DuplicateHandle(new HandleRef(this, Microsoft.Win32.NativeMethods.GetCurrentProcess()), safeFileHandle, new HandleRef(this, Microsoft.Win32.NativeMethods.GetCurrentProcess()), out parentHandle, 0, false, 2))
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				if (safeFileHandle != null && !safeFileHandle.IsInvalid)
				{
					safeFileHandle.Close();
				}
			}
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000D83A0 File Offset: 0x000D65A0
		private static StringBuilder BuildCommandLine(string executableFileName, string arguments)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = executableFileName.Trim();
			bool flag = text.StartsWith("\"", StringComparison.Ordinal) && text.EndsWith("\"", StringComparison.Ordinal);
			if (!flag)
			{
				stringBuilder.Append("\"");
			}
			stringBuilder.Append(text);
			if (!flag)
			{
				stringBuilder.Append("\"");
			}
			if (!string.IsNullOrEmpty(arguments))
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(arguments);
			}
			return stringBuilder;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000D841C File Offset: 0x000D661C
		private bool StartWithCreateProcess(ProcessStartInfo startInfo)
		{
			if (startInfo.StandardOutputEncoding != null && !startInfo.RedirectStandardOutput)
			{
				throw new InvalidOperationException(SR.GetString("StandardOutputEncodingNotAllowed"));
			}
			if (startInfo.StandardErrorEncoding != null && !startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException(SR.GetString("StandardErrorEncodingNotAllowed"));
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			StringBuilder stringBuilder = Process.BuildCommandLine(startInfo.FileName, startInfo.Arguments);
			Microsoft.Win32.NativeMethods.STARTUPINFO startupinfo = new Microsoft.Win32.NativeMethods.STARTUPINFO();
			SafeNativeMethods.PROCESS_INFORMATION process_INFORMATION = new SafeNativeMethods.PROCESS_INFORMATION();
			Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = new Microsoft.Win32.SafeHandles.SafeProcessHandle();
			Microsoft.Win32.SafeHandles.SafeThreadHandle safeThreadHandle = new Microsoft.Win32.SafeHandles.SafeThreadHandle();
			int num = 0;
			SafeFileHandle safeFileHandle = null;
			SafeFileHandle safeFileHandle2 = null;
			SafeFileHandle safeFileHandle3 = null;
			GCHandle gchandle = default(GCHandle);
			object obj = Process.s_CreateProcessLock;
			lock (obj)
			{
				try
				{
					if (startInfo.RedirectStandardInput || startInfo.RedirectStandardOutput || startInfo.RedirectStandardError)
					{
						if (startInfo.RedirectStandardInput)
						{
							this.CreatePipe(out safeFileHandle, out startupinfo.hStdInput, true);
						}
						else
						{
							startupinfo.hStdInput = new SafeFileHandle(Microsoft.Win32.NativeMethods.GetStdHandle(-10), false);
						}
						if (startInfo.RedirectStandardOutput)
						{
							this.CreatePipe(out safeFileHandle2, out startupinfo.hStdOutput, false);
						}
						else
						{
							startupinfo.hStdOutput = new SafeFileHandle(Microsoft.Win32.NativeMethods.GetStdHandle(-11), false);
						}
						if (startInfo.RedirectStandardError)
						{
							this.CreatePipe(out safeFileHandle3, out startupinfo.hStdError, false);
						}
						else
						{
							startupinfo.hStdError = new SafeFileHandle(Microsoft.Win32.NativeMethods.GetStdHandle(-12), false);
						}
						startupinfo.dwFlags = 256;
					}
					int num2 = 0;
					if (startInfo.CreateNoWindow)
					{
						num2 |= 134217728;
					}
					IntPtr intPtr = (IntPtr)0;
					if (startInfo.environmentVariables != null)
					{
						bool flag2 = false;
						if (ProcessManager.IsNt)
						{
							num2 |= 1024;
							flag2 = true;
						}
						byte[] array = EnvironmentBlock.ToByteArray(startInfo.environmentVariables, flag2);
						gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
						intPtr = gchandle.AddrOfPinnedObject();
					}
					string text = startInfo.WorkingDirectory;
					if (text == string.Empty)
					{
						text = Environment.CurrentDirectory;
					}
					bool flag3;
					if (startInfo.UserName.Length != 0)
					{
						if (startInfo.Password != null && startInfo.PasswordInClearText != null)
						{
							throw new ArgumentException(SR.GetString("CantSetDuplicatePassword"));
						}
						Microsoft.Win32.NativeMethods.LogonFlags logonFlags = (Microsoft.Win32.NativeMethods.LogonFlags)0;
						if (startInfo.LoadUserProfile)
						{
							logonFlags = Microsoft.Win32.NativeMethods.LogonFlags.LOGON_WITH_PROFILE;
						}
						IntPtr intPtr2 = IntPtr.Zero;
						try
						{
							if (startInfo.Password != null)
							{
								intPtr2 = Marshal.SecureStringToCoTaskMemUnicode(startInfo.Password);
							}
							else if (startInfo.PasswordInClearText != null)
							{
								intPtr2 = Marshal.StringToCoTaskMemUni(startInfo.PasswordInClearText);
							}
							else
							{
								intPtr2 = Marshal.StringToCoTaskMemUni(string.Empty);
							}
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
							}
							finally
							{
								flag3 = Microsoft.Win32.NativeMethods.CreateProcessWithLogonW(startInfo.UserName, startInfo.Domain, intPtr2, logonFlags, null, stringBuilder, num2, intPtr, text, startupinfo, process_INFORMATION);
								if (!flag3)
								{
									num = Marshal.GetLastWin32Error();
								}
								if (process_INFORMATION.hProcess != (IntPtr)0 && process_INFORMATION.hProcess != Microsoft.Win32.NativeMethods.INVALID_HANDLE_VALUE)
								{
									safeProcessHandle.InitialSetHandle(process_INFORMATION.hProcess);
								}
								if (process_INFORMATION.hThread != (IntPtr)0 && process_INFORMATION.hThread != Microsoft.Win32.NativeMethods.INVALID_HANDLE_VALUE)
								{
									safeThreadHandle.InitialSetHandle(process_INFORMATION.hThread);
								}
							}
							if (flag3)
							{
								goto IL_416;
							}
							if (num == 193 || num == 216)
							{
								throw new Win32Exception(num, SR.GetString("InvalidApplication"));
							}
							throw new Win32Exception(num);
						}
						finally
						{
							if (intPtr2 != IntPtr.Zero)
							{
								Marshal.ZeroFreeCoTaskMemUnicode(intPtr2);
							}
						}
					}
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						flag3 = Microsoft.Win32.NativeMethods.CreateProcess(null, stringBuilder, null, null, true, num2, intPtr, text, startupinfo, process_INFORMATION);
						if (!flag3)
						{
							num = Marshal.GetLastWin32Error();
						}
						if (process_INFORMATION.hProcess != (IntPtr)0 && process_INFORMATION.hProcess != Microsoft.Win32.NativeMethods.INVALID_HANDLE_VALUE)
						{
							safeProcessHandle.InitialSetHandle(process_INFORMATION.hProcess);
						}
						if (process_INFORMATION.hThread != (IntPtr)0 && process_INFORMATION.hThread != Microsoft.Win32.NativeMethods.INVALID_HANDLE_VALUE)
						{
							safeThreadHandle.InitialSetHandle(process_INFORMATION.hThread);
						}
					}
					if (!flag3)
					{
						if (num == 193 || num == 216)
						{
							throw new Win32Exception(num, SR.GetString("InvalidApplication"));
						}
						throw new Win32Exception(num);
					}
				}
				finally
				{
					if (gchandle.IsAllocated)
					{
						gchandle.Free();
					}
					startupinfo.Dispose();
				}
			}
			IL_416:
			if (startInfo.RedirectStandardInput)
			{
				this.standardInput = new StreamWriter(new FileStream(safeFileHandle, FileAccess.Write, 4096, false), Console.InputEncoding, 4096);
				this.standardInput.AutoFlush = true;
			}
			if (startInfo.RedirectStandardOutput)
			{
				Encoding encoding = ((startInfo.StandardOutputEncoding != null) ? startInfo.StandardOutputEncoding : Console.OutputEncoding);
				this.standardOutput = new StreamReader(new FileStream(safeFileHandle2, FileAccess.Read, 4096, false), encoding, true, 4096);
			}
			if (startInfo.RedirectStandardError)
			{
				Encoding encoding2 = ((startInfo.StandardErrorEncoding != null) ? startInfo.StandardErrorEncoding : Console.OutputEncoding);
				this.standardError = new StreamReader(new FileStream(safeFileHandle3, FileAccess.Read, 4096, false), encoding2, true, 4096);
			}
			bool flag4 = false;
			if (!safeProcessHandle.IsInvalid)
			{
				this.SetProcessHandle(safeProcessHandle);
				this.SetProcessId(process_INFORMATION.dwProcessId);
				safeThreadHandle.Close();
				flag4 = true;
			}
			return flag4;
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000D899C File Offset: 0x000D6B9C
		private bool StartWithShellExecuteEx(ProcessStartInfo startInfo)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!string.IsNullOrEmpty(startInfo.UserName) || startInfo.Password != null)
			{
				throw new InvalidOperationException(SR.GetString("CantStartAsUser"));
			}
			if (startInfo.RedirectStandardInput || startInfo.RedirectStandardOutput || startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException(SR.GetString("CantRedirectStreams"));
			}
			if (startInfo.StandardErrorEncoding != null)
			{
				throw new InvalidOperationException(SR.GetString("StandardErrorEncodingNotAllowed"));
			}
			if (startInfo.StandardOutputEncoding != null)
			{
				throw new InvalidOperationException(SR.GetString("StandardOutputEncodingNotAllowed"));
			}
			if (startInfo.environmentVariables != null)
			{
				throw new InvalidOperationException(SR.GetString("CantUseEnvVars"));
			}
			Microsoft.Win32.NativeMethods.ShellExecuteInfo shellExecuteInfo = new Microsoft.Win32.NativeMethods.ShellExecuteInfo();
			shellExecuteInfo.fMask = 64;
			if (startInfo.ErrorDialog)
			{
				shellExecuteInfo.hwnd = startInfo.ErrorDialogParentHandle;
			}
			else
			{
				shellExecuteInfo.fMask |= 1024;
			}
			switch (startInfo.WindowStyle)
			{
			case ProcessWindowStyle.Hidden:
				shellExecuteInfo.nShow = 0;
				break;
			case ProcessWindowStyle.Minimized:
				shellExecuteInfo.nShow = 2;
				break;
			case ProcessWindowStyle.Maximized:
				shellExecuteInfo.nShow = 3;
				break;
			default:
				shellExecuteInfo.nShow = 1;
				break;
			}
			try
			{
				if (startInfo.FileName.Length != 0)
				{
					shellExecuteInfo.lpFile = Marshal.StringToHGlobalAuto(startInfo.FileName);
				}
				if (startInfo.Verb.Length != 0)
				{
					shellExecuteInfo.lpVerb = Marshal.StringToHGlobalAuto(startInfo.Verb);
				}
				if (startInfo.Arguments.Length != 0)
				{
					shellExecuteInfo.lpParameters = Marshal.StringToHGlobalAuto(startInfo.Arguments);
				}
				if (startInfo.WorkingDirectory.Length != 0)
				{
					shellExecuteInfo.lpDirectory = Marshal.StringToHGlobalAuto(startInfo.WorkingDirectory);
				}
				shellExecuteInfo.fMask |= 256;
				ShellExecuteHelper shellExecuteHelper = new ShellExecuteHelper(shellExecuteInfo);
				if (!shellExecuteHelper.ShellExecuteOnSTAThread())
				{
					int num = shellExecuteHelper.ErrorCode;
					if (num == 0)
					{
						long num2 = (long)shellExecuteInfo.hInstApp;
						long num3 = num2 - 2L;
						if (num3 <= 6L)
						{
							switch ((uint)num3)
							{
							case 0U:
								num = 2;
								goto IL_274;
							case 1U:
								num = 3;
								goto IL_274;
							case 2U:
							case 4U:
							case 5U:
								goto IL_268;
							case 3U:
								num = 5;
								goto IL_274;
							case 6U:
								num = 8;
								goto IL_274;
							}
						}
						long num4 = num2 - 26L;
						if (num4 <= 6L)
						{
							switch ((uint)num4)
							{
							case 0U:
								num = 32;
								goto IL_274;
							case 2U:
							case 3U:
							case 4U:
								num = 1156;
								goto IL_274;
							case 5U:
								num = 1155;
								goto IL_274;
							case 6U:
								num = 1157;
								goto IL_274;
							}
						}
						IL_268:
						num = (int)shellExecuteInfo.hInstApp;
					}
					IL_274:
					if (num == 193 || num == 216)
					{
						throw new Win32Exception(num, SR.GetString("InvalidApplication"));
					}
					throw new Win32Exception(num);
				}
			}
			finally
			{
				if (shellExecuteInfo.lpFile != (IntPtr)0)
				{
					Marshal.FreeHGlobal(shellExecuteInfo.lpFile);
				}
				if (shellExecuteInfo.lpVerb != (IntPtr)0)
				{
					Marshal.FreeHGlobal(shellExecuteInfo.lpVerb);
				}
				if (shellExecuteInfo.lpParameters != (IntPtr)0)
				{
					Marshal.FreeHGlobal(shellExecuteInfo.lpParameters);
				}
				if (shellExecuteInfo.lpDirectory != (IntPtr)0)
				{
					Marshal.FreeHGlobal(shellExecuteInfo.lpDirectory);
				}
			}
			if (shellExecuteInfo.hProcess != (IntPtr)0)
			{
				Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = new Microsoft.Win32.SafeHandles.SafeProcessHandle(shellExecuteInfo.hProcess);
				this.SetProcessHandle(safeProcessHandle);
				return true;
			}
			return false;
		}

		/// <summary>Starts a process resource by specifying the name of an application, a user name, a password, and a domain and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <param name="fileName">The name of an application file to run in the process.</param>
		/// <param name="userName">The user name to use when starting the process.</param>
		/// <param name="password">A <see cref="T:System.Security.SecureString" /> that contains the password to use when starting the process.</param>
		/// <param name="domain">The domain to use when starting the process.</param>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> that is associated with the process resource, or <see langword="null" /> if no process resource is started. Note that a new process that's started alongside already running instances of the same process will be independent from the others. In addition, Start may return a non-null Process with its <see cref="P:System.Diagnostics.Process.HasExited" /> property already set to <see langword="true" />. In this case, the started process may have activated an existing instance of itself and then exited.</returns>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Method not supported on Linux or macOS (.NET Core only).</exception>
		// Token: 0x06002FFA RID: 12282 RVA: 0x000D8D08 File Offset: 0x000D6F08
		public static Process Start(string fileName, string userName, SecureString password, string domain)
		{
			return Process.Start(new ProcessStartInfo(fileName)
			{
				UserName = userName,
				Password = password,
				Domain = domain,
				UseShellExecute = false
			});
		}

		/// <summary>Starts a process resource by specifying the name of an application, a set of command-line arguments, a user name, a password, and a domain and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <param name="fileName">The name of an application file to run in the process.</param>
		/// <param name="arguments">Command-line arguments to pass when starting the process.</param>
		/// <param name="userName">The user name to use when starting the process.</param>
		/// <param name="password">A <see cref="T:System.Security.SecureString" /> that contains the password to use when starting the process.</param>
		/// <param name="domain">The domain to use when starting the process.</param>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> that is associated with the process resource, or <see langword="null" /> if no process resource is started. Note that a new process that's started alongside already running instances of the same process will be independent from the others. In addition, Start may return a non-null Process with its <see cref="P:System.Diagnostics.Process.HasExited" /> property already set to <see langword="true" />. In this case, the started process may have activated an existing instance of itself and then exited.</returns>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file.  
		///  -or-  
		///  The sum of the length of the arguments and the length of the full path to the associated file exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Method not supported on Linux or macOS (.NET Core only).</exception>
		// Token: 0x06002FFB RID: 12283 RVA: 0x000D8D40 File Offset: 0x000D6F40
		public static Process Start(string fileName, string arguments, string userName, SecureString password, string domain)
		{
			return Process.Start(new ProcessStartInfo(fileName, arguments)
			{
				UserName = userName,
				Password = password,
				Domain = domain,
				UseShellExecute = false
			});
		}

		/// <summary>Starts a process resource by specifying the name of a document or application file and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <param name="fileName">The name of a document or application file to run in the process.</param>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> that is associated with the process resource, or <see langword="null" /> if no process resource is started. Note that a new process that's started alongside already running instances of the same process will be independent from the others. In addition, Start may return a non-null Process with its <see cref="P:System.Diagnostics.Process.HasExited" /> property already set to <see langword="true" />. In this case, the started process may have activated an existing instance of itself and then exited.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
		// Token: 0x06002FFC RID: 12284 RVA: 0x000D8D78 File Offset: 0x000D6F78
		public static Process Start(string fileName)
		{
			return Process.Start(new ProcessStartInfo(fileName));
		}

		/// <summary>Starts a process resource by specifying the name of an application and a set of command-line arguments, and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <param name="fileName">The name of an application file to run in the process.</param>
		/// <param name="arguments">Command-line arguments to pass when starting the process.</param>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> that is associated with the process resource, or <see langword="null" /> if no process resource is started. Note that a new process that's started alongside already running instances of the same process will be independent from the others. In addition, Start may return a non-null Process with its <see cref="P:System.Diagnostics.Process.HasExited" /> property already set to <see langword="true" />. In this case, the started process may have activated an existing instance of itself and then exited.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="fileName" /> or <paramref name="arguments" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file.  
		///  -or-  
		///  The sum of the length of the arguments and the length of the full path to the process exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
		// Token: 0x06002FFD RID: 12285 RVA: 0x000D8D85 File Offset: 0x000D6F85
		public static Process Start(string fileName, string arguments)
		{
			return Process.Start(new ProcessStartInfo(fileName, arguments));
		}

		/// <summary>Starts the process resource that is specified by the parameter containing process start information (for example, the file name of the process to start) and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <param name="startInfo">The <see cref="T:System.Diagnostics.ProcessStartInfo" /> that contains the information that is used to start the process, including the file name and any command-line arguments.</param>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> that is associated with the process resource, or <see langword="null" /> if no process resource is started. Note that a new process that's started alongside already running instances of the same process will be independent from the others. In addition, Start may return a non-null Process with its <see cref="P:System.Diagnostics.Process.HasExited" /> property already set to <see langword="true" />. In this case, the started process may have activated an existing instance of itself and then exited.</returns>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified in the <paramref name="startInfo" /> parameter's <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property of the <paramref name="startInfo" /> parameter is <see langword="true" /> and the <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" />, <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" />, or <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> property is also <see langword="true" />.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property of the <paramref name="startInfo" /> parameter is <see langword="true" /> and the <see cref="P:System.Diagnostics.ProcessStartInfo.UserName" /> property is not <see langword="null" /> or empty or the <see cref="P:System.Diagnostics.ProcessStartInfo.Password" /> property is not <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="startInfo" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in the <paramref name="startInfo" /> parameter's <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property could not be found.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file.  
		///  -or-  
		///  The sum of the length of the arguments and the length of the full path to the process exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Method not supported on operating systems without shell support such as Nano Server (.NET Core only).</exception>
		// Token: 0x06002FFE RID: 12286 RVA: 0x000D8D94 File Offset: 0x000D6F94
		public static Process Start(ProcessStartInfo startInfo)
		{
			Process process = new Process();
			if (startInfo == null)
			{
				throw new ArgumentNullException("startInfo");
			}
			process.StartInfo = startInfo;
			if (process.Start())
			{
				return process;
			}
			return null;
		}

		/// <summary>Immediately stops the associated process.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The associated process could not be terminated.  
		///  -or-  
		///  The process is terminating.  
		///  -or-  
		///  The associated process is a Win16 executable.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to call <see cref="M:System.Diagnostics.Process.Kill" /> for a process that is running on a remote computer. The method is available only for processes running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has already exited.  
		///  -or-  
		///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		// Token: 0x06002FFF RID: 12287 RVA: 0x000D8DC8 File Offset: 0x000D6FC8
		public void Kill()
		{
			Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = null;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1);
				if (!Microsoft.Win32.NativeMethods.TerminateProcess(safeProcessHandle, -1))
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000D8E08 File Offset: 0x000D7008
		private void StopWatchingForExit()
		{
			if (this.watchingForExit)
			{
				lock (this)
				{
					if (this.watchingForExit)
					{
						this.watchingForExit = false;
						this.registeredWaitHandle.Unregister(null);
						this.waitHandle.Close();
						this.waitHandle = null;
						this.registeredWaitHandle = null;
					}
				}
			}
		}

		/// <summary>Formats the process's name as a string, combined with the parent component type, if applicable.</summary>
		/// <returns>The <see cref="P:System.Diagnostics.Process.ProcessName" />, combined with the base component's <see cref="M:System.Object.ToString" /> return value.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///   <see cref="M:System.Diagnostics.Process.ToString" /> is not supported on Windows 98.</exception>
		// Token: 0x06003001 RID: 12289 RVA: 0x000D8E7C File Offset: 0x000D707C
		public override string ToString()
		{
			if (!this.Associated)
			{
				return base.ToString();
			}
			string text = string.Empty;
			try
			{
				text = this.ProcessName;
			}
			catch (PlatformNotSupportedException)
			{
			}
			if (text.Length != 0)
			{
				return string.Format(CultureInfo.CurrentCulture, "{0} ({1})", new object[]
				{
					base.ToString(),
					text
				});
			}
			return base.ToString();
		}

		/// <summary>Instructs the <see cref="T:System.Diagnostics.Process" /> component to wait the specified number of milliseconds for the associated process to exit.</summary>
		/// <param name="milliseconds">The amount of time, in milliseconds, to wait for the associated process to exit. The maximum is the largest possible value of a 32-bit integer, which represents infinity to the operating system.</param>
		/// <returns>
		///   <see langword="true" /> if the associated process has exited; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The wait setting could not be accessed.</exception>
		/// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
		///  -or-  
		///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
		///  -or-  
		///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit(System.Int32)" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="milliseconds" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x06003002 RID: 12290 RVA: 0x000D8EEC File Offset: 0x000D70EC
		public bool WaitForExit(int milliseconds)
		{
			Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = null;
			ProcessWaitHandle processWaitHandle = null;
			bool flag;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1048576, false);
				if (safeProcessHandle.IsInvalid)
				{
					flag = true;
				}
				else
				{
					processWaitHandle = new ProcessWaitHandle(safeProcessHandle);
					if (processWaitHandle.WaitOne(milliseconds, false))
					{
						flag = true;
						this.signaled = true;
					}
					else
					{
						flag = false;
						this.signaled = false;
					}
				}
			}
			finally
			{
				if (processWaitHandle != null)
				{
					processWaitHandle.Close();
				}
				if (this.output != null && milliseconds == -1)
				{
					this.output.WaitUtilEOF();
				}
				if (this.error != null && milliseconds == -1)
				{
					this.error.WaitUtilEOF();
				}
				this.ReleaseProcessHandle(safeProcessHandle);
			}
			if (flag && this.watchForExit)
			{
				this.RaiseOnExited();
			}
			return flag;
		}

		/// <summary>Instructs the <see cref="T:System.Diagnostics.Process" /> component to wait indefinitely for the associated process to exit.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The wait setting could not be accessed.</exception>
		/// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
		///  -or-  
		///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
		///  -or-  
		///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
		// Token: 0x06003003 RID: 12291 RVA: 0x000D8FA0 File Offset: 0x000D71A0
		public void WaitForExit()
		{
			this.WaitForExit(-1);
		}

		/// <summary>Causes the <see cref="T:System.Diagnostics.Process" /> component to wait the specified number of milliseconds for the associated process to enter an idle state. This overload applies only to processes with a user interface and, therefore, a message loop.</summary>
		/// <param name="milliseconds">A value of 1 to <see cref="F:System.Int32.MaxValue" /> that specifies the amount of time, in milliseconds, to wait for the associated process to become idle. A value of 0 specifies an immediate return, and a value of -1 specifies an infinite wait.</param>
		/// <returns>
		///   <see langword="true" /> if the associated process has reached an idle state; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process does not have a graphical interface.  
		///  -or-  
		///  An unknown error occurred. The process failed to enter an idle state.  
		///  -or-  
		///  The process has already exited.  
		///  -or-  
		///  No process is associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		// Token: 0x06003004 RID: 12292 RVA: 0x000D8FAC File Offset: 0x000D71AC
		public bool WaitForInputIdle(int milliseconds)
		{
			Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = null;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1049600);
				int num = Microsoft.Win32.NativeMethods.WaitForInputIdle(safeProcessHandle, milliseconds);
				if (num != -1)
				{
					if (num == 0)
					{
						return true;
					}
					if (num == 258)
					{
						return false;
					}
				}
				throw new InvalidOperationException(SR.GetString("InputIdleUnkownError"));
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
			bool flag;
			return flag;
		}

		/// <summary>Causes the <see cref="T:System.Diagnostics.Process" /> component to wait indefinitely for the associated process to enter an idle state. This overload applies only to processes with a user interface and, therefore, a message loop.</summary>
		/// <returns>
		///   <see langword="true" /> if the associated process has reached an idle state.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process does not have a graphical interface.  
		///  -or-  
		///  An unknown error occurred. The process failed to enter an idle state.  
		///  -or-  
		///  The process has already exited.  
		///  -or-  
		///  No process is associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		// Token: 0x06003005 RID: 12293 RVA: 0x000D9014 File Offset: 0x000D7214
		public bool WaitForInputIdle()
		{
			return this.WaitForInputIdle(int.MaxValue);
		}

		/// <summary>Begins asynchronous read operations on the redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream of the application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" /> property is <see langword="false" />.  
		/// -or-
		///  An asynchronous read operation is already in progress on the <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.  
		/// -or-
		///  The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream has been used by a synchronous read operation.</exception>
		// Token: 0x06003006 RID: 12294 RVA: 0x000D9024 File Offset: 0x000D7224
		[ComVisible(false)]
		public void BeginOutputReadLine()
		{
			if (this.outputStreamReadMode == Process.StreamReadMode.undefined)
			{
				this.outputStreamReadMode = Process.StreamReadMode.asyncMode;
			}
			else if (this.outputStreamReadMode != Process.StreamReadMode.asyncMode)
			{
				throw new InvalidOperationException(SR.GetString("CantMixSyncAsyncOperation"));
			}
			if (this.pendingOutputRead)
			{
				throw new InvalidOperationException(SR.GetString("PendingAsyncOperation"));
			}
			this.pendingOutputRead = true;
			if (this.output == null)
			{
				if (this.standardOutput == null)
				{
					throw new InvalidOperationException(SR.GetString("CantGetStandardOut"));
				}
				Stream baseStream = this.standardOutput.BaseStream;
				this.output = new AsyncStreamReader(this, baseStream, new UserCallBack(this.OutputReadNotifyUser), this.standardOutput.CurrentEncoding);
			}
			this.output.BeginReadLine();
		}

		/// <summary>Begins asynchronous read operations on the redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream of the application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> property is <see langword="false" />.  
		/// -or-
		///  An asynchronous read operation is already in progress on the <see cref="P:System.Diagnostics.Process.StandardError" /> stream.  
		/// -or-
		///  The <see cref="P:System.Diagnostics.Process.StandardError" /> stream has been used by a synchronous read operation.</exception>
		// Token: 0x06003007 RID: 12295 RVA: 0x000D90D8 File Offset: 0x000D72D8
		[ComVisible(false)]
		public void BeginErrorReadLine()
		{
			if (this.errorStreamReadMode == Process.StreamReadMode.undefined)
			{
				this.errorStreamReadMode = Process.StreamReadMode.asyncMode;
			}
			else if (this.errorStreamReadMode != Process.StreamReadMode.asyncMode)
			{
				throw new InvalidOperationException(SR.GetString("CantMixSyncAsyncOperation"));
			}
			if (this.pendingErrorRead)
			{
				throw new InvalidOperationException(SR.GetString("PendingAsyncOperation"));
			}
			this.pendingErrorRead = true;
			if (this.error == null)
			{
				if (this.standardError == null)
				{
					throw new InvalidOperationException(SR.GetString("CantGetStandardError"));
				}
				Stream baseStream = this.standardError.BaseStream;
				this.error = new AsyncStreamReader(this, baseStream, new UserCallBack(this.ErrorReadNotifyUser), this.standardError.CurrentEncoding);
			}
			this.error.BeginReadLine();
		}

		/// <summary>Cancels the asynchronous read operation on the redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream of an application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream is not enabled for asynchronous read operations.</exception>
		// Token: 0x06003008 RID: 12296 RVA: 0x000D9189 File Offset: 0x000D7389
		[ComVisible(false)]
		public void CancelOutputRead()
		{
			if (this.output != null)
			{
				this.output.CancelOperation();
				this.pendingOutputRead = false;
				return;
			}
			throw new InvalidOperationException(SR.GetString("NoAsyncOperation"));
		}

		/// <summary>Cancels the asynchronous read operation on the redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream of an application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardError" /> stream is not enabled for asynchronous read operations.</exception>
		// Token: 0x06003009 RID: 12297 RVA: 0x000D91B7 File Offset: 0x000D73B7
		[ComVisible(false)]
		public void CancelErrorRead()
		{
			if (this.error != null)
			{
				this.error.CancelOperation();
				this.pendingErrorRead = false;
				return;
			}
			throw new InvalidOperationException(SR.GetString("NoAsyncOperation"));
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000D91E8 File Offset: 0x000D73E8
		internal void OutputReadNotifyUser(string data)
		{
			DataReceivedEventHandler outputDataReceived = this.OutputDataReceived;
			if (outputDataReceived != null)
			{
				DataReceivedEventArgs dataReceivedEventArgs = new DataReceivedEventArgs(data);
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.Invoke(outputDataReceived, new object[] { this, dataReceivedEventArgs });
					return;
				}
				outputDataReceived(this, dataReceivedEventArgs);
			}
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000D9240 File Offset: 0x000D7440
		internal void ErrorReadNotifyUser(string data)
		{
			DataReceivedEventHandler errorDataReceived = this.ErrorDataReceived;
			if (errorDataReceived != null)
			{
				DataReceivedEventArgs dataReceivedEventArgs = new DataReceivedEventArgs(data);
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.Invoke(errorDataReceived, new object[] { this, dataReceivedEventArgs });
					return;
				}
				errorDataReceived(this, dataReceivedEventArgs);
			}
		}

		// Token: 0x04002818 RID: 10264
		private bool haveProcessId;

		// Token: 0x04002819 RID: 10265
		private int processId;

		// Token: 0x0400281A RID: 10266
		private bool haveProcessHandle;

		// Token: 0x0400281B RID: 10267
		private Microsoft.Win32.SafeHandles.SafeProcessHandle m_processHandle;

		// Token: 0x0400281C RID: 10268
		private bool isRemoteMachine;

		// Token: 0x0400281D RID: 10269
		private string machineName;

		// Token: 0x0400281E RID: 10270
		private ProcessInfo processInfo;

		// Token: 0x0400281F RID: 10271
		private int m_processAccess;

		// Token: 0x04002820 RID: 10272
		private ProcessThreadCollection threads;

		// Token: 0x04002821 RID: 10273
		private ProcessModuleCollection modules;

		// Token: 0x04002822 RID: 10274
		private bool haveMainWindow;

		// Token: 0x04002823 RID: 10275
		private IntPtr mainWindowHandle;

		// Token: 0x04002824 RID: 10276
		private string mainWindowTitle;

		// Token: 0x04002825 RID: 10277
		private bool haveWorkingSetLimits;

		// Token: 0x04002826 RID: 10278
		private IntPtr minWorkingSet;

		// Token: 0x04002827 RID: 10279
		private IntPtr maxWorkingSet;

		// Token: 0x04002828 RID: 10280
		private bool haveProcessorAffinity;

		// Token: 0x04002829 RID: 10281
		private IntPtr processorAffinity;

		// Token: 0x0400282A RID: 10282
		private bool havePriorityClass;

		// Token: 0x0400282B RID: 10283
		private ProcessPriorityClass priorityClass;

		// Token: 0x0400282C RID: 10284
		private ProcessStartInfo startInfo;

		// Token: 0x0400282D RID: 10285
		private bool watchForExit;

		// Token: 0x0400282E RID: 10286
		private bool watchingForExit;

		// Token: 0x0400282F RID: 10287
		private EventHandler onExited;

		// Token: 0x04002830 RID: 10288
		private bool exited;

		// Token: 0x04002831 RID: 10289
		private int exitCode;

		// Token: 0x04002832 RID: 10290
		private bool signaled;

		// Token: 0x04002833 RID: 10291
		private DateTime exitTime;

		// Token: 0x04002834 RID: 10292
		private bool haveExitTime;

		// Token: 0x04002835 RID: 10293
		private bool responding;

		// Token: 0x04002836 RID: 10294
		private bool haveResponding;

		// Token: 0x04002837 RID: 10295
		private bool priorityBoostEnabled;

		// Token: 0x04002838 RID: 10296
		private bool havePriorityBoostEnabled;

		// Token: 0x04002839 RID: 10297
		private bool raisedOnExited;

		// Token: 0x0400283A RID: 10298
		private RegisteredWaitHandle registeredWaitHandle;

		// Token: 0x0400283B RID: 10299
		private WaitHandle waitHandle;

		// Token: 0x0400283C RID: 10300
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x0400283D RID: 10301
		private StreamReader standardOutput;

		// Token: 0x0400283E RID: 10302
		private StreamWriter standardInput;

		// Token: 0x0400283F RID: 10303
		private StreamReader standardError;

		// Token: 0x04002840 RID: 10304
		private OperatingSystem operatingSystem;

		// Token: 0x04002841 RID: 10305
		private bool disposed;

		// Token: 0x04002842 RID: 10306
		private static object s_CreateProcessLock = new object();

		// Token: 0x04002843 RID: 10307
		private Process.StreamReadMode outputStreamReadMode;

		// Token: 0x04002844 RID: 10308
		private Process.StreamReadMode errorStreamReadMode;

		// Token: 0x04002847 RID: 10311
		internal AsyncStreamReader output;

		// Token: 0x04002848 RID: 10312
		internal AsyncStreamReader error;

		// Token: 0x04002849 RID: 10313
		internal bool pendingOutputRead;

		// Token: 0x0400284A RID: 10314
		internal bool pendingErrorRead;

		// Token: 0x0400284B RID: 10315
		private static SafeFileHandle InvalidPipeHandle = new SafeFileHandle(IntPtr.Zero, false);

		// Token: 0x0400284C RID: 10316
		internal static TraceSwitch processTracing = null;

		// Token: 0x0200087D RID: 2173
		private enum StreamReadMode
		{
			// Token: 0x04003725 RID: 14117
			undefined,
			// Token: 0x04003726 RID: 14118
			syncMode,
			// Token: 0x04003727 RID: 14119
			asyncMode
		}

		// Token: 0x0200087E RID: 2174
		private enum State
		{
			// Token: 0x04003729 RID: 14121
			HaveId = 1,
			// Token: 0x0400372A RID: 14122
			IsLocal,
			// Token: 0x0400372B RID: 14123
			IsNt = 4,
			// Token: 0x0400372C RID: 14124
			HaveProcessInfo = 8,
			// Token: 0x0400372D RID: 14125
			Exited = 16,
			// Token: 0x0400372E RID: 14126
			Associated = 32,
			// Token: 0x0400372F RID: 14127
			IsWin2k = 64,
			// Token: 0x04003730 RID: 14128
			HaveNtProcessInfo = 12
		}
	}
}
