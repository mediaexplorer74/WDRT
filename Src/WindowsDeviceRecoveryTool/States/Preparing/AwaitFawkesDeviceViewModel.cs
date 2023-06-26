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
	// Token: 0x02000039 RID: 57
	[Export]
	public class AwaitFawkesDeviceViewModel : BaseViewModel, ICanHandle<DeviceConnectedMessage>, ICanHandle
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0000E0AD File Offset: 0x0000C2AD
		[ImportingConstructor]
		public AwaitFawkesDeviceViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000E0C0 File Offset: 0x0000C2C0
		public override string PreviousStateName
		{
			get
			{
				return "AutomaticManufacturerSelectionState";
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
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

		// Token: 0x0600026D RID: 621 RVA: 0x0000E130 File Offset: 0x0000C330
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("StartRecoveryManually"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			DetectionParameters detectionParams = new DetectionParameters(PhoneTypes.HoloLensAccessory, PhoneModes.Normal);
			base.Commands.Run((FlowController c) => c.StartDeviceDetection(detectionParams));
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000E204 File Offset: 0x0000C404
		public override void OnStopped()
		{
			base.Commands.Run((FlowController c) => c.StopDeviceDetection());
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000E260 File Offset: 0x0000C460
		public void Handle(DeviceConnectedMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				bool flag = message.Phone.Type == PhoneTypes.HoloLensAccessory;
				if (flag)
				{
					this.appContext.CurrentPhone = message.Phone;
					base.Commands.Run((AppController a) => a.SwitchToState("ReadingDeviceInfoState"));
				}
			}
		}

		// Token: 0x0400012B RID: 299
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;
	}
}
