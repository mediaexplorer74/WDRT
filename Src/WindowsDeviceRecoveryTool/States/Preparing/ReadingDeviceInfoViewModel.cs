using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Timers;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;
using Microsoft.WindowsDeviceRecoveryTool.States.BaseStates;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x02000051 RID: 81
	[Export]
	public class ReadingDeviceInfoViewModel : BaseViewModel, ICanHandle<DeviceInfoReadMessage>, ICanHandle, ICanHandle<DetectionTypeMessage>, ICanHandle<SelectedDeviceMessage>, ICanHandle<DeviceConnectionStatusReadMessage>, ICanHandle<DeviceDisconnectedMessage>
	{
		// Token: 0x0600031E RID: 798 RVA: 0x000116A8 File Offset: 0x0000F8A8
		[ImportingConstructor]
		public ReadingDeviceInfoViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000116BC File Offset: 0x0000F8BC
		// (set) Token: 0x06000320 RID: 800 RVA: 0x000116D4 File Offset: 0x0000F8D4
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00011714 File Offset: 0x0000F914
		public override string PreviousStateName
		{
			get
			{
				bool flag = this.conditions.IsHtcConnected();
				string text;
				if (flag)
				{
					base.EventAggregator.Publish<FlashResultMessage>(new FlashResultMessage(false));
					text = "RebootHtcState";
				}
				else
				{
					text = "AutomaticManufacturerSelectionState";
				}
				return text;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00011758 File Offset: 0x0000F958
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00011770 File Offset: 0x0000F970
		public Phone SelectedPhone
		{
			get
			{
				return this.selectedPhone;
			}
			set
			{
				base.SetValue<Phone>(() => this.SelectedPhone, ref this.selectedPhone, value);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000324 RID: 804 RVA: 0x000117B0 File Offset: 0x0000F9B0
		// (set) Token: 0x06000325 RID: 805 RVA: 0x000117B8 File Offset: 0x0000F9B8
		private DetectionType DetectionType { get; set; }

		// Token: 0x06000326 RID: 806 RVA: 0x000117C4 File Offset: 0x0000F9C4
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("ReadingDeviceInfo"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			bool flag = (this.DetectionType != DetectionType.RecoveryMode || (this.AppContext.SelectedManufacturer != PhoneTypes.Htc && this.AppContext.SelectedManufacturer != PhoneTypes.Lg && this.AppContext.SelectedManufacturer != PhoneTypes.Mcj && this.AppContext.SelectedManufacturer != PhoneTypes.Alcatel && this.AppContext.SelectedManufacturer != PhoneTypes.Generic && this.AppContext.SelectedManufacturer != PhoneTypes.Analog && this.AppContext.SelectedManufacturer != PhoneTypes.HoloLensAccessory)) && this.SelectedPhone == null;
			if (flag)
			{
				throw new Exception("No phone from DeviceSelection state.");
			}
			PhoneTypes selectedManufacturer = this.AppContext.SelectedManufacturer;
			PhoneTypes phoneTypes = selectedManufacturer;
			if (phoneTypes != PhoneTypes.Lumia)
			{
				if (phoneTypes - PhoneTypes.Htc > 6 && phoneTypes != PhoneTypes.Generic)
				{
					throw new NotImplementedException();
				}
				this.readingDeviceInfoTimer = new System.Timers.Timer(60000.0);
				this.readingDeviceInfoTimer.Elapsed += this.ReadingDeviceInfoTimerElapsed;
				this.readingDeviceInfoTimer.Start();
				base.Commands.Run((FlowController c) => c.ReadDeviceInfo(this.AppContext.CurrentPhone, CancellationToken.None));
			}
			else
			{
				base.Commands.Run((LumiaController c) => c.SetSelectedPhone(this.SelectedPhone, CancellationToken.None));
				base.Commands.Run((LumiaController c) => c.StartCurrentLumiaDetection(this.DetectionType, CancellationToken.None));
			}
			DetectionParameters detectionParams = new DetectionParameters(PhoneTypes.All, PhoneModes.Normal);
			base.Commands.Run((FlowController c) => c.StartDeviceDetection(detectionParams));
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00011B58 File Offset: 0x0000FD58
		public override void OnStopped()
		{
			PhoneTypes selectedManufacturer = this.AppContext.SelectedManufacturer;
			PhoneTypes phoneTypes = selectedManufacturer;
			if (phoneTypes != PhoneTypes.Lumia)
			{
				if (phoneTypes - PhoneTypes.Htc <= 6 || phoneTypes == PhoneTypes.Generic)
				{
					this.readingDeviceInfoTimer.Stop();
					base.Commands.Run((FlowController c) => c.CancelReadDeviceInfo());
				}
			}
			else
			{
				base.Commands.Run((LumiaController c) => c.StopCurrentLumiaDetection());
			}
			base.Commands.Run((FlowController c) => c.StopDeviceDetection());
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00011C7C File Offset: 0x0000FE7C
		public void Handle(DeviceInfoReadMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				bool flag = this.AppContext.SelectedManufacturer == PhoneTypes.Generic && !this.AppContext.CurrentPhone.PlatformId.ToString().StartsWith("MCJ.QC8916.M54TJP", StringComparison.InvariantCultureIgnoreCase) && !this.AppContext.CurrentPhone.PlatformId.ToString().StartsWith("BLU.QC8612.MTP", StringComparison.InvariantCultureIgnoreCase) && !this.AppContext.CurrentPhone.PlatformId.ToString().StartsWith("BLU.QC8916.QL850", StringComparison.InvariantCultureIgnoreCase);
				if (flag)
				{
					Tracer<ReadingDeviceInfoViewModel>.WriteError("Uknown PlatformID: {0}", new object[] { this.AppContext.CurrentPhone.PlatformId });
					base.Commands.Run((AppController c) => c.SwitchToState("UnsupportedDeviceState"));
				}
				else
				{
					bool flag2 = (this.AppContext.SelectedManufacturer == PhoneTypes.Analog || this.AppContext.SelectedManufacturer == PhoneTypes.Htc || this.AppContext.SelectedManufacturer == PhoneTypes.Lg || this.AppContext.SelectedManufacturer == PhoneTypes.Mcj || this.AppContext.SelectedManufacturer == PhoneTypes.Blu || this.AppContext.SelectedManufacturer == PhoneTypes.Alcatel || this.AppContext.SelectedManufacturer == PhoneTypes.HoloLensAccessory) && message.Result;
					if (flag2)
					{
						base.Commands.Run((AppController c) => c.SwitchToState("CheckLatestPackageState"));
					}
				}
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00011E7C File Offset: 0x0001007C
		private void ReadingDeviceInfoTimerElapsed(object sender, ElapsedEventArgs e)
		{
			bool flag = this.AppContext.CurrentPhone.Type == PhoneTypes.Htc;
			if (flag)
			{
				base.Commands.Run((AppController c) => c.SwitchToState("ManualHtcRestartState"));
			}
			else
			{
				bool flag2 = this.AppContext.CurrentPhone.Type == PhoneTypes.Analog;
				if (flag2)
				{
					base.EventAggregator.Publish<ErrorMessage>(new ErrorMessage(new ReadPhoneInformationException("Please check if device was connected in NORMAL mode")));
					base.Commands.Run((AppController c) => c.SwitchToState("ErrorState"));
				}
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00011FA0 File Offset: 0x000101A0
		public void Handle(DetectionTypeMessage message)
		{
			this.DetectionType = message.DetectionType;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00011FB0 File Offset: 0x000101B0
		public void Handle(SelectedDeviceMessage message)
		{
			this.SelectedPhone = message.SelectedPhone;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00011FC0 File Offset: 0x000101C0
		private bool NeedToCheckIfDeviceWasDisconnected()
		{
			return this.appContext != null && this.appContext.CurrentPhone != null && this.appContext.CurrentPhone.Type == PhoneTypes.HoloLensAccessory;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00012000 File Offset: 0x00010200
		public void Handle(DeviceDisconnectedMessage message)
		{
			bool flag = !base.IsStarted;
			if (!flag)
			{
				bool flag2 = this.NeedToCheckIfDeviceWasDisconnected();
				if (flag2)
				{
					base.Commands.Run((FlowController fc) => fc.CheckIfDeviceStillConnected(this.appContext.CurrentPhone, CancellationToken.None));
				}
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000120CC File Offset: 0x000102CC
		public void Handle(DeviceConnectionStatusReadMessage message)
		{
			bool flag = !base.IsStarted;
			if (!flag)
			{
				bool flag2 = !message.Status;
				if (flag2)
				{
					throw new DeviceDisconnectedException();
				}
			}
		}

		// Token: 0x04000162 RID: 354
		private const string McjPlatformId = "MCJ.QC8916.M54TJP";

		// Token: 0x04000163 RID: 355
		private const string BluW510UPlatformId = "BLU.QC8612.MTP";

		// Token: 0x04000164 RID: 356
		private const string BluLtePlatformId = "BLU.QC8916.QL850";

		// Token: 0x04000165 RID: 357
		private System.Timers.Timer readingDeviceInfoTimer;

		// Token: 0x04000166 RID: 358
		[Import]
		private Conditions conditions;

		// Token: 0x04000167 RID: 359
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x04000168 RID: 360
		private Phone selectedPhone;
	}
}
