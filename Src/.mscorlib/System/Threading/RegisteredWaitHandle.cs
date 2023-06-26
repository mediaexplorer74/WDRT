using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	/// <summary>Represents a handle that has been registered when calling <see cref="M:System.Threading.ThreadPool.RegisterWaitForSingleObject(System.Threading.WaitHandle,System.Threading.WaitOrTimerCallback,System.Object,System.UInt32,System.Boolean)" />. This class cannot be inherited.</summary>
	// Token: 0x0200051C RID: 1308
	[ComVisible(true)]
	public sealed class RegisteredWaitHandle : MarshalByRefObject
	{
		// Token: 0x06003DEE RID: 15854 RVA: 0x000E8C54 File Offset: 0x000E6E54
		internal RegisteredWaitHandle()
		{
			this.internalRegisteredWait = new RegisteredWaitHandleSafe();
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x000E8C67 File Offset: 0x000E6E67
		internal void SetHandle(IntPtr handle)
		{
			this.internalRegisteredWait.SetHandle(handle);
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x000E8C75 File Offset: 0x000E6E75
		[SecurityCritical]
		internal void SetWaitObject(WaitHandle waitObject)
		{
			this.internalRegisteredWait.SetWaitObject(waitObject);
		}

		/// <summary>Cancels a registered wait operation issued by the <see cref="M:System.Threading.ThreadPool.RegisterWaitForSingleObject(System.Threading.WaitHandle,System.Threading.WaitOrTimerCallback,System.Object,System.UInt32,System.Boolean)" /> method.</summary>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to be signaled.</param>
		/// <returns>
		///   <see langword="true" /> if the function succeeds; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003DF1 RID: 15857 RVA: 0x000E8C83 File Offset: 0x000E6E83
		[SecuritySafeCritical]
		[ComVisible(true)]
		public bool Unregister(WaitHandle waitObject)
		{
			return this.internalRegisteredWait.Unregister(waitObject);
		}

		// Token: 0x04001A1B RID: 6683
		private RegisteredWaitHandleSafe internalRegisteredWait;
	}
}
