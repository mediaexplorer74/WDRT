using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;

namespace System.Threading
{
	// Token: 0x0200051B RID: 1307
	internal sealed class RegisteredWaitHandleSafe : CriticalFinalizerObject
	{
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x000E8A68 File Offset: 0x000E6C68
		private static IntPtr InvalidHandle
		{
			[SecuritySafeCritical]
			get
			{
				return Win32Native.INVALID_HANDLE_VALUE;
			}
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x000E8A6F File Offset: 0x000E6C6F
		internal RegisteredWaitHandleSafe()
		{
			this.registeredWaitHandle = RegisteredWaitHandleSafe.InvalidHandle;
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x000E8A82 File Offset: 0x000E6C82
		internal IntPtr GetHandle()
		{
			return this.registeredWaitHandle;
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x000E8A8A File Offset: 0x000E6C8A
		internal void SetHandle(IntPtr handle)
		{
			this.registeredWaitHandle = handle;
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x000E8A94 File Offset: 0x000E6C94
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal void SetWaitObject(WaitHandle waitObject)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this.m_internalWaitObject = waitObject;
				if (waitObject != null)
				{
					this.m_internalWaitObject.SafeWaitHandle.DangerousAddRef(ref this.bReleaseNeeded);
				}
			}
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x000E8ADC File Offset: 0x000E6CDC
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal bool Unregister(WaitHandle waitObject)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				bool flag2 = false;
				do
				{
					if (Interlocked.CompareExchange(ref this.m_lock, 1, 0) == 0)
					{
						flag2 = true;
						try
						{
							if (this.ValidHandle())
							{
								flag = RegisteredWaitHandleSafe.UnregisterWaitNative(this.GetHandle(), (waitObject == null) ? null : waitObject.SafeWaitHandle);
								if (flag)
								{
									if (this.bReleaseNeeded)
									{
										this.m_internalWaitObject.SafeWaitHandle.DangerousRelease();
										this.bReleaseNeeded = false;
									}
									this.SetHandle(RegisteredWaitHandleSafe.InvalidHandle);
									this.m_internalWaitObject = null;
									GC.SuppressFinalize(this);
								}
							}
						}
						finally
						{
							this.m_lock = 0;
						}
					}
					Thread.SpinWait(1);
				}
				while (!flag2);
			}
			return flag;
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x000E8B98 File Offset: 0x000E6D98
		private bool ValidHandle()
		{
			return this.registeredWaitHandle != RegisteredWaitHandleSafe.InvalidHandle && this.registeredWaitHandle != IntPtr.Zero;
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x000E8BC0 File Offset: 0x000E6DC0
		[SecuritySafeCritical]
		~RegisteredWaitHandleSafe()
		{
			if (Interlocked.CompareExchange(ref this.m_lock, 1, 0) == 0)
			{
				try
				{
					if (this.ValidHandle())
					{
						RegisteredWaitHandleSafe.WaitHandleCleanupNative(this.registeredWaitHandle);
						if (this.bReleaseNeeded)
						{
							this.m_internalWaitObject.SafeWaitHandle.DangerousRelease();
							this.bReleaseNeeded = false;
						}
						this.SetHandle(RegisteredWaitHandleSafe.InvalidHandle);
						this.m_internalWaitObject = null;
					}
				}
				finally
				{
					this.m_lock = 0;
				}
			}
		}

		// Token: 0x06003DEC RID: 15852
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitHandleCleanupNative(IntPtr handle);

		// Token: 0x06003DED RID: 15853
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UnregisterWaitNative(IntPtr handle, SafeHandle waitObject);

		// Token: 0x04001A17 RID: 6679
		private IntPtr registeredWaitHandle;

		// Token: 0x04001A18 RID: 6680
		private WaitHandle m_internalWaitObject;

		// Token: 0x04001A19 RID: 6681
		private bool bReleaseNeeded;

		// Token: 0x04001A1A RID: 6682
		private volatile int m_lock;
	}
}
