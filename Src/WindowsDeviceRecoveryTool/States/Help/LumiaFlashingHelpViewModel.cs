using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Help
{
	// Token: 0x0200006A RID: 106
	[Export]
	public class LumiaFlashingHelpViewModel : BaseViewModel
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000147B8 File Offset: 0x000129B8
		public string GoToMyPhoneHasNotBeenDetectedIfNotDetectedText
		{
			get
			{
				return string.Format(LocalizationManager.GetTranslation("GoToMyPhoneHasNotBeenDetectedIfNotDetected"), LocalizationManager.GetTranslation("ButtonMyPhoneWasNotDetected"));
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00013C3F File Offset: 0x00011E3F
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Help"), LocalizationManager.GetTranslation("FlashMode")));
			base.EventAggregator.Publish<HelpScreenChangedMessage>(new HelpScreenChangedMessage(HelpTabs.LumiaFlashing));
		}
	}
}
