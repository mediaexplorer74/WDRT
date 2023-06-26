using System;
using System.Runtime.InteropServices;

namespace System.Management
{
	// Token: 0x0200004F RID: 79
	internal class WmiGetEventSink : WmiEventSink
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x00010668 File Offset: 0x0000E868
		internal static WmiGetEventSink GetWmiGetEventSink(ManagementOperationObserver watcher, object context, ManagementScope scope, ManagementObject managementObject)
		{
			if (MTAHelper.IsNoContextMTA())
			{
				return new WmiGetEventSink(watcher, context, scope, managementObject);
			}
			WmiGetEventSink.watcherParameter = watcher;
			WmiGetEventSink.contextParameter = context;
			WmiGetEventSink.scopeParameter = scope;
			WmiGetEventSink.managementObjectParameter = managementObject;
			ThreadDispatch threadDispatch = new ThreadDispatch(new ThreadDispatch.ThreadWorkerMethod(WmiGetEventSink.HackToCreateWmiGetEventSink));
			threadDispatch.Start();
			return WmiGetEventSink.wmiGetEventSinkNew;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000106BB File Offset: 0x0000E8BB
		private static void HackToCreateWmiGetEventSink()
		{
			WmiGetEventSink.wmiGetEventSinkNew = new WmiGetEventSink(WmiGetEventSink.watcherParameter, WmiGetEventSink.contextParameter, WmiGetEventSink.scopeParameter, WmiGetEventSink.managementObjectParameter);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000106DB File Offset: 0x0000E8DB
		private WmiGetEventSink(ManagementOperationObserver watcher, object context, ManagementScope scope, ManagementObject managementObject)
			: base(watcher, context, scope, null, null)
		{
			this.managementObject = managementObject;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000106F0 File Offset: 0x0000E8F0
		public override void Indicate(IntPtr pIWbemClassObject)
		{
			Marshal.AddRef(pIWbemClassObject);
			IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded = new IWbemClassObjectFreeThreaded(pIWbemClassObject);
			if (this.managementObject != null)
			{
				try
				{
					this.managementObject.wbemObject = wbemClassObjectFreeThreaded;
				}
				catch
				{
				}
			}
		}

		// Token: 0x040001EE RID: 494
		private ManagementObject managementObject;

		// Token: 0x040001EF RID: 495
		private static ManagementOperationObserver watcherParameter;

		// Token: 0x040001F0 RID: 496
		private static object contextParameter;

		// Token: 0x040001F1 RID: 497
		private static ManagementScope scopeParameter;

		// Token: 0x040001F2 RID: 498
		private static ManagementObject managementObjectParameter;

		// Token: 0x040001F3 RID: 499
		private static WmiGetEventSink wmiGetEventSinkNew;
	}
}
