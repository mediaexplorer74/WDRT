using System;

namespace System.Windows.Forms
{
	// Token: 0x0200034F RID: 847
	internal sealed class RTLAwareMessageBox
	{
		// Token: 0x060036AC RID: 13996 RVA: 0x000F74EB File Offset: 0x000F56EB
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			if (RTLAwareMessageBox.IsRTLResources)
			{
				options |= MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading;
			}
			return MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options);
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x060036AD RID: 13997 RVA: 0x000F750D File Offset: 0x000F570D
		public static bool IsRTLResources
		{
			get
			{
				return SR.GetString("RTL") != "RTL_False";
			}
		}
	}
}
