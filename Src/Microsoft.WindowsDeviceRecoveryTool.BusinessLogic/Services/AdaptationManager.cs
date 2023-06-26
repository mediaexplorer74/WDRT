using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services;
using Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Services;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;

namespace Microsoft.WindowsDeviceRecoveryTool.BusinessLogic.Services
{
	// Token: 0x02000003 RID: 3
	[Export(typeof(AdaptationManager))]
	[Export(typeof(IManufacturerDataProvider))]
	public class AdaptationManager : IDisposable, IManufacturerDataProvider
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002461 File Offset: 0x00000661
		[ImportingConstructor]
		public AdaptationManager(ManufacturerAutodetectionService manufacturerAutodetectionService, ReportingService reportingService)
		{
			this.manufacturerAutodetectionService = manufacturerAutodetectionService;
			this.reportingService = reportingService;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000013 RID: 19 RVA: 0x00002484 File Offset: 0x00000684
		// (remove) Token: 0x06000014 RID: 20 RVA: 0x000024BC File Offset: 0x000006BC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ProgressChangedEventArgs> ProgressChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000015 RID: 21 RVA: 0x000024F4 File Offset: 0x000006F4
		// (remove) Token: 0x06000016 RID: 22 RVA: 0x0000252C File Offset: 0x0000072C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Phone> DeviceConnected;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000017 RID: 23 RVA: 0x00002564 File Offset: 0x00000764
		// (remove) Token: 0x06000018 RID: 24 RVA: 0x0000259C File Offset: 0x0000079C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Phone> DeviceDisconnected;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000019 RID: 25 RVA: 0x000025D4 File Offset: 0x000007D4
		// (remove) Token: 0x0600001A RID: 26 RVA: 0x0000260C File Offset: 0x0000080C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Phone> DeviceEndpointConnected;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600001B RID: 27 RVA: 0x00002644 File Offset: 0x00000844
		// (remove) Token: 0x0600001C RID: 28 RVA: 0x0000267C File Offset: 0x0000087C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Phone> DeviceInfoRead;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600001D RID: 29 RVA: 0x000026B4 File Offset: 0x000008B4
		// (remove) Token: 0x0600001E RID: 30 RVA: 0x000026EC File Offset: 0x000008EC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Phone> DeviceBatteryLevelRead;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600001F RID: 31 RVA: 0x00002724 File Offset: 0x00000924
		// (remove) Token: 0x06000020 RID: 32 RVA: 0x0000275C File Offset: 0x0000095C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<BatteryStatus> DeviceBatteryStatusRead;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000021 RID: 33 RVA: 0x00002794 File Offset: 0x00000994
		// (remove) Token: 0x06000022 RID: 34 RVA: 0x000027CC File Offset: 0x000009CC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<bool> DeviceConnectionStatusRead;

		// Token: 0x06000023 RID: 35 RVA: 0x00002801 File Offset: 0x00000A01
		internal void AddAdaptation(BaseAdaptation adaptationService)
		{
			this.adaptationServices.Add(adaptationService);
			adaptationService.ProgressChanged += this.RaiseProgressChanged;
			adaptationService.DeviceInfoRead += this.RaiseDeviceInfoRead;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002838 File Offset: 0x00000A38
		private void RaiseDeviceInfoRead(Phone phone)
		{
			Action<Phone> deviceInfoRead = this.DeviceInfoRead;
			bool flag = deviceInfoRead != null;
			if (flag)
			{
				deviceInfoRead(phone);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002860 File Offset: 0x00000A60
		public BaseAdaptation GetAdaptation(PhoneTypes selectedManufacturer)
		{
			return this.adaptationServices.FirstOrDefault((BaseAdaptation adaptationService) => adaptationService.PhoneType == selectedManufacturer);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002898 File Offset: 0x00000A98
		public ReadOnlyCollection<BaseAdaptation> GetAdaptations(Predicate<BaseAdaptation> match)
		{
			return Array.AsReadOnly<BaseAdaptation>(this.adaptationServices.Where((BaseAdaptation a) => match(a)).ToArray<BaseAdaptation>());
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000028D8 File Offset: 0x00000AD8
		public string GetAdaptationExtension(PhoneTypes phoneType)
		{
			return this.GetAdaptation(phoneType).PackageExtension;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028F8 File Offset: 0x00000AF8
		public List<ManufacturerInfo> GetAdaptationsData()
		{
			return (from a in this.adaptationServices
				orderby a.PhoneType
				select a into adaptationService
				select new ManufacturerInfo(adaptationService.PhoneType, adaptationService.RecoverySupport, adaptationService.ManufacturerName, adaptationService.GetManufacturerImageData(), adaptationService.ReportManufacturerName, adaptationService.ReportManufacturerProductLine)).ToList<ManufacturerInfo>();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002960 File Offset: 0x00000B60
		public List<Phone> GetSupportedAdaptationModels(PhoneTypes phoneType)
		{
			BaseAdaptation adaptation = this.GetAdaptation(phoneType);
			bool flag = adaptation != null;
			List<Phone> list;
			if (flag)
			{
				list = adaptation.ManuallySupportedModels();
			}
			else
			{
				list = new List<Phone>();
			}
			return list;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002994 File Offset: 0x00000B94
		public List<PackageFileInfo> FindCorrectPackage(string directory, Phone phone, CancellationToken cancellationToken)
		{
			return this.GetAdaptation(phone.Type).FindPackage(directory, phone, cancellationToken);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000029BC File Offset: 0x00000BBC
		public void CheckPackageIntegrity(Phone phone, CancellationToken token)
		{
			BaseAdaptation adaptation = this.GetAdaptation(phone.Type);
			adaptation.CheckPackageIntegrity(phone, token);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000029E0 File Offset: 0x00000BE0
		public PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken)
		{
			BaseAdaptation adaptation = this.GetAdaptation(phone.Type);
			phone.ReportManufacturerName = adaptation.ReportManufacturerName;
			phone.ReportManufacturerProductLine = adaptation.ReportManufacturerProductLine;
			return adaptation.CheckLatestPackage(phone, cancellationToken);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A24 File Offset: 0x00000C24
		private void RaiseProgressChanged(ProgressChangedEventArgs progressChangedEventArgs)
		{
			Action<ProgressChangedEventArgs> progressChanged = this.ProgressChanged;
			bool flag = progressChanged != null;
			if (flag)
			{
				progressChanged(progressChangedEventArgs);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A4C File Offset: 0x00000C4C
		public void FlashDevice(Phone phone, DetectionType detectionType, CancellationToken token)
		{
			ReportOperationType reportOperationType;
			UriData uriData;
			this.GetRequiredValuesForDetectionType(detectionType, out reportOperationType, out uriData);
			try
			{
				this.reportingService.OperationStarted(phone, reportOperationType);
				this.GetAdaptation(phone.Type).FlashDevice(phone, token);
				this.reportingService.OperationSucceded(phone, reportOperationType);
			}
			catch (Exception ex)
			{
				bool flag = ex is SoftwareIsNotCorrectlySignedException;
				if (flag)
				{
					uriData = UriData.SoftwareNotCorrectlySigned;
				}
				else
				{
					bool flag2 = ex is CheckResetProtectionException;
					if (flag2)
					{
						uriData = UriData.ResetProtectionStatusIsIncorrect;
					}
					else
					{
						bool flag3 = ex is FlashModeChangeException;
						if (flag3)
						{
							uriData = UriData.ChangeToFlashModeFailed;
						}
					}
				}
				this.reportingService.OperationFailed(phone, reportOperationType, uriData, ex);
				throw;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B08 File Offset: 0x00000D08
		public void EmergencyFlashDevice(Phone phone, CancellationToken token)
		{
			try
			{
				this.reportingService.OperationStarted(phone, ReportOperationType.EmergencyFlashing);
				LumiaAdaptation lumiaAdaptation = this.GetAdaptation(PhoneTypes.Lumia) as LumiaAdaptation;
				lumiaAdaptation.EmergencyFlashDevice(phone, token);
				this.reportingService.PartialOperationSucceded(phone, ReportOperationType.EmergencyFlashing, UriData.EmergencyFlashingSuccesfullyFinished);
			}
			catch (Exception ex)
			{
				this.reportingService.OperationFailed(phone, ReportOperationType.EmergencyFlashing, UriData.EmergencyFlashingFailed, ex);
				throw;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002B7C File Offset: 0x00000D7C
		private void GetRequiredValuesForDetectionType(DetectionType detectionType, out ReportOperationType operationType, out UriData failUriData)
		{
			switch (detectionType)
			{
			case DetectionType.NormalMode:
				operationType = ReportOperationType.Flashing;
				failUriData = UriData.ProgrammingPhoneFailed;
				break;
			case DetectionType.RecoveryMode:
				operationType = ReportOperationType.Recovery;
				failUriData = UriData.DeadPhoneRecoveryFailed;
				break;
			case DetectionType.RecoveryModeAfterEmergencyFlashing:
				operationType = ReportOperationType.EmergencyFlashing;
				failUriData = UriData.RecoveryAfterEmergencyFlashingFailed;
				break;
			default:
				throw new InvalidOperationException("Detection type not supported. Currently there is only Normal mode and Recovery mode detection allowed.");
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken token)
		{
			return this.GetAdaptation(phone.Type).IsDeviceInFlashModeConnected(phone, token);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public void DownloadPackage(Phone phone, CancellationToken token)
		{
			try
			{
				this.GetAdaptation(phone.Type).DownloadPackage(phone, token);
			}
			catch (DownloadPackageException ex)
			{
				bool flag = ex.InnerException is WebException;
				if (flag)
				{
					throw ex.InnerException;
				}
				AggregateException ex2 = ex.InnerException as AggregateException;
				bool flag2 = ex2 != null;
				if (flag2)
				{
					throw ex2.Flatten().InnerException;
				}
				throw;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002C70 File Offset: 0x00000E70
		public SwVersionComparisonResult CompareFirmwareVersions(Phone phone)
		{
			bool flag = phone != null;
			SwVersionComparisonResult swVersionComparisonResult;
			if (flag)
			{
				BaseAdaptation adaptation = this.GetAdaptation(phone.Type);
				swVersionComparisonResult = adaptation.CompareFirmwareVersions(phone);
			}
			else
			{
				Tracer<AdaptationManager>.WriteError("Current phone is NULL", new object[0]);
				swVersionComparisonResult = SwVersionComparisonResult.UnableToCompare;
			}
			return swVersionComparisonResult;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public void StartDevicesAutodetection(DetectionParameters detectionParams)
		{
			this.detectionParameters = detectionParams;
			this.manufacturerAutodetectionService.DeviceConnected += this.AutodetectionServiceDeviceConnected;
			this.manufacturerAutodetectionService.DeviceDisconnected += this.AutodetectionServiceDeviceDisconnected;
			this.manufacturerAutodetectionService.DeviceEndpointConnected += this.AutodetectionServiceDeviceEndpointConnected;
			switch (detectionParams.PhoneModes)
			{
			case PhoneModes.Normal:
				this.manufacturerAutodetectionService.Start(this.GetNormalModeSupportedIdentifiers());
				break;
			case PhoneModes.Flash:
				this.manufacturerAutodetectionService.Start(this.GetFlashModeSupportedIdentifiers());
				break;
			case PhoneModes.All:
			{
				Collection<DeviceIdentifier> normalModeSupportedIdentifiers = this.GetNormalModeSupportedIdentifiers();
				Collection<DeviceIdentifier> flashModeSupportedIdentifiers = this.GetFlashModeSupportedIdentifiers();
				IEnumerable<DeviceIdentifier> enumerable = normalModeSupportedIdentifiers.Concat(flashModeSupportedIdentifiers);
				this.manufacturerAutodetectionService.Start((Collection<DeviceIdentifier>)enumerable);
				break;
			}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D84 File Offset: 0x00000F84
		public void StopDevicesAutodetection()
		{
			this.manufacturerAutodetectionService.Stop();
			this.manufacturerAutodetectionService.DeviceConnected -= this.AutodetectionServiceDeviceConnected;
			this.manufacturerAutodetectionService.DeviceDisconnected -= this.AutodetectionServiceDeviceDisconnected;
			this.manufacturerAutodetectionService.DeviceEndpointConnected -= this.AutodetectionServiceDeviceEndpointConnected;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public List<Phone> GetConnectedPhones(DetectionParameters detectionParams)
		{
			List<Phone> list = new List<Phone>();
			ReadOnlyCollection<UsbDevice> connectedDevices = this.manufacturerAutodetectionService.GetConnectedDevices();
			foreach (UsbDevice usbDevice in connectedDevices)
			{
				IEnumerable<BaseAdaptation> enumerable = this.MatchAdaptation(usbDevice, detectionParams);
				Phone phone = this.PhoneFromUsbDevice(usbDevice, enumerable.ToList<BaseAdaptation>());
				bool flag = phone != null;
				if (flag)
				{
					list.Add(phone);
				}
			}
			return list;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002E78 File Offset: 0x00001078
		private Phone PhoneFromUsbDevice(UsbDevice usbDevice, IList<BaseAdaptation> adaptations)
		{
			bool flag = adaptations.Count<BaseAdaptation>() == 1;
			if (flag)
			{
				BaseAdaptation baseAdaptation = adaptations.FirstOrDefault<BaseAdaptation>();
				bool flag2 = baseAdaptation != null;
				if (flag2)
				{
					Phone phone = new Phone(usbDevice, baseAdaptation.PhoneType, baseAdaptation.SalesNameProvider(), false, "", "");
					phone.ImageData = baseAdaptation.GetImageData(phone);
					phone.ReportManufacturerName = baseAdaptation.ReportManufacturerName;
					phone.ReportManufacturerProductLine = baseAdaptation.ReportManufacturerProductLine;
					return phone;
				}
			}
			else
			{
				bool flag3 = adaptations.Count<BaseAdaptation>() > 1;
				if (flag3)
				{
					Phone phone2 = new Phone(usbDevice, PhoneTypes.UnknownWp, null, false, "", "");
					phone2.SalesName = "Unknown Windows Phone";
					phone2.MatchedAdaptationTypes = new List<PhoneTypes>(adaptations.Select((BaseAdaptation a) => a.PhoneType));
					return phone2;
				}
			}
			return null;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002F64 File Offset: 0x00001164
		private IEnumerable<BaseAdaptation> MatchAdaptation(UsbDevice usbDevice, DetectionParameters detectionParams)
		{
			List<BaseAdaptation> list = this.adaptationServices;
			bool flag = detectionParams.PhoneTypes != PhoneTypes.All;
			if (flag)
			{
				list = list.FindAll((BaseAdaptation a) => a.PhoneType == detectionParams.PhoneTypes);
			}
			IEnumerable<BaseAdaptation> enumerable;
			switch (detectionParams.PhoneModes)
			{
			case PhoneModes.Normal:
				enumerable = from a in list
					orderby a.PhoneType
					where a.IsSupportedInNormalMode(usbDevice)
					select a;
				break;
			case PhoneModes.Flash:
				enumerable = from a in list
					orderby a.PhoneType
					where a.IsSupportedInFlashMode(usbDevice)
					select a;
				break;
			case PhoneModes.All:
			{
				IEnumerable<BaseAdaptation> enumerable2 = from a in list
					orderby a.PhoneType
					where a.IsSupportedInNormalMode(usbDevice)
					select a;
				IList<BaseAdaptation> list2 = (enumerable2 as IList<BaseAdaptation>) ?? enumerable2.ToList<BaseAdaptation>();
				bool flag2 = list2.Any<BaseAdaptation>();
				if (flag2)
				{
					enumerable = list2;
				}
				else
				{
					enumerable = from a in list
						orderby a.PhoneType
						where a.IsSupportedInFlashMode(usbDevice)
						select a;
				}
				break;
			}
			default:
				enumerable = null;
				break;
			}
			return enumerable;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000030F8 File Offset: 0x000012F8
		private void AutodetectionServiceDeviceConnected(UsbDeviceEventArgs args)
		{
			Action<Phone> deviceConnected = this.DeviceConnected;
			bool flag = deviceConnected != null;
			if (flag)
			{
				IEnumerable<BaseAdaptation> enumerable = this.MatchAdaptation(args.UsbDevice, this.detectionParameters);
				Phone phone = this.PhoneFromUsbDevice(args.UsbDevice, enumerable.ToList<BaseAdaptation>()) ?? new Phone(args.UsbDevice, PhoneTypes.None, null, false, "", "");
				deviceConnected(phone);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003164 File Offset: 0x00001364
		private void AutodetectionServiceDeviceDisconnected(UsbDeviceEventArgs args)
		{
			Action<Phone> deviceDisconnected = this.DeviceDisconnected;
			bool flag = deviceDisconnected != null;
			if (flag)
			{
				deviceDisconnected(new Phone(args.UsbDevice, PhoneTypes.None, null, false, "", ""));
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000031A4 File Offset: 0x000013A4
		private void AutodetectionServiceDeviceEndpointConnected(UsbDeviceEventArgs args)
		{
			Action<Phone> deviceEndpointConnected = this.DeviceEndpointConnected;
			bool flag = deviceEndpointConnected != null;
			if (flag)
			{
				IEnumerable<BaseAdaptation> enumerable = this.MatchAdaptation(args.UsbDevice, this.detectionParameters);
				Phone phone = this.PhoneFromUsbDevice(args.UsbDevice, enumerable.ToList<BaseAdaptation>());
				bool flag2 = phone != null;
				if (flag2)
				{
					deviceEndpointConnected(phone);
				}
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003200 File Offset: 0x00001400
		private Collection<DeviceIdentifier> GetNormalModeSupportedIdentifiers()
		{
			bool flag = this.detectionParameters.PhoneTypes == PhoneTypes.All;
			Collection<DeviceIdentifier> collection;
			if (flag)
			{
				collection = new Collection<DeviceIdentifier>(this.adaptationServices.SelectMany((BaseAdaptation adaptation) => adaptation.SupportedNormalModeIds).ToList<DeviceIdentifier>());
			}
			else
			{
				BaseAdaptation baseAdaptation = this.adaptationServices.Find((BaseAdaptation adaptation) => adaptation.PhoneType == this.detectionParameters.PhoneTypes);
				collection = new Collection<DeviceIdentifier>(baseAdaptation.SupportedNormalModeIds);
			}
			return collection;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003280 File Offset: 0x00001480
		private Collection<DeviceIdentifier> GetFlashModeSupportedIdentifiers()
		{
			bool flag = this.detectionParameters.PhoneTypes == PhoneTypes.All;
			Collection<DeviceIdentifier> collection;
			if (flag)
			{
				collection = new Collection<DeviceIdentifier>(this.adaptationServices.SelectMany((BaseAdaptation adaptation) => adaptation.SupportedFlashModeIds).ToList<DeviceIdentifier>());
			}
			else
			{
				BaseAdaptation baseAdaptation = this.adaptationServices.Find((BaseAdaptation adaptation) => adaptation.PhoneType == this.detectionParameters.PhoneTypes);
				collection = new Collection<DeviceIdentifier>(baseAdaptation.SupportedFlashModeIds);
			}
			return collection;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000032FF File Offset: 0x000014FF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003314 File Offset: 0x00001514
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				if (disposing)
				{
					this.ReleaseManagedObjects();
				}
				this.ReleaseUnmanagedObjects();
				this.disposed = true;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000334C File Offset: 0x0000154C
		protected virtual void ReleaseManagedObjects()
		{
			foreach (BaseAdaptation baseAdaptation in this.adaptationServices)
			{
				baseAdaptation.Dispose();
			}
			this.StopDevicesAutodetection();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000033AC File Offset: 0x000015AC
		protected virtual void ReleaseUnmanagedObjects()
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000033B0 File Offset: 0x000015B0
		public List<PackageFileInfo> FindAllPackages(string directory, PhoneTypes phoneType, CancellationToken cancellationToken)
		{
			return this.GetAdaptation(phoneType).FindAllPackages(directory, cancellationToken);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000033D0 File Offset: 0x000015D0
		public void DownloadEmeregencyPackage(Phone currentPhone, CancellationToken cancellationToken)
		{
			try
			{
				this.GetAdaptation(currentPhone.Type).DownloadEmergencyPackage(currentPhone, cancellationToken);
			}
			catch (DownloadPackageException ex)
			{
				bool flag = ex.InnerException is WebException;
				if (flag)
				{
					throw ex.InnerException;
				}
				AggregateException ex2 = ex.InnerException as AggregateException;
				bool flag2 = ex2 != null;
				if (flag2)
				{
					throw ex2.Flatten().InnerException;
				}
				throw;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003448 File Offset: 0x00001648
		public void ReadDeviceInfo(Phone currentPhone, CancellationToken cancellationToken)
		{
			this.GetAdaptation(currentPhone.Type).ReadDeviceInfo(currentPhone, cancellationToken);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003460 File Offset: 0x00001660
		public void ReadDeviceBatteryLevel(Phone currentPhone, CancellationToken cancellationToken)
		{
			currentPhone.BatteryLevel = this.GetAdaptation(currentPhone.Type).ReadBatteryLevel(currentPhone);
			Action<Phone> deviceBatteryLevelRead = this.DeviceBatteryLevelRead;
			bool flag = deviceBatteryLevelRead != null;
			if (flag)
			{
				deviceBatteryLevelRead(currentPhone);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000034A0 File Offset: 0x000016A0
		public void ReadDeviceBatteryStatus(Phone phone, CancellationToken cancellationToken)
		{
			BatteryStatus batteryStatus = this.GetAdaptation(phone.Type).ReadBatteryStatus(phone);
			Action<BatteryStatus> deviceBatteryStatusRead = this.DeviceBatteryStatusRead;
			bool flag = deviceBatteryStatusRead != null;
			if (flag)
			{
				deviceBatteryStatusRead(batteryStatus);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000034DC File Offset: 0x000016DC
		public void CheckIfDeviceStillConnected(Phone phone, CancellationToken cancellationToken)
		{
			bool flag = this.GetAdaptation(phone.Type).CheckIfDeviceStillConnected(phone);
			Action<bool> deviceConnectionStatusRead = this.DeviceConnectionStatusRead;
			bool flag2 = deviceConnectionStatusRead != null;
			if (flag2)
			{
				deviceConnectionStatusRead(flag);
			}
		}

		// Token: 0x0400000B RID: 11
		private readonly List<BaseAdaptation> adaptationServices = new List<BaseAdaptation>();

		// Token: 0x0400000C RID: 12
		private readonly ManufacturerAutodetectionService manufacturerAutodetectionService;

		// Token: 0x0400000D RID: 13
		private readonly ReportingService reportingService;

		// Token: 0x0400000E RID: 14
		private bool disposed;

		// Token: 0x0400000F RID: 15
		private DetectionParameters detectionParameters;
	}
}
