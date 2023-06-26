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
using Microsoft.Tools.Connectivity;
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
using Nokia.Lucid;
using Nokia.Lucid.DeviceDetection;
using Nokia.Lucid.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.AnalogAdaptation.Services
{
	// Token: 0x02000004 RID: 4
	[ExportAdaptation(Type = PhoneTypes.Analog)]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class AnalogAdaptation : BaseAdaptation
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002224 File Offset: 0x00000424
		[ImportingConstructor]
		public AnalogAdaptation(MsrService msrService, FfuFileInfoService ffuFileInfoService, ReportingService reportingService, FlowConditionService flowCondition)
		{
			this.msrService = msrService;
			this.ffuFileInfoService = ffuFileInfoService;
			this.reportingService = reportingService;
			this.flowCondition = flowCondition;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000224C File Offset: 0x0000044C
		public override PhoneTypes PhoneType
		{
			get
			{
				return PhoneTypes.Analog;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002260 File Offset: 0x00000460
		public override string ReportManufacturerName
		{
			get
			{
				return "Microsoft";
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002278 File Offset: 0x00000478
		public override string ReportManufacturerProductLine
		{
			get
			{
				return "HoloLens";
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002290 File Offset: 0x00000490
		public override string PackageExtension
		{
			get
			{
				return "ffu";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022A8 File Offset: 0x000004A8
		public override bool RecoverySupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022BC File Offset: 0x000004BC
		public override string ManufacturerName
		{
			get
			{
				return "Microsoft HoloLens";
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022D4 File Offset: 0x000004D4
		public override List<PackageFileInfo> FindPackage(string directory, Phone currentPhone, CancellationToken cancellationToken)
		{
			Tracer<AnalogAdaptation>.LogEntry("FindPackage");
			List<PackageFileInfo> list = new List<PackageFileInfo>();
			try
			{
				Tracer<AnalogAdaptation>.WriteVerbose("Device platform id: {0}", new object[] { currentPhone.PlatformId });
				bool flag = currentPhone.PlatformId == null;
				if (flag)
				{
					return list;
				}
				string[] files = Directory.GetFiles(directory, string.Format("*.{0}", this.PackageExtension), SearchOption.AllDirectories);
				foreach (string text in files)
				{
					bool isCancellationRequested = cancellationToken.IsCancellationRequested;
					if (isCancellationRequested)
					{
						return null;
					}
					PlatformId platformId = this.ffuFileInfoService.ReadFfuFilePlatformId(text);
					string text2;
					this.ffuFileInfoService.TryReadFfuSoftwareVersion(text, out text2);
					Tracer<AnalogAdaptation>.WriteVerbose("Package platform id: {0}", new object[] { platformId });
					bool flag2 = platformId.IsCompatibleWithDevicePlatformId(currentPhone.PlatformId);
					if (flag2)
					{
						Tracer<AnalogAdaptation>.WriteVerbose("PlatformIds compatible", new object[0]);
						list.Add(new FfuPackageFileInfo(text, platformId, text2));
					}
					else
					{
						Tracer<AnalogAdaptation>.WriteVerbose("PlatformIds NOT compatible - package skipped", new object[0]);
					}
				}
				bool isCancellationRequested2 = cancellationToken.IsCancellationRequested;
				if (isCancellationRequested2)
				{
					return null;
				}
			}
			catch (DirectoryNotFoundException ex)
			{
				Tracer<AnalogAdaptation>.WriteError(ex);
			}
			finally
			{
				Tracer<AnalogAdaptation>.LogExit("FindPackage");
			}
			return list;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002464 File Offset: 0x00000664
		public override List<PackageFileInfo> FindAllPackages(string directory, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000246C File Offset: 0x0000066C
		public override PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<AnalogAdaptation>.LogEntry("CheckLatestPackage");
			PackageFileInfo packageFileInfo2;
			try
			{
				Task<PackageFileInfo> task = this.msrService.CheckLatestPackage(this.DeviceQueryParameters(phone), cancellationToken);
				task.Wait(cancellationToken);
				PackageFileInfo packageFileInfo = task.Result;
				MsrPackageInfo msrPackageInfo = packageFileInfo as MsrPackageInfo;
				bool flag = msrPackageInfo != null && msrPackageInfo.PackageFileData != null;
				if (flag)
				{
					packageFileInfo = new MsrPackageInfo(msrPackageInfo.PackageId, msrPackageInfo.Name, msrPackageInfo.SoftwareVersion, msrPackageInfo.SoftwareVersion)
					{
						PackageFileData = msrPackageInfo.PackageFileData
					};
				}
				packageFileInfo2 = packageFileInfo;
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
				Tracer<AnalogAdaptation>.LogExit("CheckLatestPackage");
			}
			return packageFileInfo2;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002598 File Offset: 0x00000798
		public override void CheckPackageIntegrity(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<AnalogAdaptation>.LogEntry("CheckPackageIntegrity");
			try
			{
				bool offlinePackage = phone.PackageFileInfo.OfflinePackage;
				if (offlinePackage)
				{
					Tracer<AnalogAdaptation>.WriteInformation("Check Offline ffu package");
					this.ffuFileInfoService.ReadFfuFile(phone.PackageFilePath);
				}
			}
			finally
			{
				Tracer<AnalogAdaptation>.LogExit("CheckPackageIntegrity");
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002604 File Offset: 0x00000804
		public override void FlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<AnalogAdaptation>.LogEntry("FlashDevice");
			try
			{
				IFFUDevice iffudevice;
				bool flag = this.TryTakeFfuDevice(phone, out iffudevice);
				if (flag)
				{
					this.RelockDeviceUnlockId(iffudevice);
					this.FlashFfu(iffudevice, phone.PackageFilePath);
				}
				this.WaitForReboot(phone);
			}
			catch (FFUFlashException ex)
			{
				bool flag2 = ex.Error == FFUFlashException.ErrorCode.SignatureCheckFailed;
				if (flag2)
				{
					throw new SoftwareIsNotCorrectlySignedException("The device rejected the cryptographic signature in the software package.", ex);
				}
				throw;
			}
			finally
			{
				Tracer<AnalogAdaptation>.LogExit("FlashDevice");
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000269C File Offset: 0x0000089C
		private void WaitForReboot(Phone phone)
		{
			this.progressMessage = "WaitForDeviceToBoot";
			this.RaiseIndeterminateProgressChanged(this.progressMessage);
			IpDeviceCommunicator ipDeviceCommunicator = this.WaitNormalModeDevice(phone);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026CC File Offset: 0x000008CC
		private bool TryTakeFfuDevice(Phone phone, out IFFUDevice device)
		{
			this.ffuDeviceEvent = new AutoResetEvent(false);
			this.normalModeDeviceEvent = new AutoResetEvent(false);
			this.progressMessage = "PuttingDeviceInRecoveryMode";
			this.RaiseIndeterminateProgressChanged(this.progressMessage);
			device = this.GetConnectedFfuDevice(phone) ?? this.WaitConnectedFfuDevice(phone);
			bool flag = device == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Tracer<AnalogAdaptation>.WriteInformation("Device in FFU mode detected");
				Tracer<AnalogAdaptation>.WriteInformation("Friendly name : {0}", new object[] { device.DeviceFriendlyName });
				Tracer<AnalogAdaptation>.WriteInformation("Unique ID : {0}", new object[] { device.DeviceUniqueID });
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002778 File Offset: 0x00000978
		public override bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken cancellationToken)
		{
			return true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000278C File Offset: 0x0000098C
		public override void DownloadPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<AnalogAdaptation>.LogEntry("DownloadPackage");
			base.RaiseProgressPercentageChanged(0, null);
			try
			{
				this.reportingService.OperationStarted(phone, ReportOperationType.DownloadPackage);
				DownloadParameters downloadParameters = new DownloadParameters
				{
					DiscoveryParameters = this.DeviceQueryParameters(phone),
					DestinationFolder = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetLumiaProductsPath((phone.PlatformId != null) ? phone.PlatformId.ProductName : "holo-lens"),
					FilesVersioned = true
				};
				Tracer<AnalogAdaptation>.WriteInformation("Download Params: {0}", new object[] { downloadParameters });
				phone.PackageFilePath = this.msrService.DownloadLatestPackage(downloadParameters, cancellationToken).FirstOrDefault<string>();
				Tuple<long, long, bool> downloadPackageInformation = this.msrService.GetDownloadPackageInformation();
				Tracer<AnalogAdaptation>.WriteInformation("Downloaded package file path set: {0}", new object[] { phone.PackageFilePath });
				Tracer<AnalogAdaptation>.WriteInformation("Check downloaded ffu package");
				this.ffuFileInfoService.ReadFfuFile(phone.PackageFilePath);
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
				Tracer<AnalogAdaptation>.WriteError(ex);
				bool flag3 = flag;
				if (flag3)
				{
					throw;
				}
			}
			finally
			{
				Tracer<AnalogAdaptation>.LogExit("DownloadPackage");
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002464 File Offset: 0x00000664
		public override void DownloadEmergencyPackage(Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002970 File Offset: 0x00000B70
		public override SwVersionComparisonResult CompareFirmwareVersions(Phone phone)
		{
			PackageFileInfo packageFileInfo = phone.PackageFileInfo;
			string akVersion = phone.AkVersion;
			bool flag = packageFileInfo == null || packageFileInfo.AkVersion == null || akVersion == null;
			SwVersionComparisonResult swVersionComparisonResult;
			if (flag)
			{
				swVersionComparisonResult = SwVersionComparisonResult.UnableToCompare;
			}
			else
			{
				swVersionComparisonResult = VersionComparer.CompareSoftwareVersions(akVersion, packageFileInfo.AkVersion, new char[] { '.' });
			}
			return swVersionComparisonResult;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000029C4 File Offset: 0x00000BC4
		protected override void FillSupportedDeviceIdentifiers()
		{
			this.SupportedNormalModeIds.Add(new Microsoft.WindowsDeviceRecoveryTool.Model.DeviceIdentifier("045E", "FFE5"));
			this.SupportedNormalModeIds.Add(new Microsoft.WindowsDeviceRecoveryTool.Model.DeviceIdentifier("045E", "0653"));
			this.SupportedNormalModeIds.Add(new Microsoft.WindowsDeviceRecoveryTool.Model.DeviceIdentifier("045E", "0652"));
			this.SupportedNormalModeIds.Add(new Microsoft.WindowsDeviceRecoveryTool.Model.DeviceIdentifier("045E", "062A"));
			this.SupportedFlashModeIds.Add(new Microsoft.WindowsDeviceRecoveryTool.Model.DeviceIdentifier("045E", "062A"));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002A5C File Offset: 0x00000C5C
		public override int ReadBatteryLevel(Phone phone)
		{
			DiscoveredDeviceInfo discoveredDeviceInfo = this.MatchDeviceInNormalMode(phone);
			bool flag = discoveredDeviceInfo == null;
			int num;
			if (flag)
			{
				Tracer<AnalogAdaptation>.WriteWarning("Device not found in NORMAL mode! - skipping battery check.", new object[0]);
				num = -1;
			}
			else
			{
				IpDeviceCommunicator ipDeviceCommunicator = new IpDeviceCommunicator(discoveredDeviceInfo.UniqueId);
				int num2 = -1;
				try
				{
					ipDeviceCommunicator.Connect();
				}
				catch (Exception ex)
				{
					Tracer<AnalogAdaptation>.WriteError(ex);
					throw;
				}
				try
				{
					num2 = ipDeviceCommunicator.ReadBatteryLevel();
				}
				catch (Exception ex2)
				{
					Tracer<AnalogAdaptation>.WriteError(ex2);
				}
				finally
				{
					ipDeviceCommunicator.Disconnect();
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B0C File Offset: 0x00000D0C
		public override void ReadDeviceInfo(Phone currentPhone, CancellationToken cancellationToken)
		{
			try
			{
				Phone phone = this.ReadDeviceInfoInNormalMode(currentPhone) ?? this.ReadDeviceInfoInFfuMode(currentPhone);
				bool flag = phone != null;
				if (flag)
				{
					base.RaiseDeviceInfoRead(phone);
					return;
				}
			}
			catch (Exception ex)
			{
				Tracer<AnalogAdaptation>.WriteError(ex);
				throw new ReadPhoneInformationException(ex.Message, ex);
			}
			throw new ReadPhoneInformationException("Cannot find selected device!");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B78 File Offset: 0x00000D78
		private Phone ReadDeviceInfoInFfuMode(Phone currentPhone)
		{
			IFFUDevice connectedFfuDevice = this.GetConnectedFfuDevice(currentPhone);
			bool flag = connectedFfuDevice != null;
			Phone phone;
			if (flag)
			{
				PlatformId platformId = new PlatformId();
				platformId.SetPlatformId(connectedFfuDevice.DeviceFriendlyName);
				currentPhone.PlatformId = platformId;
				currentPhone.SalesName = connectedFfuDevice.DeviceFriendlyName;
				currentPhone.ConnectionId = connectedFfuDevice.DeviceUniqueID;
				phone = currentPhone;
			}
			else
			{
				phone = null;
			}
			return phone;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002BD8 File Offset: 0x00000DD8
		private IFFUDevice GetConnectedFfuDevice(Phone phone)
		{
			Tracer<AnalogAdaptation>.LogEntry("GetConnectedFfuDevice");
			IpDeviceCommunicator normalModeDevice = this.GetNormalModeDevice(phone);
			bool flag = normalModeDevice != null;
			IFFUDevice iffudevice;
			if (flag)
			{
				Tracer<AnalogAdaptation>.WriteInformation("Rebooting Device to FFU (UEFI) mode");
				normalModeDevice.Connect();
				normalModeDevice.ExecuteCommand(IpDeviceCommunicator.DeviceUpdateCommandRebootToUefi, null);
				normalModeDevice.Disconnect();
				iffudevice = this.WaitFfuDevice(phone);
			}
			else
			{
				iffudevice = this.GetFfuDevice(phone);
			}
			Tracer<AnalogAdaptation>.LogExit("GetConnectedFfuDevice");
			return iffudevice;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002C50 File Offset: 0x00000E50
		private IpDeviceCommunicator GetNormalModeDevice(Phone currentPhone)
		{
			Tracer<AnalogAdaptation>.LogEntry("GetNormalModeDevice");
			try
			{
				DiscoveredDeviceInfo discoveredDeviceInfo = this.MatchDeviceInNormalMode(currentPhone);
				bool flag = discoveredDeviceInfo == null;
				if (flag)
				{
					Tracer<AnalogAdaptation>.WriteError("No devices found and was looking for {0}!", new object[] { currentPhone });
					return null;
				}
				bool flag2 = discoveredDeviceInfo.Connection == DiscoveredDeviceInfo.ConnectionType.IpOverUsb;
				if (flag2)
				{
					IpDeviceCommunicator ipDeviceCommunicator = new IpDeviceCommunicator(discoveredDeviceInfo.UniqueId);
					ipDeviceCommunicator.Connect();
					try
					{
						IpDeviceCommunicator.DeviceProperties deviceProperties = default(IpDeviceCommunicator.DeviceProperties);
						bool deviceProperties2 = ipDeviceCommunicator.GetDeviceProperties(ref deviceProperties);
						if (deviceProperties2)
						{
							Tracer<AnalogAdaptation>.WriteInformation("Device in OS mode detected");
							Tracer<AnalogAdaptation>.WriteInformation("Name : {0}", new object[] { deviceProperties.Name });
							Tracer<AnalogAdaptation>.WriteInformation("Firmware version : {0}", new object[] { deviceProperties.FirmwareVersion });
							Tracer<AnalogAdaptation>.WriteInformation("UEFI name : {0}", new object[] { deviceProperties.UefiName });
							Tracer<AnalogAdaptation>.WriteInformation("Battery level : {0}", new object[] { deviceProperties.BatteryLevel });
							return ipDeviceCommunicator;
						}
					}
					finally
					{
						ipDeviceCommunicator.Disconnect();
					}
				}
				else
				{
					Tracer<AnalogAdaptation>.WriteWarning("Found device is not IpOverUsb device!", new object[0]);
				}
			}
			finally
			{
				Tracer<AnalogAdaptation>.LogExit("GetNormalModeDevice");
			}
			return null;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002DC8 File Offset: 0x00000FC8
		private IFFUDevice WaitFfuDevice(Phone phone)
		{
			this.selectedPhone = phone;
			FFUManager.DeviceConnectEvent += this.OnFfuDeviceConnected;
			FFUManager.Start();
			Tracer<AnalogAdaptation>.WriteInformation("Waiting for device to boot into FFU (UEFI) mode... ({0})", new object[] { phone.ConnectionId });
			bool flag = this.ffuDeviceEvent.WaitOne(AnalogAdaptation.FfuModeTimeout);
			FFUManager.DeviceConnectEvent -= this.OnFfuDeviceConnected;
			FFUManager.Stop();
			bool flag2 = flag;
			if (flag2)
			{
				return this.ffuDevice;
			}
			throw new DeviceNotFoundException("Could not find device booted into FFU (UEFI) mode");
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E58 File Offset: 0x00001058
		private void OnFfuDeviceConnected(object sender, ConnectEventArgs e)
		{
			bool flag = this.selectedPhone != null;
			if (flag)
			{
				bool flag2 = e.Device.DeviceUniqueID == this.selectedPhone.ConnectionId;
				if (flag2)
				{
					Tracer<AnalogAdaptation>.WriteVerbose("Found ffu device with UniqueId: {0}", new object[] { e.Device.DeviceUniqueID });
					this.ffuDevice = e.Device;
					this.ffuDeviceEvent.Set();
				}
			}
			else
			{
				Tracer<AnalogAdaptation>.WriteInformation("Taking first one, selected phone is NULL");
				this.ffuDevice = e.Device;
				this.ffuDeviceEvent.Set();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002EFC File Offset: 0x000010FC
		private IFFUDevice GetFfuDevice(Phone phone)
		{
			Tracer<AnalogAdaptation>.LogEntry("GetFfuDevice");
			FFUManager.Start();
			IFFUDevice iffudevice2;
			try
			{
				ICollection<IFFUDevice> collection = new List<IFFUDevice>();
				FFUManager.GetFlashableDevices(ref collection);
				bool flag = collection.Count == 0;
				if (flag)
				{
					Tracer<AnalogAdaptation>.WriteWarning("No FFU device found!", new object[0]);
				}
				IFFUDevice iffudevice = collection.FirstOrDefault((IFFUDevice dev) => dev.DeviceUniqueID == phone.ConnectionId);
				bool flag2 = iffudevice == null;
				if (flag2)
				{
					Tracer<AnalogAdaptation>.WriteWarning("Device not found by connectionId, trying to find with Path", new object[0]);
					iffudevice = FFUManager.GetFlashableDevice(phone.Path, false);
				}
				bool flag3 = iffudevice == null && collection.Any<IFFUDevice>();
				if (flag3)
				{
					Tracer<AnalogAdaptation>.WriteError("Taking first one, not found device: {0}", new object[] { phone.ConnectionId });
					iffudevice2 = collection.First<IFFUDevice>();
				}
				else
				{
					iffudevice2 = iffudevice;
				}
			}
			finally
			{
				FFUManager.Stop();
				Tracer<AnalogAdaptation>.LogExit("GetFfuDevice");
			}
			return iffudevice2;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003008 File Offset: 0x00001208
		private IFFUDevice WaitConnectedFfuDevice(Phone phone)
		{
			this.selectedPhone = phone;
			DeviceWatcher deviceWatcher = this.InitializeWatcher();
			deviceWatcher.DeviceChanged += this.OnDeviceChanged;
			FFUManager.DeviceConnectEvent += this.OnFfuDeviceConnected;
			deviceWatcher.Start();
			FFUManager.Start();
			WaitHandle[] array = new WaitHandle[] { this.normalModeDeviceEvent, this.ffuDeviceEvent };
			Tracer<AnalogAdaptation>.WriteInformation("Please connect device in OS or FFU (UEFI) mode");
			int num = WaitHandle.WaitAny(array, AnalogAdaptation.DeviceConnectionTimeout);
			deviceWatcher.DeviceChanged -= this.OnDeviceChanged;
			FFUManager.DeviceConnectEvent -= this.OnFfuDeviceConnected;
			FFUManager.Stop();
			bool flag = num == 0;
			IFFUDevice connectedFfuDevice;
			if (flag)
			{
				connectedFfuDevice = this.GetConnectedFfuDevice(phone);
			}
			else
			{
				bool flag2 = num == 1;
				if (!flag2)
				{
					throw new DeviceNotFoundException("Could not find device in OS or FFU (UEFI) mode");
				}
				connectedFfuDevice = this.ffuDevice;
			}
			return connectedFfuDevice;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000030E8 File Offset: 0x000012E8
		private DeviceWatcher InitializeWatcher()
		{
			return new DeviceWatcher
			{
				DeviceTypeMap = new DeviceTypeMap(new Dictionary<Guid, DeviceType> { 
				{
					AnalogAdaptation.IpOverUsbDeviceInterfaceGuid,
					DeviceType.Interface
				} }),
				Filter = (Nokia.Lucid.Primitives.DeviceIdentifier ss) => (ss.Vid("045E") && ss.Pid("FFE5")) || ((ss.Vid("045E") && ss.Pid("0653")) || (ss.Vid("045E") && ss.Pid("0652")))
			};
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003290 File Offset: 0x00001490
		private void OnDeviceChanged(object sender, DeviceChangedEventArgs e)
		{
			bool flag = e.Action == DeviceChangeAction.Attach;
			if (flag)
			{
				this.normalModeDeviceEvent.Set();
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000032BC File Offset: 0x000014BC
		private void FlashFfu(IFFUDevice device, string ffuPath)
		{
			Tracer<AnalogAdaptation>.WriteInformation("Flashing {0}", new object[] { ffuPath });
			this.progressMessage = "SoftwareInstallation";
			base.RaiseProgressPercentageChanged(0, this.progressMessage);
			device.ProgressEvent += this.FlashProgressEvent;
			try
			{
				device.EndTransfer();
				device.FlashFFUFile(ffuPath, true);
			}
			finally
			{
				device.ProgressEvent -= this.FlashProgressEvent;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003348 File Offset: 0x00001548
		private void RelockDeviceUnlockId(IFFUDevice device)
		{
			string deviceFriendlyName = device.DeviceFriendlyName;
			bool flag = AnalogAdaptation.RelockDeviceUnlockIdNotSupportedPlatformIds.Contains(deviceFriendlyName, StringComparer.OrdinalIgnoreCase);
			if (flag)
			{
				Tracer<AnalogAdaptation>.WriteInformation("Skipping device unlock ID relock. '{0}' is on the list of not supported platform IDs", new object[] { deviceFriendlyName });
			}
			else
			{
				this.progressMessage = "PreparingForSoftwareInstallation";
				this.RaiseIndeterminateProgressChanged(this.progressMessage);
				Tracer<AnalogAdaptation>.WriteInformation("Executing device unlock ID relock");
				try
				{
					device.RelockDeviceUnlockId();
				}
				catch (FFUDeviceCommandNotAvailableException)
				{
					Tracer<AnalogAdaptation>.WriteWarning("Device unlock ID relock not supported on this device", new object[0]);
				}
				catch (Exception ex)
				{
					Tracer<AnalogAdaptation>.WriteError(ex, "Error executing device unlock ID relock", new object[0]);
					throw;
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003400 File Offset: 0x00001600
		private void FlashProgressEvent(object sender, ProgressEventArgs progress)
		{
			double num = (double)progress.Position / (double)progress.Length * 100.0;
			base.RaiseProgressPercentageChanged((int)num, this.progressMessage);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003438 File Offset: 0x00001638
		private IpDeviceCommunicator WaitNormalModeDevice(Phone phone)
		{
			DeviceWatcher deviceWatcher = this.InitializeWatcher();
			deviceWatcher.DeviceChanged += this.OnDeviceChanged;
			deviceWatcher.Start();
			Tracer<AnalogAdaptation>.WriteInformation("Waiting for device to boot into OS...");
			bool flag = this.normalModeDeviceEvent.WaitOne(AnalogAdaptation.OsModeTimeout);
			deviceWatcher.DeviceChanged -= this.OnDeviceChanged;
			bool flag2 = flag;
			if (flag2)
			{
				return this.GetNormalModeDevice(phone);
			}
			throw new DeviceNotFoundException("Could not find device booted into OS");
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000034B4 File Offset: 0x000016B4
		protected override Stream GetImageDataStream(Phone phone)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("Analog.png"));
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

		// Token: 0x0600002B RID: 43 RVA: 0x0000351C File Offset: 0x0000171C
		private Phone ReadDeviceInfoInNormalMode(Phone currentPhone)
		{
			Phone phone;
			try
			{
				Tracer<AnalogAdaptation>.LogEntry("ReadDeviceInfoInNormalMode");
				DiscoveredDeviceInfo discoveredDeviceInfo = this.MatchDeviceInNormalMode(currentPhone);
				bool flag = discoveredDeviceInfo == null;
				if (flag)
				{
					Tracer<AnalogAdaptation>.WriteError("No devices found and was looking for {0}!", new object[] { currentPhone });
					phone = null;
				}
				else
				{
					Tracer<AnalogAdaptation>.WriteVerbose("Current phone: {0}", new object[] { currentPhone });
					Tracer<AnalogAdaptation>.WriteVerbose("Found device data: Address - {0}, Connection - {1}, Location - {2}, Name - {3}, UniqueId - {4}", new object[] { discoveredDeviceInfo.Address, discoveredDeviceInfo.Connection, discoveredDeviceInfo.Location, discoveredDeviceInfo.Name, discoveredDeviceInfo.UniqueId });
					bool flag2 = discoveredDeviceInfo.Connection == DiscoveredDeviceInfo.ConnectionType.IpOverUsb;
					if (flag2)
					{
						IpDeviceCommunicator ipDeviceCommunicator = new IpDeviceCommunicator(discoveredDeviceInfo.UniqueId);
						try
						{
							ipDeviceCommunicator.Connect();
						}
						catch (Exception ex)
						{
							Tracer<AnalogAdaptation>.WriteError(ex);
							throw;
						}
						try
						{
							IpDeviceCommunicator.DeviceProperties deviceProperties = default(IpDeviceCommunicator.DeviceProperties);
							bool deviceProperties2 = ipDeviceCommunicator.GetDeviceProperties(ref deviceProperties);
							if (deviceProperties2)
							{
								Tracer<AnalogAdaptation>.WriteInformation("Device in OS mode detected");
								currentPhone.SalesName = deviceProperties.Name;
								currentPhone.SoftwareVersion = deviceProperties.FirmwareVersion;
								PlatformId platformId = new PlatformId();
								platformId.SetPlatformId(deviceProperties.UefiName);
								currentPhone.PlatformId = platformId;
								currentPhone.BatteryLevel = deviceProperties.BatteryLevel;
								currentPhone.ConnectionId = discoveredDeviceInfo.UniqueId;
								currentPhone.AkVersion = ipDeviceCommunicator.GetOSVersion();
								Tracer<AnalogAdaptation>.WriteInformation("Name: {0}, Firmware version: {1}, Uefi Name: {2}, Battery Level: {3}, OS Version: {4}", new object[] { deviceProperties.Name, deviceProperties.FirmwareVersion, deviceProperties.UefiName, deviceProperties.BatteryLevel, currentPhone.AkVersion });
								return currentPhone;
							}
							Tracer<AnalogAdaptation>.WriteError("GetDeviceProperties failed!", new object[0]);
						}
						catch (Exception ex2)
						{
							Tracer<AnalogAdaptation>.WriteError(ex2);
							throw;
						}
						finally
						{
							ipDeviceCommunicator.Disconnect();
						}
					}
					else
					{
						Tracer<AnalogAdaptation>.WriteWarning("Found device is not IpOverUsb device!", new object[0]);
					}
					phone = null;
				}
			}
			finally
			{
				Tracer<AnalogAdaptation>.LogExit("ReadDeviceInfoInNormalMode");
			}
			return phone;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000378C File Offset: 0x0000198C
		public override bool IsSupportedInNormalMode(UsbDevice usbDevice)
		{
			bool flag = this.SupportedNormalModeIds.Any((Microsoft.WindowsDeviceRecoveryTool.Model.DeviceIdentifier deviceIdentifier) => deviceIdentifier.Vid.Equals(usbDevice.Vid, StringComparison.CurrentCultureIgnoreCase) && deviceIdentifier.Pid.Equals(usbDevice.Pid, StringComparison.CurrentCultureIgnoreCase));
			bool flag3;
			if (flag)
			{
				bool flag2 = this.SupportedFlashModeIds.Any((Microsoft.WindowsDeviceRecoveryTool.Model.DeviceIdentifier deviceIdentifier) => deviceIdentifier.Vid.Equals(usbDevice.Vid, StringComparison.CurrentCultureIgnoreCase) && deviceIdentifier.Pid.Equals(usbDevice.Pid, StringComparison.CurrentCultureIgnoreCase));
				flag3 = !flag2 || this.IsSupportedInFlashMode(usbDevice);
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000037F8 File Offset: 0x000019F8
		public override bool IsSupportedInFlashMode(UsbDevice usbDevice)
		{
			bool flag = this.SupportedFlashModeIds.Any((Microsoft.WindowsDeviceRecoveryTool.Model.DeviceIdentifier deviceIdentifier) => deviceIdentifier.Vid.Equals(usbDevice.Vid, StringComparison.CurrentCultureIgnoreCase) && deviceIdentifier.Pid.Equals(usbDevice.Pid, StringComparison.CurrentCultureIgnoreCase));
			if (flag)
			{
				FFUManager.Start();
				Tracer<AnalogAdaptation>.WriteInformation("FFU Manager started.");
				try
				{
					IFFUDevice flashableDevice = FFUManager.GetFlashableDevice(usbDevice.Path, false);
					bool flag2 = flashableDevice != null;
					if (flag2)
					{
						bool flag3 = flashableDevice.DeviceFriendlyName.ToLower().Contains("sakura") || flashableDevice.DeviceFriendlyName.ToLower().Contains("hololens");
						if (flag3)
						{
							Tracer<AnalogAdaptation>.WriteInformation("Analog device in ffu mode.");
							return true;
						}
						Tracer<AnalogAdaptation>.WriteWarning("Analog device friendly name not expected: {0}", new object[] { flashableDevice.DeviceFriendlyName });
					}
					return false;
				}
				finally
				{
					FFUManager.Stop();
					Tracer<AnalogAdaptation>.WriteInformation("FFU Manager stopped.");
				}
			}
			return false;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000038F0 File Offset: 0x00001AF0
		private DiscoveredDeviceInfo MatchDeviceInNormalMode(Phone currentPhone)
		{
			DiscoveredDeviceInfo discoveredDeviceInfo;
			try
			{
				Tracer<AnalogAdaptation>.LogEntry("MatchDeviceInNormalMode");
				bool flag = currentPhone == null;
				if (flag)
				{
					Tracer<AnalogAdaptation>.WriteError("Phone is NULL!", new object[0]);
					discoveredDeviceInfo = null;
				}
				else
				{
					DeviceDiscoveryService deviceDiscoveryService = new DeviceDiscoveryService();
					deviceDiscoveryService.Start(default(Guid));
					List<DiscoveredDeviceInfo> list = deviceDiscoveryService.DevicesDiscovered();
					deviceDiscoveryService.Stop();
					bool flag2 = list.Count == 0;
					if (flag2)
					{
						Tracer<AnalogAdaptation>.WriteWarning("No devices in OS mode detected!", new object[0]);
						discoveredDeviceInfo = null;
					}
					else
					{
						Tracer<AnalogAdaptation>.WriteVerbose(string.Join(Environment.NewLine, from d in list
							where d != null
							select string.Format("Device discovered: Name = {0}, loc = {1}, add = {2}, arch = {3}, con = {4}, os = {5}, uid = {6} ", new object[] { d.Name, d.Location, d.Address, d.Architecture, d.Connection, d.OSVersion, d.UniqueId })), new object[0]);
						DiscoveredDeviceInfo discoveredDeviceInfo2 = list.FirstOrDefault((DiscoveredDeviceInfo device) => string.Compare(device.Location, currentPhone.InstanceId, StringComparison.InvariantCultureIgnoreCase) == 0);
						bool flag3 = discoveredDeviceInfo2 == null;
						if (flag3)
						{
							Tracer<AnalogAdaptation>.WriteWarning("Device not found via location. Check dev UID.", new object[0]);
							string uid = this.ExtractDeviceUid(currentPhone);
							discoveredDeviceInfo2 = list.FirstOrDefault((DiscoveredDeviceInfo device) => string.Compare(uid, string.Format("{0}", device.UniqueId), StringComparison.InvariantCultureIgnoreCase) == 0);
						}
						bool flag4 = discoveredDeviceInfo2 == null;
						if (flag4)
						{
							Tracer<AnalogAdaptation>.WriteError("No match found for {0}", new object[] { currentPhone });
							discoveredDeviceInfo = null;
						}
						else
						{
							discoveredDeviceInfo = discoveredDeviceInfo2;
						}
					}
				}
			}
			finally
			{
				Tracer<AnalogAdaptation>.LogExit("MatchDeviceInNormalMode");
			}
			return discoveredDeviceInfo;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003AA8 File Offset: 0x00001CA8
		protected override Stream GetManufacturerImageDataStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("Analog.png"));
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

		// Token: 0x06000030 RID: 48 RVA: 0x00003B0C File Offset: 0x00001D0C
		public override BatteryStatus ReadBatteryStatus(Phone phone)
		{
			try
			{
				int num = this.ReadBatteryLevel(phone);
				Tracer<AnalogAdaptation>.WriteInformation("Battery level: ", new object[] { num });
				bool flag = num >= 25;
				if (flag)
				{
					return BatteryStatus.BatteryOk;
				}
				bool flag2 = num != -1;
				if (flag2)
				{
					return BatteryStatus.BatteryNotOkBlock;
				}
			}
			catch (Exception ex)
			{
				Tracer<AnalogAdaptation>.WriteError(ex, "Error reading battery status", new object[0]);
			}
			return BatteryStatus.BatteryUnknown;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003B90 File Offset: 0x00001D90
		private QueryParameters DeviceQueryParameters(Phone phone)
		{
			return new QueryParameters
			{
				ManufacturerName = "Microsoft",
				ManufacturerProductLine = "HoloLens",
				ManufacturerHardwareModel = "HoloLens",
				PackageType = "Firmware"
			};
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003BD8 File Offset: 0x00001DD8
		private string ExtractDeviceUid(Phone currentPhone)
		{
			bool flag = string.IsNullOrEmpty(currentPhone.InstanceId);
			string text;
			if (flag)
			{
				Tracer<AnalogAdaptation>.WriteWarning("Current phone has no instance ID", new object[0]);
				text = null;
			}
			else
			{
				string text2 = currentPhone.InstanceId.Substring(currentPhone.InstanceId.LastIndexOf('\\') + 1);
				bool flag2 = string.IsNullOrEmpty(text2) || text2.Length != 32;
				if (flag2)
				{
					Tracer<AnalogAdaptation>.WriteWarning("Base UID not proper: {0}", new object[] { text2 });
					text = null;
				}
				else
				{
					string text3 = new string(text2.Substring(0, 8).Reverse<char>().ToArray<char>());
					string text4 = new string(text2.Substring(8, 4).Reverse<char>().ToArray<char>());
					string text5 = new string(text2.Substring(12, 4).Reverse<char>().ToArray<char>());
					string text6 = new string(text2.Substring(16, 2).Reverse<char>().ToArray<char>());
					string text7 = new string(text2.Substring(18, 2).Reverse<char>().ToArray<char>());
					string text8 = new string(text2.Substring(20, 2).Reverse<char>().ToArray<char>());
					string text9 = new string(text2.Substring(22, 2).Reverse<char>().ToArray<char>());
					string text10 = new string(text2.Substring(24, 2).Reverse<char>().ToArray<char>());
					string text11 = new string(text2.Substring(26, 2).Reverse<char>().ToArray<char>());
					string text12 = new string(text2.Substring(28, 2).Reverse<char>().ToArray<char>());
					string text13 = new string(text2.Substring(30, 2).Reverse<char>().ToArray<char>());
					string text14 = "{0}-{1}-{2}-{3}{4}-{5}{6}{7}{8}{9}{10}";
					string text15 = string.Format(text14, new object[]
					{
						text3, text4, text5, text6, text7, text8, text9, text10, text11, text12,
						text13
					});
					Tracer<AnalogAdaptation>.WriteWarning("Extracted UID: {0}", new object[] { text15 });
					text = text15;
				}
			}
			return text;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003DEC File Offset: 0x00001FEC
		public override bool CheckIfDeviceStillConnected(Phone phone)
		{
			return this.MatchDeviceInNormalMode(phone) != null || this.GetFfuDevice(phone) != null;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003E14 File Offset: 0x00002014
		private void RaiseIndeterminateProgressChanged(string message)
		{
			base.RaiseProgressPercentageChanged(101, message);
		}

		// Token: 0x04000006 RID: 6
		private static readonly string[] RelockDeviceUnlockIdNotSupportedPlatformIds = new string[] { "Microsoft Corporation.HoloLens.HoloLens.R06", "Microsoft Corporation.HoloLens.HoloLens.1.0" };

		// Token: 0x04000007 RID: 7
		private static readonly TimeSpan OsModeTimeout = new TimeSpan(0, 8, 0);

		// Token: 0x04000008 RID: 8
		private static readonly TimeSpan FfuModeTimeout = new TimeSpan(0, 5, 0);

		// Token: 0x04000009 RID: 9
		private static readonly TimeSpan DeviceConnectionTimeout = new TimeSpan(0, 2, 0);

		// Token: 0x0400000A RID: 10
		private static readonly Guid IpOverUsbDeviceInterfaceGuid = new Guid(654236750, 27331, 16961, 158, 77, 227, 212, 178, 197, 197, 52);

		// Token: 0x0400000B RID: 11
		public static readonly string OldestRollbackOsVersion = "10.0.14393.0";

		// Token: 0x0400000C RID: 12
		private readonly MsrService msrService;

		// Token: 0x0400000D RID: 13
		private readonly ReportingService reportingService;

		// Token: 0x0400000E RID: 14
		private readonly FlowConditionService flowCondition;

		// Token: 0x0400000F RID: 15
		private readonly FfuFileInfoService ffuFileInfoService;

		// Token: 0x04000010 RID: 16
		private AutoResetEvent ffuDeviceEvent;

		// Token: 0x04000011 RID: 17
		private AutoResetEvent normalModeDeviceEvent;

		// Token: 0x04000012 RID: 18
		private IFFUDevice ffuDevice;

		// Token: 0x04000013 RID: 19
		private string progressMessage;

		// Token: 0x04000014 RID: 20
		private Phone selectedPhone;
	}
}
