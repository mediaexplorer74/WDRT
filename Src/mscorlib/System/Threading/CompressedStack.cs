using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	/// <summary>Provides methods for setting and capturing the compressed stack on the current thread. This class cannot be inherited.</summary>
	// Token: 0x020004EF RID: 1263
	[Serializable]
	public sealed class CompressedStack : ISerializable
	{
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x000E40CE File Offset: 0x000E22CE
		// (set) Token: 0x06003BC6 RID: 15302 RVA: 0x000E40D6 File Offset: 0x000E22D6
		internal bool CanSkipEvaluation
		{
			get
			{
				return this.m_canSkipEvaluation;
			}
			private set
			{
				this.m_canSkipEvaluation = value;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x000E40DF File Offset: 0x000E22DF
		internal PermissionListSet PLS
		{
			get
			{
				return this.m_pls;
			}
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x000E40E9 File Offset: 0x000E22E9
		[SecurityCritical]
		internal CompressedStack(SafeCompressedStackHandle csHandle)
		{
			this.m_csHandle = csHandle;
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x000E40FA File Offset: 0x000E22FA
		[SecurityCritical]
		private CompressedStack(SafeCompressedStackHandle csHandle, PermissionListSet pls)
		{
			this.m_csHandle = csHandle;
			this.m_pls = pls;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate an instance of this execution context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to be populated with serialization information.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure representing the destination context of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06003BCA RID: 15306 RVA: 0x000E4114 File Offset: 0x000E2314
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.CompleteConstruction(null);
			info.AddValue("PLS", this.m_pls);
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x000E413E File Offset: 0x000E233E
		private CompressedStack(SerializationInfo info, StreamingContext context)
		{
			this.m_pls = (PermissionListSet)info.GetValue("PLS", typeof(PermissionListSet));
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x000E4168 File Offset: 0x000E2368
		// (set) Token: 0x06003BCD RID: 15309 RVA: 0x000E4172 File Offset: 0x000E2372
		internal SafeCompressedStackHandle CompressedStackHandle
		{
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.m_csHandle;
			}
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			private set
			{
				this.m_csHandle = value;
			}
		}

		/// <summary>Gets the compressed stack for the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> for the current thread.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  The request for <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> failed.</exception>
		// Token: 0x06003BCE RID: 15310 RVA: 0x000E4180 File Offset: 0x000E2380
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static CompressedStack GetCompressedStack()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return CompressedStack.GetCompressedStack(ref stackCrawlMark);
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x000E4198 File Offset: 0x000E2398
		[SecurityCritical]
		internal static CompressedStack GetCompressedStack(ref StackCrawlMark stackMark)
		{
			CompressedStack compressedStack = null;
			CompressedStack compressedStack2;
			if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				compressedStack2 = new CompressedStack(null);
				compressedStack2.CanSkipEvaluation = true;
			}
			else if (CodeAccessSecurityEngine.AllDomainsHomogeneousWithNoStackModifiers())
			{
				compressedStack2 = new CompressedStack(CompressedStack.GetDelayedCompressedStack(ref stackMark, false));
				compressedStack2.m_pls = PermissionListSet.CreateCompressedState_HG();
			}
			else
			{
				compressedStack2 = new CompressedStack(null);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					compressedStack2.CompressedStackHandle = CompressedStack.GetDelayedCompressedStack(ref stackMark, true);
					if (compressedStack2.CompressedStackHandle != null && CompressedStack.IsImmediateCompletionCandidate(compressedStack2.CompressedStackHandle, out compressedStack))
					{
						try
						{
							compressedStack2.CompleteConstruction(compressedStack);
						}
						finally
						{
							CompressedStack.DestroyDCSList(compressedStack2.CompressedStackHandle);
						}
					}
				}
			}
			return compressedStack2;
		}

		/// <summary>Captures the compressed stack from the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> object.</returns>
		// Token: 0x06003BD0 RID: 15312 RVA: 0x000E4248 File Offset: 0x000E2448
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static CompressedStack Capture()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return CompressedStack.GetCompressedStack(ref stackCrawlMark);
		}

		/// <summary>Runs a method in the specified compressed stack on the current thread.</summary>
		/// <param name="compressedStack">The <see cref="T:System.Threading.CompressedStack" /> to set.</param>
		/// <param name="callback">A <see cref="T:System.Threading.ContextCallback" /> that represents the method to be run in the specified security context.</param>
		/// <param name="state">The object to be passed to the callback method.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="compressedStack" /> is <see langword="null" />.</exception>
		// Token: 0x06003BD1 RID: 15313 RVA: 0x000E4260 File Offset: 0x000E2460
		[SecurityCritical]
		public static void Run(CompressedStack compressedStack, ContextCallback callback, object state)
		{
			if (compressedStack == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NamedParamNull"), "compressedStack");
			}
			if (CompressedStack.cleanupCode == null)
			{
				CompressedStack.tryCode = new RuntimeHelpers.TryCode(CompressedStack.runTryCode);
				CompressedStack.cleanupCode = new RuntimeHelpers.CleanupCode(CompressedStack.runFinallyCode);
			}
			CompressedStack.CompressedStackRunData compressedStackRunData = new CompressedStack.CompressedStackRunData(compressedStack, callback, state);
			RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(CompressedStack.tryCode, CompressedStack.cleanupCode, compressedStackRunData);
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x000E42D4 File Offset: 0x000E24D4
		[SecurityCritical]
		internal static void runTryCode(object userData)
		{
			CompressedStack.CompressedStackRunData compressedStackRunData = (CompressedStack.CompressedStackRunData)userData;
			compressedStackRunData.cssw = CompressedStack.SetCompressedStack(compressedStackRunData.cs, CompressedStack.GetCompressedStackThread());
			compressedStackRunData.callBack(compressedStackRunData.state);
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x000E4310 File Offset: 0x000E2510
		[SecurityCritical]
		[PrePrepareMethod]
		internal static void runFinallyCode(object userData, bool exceptionThrown)
		{
			CompressedStack.CompressedStackRunData compressedStackRunData = (CompressedStack.CompressedStackRunData)userData;
			compressedStackRunData.cssw.Undo();
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x000E4330 File Offset: 0x000E2530
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static CompressedStackSwitcher SetCompressedStack(CompressedStack cs, CompressedStack prevCS)
		{
			CompressedStackSwitcher compressedStackSwitcher = default(CompressedStackSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					CompressedStack.SetCompressedStackThread(cs);
					compressedStackSwitcher.prev_CS = prevCS;
					compressedStackSwitcher.curr_CS = cs;
					compressedStackSwitcher.prev_ADStack = CompressedStack.SetAppDomainStack(cs);
				}
			}
			catch
			{
				compressedStackSwitcher.UndoNoThrow();
				throw;
			}
			return compressedStackSwitcher;
		}

		/// <summary>Creates a copy of the current compressed stack.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> object representing the current compressed stack.</returns>
		// Token: 0x06003BD5 RID: 15317 RVA: 0x000E43A0 File Offset: 0x000E25A0
		[SecuritySafeCritical]
		[ComVisible(false)]
		public CompressedStack CreateCopy()
		{
			return new CompressedStack(this.m_csHandle, this.m_pls);
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x000E43B7 File Offset: 0x000E25B7
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static IntPtr SetAppDomainStack(CompressedStack cs)
		{
			return Thread.CurrentThread.SetAppDomainStack((cs == null) ? null : cs.CompressedStackHandle);
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x000E43CF File Offset: 0x000E25CF
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static void RestoreAppDomainStack(IntPtr appDomainStack)
		{
			Thread.CurrentThread.RestoreAppDomainStack(appDomainStack);
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x000E43DC File Offset: 0x000E25DC
		[SecurityCritical]
		internal static CompressedStack GetCompressedStackThread()
		{
			return Thread.CurrentThread.GetExecutionContextReader().SecurityContext.CompressedStack;
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x000E4404 File Offset: 0x000E2604
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal static void SetCompressedStackThread(CompressedStack cs)
		{
			Thread currentThread = Thread.CurrentThread;
			if (currentThread.GetExecutionContextReader().SecurityContext.CompressedStack != cs)
			{
				ExecutionContext mutableExecutionContext = currentThread.GetMutableExecutionContext();
				if (mutableExecutionContext.SecurityContext != null)
				{
					mutableExecutionContext.SecurityContext.CompressedStack = cs;
					return;
				}
				if (cs != null)
				{
					mutableExecutionContext.SecurityContext = new SecurityContext
					{
						CompressedStack = cs
					};
				}
			}
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x000E4466 File Offset: 0x000E2666
		[SecurityCritical]
		internal bool CheckDemand(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
		{
			this.CompleteConstruction(null);
			if (this.PLS == null)
			{
				return false;
			}
			this.PLS.CheckDemand(demand, permToken, rmh);
			return false;
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x000E4489 File Offset: 0x000E2689
		[SecurityCritical]
		internal bool CheckDemandNoHalt(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
		{
			this.CompleteConstruction(null);
			return this.PLS == null || this.PLS.CheckDemand(demand, permToken, rmh);
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x000E44AA File Offset: 0x000E26AA
		[SecurityCritical]
		internal bool CheckSetDemand(PermissionSet pset, RuntimeMethodHandleInternal rmh)
		{
			this.CompleteConstruction(null);
			return this.PLS != null && this.PLS.CheckSetDemand(pset, rmh);
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x000E44CA File Offset: 0x000E26CA
		[SecurityCritical]
		internal bool CheckSetDemandWithModificationNoHalt(PermissionSet pset, out PermissionSet alteredDemandSet, RuntimeMethodHandleInternal rmh)
		{
			alteredDemandSet = null;
			this.CompleteConstruction(null);
			return this.PLS == null || this.PLS.CheckSetDemandWithModification(pset, out alteredDemandSet, rmh);
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x000E44EE File Offset: 0x000E26EE
		[SecurityCritical]
		internal void DemandFlagsOrGrantSet(int flags, PermissionSet grantSet)
		{
			this.CompleteConstruction(null);
			if (this.PLS == null)
			{
				return;
			}
			this.PLS.DemandFlagsOrGrantSet(flags, grantSet);
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x000E450D File Offset: 0x000E270D
		[SecurityCritical]
		internal void GetZoneAndOrigin(ArrayList zoneList, ArrayList originList, PermissionToken zoneToken, PermissionToken originToken)
		{
			this.CompleteConstruction(null);
			if (this.PLS != null)
			{
				this.PLS.GetZoneAndOrigin(zoneList, originList, zoneToken, originToken);
			}
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x000E4530 File Offset: 0x000E2730
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal void CompleteConstruction(CompressedStack innerCS)
		{
			if (this.PLS != null)
			{
				return;
			}
			PermissionListSet permissionListSet = PermissionListSet.CreateCompressedState(this, innerCS);
			lock (this)
			{
				if (this.PLS == null)
				{
					this.m_pls = permissionListSet;
				}
			}
		}

		// Token: 0x06003BE1 RID: 15329
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern SafeCompressedStackHandle GetDelayedCompressedStack(ref StackCrawlMark stackMark, bool walkStack);

		// Token: 0x06003BE2 RID: 15330
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroyDelayedCompressedStack(IntPtr unmanagedCompressedStack);

		// Token: 0x06003BE3 RID: 15331
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroyDCSList(SafeCompressedStackHandle compressedStack);

		// Token: 0x06003BE4 RID: 15332
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetDCSCount(SafeCompressedStackHandle compressedStack);

		// Token: 0x06003BE5 RID: 15333
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsImmediateCompletionCandidate(SafeCompressedStackHandle compressedStack, out CompressedStack innerCS);

		// Token: 0x06003BE6 RID: 15334
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern DomainCompressedStack GetDomainCompressedStack(SafeCompressedStackHandle compressedStack, int index);

		// Token: 0x06003BE7 RID: 15335
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetHomogeneousPLS(PermissionListSet hgPLS);

		// Token: 0x04001980 RID: 6528
		private volatile PermissionListSet m_pls;

		// Token: 0x04001981 RID: 6529
		[SecurityCritical]
		private volatile SafeCompressedStackHandle m_csHandle;

		// Token: 0x04001982 RID: 6530
		private bool m_canSkipEvaluation;

		// Token: 0x04001983 RID: 6531
		internal static volatile RuntimeHelpers.TryCode tryCode;

		// Token: 0x04001984 RID: 6532
		internal static volatile RuntimeHelpers.CleanupCode cleanupCode;

		// Token: 0x02000BE7 RID: 3047
		internal class CompressedStackRunData
		{
			// Token: 0x06006F6A RID: 28522 RVA: 0x0018119C File Offset: 0x0017F39C
			internal CompressedStackRunData(CompressedStack cs, ContextCallback cb, object state)
			{
				this.cs = cs;
				this.callBack = cb;
				this.state = state;
				this.cssw = default(CompressedStackSwitcher);
			}

			// Token: 0x04003609 RID: 13833
			internal CompressedStack cs;

			// Token: 0x0400360A RID: 13834
			internal ContextCallback callBack;

			// Token: 0x0400360B RID: 13835
			internal object state;

			// Token: 0x0400360C RID: 13836
			internal CompressedStackSwitcher cssw;
		}
	}
}
