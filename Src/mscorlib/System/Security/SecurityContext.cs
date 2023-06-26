using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security
{
	/// <summary>Encapsulates and propagates all security-related data for execution contexts transferred across threads. This class cannot be inherited.</summary>
	// Token: 0x020001EE RID: 494
	public sealed class SecurityContext : IDisposable
	{
		// Token: 0x06001DAF RID: 7599 RVA: 0x00067A58 File Offset: 0x00065C58
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal SecurityContext()
		{
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x00067A60 File Offset: 0x00065C60
		internal static SecurityContext FullTrustSecurityContext
		{
			[SecurityCritical]
			get
			{
				if (SecurityContext._fullTrustSC == null)
				{
					SecurityContext._fullTrustSC = SecurityContext.CreateFullTrustSecurityContext();
				}
				return SecurityContext._fullTrustSC;
			}
		}

		// Token: 0x1700034C RID: 844
		// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x00067A7E File Offset: 0x00065C7E
		internal ExecutionContext ExecutionContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._executionContext = value;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x00067A87 File Offset: 0x00065C87
		// (set) Token: 0x06001DB3 RID: 7603 RVA: 0x00067A91 File Offset: 0x00065C91
		internal WindowsIdentity WindowsIdentity
		{
			get
			{
				return this._windowsIdentity;
			}
			set
			{
				this._windowsIdentity = value;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x00067A9C File Offset: 0x00065C9C
		// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x00067AA6 File Offset: 0x00065CA6
		internal CompressedStack CompressedStack
		{
			get
			{
				return this._compressedStack;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._compressedStack = value;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.SecurityContext" /> class.</summary>
		// Token: 0x06001DB6 RID: 7606 RVA: 0x00067AB1 File Offset: 0x00065CB1
		public void Dispose()
		{
			if (this._windowsIdentity != null)
			{
				this._windowsIdentity.Dispose();
			}
		}

		/// <summary>Suppresses the flow of the security context across asynchronous threads.</summary>
		/// <returns>An <see cref="T:System.Threading.AsyncFlowControl" /> structure for restoring the flow.</returns>
		// Token: 0x06001DB7 RID: 7607 RVA: 0x00067ACA File Offset: 0x00065CCA
		[SecurityCritical]
		public static AsyncFlowControl SuppressFlow()
		{
			return SecurityContext.SuppressFlow(SecurityContextDisableFlow.All);
		}

		/// <summary>Suppresses the flow of the Windows identity portion of the current security context across asynchronous threads.</summary>
		/// <returns>A structure for restoring the flow.</returns>
		// Token: 0x06001DB8 RID: 7608 RVA: 0x00067AD6 File Offset: 0x00065CD6
		[SecurityCritical]
		public static AsyncFlowControl SuppressFlowWindowsIdentity()
		{
			return SecurityContext.SuppressFlow(SecurityContextDisableFlow.WI);
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x00067AE0 File Offset: 0x00065CE0
		[SecurityCritical]
		internal static AsyncFlowControl SuppressFlow(SecurityContextDisableFlow flags)
		{
			if (SecurityContext.IsFlowSuppressed(flags))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotSupressFlowMultipleTimes"));
			}
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			if (mutableExecutionContext.SecurityContext == null)
			{
				mutableExecutionContext.SecurityContext = new SecurityContext();
			}
			AsyncFlowControl asyncFlowControl = default(AsyncFlowControl);
			asyncFlowControl.Setup(flags);
			return asyncFlowControl;
		}

		/// <summary>Restores the flow of the security context across asynchronous threads.</summary>
		/// <exception cref="T:System.InvalidOperationException">The security context is <see langword="null" /> or an empty string.</exception>
		// Token: 0x06001DBA RID: 7610 RVA: 0x00067B34 File Offset: 0x00065D34
		[SecuritySafeCritical]
		public static void RestoreFlow()
		{
			SecurityContext securityContext = Thread.CurrentThread.GetMutableExecutionContext().SecurityContext;
			if (securityContext == null || securityContext._disableFlow == SecurityContextDisableFlow.Nothing)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRestoreUnsupressedFlow"));
			}
			securityContext._disableFlow = SecurityContextDisableFlow.Nothing;
		}

		/// <summary>Determines whether the flow of the security context has been suppressed.</summary>
		/// <returns>
		///   <see langword="true" /> if the flow has been suppressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001DBB RID: 7611 RVA: 0x00067B77 File Offset: 0x00065D77
		public static bool IsFlowSuppressed()
		{
			return SecurityContext.IsFlowSuppressed(SecurityContextDisableFlow.All);
		}

		/// <summary>Determines whether the flow of the Windows identity portion of the current security context has been suppressed.</summary>
		/// <returns>
		///   <see langword="true" /> if the flow has been suppressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001DBC RID: 7612 RVA: 0x00067B83 File Offset: 0x00065D83
		public static bool IsWindowsIdentityFlowSuppressed()
		{
			return SecurityContext._LegacyImpersonationPolicy || SecurityContext.IsFlowSuppressed(SecurityContextDisableFlow.WI);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00067B94 File Offset: 0x00065D94
		[SecuritySafeCritical]
		internal static bool IsFlowSuppressed(SecurityContextDisableFlow flags)
		{
			return Thread.CurrentThread.GetExecutionContextReader().SecurityContext.IsFlowSuppressed(flags);
		}

		/// <summary>Runs the specified method in the specified security context on the current thread.</summary>
		/// <param name="securityContext">The security context to set.</param>
		/// <param name="callback">The delegate that represents the method to run in the specified security context.</param>
		/// <param name="state">The object to pass to the callback method.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="securityContext" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="securityContext" /> was not acquired through a capture operation.  
		/// -or-  
		/// <paramref name="securityContext" /> has already been used as the argument to a <see cref="M:System.Security.SecurityContext.Run(System.Security.SecurityContext,System.Threading.ContextCallback,System.Object)" /> method call.</exception>
		// Token: 0x06001DBE RID: 7614 RVA: 0x00067BBC File Offset: 0x00065DBC
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Run(SecurityContext securityContext, ContextCallback callback, object state)
		{
			if (securityContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullContext"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMe;
			if (!securityContext.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
			}
			securityContext.isNewCapture = false;
			ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
			if (SecurityContext.CurrentlyInDefaultFTSecurityContext(executionContextReader) && securityContext.IsDefaultFTSecurityContext())
			{
				callback(state);
				if (SecurityContext.GetCurrentWI(Thread.CurrentThread.GetExecutionContextReader()) != null)
				{
					WindowsIdentity.SafeRevertToSelf(ref stackCrawlMark);
					return;
				}
			}
			else
			{
				SecurityContext.RunInternal(securityContext, callback, state);
			}
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00067C48 File Offset: 0x00065E48
		[SecurityCritical]
		internal static void RunInternal(SecurityContext securityContext, ContextCallback callBack, object state)
		{
			if (SecurityContext.cleanupCode == null)
			{
				SecurityContext.tryCode = new RuntimeHelpers.TryCode(SecurityContext.runTryCode);
				SecurityContext.cleanupCode = new RuntimeHelpers.CleanupCode(SecurityContext.runFinallyCode);
			}
			SecurityContext.SecurityContextRunData securityContextRunData = new SecurityContext.SecurityContextRunData(securityContext, callBack, state);
			RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(SecurityContext.tryCode, SecurityContext.cleanupCode, securityContextRunData);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00067CA4 File Offset: 0x00065EA4
		[SecurityCritical]
		internal static void runTryCode(object userData)
		{
			SecurityContext.SecurityContextRunData securityContextRunData = (SecurityContext.SecurityContextRunData)userData;
			securityContextRunData.scsw = SecurityContext.SetSecurityContext(securityContextRunData.sc, Thread.CurrentThread.GetExecutionContextReader().SecurityContext, true);
			securityContextRunData.callBack(securityContextRunData.state);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00067CF0 File Offset: 0x00065EF0
		[SecurityCritical]
		[PrePrepareMethod]
		internal static void runFinallyCode(object userData, bool exceptionThrown)
		{
			SecurityContext.SecurityContextRunData securityContextRunData = (SecurityContext.SecurityContextRunData)userData;
			securityContextRunData.scsw.Undo();
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00067D10 File Offset: 0x00065F10
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static SecurityContextSwitcher SetSecurityContext(SecurityContext sc, SecurityContext.Reader prevSecurityContext, bool modifyCurrentExecutionContext)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return SecurityContext.SetSecurityContext(sc, prevSecurityContext, modifyCurrentExecutionContext, ref stackCrawlMark);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00067D2C File Offset: 0x00065F2C
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static SecurityContextSwitcher SetSecurityContext(SecurityContext sc, SecurityContext.Reader prevSecurityContext, bool modifyCurrentExecutionContext, ref StackCrawlMark stackMark)
		{
			SecurityContextDisableFlow disableFlow = sc._disableFlow;
			sc._disableFlow = SecurityContextDisableFlow.Nothing;
			SecurityContextSwitcher securityContextSwitcher = default(SecurityContextSwitcher);
			securityContextSwitcher.currSC = sc;
			securityContextSwitcher.prevSC = prevSecurityContext;
			if (modifyCurrentExecutionContext)
			{
				ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
				securityContextSwitcher.currEC = mutableExecutionContext;
				mutableExecutionContext.SecurityContext = sc;
			}
			if (sc != null)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					securityContextSwitcher.wic = null;
					if (!SecurityContext._LegacyImpersonationPolicy)
					{
						if (sc.WindowsIdentity != null)
						{
							securityContextSwitcher.wic = sc.WindowsIdentity.Impersonate(ref stackMark);
						}
						else if ((disableFlow & SecurityContextDisableFlow.WI) == SecurityContextDisableFlow.Nothing && prevSecurityContext.WindowsIdentity != null)
						{
							securityContextSwitcher.wic = WindowsIdentity.SafeRevertToSelf(ref stackMark);
						}
					}
					securityContextSwitcher.cssw = CompressedStack.SetCompressedStack(sc.CompressedStack, prevSecurityContext.CompressedStack);
				}
				catch
				{
					securityContextSwitcher.UndoNoThrow();
					throw;
				}
			}
			return securityContextSwitcher;
		}

		/// <summary>Creates a copy of the current security context.</summary>
		/// <returns>The security context for the current thread.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current security context has been previously used, was marshaled across application domains, or was not acquired through the <see cref="M:System.Security.SecurityContext.Capture" /> method.</exception>
		// Token: 0x06001DC4 RID: 7620 RVA: 0x00067E08 File Offset: 0x00066008
		[SecuritySafeCritical]
		public SecurityContext CreateCopy()
		{
			if (!this.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
			}
			SecurityContext securityContext = new SecurityContext();
			securityContext.isNewCapture = true;
			securityContext._disableFlow = this._disableFlow;
			if (this.WindowsIdentity != null)
			{
				securityContext._windowsIdentity = new WindowsIdentity(this.WindowsIdentity.AccessToken);
			}
			if (this._compressedStack != null)
			{
				securityContext._compressedStack = this._compressedStack.CreateCopy();
			}
			return securityContext;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00067E90 File Offset: 0x00066090
		[SecuritySafeCritical]
		internal SecurityContext CreateMutableCopy()
		{
			SecurityContext securityContext = new SecurityContext();
			securityContext._disableFlow = this._disableFlow;
			if (this.WindowsIdentity != null)
			{
				securityContext._windowsIdentity = new WindowsIdentity(this.WindowsIdentity.AccessToken);
			}
			if (this._compressedStack != null)
			{
				securityContext._compressedStack = this._compressedStack.CreateCopy();
			}
			return securityContext;
		}

		/// <summary>Captures the security context for the current thread.</summary>
		/// <returns>The security context for the current thread.</returns>
		// Token: 0x06001DC6 RID: 7622 RVA: 0x00067EF4 File Offset: 0x000660F4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static SecurityContext Capture()
		{
			if (SecurityContext.IsFlowSuppressed())
			{
				return null;
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityContext securityContext = SecurityContext.Capture(Thread.CurrentThread.GetExecutionContextReader(), ref stackCrawlMark);
			if (securityContext == null)
			{
				securityContext = SecurityContext.CreateFullTrustSecurityContext();
			}
			return securityContext;
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00067F28 File Offset: 0x00066128
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static SecurityContext Capture(ExecutionContext.Reader currThreadEC, ref StackCrawlMark stackMark)
		{
			if (currThreadEC.SecurityContext.IsFlowSuppressed(SecurityContextDisableFlow.All))
			{
				return null;
			}
			if (SecurityContext.CurrentlyInDefaultFTSecurityContext(currThreadEC))
			{
				return null;
			}
			return SecurityContext.CaptureCore(currThreadEC, ref stackMark);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00067F60 File Offset: 0x00066160
		[SecurityCritical]
		private static SecurityContext CaptureCore(ExecutionContext.Reader currThreadEC, ref StackCrawlMark stackMark)
		{
			SecurityContext securityContext = new SecurityContext();
			securityContext.isNewCapture = true;
			if (!SecurityContext.IsWindowsIdentityFlowSuppressed())
			{
				WindowsIdentity currentWI = SecurityContext.GetCurrentWI(currThreadEC);
				if (currentWI != null)
				{
					securityContext._windowsIdentity = new WindowsIdentity(currentWI.AccessToken);
				}
			}
			else
			{
				securityContext._disableFlow = SecurityContextDisableFlow.WI;
			}
			securityContext.CompressedStack = CompressedStack.GetCompressedStack(ref stackMark);
			return securityContext;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00067FB8 File Offset: 0x000661B8
		[SecurityCritical]
		internal static SecurityContext CreateFullTrustSecurityContext()
		{
			SecurityContext securityContext = new SecurityContext();
			securityContext.isNewCapture = true;
			if (SecurityContext.IsWindowsIdentityFlowSuppressed())
			{
				securityContext._disableFlow = SecurityContextDisableFlow.WI;
			}
			securityContext.CompressedStack = new CompressedStack(null);
			return securityContext;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x00067FF1 File Offset: 0x000661F1
		internal static bool AlwaysFlowImpersonationPolicy
		{
			get
			{
				return SecurityContext._alwaysFlowImpersonationPolicy;
			}
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00067FF8 File Offset: 0x000661F8
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static WindowsIdentity GetCurrentWI(ExecutionContext.Reader threadEC)
		{
			return SecurityContext.GetCurrentWI(threadEC, SecurityContext._alwaysFlowImpersonationPolicy);
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00068008 File Offset: 0x00066208
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static WindowsIdentity GetCurrentWI(ExecutionContext.Reader threadEC, bool cachedAlwaysFlowImpersonationPolicy)
		{
			if (cachedAlwaysFlowImpersonationPolicy)
			{
				return WindowsIdentity.GetCurrentInternal(TokenAccessLevels.MaximumAllowed, true);
			}
			return threadEC.SecurityContext.WindowsIdentity;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00068034 File Offset: 0x00066234
		[SecurityCritical]
		internal static void RestoreCurrentWI(ExecutionContext.Reader currentEC, ExecutionContext.Reader prevEC, WindowsIdentity targetWI, bool cachedAlwaysFlowImpersonationPolicy)
		{
			if (cachedAlwaysFlowImpersonationPolicy || prevEC.SecurityContext.WindowsIdentity != targetWI)
			{
				SecurityContext.RestoreCurrentWIInternal(targetWI);
			}
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x0006805C File Offset: 0x0006625C
		[SecurityCritical]
		private static void RestoreCurrentWIInternal(WindowsIdentity targetWI)
		{
			int num = Win32.RevertToSelf();
			if (num < 0)
			{
				Environment.FailFast(Win32Native.GetMessage(num));
			}
			if (targetWI != null)
			{
				SafeAccessTokenHandle accessToken = targetWI.AccessToken;
				if (accessToken != null && !accessToken.IsInvalid)
				{
					num = Win32.ImpersonateLoggedOnUser(accessToken);
					if (num < 0)
					{
						Environment.FailFast(Win32Native.GetMessage(num));
					}
				}
			}
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000680A9 File Offset: 0x000662A9
		[SecurityCritical]
		internal bool IsDefaultFTSecurityContext()
		{
			return this.WindowsIdentity == null && (this.CompressedStack == null || this.CompressedStack.CompressedStackHandle == null);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x000680CD File Offset: 0x000662CD
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool CurrentlyInDefaultFTSecurityContext(ExecutionContext.Reader threadEC)
		{
			return SecurityContext.IsDefaultThreadSecurityInfo() && SecurityContext.GetCurrentWI(threadEC) == null;
		}

		// Token: 0x06001DD1 RID: 7633
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern WindowsImpersonationFlowMode GetImpersonationFlowMode();

		// Token: 0x06001DD2 RID: 7634
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsDefaultThreadSecurityInfo();

		// Token: 0x04000A62 RID: 2658
		private static bool _LegacyImpersonationPolicy = SecurityContext.GetImpersonationFlowMode() == WindowsImpersonationFlowMode.IMP_NOFLOW;

		// Token: 0x04000A63 RID: 2659
		private static bool _alwaysFlowImpersonationPolicy = SecurityContext.GetImpersonationFlowMode() == WindowsImpersonationFlowMode.IMP_ALWAYSFLOW;

		// Token: 0x04000A64 RID: 2660
		private ExecutionContext _executionContext;

		// Token: 0x04000A65 RID: 2661
		private volatile WindowsIdentity _windowsIdentity;

		// Token: 0x04000A66 RID: 2662
		private volatile CompressedStack _compressedStack;

		// Token: 0x04000A67 RID: 2663
		private static volatile SecurityContext _fullTrustSC;

		// Token: 0x04000A68 RID: 2664
		internal volatile bool isNewCapture;

		// Token: 0x04000A69 RID: 2665
		internal volatile SecurityContextDisableFlow _disableFlow;

		// Token: 0x04000A6A RID: 2666
		internal static volatile RuntimeHelpers.TryCode tryCode;

		// Token: 0x04000A6B RID: 2667
		internal static volatile RuntimeHelpers.CleanupCode cleanupCode;

		// Token: 0x02000B2B RID: 2859
		internal struct Reader
		{
			// Token: 0x06006B78 RID: 27512 RVA: 0x00174E6A File Offset: 0x0017306A
			public Reader(SecurityContext sc)
			{
				this.m_sc = sc;
			}

			// Token: 0x06006B79 RID: 27513 RVA: 0x00174E73 File Offset: 0x00173073
			public SecurityContext DangerousGetRawSecurityContext()
			{
				return this.m_sc;
			}

			// Token: 0x1700121E RID: 4638
			// (get) Token: 0x06006B7A RID: 27514 RVA: 0x00174E7B File Offset: 0x0017307B
			public bool IsNull
			{
				get
				{
					return this.m_sc == null;
				}
			}

			// Token: 0x06006B7B RID: 27515 RVA: 0x00174E86 File Offset: 0x00173086
			public bool IsSame(SecurityContext sc)
			{
				return this.m_sc == sc;
			}

			// Token: 0x06006B7C RID: 27516 RVA: 0x00174E91 File Offset: 0x00173091
			public bool IsSame(SecurityContext.Reader sc)
			{
				return this.m_sc == sc.m_sc;
			}

			// Token: 0x06006B7D RID: 27517 RVA: 0x00174EA1 File Offset: 0x001730A1
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool IsFlowSuppressed(SecurityContextDisableFlow flags)
			{
				return this.m_sc != null && (this.m_sc._disableFlow & flags) == flags;
			}

			// Token: 0x1700121F RID: 4639
			// (get) Token: 0x06006B7E RID: 27518 RVA: 0x00174EBF File Offset: 0x001730BF
			public CompressedStack CompressedStack
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_sc.CompressedStack;
					}
					return null;
				}
			}

			// Token: 0x17001220 RID: 4640
			// (get) Token: 0x06006B7F RID: 27519 RVA: 0x00174ED6 File Offset: 0x001730D6
			public WindowsIdentity WindowsIdentity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					if (!this.IsNull)
					{
						return this.m_sc.WindowsIdentity;
					}
					return null;
				}
			}

			// Token: 0x04003349 RID: 13129
			private SecurityContext m_sc;
		}

		// Token: 0x02000B2C RID: 2860
		internal class SecurityContextRunData
		{
			// Token: 0x06006B80 RID: 27520 RVA: 0x00174EED File Offset: 0x001730ED
			internal SecurityContextRunData(SecurityContext securityContext, ContextCallback cb, object state)
			{
				this.sc = securityContext;
				this.callBack = cb;
				this.state = state;
				this.scsw = default(SecurityContextSwitcher);
			}

			// Token: 0x0400334A RID: 13130
			internal SecurityContext sc;

			// Token: 0x0400334B RID: 13131
			internal ContextCallback callBack;

			// Token: 0x0400334C RID: 13132
			internal object state;

			// Token: 0x0400334D RID: 13133
			internal SecurityContextSwitcher scsw;
		}
	}
}
