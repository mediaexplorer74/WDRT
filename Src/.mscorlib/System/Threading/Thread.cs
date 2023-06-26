using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Threading
{
	/// <summary>Creates and controls a thread, sets its priority, and gets its status.</summary>
	// Token: 0x02000514 RID: 1300
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Thread))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class Thread : CriticalFinalizerObject, _Thread
	{
		// Token: 0x06003D3A RID: 15674 RVA: 0x000E79DD File Offset: 0x000E5BDD
		private static void AsyncLocalSetCurrentCulture(AsyncLocalValueChangedArgs<CultureInfo> args)
		{
			Thread.CurrentThread.m_CurrentCulture = args.CurrentValue;
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x000E79F0 File Offset: 0x000E5BF0
		private static void AsyncLocalSetCurrentUICulture(AsyncLocalValueChangedArgs<CultureInfo> args)
		{
			Thread.CurrentThread.m_CurrentUICulture = args.CurrentValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="start" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003D3C RID: 15676 RVA: 0x000E7A03 File Offset: 0x000E5C03
		[SecuritySafeCritical]
		public Thread(ThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.SetStartHelper(start, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class, specifying the maximum stack size for the thread.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing.</param>
		/// <param name="maxStackSize">The maximum stack size, in bytes, to be used by the thread, or 0 to use the default maximum stack size specified in the header for the executable.  
		///  Important   For partially trusted code, <paramref name="maxStackSize" /> is ignored if it is greater than the default stack size. No exception is thrown.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStackSize" /> is less than zero.</exception>
		// Token: 0x06003D3D RID: 15677 RVA: 0x000E7A21 File Offset: 0x000E5C21
		[SecuritySafeCritical]
		public Thread(ThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (0 > maxStackSize)
			{
				throw new ArgumentOutOfRangeException("maxStackSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.SetStartHelper(start, maxStackSize);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class, specifying a delegate that allows an object to be passed to the thread when the thread is started.</summary>
		/// <param name="start">A delegate that represents the methods to be invoked when this thread begins executing.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is <see langword="null" />.</exception>
		// Token: 0x06003D3E RID: 15678 RVA: 0x000E7A58 File Offset: 0x000E5C58
		[SecuritySafeCritical]
		public Thread(ParameterizedThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.SetStartHelper(start, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class, specifying a delegate that allows an object to be passed to the thread when the thread is started and specifying the maximum stack size for the thread.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ParameterizedThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing.</param>
		/// <param name="maxStackSize">The maximum stack size, in bytes, to be used by the thread, or 0 to use the default maximum stack size specified in the header for the executable.  
		///  Important   For partially trusted code, <paramref name="maxStackSize" /> is ignored if it is greater than the default stack size. No exception is thrown.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStackSize" /> is less than zero.</exception>
		// Token: 0x06003D3F RID: 15679 RVA: 0x000E7A76 File Offset: 0x000E5C76
		[SecuritySafeCritical]
		public Thread(ParameterizedThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (0 > maxStackSize)
			{
				throw new ArgumentOutOfRangeException("maxStackSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.SetStartHelper(start, maxStackSize);
		}

		/// <summary>Returns a hash code for the current thread.</summary>
		/// <returns>An integer hash code value.</returns>
		// Token: 0x06003D40 RID: 15680 RVA: 0x000E7AAD File Offset: 0x000E5CAD
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.m_ManagedThreadId;
		}

		/// <summary>Gets a unique identifier for the current managed thread.</summary>
		/// <returns>An integer that represents a unique identifier for this managed thread.</returns>
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06003D41 RID: 15681
		[__DynamicallyInvokable]
		public extern int ManagedThreadId
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x000E7AB8 File Offset: 0x000E5CB8
		internal ThreadHandle GetNativeHandle()
		{
			IntPtr dont_USE_InternalThread = this.DONT_USE_InternalThread;
			if (dont_USE_InternalThread.IsNull())
			{
				throw new ArgumentException(null, Environment.GetResourceString("Argument_InvalidHandle"));
			}
			return new ThreadHandle(dont_USE_InternalThread);
		}

		/// <summary>Causes the operating system to change the state of the current instance to <see cref="F:System.Threading.ThreadState.Running" />.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available to start this thread.</exception>
		// Token: 0x06003D43 RID: 15683 RVA: 0x000E7AEC File Offset: 0x000E5CEC
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Start()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Start(ref stackCrawlMark);
		}

		/// <summary>Causes the operating system to change the state of the current instance to <see cref="F:System.Threading.ThreadState.Running" />, and optionally supplies an object containing data to be used by the method the thread executes.</summary>
		/// <param name="parameter">An object that contains data to be used by the method the thread executes.</param>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available to start this thread.</exception>
		/// <exception cref="T:System.InvalidOperationException">This thread was created using a <see cref="T:System.Threading.ThreadStart" /> delegate instead of a <see cref="T:System.Threading.ParameterizedThreadStart" /> delegate.</exception>
		// Token: 0x06003D44 RID: 15684 RVA: 0x000E7B04 File Offset: 0x000E5D04
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Start(object parameter)
		{
			if (this.m_Delegate is ThreadStart)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ThreadWrongThreadStart"));
			}
			this.m_ThreadStartArg = parameter;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Start(ref stackCrawlMark);
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x000E7B40 File Offset: 0x000E5D40
		[SecuritySafeCritical]
		private void Start(ref StackCrawlMark stackMark)
		{
			this.StartupSetApartmentStateInternal();
			if (this.m_Delegate != null)
			{
				ThreadHelper threadHelper = (ThreadHelper)this.m_Delegate.Target;
				ExecutionContext executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx);
				threadHelper.SetExecutionContextHelper(executionContext);
			}
			IPrincipal principal = CallContext.Principal;
			this.StartInternal(principal, ref stackMark);
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x000E7B89 File Offset: 0x000E5D89
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext.Reader GetExecutionContextReader()
		{
			return new ExecutionContext.Reader(this.m_ExecutionContext);
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06003D47 RID: 15687 RVA: 0x000E7B96 File Offset: 0x000E5D96
		// (set) Token: 0x06003D48 RID: 15688 RVA: 0x000E7BA1 File Offset: 0x000E5DA1
		internal bool ExecutionContextBelongsToCurrentScope
		{
			get
			{
				return !this.m_ExecutionContextBelongsToOuterScope;
			}
			set
			{
				this.m_ExecutionContextBelongsToOuterScope = !value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Threading.ExecutionContext" /> object that contains information about the various contexts of the current thread.</summary>
		/// <returns>An <see cref="T:System.Threading.ExecutionContext" /> object that consolidates context information for the current thread.</returns>
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06003D49 RID: 15689 RVA: 0x000E7BB0 File Offset: 0x000E5DB0
		public ExecutionContext ExecutionContext
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				ExecutionContext executionContext;
				if (this == Thread.CurrentThread)
				{
					executionContext = this.GetMutableExecutionContext();
				}
				else
				{
					executionContext = this.m_ExecutionContext;
				}
				return executionContext;
			}
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x000E7BD8 File Offset: 0x000E5DD8
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal ExecutionContext GetMutableExecutionContext()
		{
			if (this.m_ExecutionContext == null)
			{
				this.m_ExecutionContext = new ExecutionContext();
			}
			else if (!this.ExecutionContextBelongsToCurrentScope)
			{
				ExecutionContext executionContext = this.m_ExecutionContext.CreateMutableCopy();
				this.m_ExecutionContext = executionContext;
			}
			this.ExecutionContextBelongsToCurrentScope = true;
			return this.m_ExecutionContext;
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x000E7C22 File Offset: 0x000E5E22
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetExecutionContext(ExecutionContext value, bool belongsToCurrentScope)
		{
			this.m_ExecutionContext = value;
			this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x000E7C32 File Offset: 0x000E5E32
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetExecutionContext(ExecutionContext.Reader value, bool belongsToCurrentScope)
		{
			this.m_ExecutionContext = value.DangerousGetRawExecutionContext();
			this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
		}

		// Token: 0x06003D4D RID: 15693
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StartInternal(IPrincipal principal, ref StackCrawlMark stackMark);

		/// <summary>Applies a captured <see cref="T:System.Threading.CompressedStack" /> to the current thread.</summary>
		/// <param name="stack">The <see cref="T:System.Threading.CompressedStack" /> object to be applied to the current thread.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06003D4E RID: 15694 RVA: 0x000E7C48 File Offset: 0x000E5E48
		[SecurityCritical]
		[Obsolete("Thread.SetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
		public void SetCompressedStack(CompressedStack stack)
		{
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ThreadAPIsNotSupported"));
		}

		// Token: 0x06003D4F RID: 15695
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr SetAppDomainStack(SafeCompressedStackHandle csHandle);

		// Token: 0x06003D50 RID: 15696
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RestoreAppDomainStack(IntPtr appDomainStack);

		/// <summary>Returns a <see cref="T:System.Threading.CompressedStack" /> object that can be used to capture the stack for the current thread.</summary>
		/// <returns>None.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06003D51 RID: 15697 RVA: 0x000E7C59 File Offset: 0x000E5E59
		[SecurityCritical]
		[Obsolete("Thread.GetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
		public CompressedStack GetCompressedStack()
		{
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ThreadAPIsNotSupported"));
		}

		// Token: 0x06003D52 RID: 15698
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr InternalGetCurrentThread();

		/// <summary>Raises a <see cref="T:System.Threading.ThreadAbortException" /> in the thread on which it is invoked, to begin the process of terminating the thread while also providing exception information about the thread termination. Calling this method usually terminates the thread.</summary>
		/// <param name="stateInfo">An object that contains application-specific information, such as state, which can be used by the thread being aborted.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core only: This member is not supported.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread that is being aborted is currently suspended.</exception>
		// Token: 0x06003D53 RID: 15699 RVA: 0x000E7C6A File Offset: 0x000E5E6A
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Abort(object stateInfo)
		{
			this.AbortReason = stateInfo;
			this.AbortInternal();
		}

		/// <summary>Raises a <see cref="T:System.Threading.ThreadAbortException" /> in the thread on which it is invoked, to begin the process of terminating the thread. Calling this method usually terminates the thread.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core only: This member is not supported.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread that is being aborted is currently suspended.</exception>
		// Token: 0x06003D54 RID: 15700 RVA: 0x000E7C79 File Offset: 0x000E5E79
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Abort()
		{
			this.AbortInternal();
		}

		// Token: 0x06003D55 RID: 15701
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AbortInternal();

		/// <summary>Cancels an <see cref="M:System.Threading.Thread.Abort(System.Object)" /> requested for the current thread.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core only: This member is not supported.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">
		///   <see langword="Abort" /> was not invoked on the current thread.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required security permission for the current thread.</exception>
		// Token: 0x06003D56 RID: 15702 RVA: 0x000E7C84 File Offset: 0x000E5E84
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public static void ResetAbort()
		{
			Thread currentThread = Thread.CurrentThread;
			if ((currentThread.ThreadState & ThreadState.AbortRequested) == ThreadState.Running)
			{
				throw new ThreadStateException(Environment.GetResourceString("ThreadState_NoAbortRequested"));
			}
			currentThread.ResetAbortNative();
			currentThread.ClearAbortReason();
		}

		// Token: 0x06003D57 RID: 15703
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetAbortNative();

		/// <summary>Either suspends the thread, or if the thread is already suspended, has no effect.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core only: This member is not supported.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has not been started or is dead.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate <see cref="T:System.Security.Permissions.SecurityPermission" />.</exception>
		// Token: 0x06003D58 RID: 15704 RVA: 0x000E7CC1 File Offset: 0x000E5EC1
		[SecuritySafeCritical]
		[Obsolete("Thread.Suspend has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Suspend()
		{
			this.SuspendInternal();
		}

		// Token: 0x06003D59 RID: 15705
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SuspendInternal();

		/// <summary>Resumes a thread that has been suspended.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core only: This member is not supported.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has not been started, is dead, or is not in the suspended state.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate <see cref="T:System.Security.Permissions.SecurityPermission" />.</exception>
		// Token: 0x06003D5A RID: 15706 RVA: 0x000E7CC9 File Offset: 0x000E5EC9
		[SecuritySafeCritical]
		[Obsolete("Thread.Resume has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Resume()
		{
			this.ResumeInternal();
		}

		// Token: 0x06003D5B RID: 15707
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResumeInternal();

		/// <summary>Interrupts a thread that is in the <see langword="WaitSleepJoin" /> thread state.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate <see cref="T:System.Security.Permissions.SecurityPermission" />.</exception>
		// Token: 0x06003D5C RID: 15708 RVA: 0x000E7CD1 File Offset: 0x000E5ED1
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public void Interrupt()
		{
			this.InterruptInternal();
		}

		// Token: 0x06003D5D RID: 15709
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InterruptInternal();

		/// <summary>Gets or sets a value indicating the scheduling priority of a thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ThreadPriority" /> values. The default value is <see cref="F:System.Threading.ThreadPriority.Normal" />.</returns>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has reached a final state, such as <see cref="F:System.Threading.ThreadState.Aborted" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is not a valid <see cref="T:System.Threading.ThreadPriority" /> value.</exception>
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06003D5E RID: 15710 RVA: 0x000E7CD9 File Offset: 0x000E5ED9
		// (set) Token: 0x06003D5F RID: 15711 RVA: 0x000E7CE1 File Offset: 0x000E5EE1
		public ThreadPriority Priority
		{
			[SecuritySafeCritical]
			get
			{
				return (ThreadPriority)this.GetPriorityNative();
			}
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true)]
			set
			{
				this.SetPriorityNative((int)value);
			}
		}

		// Token: 0x06003D60 RID: 15712
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPriorityNative();

		// Token: 0x06003D61 RID: 15713
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPriorityNative(int priority);

		/// <summary>Gets a value indicating the execution status of the current thread.</summary>
		/// <returns>
		///   <see langword="true" /> if this thread has been started and has not terminated normally or aborted; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06003D62 RID: 15714
		public extern bool IsAlive
		{
			[SecuritySafeCritical]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		/// <summary>Gets a value indicating whether or not a thread belongs to the managed thread pool.</summary>
		/// <returns>
		///   <see langword="true" /> if this thread belongs to the managed thread pool; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06003D63 RID: 15715
		public extern bool IsThreadPoolThread
		{
			[SecuritySafeCritical]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06003D64 RID: 15716
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool JoinInternal(int millisecondsTimeout);

		/// <summary>Blocks the calling thread until the thread represented by this instance terminates, while continuing to perform standard COM and <see langword="SendMessage" /> pumping.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The caller attempted to join a thread that is in the <see cref="F:System.Threading.ThreadState.Unstarted" /> state.</exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread is interrupted while waiting.</exception>
		// Token: 0x06003D65 RID: 15717 RVA: 0x000E7CEA File Offset: 0x000E5EEA
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public void Join()
		{
			this.JoinInternal(-1);
		}

		/// <summary>Blocks the calling thread until the thread represented by this instance terminates or the specified time elapses, while continuing to perform standard COM and SendMessage pumping.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait for the thread to terminate.</param>
		/// <returns>
		///   <see langword="true" /> if the thread has terminated; <see langword="false" /> if the thread has not terminated after the amount of time specified by the <paramref name="millisecondsTimeout" /> parameter has elapsed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="millisecondsTimeout" /> is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> in milliseconds.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has not been started.</exception>
		// Token: 0x06003D66 RID: 15718 RVA: 0x000E7CF4 File Offset: 0x000E5EF4
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public bool Join(int millisecondsTimeout)
		{
			return this.JoinInternal(millisecondsTimeout);
		}

		/// <summary>Blocks the calling thread until the thread represented by this instance terminates or the specified time elapses, while continuing to perform standard COM and SendMessage pumping.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> set to the amount of time to wait for the thread to terminate.</param>
		/// <returns>
		///   <see langword="true" /> if the thread terminated; <see langword="false" /> if the thread has not terminated after the amount of time specified by the <paramref name="timeout" /> parameter has elapsed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> in milliseconds, or is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The caller attempted to join a thread that is in the <see cref="F:System.Threading.ThreadState.Unstarted" /> state.</exception>
		// Token: 0x06003D67 RID: 15719 RVA: 0x000E7D00 File Offset: 0x000E5F00
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public bool Join(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return this.Join((int)num);
		}

		// Token: 0x06003D68 RID: 15720
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SleepInternal(int millisecondsTimeout);

		/// <summary>Suspends the current thread for the specified number of milliseconds.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds for which the thread is suspended. If the value of the <paramref name="millisecondsTimeout" /> argument is zero, the thread relinquishes the remainder of its time slice to any thread of equal priority that is ready to run. If there are no other threads of equal priority that are ready to run, execution of the current thread is not suspended.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The time-out value is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x06003D69 RID: 15721 RVA: 0x000E7D41 File Offset: 0x000E5F41
		[SecuritySafeCritical]
		public static void Sleep(int millisecondsTimeout)
		{
			Thread.SleepInternal(millisecondsTimeout);
			if (AppDomainPauseManager.IsPaused)
			{
				AppDomainPauseManager.ResumeEvent.WaitOneWithoutFAS();
			}
		}

		/// <summary>Suspends the current thread for the specified amount of time.</summary>
		/// <param name="timeout">The amount of time for which the thread is suspended. If the value of the <paramref name="millisecondsTimeout" /> argument is <see cref="F:System.TimeSpan.Zero" />, the thread relinquishes the remainder of its time slice to any thread of equal priority that is ready to run. If there are no other threads of equal priority that are ready to run, execution of the current thread is not suspended.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> in milliseconds, or is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds.</exception>
		// Token: 0x06003D6A RID: 15722 RVA: 0x000E7D5C File Offset: 0x000E5F5C
		public static void Sleep(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			Thread.Sleep((int)num);
		}

		// Token: 0x06003D6B RID: 15723
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCurrentProcessorNumber();

		// Token: 0x06003D6C RID: 15724 RVA: 0x000E7D9C File Offset: 0x000E5F9C
		private static int RefreshCurrentProcessorId()
		{
			int num = Thread.GetCurrentProcessorNumber();
			if (num < 0)
			{
				num = Environment.CurrentManagedThreadId;
			}
			num += 100;
			Thread.t_currentProcessorIdCache = ((num << 16) & int.MaxValue) | 5000;
			return num;
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x000E7DD4 File Offset: 0x000E5FD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int GetCurrentProcessorId()
		{
			int num = Thread.t_currentProcessorIdCache--;
			if ((num & 65535) == 0)
			{
				return Thread.RefreshCurrentProcessorId();
			}
			return num >> 16;
		}

		// Token: 0x06003D6E RID: 15726
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SpinWaitInternal(int iterations);

		/// <summary>Causes a thread to wait the number of times defined by the <paramref name="iterations" /> parameter.</summary>
		/// <param name="iterations">A 32-bit signed integer that defines how long a thread is to wait.</param>
		// Token: 0x06003D6F RID: 15727 RVA: 0x000E7E02 File Offset: 0x000E6002
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public static void SpinWait(int iterations)
		{
			Thread.SpinWaitInternal(iterations);
		}

		// Token: 0x06003D70 RID: 15728
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool YieldInternal();

		/// <summary>Causes the calling thread to yield execution to another thread that is ready to run on the current processor. The operating system selects the thread to yield to.</summary>
		/// <returns>
		///   <see langword="true" /> if the operating system switched execution to another thread; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003D71 RID: 15729 RVA: 0x000E7E0A File Offset: 0x000E600A
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public static bool Yield()
		{
			return Thread.YieldInternal();
		}

		/// <summary>Gets the currently running thread.</summary>
		/// <returns>A <see cref="T:System.Threading.Thread" /> that is the representation of the currently running thread.</returns>
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06003D72 RID: 15730 RVA: 0x000E7E11 File Offset: 0x000E6011
		[__DynamicallyInvokable]
		public static Thread CurrentThread
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[__DynamicallyInvokable]
			get
			{
				return Thread.GetCurrentThreadNative();
			}
		}

		// Token: 0x06003D73 RID: 15731
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Thread GetCurrentThreadNative();

		// Token: 0x06003D74 RID: 15732 RVA: 0x000E7E18 File Offset: 0x000E6018
		[SecurityCritical]
		private void SetStartHelper(Delegate start, int maxStackSize)
		{
			ulong processDefaultStackSize = Thread.GetProcessDefaultStackSize();
			if ((ulong)maxStackSize > processDefaultStackSize)
			{
				try
				{
					CodeAccessPermission.Demand(PermissionType.FullTrust);
				}
				catch (SecurityException)
				{
					maxStackSize = (int)Math.Min(processDefaultStackSize, 2147483647UL);
				}
			}
			ThreadHelper threadHelper = new ThreadHelper(start);
			if (start is ThreadStart)
			{
				this.SetStart(new ThreadStart(threadHelper.ThreadStart), maxStackSize);
				return;
			}
			this.SetStart(new ParameterizedThreadStart(threadHelper.ThreadStart), maxStackSize);
		}

		// Token: 0x06003D75 RID: 15733
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern ulong GetProcessDefaultStackSize();

		// Token: 0x06003D76 RID: 15734
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetStart(Delegate start, int maxStackSize);

		/// <summary>Ensures that resources are freed and other cleanup operations are performed when the garbage collector reclaims the <see cref="T:System.Threading.Thread" /> object.</summary>
		// Token: 0x06003D77 RID: 15735 RVA: 0x000E7E90 File Offset: 0x000E6090
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~Thread()
		{
			this.InternalFinalize();
		}

		// Token: 0x06003D78 RID: 15736
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalFinalize();

		/// <summary>Turns off automatic cleanup of runtime callable wrappers (RCW) for the current thread.</summary>
		// Token: 0x06003D79 RID: 15737
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DisableComObjectEagerCleanup();

		/// <summary>Gets or sets a value indicating whether or not a thread is a background thread.</summary>
		/// <returns>
		///   <see langword="true" /> if this thread is or is to become a background thread; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread is dead.</exception>
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06003D7A RID: 15738 RVA: 0x000E7EBC File Offset: 0x000E60BC
		// (set) Token: 0x06003D7B RID: 15739 RVA: 0x000E7EC4 File Offset: 0x000E60C4
		public bool IsBackground
		{
			[SecuritySafeCritical]
			get
			{
				return this.IsBackgroundNative();
			}
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true)]
			set
			{
				this.SetBackgroundNative(value);
			}
		}

		// Token: 0x06003D7C RID: 15740
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsBackgroundNative();

		// Token: 0x06003D7D RID: 15741
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBackgroundNative(bool isBackground);

		/// <summary>Gets a value containing the states of the current thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ThreadState" /> values indicating the state of the current thread. The initial value is <see langword="Unstarted" />.</returns>
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06003D7E RID: 15742 RVA: 0x000E7ECD File Offset: 0x000E60CD
		public ThreadState ThreadState
		{
			[SecuritySafeCritical]
			get
			{
				return (ThreadState)this.GetThreadStateNative();
			}
		}

		// Token: 0x06003D7F RID: 15743
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetThreadStateNative();

		/// <summary>Gets or sets the apartment state of this thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ApartmentState" /> values. The initial value is <see langword="Unknown" />.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to a state that is not a valid apartment state (a state other than single-threaded apartment (<see langword="STA" />) or multithreaded apartment (<see langword="MTA" />)).</exception>
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x000E7ED5 File Offset: 0x000E60D5
		// (set) Token: 0x06003D81 RID: 15745 RVA: 0x000E7EDD File Offset: 0x000E60DD
		[Obsolete("The ApartmentState property has been deprecated.  Use GetApartmentState, SetApartmentState or TrySetApartmentState instead.", false)]
		public ApartmentState ApartmentState
		{
			[SecuritySafeCritical]
			get
			{
				return (ApartmentState)this.GetApartmentStateNative();
			}
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SelfAffectingThreading = true)]
			set
			{
				this.SetApartmentStateNative((int)value, true);
			}
		}

		/// <summary>Returns an <see cref="T:System.Threading.ApartmentState" /> value indicating the apartment state.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ApartmentState" /> values indicating the apartment state of the managed thread. The default is <see cref="F:System.Threading.ApartmentState.Unknown" />.</returns>
		// Token: 0x06003D82 RID: 15746 RVA: 0x000E7EE8 File Offset: 0x000E60E8
		[SecuritySafeCritical]
		public ApartmentState GetApartmentState()
		{
			return (ApartmentState)this.GetApartmentStateNative();
		}

		/// <summary>Sets the apartment state of a thread before it is started.</summary>
		/// <param name="state">The new apartment state.</param>
		/// <returns>
		///   <see langword="true" /> if the apartment state is set; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid apartment state.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started.</exception>
		// Token: 0x06003D83 RID: 15747 RVA: 0x000E7EF0 File Offset: 0x000E60F0
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SelfAffectingThreading = true)]
		public bool TrySetApartmentState(ApartmentState state)
		{
			return this.SetApartmentStateHelper(state, false);
		}

		/// <summary>Sets the apartment state of a thread before it is started.</summary>
		/// <param name="state">The new apartment state.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core only: This member is not supported on the macOS and Linux platforms.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid apartment state.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started.</exception>
		/// <exception cref="T:System.InvalidOperationException">The apartment state has already been initialized.</exception>
		// Token: 0x06003D84 RID: 15748 RVA: 0x000E7EFC File Offset: 0x000E60FC
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SelfAffectingThreading = true)]
		public void SetApartmentState(ApartmentState state)
		{
			if (!this.SetApartmentStateHelper(state, true))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ApartmentStateSwitchFailed"));
			}
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x000E7F28 File Offset: 0x000E6128
		[SecurityCritical]
		private bool SetApartmentStateHelper(ApartmentState state, bool fireMDAOnMismatch)
		{
			ApartmentState apartmentState = (ApartmentState)this.SetApartmentStateNative((int)state, fireMDAOnMismatch);
			return (state == ApartmentState.Unknown && apartmentState == ApartmentState.MTA) || apartmentState == state;
		}

		// Token: 0x06003D86 RID: 15750
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetApartmentStateNative();

		// Token: 0x06003D87 RID: 15751
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int SetApartmentStateNative(int state, bool fireMDAOnMismatch);

		// Token: 0x06003D88 RID: 15752
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StartupSetApartmentStateInternal();

		/// <summary>Allocates an unnamed data slot on all the threads. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <returns>The allocated named data slot on all threads.</returns>
		// Token: 0x06003D89 RID: 15753 RVA: 0x000E7F4F File Offset: 0x000E614F
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return Thread.LocalDataStoreManager.AllocateDataSlot();
		}

		/// <summary>Allocates a named data slot on all threads. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <param name="name">The name of the data slot to be allocated.</param>
		/// <returns>The allocated named data slot on all threads.</returns>
		/// <exception cref="T:System.ArgumentException">A named data slot with the specified name already exists.</exception>
		// Token: 0x06003D8A RID: 15754 RVA: 0x000E7F5B File Offset: 0x000E615B
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			return Thread.LocalDataStoreManager.AllocateNamedDataSlot(name);
		}

		/// <summary>Looks up a named data slot. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <param name="name">The name of the local data slot.</param>
		/// <returns>A <see cref="T:System.LocalDataStoreSlot" /> allocated for this thread.</returns>
		// Token: 0x06003D8B RID: 15755 RVA: 0x000E7F68 File Offset: 0x000E6168
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			return Thread.LocalDataStoreManager.GetNamedDataSlot(name);
		}

		/// <summary>Eliminates the association between a name and a slot, for all threads in the process. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <param name="name">The name of the data slot to be freed.</param>
		// Token: 0x06003D8C RID: 15756 RVA: 0x000E7F75 File Offset: 0x000E6175
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static void FreeNamedDataSlot(string name)
		{
			Thread.LocalDataStoreManager.FreeNamedDataSlot(name);
		}

		/// <summary>Retrieves the value from the specified slot on the current thread, within the current thread's current domain. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <param name="slot">The <see cref="T:System.LocalDataStoreSlot" /> from which to get the value.</param>
		/// <returns>The retrieved value.</returns>
		// Token: 0x06003D8D RID: 15757 RVA: 0x000E7F84 File Offset: 0x000E6184
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static object GetData(LocalDataStoreSlot slot)
		{
			LocalDataStoreHolder localDataStoreHolder = Thread.s_LocalDataStore;
			if (localDataStoreHolder == null)
			{
				Thread.LocalDataStoreManager.ValidateSlot(slot);
				return null;
			}
			return localDataStoreHolder.Store.GetData(slot);
		}

		/// <summary>Sets the data in the specified slot on the currently running thread, for that thread's current domain. For better performance, use fields marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <param name="slot">The <see cref="T:System.LocalDataStoreSlot" /> in which to set the value.</param>
		/// <param name="data">The value to be set.</param>
		// Token: 0x06003D8E RID: 15758 RVA: 0x000E7FB4 File Offset: 0x000E61B4
		[HostProtection(SecurityAction.LinkDemand, SharedState = true, ExternalThreading = true)]
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			LocalDataStoreHolder localDataStoreHolder = Thread.s_LocalDataStore;
			if (localDataStoreHolder == null)
			{
				localDataStoreHolder = Thread.LocalDataStoreManager.CreateLocalDataStore();
				Thread.s_LocalDataStore = localDataStoreHolder;
			}
			localDataStoreHolder.Store.SetData(slot, data);
		}

		// Token: 0x06003D8F RID: 15759
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool nativeGetSafeCulture(Thread t, int appDomainId, bool isUI, ref CultureInfo safeCulture);

		/// <summary>Gets or sets the current culture used by the Resource Manager to look up culture-specific resources at run time.</summary>
		/// <returns>An object that represents the current culture.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property is set to a culture name that cannot be used to locate a resource file. Resource filenames must include only letters, numbers, hyphens or underscores.</exception>
		/// <exception cref="T:System.InvalidOperationException">.NET Core only: Reading or writing the culture of a thread from another thread is not supported.</exception>
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06003D90 RID: 15760 RVA: 0x000E7FE8 File Offset: 0x000E61E8
		// (set) Token: 0x06003D91 RID: 15761 RVA: 0x000E8008 File Offset: 0x000E6208
		[__DynamicallyInvokable]
		public CultureInfo CurrentUICulture
		{
			[__DynamicallyInvokable]
			get
			{
				if (AppDomain.IsAppXModel())
				{
					return CultureInfo.GetCultureInfoForUserPreferredLanguageInAppX() ?? this.GetCurrentUICultureNoAppX();
				}
				return this.GetCurrentUICultureNoAppX();
			}
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				CultureInfo.VerifyCultureName(value, true);
				if (!Thread.nativeSetThreadUILocale(value.SortName))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidResourceCultureName", new object[] { value.Name }));
				}
				value.StartCrossDomainTracking();
				if (!AppContextSwitches.NoAsyncCurrentCulture)
				{
					if (Thread.s_asyncLocalCurrentUICulture == null)
					{
						Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentUICulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentUICulture)), null);
					}
					Thread.s_asyncLocalCurrentUICulture.Value = value;
					return;
				}
				this.m_CurrentUICulture = value;
			}
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x000E809C File Offset: 0x000E629C
		[SecuritySafeCritical]
		internal CultureInfo GetCurrentUICultureNoAppX()
		{
			if (this.m_CurrentUICulture == null)
			{
				CultureInfo defaultThreadCurrentUICulture = CultureInfo.DefaultThreadCurrentUICulture;
				if (defaultThreadCurrentUICulture == null)
				{
					return CultureInfo.UserDefaultUICulture;
				}
				return defaultThreadCurrentUICulture;
			}
			else
			{
				CultureInfo cultureInfo = null;
				if (!Thread.nativeGetSafeCulture(this, Thread.GetDomainID(), true, ref cultureInfo) || cultureInfo == null)
				{
					return CultureInfo.UserDefaultUICulture;
				}
				return cultureInfo;
			}
		}

		// Token: 0x06003D93 RID: 15763
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool nativeSetThreadUILocale(string locale);

		/// <summary>Gets or sets the culture for the current thread.</summary>
		/// <returns>An object that represents the culture for the current thread.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">.NET Core only: Reading or writing the culture of a thread from another thread is not supported.</exception>
		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06003D94 RID: 15764 RVA: 0x000E80DE File Offset: 0x000E62DE
		// (set) Token: 0x06003D95 RID: 15765 RVA: 0x000E8100 File Offset: 0x000E6300
		[__DynamicallyInvokable]
		public CultureInfo CurrentCulture
		{
			[__DynamicallyInvokable]
			get
			{
				if (AppDomain.IsAppXModel())
				{
					return CultureInfo.GetCultureInfoForUserPreferredLanguageInAppX() ?? this.GetCurrentCultureNoAppX();
				}
				return this.GetCurrentCultureNoAppX();
			}
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				CultureInfo.nativeSetThreadLocale(value.SortName);
				value.StartCrossDomainTracking();
				if (!AppContextSwitches.NoAsyncCurrentCulture)
				{
					if (Thread.s_asyncLocalCurrentCulture == null)
					{
						Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentCulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentCulture)), null);
					}
					Thread.s_asyncLocalCurrentCulture.Value = value;
					return;
				}
				this.m_CurrentCulture = value;
			}
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x000E816C File Offset: 0x000E636C
		[SecuritySafeCritical]
		private CultureInfo GetCurrentCultureNoAppX()
		{
			if (this.m_CurrentCulture == null)
			{
				CultureInfo defaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentCulture;
				if (defaultThreadCurrentCulture == null)
				{
					return CultureInfo.UserDefaultCulture;
				}
				return defaultThreadCurrentCulture;
			}
			else
			{
				CultureInfo cultureInfo = null;
				if (!Thread.nativeGetSafeCulture(this, Thread.GetDomainID(), false, ref cultureInfo) || cultureInfo == null)
				{
					return CultureInfo.UserDefaultCulture;
				}
				return cultureInfo;
			}
		}

		/// <summary>Gets the current context in which the thread is executing.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Contexts.Context" /> representing the current thread context.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06003D97 RID: 15767 RVA: 0x000E81AE File Offset: 0x000E63AE
		public static Context CurrentContext
		{
			[SecurityCritical]
			get
			{
				return Thread.CurrentThread.GetCurrentContextInternal();
			}
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x000E81BA File Offset: 0x000E63BA
		[SecurityCritical]
		internal Context GetCurrentContextInternal()
		{
			if (this.m_Context == null)
			{
				this.m_Context = Context.DefaultContext;
			}
			return this.m_Context;
		}

		/// <summary>Gets or sets the thread's current principal (for role-based security).</summary>
		/// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> value representing the security context.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the permission required to set the principal.</exception>
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06003D99 RID: 15769 RVA: 0x000E81D8 File Offset: 0x000E63D8
		// (set) Token: 0x06003D9A RID: 15770 RVA: 0x000E8230 File Offset: 0x000E6430
		public static IPrincipal CurrentPrincipal
		{
			[SecuritySafeCritical]
			get
			{
				Thread currentThread = Thread.CurrentThread;
				IPrincipal principal2;
				lock (currentThread)
				{
					IPrincipal principal = CallContext.Principal;
					if (principal == null)
					{
						principal = Thread.GetDomain().GetThreadPrincipal();
						CallContext.Principal = principal;
					}
					principal2 = principal;
				}
				return principal2;
			}
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
			set
			{
				CallContext.Principal = value;
			}
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x000E8238 File Offset: 0x000E6438
		[SecurityCritical]
		private void SetPrincipalInternal(IPrincipal principal)
		{
			this.GetMutableExecutionContext().LogicalCallContext.SecurityData.Principal = principal;
		}

		// Token: 0x06003D9C RID: 15772
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Context GetContextInternal(IntPtr id);

		// Token: 0x06003D9D RID: 15773
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object InternalCrossContextCallback(Context ctx, IntPtr ctxID, int appDomainID, InternalCrossContextDelegate ftnToCall, object[] args);

		// Token: 0x06003D9E RID: 15774 RVA: 0x000E8250 File Offset: 0x000E6450
		[SecurityCritical]
		internal object InternalCrossContextCallback(Context ctx, InternalCrossContextDelegate ftnToCall, object[] args)
		{
			return this.InternalCrossContextCallback(ctx, ctx.InternalContextID, 0, ftnToCall, args);
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x000E8262 File Offset: 0x000E6462
		private static object CompleteCrossContextCallback(InternalCrossContextDelegate ftnToCall, object[] args)
		{
			return ftnToCall(args);
		}

		// Token: 0x06003DA0 RID: 15776
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain GetDomainInternal();

		// Token: 0x06003DA1 RID: 15777
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain GetFastDomainInternal();

		/// <summary>Returns the current domain in which the current thread is running.</summary>
		/// <returns>An <see cref="T:System.AppDomain" /> representing the current application domain of the running thread.</returns>
		// Token: 0x06003DA2 RID: 15778 RVA: 0x000E826C File Offset: 0x000E646C
		[SecuritySafeCritical]
		public static AppDomain GetDomain()
		{
			AppDomain appDomain = Thread.GetFastDomainInternal();
			if (appDomain == null)
			{
				appDomain = Thread.GetDomainInternal();
			}
			return appDomain;
		}

		/// <summary>Returns a unique application domain identifier.</summary>
		/// <returns>A 32-bit signed integer uniquely identifying the application domain.</returns>
		// Token: 0x06003DA3 RID: 15779 RVA: 0x000E8289 File Offset: 0x000E6489
		public static int GetDomainID()
		{
			return Thread.GetDomain().GetId();
		}

		/// <summary>Gets or sets the name of the thread.</summary>
		/// <returns>A string containing the name of the thread, or <see langword="null" /> if no name was set.</returns>
		/// <exception cref="T:System.InvalidOperationException">A set operation was requested, but the <see langword="Name" /> property has already been set.</exception>
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06003DA4 RID: 15780 RVA: 0x000E8295 File Offset: 0x000E6495
		// (set) Token: 0x06003DA5 RID: 15781 RVA: 0x000E82A0 File Offset: 0x000E64A0
		public string Name
		{
			get
			{
				return this.m_Name;
			}
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			set
			{
				lock (this)
				{
					if (this.m_Name != null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WriteOnce"));
					}
					this.m_Name = value;
					Thread.InformThreadNameChange(this.GetNativeHandle(), value, (value != null) ? value.Length : 0);
				}
			}
		}

		// Token: 0x06003DA6 RID: 15782
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void InformThreadNameChange(ThreadHandle t, string name, int len);

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06003DA7 RID: 15783 RVA: 0x000E830C File Offset: 0x000E650C
		// (set) Token: 0x06003DA8 RID: 15784 RVA: 0x000E8348 File Offset: 0x000E6548
		internal object AbortReason
		{
			[SecurityCritical]
			get
			{
				object obj = null;
				try
				{
					obj = this.GetAbortReason();
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ExceptionStateCrossAppDomain"), ex);
				}
				return obj;
			}
			[SecurityCritical]
			set
			{
				this.SetAbortReason(value);
			}
		}

		/// <summary>Notifies a host that execution is about to enter a region of code in which the effects of a thread abort or unhandled exception might jeopardize other tasks in the application domain.</summary>
		// Token: 0x06003DA9 RID: 15785
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginCriticalRegion();

		/// <summary>Notifies a host that execution is about to enter a region of code in which the effects of a thread abort or unhandled exception are limited to the current task.</summary>
		// Token: 0x06003DAA RID: 15786
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndCriticalRegion();

		/// <summary>Notifies a host that managed code is about to execute instructions that depend on the identity of the current physical operating system thread.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06003DAB RID: 15787
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginThreadAffinity();

		/// <summary>Notifies a host that managed code has finished executing instructions that depend on the identity of the current physical operating system thread.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06003DAC RID: 15788
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndThreadAffinity();

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <param name="address">The field to be read.</param>
		/// <returns>The latest value written to the field by any processor.</returns>
		// Token: 0x06003DAD RID: 15789 RVA: 0x000E8354 File Offset: 0x000E6554
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static byte VolatileRead(ref byte address)
		{
			byte b = address;
			Thread.MemoryBarrier();
			return b;
		}

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <param name="address">The field to be read.</param>
		/// <returns>The latest value written to the field by any processor.</returns>
		// Token: 0x06003DAE RID: 15790 RVA: 0x000E836C File Offset: 0x000E656C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static short VolatileRead(ref short address)
		{
			short num = address;
			Thread.MemoryBarrier();
			return num;
		}

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <param name="address">The field to be read.</param>
		/// <returns>The latest value written to the field by any processor.</returns>
		// Token: 0x06003DAF RID: 15791 RVA: 0x000E8384 File Offset: 0x000E6584
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static int VolatileRead(ref int address)
		{
			int num = address;
			Thread.MemoryBarrier();
			return num;
		}

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <param name="address">The field to be read.</param>
		/// <returns>The latest value written to the field by any processor.</returns>
		// Token: 0x06003DB0 RID: 15792 RVA: 0x000E839C File Offset: 0x000E659C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static long VolatileRead(ref long address)
		{
			long num = address;
			Thread.MemoryBarrier();
			return num;
		}

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <param name="address">The field to be read.</param>
		/// <returns>The latest value written to the field by any processor.</returns>
		// Token: 0x06003DB1 RID: 15793 RVA: 0x000E83B4 File Offset: 0x000E65B4
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static sbyte VolatileRead(ref sbyte address)
		{
			sbyte b = address;
			Thread.MemoryBarrier();
			return b;
		}

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <param name="address">The field to be read.</param>
		/// <returns>The latest value written to the field by any processor.</returns>
		// Token: 0x06003DB2 RID: 15794 RVA: 0x000E83CC File Offset: 0x000E65CC
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ushort VolatileRead(ref ushort address)
		{
			ushort num = address;
			Thread.MemoryBarrier();
			return num;
		}

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <param name="address">The field to be read.</param>
		/// <returns>The latest value written to the field by any processor.</returns>
		// Token: 0x06003DB3 RID: 15795 RVA: 0x000E83E4 File Offset: 0x000E65E4
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static uint VolatileRead(ref uint address)
		{
			uint num = address;
			Thread.MemoryBarrier();
			return num;
		}

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <param name="address">The field to be read.</param>
		/// <returns>The latest value written to the field by any processor.</returns>
		// Token: 0x06003DB4 RID: 15796 RVA: 0x000E83FC File Offset: 0x000E65FC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static IntPtr VolatileRead(ref IntPtr address)
		{
			IntPtr intPtr = address;
			Thread.MemoryBarrier();
			return intPtr;
		}

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <param name="address">The field to be read.</param>
		/// <returns>The latest value written to the field by any processor.</returns>
		// Token: 0x06003DB5 RID: 15797 RVA: 0x000E8414 File Offset: 0x000E6614
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static UIntPtr VolatileRead(ref UIntPtr address)
		{
			UIntPtr uintPtr = address;
			Thread.MemoryBarrier();
			return uintPtr;
		}

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <param name="address">The field to be read.</param>
		/// <returns>The latest value written to the field by any processor.</returns>
		// Token: 0x06003DB6 RID: 15798 RVA: 0x000E842C File Offset: 0x000E662C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ulong VolatileRead(ref ulong address)
		{
			ulong num = address;
			Thread.MemoryBarrier();
			return num;
		}

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <param name="address">The field to be read.</param>
		/// <returns>The latest value written to the field by any processor.</returns>
		// Token: 0x06003DB7 RID: 15799 RVA: 0x000E8444 File Offset: 0x000E6644
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static float VolatileRead(ref float address)
		{
			float num = address;
			Thread.MemoryBarrier();
			return num;
		}

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <param name="address">The field to be read.</param>
		/// <returns>The latest value written to the field by any processor.</returns>
		// Token: 0x06003DB8 RID: 15800 RVA: 0x000E845C File Offset: 0x000E665C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static double VolatileRead(ref double address)
		{
			double num = address;
			Thread.MemoryBarrier();
			return num;
		}

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <param name="address">The field to be read.</param>
		/// <returns>The latest value written to the field by any processor.</returns>
		// Token: 0x06003DB9 RID: 15801 RVA: 0x000E8474 File Offset: 0x000E6674
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static object VolatileRead(ref object address)
		{
			object obj = address;
			Thread.MemoryBarrier();
			return obj;
		}

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written.</param>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06003DBA RID: 15802 RVA: 0x000E848A File Offset: 0x000E668A
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref byte address, byte value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written.</param>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06003DBB RID: 15803 RVA: 0x000E8494 File Offset: 0x000E6694
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref short address, short value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written.</param>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06003DBC RID: 15804 RVA: 0x000E849E File Offset: 0x000E669E
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref int address, int value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written.</param>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06003DBD RID: 15805 RVA: 0x000E84A8 File Offset: 0x000E66A8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref long address, long value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written.</param>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06003DBE RID: 15806 RVA: 0x000E84B2 File Offset: 0x000E66B2
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref sbyte address, sbyte value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written.</param>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06003DBF RID: 15807 RVA: 0x000E84BC File Offset: 0x000E66BC
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref ushort address, ushort value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written.</param>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06003DC0 RID: 15808 RVA: 0x000E84C6 File Offset: 0x000E66C6
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref uint address, uint value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written.</param>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06003DC1 RID: 15809 RVA: 0x000E84D0 File Offset: 0x000E66D0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref IntPtr address, IntPtr value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written.</param>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06003DC2 RID: 15810 RVA: 0x000E84DA File Offset: 0x000E66DA
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref UIntPtr address, UIntPtr value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written.</param>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06003DC3 RID: 15811 RVA: 0x000E84E4 File Offset: 0x000E66E4
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref ulong address, ulong value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written.</param>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06003DC4 RID: 15812 RVA: 0x000E84EE File Offset: 0x000E66EE
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref float address, float value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written.</param>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06003DC5 RID: 15813 RVA: 0x000E84F8 File Offset: 0x000E66F8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref double address, double value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written.</param>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06003DC6 RID: 15814 RVA: 0x000E8502 File Offset: 0x000E6702
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void VolatileWrite(ref object address, object value)
		{
			Thread.MemoryBarrier();
			address = value;
		}

		/// <summary>Synchronizes memory access as follows: The processor executing the current thread cannot reorder instructions in such a way that memory accesses prior to the call to <see cref="M:System.Threading.Thread.MemoryBarrier" /> execute after memory accesses that follow the call to <see cref="M:System.Threading.Thread.MemoryBarrier" />.</summary>
		// Token: 0x06003DC7 RID: 15815
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MemoryBarrier();

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x000E850C File Offset: 0x000E670C
		private static LocalDataStoreMgr LocalDataStoreManager
		{
			get
			{
				if (Thread.s_LocalDataStoreMgr == null)
				{
					Interlocked.CompareExchange<LocalDataStoreMgr>(ref Thread.s_LocalDataStoreMgr, new LocalDataStoreMgr(), null);
				}
				return Thread.s_LocalDataStoreMgr;
			}
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06003DC9 RID: 15817 RVA: 0x000E852B File Offset: 0x000E672B
		void _Thread.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06003DCA RID: 15818 RVA: 0x000E8532 File Offset: 0x000E6732
		void _Thread.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06003DCB RID: 15819 RVA: 0x000E8539 File Offset: 0x000E6739
		void _Thread.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06003DCC RID: 15820 RVA: 0x000E8540 File Offset: 0x000E6740
		void _Thread.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003DCD RID: 15821
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetAbortReason(object o);

		// Token: 0x06003DCE RID: 15822
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object GetAbortReason();

		// Token: 0x06003DCF RID: 15823
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void ClearAbortReason();

		// Token: 0x040019F0 RID: 6640
		private Context m_Context;

		// Token: 0x040019F1 RID: 6641
		private ExecutionContext m_ExecutionContext;

		// Token: 0x040019F2 RID: 6642
		private string m_Name;

		// Token: 0x040019F3 RID: 6643
		private Delegate m_Delegate;

		// Token: 0x040019F4 RID: 6644
		private CultureInfo m_CurrentCulture;

		// Token: 0x040019F5 RID: 6645
		private CultureInfo m_CurrentUICulture;

		// Token: 0x040019F6 RID: 6646
		private object m_ThreadStartArg;

		// Token: 0x040019F7 RID: 6647
		private IntPtr DONT_USE_InternalThread;

		// Token: 0x040019F8 RID: 6648
		private int m_Priority;

		// Token: 0x040019F9 RID: 6649
		private int m_ManagedThreadId;

		// Token: 0x040019FA RID: 6650
		private bool m_ExecutionContextBelongsToOuterScope;

		// Token: 0x040019FB RID: 6651
		private static LocalDataStoreMgr s_LocalDataStoreMgr;

		// Token: 0x040019FC RID: 6652
		[ThreadStatic]
		private static LocalDataStoreHolder s_LocalDataStore;

		// Token: 0x040019FD RID: 6653
		private static AsyncLocal<CultureInfo> s_asyncLocalCurrentCulture;

		// Token: 0x040019FE RID: 6654
		private static AsyncLocal<CultureInfo> s_asyncLocalCurrentUICulture;

		// Token: 0x040019FF RID: 6655
		[ThreadStatic]
		private static int t_currentProcessorIdCache;

		// Token: 0x04001A00 RID: 6656
		private const int ProcessorIdCacheShift = 16;

		// Token: 0x04001A01 RID: 6657
		private const int ProcessorIdCacheCountDownMask = 65535;

		// Token: 0x04001A02 RID: 6658
		private const int ProcessorIdRefreshRate = 5000;
	}
}
