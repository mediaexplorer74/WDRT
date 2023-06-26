using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x0200003C RID: 60
	[Export]
	public class AwaitHtcViewModel : BaseViewModel, ICanHandle<DeviceConnectedMessage>, ICanHandle
	{
		// Token: 0x06000277 RID: 631 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
		[ImportingConstructor]
		public AwaitHtcViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		public override string PreviousStateName
		{
			get
			{
				return "AutomaticManufacturerSelectionState";
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		public string HtcBootLoaderModeText
		{
			get
			{
				return string.Format(LocalizationManager.GetTranslation("HtcBootloaderMode"), "boot-loader");
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000E41C File Offset: 0x0000C61C
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000E434 File Offset: 0x0000C634
		public Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext AppContext
		{
			get
			{
				return this.appContext;
			}
			set
			{
				base.SetValue<Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext>(() => this.AppContext, ref this.appContext, value);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000E474 File Offset: 0x0000C674
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("PhoneRestart"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			DetectionParameters detectionParams = new DetectionParameters(PhoneTypes.Htc, PhoneModes.Flash);
			base.Commands.Run((FlowController c) => c.StartDeviceDetection(detectionParams));
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000E548 File Offset: 0x0000C748
		public override void OnStopped()
		{
			base.Commands.Run((FlowController c) => c.StopDeviceDetection());
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		public void Handle(DeviceConnectedMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				bool flag = message.Phone.Type == PhoneTypes.Htc;
				if (flag)
				{
					bool flag2 = this.appContext.CurrentPhone != null && this.appContext.CurrentPhone.LocationPath == message.Phone.LocationPath;
					if (flag2)
					{
						message.Phone.SalesName = this.appContext.CurrentPhone.SalesName;
					}
					this.appContext.CurrentPhone = message.Phone;
					base.Commands.Run((AppController a) => a.SwitchToState("ReadingDeviceInfoState"));
				}
			}
		}

		// Token: 0x0400012E RID: 302
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;
	}
}
