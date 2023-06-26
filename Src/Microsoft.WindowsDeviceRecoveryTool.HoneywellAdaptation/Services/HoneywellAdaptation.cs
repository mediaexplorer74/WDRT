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
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.HoneywellAdaptation.Services
{
	// Token: 0x02000005 RID: 5
	[ExportAdaptation(Type = PhoneTypes.Honeywell)]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class HoneywellAdaptation : BaseAdaptation
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002490 File Offset: 0x00000690
		[ImportingConstructor]
		public HoneywellAdaptation(FfuFileInfoService ffuFileInfoService, MsrService msrService, ReportingService reportingService)
		{
			this.reportingService = reportingService;
			this.ffuFileInfoService = ffuFileInfoService;
			this.msrService = msrService;
			this.msrService.ProgressChanged += this.MsrDownloadProgressEvent;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000024E5 File Offset: 0x000006E5
		public override string PackageExtension
		{
			get
			{
				return "ffu";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000024EC File Offset: 0x000006EC
		public override PhoneTypes PhoneType
		{
			get
			{
				return PhoneTypes.Honeywell;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000024F0 File Offset: 0x000006F0
		public override bool RecoverySupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000024F3 File Offset: 0x000006F3
		public override string ReportManufacturerName
		{
			get
			{
				return "HONEYWELL";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000024F3 File Offset: 0x000006F3
		public override string ManufacturerName
		{
			get
			{
				return "HONEYWELL";
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024FC File Offset: 0x000006FC
		public override PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken)
		{
			PackageFileInfo result;
			try
			{
				Tracer<HoneywellAdaptation>.LogEntry("CheckLatestPackage");
				Task<PackageFileInfo> task = this.msrService.CheckLatestPackage(phone.QueryParameters, cancellationToken);
				task.Wait(cancellationToken);
				result = task.Result;
			}
			catch (Exception ex)
			{
				if (ex.InnerException is PackageNotFoundException)
				{
					throw ex.InnerException;
				}
				if (ex.InnerException != null && ex.InnerException.GetBaseException() is WebException)
				{
					throw new WebException();
				}
				if (!(ex is OperationCanceledException))
				{
					TaskCanceledException ex2 = ex.InnerException as TaskCanceledException;
				}
				throw;
			}
			finally
			{
				Tracer<HoneywellAdaptation>.LogExit("CheckLatestPackage");
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025A8 File Offset: 0x000007A8
		public override void CheckPackageIntegrity(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<HoneywellAdaptation>.LogEntry("CheckPackageIntegrity");
			this.ffuFileInfoService.ReadFfuFile(phone.PackageFilePath);
			Tracer<HoneywellAdaptation>.LogExit("CheckPackageIntegrity");
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000025CF File Offset: 0x000007CF
		public override SwVersionComparisonResult CompareFirmwareVersions(Phone phone)
		{
			return SwVersionComparisonResult.UnableToCompare;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025D2 File Offset: 0x000007D2
		public override void DownloadEmergencyPackage(Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000025DC File Offset: 0x000007DC
		public override void DownloadPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<HoneywellAdaptation>.LogEntry("DownloadPackage");
			base.RaiseProgressPercentageChanged(0, null);
			try
			{
				this.reportingService.OperationStarted(phone, ReportOperationType.DownloadPackage);
				DownloadParameters downloadParameters = new DownloadParameters
				{
					DiscoveryParameters = phone.QueryParameters,
					DestinationFolder = ((!string.IsNullOrEmpty(phone.QueryParameters.ManufacturerHardwareModel)) ? Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetHoneywellProductsPath(phone.QueryParameters.ManufacturerHardwareModel) : Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetHoneywellProductsPath(phone.QueryParameters.ManufacturerHardwareVariant)),
					FilesVersioned = true
				};
				Tracer<HoneywellAdaptation>.WriteInformation("Download Params: {0}", new object[] { downloadParameters });
				phone.PackageFiles = this.msrService.DownloadLatestPackage(downloadParameters, cancellationToken);
				Tuple<long, long, bool> downloadPackageInformation = this.msrService.GetDownloadPackageInformation();
				this.reportingService.SetDownloadByteInformation(phone, ReportOperationType.DownloadPackage, downloadPackageInformation.Item1, downloadPackageInformation.Item2, downloadPackageInformation.Item3);
				this.reportingService.OperationSucceded(phone, ReportOperationType.DownloadPackage);
			}
			catch (Exception ex)
			{
				bool flag = true;
				UriData uriData;
				if (ex is OperationCanceledException || ex.GetBaseException() is TaskCanceledException)
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
				Tracer<HoneywellAdaptation>.WriteError(ex);
				if (flag)
				{
					throw;
				}
			}
			finally
			{
				Tracer<HoneywellAdaptation>.LogExit("DownloadPackage");
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025D2 File Offset: 0x000007D2
		public override List<PackageFileInfo> FindAllPackages(string directory, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025D2 File Offset: 0x000007D2
		public override List<PackageFileInfo> FindPackage(string directory, Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000276C File Offset: 0x0000096C
		public override void FlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<HoneywellAdaptation>.LogEntry("FlashDevice");
			IFFUDevice ffuDevice = this.GetFfuDevice(phone);
			if (ffuDevice != null)
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
			Tracer<HoneywellAdaptation>.LogExit("FlashDevice");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000027EC File Offset: 0x000009EC
		private void FlashProgressEvent(object obj, ProgressEventArgs progress)
		{
			double num = (double)progress.Position / (double)progress.Length * 100.0;
			base.RaiseProgressPercentageChanged((int)num, this.progressMessage);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002821 File Offset: 0x00000A21
		public override bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken cancellationToken)
		{
			return this.GetFfuDevice(phone) != null;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002830 File Offset: 0x00000A30
		public override void ReadDeviceInfo(Phone currentPhone, CancellationToken cancellationToken)
		{
			try
			{
				SimpleIODevice simpleIODevice = new SimpleIODevice(currentPhone.Path);
				if (simpleIODevice.IsConnected())
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
				Tracer<HoneywellAdaptation>.WriteError(ex);
				throw new ReadPhoneInformationException(ex.Message, ex);
			}
			throw new ReadPhoneInformationException("Cannot find selected device!");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000028BC File Offset: 0x00000ABC
		protected override void FillSupportedDeviceIdentifiers()
		{
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("045E", "F0CA"));
			this.SupportedFlashModeIds.Add(new DeviceIdentifier("045E", "062A"));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000028F4 File Offset: 0x00000AF4
		private Phone GetPhone(ModelInfo modelInfo)
		{
			return new Phone
			{
				Type = this.PhoneType,
				SalesName = modelInfo.Name,
				HardwareModel = modelInfo.Name,
				ImageData = modelInfo.Bitmap.ToBytes(),
				ModelIdentificationInstruction = LocalizationManager.GetTranslation("ModelIdentificationUnderBackCover")
			};
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000294C File Offset: 0x00000B4C
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

		// Token: 0x06000020 RID: 32 RVA: 0x00002990 File Offset: 0x00000B90
		protected override void InitializeManuallySupportedModels()
		{
			foreach (ModelInfo modelInfo in new ModelInfo[]
			{
				HoneywellModels.Dolphin_75e_W10M,
				HoneywellModels.Dolphin_CT50_W10M
			})
			{
				Phone phone = this.GetPhone(modelInfo);
				this.manuallySupportedModels.Add(phone);
				if (modelInfo.Variants.Length == 1)
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

		// Token: 0x06000021 RID: 33 RVA: 0x00002A10 File Offset: 0x00000C10
		public override List<Phone> ManuallySupportedModels()
		{
			return this.manuallySupportedModels;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A18 File Offset: 0x00000C18
		public override List<Phone> ManuallySupportedVariants(Phone phone)
		{
			return this.manuallySupportedVariants.Where((Phone variant) => string.Equals(variant.HardwareModel, phone.HardwareModel, StringComparison.OrdinalIgnoreCase)).ToList<Phone>();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A50 File Offset: 0x00000C50
		protected override Stream GetImageDataStream(Phone phone)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = string.Empty;
			if (phone.SalesName.ToLower().Contains("dolphin 75e"))
			{
				text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("D75e_360Tour.jpg"));
			}
			else if (phone.SalesName.ToLower().Contains("dolphin ct50"))
			{
				text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("CT50_360Tour.jpg"));
			}
			if (!string.IsNullOrEmpty(text))
			{
				return executingAssembly.GetManifestResourceStream(text);
			}
			return base.GetImageDataStream(phone);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002B04 File Offset: 0x00000D04
		protected override Stream GetManufacturerImageDataStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().FirstOrDefault((string resourceName) => resourceName.Contains("Honeywell_logo.png"));
			if (!string.IsNullOrEmpty(text))
			{
				return executingAssembly.GetManifestResourceStream(text);
			}
			return null;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002B53 File Offset: 0x00000D53
		private void MsrDownloadProgressEvent(ProgressChangedEventArgs progressArgs)
		{
			base.RaiseProgressPercentageChanged(progressArgs.Percentage, progressArgs.Message, progressArgs.DownloadedSize, progressArgs.TotalSize, progressArgs.BytesPerSecond, progressArgs.SecondsLeft);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002B80 File Offset: 0x00000D80
		private IFFUDevice GetFfuDevice(Phone phone)
		{
			Tracer<HoneywellAdaptation>.LogEntry("GetFfuDevice");
			FFUManager.Start();
			IFFUDevice flashableDevice;
			try
			{
				flashableDevice = FFUManager.GetFlashableDevice(phone.Path, false);
			}
			finally
			{
				FFUManager.Stop();
				Tracer<HoneywellAdaptation>.LogExit("GetFfuDevice");
			}
			return flashableDevice;
		}

		// Token: 0x0400000E RID: 14
		private readonly MsrService msrService;

		// Token: 0x0400000F RID: 15
		private readonly ReportingService reportingService;

		// Token: 0x04000010 RID: 16
		private readonly FfuFileInfoService ffuFileInfoService;

		// Token: 0x04000011 RID: 17
		private readonly List<Phone> manuallySupportedModels = new List<Phone>();

		// Token: 0x04000012 RID: 18
		private readonly List<Phone> manuallySupportedVariants = new List<Phone>();

		// Token: 0x04000013 RID: 19
		private string progressMessage;
	}
}
