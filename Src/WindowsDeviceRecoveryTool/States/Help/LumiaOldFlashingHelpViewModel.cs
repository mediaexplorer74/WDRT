using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Help
{
	// Token: 0x0200005B RID: 91
	[Export]
	public class LumiaOldFlashingHelpViewModel : BaseViewModel
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00013C14 File Offset: 0x00011E14
		public string GoToMyPhoneHasNotBeenDetectedIfNotDetectedText
		{
			get
			{
				return string.Format(LocalizationManager.GetTranslation("GoToMyPhoneHasNotBeenDetectedIfNotDetected"), LocalizationManager.GetTranslation("ButtonMyPhoneWasNotDetected"));
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00013C3F File Offset: 0x00011E3F
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Help"), LocalizationManager.GetTranslation("FlashMode")));
			base.EventAggregator.Publish<HelpScreenChangedMessage>(new HelpScreenChangedMessage(HelpTabs.LumiaFlashing));
		}
	}
}
