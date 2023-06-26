using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FFUComponents;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Contracts;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.AlcatelAdaptation.Services
{
	// Token: 0x02000006 RID: 6
	[ExportAdaptation(Type = PhoneTypes.Alcatel)]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class AlcatelAdaptation : BaseAdaptation
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002490 File Offset: 0x00000690
		[ImportingConstructor]
		public AlcatelAdaptation(FfuFileInfoService ffuFileInfoService, MsrService msrService, ReportingService reportingService)
		{
			this.reportingService = reportingService;
			this.ffuFileInfoService = ffuFileInfoService;
			this.msrService = msrService;
			this.msrService.ProgressChanged += this.MsrDownloadProgressEvent;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000024E8 File Offset: 0x000006E8
		public override PhoneTypes PhoneType
		{
			get
			{
				return PhoneTypes.Alcatel;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000024FC File Offset: 0x000006FC
		public override bool RecoverySupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002510 File Offset: 0x00000710
		public override string ReportManufacturerName
		{
			get
			{
				return "Alcatel";
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002528 File Offset: 0x00000728
		public override string ManufacturerName
		{
			get
			{
				return "Alcatel";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002540 File Offset: 0x00000740
		public override string PackageExtension
		{
			get
			{
				return "ffu";
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002558 File Offset: 0x00000758
		public override PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken)
		{
			PackageFileInfo result;
			try
			{
				Tracer<AlcatelAdaptation>.LogEntry("CheckLatestPackage");
				Task<PackageFileInfo> task = this.msrService.CheckLatestPackage(phone.QueryParameters, cancellationToken);
				task.Wait(cancellationToken);
				result = task.Result;
			}
			catch (Exception ex)
			{
				bool flag = ex.InnerException is PackageNotFoundException;
				if (flag)
				{
					throw ex.InnerException;
				}
				bool flag2 = ex.InnerException != null && ex.InnerException.GetBaseException() is WebException;
				if (flag2)
				{
					throw new WebException();
				}
				bool flag3 = ex is OperationCanceledException || ex.InnerException is TaskCanceledException;
				if (flag3)
				{
					throw;
				}
				throw;
			}
			finally
			{
				Tracer<AlcatelAdaptation>.LogExit("CheckLatestPackage");
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000262C File Offset: 0x0000082C
		public override void CheckPackageIntegrity(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<AlcatelAdaptation>.LogEntry("CheckPackageIntegrity");
			this.ffuFileInfoService.ReadFfuFile(phone.PackageFilePath);
			Tracer<AlcatelAdaptation>.LogExit("CheckPackageIntegrity");
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002658 File Offset: 0x00000858
		public override SwVersionComparisonResult CompareFirmwareVersions(Phone phone)
		{
			return SwVersionComparisonResult.UnableToCompare;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000266B File Offset: 0x0000086B
		public override void DownloadEmergencyPackage(Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002674 File Offset: 0x00000874
		public override void DownloadPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<AlcatelAdaptation>.LogEntry("DownloadPackage");
			base.RaiseProgressPercentageChanged(0, null);
			try
			{
				this.reportingService.OperationStarted(phone, ReportOperationType.DownloadPackage);
				DownloadParameters downloadParameters = new DownloadParameters
				{
					DiscoveryParameters = phone.QueryParameters,
					DestinationFolder = ((!string.IsNullOrEmpty(phone.QueryParameters.ManufacturerHardwareModel)) ? Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetAlcatelProductsPath(phone.QueryParameters.ManufacturerHardwareModel) : Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetAlcatelProductsPath(phone.QueryParameters.ManufacturerHardwareVariant)),
					FilesVersioned = true
				};
				Tracer<AlcatelAdaptation>.WriteInformation("Download Params: {0}", new object[] { downloadParameters });
				phone.PackageFiles = this.msrService.DownloadLatestPackage(downloadParameters, cancellationToken);
				Tuple<long, long, bool> downloadPackageInformation = this.msrService.GetDownloadPackageInformation();
				this.reportingService.SetDownloadByteInformation(phone, ReportOperationType.DownloadPackage, downloadPackageInformation.Item1, downloadPackageInformation.Item2, downloadPackageInformation.Item3);
				this.reportingService.OperationSucceded(phone, ReportOperationType.DownloadPackage);
			}
			catch (Exception ex)
			{
				bool flag = true;
				bool flag2 = ex is OperationCanceledException || ex.GetBaseException() is TaskCanceledException;
				UriData uriData;
				if (flag2)
				{
					uriData = UriData.DownloadVariantPackageAbortedByUser;
					flag = false;
				}
				else
				{
					uriData = UriData.FailedToDownloadVariantPackage;
				}
				Tuple<long, long, bool> downloadPackageInformation2 = this.msrService.GetDownloadPackageInformation();
				this.reportingService.SetDownloadByteInformation(phone, ReportOperationType.DownloadPackage, downloadPackageInformation2.Item1, downloadPackageInformation2.Item2, downloadPackageInformation2.Item3);
				this.reportingService.OperationFailed(phone, ReportOperationType.DownloadPackage, uriData, ex);
				Tracer<AlcatelAdaptation>.WriteError(ex);
				bool flag3 = flag;
				if (flag3)
				{
					throw;
				}
			}
			finally
			{
				Tracer<AlcatelAdaptation>.LogExit("DownloadPackage");
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000266B File Offset: 0x0000086B
		public override List<PackageFileInfo> FindPackage(string directory, Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000266B File Offset: 0x0000086B
		public override List<PackageFileInfo> FindAllPackages(string directory, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002830 File Offset: 0x00000A30
		public override void FlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<AlcatelAdaptation>.LogEntry("FlashDevice");
			IFFUDevice ffuDevice = this.GetFfuDevice(phone);
			bool flag = ffuDevice != null;
			if (flag)
			{
				ffuDevice.ProgressEvent += this.FlashProgressEvent;
				try
				{
					this.progressMessage = "SoftwareInstallation";
					ffuDevice.FlashFFUFile(phone.PackageFiles.First<string>(), true);
				}
				finally
				{
					ffuDevice.ProgressEvent -= this.FlashProgressEvent;
				}
			}
			Tracer<AlcatelAdaptation>.LogExit("FlashDevice");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028C4 File Offset: 0x00000AC4
		private void FlashProgressEvent(object obj, ProgressEventArgs progress)
		{
			double num = (double)progress.Position / (double)progress.Length * 100.0;
			base.RaiseProgressPercentageChanged((int)num, this.progressMessage);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000028FC File Offset: 0x00000AFC
		public override bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken cancellationToken)
		{
			return this.GetFfuDevice(phone) != null;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002918 File Offset: 0x00000B18
		public override void ReadDeviceInfo(Phone currentPhone, CancellationToken cancellationToken)
		{
			try
			{
				SimpleIODevice simpleIODevice = new SimpleIODevice(currentPhone.Path);
				bool flag = simpleIODevice.IsConnected();
				if (flag)
				{
					PlatformId platformId = new PlatformId();
					platformId.SetPlatformId(simpleIODevice.DeviceFriendlyName);
					currentPhone.PlatformId = platformId;
					currentPhone.SalesName = simpleIODevice.DeviceFriendlyName;
					currentPhone.ConnectionId = simpleIODevice.DeviceUniqueID;
					base.RaiseDeviceInfoRead(currentPhone);
					return;
				}
			}
			catch (Exception ex)
			{
				Tracer<AlcatelAdaptation>.WriteError(ex);
				throw new ReadPhoneInformationException(ex.Message, ex);
			}
			throw new ReadPhoneInformationException("Cannot find selected device!");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000029B4 File Offset: 0x00000BB4
		protected override void FillSupportedDeviceIdentifiers()
		{
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("045E", "F0CA"));
			this.SupportedFlashModeIds.Add(new DeviceIdentifier("045E", "062A"));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000029F0 File Offset: 0x00000BF0
		private Phone GetPhone(ModelInfo modelInfo)
		{
			return new Phone
			{
				Type = this.PhoneType,
				SalesName = modelInfo.Name,
				HardwareModel = modelInfo.Name,
				ImageData = modelInfo.Bitmap.ToBytes(),
				ModelIdentificationInstruction = LocalizationManager.GetTranslation("ModelIndentificationAlcatelIDO4S")
			};
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002A54 File Offset: 0x00000C54
		private Phone[] GetPhoneVariants(ModelInfo modelInfo)
		{
			return modelInfo.Variants.Select((Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives.VariantInfo v) => new Phone
			{
				Type = this.PhoneType,
				SalesName = v.Name,
				HardwareVariant = v.Name,
				HardwareModel = modelInfo.Name,
				ImageData = modelInfo.Bitmap.ToBytes(),
				QueryParameters = v.MsrQueryParameters
			}).ToArray<Phone>();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002A9C File Offset: 0x00000C9C
		protected override void InitializeManuallySupportedModels()
		{
			ModelInfo[] array = new ModelInfo[]
			{
				AlcatelModels.IDOL4S,
				AlcatelModels.IDO4SPRO,
				AlcatelModels.FierceXL
			};
			foreach (ModelInfo modelInfo in array)
			{
				Phone phone = this.GetPhone(modelInfo);
				this.manuallySupportedModels.Add(phone);
				bool flag = modelInfo.Variants.Length == 1;
				if (flag)
				{
					phone.QueryParameters = modelInfo.Variants[0].MsrQueryParameters;
				}
				else
				{
					Phone[] phoneVariants = this.GetPhoneVariants(modelInfo);
					this.manuallySupportedVariants.AddRange(phoneVariants);
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002B3C File Offset: 0x00000D3C
		public override List<Phone> ManuallySupportedModels()
		{
			return this.manuallySupportedModels;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002B54 File Offset: 0x00000D54
		public override List<Phone> ManuallySupportedVariants(Phone phone)
		{
			return this.manuallySupportedVariants.Where((Phone variant) => string.Equals(variant.HardwareModel, phone.HardwareModel, StringComparison.OrdinalIgnoreCase)).ToList<Phone>();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002B90 File Offset: 0x00000D90
		protected override Stream GetImageDataStream(Phone phone)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = string.Empty;
			bool flag = phone.SalesName.ToLower().Contains("idol4s");
			if (flag)
			{
				text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("IDOL_4S_device_front.png"));
			}
			else
			{
				bool flag2 = phone.SalesName.ToLower().Contains("fierce xl");
				if (flag2)
				{
					text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("5055W_front.jpg"));
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

		// Token: 0x06000024 RID: 36 RVA: 0x00002C60 File Offset: 0x00000E60
		protected override Stream GetManufacturerImageDataStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("Alcatel_Logo.png"));
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

		// Token: 0x06000025 RID: 37 RVA: 0x00002CC1 File Offset: 0x00000EC1
		private void MsrDownloadProgressEvent(ProgressChangedEventArgs progressArgs)
		{
			base.RaiseProgressPercentageChanged(progressArgs.Percentage, progressArgs.Message, progressArgs.DownloadedSize, progressArgs.TotalSize, progressArgs.BytesPerSecond, progressArgs.SecondsLeft);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002CF0 File Offset: 0x00000EF0
		private IFFUDevice GetFfuDevice(Phone phone)
		{
			Tracer<AlcatelAdaptation>.LogEntry("GetFfuDevice");
			FFUManager.Start();
			IFFUDevice flashableDevice;
			try
			{
				flashableDevice = FFUManager.GetFlashableDevice(phone.Path, false);
			}
			finally
			{
				FFUManager.Stop();
				Tracer<AlcatelAdaptation>.LogExit("GetFfuDevice");
			}
			return flashableDevice;
		}

		// Token: 0x0400000C RID: 12
		private readonly MsrService msrService;

		// Token: 0x0400000D RID: 13
		private readonly ReportingService reportingService;

		// Token: 0x0400000E RID: 14
		private readonly FfuFileInfoService ffuFileInfoService;

		// Token: 0x0400000F RID: 15
		private readonly List<Phone> manuallySupportedModels = new List<Phone>();

		// Token: 0x04000010 RID: 16
		private readonly List<Phone> manuallySupportedVariants = new List<Phone>();

		// Token: 0x04000011 RID: 17
		private string progressMessage;
	}
}
