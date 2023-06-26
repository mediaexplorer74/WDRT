using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Help
{
	// Token: 0x02000060 RID: 96
	[Export]
	public class HtcBootloaderHelpViewModel : BaseViewModel
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00014348 File Offset: 0x00012548
		public string GoToMyPhoneHasNotBeenDetectedText
		{
			get
			{
				return string.Format(LocalizationManager.GetTranslation("GoToMyPhoneHasNotBeenDetected"), LocalizationManager.GetTranslation("ButtonMyPhoneWasNotDetected"));
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00014374 File Offset: 0x00012574
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Help"), string.Format(LocalizationManager.GetTranslation("HtcBootloaderMode"), "boot-loader")));
			base.EventAggregator.Publish<HelpScreenChangedMessage>(new HelpScreenChangedMessage(HelpTabs.HtcBootloader));
		}
	}
}
