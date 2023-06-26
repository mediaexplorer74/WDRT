using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.BusinessLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Controls;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;
using Microsoft.WindowsDeviceRecoveryTool.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.Controllers
{
	// Token: 0x02000090 RID: 144
	[Export("Microsoft.WindowsDeviceRecoveryTool.Controllers.FlowController", typeof(IController))]
	public class FlowController : BaseController
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x000184BC File Offset: 0x000166BC
		[ImportingConstructor]
		public FlowController(ICommandRepository commandRepository, Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext, LogicContext logics, EventAggregator eventAggregator, ReportingService reportingService)
			: base(commandRepository, eventAggregator)
		{
			this.appContext = appContext;
			this.logics = logics;
			this.reportingService = reportingService;
			this.logics.AdaptationManager.DeviceInfoRead += this.AdaptationManagerDeviceInfoRead;
			this.logics.AdaptationManager.ProgressChanged += this.AdaptationManagerProgressChanged;
			this.logics.AdaptationManager.DeviceBatteryLevelRead += this.AdaptationManagerDeviceBatteryLevelRead;
			this.logics.AdaptationManager.DeviceBatteryStatusRead += this.AdaptationManagerDeviceBatteryStatusRead;
			this.logics.AdaptationManager.DeviceConnectionStatusRead += this.AdaptationManagerDeviceConnectionStatusRead;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001858C File Offset: 0x0001678C
		[CustomCommand]
		public void ChangePackageDirectoryCommand()
		{
			string result = DialogManager.Instance.OpenDirectoryDialog("c:\\");
			bool flag = !string.IsNullOrEmpty(result);
			if (flag)
			{
				base.EventAggregator.Publish<PackageDirectoryMessage>(new PackageDirectoryMessage(result));
				base.Commands.Run((FlowController c) => c.FindCorrectPackage(result, CancellationToken.None));
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00018668 File Offset: 0x00016868
		[CustomCommand]
		public void ChangePackagePathCommand()
		{
			string adaptationExtension = this.logics.AdaptationManager.GetAdaptationExtension(this.appContext.CurrentPhone.Type);
			string text = DialogManager.Instance.OpenFileDialog(adaptationExtension, Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.DefaultFfuPath);
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				base.EventAggregator.Publish<PackageDirectoryMessage>(new PackageDirectoryMessage(text));
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000186CC File Offset: 0x000168CC
		[CustomCommand]
		public void CancelSearchingPackageAndSwitchToManual()
		{
			((IAsyncDelegateCommand)base.Commands["FindCorrectPackage"]).Cancel();
			base.Commands.Run((AppController c) => c.SwitchToState("ManualPackageSelectionState"));
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00018758 File Offset: 0x00016958
		[CustomCommand(IsAsynchronous = true)]
		public void FindCorrectPackage(string directory, CancellationToken token)
		{
			List<PackageFileInfo> list = this.logics.AdaptationManager.FindCorrectPackage(directory, this.appContext.CurrentPhone, token);
			base.EventAggregator.Publish<CompatibleFfuFilesMessage>(new CompatibleFfuFilesMessage(list));
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00018798 File Offset: 0x00016998
		[CustomCommand(IsAsynchronous = true)]
		public void FindAllLumiaPackages(string directory, CancellationToken cancellationToken)
		{
			List<PackageFileInfo> list = this.logics.AdaptationManager.FindAllPackages(directory, PhoneTypes.Lumia, cancellationToken);
			Tracer<FlowController>.WriteInformation("Found packages: {0}", new object[] { list.Count });
			base.EventAggregator.Publish<CompatibleFfuFilesMessage>(new CompatibleFfuFilesMessage(list));
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000187EB File Offset: 0x000169EB
		[CustomCommand]
		public void StartAwaitRecoveryState()
		{
			base.EventAggregator.Publish<DetectionTypeMessage>(new DetectionTypeMessage(DetectionType.RecoveryMode));
			base.EventAggregator.Publish<SwitchStateMessage>(new SwitchStateMessage("AwaitRecoveryDeviceState"));
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00018818 File Offset: 0x00016A18
		[CustomCommand(IsAsynchronous = true)]
		public void CancelAwaitRecoveryAfterEmergency(bool cancelled, CancellationToken token)
		{
			this.reportingService.OperationFailed(new Phone(), ReportOperationType.EmergencyFlashing, UriData.AwaitAfterEmergencyFlashingCanceled, new OperationCanceledException("User canceled waiting for device after succesfull emergency flashing operation"));
			List<string> list = new List<string> { "Error_OperationCanceledException" };
			base.EventAggregator.Publish<FlashResultMessage>(new FlashResultMessage(false, list, null));
			base.Commands.Run((AppController c) => c.SwitchToState("SummaryState"));
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000188D0 File Offset: 0x00016AD0
		[CustomCommand(IsAsynchronous = true)]
		public void FinishAwaitRecoveryAfterEmergency(bool cancelled, CancellationToken token)
		{
			Phone currentPhone = this.appContext.CurrentPhone;
			bool flag = currentPhone.IsDeviceInEmergencyMode();
			if (flag)
			{
				this.reportingService.OperationFailed(currentPhone, ReportOperationType.EmergencyFlashing, UriData.EmergencyModeAfterEmergencyFlashing, new Exception("Emergency mode appeared after succesfull emergency flashing operation"));
				List<string> list = new List<string> { "Error_DeviceNotFoundException" };
				base.EventAggregator.Publish<FlashResultMessage>(new FlashResultMessage(false, list, null));
				base.Commands.Run((AppController c) => c.SwitchToState("SummaryState"));
			}
			else
			{
				try
				{
					this.logics.LumiaAdaptation.StopDetection();
					this.logics.Thor2Service.TryReadMissingInfoWithThor(currentPhone, CancellationToken.None, true);
					this.reportingService.PartialOperationSucceded(currentPhone, ReportOperationType.EmergencyFlashing, UriData.UefiModeAfterEmergencyFlashing);
				}
				catch (Exception ex)
				{
					this.reportingService.OperationFailed(currentPhone, ReportOperationType.ReadInfoAfterEmergencyFlashing, UriData.ReadingDeviceInfoAfterEmergencyFlashingFailed, ex);
				}
				base.EventAggregator.Publish<DetectionTypeMessage>(new DetectionTypeMessage(DetectionType.RecoveryModeAfterEmergencyFlashing));
				base.EventAggregator.Publish<SwitchStateMessage>(new SwitchStateMessage("AwaitRecoveryDeviceState"));
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00018A38 File Offset: 0x00016C38
		[CustomCommand(IsAsynchronous = true)]
		public void EmergencyFlashDevice(object parameter, CancellationToken token)
		{
			bool flag = false;
			List<string> list = new List<string>();
			string text = string.Empty;
			try
			{
				this.logics.AdaptationManager.EmergencyFlashDevice(this.appContext.CurrentPhone, token);
				flag = true;
			}
			catch (NoDeviceException)
			{
				list = new List<string> { "Error_DeviceNotFoundException" };
			}
			catch (FileNotFoundException ex)
			{
				list = new List<string> { "Error_FileNotFoundException" };
				text = ex.Message;
			}
			catch (SoftwareIsNotCorrectlySignedException ex2)
			{
				list = new List<string> { "Error_SoftwareIsNotCorrectlySignedException" };
				text = ex2.Message;
			}
			catch (Exception ex3)
			{
				list = new List<string> { "Error_SoftwareInstallationFailed", "ButtonMyPhoneWasNotDetected" };
				string text2 = "Flashing failed:\n";
				Exception ex4 = ex3;
				Tracer<FlowController>.WriteInformation(text2 + ((ex4 != null) ? ex4.ToString() : null));
			}
			finally
			{
				bool flag2 = flag;
				if (flag2)
				{
					base.Commands.Run((AppController c) => c.SwitchToState("AwaitRecoveryModeAfterEmergencyFlashingState"));
				}
				else
				{
					base.EventAggregator.Publish<FlashResultMessage>(new FlashResultMessage(false, list, text));
					base.Commands.Run((AppController c) => c.SwitchToState("SummaryState"));
				}
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00018C48 File Offset: 0x00016E48
		[CustomCommand(IsAsynchronous = true)]
		public void FlashDevice(DetectionType detectionType, CancellationToken token)
		{
			bool flag = false;
			List<string> list = new List<string>();
			string text = string.Empty;
			bool flag2 = this.appContext.CurrentPhone == null || !this.logics.AdaptationManager.IsDeviceInFlashModeConnected(this.appContext.CurrentPhone, token);
			if (flag2)
			{
				throw new DeviceNotFoundException();
			}
			try
			{
				this.logics.AdaptationManager.FlashDevice(this.appContext.CurrentPhone, detectionType, token);
				flag = true;
			}
			catch (NoDeviceException)
			{
				list = new List<string> { "Error_DeviceNotFoundException" };
			}
			catch (DeviceDisconnectedException)
			{
				list = new List<string> { "Error_DeviceDisconnectedException" };
			}
			catch (FileNotFoundException ex)
			{
				list = new List<string> { "Error_FileNotFoundException" };
				text = ex.Message;
			}
			catch (SoftwareIsNotCorrectlySignedException ex2)
			{
				list = new List<string> { "Error_SoftwareIsNotCorrectlySignedException" };
				text = ex2.Message;
			}
			catch (Exception ex3)
			{
				list = new List<string>
				{
					(this.appContext != null && this.appContext.CurrentPhone != null && this.appContext.CurrentPhone.Type == PhoneTypes.HoloLensAccessory) ? "Error_SoftwareInstallationFailed_ReconnectUSB" : "Error_SoftwareInstallationFailed",
					"ButtonMyPhoneWasNotDetected"
				};
				Tracer<FlowController>.WriteInformation("Flashing failed!");
				Tracer<FlowController>.WriteError(ex3);
			}
			finally
			{
				base.EventAggregator.Publish<FlashResultMessage>(new FlashResultMessage(flag, list, text));
				base.Commands.Run((AppController c) => c.SwitchToState("SummaryState"));
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00018EB8 File Offset: 0x000170B8
		[CustomCommand(IsAsynchronous = true)]
		public void CheckPackageIntegrity(object parameter, CancellationToken token)
		{
			bool flag = this.appContext.CurrentPhone == null;
			if (flag)
			{
				throw new DeviceNotFoundException();
			}
			bool flag2 = false;
			try
			{
				this.logics.AdaptationManager.CheckPackageIntegrity(this.appContext.CurrentPhone, token);
				flag2 = true;
			}
			finally
			{
				base.EventAggregator.Publish<FfuIntegrityCheckMessage>(new FfuIntegrityCheckMessage(flag2));
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00018F2C File Offset: 0x0001712C
		private void AdaptationManagerProgressChanged(ProgressChangedEventArgs progressChangedEventArgs)
		{
			base.EventAggregator.Publish<ProgressMessage>(new ProgressMessage(progressChangedEventArgs.Percentage, progressChangedEventArgs.Message, progressChangedEventArgs.DownloadedSize, progressChangedEventArgs.TotalSize, progressChangedEventArgs.BytesPerSecond, progressChangedEventArgs.SecondsLeft));
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00018F64 File Offset: 0x00017164
		[CustomCommand(IsAsynchronous = true)]
		public void CheckLatestPackage(object parameter, CancellationToken cancellationToken)
		{
			bool flag = false;
			PackageFileInfo packageFileInfo = null;
			bool flag2 = this.appContext.CurrentPhone == null;
			if (flag2)
			{
				throw new DeviceNotFoundException();
			}
			bool flag3 = Settings.Default.CustomPackagesPathEnabled && !Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.CheckDirectoryWritePermission(Settings.Default.PackagesPath);
			if (flag3)
			{
				throw new CannotAccessDirectoryException(Settings.Default.PackagesPath);
			}
			try
			{
				packageFileInfo = this.logics.AdaptationManager.CheckLatestPackage(this.appContext.CurrentPhone, cancellationToken);
				flag = packageFileInfo != null;
				bool flag4 = packageFileInfo == null;
				if (flag4)
				{
					packageFileInfo = new MsrPackageInfo(this.notFoundText, this.notFoundText, this.notFoundText);
				}
			}
			catch (PackageNotFoundException ex)
			{
				packageFileInfo = new MsrPackageInfo(this.notFoundText, this.notFoundText, this.notFoundText);
				flag = false;
				Tracer<FlowController>.WriteError(ex);
			}
			catch (WebException)
			{
				bool flag5 = !NetworkInterface.GetIsNetworkAvailable();
				if (flag5)
				{
					throw new NoInternetConnectionException();
				}
				throw;
			}
			catch (Exception ex2)
			{
				Tracer<FlowController>.WriteError(ex2);
				bool flag6 = ex2 is OperationCanceledException || ex2.InnerException is PackageNotFoundException;
				if (!flag6)
				{
					throw;
				}
				packageFileInfo = new MsrPackageInfo(this.notFoundText, this.notFoundText, this.notFoundText);
			}
			finally
			{
				bool flag7 = this.appContext.CurrentPhone != null;
				if (flag7)
				{
					this.appContext.CurrentPhone.PackageFileInfo = packageFileInfo;
					bool flag8 = packageFileInfo != null && !string.IsNullOrEmpty(packageFileInfo.ManufacturerModelName) && (this.appContext.CurrentPhone.Type == PhoneTypes.Htc || this.appContext.CurrentPhone.Type == PhoneTypes.Lg || this.appContext.CurrentPhone.Type == PhoneTypes.Mcj || this.appContext.CurrentPhone.Type == PhoneTypes.Alcatel);
					if (flag8)
					{
						this.appContext.CurrentPhone.SalesName = packageFileInfo.ManufacturerModelName;
					}
					base.EventAggregator.Publish<FoundSoftwareVersionMessage>(new FoundSoftwareVersionMessage(flag, packageFileInfo));
				}
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001919C File Offset: 0x0001739C
		[CustomCommand(IsAsynchronous = true)]
		public void DownloadEmergencyPackage(object parameter, CancellationToken cancellationToken)
		{
			bool flag = this.appContext.CurrentPhone == null;
			if (flag)
			{
				throw new DeviceNotFoundException();
			}
			bool flag2 = !Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.CheckPermission(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.DefaultPackagesPath);
			if (flag2)
			{
				throw new CannotAccessDirectoryException(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.DefaultPackagesPath);
			}
			try
			{
				this.logics.AdaptationManager.DownloadEmeregencyPackage(this.appContext.CurrentPhone, cancellationToken);
				bool isCancellationRequested = cancellationToken.IsCancellationRequested;
				if (isCancellationRequested)
				{
					base.EventAggregator.Publish<FlashResultMessage>(new FlashResultMessage(false, new List<string> { "DownloadCancelled" }));
					base.Commands.Run((AppController c) => c.SwitchToState("SummaryState"));
					Tracer<FlowController>.WriteInformation("Download package canceled.");
				}
				else
				{
					base.Commands.Run((AppController c) => c.SwitchToState("FlashingState"));
				}
			}
			catch (TaskCanceledException)
			{
				base.EventAggregator.Publish<FlashResultMessage>(new FlashResultMessage(false, new List<string> { "DownloadCancelled" }));
				base.Commands.Run((AppController c) => c.SwitchToState("SummaryState"));
				Tracer<FlowController>.WriteInformation("Download package canceled.");
			}
			catch (WebException ex)
			{
				string text = "Download package failed:\n";
				WebException ex2 = ex;
				Tracer<FlowController>.WriteInformation(text + ((ex2 != null) ? ex2.ToString() : null));
				bool flag3 = !NetworkInterface.GetIsNetworkAvailable();
				if (flag3)
				{
					throw new NoInternetConnectionException();
				}
				throw;
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001940C File Offset: 0x0001760C
		[CustomCommand]
		public void CancelDownloadEmergencyPackage()
		{
			DialogMessageManager dialogMessageManager = new DialogMessageManager();
			bool? flag = dialogMessageManager.ShowQuestionDialog(LocalizationManager.GetTranslation("DownloadingCancelMessage"), null, true);
			bool flag2 = true;
			bool flag3 = (flag.GetValueOrDefault() == flag2) & (flag != null);
			if (flag3)
			{
				((IAsyncDelegateCommand)base.Commands["DownloadEmergencyPackage"]).Cancel();
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00019468 File Offset: 0x00017668
		[CustomCommand(IsAsynchronous = true)]
		public void DownloadPackage(object parameter, CancellationToken token)
		{
			bool flag = this.appContext.CurrentPhone == null;
			if (flag)
			{
				throw new DeviceNotFoundException();
			}
			bool flag2 = !Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.CheckPermission(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.DefaultPackagesPath);
			if (flag2)
			{
				throw new CannotAccessDirectoryException(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.DefaultPackagesPath);
			}
			try
			{
				this.logics.AdaptationManager.DownloadPackage(this.appContext.CurrentPhone, token);
				bool isCancellationRequested = token.IsCancellationRequested;
				if (isCancellationRequested)
				{
					base.EventAggregator.Publish<FlashResultMessage>(new FlashResultMessage(false, new List<string> { "DownloadCancelled" }));
					base.Commands.Run((AppController c) => c.SwitchToState("SummaryState"));
					Tracer<FlowController>.WriteInformation("Download package canceled.");
				}
				else
				{
					string nextState = ((this.appContext.CurrentPhone.Type == PhoneTypes.HoloLensAccessory) ? "FlashingState" : "BatteryCheckingState");
					base.Commands.Run((AppController c) => c.SwitchToState(nextState));
				}
			}
			catch (OperationCanceledException)
			{
				base.EventAggregator.Publish<FlashResultMessage>(new FlashResultMessage(false, new List<string> { "DownloadCancelled" }));
				base.Commands.Run((AppController c) => c.SwitchToState("SummaryState"));
				Tracer<FlowController>.WriteInformation("Download package canceled.");
			}
			catch (WebException ex)
			{
				string text = "Download package failed:\n";
				WebException ex2 = ex;
				Tracer<FlowController>.WriteInformation(text + ((ex2 != null) ? ex2.ToString() : null));
				bool flag3 = !NetworkInterface.GetIsNetworkAvailable();
				if (flag3)
				{
					throw new NoInternetConnectionException();
				}
				throw;
			}
			catch (NotEnoughSpaceException)
			{
				base.EventAggregator.Publish<FlashResultMessage>(new FlashResultMessage(false));
				throw;
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00019744 File Offset: 0x00017944
		[CustomCommand]
		public void CancelDownloadPackage()
		{
			DialogMessageManager dialogMessageManager = new DialogMessageManager();
			bool? flag = dialogMessageManager.ShowQuestionDialog(LocalizationManager.GetTranslation("DownloadingCancelMessage"), null, true);
			bool flag2 = true;
			bool flag3 = (flag.GetValueOrDefault() == flag2) & (flag != null);
			if (flag3)
			{
				((IAsyncDelegateCommand)base.Commands["DownloadPackage"]).Cancel();
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000197A0 File Offset: 0x000179A0
		[CustomCommand]
		public void CompareFirmwareVersions()
		{
			SwVersionComparisonResult swVersionComparisonResult = this.logics.AdaptationManager.CompareFirmwareVersions(this.appContext.CurrentPhone);
			base.EventAggregator.Publish<FirmwareVersionsCompareMessage>(new FirmwareVersionsCompareMessage(swVersionComparisonResult));
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000197DC File Offset: 0x000179DC
		[CustomCommand]
		public void StartDeviceDetection(DetectionParameters detectionParams)
		{
			this.logics.AdaptationManager.DeviceConnected += this.AdaptationManagerDeviceConnected;
			this.logics.AdaptationManager.DeviceDisconnected += this.AdaptationManagerDeviceDisconnected;
			this.logics.AdaptationManager.StartDevicesAutodetection(detectionParams);
			List<Phone> connectedPhones = this.logics.AdaptationManager.GetConnectedPhones(detectionParams);
			foreach (Phone phone in connectedPhones)
			{
				this.AdaptationManagerDeviceConnected(phone);
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00019890 File Offset: 0x00017A90
		[CustomCommand]
		public void StopDeviceDetection()
		{
			this.logics.AdaptationManager.StopDevicesAutodetection();
			this.logics.AdaptationManager.DeviceConnected -= this.AdaptationManagerDeviceConnected;
			this.logics.AdaptationManager.DeviceDisconnected -= this.AdaptationManagerDeviceDisconnected;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000198E9 File Offset: 0x00017AE9
		[CustomCommand]
		public void GetConnectedPhones(DetectionParameters detectionParams)
		{
			base.EventAggregator.Publish<ConnectedPhonesMessage>(new ConnectedPhonesMessage(this.logics.AdaptationManager.GetConnectedPhones(detectionParams)));
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001990E File Offset: 0x00017B0E
		[CustomCommand]
		public void GetSupportedManufacturers()
		{
			base.EventAggregator.Publish<SupportedManufacturersMessage>(new SupportedManufacturersMessage(this.logics.AdaptationManager.GetAdaptationsData()));
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00019932 File Offset: 0x00017B32
		[CustomCommand]
		public void GetSupportedAdaptationModels(PhoneTypes phoneType)
		{
			base.EventAggregator.Publish<SupportedAdaptationModelsMessage>(new SupportedAdaptationModelsMessage(this.logics.AdaptationManager.GetSupportedAdaptationModels(phoneType)));
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00019958 File Offset: 0x00017B58
		[CustomCommand]
		public void Finish(bool isPassed)
		{
			bool flag = !isPassed && this.appContext.SelectedManufacturer == PhoneTypes.Htc;
			if (flag)
			{
				base.Commands.Run((AppController c) => c.SwitchToState("RebootHtcState"));
			}
			else
			{
				base.Commands.Run((AppController c) => c.SwitchToState("AutomaticManufacturerSelectionState"));
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00019A48 File Offset: 0x00017C48
		[CustomCommand]
		public void CancelBatteryChecking()
		{
			base.EventAggregator.Publish<FlashResultMessage>(new FlashResultMessage(false, new List<string> { "BatteryCheckingCancelled" }));
			base.Commands.Run((AppController c) => c.SwitchToState("SummaryState"));
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00019ADB File Offset: 0x00017CDB
		private void AdaptationManagerDeviceDisconnected(Phone phone)
		{
			base.EventAggregator.Publish<DeviceDisconnectedMessage>(new DeviceDisconnectedMessage(phone));
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00019AF0 File Offset: 0x00017CF0
		private void AdaptationManagerDeviceConnected(Phone phone)
		{
			base.EventAggregator.Publish<DeviceConnectedMessage>(new DeviceConnectedMessage(phone));
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00019B05 File Offset: 0x00017D05
		[CustomCommand]
		public void CancelCheckLatestPackage()
		{
			((IAsyncDelegateCommand)base.Commands["CheckLatestPackage"]).Cancel();
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00019B23 File Offset: 0x00017D23
		[CustomCommand(IsAsynchronous = true)]
		public void ReadDeviceInfo(Phone currentPhone, CancellationToken cancellationToken)
		{
			this.logics.AdaptationManager.ReadDeviceInfo(currentPhone, cancellationToken);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00019B39 File Offset: 0x00017D39
		[CustomCommand(IsAsynchronous = true)]
		public void ReadDeviceBatteryLevel(Phone currentPhone, CancellationToken cancellationToken)
		{
			this.logics.AdaptationManager.ReadDeviceBatteryLevel(currentPhone, cancellationToken);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00019B4F File Offset: 0x00017D4F
		[CustomCommand(IsAsynchronous = true)]
		public void ReadDeviceBatteryStatus(Phone phone, CancellationToken cancellationToken)
		{
			this.logics.AdaptationManager.ReadDeviceBatteryStatus(phone, cancellationToken);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00019B65 File Offset: 0x00017D65
		[CustomCommand(IsAsynchronous = true)]
		public void CheckIfDeviceStillConnected(Phone phone, CancellationToken cancellationToken)
		{
			this.logics.AdaptationManager.CheckIfDeviceStillConnected(phone, cancellationToken);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00019B7B File Offset: 0x00017D7B
		[CustomCommand]
		public void CancelReadDeviceInfo()
		{
			((IAsyncDelegateCommand)base.Commands["ReadDeviceInfo"]).Cancel();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00019B99 File Offset: 0x00017D99
		[CustomCommand(IsAsynchronous = true)]
		public void SurveyCompleted(SurveyReport survey, CancellationToken cancellationToken)
		{
			this.reportingService.SurveySucceded(survey);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00019BA9 File Offset: 0x00017DA9
		[CustomCommand(IsAsynchronous = true)]
		public void StartSessionFlow(string sessionParameter, CancellationToken cancellationToken)
		{
			this.reportingService.StartFlowSession();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00019BB8 File Offset: 0x00017DB8
		private void AdaptationManagerDeviceInfoRead(Phone phone)
		{
			bool flag = phone != null;
			if (flag)
			{
				this.appContext.CurrentPhone = phone;
			}
			base.EventAggregator.Publish<DeviceInfoReadMessage>(new DeviceInfoReadMessage(phone != null));
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00019BF4 File Offset: 0x00017DF4
		private void AdaptationManagerDeviceBatteryLevelRead(Phone phone)
		{
			bool flag = phone != null;
			if (flag)
			{
				this.appContext.CurrentPhone = phone;
			}
			base.EventAggregator.Publish<DeviceInfoReadMessage>(new DeviceInfoReadMessage(phone != null));
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00019C2E File Offset: 0x00017E2E
		private void AdaptationManagerDeviceBatteryStatusRead(BatteryStatus batteryStatus)
		{
			base.EventAggregator.Publish<DeviceBatteryStatusReadMessage>(new DeviceBatteryStatusReadMessage(batteryStatus));
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00019C43 File Offset: 0x00017E43
		private void AdaptationManagerDeviceConnectionStatusRead(bool deviceConnectionStatus)
		{
			base.EventAggregator.Publish<DeviceConnectionStatusReadMessage>(new DeviceConnectionStatusReadMessage(deviceConnectionStatus));
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00019C58 File Offset: 0x00017E58
		private bool CheckCustomDirectoryExistenceAndPermissions(string path)
		{
			string text = Path.Combine(path, "Products");
			bool flag = !Directory.Exists(text);
			if (flag)
			{
				try
				{
					Directory.CreateDirectory(text);
				}
				catch
				{
					return false;
				}
			}
			return Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.CheckDirectoryWritePermission(text);
		}

		// Token: 0x0400022C RID: 556
		private readonly string notFoundText = LocalizationManager.GetTranslation("NotFound");

		// Token: 0x0400022D RID: 557
		private readonly Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x0400022E RID: 558
		private readonly LogicContext logics;

		// Token: 0x0400022F RID: 559
		private readonly ReportingService reportingService;
	}
}
