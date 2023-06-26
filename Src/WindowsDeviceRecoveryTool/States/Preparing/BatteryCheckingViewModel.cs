using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x0200003F RID: 63
	[Export]
	public class BatteryCheckingViewModel : BaseViewModel, ICanHandle<DeviceBatteryStatusReadMessage>, ICanHandle
	{
		// Token: 0x0600028A RID: 650 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		[ImportingConstructor]
		public BatteryCheckingViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.appContext = appContext;
			this.ReadDeviceBatteryStatusCommand = new DelegateCommand<object>(new Action<object>(this.ReadDeviceBatteryStatus));
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000EBD5 File Offset: 0x0000CDD5
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000EBDD File Offset: 0x0000CDDD
		public ICommand ReadDeviceBatteryStatusCommand { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		public override string PreviousStateName
		{
			get
			{
				return "AutomaticManufacturerSelectionState";
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000EC00 File Offset: 0x0000CE00
		public string NextCommand
		{
			get
			{
				switch (this.appContext.CurrentPhone.Type)
				{
				case PhoneTypes.Analog:
					return "AbsoluteConfirmationState";
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
					return "AwaitGenericDeviceState";
				}
				return "FlashingState";
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				base.SetValue<string>(() => this.Description, ref this.description, value);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000ED08 File Offset: 0x0000CF08
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000ED20 File Offset: 0x0000CF20
		public bool BlockFlow
		{
			get
			{
				return this.blockFlow;
			}
			set
			{
				base.SetValue<bool>(() => this.BlockFlow, ref this.blockFlow, value);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000ED60 File Offset: 0x0000CF60
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000ED78 File Offset: 0x0000CF78
		public bool CheckingBatteryStatus
		{
			get
			{
				return this.checkingBatteryStatus;
			}
			set
			{
				base.SetValue<bool>(() => this.CheckingBatteryStatus, ref this.checkingBatteryStatus, value);
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000EDB8 File Offset: 0x0000CFB8
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("BatteryCheckHeader"), ""));
			base.RaisePropertyChanged<string>(() => this.NextCommand);
			this.CheckingBatteryStatus = true;
			this.ReadDeviceBatteryStatus(null);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000EE5C File Offset: 0x0000D05C
		private void CheckBatteries(BatteryStatus status)
		{
			this.SetupPageContent(status);
			bool flag = this.CheckComputerBattery();
			this.BlockFlow = this.BlockFlow || !flag;
			bool flag2 = status == BatteryStatus.BatteryOk && flag;
			if (flag2)
			{
				base.Commands.Run((AppController c) => c.SwitchToState(this.NextCommand));
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000EF10 File Offset: 0x0000D110
		private bool CheckComputerBattery()
		{
			PowerStatus powerStatus = SystemInformation.PowerStatus;
			Tracer<BatteryCheckingViewModel>.WriteInformation("ComputerBattery PowerStatus: " + powerStatus.BatteryChargeStatus.ToString() + ", Percent: " + powerStatus.BatteryLifePercent.ToString());
			bool flag = (double)powerStatus.BatteryLifePercent < 0.25 && powerStatus.PowerLineStatus == PowerLineStatus.Offline;
			bool flag2;
			if (flag)
			{
				base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("BatteryCheckHeader"), ""));
				this.Description = LocalizationManager.GetTranslation("ComputerBatteryWarning");
				flag2 = false;
			}
			else
			{
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000EFBC File Offset: 0x0000D1BC
		private string GetDescription(PhoneTypes phoneType, BatteryStatus deviceBatteryStatus)
		{
			bool flag = deviceBatteryStatus != BatteryStatus.BatteryUnknown;
			if (flag)
			{
				if (phoneType == PhoneTypes.Lumia)
				{
					return LocalizationManager.GetTranslation("LumiaBatteryChecking");
				}
				if (phoneType == PhoneTypes.Analog)
				{
					return LocalizationManager.GetTranslation("AnalogBatteryChecking");
				}
			}
			return LocalizationManager.GetTranslation("BatteryWarning");
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000F010 File Offset: 0x0000D210
		private void SetupPageContent(BatteryStatus deviceBatteryStatus)
		{
			switch (deviceBatteryStatus)
			{
			case BatteryStatus.BatteryNotOkBlock:
				this.CheckingBatteryStatus = false;
				this.Description = this.GetDescription(this.appContext.CurrentPhone.Type, deviceBatteryStatus);
				this.BlockFlow = true;
				break;
			case BatteryStatus.BatteryNotOkDoNotBlock:
			case BatteryStatus.BatteryUnknown:
				this.CheckingBatteryStatus = false;
				this.Description = this.GetDescription(this.appContext.CurrentPhone.Type, deviceBatteryStatus);
				this.BlockFlow = false;
				break;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000F09C File Offset: 0x0000D29C
		private void ReadDeviceBatteryStatus(object obj)
		{
			this.CheckingBatteryStatus = true;
			base.Commands.Run((FlowController c) => c.ReadDeviceBatteryStatus(this.appContext.CurrentPhone, CancellationToken.None));
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000F14C File Offset: 0x0000D34C
		public void Handle(DeviceBatteryStatusReadMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				this.CheckBatteries(message.Status);
			}
		}

		// Token: 0x04000131 RID: 305
		private readonly Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x04000132 RID: 306
		private string description;

		// Token: 0x04000133 RID: 307
		private bool blockFlow;

		// Token: 0x04000134 RID: 308
		private bool checkingBatteryStatus;
	}
}
