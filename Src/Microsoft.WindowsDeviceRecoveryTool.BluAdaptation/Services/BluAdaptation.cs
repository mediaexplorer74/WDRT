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

namespace Microsoft.WindowsDeviceRecoveryTool.BluAdaptation.Services
{
	// Token: 0x02000005 RID: 5
	[ExportAdaptation(Type = PhoneTypes.Blu)]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class BluAdaptation : BaseAdaptation
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000027E4 File Offset: 0x000009E4
		[ImportingConstructor]
		public BluAdaptation(FfuFileInfoService ffuFileInfoService, MsrService msrService, ReportingService reportingService)
		{
			this.reportingService = reportingService;
			this.ffuFileInfoService = ffuFileInfoService;
			this.msrService = msrService;
			this.msrService.ProgressChanged += this.MsrDownloadProgressEvent;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000283C File Offset: 0x00000A3C
		public override PhoneTypes PhoneType
		{
			get
			{
				return PhoneTypes.Blu;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002850 File Offset: 0x00000A50
		public override bool RecoverySupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002864 File Offset: 0x00000A64
		public override string ReportManufacturerName
		{
			get
			{
				return "BLU";
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000287C File Offset: 0x00000A7C
		public override string ManufacturerName
		{
			get
			{
				return "BLU";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002894 File Offset: 0x00000A94
		public override string PackageExtension
		{
			get
			{
				return "ffu";
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000028AB File Offset: 0x00000AAB
		public override List<PackageFileInfo> FindAllPackages(string directory, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000028B4 File Offset: 0x00000AB4
		protected override void FillSupportedDeviceIdentifiers()
		{
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("045E", "F0CA"));
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("05C6", "9093"));
			this.SupportedFlashModeIds.Add(new DeviceIdentifier("045E", "062A"));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002913 File Offset: 0x00000B13
		public override void ReadDeviceInfo(Phone currentPhone, CancellationToken cancellationToken)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002918 File Offset: 0x00000B18
		public override SwVersionComparisonResult CompareFirmwareVersions(Phone phone)
		{
			return SwVersionComparisonResult.UnableToCompare;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000028AB File Offset: 0x00000AAB
		public override void DownloadEmergencyPackage(Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000292C File Offset: 0x00000B2C
		public override void DownloadPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<BluAdaptation>.LogEntry("DownloadPackage");
			base.RaiseProgressPercentageChanged(0, null);
			try
			{
				this.reportingService.OperationStarted(phone, ReportOperationType.DownloadPackage);
				DownloadParameters downloadParameters = new DownloadParameters
				{
					DiscoveryParameters = phone.QueryParameters,
					DestinationFolder = ((!string.IsNullOrEmpty(phone.QueryParameters.ManufacturerHardwareModel)) ? Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetBluProductsPath(phone.QueryParameters.ManufacturerHardwareModel) : Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetBluProductsPath(phone.QueryParameters.ManufacturerHardwareVariant)),
					FilesVersioned = true
				};
				Tracer<BluAdaptation>.WriteInformation("Download Params: {0}", new object[] { downloadParameters });
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
				Tracer<BluAdaptation>.WriteError(ex);
				bool flag3 = flag;
				if (flag3)
				{
					throw;
				}
			}
			finally
			{
				Tracer<BluAdaptation>.LogExit("DownloadPackage");
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public override bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken cancellationToken)
		{
			return this.GetFfuDevice(phone) != null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002B04 File Offset: 0x00000D04
		public override void FlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<BluAdaptation>.LogEntry("FlashDevice");
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
			Tracer<BluAdaptation>.LogExit("FlashDevice");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002B98 File Offset: 0x00000D98
		public override void CheckPackageIntegrity(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<BluAdaptation>.LogEntry("CheckPackageIntegrity");
			this.ffuFileInfoService.ReadFfuFile(phone.PackageFilePath);
			Tracer<BluAdaptation>.LogExit("CheckPackageIntegrity");
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public override PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken)
		{
			PackageFileInfo result;
			try
			{
				Tracer<BluAdaptation>.LogEntry("CheckLatestPackage");
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
				Tracer<BluAdaptation>.LogExit("CheckLatestPackage");
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000028AB File Offset: 0x00000AAB
		public override List<PackageFileInfo> FindPackage(string directory, Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002CB4 File Offset: 0x00000EB4
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

		// Token: 0x0600001E RID: 30 RVA: 0x00002CFC File Offset: 0x00000EFC
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

		// Token: 0x0600001F RID: 31 RVA: 0x00002D60 File Offset: 0x00000F60
		protected override void InitializeManuallySupportedModels()
		{
			ModelInfo[] array = new ModelInfo[]
			{
				BluModels.WinJrLte,
				BluModels.WinHdLte,
				BluModels.WinJR410,
				BluModels.WinHd510
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

		// Token: 0x06000020 RID: 32 RVA: 0x00002E08 File Offset: 0x00001008
		public override List<Phone> ManuallySupportedModels()
		{
			return this.manuallySupportedModels;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E20 File Offset: 0x00001020
		public override List<Phone> ManuallySupportedVariants(Phone phone)
		{
			return this.manuallySupportedVariants.Where((Phone variant) => string.Equals(variant.HardwareModel, phone.HardwareModel, StringComparison.OrdinalIgnoreCase)).ToList<Phone>();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002E5C File Offset: 0x0000105C
		protected override Stream GetImageDataStream(Phone phone)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = string.Empty;
			bool flag = phone.SalesName.ToLower().Contains("win hd lte");
			if (flag)
			{
				text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("winhdlte.png"));
			}
			else
			{
				bool flag2 = phone.SalesName.ToLower().Contains("win jr w410a");
				if (flag2)
				{
					text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("winjr.png"));
				}
				else
				{
					bool flag3 = phone.SalesName.ToLower().Contains("win hd w510u");
					if (flag3)
					{
						text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("winhd.png"));
					}
					else
					{
						bool flag4 = phone.SalesName.ToLower().Contains("win jr lte");
						if (flag4)
						{
							text = manifestResourceNames.FirstOrDefault((string resourcesName) => resourcesName.Contains("winjrlte.png"));
						}
					}
				}
			}
			bool flag5 = !string.IsNullOrEmpty(text);
			Stream stream;
			if (flag5)
			{
				stream = executingAssembly.GetManifestResourceStream(text);
			}
			else
			{
				stream = base.GetImageDataStream(phone);
			}
			return stream;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002FBC File Offset: 0x000011BC
		protected override Stream GetManufacturerImageDataStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("blulogo.png"));
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

		// Token: 0x06000024 RID: 36 RVA: 0x0000301D File Offset: 0x0000121D
		private void MsrDownloadProgressEvent(ProgressChangedEventArgs progressArgs)
		{
			base.RaiseProgressPercentageChanged(progressArgs.Percentage, progressArgs.Message, progressArgs.DownloadedSize, progressArgs.TotalSize, progressArgs.BytesPerSecond, progressArgs.SecondsLeft);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000304C File Offset: 0x0000124C
		private IFFUDevice GetFfuDevice(Phone phone)
		{
			Tracer<BluAdaptation>.LogEntry("GetFfuDevice");
			FFUManager.Start();
			IFFUDevice flashableDevice;
			try
			{
				flashableDevice = FFUManager.GetFlashableDevice(phone.Path, false);
			}
			finally
			{
				FFUManager.Stop();
				Tracer<BluAdaptation>.LogExit("GetFfuDevice");
			}
			return flashableDevice;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000030A0 File Offset: 0x000012A0
		private void FlashProgressEvent(object obj, ProgressEventArgs progress)
		{
			double num = (double)progress.Position / (double)progress.Length * 100.0;
			base.RaiseProgressPercentageChanged((int)num, this.progressMessage);
		}

		// Token: 0x04000013 RID: 19
		private readonly MsrService msrService;

		// Token: 0x04000014 RID: 20
		private readonly ReportingService reportingService;

		// Token: 0x04000015 RID: 21
		private readonly FfuFileInfoService ffuFileInfoService;

		// Token: 0x04000016 RID: 22
		private readonly List<Phone> manuallySupportedModels = new List<Phone>();

		// Token: 0x04000017 RID: 23
		private readonly List<Phone> manuallySupportedVariants = new List<Phone>();

		// Token: 0x04000018 RID: 24
		private string progressMessage;
	}
}
