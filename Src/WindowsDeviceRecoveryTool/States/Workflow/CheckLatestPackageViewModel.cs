using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Microsoft.WindowsDeviceRecoveryTool.AnalogAdaptation.Services;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;
using Microsoft.WindowsDeviceRecoveryTool.States.BaseStates;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Workflow
{
	// Token: 0x02000013 RID: 19
	[Export]
	public class CheckLatestPackageViewModel : BaseViewModel, ICanHandle<FoundSoftwareVersionMessage>, ICanHandle, ICanHandle<FirmwareVersionsCompareMessage>, ICanHandle<FfuFilePlatformIdMessage>, ICanHandle<PackageDirectoryMessage>, ICanHandle<DeviceDisconnectedMessage>, ICanHandle<DeviceConnectionStatusReadMessage>
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00005CED File Offset: 0x00003EED
		[ImportingConstructor]
		public CheckLatestPackageViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
			this.StartSoftwareInstallCommand = new DelegateCommand<object>(new Action<object>(this.StartSoftwareInstallCommandOnExecuted));
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005D17 File Offset: 0x00003F17
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00005D1F File Offset: 0x00003F1F
		public ICommand StartSoftwareInstallCommand { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00005D28 File Offset: 0x00003F28
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005D6C File Offset: 0x00003F6C
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00005D84 File Offset: 0x00003F84
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005DC4 File Offset: 0x00003FC4
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00005DDC File Offset: 0x00003FDC
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005E1C File Offset: 0x0000401C
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00005E34 File Offset: 0x00004034
		public bool IsBusy
		{
			get
			{
				return this.isBusy;
			}
			set
			{
				base.SetValue<bool>(() => this.IsBusy, ref this.isBusy, value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00005E74 File Offset: 0x00004074
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00005E8C File Offset: 0x0000408C
		public bool IsAkVersionVisible
		{
			get
			{
				return this.isAkVersionVisible;
			}
			set
			{
				base.SetValue<bool>(() => this.IsAkVersionVisible, ref this.isAkVersionVisible, value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005ECC File Offset: 0x000040CC
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00005EE4 File Offset: 0x000040E4
		public bool IsFirmwareVersionVisible
		{
			get
			{
				return this.isFirmwareVersionVisible;
			}
			set
			{
				base.SetValue<bool>(() => this.IsFirmwareVersionVisible, ref this.isFirmwareVersionVisible, value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005F24 File Offset: 0x00004124
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00005F3C File Offset: 0x0000413C
		public bool IsBuildVersionVisible
		{
			get
			{
				return this.isBuildVersionVisible;
			}
			set
			{
				base.SetValue<bool>(() => this.IsBuildVersionVisible, ref this.isBuildVersionVisible, value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005F7C File Offset: 0x0000417C
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00005F94 File Offset: 0x00004194
		public bool IsPlatformIdVisible
		{
			get
			{
				return this.isPlatformIdVisible;
			}
			set
			{
				base.SetValue<bool>(() => this.IsPlatformIdVisible, ref this.isPlatformIdVisible, value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005FD4 File Offset: 0x000041D4
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00005FF4 File Offset: 0x000041F4
		public bool UseSignatureCheck
		{
			get
			{
				return this.flowConditions.UseSignatureCheck;
			}
			set
			{
				this.flowConditions.UseSignatureCheck = value;
				base.RaisePropertyChanged<bool>(() => this.UseSignatureCheck);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00006048 File Offset: 0x00004248
		public bool IsUseSignatureCheckChoiceAvailable
		{
			get
			{
				return this.flowConditions.IsSignatureCheckChoiceAvailable;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00006068 File Offset: 0x00004268
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00006090 File Offset: 0x00004290
		public bool IsManualSelectionEnabled
		{
			get
			{
				return this.flowConditions.IsManualSelectionAvailable && this.isManualSelectionEnabled;
			}
			private set
			{
				base.SetValue<bool>(() => this.IsManualSelectionEnabled, ref this.isManualSelectionEnabled, value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000060D0 File Offset: 0x000042D0
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000060E8 File Offset: 0x000042E8
		public bool IsNextEnabled
		{
			get
			{
				return this.isNextEnabled;
			}
			set
			{
				base.SetValue<bool>(() => this.IsNextEnabled, ref this.isNextEnabled, value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00006128 File Offset: 0x00004328
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00006140 File Offset: 0x00004340
		public bool IsPackageFound
		{
			get
			{
				return this.isPackageFound;
			}
			set
			{
				base.SetValue<bool>(() => this.IsPackageFound, ref this.isPackageFound, value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00006180 File Offset: 0x00004380
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00006198 File Offset: 0x00004398
		public SwVersionComparisonResult SoftwareComparisonStatus
		{
			get
			{
				return this.softwareComparisonStatus;
			}
			set
			{
				base.SetValue<SwVersionComparisonResult>(() => this.SoftwareComparisonStatus, ref this.softwareComparisonStatus, value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000061D8 File Offset: 0x000043D8
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x000061F0 File Offset: 0x000043F0
		public string ContinueButtonText
		{
			get
			{
				return this.continueButtonText;
			}
			set
			{
				base.SetValue<string>(() => this.ContinueButtonText, ref this.continueButtonText, value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00006230 File Offset: 0x00004430
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00006248 File Offset: 0x00004448
		public string SoftwareInfoHeader
		{
			get
			{
				return this.softwareInfoHeader;
			}
			set
			{
				base.SetValue<string>(() => this.SoftwareInfoHeader, ref this.softwareInfoHeader, value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00006288 File Offset: 0x00004488
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000062C8 File Offset: 0x000044C8
		public string FfuFilePath
		{
			get
			{
				bool flag = this.appContext.CurrentPhone != null;
				string text;
				if (flag)
				{
					text = this.appContext.CurrentPhone.PackageFilePath;
				}
				else
				{
					text = string.Empty;
				}
				return text;
			}
			set
			{
				bool flag = this.appContext.CurrentPhone != null;
				if (flag)
				{
					base.SetValue<string>(() => this.FfuFilePath, delegate
					{
						this.appContext.CurrentPhone.PackageFilePath = value;
					});
				}
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006340 File Offset: 0x00004540
		public override void OnStarted()
		{
			bool flag = this.AppContext.CurrentPhone == null;
			if (flag)
			{
				Tracer<CheckLatestPackageViewModel>.WriteInformation("Current phone is empty. Unable to check latest package.");
				throw new DeviceNotFoundException();
			}
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("CheckLatestPackageHeader"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			this.ContinueButtonText = LocalizationManager.GetTranslation("ButtonInstallSoftware");
			this.IsNextEnabled = false;
			this.IsManualSelectionEnabled = false;
			this.IsAkVersionVisible = this.appContext.CurrentPhone != null && this.appContext.CurrentPhone.Type == PhoneTypes.Analog;
			this.IsFirmwareVersionVisible = this.appContext.CurrentPhone != null && this.appContext.CurrentPhone.Type != PhoneTypes.Analog;
			this.IsBuildVersionVisible = false;
			this.IsPackageFound = true;
			this.Description = string.Empty;
			this.IsPlatformIdVisible = false;
			this.SoftwareInfoHeader = LocalizationManager.GetTranslation("SoftwareOnServer");
			DetectionParameters detectionParams = new DetectionParameters(PhoneTypes.All, PhoneModes.Normal);
			base.Commands.Run((FlowController c) => c.StartDeviceDetection(detectionParams));
			bool flag2 = this.AppContext.CurrentPhone.Type == PhoneTypes.Lumia || this.AppContext.CurrentPhone.Type == PhoneTypes.Htc || this.AppContext.CurrentPhone.Type == PhoneTypes.Lg || this.AppContext.CurrentPhone.Type == PhoneTypes.Mcj || this.AppContext.CurrentPhone.Type == PhoneTypes.Blu || this.AppContext.CurrentPhone.Type == PhoneTypes.Alcatel || this.AppContext.CurrentPhone.Type == PhoneTypes.Analog || this.AppContext.CurrentPhone.Type == PhoneTypes.HoloLensAccessory || this.AppContext.CurrentPhone.Type == PhoneTypes.Acer || this.AppContext.CurrentPhone.Type == PhoneTypes.Trinity || this.AppContext.CurrentPhone.Type == PhoneTypes.Unistrong || this.AppContext.CurrentPhone.Type == PhoneTypes.YEZZ || this.AppContext.CurrentPhone.Type == PhoneTypes.Acer || this.AppContext.CurrentPhone.Type == PhoneTypes.VAIO || this.AppContext.CurrentPhone.Type == PhoneTypes.Diginnos || this.AppContext.CurrentPhone.Type == PhoneTypes.VAIO || this.AppContext.CurrentPhone.Type == PhoneTypes.Inversenet || this.AppContext.CurrentPhone.Type == PhoneTypes.Freetel || this.AppContext.CurrentPhone.Type == PhoneTypes.Funker || this.AppContext.CurrentPhone.Type == PhoneTypes.Micromax || this.AppContext.CurrentPhone.Type == PhoneTypes.XOLO || this.AppContext.CurrentPhone.Type == PhoneTypes.KM || this.AppContext.CurrentPhone.Type == PhoneTypes.Jenesis || this.AppContext.CurrentPhone.Type == PhoneTypes.Gomobile || this.AppContext.CurrentPhone.Type == PhoneTypes.HP || this.AppContext.CurrentPhone.Type == PhoneTypes.Lenovo || this.AppContext.CurrentPhone.Type == PhoneTypes.Zebra || this.AppContext.CurrentPhone.Type == PhoneTypes.Honeywell || this.AppContext.CurrentPhone.Type == PhoneTypes.Panasonic || this.AppContext.CurrentPhone.Type == PhoneTypes.TrekStor || this.AppContext.CurrentPhone.Type == PhoneTypes.Wileyfox;
			if (flag2)
			{
				this.IsBusy = true;
				base.Commands.Run((FlowController c) => c.CheckLatestPackage(null, CancellationToken.None));
			}
			else
			{
				bool flag3 = this.AppContext.CurrentPhone.Type == PhoneTypes.Generic;
				if (flag3)
				{
					this.IsManualSelectionEnabled = true;
				}
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000682C File Offset: 0x00004A2C
		public override void OnStopped()
		{
			base.OnStopped();
			base.Commands.Run((FlowController c) => c.StopDeviceDetection());
			base.Commands.Run((FlowController c) => c.CancelCheckLatestPackage());
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000068D8 File Offset: 0x00004AD8
		public void Handle(FoundSoftwareVersionMessage message)
		{
			this.IsBusy = false;
			this.IsManualSelectionEnabled = this.AppContext.CurrentPhone != null && this.AppContext.CurrentPhone.Type == PhoneTypes.Analog;
			bool status = message.Status;
			if (status)
			{
				base.Commands.Run((FlowController c) => c.CompareFirmwareVersions());
				this.IsAkVersionVisible = message.PackageFileInfo != null && !string.IsNullOrWhiteSpace(message.PackageFileInfo.AkVersion) && message.PackageFileInfo.AkVersion != "0000.0000";
			}
			else
			{
				this.Handle(new FirmwareVersionsCompareMessage(SwVersionComparisonResult.PackageNotFound));
				bool flag = this.AppContext.CurrentPhone != null;
				if (flag)
				{
					this.IsAkVersionVisible = this.AppContext.CurrentPhone.Type == PhoneTypes.Analog;
					this.IsManualSelectionEnabled = this.AppContext.CurrentPhone.Type == PhoneTypes.Analog;
				}
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006A04 File Offset: 0x00004C04
		public void Handle(FirmwareVersionsCompareMessage message)
		{
			this.IsPackageFound = true;
			this.SoftwareComparisonStatus = message.Status;
			Tracer<CheckLatestPackageViewModel>.WriteInformation("Software comparison result: {0}", new object[] { message.Status });
			this.ContinueButtonText = LocalizationManager.GetTranslation("ButtonInstallSoftware");
			switch (message.Status)
			{
			case SwVersionComparisonResult.UnableToCompare:
				this.IsNextEnabled = true;
				break;
			case SwVersionComparisonResult.FirstIsGreater:
				this.IsNextEnabled = true;
				this.Description = LocalizationManager.GetTranslation("AvailablePackageIsOlder");
				break;
			case SwVersionComparisonResult.SecondIsGreater:
				this.IsNextEnabled = true;
				this.Description = LocalizationManager.GetTranslation("UpdateAvailable");
				break;
			case SwVersionComparisonResult.NumbersAreEqual:
				this.ContinueButtonText = LocalizationManager.GetTranslation("ReinstallSoftware");
				this.IsNextEnabled = true;
				this.Description = LocalizationManager.GetTranslation("PhoneIsUpToDate");
				break;
			case SwVersionComparisonResult.PackageNotFound:
			{
				this.IsNextEnabled = false;
				this.IsPackageFound = false;
				this.Description = LocalizationManager.GetTranslation("PackageNotFound");
				bool flag = this.appContext.SelectedManufacturer == PhoneTypes.Htc;
				if (flag)
				{
					base.Commands.Run((AppController c) => c.SwitchToState("RebootHtcState"));
				}
				break;
			}
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006B90 File Offset: 0x00004D90
		public void Handle(PackageDirectoryMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				Tracer<CheckLatestPackageViewModel>.LogEntry("Handle");
				Tracer<CheckLatestPackageViewModel>.WriteInformation("Selected package manually: {0}", new object[] { message.Directory });
				this.FfuFilePath = message.Directory;
				bool flag = !string.IsNullOrEmpty(this.FfuFilePath) && this.appContext.CurrentPhone.Type > PhoneTypes.Lumia;
				if (flag)
				{
					base.Commands.Run((FfuController c) => c.ReadFfuFilePlatformId(this.FfuFilePath, CancellationToken.None));
				}
				else
				{
					this.CheckCompatibility(null);
				}
				Tracer<CheckLatestPackageViewModel>.LogExit("Handle");
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006CAC File Offset: 0x00004EAC
		private void CheckCompatibility(FfuFilePlatformIdMessage platformIdMessage)
		{
			Tracer<CheckLatestPackageViewModel>.LogEntry("CheckCompatibility");
			PlatformId platformId = CheckLatestPackageViewModel.FindCompatiblePlatformId(platformIdMessage, this.appContext.CurrentPhone);
			bool flag = this.appContext.CurrentPhone.Type == PhoneTypes.Analog;
			if (flag)
			{
				this.SoftwareInfoHeader = LocalizationManager.GetTranslation("LocalPackage");
				bool flag2 = platformIdMessage != null;
				if (flag2)
				{
					this.AppContext.CurrentPhone.PackageFileInfo = new FfuPackageFileInfo(this.FfuFilePath, platformId ?? platformIdMessage.PlatformId, platformIdMessage.Version, platformIdMessage.Version)
					{
						OfflinePackage = true
					};
				}
				else
				{
					this.AppContext.CurrentPhone.PackageFileInfo = new FfuPackageFileInfo(this.FfuFilePath, null, null);
				}
				this.AppContext.CurrentPhone.PackageFilePath = this.FfuFilePath;
				base.Commands.Run((FlowController c) => c.CompareFirmwareVersions());
				Tracer<CheckLatestPackageViewModel>.WriteInformation("Set local package: {0}", new object[] { this.FfuFilePath });
				SwVersionComparisonResult swVersionComparisonResult = VersionComparer.CompareSoftwareVersions(this.AppContext.CurrentPhone.PackageFileInfo.SoftwareVersion, AnalogAdaptation.OldestRollbackOsVersion, new char[] { '.' });
				bool flag3 = platformId != null && (swVersionComparisonResult == SwVersionComparisonResult.FirstIsGreater || swVersionComparisonResult == SwVersionComparisonResult.NumbersAreEqual);
				if (flag3)
				{
					this.IsNextEnabled = true;
					this.Description = string.Empty;
				}
				else
				{
					this.IsNextEnabled = false;
					this.Description = LocalizationManager.GetTranslation("PackageNotCompatible");
				}
			}
			Tracer<CheckLatestPackageViewModel>.LogExit("CheckCompatibility");
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006E6C File Offset: 0x0000506C
		public void Handle(FfuFilePlatformIdMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				bool flag = this.appContext.CurrentPhone == null;
				if (!flag)
				{
					this.CheckCompatibility(message);
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006EA4 File Offset: 0x000050A4
		private static PlatformId FindCompatiblePlatformId(FfuFilePlatformIdMessage message, Phone phone)
		{
			bool flag = message == null || phone == null || phone.PlatformId == null;
			PlatformId platformId;
			if (flag)
			{
				platformId = null;
			}
			else
			{
				bool flag2 = message.AllPlatformIds == null || !message.AllPlatformIds.Any<PlatformId>();
				if (flag2)
				{
					bool flag3 = message.PlatformId.IsCompatibleWithDevicePlatformId(phone.PlatformId);
					if (flag3)
					{
						platformId = message.PlatformId;
					}
					else
					{
						platformId = null;
					}
				}
				else
				{
					platformId = message.AllPlatformIds.FirstOrDefault((PlatformId id) => id.IsCompatibleWithDevicePlatformId(phone.PlatformId));
				}
			}
			return platformId;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006F48 File Offset: 0x00005148
		private void StartSoftwareInstallCommandOnExecuted(object obj)
		{
			bool flag = this.AppContext.CurrentPhone != null && (this.AppContext.CurrentPhone.Type == PhoneTypes.Analog || this.AppContext.CurrentPhone.Type == PhoneTypes.HoloLensAccessory);
			if (flag)
			{
				string state = ((this.appContext.CurrentPhone.PackageFileInfo != null && this.appContext.CurrentPhone.PackageFileInfo.OfflinePackage) ? "PackageIntegrityCheckState" : "DownloadPackageState");
				base.Commands.Run((AppController c) => c.StartSoftwareInstallStatus(new Tuple<SwVersionComparisonResult, string>(this.SoftwareComparisonStatus, state)));
			}
			else
			{
				SwVersionComparisonResult softwareStatus = (SwVersionComparisonResult)obj;
				base.Commands.Run((AppController c) => c.StartSoftwareInstall(softwareStatus));
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000711C File Offset: 0x0000531C
		private bool NeedToCheckIfDeviceWasDisconnected()
		{
			return this.appContext != null && this.appContext.CurrentPhone != null && this.appContext.CurrentPhone.Type == PhoneTypes.HoloLensAccessory;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000715C File Offset: 0x0000535C
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

		// Token: 0x060000E7 RID: 231 RVA: 0x00007228 File Offset: 0x00005428
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

		// Token: 0x04000080 RID: 128
		[Import]
		private Conditions conditions;

		// Token: 0x04000081 RID: 129
		[Import]
		private FlowConditionService flowConditions;

		// Token: 0x04000082 RID: 130
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x04000083 RID: 131
		private bool isBusy;

		// Token: 0x04000084 RID: 132
		private bool isManualSelectionEnabled;

		// Token: 0x04000085 RID: 133
		private bool isNextEnabled;

		// Token: 0x04000086 RID: 134
		private bool isPackageFound;

		// Token: 0x04000087 RID: 135
		private string continueButtonText;

		// Token: 0x04000088 RID: 136
		private bool isAkVersionVisible;

		// Token: 0x04000089 RID: 137
		private bool isPlatformIdVisible;

		// Token: 0x0400008A RID: 138
		private bool isFirmwareVersionVisible;

		// Token: 0x0400008B RID: 139
		private bool isBuildVersionVisible;

		// Token: 0x0400008C RID: 140
		private string description;

		// Token: 0x0400008D RID: 141
		private string softwareInfoHeader;

		// Token: 0x0400008E RID: 142
		private SwVersionComparisonResult softwareComparisonStatus;
	}
}
