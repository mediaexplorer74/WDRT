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

namespace Microsoft.WindowsDeviceRecoveryTool.McjAdaptation.Services
{
	// Token: 0x02000005 RID: 5
	[ExportAdaptation(Type = PhoneTypes.Mcj)]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class McjAdaptation : BaseAdaptation
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002300 File Offset: 0x00000500
		[ImportingConstructor]
		public McjAdaptation(FfuFileInfoService ffuFileInfoService, MsrService msrService, ReportingService reportingService)
		{
			this.salesNameProvider = new SalesNameProvider();
			this.reportingService = reportingService;
			this.ffuFileInfoService = ffuFileInfoService;
			this.msrService = msrService;
			this.msrService.ProgressChanged += this.MsrDownloadProgressEvent;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002364 File Offset: 0x00000564
		public override PhoneTypes PhoneType
		{
			get
			{
				return PhoneTypes.Mcj;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002378 File Offset: 0x00000578
		public override bool RecoverySupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000238C File Offset: 0x0000058C
		public override string ReportManufacturerName
		{
			get
			{
				return "MCJ";
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000023A4 File Offset: 0x000005A4
		public override string ManufacturerName
		{
			get
			{
				return "MCJ";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000023BC File Offset: 0x000005BC
		public override string PackageExtension
		{
			get
			{
				return "ffu";
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023D4 File Offset: 0x000005D4
		public override List<Phone> ManuallySupportedModels()
		{
			return this.manuallySupportedModels;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023EC File Offset: 0x000005EC
		public override List<Phone> ManuallySupportedVariants(Phone phone)
		{
			return this.manuallySupportedVariants.Where((Phone variant) => string.Equals(variant.HardwareModel, phone.HardwareModel, StringComparison.OrdinalIgnoreCase)).ToList<Phone>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002428 File Offset: 0x00000628
		public override ISalesNameProvider SalesNameProvider()
		{
			return this.salesNameProvider;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002440 File Offset: 0x00000640
		public override List<PackageFileInfo> FindPackage(string directory, Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002440 File Offset: 0x00000640
		public override List<PackageFileInfo> FindAllPackages(string directory, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002448 File Offset: 0x00000648
		public override PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken)
		{
			PackageFileInfo result;
			try
			{
				Tracer<McjAdaptation>.LogEntry("CheckLatestPackage");
				bool flag = phone.QueryParameters == null;
				if (flag)
				{
					throw new ArgumentException("Package query parameter not set.");
				}
				Task<PackageFileInfo> task = this.msrService.CheckLatestPackage(phone.QueryParameters, cancellationToken);
				task.Wait(cancellationToken);
				result = task.Result;
			}
			catch (Exception ex)
			{
				bool flag2 = ex.InnerException is PackageNotFoundException;
				if (flag2)
				{
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
				Tracer<McjAdaptation>.LogExit("CheckLatestPackage");
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002538 File Offset: 0x00000738
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

		// Token: 0x06000017 RID: 23 RVA: 0x00002580 File Offset: 0x00000780
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

		// Token: 0x06000018 RID: 24 RVA: 0x000025E4 File Offset: 0x000007E4
		protected override void InitializeManuallySupportedModels()
		{
			ModelInfo[] array = new ModelInfo[]
			{
				McjModels.MadosmaQ501,
				McjModels.MadosmaQ601
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

		// Token: 0x06000019 RID: 25 RVA: 0x0000267A File Offset: 0x0000087A
		public override void CheckPackageIntegrity(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<McjAdaptation>.LogEntry("CheckPackageIntegrity");
			this.ffuFileInfoService.ReadFfuFile(phone.PackageFilePath);
			Tracer<McjAdaptation>.LogExit("CheckPackageIntegrity");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026A8 File Offset: 0x000008A8
		public override void FlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<McjAdaptation>.LogEntry("FlashDevice");
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
			Tracer<McjAdaptation>.LogExit("FlashDevice");
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000273C File Offset: 0x0000093C
		public override bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken cancellationToken)
		{
			return this.GetFfuDevice(phone) != null;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002758 File Offset: 0x00000958
		public override void DownloadPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<McjAdaptation>.LogEntry("DownloadPackage");
			base.RaiseProgressPercentageChanged(0, null);
			try
			{
				this.reportingService.OperationStarted(phone, ReportOperationType.DownloadPackage);
				DownloadParameters downloadParameters = new DownloadParameters
				{
					DiscoveryParameters = phone.QueryParameters,
					DestinationFolder = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetMcjProductsPath(phone.QueryParameters.ManufacturerHardwareModel),
					FilesVersioned = true
				};
				Tracer<McjAdaptation>.WriteInformation("Download Params: {0}", new object[] { downloadParameters });
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
				Tracer<McjAdaptation>.WriteError(ex);
				bool flag3 = flag;
				if (flag3)
				{
					throw;
				}
			}
			finally
			{
				Tracer<McjAdaptation>.LogExit("DownloadPackage");
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002440 File Offset: 0x00000640
		public override void DownloadEmergencyPackage(Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000028F0 File Offset: 0x00000AF0
		public override SwVersionComparisonResult CompareFirmwareVersions(Phone phone)
		{
			return SwVersionComparisonResult.UnableToCompare;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002904 File Offset: 0x00000B04
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
				Tracer<McjAdaptation>.WriteError(ex);
				throw new ReadPhoneInformationException(ex.Message, ex);
			}
			throw new ReadPhoneInformationException("Cannot find selected device!");
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000029A0 File Offset: 0x00000BA0
		protected override void FillSupportedDeviceIdentifiers()
		{
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("045E", "F0CA"));
			this.SupportedFlashModeIds.Add(new DeviceIdentifier("045E", "062A"));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000029DC File Offset: 0x00000BDC
		private IFFUDevice GetFfuDevice(Phone phone)
		{
			Tracer<McjAdaptation>.LogEntry("GetFfuDevice");
			FFUManager.Start();
			IFFUDevice flashableDevice;
			try
			{
				flashableDevice = FFUManager.GetFlashableDevice(phone.Path, false);
			}
			finally
			{
				FFUManager.Stop();
				Tracer<McjAdaptation>.LogExit("GetFfuDevice");
			}
			return flashableDevice;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A30 File Offset: 0x00000C30
		private void FlashProgressEvent(object obj, ProgressEventArgs progress)
		{
			double num = (double)progress.Position / (double)progress.Length * 100.0;
			base.RaiseProgressPercentageChanged((int)num, this.progressMessage);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A67 File Offset: 0x00000C67
		private void MsrDownloadProgressEvent(ProgressChangedEventArgs progressArgs)
		{
			base.RaiseProgressPercentageChanged(progressArgs.Percentage, progressArgs.Message, progressArgs.DownloadedSize, progressArgs.TotalSize, progressArgs.BytesPerSecond, progressArgs.SecondsLeft);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A98 File Offset: 0x00000C98
		protected override Stream GetImageDataStream(Phone phone)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = string.Empty;
			bool flag = phone.SalesName.ToLower().Contains("Q501");
			if (flag)
			{
				text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("Madosma.png"));
			}
			else
			{
				bool flag2 = phone.SalesName.ToLower().Contains("Q601");
				if (flag2)
				{
					text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("Madosma_Q601.jpg"));
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

		// Token: 0x06000025 RID: 37 RVA: 0x00002B68 File Offset: 0x00000D68
		protected override Stream GetManufacturerImageDataStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("Mouse_logo_new.gif"));
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

		// Token: 0x04000009 RID: 9
		private readonly MsrService msrService;

		// Token: 0x0400000A RID: 10
		private readonly ReportingService reportingService;

		// Token: 0x0400000B RID: 11
		private readonly FfuFileInfoService ffuFileInfoService;

		// Token: 0x0400000C RID: 12
		private readonly SalesNameProvider salesNameProvider;

		// Token: 0x0400000D RID: 13
		private readonly List<Phone> manuallySupportedModels = new List<Phone>();

		// Token: 0x0400000E RID: 14
		private readonly List<Phone> manuallySupportedVariants = new List<Phone>();

		// Token: 0x0400000F RID: 15
		private string progressMessage;
	}
}
