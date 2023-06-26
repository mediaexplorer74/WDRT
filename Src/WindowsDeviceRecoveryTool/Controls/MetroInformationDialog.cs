using System;
using System.Windows;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000D8 RID: 216
	public class MetroInformationDialog : MetroDialog
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x0001F5C1 File Offset: 0x0001D7C1
		public MetroInformationDialog()
		{
			base.YesButtonFocused = true;
			this.ButtonNo.Visibility = Visibility.Collapsed;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001F5E0 File Offset: 0x0001D7E0
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0001DFB8 File Offset: 0x0001C1B8
		public string ButtonText
		{
			get
			{
				return base.YesButtonText;
			}
			set
			{
				base.YesButtonText = value;
			}
		}
	}
}
