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

namespace Microsoft.WindowsDeviceRecoveryTool.AcerAdaptation.Services
{
	// Token: 0x02000006 RID: 6
	[ExportAdaptation(Type = PhoneTypes.Acer)]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class AcerAdaptation : BaseAdaptation
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002734 File Offset: 0x00000934
		[ImportingConstructor]
		public AcerAdaptation(MsrService msrService, ReportingService reportingService)
		{
			this.msrService = msrService;
			this.msrService.ProgressChanged += this.MsrDownloadProgressEvent;
			this.reportingService = reportingService;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002782 File Offset: 0x00000982
		public override PhoneTypes PhoneType
		{
			get
			{
				return PhoneTypes.Acer;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002785 File Offset: 0x00000985
		public override string ReportManufacturerName
		{
			get
			{
				return "AcerInc";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000278C File Offset: 0x0000098C
		public override string ManufacturerName
		{
			get
			{
				return "Acer";
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002793 File Offset: 0x00000993
		public override bool RecoverySupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002796 File Offset: 0x00000996
		public override string PackageExtension
		{
			get
			{
				return "ffu";
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000279D File Offset: 0x0000099D
		public override List<PackageFileInfo> FindPackage(string directory, Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000279D File Offset: 0x0000099D
		public override List<PackageFileInfo> FindAllPackages(string directory, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000027A4 File Offset: 0x000009A4
		public override PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken)
		{
			PackageFileInfo result;
			try
			{
				Tracer<AcerAdaptation>.LogEntry("CheckLatestPackage");
				if (phone.QueryParameters == null)
				{
					throw new ArgumentException("Package query parameter not set.");
				}
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
				Tracer<AcerAdaptation>.LogExit("CheckLatestPackage");
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000279D File Offset: 0x0000099D
		public override void CheckPackageIntegrity(Phone phone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002864 File Offset: 0x00000A64
		public override void FlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<AcerAdaptation>.LogEntry("FlashDevice");
			IFFUDevice ffuDevice = this.GetFfuDevice(phone);
			if (ffuDevice != null)
			{
				ffuDevice.ProgressEvent += this.FlashProgressEvent;
				try
				{
					this.progressMessage = "SoftwareInstallation";
					ffuDevice.FlashFFUFile(phone.PackageFiles.Where((string f) => f.EndsWith(".ffu", StringComparison.OrdinalIgnoreCase)).First<string>(), true);
				}
				finally
				{
					ffuDevice.ProgressEvent -= this.FlashProgressEvent;
				}
			}
			Tracer<AcerAdaptation>.LogExit("FlashDevice");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002908 File Offset: 0x00000B08
		public override bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken cancellationToken)
		{
			bool flag;
			using (IFFUDevice ffuDevice = this.GetFfuDevice(phone))
			{
				flag = ffuDevice != null;
			}
			return flag;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002940 File Offset: 0x00000B40
		public override void DownloadPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<AcerAdaptation>.LogEntry("DownloadPackage");
			base.RaiseProgressPercentageChanged(0, null);
			try
			{
				this.reportingService.OperationStarted(phone, ReportOperationType.DownloadPackage);
				DownloadParameters downloadParameters = new DownloadParameters
				{
					DiscoveryParameters = phone.QueryParameters,
					DestinationFolder = ((!string.IsNullOrEmpty(phone.QueryParameters.ManufacturerHardwareModel)) ? Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetAcerProductsPath(phone.QueryParameters.ManufacturerHardwareModel) : Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetAcerProductsPath(phone.QueryParameters.ManufacturerHardwareVariant)),
					FilesVersioned = true
				};
				Tracer<AcerAdaptation>.WriteInformation("Download Params: {0}", new object[] { downloadParameters });
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
				Tracer<AcerAdaptation>.WriteError(ex);
				if (flag)
				{
					throw;
				}
			}
			finally
			{
				Tracer<AcerAdaptation>.LogExit("DownloadPackage");
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000279D File Offset: 0x0000099D
		public override void DownloadEmergencyPackage(Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public override SwVersionComparisonResult CompareFirmwareVersions(Phone phone)
		{
			return SwVersionComparisonResult.UnableToCompare;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000279D File Offset: 0x0000099D
		public override void ReadDeviceInfo(Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002AD4 File Offset: 0x00000CD4
		protected override void FillSupportedDeviceIdentifiers()
		{
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("045E", "F0CA"));
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("0502", "37A3"));
			this.SupportedFlashModeIds.Add(new DeviceIdentifier("045E", "062A"));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002B30 File Offset: 0x00000D30
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

		// Token: 0x06000024 RID: 36 RVA: 0x00002B72 File Offset: 0x00000D72
		private Phone GetPhone(ModelInfo modelInfo)
		{
			return new Phone
			{
				Type = this.PhoneType,
				SalesName = modelInfo.Name,
				HardwareModel = modelInfo.Name,
				ImageData = modelInfo.Bitmap.ToBytes()
			};
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002BB0 File Offset: 0x00000DB0
		protected override void InitializeManuallySupportedModels()
		{
			var array = new <>f__AnonymousType0<ModelInfo, string>[]
			{
				new
				{
					Model = AcerModels.LiquidM220,
					IdentificationInstruction = LocalizationManager.GetTranslation("ModelIdentificationAcerM220")
				},
				new
				{
					Model = AcerModels.JadePrimo,
					IdentificationInstruction = null
				},
				new
				{
					Model = AcerModels.LiquidM330,
					IdentificationInstruction = LocalizationManager.GetTranslation("ModelIdentificationAcerM330")
				}
			};
			for (int i = 0; i < array.Length; i++)
			{
				var <>f__AnonymousType = array[i];
				ModelInfo model = <>f__AnonymousType.Model;
				Phone phone = this.GetPhone(<>f__AnonymousType.Model);
				phone.ModelIdentificationInstruction = <>f__AnonymousType.IdentificationInstruction;
				this.manuallySupportedModels.Add(phone);
				if (model.Variants.Length == 1)
				{
					phone.QueryParameters = model.Variants[0].MsrQueryParameters;
				}
				else
				{
					Phone[] phoneVariants = this.GetPhoneVariants(model);
					this.manuallySupportedVariants.AddRange(phoneVariants);
				}
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002C78 File Offset: 0x00000E78
		public override List<Phone> ManuallySupportedModels()
		{
			return this.manuallySupportedModels;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002C80 File Offset: 0x00000E80
		public override List<Phone> ManuallySupportedVariants(Phone phone)
		{
			return this.manuallySupportedVariants.Where((Phone variant) => variant.HardwareModel.Equals(phone.HardwareModel, StringComparison.OrdinalIgnoreCase)).ToList<Phone>();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002CB8 File Offset: 0x00000EB8
		protected override Stream GetImageDataStream(Phone phone)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = string.Empty;
			if (phone.SalesName.ToLower().Contains("m220"))
			{
				text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("M220.png"));
			}
			else if (phone.SalesName.ToLower().Contains("jade primo"))
			{
				text = manifestResourceNames.FirstOrDefault((string resourcesName) => resourcesName.Contains("JadePrimo.png"));
			}
			else if (phone.SalesName.ToLower().Contains("m330"))
			{
				text = manifestResourceNames.FirstOrDefault((string resourcesName) => resourcesName.Contains("M330.png"));
			}
			if (!string.IsNullOrEmpty(text))
			{
				return executingAssembly.GetManifestResourceStream(text);
			}
			return base.GetImageDataStream(phone);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002DAC File Offset: 0x00000FAC
		protected override Stream GetManufacturerImageDataStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().FirstOrDefault((string resourceName) => resourceName.Contains("AcerLogo.png"));
			if (!string.IsNullOrEmpty(text))
			{
				return executingAssembly.GetManifestResourceStream(text);
			}
			return null;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002DFB File Offset: 0x00000FFB
		private void MsrDownloadProgressEvent(ProgressChangedEventArgs progressArgs)
		{
			base.RaiseProgressPercentageChanged(progressArgs.Percentage, progressArgs.Message, progressArgs.DownloadedSize, progressArgs.TotalSize, progressArgs.BytesPerSecond, progressArgs.SecondsLeft);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002E28 File Offset: 0x00001028
		private IFFUDevice GetFfuDevice(Phone phone)
		{
			Tracer<AcerAdaptation>.LogEntry("GetFfuDevice");
			FFUManager.Start();
			IFFUDevice flashableDevice;
			try
			{
				flashableDevice = FFUManager.GetFlashableDevice(phone.Path, false);
			}
			finally
			{
				FFUManager.Stop();
				Tracer<AcerAdaptation>.LogExit("GetFfuDevice");
			}
			return flashableDevice;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002E74 File Offset: 0x00001074
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
		private readonly List<Phone> manuallySupportedModels = new List<Phone>();

		// Token: 0x04000016 RID: 22
		private readonly List<Phone> manuallySupportedVariants = new List<Phone>();

		// Token: 0x04000017 RID: 23
		private string progressMessage;
	}
}
