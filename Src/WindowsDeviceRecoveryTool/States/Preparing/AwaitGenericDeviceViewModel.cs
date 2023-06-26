using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x0200003D RID: 61
	[Export]
	public class AwaitGenericDeviceViewModel : BaseViewModel, ICanHandle<DeviceConnectedMessage>, ICanHandle
	{
		// Token: 0x0600027F RID: 639 RVA: 0x0000E69E File Offset: 0x0000C89E
		[ImportingConstructor]
		public AwaitGenericDeviceViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		public override string PreviousStateName
		{
			get
			{
				return "AutomaticManufacturerSelectionState";
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
		public string FlashModeText
		{
			get
			{
				return string.Format(LocalizationManager.GetTranslation("Mode"), "Flash");
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000E708 File Offset: 0x0000C908
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

		// Token: 0x06000284 RID: 644 RVA: 0x0000E748 File Offset: 0x0000C948
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("PhoneRestart"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			Tracer<AwaitGenericDeviceViewModel>.WriteInformation("Current phone: ", new object[] { this.AppContext.CurrentPhone });
			PhoneTypes phoneTypes = this.AppContext.SelectedManufacturer;
			bool flag = this.AppContext.CurrentPhone != null;
			if (flag)
			{
				bool flag2 = this.AppContext.CurrentPhone.Type == PhoneTypes.UnknownWp;
				if (flag2)
				{
					this.AppContext.CurrentPhone.Type = this.AppContext.SelectedManufacturer;
				}
				phoneTypes = this.AppContext.CurrentPhone.Type;
			}
			DetectionParameters detectionParams = new DetectionParameters(phoneTypes, PhoneModes.Flash);
			base.Commands.Run((FlowController c) => c.StartDeviceDetection(detectionParams));
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		public override void OnStopped()
		{
			base.Commands.Run((FlowController c) => c.StopDeviceDetection());
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000E904 File Offset: 0x0000CB04
		public void Handle(DeviceConnectedMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				PhoneTypes type = message.Phone.Type;
				PhoneTypes phoneTypes = type;
				switch (phoneTypes)
				{
				case PhoneTypes.Lg:
					break;
				case PhoneTypes.Mcj:
				case PhoneTypes.Blu:
				case PhoneTypes.Alcatel:
				case PhoneTypes.Acer:
				case PhoneTypes.Trinity:
				case PhoneTypes.Unistrong:
				case PhoneTypes.YEZZ:
				case PhoneTypes.Micromax:
				case PhoneTypes.Funker:
				case PhoneTypes.Diginnos:
				case PhoneTypes.VAIO:
				case PhoneTypes.HP:
				case PhoneTypes.Inversenet:
				case PhoneTypes.Freetel:
				case PhoneTypes.XOLO:
				case PhoneTypes.KM:
				case PhoneTypes.Jenesis:
				case PhoneTypes.Gomobile:
				case PhoneTypes.Lenovo:
				case PhoneTypes.Zebra:
				case PhoneTypes.Honeywell:
				case PhoneTypes.Panasonic:
				case PhoneTypes.TrekStor:
				case PhoneTypes.Wileyfox:
				{
					bool flag = this.appContext.CurrentPhone.LocationPath == null || this.appContext.CurrentPhone.LocationPath == message.Phone.LocationPath;
					if (flag)
					{
						this.appContext.CurrentPhone.Path = message.Phone.Path;
						base.Commands.Run((AppController a) => a.SwitchToState("FlashingState"));
					}
					else
					{
						Tracer<AwaitGenericDeviceViewModel>.WriteWarning("Found device but location paths are different!", new object[] { message.Phone });
					}
					goto IL_248;
				}
				case PhoneTypes.HoloLensAccessory:
					goto IL_248;
				default:
					if (phoneTypes != PhoneTypes.Generic)
					{
						goto IL_248;
					}
					break;
				}
				bool flag2 = this.appContext.CurrentPhone != null && this.appContext.CurrentPhone.LocationPath == message.Phone.LocationPath;
				if (flag2)
				{
					message.Phone.SalesName = this.appContext.CurrentPhone.SalesName;
				}
				this.appContext.CurrentPhone = message.Phone;
				base.Commands.Run((AppController a) => a.SwitchToState("ReadingDeviceInfoState"));
				IL_248:;
			}
		}

		// Token: 0x0400012F RID: 303
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;
	}
}
