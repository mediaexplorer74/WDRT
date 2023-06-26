using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services;
using Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Connectivity;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;

namespace Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Services
{
	// Token: 0x02000005 RID: 5
	[Export]
	public class LumiaAdaptation : BaseAdaptation
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00003140 File Offset: 0x00001340
		[ImportingConstructor]
		public LumiaAdaptation(DataPackageService dataPackageService, Thor2Service thor2Service, MsrService msrService, ReportingService reportingService)
		{
			this.msrService = msrService;
			this.salesNameProvider = new SalesNameProvider();
			this.dataPackageService = dataPackageService;
			this.thor2Service = thor2Service;
			this.reportingService = reportingService;
			this.dataPackageService.DownloadProgressChanged += this.DataPackageServiceDownloadProgressEvent;
			this.msrService.ProgressChanged += this.MsrDownloadProgressEvent;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000021 RID: 33 RVA: 0x000031B0 File Offset: 0x000013B0
		// (remove) Token: 0x06000022 RID: 34 RVA: 0x000031E8 File Offset: 0x000013E8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Phone> DeviceConnected;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000023 RID: 35 RVA: 0x00003220 File Offset: 0x00001420
		// (remove) Token: 0x06000024 RID: 36 RVA: 0x00003258 File Offset: 0x00001458
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Phone> DeviceDisconnected;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000025 RID: 37 RVA: 0x00003290 File Offset: 0x00001490
		// (remove) Token: 0x06000026 RID: 38 RVA: 0x000032C8 File Offset: 0x000014C8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Phone> DeviceReadyChanged;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00003300 File Offset: 0x00001500
		public override string PackageExtension
		{
			get
			{
				return "vpl";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00003318 File Offset: 0x00001518
		public override PhoneTypes PhoneType
		{
			get
			{
				return PhoneTypes.Lumia;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000332C File Offset: 0x0000152C
		public override bool RecoverySupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00003340 File Offset: 0x00001540
		public override string ManufacturerName
		{
			get
			{
				return "Lumia";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00003358 File Offset: 0x00001558
		public override string ReportManufacturerName
		{
			get
			{
				return "Microsoft";
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00003370 File Offset: 0x00001570
		public override string ReportManufacturerProductLine
		{
			get
			{
				return "Lumia";
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003388 File Offset: 0x00001588
		public override ISalesNameProvider SalesNameProvider()
		{
			return this.salesNameProvider;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000033A0 File Offset: 0x000015A0
		public override void ReadDeviceInfo(Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000033A8 File Offset: 0x000015A8
		protected override void FillSupportedDeviceIdentifiers()
		{
			this.FillNormalModeDeviceIdentifiers();
			this.FillRecoveryModeDeviceIdentifiers();
			this.FillEmergencyModeDeviceIdentifiers();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000033C0 File Offset: 0x000015C0
		private void FillEmergencyModeDeviceIdentifiers()
		{
			this.SupportedEmergencyModeIds.Add(new DeviceIdentifier("05C6", "9008"));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000033E0 File Offset: 0x000015E0
		private void FillRecoveryModeDeviceIdentifiers()
		{
			this.SupportedRecoveryModeIds.Add(new DeviceIdentifier("0421", "066E"));
			this.SupportedRecoveryModeIds.Add(new DeviceIdentifier("0421", "0714"));
			this.SupportedRecoveryModeIds.Add(new DeviceIdentifier("045E", "0A02"));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003440 File Offset: 0x00001640
		private void FillNormalModeDeviceIdentifiers()
		{
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("0421", "0661", new int[] { 2, 3 }));
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("0421", "06FC"));
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("045E", "0A00"));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000034B0 File Offset: 0x000016B0
		public override List<PackageFileInfo> FindPackage(string directory, Phone currentPhone, CancellationToken cancellationToken)
		{
			Tracer<LumiaAdaptation>.LogEntry("FindPackage");
			List<PackageFileInfo> list = new List<PackageFileInfo>();
			try
			{
				List<VariantInfo> list2 = this.dataPackageService.FindLocalVariants();
				Tracer<LumiaAdaptation>.WriteInformation("Selecting variants for Product Type: {0}", new object[] { currentPhone.HardwareModel });
				List<VplPackageFileInfo> list3 = new List<VplPackageFileInfo>();
				foreach (VariantInfo variantInfo in list2)
				{
					bool flag = variantInfo.ProductType == currentPhone.HardwareModel && variantInfo.ProductCode == currentPhone.HardwareVariant;
					if (flag)
					{
						VplPackageFileInfo vplPackageFileInfo = new VplPackageFileInfo(variantInfo.Path, variantInfo);
						list3.Add(vplPackageFileInfo);
					}
				}
				list.AddRange(list3);
			}
			catch (Exception ex)
			{
				Tracer<LumiaAdaptation>.WriteError(ex, "Error while searching for packages", new object[0]);
				throw;
			}
			Tracer<LumiaAdaptation>.LogExit("FindPackage");
			return list;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000035C8 File Offset: 0x000017C8
		public override List<PackageFileInfo> FindAllPackages(string directory, CancellationToken cancellationToken)
		{
			Tracer<LumiaAdaptation>.LogEntry("FindAllPackages");
			List<PackageFileInfo> list = new List<PackageFileInfo>();
			try
			{
				List<VariantInfo> list2 = new List<VariantInfo>();
				this.dataPackageService.FindLocalVariants(list2, directory);
				List<VplPackageFileInfo> list3 = new List<VplPackageFileInfo>();
				foreach (VariantInfo variantInfo in list2)
				{
					VplPackageFileInfo vplPackageFileInfo = new VplPackageFileInfo(variantInfo.Path, variantInfo);
					bool flag = File.Exists(vplPackageFileInfo.FfuFilePath);
					if (flag)
					{
						list3.Add(vplPackageFileInfo);
					}
				}
				list.AddRange(list3);
			}
			catch (Exception ex)
			{
				Tracer<LumiaAdaptation>.WriteError(ex, "Error while searching for packages", new object[0]);
				throw;
			}
			Tracer<LumiaAdaptation>.LogExit("FindAllPackages");
			return list;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000036B0 File Offset: 0x000018B0
		public override void CheckPackageIntegrity(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<LumiaAdaptation>.LogEntry("CheckPackageIntegrity");
			base.RaiseProgressPercentageChanged(0, null);
			this.dataPackageService.IntegrityCheckProgressEvent += this.DataPackageServiceIntegrityCheckProgressEvent;
			try
			{
				this.dataPackageService.CheckVariantIntegrity(phone.PackageFilePath, cancellationToken);
			}
			finally
			{
				this.dataPackageService.IntegrityCheckProgressEvent -= this.DataPackageServiceIntegrityCheckProgressEvent;
			}
			Tracer<LumiaAdaptation>.LogExit("CheckPackageIntegrity");
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003738 File Offset: 0x00001938
		private void DataPackageServiceIntegrityCheckProgressEvent(int percentage)
		{
			base.RaiseProgressPercentageChanged(percentage, null);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003744 File Offset: 0x00001944
		public override bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken cancellationToken)
		{
			return this.thor2Service.IsDeviceConnected(phone, cancellationToken);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003764 File Offset: 0x00001964
		public override void DownloadPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<LumiaAdaptation>.LogEntry("DownloadPackage");
			base.RaiseProgressPercentageChanged(0, null);
			try
			{
				this.reportingService.OperationStarted(phone, ReportOperationType.DownloadPackage);
				DownloadParameters downloadParameters = new DownloadParameters
				{
					DiscoveryParameters = this.DeviceQueryParameters(phone),
					DestinationFolder = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetLumiaProductsPath(phone.HardwareModel)
				};
				List<string> list = this.msrService.DownloadLatestPackage(downloadParameters, cancellationToken);
				bool flag = list.Any<string>();
				if (flag)
				{
					string text = list.FirstOrDefault((string file) => file.ToLower().EndsWith(".vpl"));
					phone.PackageFilePath = text;
				}
				Tuple<long, long, bool> downloadPackageInformation = this.msrService.GetDownloadPackageInformation();
				this.reportingService.SetDownloadByteInformation(phone, ReportOperationType.DownloadPackage, downloadPackageInformation.Item1, downloadPackageInformation.Item2, downloadPackageInformation.Item3);
				this.reportingService.OperationSucceded(phone, ReportOperationType.DownloadPackage);
			}
			catch (Exception ex)
			{
				bool flag2 = ex.InnerException is AggregateException && ex.InnerException.InnerException is IOException && (long)ex.InnerException.InnerException.HResult == -2147024784L;
				if (flag2)
				{
					Tracer<LumiaAdaptation>.WriteInformation("--100: For some reason the exception wasn't thrown until here.");
					throw new NotEnoughSpaceException();
				}
				UriData uriDataForException = this.GetUriDataForException(ex);
				Tuple<long, long, bool> downloadPackageInformation2 = this.msrService.GetDownloadPackageInformation();
				this.reportingService.SetDownloadByteInformation(phone, ReportOperationType.DownloadPackage, downloadPackageInformation2.Item1, downloadPackageInformation2.Item2, downloadPackageInformation2.Item3);
				this.reportingService.OperationFailed(phone, ReportOperationType.DownloadPackage, uriDataForException, ex);
				Tracer<LumiaAdaptation>.WriteError(ex);
				throw;
			}
			finally
			{
				Tracer<LumiaAdaptation>.LogExit("DownloadPackage");
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003938 File Offset: 0x00001B38
		private void GetDownloadInformationToReport(Phone phone, ReportOperationType operationType)
		{
			Tuple<long, long, bool> downloadPackageInformation = this.dataPackageService.GetDownloadPackageInformation();
			this.reportingService.SetDownloadByteInformation(phone, operationType, downloadPackageInformation.Item1, downloadPackageInformation.Item2, downloadPackageInformation.Item3);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003974 File Offset: 0x00001B74
		public override void DownloadEmergencyPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<LumiaAdaptation>.LogEntry("DownloadEmergencyPackage");
			base.RaiseProgressPercentageChanged(0, null);
			try
			{
				string text = (string.IsNullOrWhiteSpace(phone.PackageFilePath) ? Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetLumiaProductsPath(phone.HardwareModel) : Path.GetDirectoryName(phone.PackageFilePath));
				this.reportingService.OperationStarted(phone, ReportOperationType.DownloadEmergencyPackage);
				this.dataPackageService.DownloadProgressChanged += this.DataPackageServiceDownloadProgressEvent;
				EmergencyPackageInfo emergencyPackageInfo = this.dataPackageService.DownloadEmergencyPackage(phone.HardwareModel, text, cancellationToken);
				this.GetDownloadInformationToReport(phone, ReportOperationType.DownloadEmergencyPackage);
				this.reportingService.OperationSucceded(phone, ReportOperationType.DownloadEmergencyPackage);
				phone.EmergencyPackageFileInfo = emergencyPackageInfo;
			}
			catch (Exception ex)
			{
				bool flag = ex.InnerException is AggregateException && ex.InnerException.InnerException is IOException && (long)ex.InnerException.InnerException.HResult == -2147024784L;
				if (flag)
				{
					Tracer<LumiaAdaptation>.WriteInformation("--100: For some reason the exception wasn't thrown until here.");
					throw new NotEnoughSpaceException();
				}
				Tracer<LumiaAdaptation>.WriteError(ex);
				bool flag2 = ex.GetBaseException() is WebException && (long)ex.GetBaseException().HResult == -2146233079L;
				if (flag2)
				{
					this.reportingService.OperationFailed(phone, ReportOperationType.DownloadEmergencyPackage, UriData.EmergencyFlashFilesNotFoundOnServer, ex);
					throw new EmergencyPackageNotFoundOnServerException();
				}
				this.GetDownloadInformationToReport(phone, ReportOperationType.DownloadEmergencyPackage);
				UriData uriDataForException = this.GetUriDataForException(ex);
				this.reportingService.OperationFailed(phone, ReportOperationType.DownloadEmergencyPackage, uriDataForException, ex);
				throw;
			}
			finally
			{
				this.dataPackageService.DownloadProgressChanged -= this.DataPackageServiceDownloadProgressEvent;
				Tracer<LumiaAdaptation>.LogExit("DownloadEmergencyPackage");
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003B44 File Offset: 0x00001D44
		private UriData GetUriDataForException(Exception ex)
		{
			bool flag = ex is OperationCanceledException || ex.GetBaseException() is TaskCanceledException;
			UriData uriData;
			if (flag)
			{
				uriData = UriData.DownloadVariantPackageAbortedByUser;
			}
			else
			{
				bool flag2 = ex is Crc32Exception;
				if (flag2)
				{
					uriData = UriData.FailedToDownloadVariantPackageCrc32Failed;
				}
				else
				{
					bool flag3 = ex is NotEnoughSpaceException;
					if (flag3)
					{
						uriData = UriData.DownloadVariantPackageFilesFailedBecauseOfInsufficientDiskSpace;
					}
					else
					{
						bool flag4 = ex is PlannedServiceBreakException;
						if (flag4)
						{
							uriData = UriData.FailedToDownloadVariantPackageFireServiceBreak;
						}
						else
						{
							uriData = UriData.FailedToDownloadVariantPackage;
						}
					}
				}
			}
			return uriData;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003BBE File Offset: 0x00001DBE
		private void DataPackageServiceDownloadProgressEvent(DownloadingProgressChangedEventArgs args)
		{
			base.RaiseProgressPercentageChanged(args.Percentage, args.Message, args.DownloadedSize, args.TotalSize, args.BytesPerSecond, args.SecondsLeft);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003BEC File Offset: 0x00001DEC
		public override void FlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			this.thor2Service.ProgressChanged += this.Thor2ServiceOnProgressChanged;
			try
			{
				this.thor2Service.FlashDevice(phone, cancellationToken);
			}
			finally
			{
				this.thor2Service.ProgressChanged -= this.Thor2ServiceOnProgressChanged;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003C50 File Offset: 0x00001E50
		public void EmergencyFlashDevice(Phone phone, CancellationToken token)
		{
			this.thor2Service.ProgressChanged += this.Thor2ServiceOnProgressChanged;
			try
			{
				this.thor2Service.EmergencyFlashDevice(phone, token);
			}
			finally
			{
				this.thor2Service.ProgressChanged -= this.Thor2ServiceOnProgressChanged;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003CB4 File Offset: 0x00001EB4
		private void Thor2ServiceOnProgressChanged(ProgressChangedEventArgs progressChangedEventArgs)
		{
			base.RaiseProgressPercentageChanged(progressChangedEventArgs);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public void Initialize()
		{
			this.lumiaConnectivity = new LumiaConnectivity();
			this.lumiaConnectivity.DeviceConnected += this.LumiaConnectivityDeviceConnected;
			this.lumiaConnectivity.DeviceDisconnected += this.LumiaConnectivityDeviceDisconnected;
			this.lumiaConnectivity.DeviceReadyChanged += this.LumiaConnectivityDeviceReadyChanged;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003D24 File Offset: 0x00001F24
		public void StartDetection(DetectionType detectionType)
		{
			this.Initialize();
			if (detectionType != DetectionType.NormalMode)
			{
				if (detectionType - DetectionType.RecoveryMode > 1)
				{
					throw new InvalidOperationException();
				}
				List<DeviceIdentifier> list = this.SupportedEmergencyModeIds.Union(this.SupportedRecoveryModeIds).ToList<DeviceIdentifier>();
				this.lumiaConnectivity.Start(list);
			}
			else
			{
				this.lumiaConnectivity.Start(this.SupportedNormalModeIds);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003D8C File Offset: 0x00001F8C
		public void StopDetection()
		{
			bool flag = this.lumiaConnectivity != null;
			if (flag)
			{
				this.lumiaConnectivity.Stop();
				this.lumiaConnectivity.DeviceConnected -= this.LumiaConnectivityDeviceConnected;
				this.lumiaConnectivity.DeviceDisconnected -= this.LumiaConnectivityDeviceDisconnected;
				this.lumiaConnectivity.DeviceReadyChanged -= this.LumiaConnectivityDeviceReadyChanged;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003E00 File Offset: 0x00002000
		public ReadOnlyCollection<Phone> GetAllPhones()
		{
			return new ReadOnlyCollection<Phone>(this.lumiaConnectivity.GetAllConnectedDevices().Select(new Func<ConnectedDevice, Phone>(this.CreatePhoneObjectForDevice)).ToList<Phone>());
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003E38 File Offset: 0x00002038
		private Phone CreatePhoneObjectForDevice(ConnectedDevice device)
		{
			return new Phone(device.PortId, device.Vid, device.Pid, device.DevicePath, device.TypeDesignator, string.Empty, device.SalesName, string.Empty, device.Path, this.PhoneType, device.InstanceId, this.SalesNameProvider(), device.DeviceReady, "", "");
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003EA8 File Offset: 0x000020A8
		public void TryReadMissingInfoWithThor(Phone phone, CancellationToken token)
		{
			this.thor2Service.TryReadMissingInfoWithThor(phone, token, false);
			phone.DeviceReady = true;
			bool flag = string.IsNullOrEmpty(phone.HardwareVariant);
			if (flag)
			{
				this.reportingService.OperationFailed(phone, ReportOperationType.ReadDeviceInfoWithThor, UriData.ProductCodeReadFailed, new ReadPhoneInformationException("Could not read product code"));
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(phone.HardwareModel);
				if (flag2)
				{
					this.reportingService.OperationFailed(phone, ReportOperationType.ReadDeviceInfoWithThor, UriData.CouldNotReadProductType, new ReadPhoneInformationException("Could not read product type"));
				}
				else
				{
					this.reportingService.OperationSucceded(phone, ReportOperationType.ReadDeviceInfoWithThor);
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003F38 File Offset: 0x00002138
		private void LumiaConnectivityDeviceConnected(object sender, DeviceConnectedEventArgs args)
		{
			Action<Phone> deviceConnected = this.DeviceConnected;
			bool flag = deviceConnected != null;
			if (flag)
			{
				deviceConnected(this.CreatePhoneObjectForDevice(args.ConnectedDevice));
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003F6C File Offset: 0x0000216C
		private void LumiaConnectivityDeviceDisconnected(object sender, DeviceConnectedEventArgs args)
		{
			Action<Phone> deviceDisconnected = this.DeviceDisconnected;
			bool flag = deviceDisconnected != null;
			if (flag)
			{
				deviceDisconnected(this.CreatePhoneObjectForDevice(args.ConnectedDevice));
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003FA0 File Offset: 0x000021A0
		private void LumiaConnectivityDeviceReadyChanged(object sender, DeviceReadyChangedEventArgs args)
		{
			Action<Phone> deviceReadyChanged = this.DeviceReadyChanged;
			bool flag = deviceReadyChanged != null;
			if (flag)
			{
				Phone phone = this.CreatePhoneObjectForDevice(args.ConnectedDevice);
				phone.DeviceReady = args.DeviceReady;
				deviceReadyChanged(phone);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003FE4 File Offset: 0x000021E4
		public void FillLumiaDeviceInfo(Phone phone, CancellationToken token)
		{
			this.lumiaConnectivity.FillLumiaDeviceInfo(phone, token);
			bool flag = !phone.IsProductCodeTypeEmpty();
			if (flag)
			{
				phone.ReportManufacturerName = this.ReportManufacturerName;
				phone.ReportManufacturerProductLine = this.ReportManufacturerProductLine;
				this.reportingService.OperationSucceded(phone, ReportOperationType.ReadDeviceInfo);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004038 File Offset: 0x00002238
		public override int ReadBatteryLevel(Phone phone)
		{
			return this.lumiaConnectivity.ReadBatteryLevel(phone);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004056 File Offset: 0x00002256
		protected override void ReleaseManagedObjects()
		{
			this.thor2Service.ReleaseManagedObjects();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004068 File Offset: 0x00002268
		public override PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken)
		{
			PackageFileInfo packageFileInfo;
			try
			{
				Tracer<LumiaAdaptation>.LogEntry("CheckLatestPackage");
				DownloadParameters downloadParameters = new DownloadParameters
				{
					DiscoveryParameters = this.DeviceQueryParameters(phone),
					FileExtension = "vpl",
					DestinationFolder = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetLumiaProductsPath(phone.HardwareModel)
				};
				List<string> list = this.msrService.DownloadLatestPackage(downloadParameters, cancellationToken);
				bool flag = list.Any<string>();
				if (!flag)
				{
					throw new PackageNotFoundException();
				}
				string text = list.First<string>();
				packageFileInfo = new VplPackageFileInfo(text, VariantInfo.GetVariantInfo(text));
			}
			catch (Exception ex)
			{
				bool flag2 = ex.InnerException is PackageNotFoundException;
				if (flag2)
				{
					this.reportingService.OperationFailed(phone, ReportOperationType.CheckPackage, UriData.NoPackageFound, ex.InnerException);
					throw ex.InnerException;
				}
				bool flag3 = ex.InnerException != null && ex.InnerException.GetBaseException() is WebException;
				if (flag3)
				{
					throw new WebException();
				}
				bool flag4 = ex is OperationCanceledException || ex.InnerException is TaskCanceledException;
				if (flag4)
				{
					throw;
				}
				throw;
			}
			finally
			{
				Tracer<LumiaAdaptation>.LogExit("CheckLatestPackage");
			}
			return packageFileInfo;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000041C0 File Offset: 0x000023C0
		public override SwVersionComparisonResult CompareFirmwareVersions(Phone phone)
		{
			string softwareVersion = phone.SoftwareVersion;
			return VersionComparer.CompareSoftwareVersions(softwareVersion, phone.PackageFileInfo.SoftwareVersion, new char[] { '.' });
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000041F8 File Offset: 0x000023F8
		protected override Stream GetImageDataStream(Phone phone)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = string.Empty;
			bool flag = phone.Vid.ToUpper() == "0421";
			if (flag)
			{
				text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("NokiaLumia.png"));
			}
			else
			{
				bool flag2 = phone.Vid.ToUpper() == "045E";
				if (flag2)
				{
					text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("MicrosoftLumia.png"));
				}
			}
			bool flag3 = !string.IsNullOrEmpty(text);
			Stream stream;
			if (flag3)
			{
				stream = executingAssembly.GetManifestResourceStream(text);
			}
			else
			{
				stream = base.GetImageDataStream(phone);
			}
			return stream;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000042C8 File Offset: 0x000024C8
		protected override Stream GetManufacturerImageDataStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("LumiaLogo.png"));
			bool flag = !string.IsNullOrEmpty(text);
			Stream stream;
			if (flag)
			{
				stream = executingAssembly.GetManifestResourceStream(text);
			}
			else
			{
				stream = null;
			}
			return stream;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000432C File Offset: 0x0000252C
		private QueryParameters DeviceQueryParameters(Phone phone)
		{
			return new QueryParameters
			{
				ManufacturerName = "Microsoft",
				ManufacturerProductLine = "Lumia",
				PackageType = "Firmware",
				PackageClass = "Public",
				ManufacturerHardwareModel = phone.HardwareModel,
				ManufacturerHardwareVariant = phone.HardwareVariant
			};
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000438D File Offset: 0x0000258D
		private void MsrDownloadProgressEvent(ProgressChangedEventArgs args)
		{
			base.RaiseProgressPercentageChanged(args.Percentage, args.Message, args.DownloadedSize, args.TotalSize, args.BytesPerSecond, args.SecondsLeft);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000043BC File Offset: 0x000025BC
		public override BatteryStatus ReadBatteryStatus(Phone phone)
		{
			try
			{
				int num = this.ReadBatteryLevel(phone);
				Tracer<LumiaAdaptation>.WriteInformation("Battery level: ", new object[] { num });
				bool flag = num >= 25;
				if (flag)
				{
					return BatteryStatus.BatteryOk;
				}
				bool flag2 = num != -1;
				if (flag2)
				{
					return BatteryStatus.BatteryNotOkDoNotBlock;
				}
			}
			catch (Exception ex)
			{
				Tracer<LumiaAdaptation>.WriteError("Cannot read battery level!: ", new object[] { ex });
			}
			return BatteryStatus.BatteryUnknown;
		}

		// Token: 0x04000017 RID: 23
		private const long ErrorEmptyDiskSpace = -2147024784L;

		// Token: 0x04000018 RID: 24
		private const long ErrorNotFound = -2146233079L;

		// Token: 0x04000019 RID: 25
		private readonly DataPackageService dataPackageService;

		// Token: 0x0400001A RID: 26
		private readonly Thor2Service thor2Service;

		// Token: 0x0400001B RID: 27
		private readonly ReportingService reportingService;

		// Token: 0x0400001C RID: 28
		private readonly SalesNameProvider salesNameProvider;

		// Token: 0x0400001D RID: 29
		private readonly MsrService msrService;

		// Token: 0x0400001E RID: 30
		private LumiaConnectivity lumiaConnectivity;
	}
}
