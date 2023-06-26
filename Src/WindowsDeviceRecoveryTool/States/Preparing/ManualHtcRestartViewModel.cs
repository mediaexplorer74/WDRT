using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x0200004D RID: 77
	[Export]
	public class ManualHtcRestartViewModel : BaseViewModel, ICanHandle<DeviceConnectedMessage>, ICanHandle
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00010FA8 File Offset: 0x0000F1A8
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00010FC0 File Offset: 0x0000F1C0
		public string SubHeader
		{
			get
			{
				return this.subHeader;
			}
			set
			{
				base.SetValue<string>(() => this.SubHeader, ref this.subHeader, value);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00011000 File Offset: 0x0000F200
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("RestartDeviceHeader"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			this.SubHeader = string.Format(LocalizationManager.GetTranslation("ManualRestartHtcInfo"), LocalizationManager.GetTranslation("ButtonCancel"));
			DetectionParameters detectionParams = new DetectionParameters(PhoneTypes.Htc, PhoneModes.Normal);
			base.Commands.Run((FlowController c) => c.StartDeviceDetection(detectionParams));
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000110E8 File Offset: 0x0000F2E8
		public override void OnStopped()
		{
			base.OnStopped();
			base.Commands.Run((FlowController c) => c.StopDeviceDetection());
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00011148 File Offset: 0x0000F348
		public void Handle(DeviceConnectedMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				bool flag = message.Phone.Type == PhoneTypes.Htc;
				if (flag)
				{
					base.Commands.Run((AppController c) => c.SwitchToState("AwaitHtcState"));
				}
			}
		}

		// Token: 0x0400015B RID: 347
		private string subHeader;
	}
}
