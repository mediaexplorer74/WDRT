using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	/// <summary>Manages the execution context for the current thread. This class cannot be inherited.</summary>
	// Token: 0x020004F6 RID: 1270
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class ExecutionContext : IDisposable, ISerializable
	{
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06003C0D RID: 15373 RVA: 0x000E4C2A File Offset: 0x000E2E2A
		// (set) Token: 0x06003C0E RID: 15374 RVA: 0x000E4C37 File Offset: 0x000E2E37
		internal bool isNewCapture
		{
			get
			{
				return (this._flags & (ExecutionContext.Flags)5) > ExecutionContext.Flags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= ExecutionContext.Flags.IsNewCapture;
					return;
				}
				this._flags &= (ExecutionContext.Flags)(-2);
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06003C0F RID: 15375 RVA: 0x000E4C5A File Offset: 0x000E2E5A
		// (set) Token: 0x06003C10 RID: 15376 RVA: 0x000E4C67 File Offset: 0x000E2E67
		internal bool isFlowSuppressed
		{
			get
			{
				return (this._flags & ExecutionContext.Flags.IsFlowSuppressed) > ExecutionContext.Flags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= ExecutionContext.Flags.IsFlowSuppressed;
					return;
				}
				this._flags &= (ExecutionContext.Flags)(-3);
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06003C11 RID: 15377 RVA: 0x000E4C8A File Offset: 0x000E2E8A
		internal static ExecutionContext PreAllocatedDefault
		{
			[SecuritySafeCritical]
			get
			{
				return ExecutionContext.s_dummyDefaultEC;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06003C12 RID: 15378 RVA: 0x000E4C91 File Offset: 0x000E2E91
		internal bool IsPreAllocatedDefault
		{
			get
			{
				return (this._flags & ExecutionContext.Flags.IsPreAllocatedDefault) != ExecutionContext.Flags.None;
			}
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x000E4CA0 File Offset: 0x000E2EA0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext()
		{
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x000E4CA8 File Offset: 0x000E2EA8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext(bool isPreAllocatedDefault)
		{
			if (isPreAllocatedDefault)
			{
				this._flags = ExecutionContext.Flags.IsPreAllocatedDefault;
			}
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x000E4CBC File Offset: 0x000E2EBC
		[SecurityCritical]
		internal static object GetLocalValue(IAsyncLocal local)
		{
			return Thread.CurrentThread.GetExecutionContextReader().GetLocalValue(local);
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x000E4CDC File Offset: 0x000E2EDC
		[SecurityCritical]
		internal static void SetLocalValue(IAsyncLocal local, object newValue, bool needChangeNotifications)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			object obj = null;
			bool flag = mutableExecutionContext._localValues != null && mutableExecutionContext._localValues.TryGetValue(local, out obj);
			if (obj == newValue)
			{
				return;
			}
			IAsyncLocalValueMap asyncLocalValueMap = mutableExecutionContext._localValues;
			if (asyncLocalValueMap == null)
			{
				asyncLocalValueMap = AsyncLocalValueMap.Create(local, newValue, !needChangeNotifications);
			}
			else
			{
				asyncLocalValueMap = asyncLocalValueMap.Set(local, newValue, !needChangeNotifications);
			}
			mutableExecutionContext._localValues = asyncLocalValueMap;
			if (needChangeNotifications)
			{
				if (!flag)
				{
					IAsyncLocal[] array = mutableExecutionContext._localChangeNotifications;
					if (array == null)
					{
						array = new IAsyncLocal[] { local };
					}
					else
					{
						int num = array.Length;
						Array.Resize<IAsyncLocal>(ref array, num + 1);
						array[num] = local;
					}
					mutableExecutionContext._localChangeNotifications = array;
				}
				local.OnValueChanged(obj, newValue, false);
			}
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x000E4D8C File Offset: 0x000E2F8C
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static void OnAsyncLocalContextChanged(ExecutionContext previous, ExecutionContext current)
		{
			IAsyncLocal[] array = ((previous == null) ? null : previous._localChangeNotifications);
			if (array != null)
			{
				foreach (IAsyncLocal asyncLocal in array)
				{
					object obj = null;
					if (previous != null && previous._localValues != null)
					{
						previous._localValues.TryGetValue(asyncLocal, out obj);
					}
					object obj2 = null;
					if (current != null && current._localValues != null)
					{
						current._localValues.TryGetValue(asyncLocal, out obj2);
					}
					if (obj != obj2)
					{
						asyncLocal.OnValueChanged(obj, obj2, true);
					}
				}
			}
			IAsyncLocal[] array3 = ((current == null) ? null : current._localChangeNotifications);
			if (array3 != null && array3 != array)
			{
				try
				{
					foreach (IAsyncLocal asyncLocal2 in array3)
					{
						object obj3 = null;
						if (previous == null || previous._localValues == null || !previous._localValues.TryGetValue(asyncLocal2, out obj3))
						{
							object obj4 = null;
							if (current != null && current._localValues != null)
							{
								current._localValues.TryGetValue(asyncLocal2, out obj4);
							}
							if (obj3 != obj4)
							{
								asyncLocal2.OnValueChanged(obj3, obj4, true);
							}
						}
					}
				}
				catch (Exception ex)
				{
					Environment.FailFast(Environment.GetResourceString("ExecutionContext_ExceptionInAsyncLocalNotification"), ex);
				}
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06003C18 RID: 15384 RVA: 0x000E4EBC File Offset: 0x000E30BC
		// (set) Token: 0x06003C19 RID: 15385 RVA: 0x000E4ED7 File Offset: 0x000E30D7
		internal LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				if (this._logicalCallContext == null)
				{
					this._logicalCallContext = new LogicalCallContext();
				}
				return this._logicalCallContext;
			}
			[SecurityCritical]
			set
			{
				this._logicalCallContext = value;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06003C1A RID: 15386 RVA: 0x000E4EE0 File Offset: 0x000E30E0
		// (set) Token: 0x06003C1B RID: 15387 RVA: 0x000E4EFB File Offset: 0x000E30FB
		internal IllogicalCallContext IllogicalCallContext
		{
			get
			{
				if (this._illogicalCallContext == null)
				{
					this._illogicalCallContext = new IllogicalCallContext();
				}
				return this._illogicalCallContext;
			}
			set
			{
				this._illogicalCallContext = value;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06003C1C RID: 15388 RVA: 0x000E4F04 File Offset: 0x000E3104
		// (set) Token: 0x06003C1D RID: 15389 RVA: 0x000E4F0C File Offset: 0x000E310C
		internal SynchronizationContext SynchronizationContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._syncContext;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._syncContext = value;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x000E4F15 File Offset: 0x000E3115
		// (set) Token: 0x06003C1F RID: 15391 RVA: 0x000E4F1D File Offset: 0x000E311D
		internal SynchronizationContext SynchronizationContextNoFlow
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._syncContextNoFlow;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._syncContextNoFlow = value;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x000E4F26 File Offset: 0x000E3126
		// (set) Token: 0x06003C21 RID: 15393 RVA: 0x000E4F2E File Offset: 0x000E312E
		internal HostExecutionContext HostExecutionContext
		{
			get
			{
				return this._hostExecutionContext;
			}
			set
			{
				this._hostExecutionContext = value;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x000E4F37 File Offset: 0x000E3137
		// (set) Token: 0x06003C23 RID: 15395 RVA: 0x000E4F3F File Offset: 0x000E313F
		internal SecurityContext SecurityContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._securityContext;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._securityContext = value;
				if (value != null)
				{
					this._securityContext.ExecutionContext = this;
				}
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.ExecutionContext" /> class.</summary>
		// Token: 0x06003C24 RID: 15396 RVA: 0x000E4F57 File Offset: 0x000E3157
		public void Dispose()
		{
			if (this.IsPreAllocatedDefault)
			{
				return;
			}
			if (this._hostExecutionContext != null)
			{
				this._hostExecutionContext.Dispose();
			}
			if (this._securityContext != null)
			{
				this._securityContext.Dispose();
			}
		}

		/// <summary>Runs a method in a specified execution context on the current thread.</summary>
		/// <param name="executionContext">The <see cref="T:System.Threading.ExecutionContext" /> to set.</param>
		/// <param name="callback">A <see cref="T:System.Threading.ContextCallback" /> delegate that represents the method to be run in the provided execution context.</param>
		/// <param name="state">The object to pass to the callback method.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="executionContext" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="executionContext" /> was not acquired through a capture operation.  
		/// -or-  
		/// <paramref name="executionContext" /> has already been used as the argument to a <see cref="M:System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext,System.Threading.ContextCallback,System.Object)" /> call.</exception>
		// Token: 0x06003C25 RID: 15397 RVA: 0x000E4F88 File Offset: 0x000E3188
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void Run(ExecutionContext executionContext, ContextCallback callback, object state)
		{
			if (executionContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullContext"));
			}
			if (!executionContext.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
			}
			ExecutionContext.Run(executionContext, callback, state, false);
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x000E4FBE File Offset: 0x000E31BE
		[SecurityCritical]
		[FriendAccessAllowed]
		internal static void Run(ExecutionContext executionContext, ContextCallback callback, object state, bool preserveSyncCtx)
		{
			ExecutionContext.RunInternal(executionContext, callback, state, preserveSyncCtx);
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x000E4FCC File Offset: 0x000E31CC
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static void RunInternal(ExecutionContext executionContext, ContextCallback callback, object state, bool preserveSyncCtx)
		{
			if (!executionContext.IsPreAllocatedDefault)
			{
				executionContext.isNewCapture = false;
			}
			Thread currentThread = Thread.CurrentThread;
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
				if ((executionContextReader.IsNull || executionContextReader.IsDefaultFTContext(preserveSyncCtx)) && SecurityContext.CurrentlyInDefaultFTSecurityContext(executionContextReader) && executionContext.IsDefaultFTContext(preserveSyncCtx) && executionContextReader.HasSameLocalValues(executionContext))
				{
					ExecutionContext.EstablishCopyOnWriteScope(currentThread, true, ref executionContextSwitcher);
				}
				else
				{
					if (executionContext.IsPreAllocatedDefault)
					{
						executionContext = new ExecutionContext();
					}
					executionContextSwitcher = ExecutionContext.SetExecutionContext(executionContext, preserveSyncCtx);
				}
				callback(state);
			}
			finally
			{
				executionContextSwitcher.Undo();
			}
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x000E5074 File Offset: 0x000E3274
		[SecurityCritical]
		internal static void EstablishCopyOnWriteScope(ref ExecutionContextSwitcher ecsw)
		{
			ExecutionContext.EstablishCopyOnWriteScope(Thread.CurrentThread, false, ref ecsw);
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x000E5084 File Offset: 0x000E3284
		[SecurityCritical]
		private static void EstablishCopyOnWriteScope(Thread currentThread, bool knownNullWindowsIdentity, ref ExecutionContextSwitcher ecsw)
		{
			ecsw.outerEC = currentThread.GetExecutionContextReader();
			ecsw.outerECBelongsToScope = currentThread.ExecutionContextBelongsToCurrentScope;
			ecsw.cachedAlwaysFlowImpersonationPolicy = SecurityContext.AlwaysFlowImpersonationPolicy;
			if (!knownNullWindowsIdentity)
			{
				ecsw.wi = SecurityContext.GetCurrentWI(ecsw.outerEC, ecsw.cachedAlwaysFlowImpersonationPolicy);
			}
			ecsw.wiIsValid = true;
			currentThread.ExecutionContextBelongsToCurrentScope = false;
			ecsw.thread = currentThread;
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x000E50E4 File Offset: 0x000E32E4
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static ExecutionContextSwitcher SetExecutionContext(ExecutionContext executionContext, bool preserveSyncCtx)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			Thread currentThread = Thread.CurrentThread;
			ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
			executionContextSwitcher.thread = currentThread;
			executionContextSwitcher.outerEC = executionContextReader;
			executionContextSwitcher.outerECBelongsToScope = currentThread.ExecutionContextBelongsToCurrentScope;
			if (preserveSyncCtx)
			{
				executionContext.SynchronizationContext = executionContextReader.SynchronizationContext;
			}
			executionContext.SynchronizationContextNoFlow = executionContextReader.SynchronizationContextNoFlow;
			currentThread.SetExecutionContext(executionContext, true);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.OnAsyncLocalContextChanged(executionContextReader.DangerousGetRawExecutionContext(), executionContext);
				SecurityContext securityContext = executionContext.SecurityContext;
				if (securityContext != null)
				{
					SecurityContext.Reader securityContext2 = executionContextReader.SecurityContext;
					executionContextSwitcher.scsw = SecurityContext.SetSecurityContext(securityContext, securityContext2, false, ref stackCrawlMark);
				}
				else if (!SecurityContext.CurrentlyInDefaultFTSecurityContext(executionContextSwitcher.outerEC))
				{
					SecurityContext.Reader securityContext3 = executionContextReader.SecurityContext;
					executionContextSwitcher.scsw = SecurityContext.SetSecurityContext(SecurityContext.FullTrustSecurityContext, securityContext3, false, ref stackCrawlMark);
				}
				HostExecutionContext hostExecutionContext = executionContext.HostExecutionContext;
				if (hostExecutionContext != null)
				{
					executionContextSwitcher.hecsw = HostExecutionContextManager.SetHostExecutionContextInternal(hostExecutionContext);
				}
			}
			catch
			{
				executionContextSwitcher.UndoNoThrow();
				throw;
			}
			return executionContextSwitcher;
		}

		/// <summary>Creates a copy of the current execution context.</summary>
		/// <returns>An <see cref="T:System.Threading.ExecutionContext" /> object representing the current execution context.</returns>
		/// <exception cref="T:System.InvalidOperationException">This context cannot be copied because it is used. Only newly captured contexts can be copied.</exception>
		// Token: 0x06003C2B RID: 15403 RVA: 0x000E51EC File Offset: 0x000E33EC
		[SecuritySafeCritical]
		public ExecutionContext CreateCopy()
		{
			if (!this.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotCopyUsedContext"));
			}
			ExecutionContext executionContext = new ExecutionContext();
			executionContext.isNewCapture = true;
			executionContext._syncContext = ((this._syncContext == null) ? null : this._syncContext.CreateCopy());
			executionContext._localValues = this._localValues;
			executionContext._localChangeNotifications = this._localChangeNotifications;
			executionContext._hostExecutionContext = ((this._hostExecutionContext == null) ? null : this._hostExecutionContext.CreateCopy());
			if (this._securityContext != null)
			{
				executionContext._securityContext = this._securityContext.CreateCopy();
				executionContext._securityContext.ExecutionContext = executionContext;
			}
			if (this._logicalCallContext != null)
			{
				executionContext.LogicalCallContext = (LogicalCallContext)this.LogicalCallContext.Clone();
			}
			return executionContext;
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x000E52B4 File Offset: 0x000E34B4
		[SecuritySafeCritical]
		internal ExecutionContext CreateMutableCopy()
		{
			ExecutionContext executionContext = new ExecutionContext();
			executionContext._syncContext = this._syncContext;
			executionContext._syncContextNoFlow = this._syncContextNoFlow;
			executionContext._hostExecutionContext = ((this._hostExecutionContext == null) ? null : this._hostExecutionContext.CreateCopy());
			if (this._securityContext != null)
			{
				executionContext._securityContext = this._securityContext.CreateMutableCopy();
				executionContext._securityContext.ExecutionContext = executionContext;
			}
			if (this._logicalCallContext != null)
			{
				executionContext.LogicalCallContext = (LogicalCallContext)this.LogicalCallContext.Clone();
			}
			if (this._illogicalCallContext != null)
			{
				executionContext.IllogicalCallContext = this.IllogicalCallContext.CreateCopy();
			}
			executionContext._localValues = this._localValues;
			executionContext._localChangeNotifications = this._localChangeNotifications;
			executionContext.isFlowSuppressed = this.isFlowSuppressed;
			return executionContext;
		}

		/// <summary>Suppresses the flow of the execution context across asynchronous threads.</summary>
		/// <returns>An <see cref="T:System.Threading.AsyncFlowControl" /> structure for restoring the flow.</returns>
		/// <exception cref="T:System.InvalidOperationException">The context flow is already suppressed.</exception>
		// Token: 0x06003C2D RID: 15405 RVA: 0x000E537C File Offset: 0x000E357C
		[SecurityCritical]
		public static AsyncFlowControl SuppressFlow()
		{
			if (ExecutionContext.IsFlowSuppressed())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotSupressFlowMultipleTimes"));
			}
			AsyncFlowControl asyncFlowControl = default(AsyncFlowControl);
			asyncFlowControl.Setup();
			return asyncFlowControl;
		}

		/// <summary>Restores the flow of the execution context across asynchronous threads.</summary>
		/// <exception cref="T:System.InvalidOperationException">The context flow cannot be restored because it is not being suppressed.</exception>
		// Token: 0x06003C2E RID: 15406 RVA: 0x000E53B0 File Offset: 0x000E35B0
		[SecuritySafeCritical]
		public static void RestoreFlow()
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			if (!mutableExecutionContext.isFlowSuppressed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRestoreUnsupressedFlow"));
			}
			mutableExecutionContext.isFlowSuppressed = false;
		}

		/// <summary>Indicates whether the flow of the execution context is currently suppressed.</summary>
		/// <returns>
		///   <see langword="true" /> if the flow is suppressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003C2F RID: 15407 RVA: 0x000E53E8 File Offset: 0x000E35E8
		public static bool IsFlowSuppressed()
		{
			return Thread.CurrentThread.GetExecutionContextReader().IsFlowSuppressed;
		}

		/// <summary>Captures the execution context from the current thread.</summary>
		/// <returns>An <see cref="T:System.Threading.ExecutionContext" /> object representing the execution context for the current thread.</returns>
		// Token: 0x06003C30 RID: 15408 RVA: 0x000E5408 File Offset: 0x000E3608
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ExecutionContext Capture()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.None);
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x000E5420 File Offset: 0x000E3620
		[SecuritySafeCritical]
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static ExecutionContext FastCapture()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x000E5438 File Offset: 0x000E3638
		[SecurityCritical]
		internal static ExecutionContext Capture(ref StackCrawlMark stackMark, ExecutionContext.CaptureOptions options)
		{
			ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
			if (executionContextReader.IsFlowSuppressed)
			{
				return null;
			}
			SecurityContext securityContext = SecurityContext.Capture(executionContextReader, ref stackMark);
			HostExecutionContext hostExecutionContext = HostExecutionContextManager.CaptureHostExecutionContext();
			SynchronizationContext synchronizationContext = null;
			LogicalCallContext logicalCallContext = null;
			if (!executionContextReader.IsNull)
			{
				if ((options & ExecutionContext.CaptureOptions.IgnoreSyncCtx) == ExecutionContext.CaptureOptions.None)
				{
					synchronizationContext = ((executionContextReader.SynchronizationContext == null) ? null : executionContextReader.SynchronizationContext.CreateCopy());
				}
				if (executionContextReader.LogicalCallContext.HasInfo)
				{
					logicalCallContext = executionContextReader.LogicalCallContext.Clone();
				}
			}
			IAsyncLocalValueMap asyncLocalValueMap = null;
			IAsyncLocal[] array = null;
			if (!executionContextReader.IsNull)
			{
				asyncLocalValueMap = executionContextReader.DangerousGetRawExecutionContext()._localValues;
				array = executionContextReader.DangerousGetRawExecutionContext()._localChangeNotifications;
			}
			if ((options & ExecutionContext.CaptureOptions.OptimizeDefaultCase) != ExecutionContext.CaptureOptions.None && securityContext == null && hostExecutionContext == null && synchronizationContext == null && (logicalCallContext == null || !logicalCallContext.HasInfo) && asyncLocalValueMap == null && array == null)
			{
				return ExecutionContext.s_dummyDefaultEC;
			}
			ExecutionContext executionContext = new ExecutionContext();
			executionContext.SecurityContext = securityContext;
			if (executionContext.SecurityContext != null)
			{
				executionContext.SecurityContext.ExecutionContext = executionContext;
			}
			executionContext._hostExecutionContext = hostExecutionContext;
			executionContext._syncContext = synchronizationContext;
			executionContext.LogicalCallContext = logicalCallContext;
			executionContext._localValues = asyncLocalValueMap;
			executionContext._localChangeNotifications = array;
			executionContext.isNewCapture = true;
			return executionContext;
		}

		/// <summary>Sets the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate an instance of the current execution context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to be populated with serialization information.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure representing the destination context of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06003C33 RID: 15411 RVA: 0x000E5568 File Offset: 0x000E3768
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this._logicalCallContext != null)
			{
				info.AddValue("LogicalCallContext", this._logicalCallContext, typeof(LogicalCallContext));
			}
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x000E559C File Offset: 0x000E379C
		[SecurityCritical]
		private ExecutionContext(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("LogicalCallContext"))
				{
					this._logicalCallContext = (LogicalCallContext)enumerator.Value;
				}
			}
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x000E55E4 File Offset: 0x000E37E4
		[SecurityCritical]
		internal bool IsDefaultFTContext(bool ignoreSyncCtx)
		{
			return this._hostExecutionContext == null && (ignoreSyncCtx || this._syncContext == null) && (this._securityContext == null || this._securityContext.IsDefaultFTSecurityContext()) && (this._logicalCallContext == null || !this._logicalCallContext.HasInfo) && (this._illogicalCallContext == null || !this._illogicalCallContext.HasUserData);
		}

		// Token: 0x04001996 RID: 6550
		private HostExecutionContext _hostExecutionContext;

		// Token: 0x04001997 RID: 6551
		private SynchronizationContext _syncContext;

		// Token: 0x04001998 RID: 6552
		private SynchronizationContext _syncContextNoFlow;

		// Token: 0x04001999 RID: 6553
		private SecurityContext _securityContext;

		// Token: 0x0400199A RID: 6554
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x0400199B RID: 6555
		private IllogicalCallContext _illogicalCallContext;

		// Token: 0x0400199C RID: 6556
		private ExecutionContext.Flags _flags;

		// Token: 0x0400199D RID: 6557
		private IAsyncLocalValueMap _localValues;

		// Token: 0x0400199E RID: 6558
		private IAsyncLocal[] _localChangeNotifications;

		// Token: 0x0400199F RID: 6559
		private static readonly ExecutionContext s_dummyDefaultEC = new ExecutionContext(true);

		// Token: 0x02000BE8 RID: 3048
		private enum Flags
		{
			// Token: 0x0400360E RID: 13838
			None,
			// Token: 0x0400360F RID: 13839
			IsNewCapture,
			// Token: 0x04003610 RID: 13840
			IsFlowSuppressed,
			// Token: 0x04003611 RID: 13841
			IsPreAllocatedDefault = 4
		}

		// Token: 0x02000BE9 RID: 3049
		internal struct Reader
		{
			// Token: 0x06006F6B RID: 28523 RVA: 0x001811C5 File Offset: 0x0017F3C5
			public Reader(ExecutionContext ec)
			{
				this.m_ec = ec;
			}

			// Token: 0x06006F6C RID: 28524 RVA: 0x001811CE File Offset: 0x0017F3CE
			public ExecutionContext DangerousGetRawExecutionContext()
			{
				return this.m_ec;
			}

			// Token: 0x1700131B RID: 4891
			// (get) Token: 0x06006F6D RID: 28525 RVA: 0x001811D6 File Offset: 0x0017F3D6
			public bool IsNull
			{
				get
				{
					return this.m_ec == null;
				}
			}

			// Token: 0x06006F6E RID: 28526 RVA: 0x001811E1 File Offset: 0x0017F3E1
			[SecurityCritical]
			public bool IsDefaultFTContext(bool ignoreSyncCtx)
			{
				return this.m_ec.IsDefaultFTContext(ignoreSyncCtx);
			}

			// Token: 0x1700131C RID: 4892
			// (get) Token: 0x06006F6F RID: 28527 RVA: 0x001811EF File Offset: 0x0017F3EF
			public bool IsFlowSuppressed
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsNull && this.m_ec.isFlowSuppressed;
				}
			}

			// Token: 0x06006F70 RID: 28528 RVA: 0x00181206 File Offset: 0x0017F406
			public bool IsSame(ExecutionContext.Reader other)
			{
				return this.m_ec == other.m_ec;
			}

			// Token: 0x1700131D RID: 4893
			// (get) Token: 0x06006F71 RID: 28529 RVA: 0x00181216 File Offset: 0x0017F416
			public SynchronizationContext SynchronizationContext
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ec.SynchronizationContext;
					}
					return null;
				}
			}

			// Token: 0x1700131E RID: 4894
			// (get) Token: 0x06006F72 RID: 28530 RVA: 0x0018122D File Offset: 0x0017F42D
			public SynchronizationContext SynchronizationContextNoFlow
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ec.SynchronizationContextNoFlow;
					}
					return null;
				}
			}

			// Token: 0x1700131F RID: 4895
			// (get) Token: 0x06006F73 RID: 28531 RVA: 0x00181244 File Offset: 0x0017F444
			public SecurityContext.Reader SecurityContext
			{
				[SecurityCritical]
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return new SecurityContext.Reader(this.IsNull ? null : this.m_ec.SecurityContext);
				}
			}

			// Token: 0x17001320 RID: 4896
			// (get) Token: 0x06006F74 RID: 28532 RVA: 0x00181261 File Offset: 0x0017F461
			public LogicalCallContext.Reader LogicalCallContext
			{
				[SecurityCritical]
				get
				{
					return new LogicalCallContext.Reader(this.IsNull ? null : this.m_ec.LogicalCallContext);
				}
			}

			// Token: 0x17001321 RID: 4897
			// (get) Token: 0x06006F75 RID: 28533 RVA: 0x0018127E File Offset: 0x0017F47E
			public IllogicalCallContext.Reader IllogicalCallContext
			{
				[SecurityCritical]
				get
				{
					return new IllogicalCallContext.Reader(this.IsNull ? null : this.m_ec.IllogicalCallContext);
				}
			}

			// Token: 0x06006F76 RID: 28534 RVA: 0x0018129C File Offset: 0x0017F49C
			[SecurityCritical]
			public object GetLocalValue(IAsyncLocal local)
			{
				if (this.IsNull)
				{
					return null;
				}
				if (this.m_ec._localValues == null)
				{
					return null;
				}
				object obj;
				this.m_ec._localValues.TryGetValue(local, out obj);
				return obj;
			}

			// Token: 0x06006F77 RID: 28535 RVA: 0x001812D8 File Offset: 0x0017F4D8
			[SecurityCritical]
			public bool HasSameLocalValues(ExecutionContext other)
			{
				IAsyncLocalValueMap asyncLocalValueMap = (this.IsNull ? null : this.m_ec._localValues);
				IAsyncLocalValueMap asyncLocalValueMap2 = ((other == null) ? null : other._localValues);
				return asyncLocalValueMap == asyncLocalValueMap2;
			}

			// Token: 0x06006F78 RID: 28536 RVA: 0x0018130D File Offset: 0x0017F50D
			[SecurityCritical]
			public bool HasLocalValues()
			{
				return !this.IsNull && this.m_ec._localValues != null;
			}

			// Token: 0x04003612 RID: 13842
			private ExecutionContext m_ec;
		}

		// Token: 0x02000BEA RID: 3050
		[Flags]
		internal enum CaptureOptions
		{
			// Token: 0x04003614 RID: 13844
			None = 0,
			// Token: 0x04003615 RID: 13845
			IgnoreSyncCtx = 1,
			// Token: 0x04003616 RID: 13846
			OptimizeDefaultCase = 2
		}
	}
}
