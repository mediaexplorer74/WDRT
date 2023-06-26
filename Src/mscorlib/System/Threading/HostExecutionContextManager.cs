using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	/// <summary>Provides the functionality that allows a common language runtime host to participate in the flow, or migration, of the execution context.</summary>
	// Token: 0x020004FB RID: 1275
	public class HostExecutionContextManager
	{
		// Token: 0x06003C60 RID: 15456
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HostSecurityManagerPresent();

		// Token: 0x06003C61 RID: 15457
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int ReleaseHostSecurityContext(IntPtr context);

		// Token: 0x06003C62 RID: 15458
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int CloneHostSecurityContext(SafeHandle context, SafeHandle clonedContext);

		// Token: 0x06003C63 RID: 15459
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CaptureHostSecurityContext(SafeHandle capturedContext);

		// Token: 0x06003C64 RID: 15460
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SetHostSecurityContext(SafeHandle context, bool fReturnPrevious, SafeHandle prevContext);

		// Token: 0x06003C65 RID: 15461 RVA: 0x000E57D4 File Offset: 0x000E39D4
		[SecurityCritical]
		internal static bool CheckIfHosted()
		{
			if (!HostExecutionContextManager._fIsHostedChecked)
			{
				HostExecutionContextManager._fIsHosted = HostExecutionContextManager.HostSecurityManagerPresent();
				HostExecutionContextManager._fIsHostedChecked = true;
			}
			return HostExecutionContextManager._fIsHosted;
		}

		/// <summary>Captures the host execution context from the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.HostExecutionContext" /> object representing the host execution context of the current thread.</returns>
		// Token: 0x06003C66 RID: 15462 RVA: 0x000E57FC File Offset: 0x000E39FC
		[SecuritySafeCritical]
		public virtual HostExecutionContext Capture()
		{
			HostExecutionContext hostExecutionContext = null;
			if (HostExecutionContextManager.CheckIfHosted())
			{
				IUnknownSafeHandle unknownSafeHandle = new IUnknownSafeHandle();
				hostExecutionContext = new HostExecutionContext(unknownSafeHandle);
				HostExecutionContextManager.CaptureHostSecurityContext(unknownSafeHandle);
			}
			return hostExecutionContext;
		}

		/// <summary>Sets the current host execution context to the specified host execution context.</summary>
		/// <param name="hostExecutionContext">The <see cref="T:System.Threading.HostExecutionContext" /> to be set.</param>
		/// <returns>An object for restoring the <see cref="T:System.Threading.HostExecutionContext" /> to its previous state.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="hostExecutionContext" /> was not acquired through a capture operation.  
		/// -or-  
		/// <paramref name="hostExecutionContext" /> has been the argument to a previous <see cref="M:System.Threading.HostExecutionContextManager.SetHostExecutionContext(System.Threading.HostExecutionContext)" /> method call.</exception>
		// Token: 0x06003C67 RID: 15463 RVA: 0x000E5828 File Offset: 0x000E3A28
		[SecurityCritical]
		public virtual object SetHostExecutionContext(HostExecutionContext hostExecutionContext)
		{
			if (hostExecutionContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
			}
			HostExecutionContextSwitcher hostExecutionContextSwitcher = new HostExecutionContextSwitcher();
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			hostExecutionContextSwitcher.executionContext = mutableExecutionContext;
			hostExecutionContextSwitcher.currentHostContext = hostExecutionContext;
			hostExecutionContextSwitcher.previousHostContext = null;
			if (HostExecutionContextManager.CheckIfHosted() && hostExecutionContext.State is IUnknownSafeHandle)
			{
				IUnknownSafeHandle unknownSafeHandle = new IUnknownSafeHandle();
				hostExecutionContextSwitcher.previousHostContext = new HostExecutionContext(unknownSafeHandle);
				IUnknownSafeHandle unknownSafeHandle2 = (IUnknownSafeHandle)hostExecutionContext.State;
				HostExecutionContextManager.SetHostSecurityContext(unknownSafeHandle2, true, unknownSafeHandle);
			}
			mutableExecutionContext.HostExecutionContext = hostExecutionContext;
			return hostExecutionContextSwitcher;
		}

		/// <summary>Restores the host execution context to its prior state.</summary>
		/// <param name="previousState">The previous context state to revert to.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="previousState" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="previousState" /> was not created on the current thread.  
		/// -or-  
		/// <paramref name="previousState" /> is not the last state for the <see cref="T:System.Threading.HostExecutionContext" />.</exception>
		// Token: 0x06003C68 RID: 15464 RVA: 0x000E58B4 File Offset: 0x000E3AB4
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public virtual void Revert(object previousState)
		{
			HostExecutionContextSwitcher hostExecutionContextSwitcher = previousState as HostExecutionContextSwitcher;
			if (hostExecutionContextSwitcher == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotOverrideSetWithoutRevert"));
			}
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			if (mutableExecutionContext != hostExecutionContextSwitcher.executionContext)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseSwitcherOtherThread"));
			}
			hostExecutionContextSwitcher.executionContext = null;
			HostExecutionContext hostExecutionContext = mutableExecutionContext.HostExecutionContext;
			if (hostExecutionContext != hostExecutionContextSwitcher.currentHostContext)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseSwitcherOtherThread"));
			}
			HostExecutionContext previousHostContext = hostExecutionContextSwitcher.previousHostContext;
			if (HostExecutionContextManager.CheckIfHosted() && previousHostContext != null && previousHostContext.State is IUnknownSafeHandle)
			{
				IUnknownSafeHandle unknownSafeHandle = (IUnknownSafeHandle)previousHostContext.State;
				HostExecutionContextManager.SetHostSecurityContext(unknownSafeHandle, false, null);
			}
			mutableExecutionContext.HostExecutionContext = previousHostContext;
		}

		// Token: 0x06003C69 RID: 15465 RVA: 0x000E5964 File Offset: 0x000E3B64
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HostExecutionContext CaptureHostExecutionContext()
		{
			HostExecutionContext hostExecutionContext = null;
			HostExecutionContextManager currentHostExecutionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
			if (currentHostExecutionContextManager != null)
			{
				hostExecutionContext = currentHostExecutionContextManager.Capture();
			}
			return hostExecutionContext;
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x000E5984 File Offset: 0x000E3B84
		[SecurityCritical]
		internal static object SetHostExecutionContextInternal(HostExecutionContext hostContext)
		{
			HostExecutionContextManager currentHostExecutionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
			object obj = null;
			if (currentHostExecutionContextManager != null)
			{
				obj = currentHostExecutionContextManager.SetHostExecutionContext(hostContext);
			}
			return obj;
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x000E59A8 File Offset: 0x000E3BA8
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HostExecutionContextManager GetCurrentHostExecutionContextManager()
		{
			AppDomainManager currentAppDomainManager = AppDomainManager.CurrentAppDomainManager;
			if (currentAppDomainManager != null)
			{
				return currentAppDomainManager.HostExecutionContextManager;
			}
			return null;
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x000E59C6 File Offset: 0x000E3BC6
		internal static HostExecutionContextManager GetInternalHostExecutionContextManager()
		{
			if (HostExecutionContextManager._hostExecutionContextManager == null)
			{
				HostExecutionContextManager._hostExecutionContextManager = new HostExecutionContextManager();
			}
			return HostExecutionContextManager._hostExecutionContextManager;
		}

		// Token: 0x040019A4 RID: 6564
		private static volatile bool _fIsHostedChecked;

		// Token: 0x040019A5 RID: 6565
		private static volatile bool _fIsHosted;

		// Token: 0x040019A6 RID: 6566
		private static HostExecutionContextManager _hostExecutionContextManager;
	}
}
