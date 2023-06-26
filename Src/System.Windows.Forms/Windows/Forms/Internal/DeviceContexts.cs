using System;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004D4 RID: 1236
	internal static class DeviceContexts
	{
		// Token: 0x06005135 RID: 20789 RVA: 0x00152DD0 File Offset: 0x00150FD0
		internal static void AddDeviceContext(DeviceContext dc)
		{
			if (DeviceContexts.activeDeviceContexts == null)
			{
				DeviceContexts.activeDeviceContexts = new ClientUtils.WeakRefCollection();
				DeviceContexts.activeDeviceContexts.RefCheckThreshold = 20;
			}
			if (!DeviceContexts.activeDeviceContexts.Contains(dc))
			{
				dc.Disposing += DeviceContexts.OnDcDisposing;
				DeviceContexts.activeDeviceContexts.Add(dc);
			}
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x00152E28 File Offset: 0x00151028
		private static void OnDcDisposing(object sender, EventArgs e)
		{
			DeviceContext deviceContext = sender as DeviceContext;
			if (deviceContext != null)
			{
				deviceContext.Disposing -= DeviceContexts.OnDcDisposing;
				DeviceContexts.RemoveDeviceContext(deviceContext);
			}
		}

		// Token: 0x06005137 RID: 20791 RVA: 0x00152E57 File Offset: 0x00151057
		internal static void RemoveDeviceContext(DeviceContext dc)
		{
			if (DeviceContexts.activeDeviceContexts == null)
			{
				return;
			}
			DeviceContexts.activeDeviceContexts.RemoveByHashCode(dc);
		}

		// Token: 0x06005138 RID: 20792 RVA: 0x00152E6C File Offset: 0x0015106C
		internal static bool IsFontInUse(WindowsFont wf)
		{
			if (wf == null)
			{
				return false;
			}
			for (int i = 0; i < DeviceContexts.activeDeviceContexts.Count; i++)
			{
				DeviceContext deviceContext = DeviceContexts.activeDeviceContexts[i] as DeviceContext;
				if (deviceContext != null && (deviceContext.ActiveFont == wf || deviceContext.IsFontOnContextStack(wf)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400350D RID: 13581
		[ThreadStatic]
		private static ClientUtils.WeakRefCollection activeDeviceContexts;
	}
}
