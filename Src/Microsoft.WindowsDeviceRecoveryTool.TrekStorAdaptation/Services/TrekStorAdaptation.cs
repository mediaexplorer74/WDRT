﻿using System;
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

namespace Microsoft.WindowsDeviceRecoveryTool.TrekStorAdaptation.Services
{
	// Token: 0x02000005 RID: 5
	[ExportAdaptation(Type = PhoneTypes.TrekStor)]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class TrekStorAdaptation : BaseAdaptation
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021D8 File Offset: 0x000003D8
		[ImportingConstructor]
		public TrekStorAdaptation(FfuFileInfoService ffuFileInfoService, MsrService msrService, ReportingService reportingService)
		{
			this.reportingService = reportingService;
			this.ffuFileInfoService = ffuFileInfoService;
			this.msrService = msrService;
			this.msrService.ProgressChanged += this.MsrDownloadProgressEvent;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000222D File Offset: 0x0000042D
		public override string PackageExtension
		{
			get
			{
				return "ffu";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002234 File Offset: 0x00000434
		public override PhoneTypes PhoneType
		{
			get
			{
				return PhoneTypes.TrekStor;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002238 File Offset: 0x00000438
		public override bool RecoverySupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000223B File Offset: 0x0000043B
		public override string ReportManufacturerName
		{
			get
			{
				return "TREKSTOR";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000223B File Offset: 0x0000043B
		public override string ManufacturerName
		{
			get
			{
				return "TREKSTOR";
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002244 File Offset: 0x00000444
		public override PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken)
		{
			PackageFileInfo result;
			try
			{
				Tracer<TrekStorAdaptation>.LogEntry("CheckLatestPackage");
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
				Tracer<TrekStorAdaptation>.LogExit("CheckLatestPackage");
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022F0 File Offset: 0x000004F0
		public override void CheckPackageIntegrity(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<TrekStorAdaptation>.LogEntry("CheckPackageIntegrity");
			this.ffuFileInfoService.ReadFfuFile(phone.PackageFilePath);
			Tracer<TrekStorAdaptation>.LogExit("CheckPackageIntegrity");
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002317 File Offset: 0x00000517
		public override SwVersionComparisonResult CompareFirmwareVersions(Phone phone)
		{
			return SwVersionComparisonResult.UnableToCompare;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000231A File Offset: 0x0000051A
		public override void DownloadEmergencyPackage(Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002324 File Offset: 0x00000524
		public override void DownloadPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<TrekStorAdaptation>.LogEntry("DownloadPackage");
			base.RaiseProgressPercentageChanged(0, null);
			try
			{
				this.reportingService.OperationStarted(phone, ReportOperationType.DownloadPackage);
				DownloadParameters downloadParameters = new DownloadParameters
				{
					DiscoveryParameters = phone.QueryParameters,
					DestinationFolder = ((!string.IsNullOrEmpty(phone.QueryParameters.ManufacturerHardwareModel)) ? Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetTrekStorProductsPath(phone.QueryParameters.ManufacturerHardwareModel) : Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetTrekStorProductsPath(phone.QueryParameters.ManufacturerHardwareVariant)),
					FilesVersioned = true
				};
				Tracer<TrekStorAdaptation>.WriteInformation("Download Params: {0}", new object[] { downloadParameters });
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
				Tracer<TrekStorAdaptation>.WriteError(ex);
				if (flag)
				{
					throw;
				}
			}
			finally
			{
				Tracer<TrekStorAdaptation>.LogExit("DownloadPackage");
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000231A File Offset: 0x0000051A
		public override List<PackageFileInfo> FindAllPackages(string directory, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000231A File Offset: 0x0000051A
		public override List<PackageFileInfo> FindPackage(string directory, Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024B4 File Offset: 0x000006B4
		public override void FlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<TrekStorAdaptation>.LogEntry("FlashDevice");
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
			Tracer<TrekStorAdaptation>.LogExit("FlashDevice");
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002534 File Offset: 0x00000734
		private void FlashProgressEvent(object obj, ProgressEventArgs progress)
		{
			double num = (double)progress.Position / (double)progress.Length * 100.0;
			base.RaiseProgressPercentageChanged((int)num, this.progressMessage);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002569 File Offset: 0x00000769
		public override bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken cancellationToken)
		{
			return this.GetFfuDevice(phone) != null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002578 File Offset: 0x00000778
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
				Tracer<TrekStorAdaptation>.WriteError(ex);
				throw new ReadPhoneInformationException(ex.Message, ex);
			}
			throw new ReadPhoneInformationException("Cannot find selected device!");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002604 File Offset: 0x00000804
		protected override void FillSupportedDeviceIdentifiers()
		{
			this.SupportedNormalModeIds.Add(new DeviceIdentifier("045E", "F0CA"));
			this.SupportedFlashModeIds.Add(new DeviceIdentifier("045E", "062A"));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000263C File Offset: 0x0000083C
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

		// Token: 0x0600001C RID: 28 RVA: 0x00002694 File Offset: 0x00000894
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

		// Token: 0x0600001D RID: 29 RVA: 0x000026D8 File Offset: 0x000008D8
		protected override void InitializeManuallySupportedModels()
		{
			foreach (ModelInfo modelInfo in new ModelInfo[] { TrekStorModels.TrekStor_Winphone_5_0_LTE })
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

		// Token: 0x0600001E RID: 30 RVA: 0x00002750 File Offset: 0x00000950
		public override List<Phone> ManuallySupportedModels()
		{
			return this.manuallySupportedModels;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002758 File Offset: 0x00000958
		public override List<Phone> ManuallySupportedVariants(Phone phone)
		{
			return this.manuallySupportedVariants.Where((Phone variant) => string.Equals(variant.HardwareModel, phone.HardwareModel, StringComparison.OrdinalIgnoreCase)).ToList<Phone>();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002790 File Offset: 0x00000990
		protected override Stream GetImageDataStream(Phone phone)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = string.Empty;
			if (phone.SalesName.ToLower().Contains("winphone"))
			{
				text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("WinPhone 4.7_HD_wp47.jpg"));
			}
			if (!string.IsNullOrEmpty(text))
			{
				return executingAssembly.GetManifestResourceStream(text);
			}
			return base.GetImageDataStream(phone);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002804 File Offset: 0x00000A04
		protected override Stream GetManufacturerImageDataStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().FirstOrDefault((string resourceName) => resourceName.Contains("Trekstor_logo.png"));
			if (!string.IsNullOrEmpty(text))
			{
				return executingAssembly.GetManifestResourceStream(text);
			}
			return null;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002853 File Offset: 0x00000A53
		private void MsrDownloadProgressEvent(ProgressChangedEventArgs progressArgs)
		{
			base.RaiseProgressPercentageChanged(progressArgs.Percentage, progressArgs.Message, progressArgs.DownloadedSize, progressArgs.TotalSize, progressArgs.BytesPerSecond, progressArgs.SecondsLeft);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002880 File Offset: 0x00000A80
		private IFFUDevice GetFfuDevice(Phone phone)
		{
			Tracer<TrekStorAdaptation>.LogEntry("GetFfuDevice");
			FFUManager.Start();
			IFFUDevice flashableDevice;
			try
			{
				flashableDevice = FFUManager.GetFlashableDevice(phone.Path, false);
			}
			finally
			{
				FFUManager.Stop();
				Tracer<TrekStorAdaptation>.LogExit("GetFfuDevice");
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
		private readonly List<Phone> manuallySupportedVariants = new List<Phone>();

		// Token: 0x0400000C RID: 12
		private string progressMessage;
	}
}
