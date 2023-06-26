using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Help
{
	// Token: 0x02000062 RID: 98
	[Export]
	public class HtcChooseHelpViewModel : BaseViewModel
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00014450 File Offset: 0x00012650
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00014468 File Offset: 0x00012668
		public string HTCBootloaderModeText
		{
			get
			{
				return this.htcBootloaderModeText;
			}
			set
			{
				base.SetValue<string>(() => this.htcBootloaderModeText, ref this.htcBootloaderModeText, value);
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000144A4 File Offset: 0x000126A4
		public override void OnStarted()
		{
			this.HTCBootloaderModeText = string.Format(LocalizationManager.GetTranslation("HtcBootloaderMode"), "boot-loader");
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Help"), "HTC"));
			base.EventAggregator.Publish<HelpScreenChangedMessage>(new HelpScreenChangedMessage(HelpTabs.HtcChoose));
		}

		// Token: 0x040001A8 RID: 424
		private string htcBootloaderModeText;
	}
}
