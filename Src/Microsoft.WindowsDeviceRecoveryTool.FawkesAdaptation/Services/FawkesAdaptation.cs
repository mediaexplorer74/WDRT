using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Contracts;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;

namespace Microsoft.WindowsDeviceRecoveryTool.FawkesAdaptation.Services
{
	// Token: 0x02000006 RID: 6
	[ExportAdaptation(Type = PhoneTypes.HoloLensAccessory)]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class FawkesAdaptation : BaseAdaptation
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002258 File Offset: 0x00000458
		[ImportingConstructor]
		public FawkesAdaptation(MsrService msrService, ReportingService reportingService, Md5Sevice md5Sevice)
		{
			this.msrService = msrService;
			this.reportingService = reportingService;
			this.md5Sevice = md5Sevice;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002275 File Offset: 0x00000475
		public override PhoneTypes PhoneType
		{
			get
			{
				return PhoneTypes.HoloLensAccessory;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002278 File Offset: 0x00000478
		public override bool RecoverySupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000227B File Offset: 0x0000047B
		public override string ManufacturerName
		{
			get
			{
				return "Microsoft Hololens Clicker";
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002282 File Offset: 0x00000482
		public override string ReportManufacturerName
		{
			get
			{
				return "Microsoft";
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002289 File Offset: 0x00000489
		public override string ReportManufacturerProductLine
		{
			get
			{
				return "Hololens Clicker";
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002290 File Offset: 0x00000490
		public override string PackageExtension
		{
			get
			{
				return "bin";
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002298 File Offset: 0x00000498
		public override List<PackageFileInfo> FindPackage(string directory, Phone currentPhone, CancellationToken cancellationToken)
		{
			Tracer<FawkesAdaptation>.LogEntry("FindPackage");
			List<PackageFileInfo> list = new List<PackageFileInfo>();
			Tracer<FawkesAdaptation>.WriteVerbose("Device platform id: {0}", new object[] { currentPhone.PlatformId });
			if (currentPhone.PlatformId == null)
			{
				return list;
			}
			try
			{
				foreach (string text in Directory.GetFiles(directory, string.Format("*.{0}", this.PackageExtension), SearchOption.AllDirectories))
				{
					if (cancellationToken.IsCancellationRequested)
					{
						return null;
					}
				}
				if (cancellationToken.IsCancellationRequested)
				{
					return null;
				}
			}
			catch (DirectoryNotFoundException ex)
			{
				Tracer<FawkesAdaptation>.WriteError(ex);
			}
			finally
			{
				Tracer<FawkesAdaptation>.LogExit("FindPackage");
			}
			return list;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002350 File Offset: 0x00000550
		public override List<PackageFileInfo> FindAllPackages(string directory, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002358 File Offset: 0x00000558
		public override PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken)
		{
			PackageFileInfo packageFileInfo2;
			try
			{
				Tracer<FawkesAdaptation>.LogEntry("CheckLatestPackage");
				Task<PackageFileInfo> task = this.msrService.CheckLatestPackage(this.DeviceQueryParameters(phone), cancellationToken);
				task.Wait(cancellationToken);
				PackageFileInfo packageFileInfo = task.Result;
				MsrPackageInfo msrPackageInfo = packageFileInfo as MsrPackageInfo;
				if (msrPackageInfo != null && msrPackageInfo.PackageFileData != null)
				{
					MsrPackageInfo.MsrFileInfo msrFileInfo = msrPackageInfo.PackageFileData.FirstOrDefault((MsrPackageInfo.MsrFileInfo f) => string.Equals(f.FileType, "APP", StringComparison.InvariantCultureIgnoreCase));
					if (msrFileInfo == null && msrPackageInfo.PackageFileData.Count<MsrPackageInfo.MsrFileInfo>() == 1)
					{
						msrFileInfo = msrPackageInfo.PackageFileData.Single<MsrPackageInfo.MsrFileInfo>();
					}
					if (msrFileInfo != null)
					{
						packageFileInfo = new MsrPackageInfo(msrPackageInfo.PackageId, msrPackageInfo.Name, msrFileInfo.FileVersion)
						{
							PackageFileData = msrPackageInfo.PackageFileData
						};
					}
				}
				packageFileInfo2 = packageFileInfo;
			}
			catch (Exception ex)
			{
				if (ex.InnerException is PackageNotFoundException)
				{
					Tracer<FawkesAdaptation>.WriteWarning("Package not found", new object[0]);
					throw ex.InnerException;
				}
				if (ex.InnerException != null && ex.InnerException.GetBaseException() is WebException)
				{
					Tracer<FawkesAdaptation>.WriteWarning("Web connection error: {0}", new object[] { ex });
					throw new WebException();
				}
				if (ex is OperationCanceledException || ex.InnerException is TaskCanceledException)
				{
					Tracer<FawkesAdaptation>.WriteInformation("Package search cancelled");
					throw;
				}
				throw;
			}
			finally
			{
				Tracer<FawkesAdaptation>.LogExit("CheckLatestPackage");
			}
			return packageFileInfo2;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024DC File Offset: 0x000006DC
		protected override Stream GetImageDataStream(Phone phone)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().FirstOrDefault((string resourceName) => resourceName.Contains("FawkesTile.png"));
			if (!string.IsNullOrEmpty(text))
			{
				return executingAssembly.GetManifestResourceStream(text);
			}
			return null;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000252C File Offset: 0x0000072C
		protected override Stream GetManufacturerImageDataStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().FirstOrDefault((string resourceName) => resourceName.Contains("FawkesTile.png"));
			if (!string.IsNullOrEmpty(text))
			{
				return executingAssembly.GetManifestResourceStream(text);
			}
			return null;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000257B File Offset: 0x0000077B
		public override void CheckPackageIntegrity(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<FawkesAdaptation>.LogEntry("CheckPackageIntegrity");
			Tracer<FawkesAdaptation>.WriteWarning("NOT IMPLEMENTED!!!", new object[0]);
			Tracer<FawkesAdaptation>.LogExit("CheckPackageIntegrity");
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025A4 File Offset: 0x000007A4
		public override void FlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<FawkesAdaptation>.LogEntry("FlashDevice");
			if (!this.CheckIfDeviceStillConnected(phone))
			{
				throw new DeviceNotFoundException();
			}
			FawkesProgress fawkesProgress = new FawkesProgress(delegate(FawkesProgressData progressData)
			{
				Tracer<FawkesAdaptation>.WriteInformation("Fawkes flashing progress data received: {0}", new object[] { progressData.Value });
				base.RaiseProgressPercentageChanged((progressData.Value != null) ? ((int)progressData.Value.Value) : (-1), progressData.Message);
			});
			FawkesFlasher.FlashDevice(phone, fawkesProgress, cancellationToken);
			Tracer<FawkesAdaptation>.LogExit("FlashDevice");
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002278 File Offset: 0x00000478
		public override bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken cancellationToken)
		{
			return true;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025F0 File Offset: 0x000007F0
		public override void DownloadPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<FawkesAdaptation>.LogEntry("DownloadPackage");
			base.RaiseProgressPercentageChanged(0, null);
			try
			{
				this.reportingService.OperationStarted(phone, ReportOperationType.DownloadPackage);
				DownloadParameters downloadParameters = new DownloadParameters
				{
					DiscoveryParameters = this.DeviceQueryParameters(phone),
					DestinationFolder = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetLumiaProductsPath("HoloLensClicker"),
					FilesVersioned = true
				};
				phone.PackageFiles = this.msrService.DownloadLatestPackage(downloadParameters, cancellationToken);
				Tuple<long, long, bool> downloadPackageInformation = this.msrService.GetDownloadPackageInformation();
				this.reportingService.SetDownloadByteInformation(phone, ReportOperationType.DownloadPackage, downloadPackageInformation.Item1, downloadPackageInformation.Item2, downloadPackageInformation.Item3);
				this.reportingService.OperationSucceded(phone, ReportOperationType.DownloadPackage);
			}
			catch (Exception ex)
			{
				if (ex.InnerException is AggregateException && ex.InnerException.InnerException is IOException && (long)ex.InnerException.InnerException.HResult == -2147024784L)
				{
					Tracer<FawkesAdaptation>.WriteInformation("--100: For some reason the exception wasn't thrown until here.");
					throw new NotEnoughSpaceException();
				}
				if ((ex.InnerException != null && ex.InnerException.GetBaseException() is WebException) || (ex.InnerException is AggregateException && ex.InnerException.InnerException.GetBaseException() is WebException))
				{
					Tracer<FawkesAdaptation>.WriteWarning("Web connection error: {0}", new object[] { ex });
					throw new WebException();
				}
				UriData uriDataForException = this.GetUriDataForException(ex);
				Tuple<long, long, bool> downloadPackageInformation2 = this.msrService.GetDownloadPackageInformation();
				this.reportingService.SetDownloadByteInformation(phone, ReportOperationType.DownloadPackage, downloadPackageInformation2.Item1, downloadPackageInformation2.Item2, downloadPackageInformation2.Item3);
				this.reportingService.OperationFailed(phone, ReportOperationType.DownloadPackage, uriDataForException, ex);
				Tracer<FawkesAdaptation>.WriteError(ex);
				throw;
			}
			finally
			{
				Tracer<FawkesAdaptation>.LogExit("DownloadPackage");
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002350 File Offset: 0x00000550
		public override void DownloadEmergencyPackage(Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027C4 File Offset: 0x000009C4
		public override SwVersionComparisonResult CompareFirmwareVersions(Phone phone)
		{
			return VersionComparer.CompareSoftwareVersions(phone.SoftwareVersion, phone.PackageFileInfo.SoftwareVersion, new char[] { '.', '-' });
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027EC File Offset: 0x000009EC
		public override void ReadDeviceInfo(Phone currentPhone, CancellationToken cancellationToken)
		{
			try
			{
				FawkesDeviceInfo fawkesDeviceInfo = FawkesFlasher.ReadDeviceInfoFromNormalMode();
				if (fawkesDeviceInfo != null)
				{
					currentPhone.WriteFawkesDeviceInfo(fawkesDeviceInfo);
					base.RaiseDeviceInfoRead(currentPhone);
					Tracer<FawkesAdaptation>.WriteVerbose("Current phone: {0}", new object[] { currentPhone });
					return;
				}
			}
			catch (Exception ex)
			{
				Tracer<FawkesAdaptation>.WriteError(ex);
				throw new ReadPhoneInformationException(ex.Message, ex);
			}
			throw new ReadPhoneInformationException("Cannot find selected device!");
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002858 File Offset: 0x00000A58
		public override bool CheckIfDeviceStillConnected(Phone phone)
		{
			return FawkesFlasher.IsDeviceConnected();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000285F File Offset: 0x00000A5F
		protected override void FillSupportedDeviceIdentifiers()
		{
			this.FillNormalModeDeviceIdentifiers();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002867 File Offset: 0x00000A67
		private void FillNormalModeDeviceIdentifiers()
		{
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("045E", "0654"));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002884 File Offset: 0x00000A84
		private UriData GetUriDataForException(Exception ex)
		{
			if (ex is OperationCanceledException || ex.GetBaseException() is TaskCanceledException)
			{
				return UriData.DownloadVariantPackageAbortedByUser;
			}
			if (ex is Crc32Exception)
			{
				return UriData.FailedToDownloadVariantPackageCrc32Failed;
			}
			if (ex is NotEnoughSpaceException)
			{
				return UriData.DownloadVariantPackageFilesFailedBecauseOfInsufficientDiskSpace;
			}
			if (ex is PlannedServiceBreakException)
			{
				return UriData.FailedToDownloadVariantPackageFireServiceBreak;
			}
			return UriData.FailedToDownloadVariantPackage;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028D4 File Offset: 0x00000AD4
		private QueryParameters DeviceQueryParameters(Phone phone)
		{
			return new QueryParameters
			{
				ManufacturerName = "Microsoft",
				ManufacturerProductLine = this.ReportManufacturerProductLine,
				PackageType = "Firmware",
				PackageClass = "Public",
				ExtendedAttributes = new Dictionary<string, string> { { "HardwareId", phone.HardwareId } }
			};
		}

		// Token: 0x04000006 RID: 6
		private const long ErrorEmptyDiskSpace = -2147024784L;

		// Token: 0x04000007 RID: 7
		private const long ErrorNotFound = -2146233079L;

		// Token: 0x04000008 RID: 8
		private readonly MsrService msrService;

		// Token: 0x04000009 RID: 9
		private readonly ReportingService reportingService;

		// Token: 0x0400000A RID: 10
		private readonly Md5Sevice md5Sevice;
	}
}
