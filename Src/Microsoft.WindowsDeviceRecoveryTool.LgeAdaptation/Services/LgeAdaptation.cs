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

namespace Microsoft.WindowsDeviceRecoveryTool.LgeAdaptation.Services
{
	// Token: 0x02000004 RID: 4
	[ExportAdaptation(Type = PhoneTypes.Lg)]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class LgeAdaptation : BaseAdaptation
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000216C File Offset: 0x0000036C
		[ImportingConstructor]
		public LgeAdaptation(FfuFileInfoService ffuFileInfoService, MsrService msrService, ReportingService reportingService)
		{
			this.salesNameProvider = new SalesNameProvider();
			this.reportingService = reportingService;
			this.ffuFileInfoService = ffuFileInfoService;
			this.msrService = msrService;
			this.msrService.ProgressChanged += this.MsrDownloadProgressEvent;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021BC File Offset: 0x000003BC
		public override PhoneTypes PhoneType
		{
			get
			{
				return PhoneTypes.Lg;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021D0 File Offset: 0x000003D0
		public override bool RecoverySupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021E4 File Offset: 0x000003E4
		public override string ManufacturerName
		{
			get
			{
				return "LG";
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021FC File Offset: 0x000003FC
		public override string ReportManufacturerName
		{
			get
			{
				return "LGE";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002214 File Offset: 0x00000414
		public override string PackageExtension
		{
			get
			{
				return "ffu";
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000222C File Offset: 0x0000042C
		public override ISalesNameProvider SalesNameProvider()
		{
			return this.salesNameProvider;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002244 File Offset: 0x00000444
		public override List<PackageFileInfo> FindPackage(string directory, Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002244 File Offset: 0x00000444
		public override List<PackageFileInfo> FindAllPackages(string directory, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000224C File Offset: 0x0000044C
		public override PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken)
		{
			PackageFileInfo result;
			try
			{
				Tracer<LgeAdaptation>.LogEntry("CheckLatestPackage");
				Task<PackageFileInfo> task = this.msrService.CheckLatestPackage(this.DeviceQueryParameters(phone), cancellationToken);
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
				Tracer<LgeAdaptation>.LogExit("CheckLatestPackage");
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002324 File Offset: 0x00000524
		public override void CheckPackageIntegrity(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<LgeAdaptation>.LogEntry("CheckPackageIntegrity");
			this.ffuFileInfoService.ReadFfuFile(phone.PackageFilePath);
			Tracer<LgeAdaptation>.LogExit("CheckPackageIntegrity");
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002350 File Offset: 0x00000550
		public override void FlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<LgeAdaptation>.LogEntry("FlashDevice");
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
			Tracer<LgeAdaptation>.LogExit("FlashDevice");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023E4 File Offset: 0x000005E4
		private void FlashProgressEvent(object obj, ProgressEventArgs progress)
		{
			double num = (double)progress.Position / (double)progress.Length * 100.0;
			base.RaiseProgressPercentageChanged((int)num, this.progressMessage);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000241C File Offset: 0x0000061C
		public override bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken cancellationToken)
		{
			return this.GetFfuDevice(phone) != null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002438 File Offset: 0x00000638
		public override void DownloadPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<LgeAdaptation>.LogEntry("DownloadPackage");
			base.RaiseProgressPercentageChanged(0, null);
			try
			{
				this.reportingService.OperationStarted(phone, ReportOperationType.DownloadPackage);
				DownloadParameters downloadParameters = new DownloadParameters
				{
					DiscoveryParameters = this.DeviceQueryParameters(phone),
					DestinationFolder = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetLgeProductsPath(phone.PlatformId.ProductName),
					FilesVersioned = true
				};
				Tracer<LgeAdaptation>.WriteInformation("Download Params: {0}", new object[] { downloadParameters });
				phone.PackageFiles = this.msrService.DownloadLatestPackage(downloadParameters, cancellationToken);
				Tuple<long, long, bool> downloadPackageInformation = this.msrService.GetDownloadPackageInformation();
				this.reportingService.SetDownloadByteInformation(phone, ReportOperationType.DownloadPackage, downloadPackageInformation.Item1, downloadPackageInformation.Item2, downloadPackageInformation.Item3);
				this.reportingService.SetDownloadByteInformation(phone, ReportOperationType.DownloadPackage, downloadPackageInformation.Item1, downloadPackageInformation.Item2, downloadPackageInformation.Item3);
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
				Tracer<LgeAdaptation>.WriteError(ex);
				bool flag3 = flag;
				if (flag3)
				{
					throw;
				}
			}
			finally
			{
				Tracer<LgeAdaptation>.LogExit("DownloadPackage");
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002244 File Offset: 0x00000444
		public override void DownloadEmergencyPackage(Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025E4 File Offset: 0x000007E4
		public override SwVersionComparisonResult CompareFirmwareVersions(Phone phone)
		{
			return SwVersionComparisonResult.UnableToCompare;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025F8 File Offset: 0x000007F8
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
				Tracer<LgeAdaptation>.WriteError(ex);
				throw new ReadPhoneInformationException(ex.Message, ex);
			}
			throw new ReadPhoneInformationException("Cannot find selected device!");
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002694 File Offset: 0x00000894
		protected override void FillSupportedDeviceIdentifiers()
		{
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("1004", "627E"));
			this.SupportedFlashModeIds.Add(new DeviceIdentifier("045E", "062A"));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026D0 File Offset: 0x000008D0
		private QueryParameters DeviceQueryParameters(Phone phone)
		{
			return new QueryParameters
			{
				ManufacturerName = "LGE",
				ManufacturerHardwareModel = phone.PlatformId.ProductName,
				PackageType = "Firmware"
			};
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002711 File Offset: 0x00000911
		private void MsrDownloadProgressEvent(ProgressChangedEventArgs progressArgs)
		{
			base.RaiseProgressPercentageChanged(progressArgs.Percentage, progressArgs.Message, progressArgs.DownloadedSize, progressArgs.TotalSize, progressArgs.BytesPerSecond, progressArgs.SecondsLeft);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002740 File Offset: 0x00000940
		private IFFUDevice GetFfuDevice(Phone phone)
		{
			Tracer<LgeAdaptation>.LogEntry("GetFfuDevice");
			FFUManager.Start();
			IFFUDevice flashableDevice;
			try
			{
				flashableDevice = FFUManager.GetFlashableDevice(phone.Path, false);
			}
			finally
			{
				FFUManager.Stop();
				Tracer<LgeAdaptation>.LogExit("GetFfuDevice");
			}
			return flashableDevice;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002794 File Offset: 0x00000994
		protected override Stream GetImageDataStream(Phone phone)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("Lancet.png"));
			bool flag = !string.IsNullOrEmpty(text);
			Stream stream;
			if (flag)
			{
				stream = executingAssembly.GetManifestResourceStream(text);
			}
			else
			{
				stream = base.GetImageDataStream(phone);
			}
			return stream;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027FC File Offset: 0x000009FC
		protected override Stream GetManufacturerImageDataStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("LgLogo.png"));
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

		// Token: 0x04000004 RID: 4
		private readonly MsrService msrService;

		// Token: 0x04000005 RID: 5
		private readonly ReportingService reportingService;

		// Token: 0x04000006 RID: 6
		private readonly FfuFileInfoService ffuFileInfoService;

		// Token: 0x04000007 RID: 7
		private readonly SalesNameProvider salesNameProvider;

		// Token: 0x04000008 RID: 8
		private string progressMessage;
	}
}
