using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Security;
using System.Threading;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000304 RID: 772
	[SuppressUnmanagedCodeSecurity]
	internal class TeredoHelper
	{
		// Token: 0x06001B52 RID: 6994 RVA: 0x00081BC7 File Offset: 0x0007FDC7
		static TeredoHelper()
		{
			AppDomain.CurrentDomain.DomainUnload += TeredoHelper.OnAppDomainUnload;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00081BE9 File Offset: 0x0007FDE9
		private TeredoHelper(Action<object> callback, object state)
		{
			this.callback = callback;
			this.state = state;
			this.onStabilizedDelegate = new StableUnicastIpAddressTableDelegate(this.OnStabilized);
			this.runCallbackCalled = false;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00081C18 File Offset: 0x0007FE18
		public static bool UnsafeNotifyStableUnicastIpAddressTable(Action<object> callback, object state)
		{
			TeredoHelper teredoHelper = new TeredoHelper(callback, state);
			uint num = 0U;
			SafeFreeMibTable safeFreeMibTable = null;
			List<TeredoHelper> list = TeredoHelper.pendingNotifications;
			lock (list)
			{
				if (TeredoHelper.impendingAppDomainUnload)
				{
					return false;
				}
				num = UnsafeNetInfoNativeMethods.NotifyStableUnicastIpAddressTable(AddressFamily.Unspecified, out safeFreeMibTable, teredoHelper.onStabilizedDelegate, IntPtr.Zero, out teredoHelper.cancelHandle);
				if (safeFreeMibTable != null)
				{
					safeFreeMibTable.Dispose();
				}
				if (num == 997U)
				{
					TeredoHelper.pendingNotifications.Add(teredoHelper);
					return false;
				}
			}
			if (num != 0U)
			{
				throw new Win32Exception((int)num);
			}
			return true;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00081CB4 File Offset: 0x0007FEB4
		private static void OnAppDomainUnload(object sender, EventArgs args)
		{
			List<TeredoHelper> list = TeredoHelper.pendingNotifications;
			lock (list)
			{
				TeredoHelper.impendingAppDomainUnload = true;
				foreach (TeredoHelper teredoHelper in TeredoHelper.pendingNotifications)
				{
					teredoHelper.cancelHandle.Dispose();
				}
			}
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00081D38 File Offset: 0x0007FF38
		private void RunCallback(object o)
		{
			List<TeredoHelper> list = TeredoHelper.pendingNotifications;
			lock (list)
			{
				if (TeredoHelper.impendingAppDomainUnload)
				{
					return;
				}
				TeredoHelper.pendingNotifications.Remove(this);
				this.cancelHandle.Dispose();
			}
			this.callback(this.state);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00081DA4 File Offset: 0x0007FFA4
		private void OnStabilized(IntPtr context, IntPtr table)
		{
			UnsafeNetInfoNativeMethods.FreeMibTable(table);
			if (!this.runCallbackCalled)
			{
				lock (this)
				{
					if (!this.runCallbackCalled)
					{
						this.runCallbackCalled = true;
						ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(this.RunCallback), null);
					}
				}
			}
		}

		// Token: 0x04001AD5 RID: 6869
		private static List<TeredoHelper> pendingNotifications = new List<TeredoHelper>();

		// Token: 0x04001AD6 RID: 6870
		private static bool impendingAppDomainUnload;

		// Token: 0x04001AD7 RID: 6871
		private readonly Action<object> callback;

		// Token: 0x04001AD8 RID: 6872
		private readonly object state;

		// Token: 0x04001AD9 RID: 6873
		private bool runCallbackCalled;

		// Token: 0x04001ADA RID: 6874
		private readonly StableUnicastIpAddressTableDelegate onStabilizedDelegate;

		// Token: 0x04001ADB RID: 6875
		private SafeCancelMibChangeNotify cancelHandle;
	}
}
