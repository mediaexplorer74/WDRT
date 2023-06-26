using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Help
{
	// Token: 0x02000068 RID: 104
	[Export]
	public class LumiaEmergencyHelpViewModel : BaseViewModel
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00014700 File Offset: 0x00012900
		public string GoToMyPhoneHasNotBeenDetectedText
		{
			get
			{
				return string.Format(LocalizationManager.GetTranslation("GoToMyPhoneHasNotBeenDetected"), LocalizationManager.GetTranslation("ButtonMyPhoneWasNotDetected"));
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001472B File Offset: 0x0001292B
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Help"), LocalizationManager.GetTranslation("EmergencyMode")));
			base.EventAggregator.Publish<HelpScreenChangedMessage>(new HelpScreenChangedMessage(HelpTabs.LumiaEmergency));
		}
	}
}
