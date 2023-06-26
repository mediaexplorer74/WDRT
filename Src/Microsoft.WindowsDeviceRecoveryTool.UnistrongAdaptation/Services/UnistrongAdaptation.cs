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

namespace Microsoft.WindowsDeviceRecoveryTool.UnistrongAdaptation.Services
{
	// Token: 0x02000005 RID: 5
	[ExportAdaptation(Type = PhoneTypes.Unistrong)]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class UnistrongAdaptation : BaseAdaptation
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021E0 File Offset: 0x000003E0
		[ImportingConstructor]
		public UnistrongAdaptation(FfuFileInfoService ffuFileInfoService, MsrService msrService, ReportingService reportingService)
		{
			this.reportingService = reportingService;
			this.ffuFileInfoService = ffuFileInfoService;
			this.msrService = msrService;
			this.msrService.ProgressChanged += this.MsrDownloadProgressEvent;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000221F File Offset: 0x0000041F
		public override string PackageExtension
		{
			get
			{
				return "ffu";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002226 File Offset: 0x00000426
		public override PhoneTypes PhoneType
		{
			get
			{
				return PhoneTypes.Unistrong;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000222A File Offset: 0x0000042A
		public override bool RecoverySupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000222D File Offset: 0x0000042D
		public override string ReportManufacturerName
		{
			get
			{
				return "Unistrong";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000222D File Offset: 0x0000042D
		public override string ManufacturerName
		{
			get
			{
				return "Unistrong";
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002234 File Offset: 0x00000434
		public override PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken)
		{
			PackageFileInfo result;
			try
			{
				Tracer<UnistrongAdaptation>.LogEntry("CheckLatestPackage");
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
				Tracer<UnistrongAdaptation>.LogExit("CheckLatestPackage");
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022E0 File Offset: 0x000004E0
		public override void CheckPackageIntegrity(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<UnistrongAdaptation>.LogEntry("CheckPackageIntegrity");
			this.ffuFileInfoService.ReadFfuFile(phone.PackageFilePath);
			Tracer<UnistrongAdaptation>.LogExit("CheckPackageIntegrity");
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002307 File Offset: 0x00000507
		public override SwVersionComparisonResult CompareFirmwareVersions(Phone phone)
		{
			return SwVersionComparisonResult.UnableToCompare;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000230A File Offset: 0x0000050A
		public override void DownloadEmergencyPackage(Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002314 File Offset: 0x00000514
		public override void DownloadPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<UnistrongAdaptation>.LogEntry("DownloadPackage");
			base.RaiseProgressPercentageChanged(0, null);
			try
			{
				this.reportingService.OperationStarted(phone, ReportOperationType.DownloadPackage);
				DownloadParameters downloadParameters = new DownloadParameters
				{
					DiscoveryParameters = phone.QueryParameters,
					DestinationFolder = ((!string.IsNullOrEmpty(phone.QueryParameters.ManufacturerHardwareModel)) ? Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetUnistrongProductsPath(phone.QueryParameters.ManufacturerHardwareModel) : Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetUnistrongProductsPath(phone.QueryParameters.ManufacturerHardwareVariant)),
					FilesVersioned = true
				};
				Tracer<UnistrongAdaptation>.WriteInformation("Download Params: {0}", new object[] { downloadParameters });
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
				Tracer<UnistrongAdaptation>.WriteError(ex);
				if (flag)
				{
					throw;
				}
			}
			finally
			{
				Tracer<UnistrongAdaptation>.LogExit("DownloadPackage");
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000230A File Offset: 0x0000050A
		public override List<PackageFileInfo> FindAllPackages(string directory, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000230A File Offset: 0x0000050A
		public override List<PackageFileInfo> FindPackage(string directory, Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024A4 File Offset: 0x000006A4
		public override void FlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<UnistrongAdaptation>.LogEntry("FlashDevice");
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
			Tracer<UnistrongAdaptation>.LogExit("FlashDevice");
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002524 File Offset: 0x00000724
		private void FlashProgressEvent(object obj, ProgressEventArgs progress)
		{
			double num = (double)progress.Position / (double)progress.Length * 100.0;
			base.RaiseProgressPercentageChanged((int)num, this.progressMessage);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002559 File Offset: 0x00000759
		public override bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken cancellationToken)
		{
			return this.GetFfuDevice(phone) != null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002568 File Offset: 0x00000768
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
				Tracer<UnistrongAdaptation>.WriteError(ex);
				throw new ReadPhoneInformationException(ex.Message, ex);
			}
			throw new ReadPhoneInformationException("Cannot find selected device!");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025F4 File Offset: 0x000007F4
		protected override void FillSupportedDeviceIdentifiers()
		{
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("045E", "F0CA"));
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("05C6", "9093"));
			this.SupportedFlashModeIds.Add(new DeviceIdentifier("045E", "062A"));
			this.SupportedFlashModeIds.Add(new DeviceIdentifier("05C6", "9093"));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000266C File Offset: 0x0000086C
		protected override void InitializeManuallySupportedModels()
		{
			Phone phone = new Phone
			{
				Type = this.PhoneType,
				SalesName = "T536",
				HardwareModel = "T536"
			};
			phone.ImageData = base.GetImageData(phone);
			phone.QueryParameters = new QueryParameters
			{
				ManufacturerName = "Unistrong",
				ManufacturerHardwareModel = "T536",
				ManufacturerModelName = "EE7736A2",
				ManufacturerHardwareVariant = "EE7736A2"
			};
			this.manuallySupportedModels.Add(phone);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026F1 File Offset: 0x000008F1
		public override List<Phone> ManuallySupportedModels()
		{
			return this.manuallySupportedModels;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026FC File Offset: 0x000008FC
		protected override Stream GetImageDataStream(Phone phone)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().FirstOrDefault((string resourceName) => resourceName.Contains("DevicePicture_7739.jpg"));
			if (!string.IsNullOrEmpty(text))
			{
				return executingAssembly.GetManifestResourceStream(text);
			}
			return base.GetImageDataStream(phone);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002754 File Offset: 0x00000954
		protected override Stream GetManufacturerImageDataStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().FirstOrDefault((string resourceName) => resourceName.Contains("Unistrong_Logo.png"));
			if (!string.IsNullOrEmpty(text))
			{
				return executingAssembly.GetManifestResourceStream(text);
			}
			return null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000027A3 File Offset: 0x000009A3
		private void MsrDownloadProgressEvent(ProgressChangedEventArgs progressArgs)
		{
			base.RaiseProgressPercentageChanged(progressArgs.Percentage, progressArgs.Message, progressArgs.DownloadedSize, progressArgs.TotalSize, progressArgs.BytesPerSecond, progressArgs.SecondsLeft);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027D0 File Offset: 0x000009D0
		private IFFUDevice GetFfuDevice(Phone phone)
		{
			Tracer<UnistrongAdaptation>.LogEntry("GetFfuDevice");
			FFUManager.Start();
			IFFUDevice flashableDevice;
			try
			{
				flashableDevice = FFUManager.GetFlashableDevice(phone.Path, false);
			}
			finally
			{
				FFUManager.Stop();
				Tracer<UnistrongAdaptation>.LogExit("GetFfuDevice");
			}
			return flashableDevice;
		}

		// Token: 0x04000007 RID: 7
		private readonly MsrService msrService;

		// Token: 0x04000008 RID: 8
		private readonly ReportingService reportingService;

		// Token: 0x04000009 RID: 9
		private readonly FfuFileInfoService ffuFileInfoService;

		// Token: 0x0400000A RID: 10
		private readonly List<Phone> manuallySupportedModels = new List<Phone>();

		// Token: 0x0400000B RID: 11
		private string progressMessage;
	}
}
