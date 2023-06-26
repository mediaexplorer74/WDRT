using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.BusinessLogic.Services;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.LucidConnectivity;
using Microsoft.WindowsDeviceRecoveryTool.Lucid;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Nokia.Lucid;
using Nokia.Lucid.DeviceInformation;
using Nokia.Lucid.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.Detection.LegacySupport
{
	// Token: 0x020000C0 RID: 192
	[Export]
	internal sealed class PhoneFactory
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x0001C0A5 File Offset: 0x0001A2A5
		[ImportingConstructor]
		public PhoneFactory(AdaptationManager adaptationManager, ILucidService lucidService)
		{
			this.adaptationManager = adaptationManager;
			this.lucidService = lucidService;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001C0C0 File Offset: 0x0001A2C0
		public async Task<Phone> CreateAsync(DeviceInfo deviceInfo, CancellationToken cancellationToken)
		{
			Tracer<PhoneFactory>.WriteInformation("Create phone instance for device path: {0}, identifier: {1}", new object[] { deviceInfo.DeviceIdentifier, deviceInfo.DeviceIdentifier });
			VidPidPair vidPidPair = VidPidPair.Parse(deviceInfo.DeviceIdentifier);
			bool flag = !deviceInfo.IsDeviceSupported;
			Phone phone;
			if (flag)
			{
				Tracer<PhoneFactory>.WriteInformation("Device not supported");
				phone = PhoneFactory.CreateNotSupported(deviceInfo, vidPidPair);
			}
			else
			{
				Guid supportId = deviceInfo.SupportId;
				Tracer<PhoneFactory>.WriteInformation("Support ID {0}", new object[] { supportId });
				bool flag2 = supportId == PhoneFactory.LumiaSupportGuid;
				if (flag2)
				{
					Phone phone2 = await this.CreateLumiaAsync(deviceInfo, vidPidPair, cancellationToken);
					phone = phone2;
				}
				else
				{
					PhoneTypes phoneType = PhoneFactory.GuidToPhoneTypeDictionary[supportId];
					Tracer<PhoneFactory>.WriteInformation("Phone type: {0}", new object[] { Enum.GetName(typeof(PhoneTypes), phoneType) });
					Phone phone3 = await this.CreateAsync(phoneType, deviceInfo, vidPidPair, cancellationToken);
					phone = phone3;
				}
			}
			return phone;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001C114 File Offset: 0x0001A314
		private async Task<Phone> CreateAsync(PhoneTypes phoneType, DeviceInfo deviceInfo, VidPidPair vidPidPair, CancellationToken cancellationToken)
		{
			BaseAdaptation adaptation = this.adaptationManager.GetAdaptation(phoneType);
			DeviceInfo lucidDeviceInfo = this.GetDeviceInfo(deviceInfo, vidPidPair);
			UsbDevice usbDevice2 = await this.GetUsbDeviceAsync(lucidDeviceInfo, vidPidPair, deviceInfo.DeviceSalesName, cancellationToken);
			UsbDevice usbDevice = usbDevice2;
			usbDevice2 = null;
			Phone phone = new Phone(usbDevice, adaptation.PhoneType, adaptation.SalesNameProvider(), false, "", "");
			Phone modelFromAdaptation = adaptation.ManuallySupportedModels().FirstOrDefault((Phone p) => string.Equals(p.SalesName, phone.SalesName, StringComparison.OrdinalIgnoreCase));
			if (modelFromAdaptation != null)
			{
				phone.QueryParameters = modelFromAdaptation.QueryParameters;
				phone.ImageData = modelFromAdaptation.ImageData;
				phone.HardwareModel = modelFromAdaptation.HardwareModel;
				phone.ModelIdentificationInstruction = modelFromAdaptation.ModelIdentificationInstruction;
			}
			phone.ReportManufacturerName = adaptation.ReportManufacturerName;
			phone.ReportManufacturerProductLine = adaptation.ReportManufacturerProductLine;
			string[] array = new string[6];
			array[0] = "Created phone: ";
			array[1] = Environment.NewLine;
			array[2] = phone.HardwareModel;
			array[3] = Environment.NewLine;
			int num = 4;
			QueryParameters queryParameters = phone.QueryParameters;
			array[num] = ((queryParameters != null) ? queryParameters.ToString() : null);
			array[5] = Environment.NewLine;
			Tracer<PhoneFactory>.WriteInformation(string.Concat(array));
			return phone;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001C178 File Offset: 0x0001A378
		private DeviceInfo GetDeviceInfo(DeviceInfo deviceInfo, VidPidPair vidPidPair)
		{
			string vid = vidPidPair.Vid;
			string pid = vidPidPair.Pid;
			string deviceIdentifier = deviceInfo.DeviceIdentifier;
			return new DeviceInfoSet
			{
				Filter = (Nokia.Lucid.Primitives.DeviceIdentifier di) => di.Vid(vid) && di.Pid(pid),
				DeviceTypeMap = new DeviceTypeMap(PhoneFactory.UsbDeviceInterfaceClassGuid, DeviceType.PhysicalDevice)
			}.GetDevice(deviceIdentifier);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001C27C File Offset: 0x0001A47C
		private static Phone CreateNotSupported(DeviceInfo basicDeviceInformation, VidPidPair vidPidPair)
		{
			string deviceIdentifier = basicDeviceInformation.DeviceIdentifier;
			return new Phone(string.Empty, vidPidPair.Vid, vidPidPair.Pid, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, deviceIdentifier, PhoneTypes.UnknownWp, string.Empty, null, false, "", "");
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001C2D8 File Offset: 0x0001A4D8
		private async Task<UsbDevice> GetUsbDeviceAsync(DeviceInfo deviceInfo, VidPidPair vidPidPair, string friendlyName, CancellationToken cancellationToken)
		{
			try
			{
				string vid = vidPidPair.Vid;
				string pid = vidPidPair.Pid;
				Tracer<PhoneFactory>.WriteInformation("Getting USB device");
				string deviceId = deviceInfo.InstanceId;
				Tracer<PhoneFactory>.WriteInformation("Device detected: {0}", new object[] { deviceId });
				string locationPath = null;
				string locationInfo = null;
				string busReportedDeviceDescription = null;
				try
				{
					string text = await this.GetLocationPathAsync(deviceInfo, cancellationToken);
					locationPath = text;
					text = null;
					Tracer<PhoneFactory>.WriteInformation("Location path = {0}", new object[] { locationPath });
				}
				catch (Exception ex)
				{
					Tracer<PhoneFactory>.WriteWarning(ex, "Location path: not found", new object[0]);
				}
				try
				{
					busReportedDeviceDescription = deviceInfo.ReadBusReportedDeviceDescription();
					Tracer<PhoneFactory>.WriteInformation("Bus reported device description = {0}", new object[] { busReportedDeviceDescription });
				}
				catch (Exception ex2)
				{
					Tracer<PhoneFactory>.WriteWarning(ex2, "Bus reported device description: not found", new object[0]);
				}
				try
				{
					locationInfo = deviceInfo.ReadLocationInformation();
					Tracer<PhoneFactory>.WriteInformation("Location info = {0}", new object[] { locationInfo });
				}
				catch (Exception ex3)
				{
					Tracer<PhoneFactory>.WriteWarning(ex3, "Location info: not found", new object[0]);
				}
				if (string.IsNullOrEmpty(locationPath))
				{
					Tracer<PhoneFactory>.WriteWarning("Location path is empty", new object[0]);
					Tracer<PhoneFactory>.WriteError("Needed properties are not available", new object[0]);
					return null;
				}
				string portId = this.GetPhysicalPortId(locationPath, locationInfo);
				Tracer<PhoneFactory>.WriteInformation("USB device: {0}/{1}&{2}", new object[] { portId, vid, pid });
				return new UsbDevice(portId, vid, pid, locationPath, string.Empty, friendlyName, deviceInfo.Path, deviceInfo.InstanceId);
			}
			catch (Exception ex4)
			{
				Tracer<PhoneFactory>.WriteError(ex4, "Cannot get USB device", new object[0]);
			}
			Tracer<PhoneFactory>.WriteInformation("Device not compatible");
			return null;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001C339 File Offset: 0x0001A539
		internal void DetermineDeviceTypeDesignatorAndSalesName(string pid, string busReportedDeviceDescription, out string typeDesignator, out string salesName)
		{
			LucidConnectivityHelper.ParseTypeDesignatorAndSalesName(busReportedDeviceDescription, out typeDesignator, out salesName);
			Tracer<UsbDeviceScanner>.WriteInformation("Type designator: {0}, Sales name: {1}", new object[] { typeDesignator, salesName });
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001C364 File Offset: 0x0001A564
		private async Task<string> GetLocationPathAsync(IDevicePropertySet propertySet, CancellationToken cancellationToken)
		{
			for (int i = 0; i < 40; i++)
			{
				bool flag = i > 0;
				if (flag)
				{
					await Task.Delay(100 * i + 100, cancellationToken);
				}
				Tracer<UsbDeviceScanner>.WriteInformation("Reading location paths (attempt {0})", new object[] { i });
				try
				{
					propertySet.EnumeratePropertyKeys();
					Tracer<UsbDeviceScanner>.WriteInformation("Property keys enumerated");
					string[] locationPaths = propertySet.ReadLocationPaths();
					if (locationPaths.Length != 0)
					{
						Tracer<UsbDeviceScanner>.WriteInformation("Location path: {0}", new object[] { locationPaths[0] });
						return locationPaths[0];
					}
					locationPaths = null;
				}
				catch (Exception ex)
				{
					Tracer<UsbDeviceScanner>.WriteWarning(ex, "Location paths not found", new object[0]);
					if (i < 39)
					{
						Tracer<UsbDeviceScanner>.WriteWarning("Retrying after delay", new object[0]);
					}
				}
			}
			Tracer<UsbDeviceScanner>.WriteError("Location paths not found (after all retries).", new object[0]);
			return string.Empty;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001C3B8 File Offset: 0x0001A5B8
		internal string GetPhysicalPortId(string locationPath, string locationInfo)
		{
			string text = string.Empty;
			bool flag = !string.IsNullOrEmpty(locationPath);
			if (flag)
			{
				text = LucidConnectivityHelper.LocationPath2ControllerId(locationPath);
			}
			string text2 = string.Format("{0}:{1}", text, LucidConnectivityHelper.GetHubAndPort(locationInfo));
			Tracer<UsbDeviceScanner>.WriteInformation("Parsed port identifier: {0}", new object[] { text2 });
			return text2;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001C410 File Offset: 0x0001A610
		private async Task<Phone> CreateLumiaAsync(DeviceInfo deviceInfo, VidPidPair vidPidPair, CancellationToken cancellationToken)
		{
			Tracer<PhoneFactory>.WriteInformation("Create Lumia instance for device path {0}", new object[] { deviceInfo.DeviceIdentifier });
			string usbDeviceInterfaceDevicePath = deviceInfo.DeviceIdentifier;
			BaseAdaptation adaptation = this.adaptationManager.GetAdaptation(PhoneTypes.Lumia);
			string ncsdDevicePath = null;
			bool flag = vidPidPair == PhoneFactory.MsftVidPid;
			if (flag)
			{
				Tracer<PhoneFactory>.WriteInformation(string.Format("Create WP10 Lumia", new object[0]));
				string text = await this.lucidService.TakeFirstDevicePathForInterfaceGuidAsync(usbDeviceInterfaceDevicePath, PhoneFactory.MsftNcsdGuid, cancellationToken);
				ncsdDevicePath = text;
				text = null;
			}
			else if (vidPidPair == PhoneFactory.BluVidPid)
			{
				Tracer<PhoneFactory>.WriteInformation(string.Format("Create BLU Lumia", new object[0]));
				string text2 = await this.lucidService.TakeFirstDevicePathForInterfaceGuidAsync(usbDeviceInterfaceDevicePath, PhoneFactory.BluNcsdGuid, cancellationToken);
				ncsdDevicePath = text2;
				text2 = null;
			}
			else
			{
				if (!(vidPidPair == PhoneFactory.LegacyOrApolloVidPid))
				{
					throw new InvalidOperationException();
				}
				using (CancellationTokenSource internalCancellationToken = new CancellationTokenSource())
				{
					using (CancellationTokenSource linkedToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, internalCancellationToken.Token))
					{
						Tracer<PhoneFactory>.WriteInformation("Create Legacy or Apollo Lumia");
						Task<string> legacyDevicesTask = this.lucidService.TakeFirstDevicePathForInterfaceGuidAsync(usbDeviceInterfaceDevicePath, PhoneFactory.CareConnectivityGuid, 2, linkedToken.Token);
						Task<string> apolloDevicesTask = this.lucidService.TakeFirstDevicePathForInterfaceGuidAsync(usbDeviceInterfaceDevicePath, PhoneFactory.ApolloGuid, 3, linkedToken.Token);
						Task<string> task = await Task.WhenAny<string>(new Task<string>[] { legacyDevicesTask, apolloDevicesTask });
						Task<string> complitedTask = task;
						task = null;
						string text3 = await complitedTask;
						ncsdDevicePath = text3;
						text3 = null;
						Task<string> toCancelTask = ((complitedTask == apolloDevicesTask) ? legacyDevicesTask : apolloDevicesTask);
						internalCancellationToken.Cancel();
						try
						{
							await toCancelTask;
						}
						catch (Exception)
						{
						}
						legacyDevicesTask = null;
						apolloDevicesTask = null;
						complitedTask = null;
						toCancelTask = null;
					}
					CancellationTokenSource linkedToken = null;
				}
				CancellationTokenSource internalCancellationToken = null;
			}
			Tracer<PhoneFactory>.WriteInformation("Ncsd device path: {0}", new object[] { ncsdDevicePath });
			DeviceInfo lucidDeviceInfo = this.lucidService.GetDeviceInfoForInterfaceGuid(usbDeviceInterfaceDevicePath, WellKnownGuids.UsbDeviceInterfaceGuid);
			UsbDevice usbDevice2 = await this.GetUsbDeviceAsync(lucidDeviceInfo, vidPidPair, deviceInfo.DeviceSalesName, cancellationToken);
			UsbDevice usbDevice = usbDevice2;
			usbDevice2 = null;
			return new Phone(usbDevice, adaptation.PhoneType, adaptation.SalesNameProvider(), false, "", "")
			{
				LocationPath = ncsdDevicePath
			};
		}

		// Token: 0x04000291 RID: 657
		private static readonly Guid UsbDeviceInterfaceClassGuid = new Guid("a5dcbf10-6530-11d2-901f-00c04fb951ed");

		// Token: 0x04000292 RID: 658
		private static readonly Guid HtcSupportGuid = new Guid("44A15B98-32C3-4641-A695-FE897AAF4777");

		// Token: 0x04000293 RID: 659
		private static readonly Guid BluSupportGuid = new Guid("93AB08C4-B626-420D-BCD8-B4C3D45B1B04");

		// Token: 0x04000294 RID: 660
		private static readonly Guid McjSupportGuid = new Guid("E1213532-3425-4DD8-A468-0E72A75DEF04");

		// Token: 0x04000295 RID: 661
		private static readonly Guid LumiaSupportGuid = new Guid("AC04B553-A566-4D49-A097-4CC73ED820F3");

		// Token: 0x04000296 RID: 662
		private static readonly Guid LgeSupportGuid = new Guid("77D0B92B-C020-4163-AB74-B251F5C32EEA");

		// Token: 0x04000297 RID: 663
		private static readonly Guid AlcatelSupportGuid = new Guid("C40DAE33-E5D2-4778-807A-903A343F610F");

		// Token: 0x04000298 RID: 664
		private static readonly Guid AnalogSupportGuid = new Guid("60DAA760-9C63-46E1-B284-0B282D2A307A");

		// Token: 0x04000299 RID: 665
		private static readonly Guid FawkesSupportGuid = new Guid("58AE8994-C31B-49D9-B23C-F7FAB49ADB6E");

		// Token: 0x0400029A RID: 666
		private static readonly Guid AcerSupportGuid = new Guid("5DD6D8CE-DAB9-4E27-8846-669B343E89BE");

		// Token: 0x0400029B RID: 667
		private static readonly Guid TrinitySupportGuid = new Guid("B9A97DB5-DC7F-4CB9-A8E3-53FE6D1958C4");

		// Token: 0x0400029C RID: 668
		private static readonly Guid UnistrongSupportGuid = new Guid("7AD442A9-8CA3-4C4D-8137-64275B61EE9D");

		// Token: 0x0400029D RID: 669
		private static readonly Guid YEZZSupportGuid = new Guid("BA2D8739-0A4E-4AE7-8287-9D0E31E1F391");

		// Token: 0x0400029E RID: 670
		private static readonly Guid VAIOSupportGuid = new Guid("1097789E-98FF-4215-AF33-5D3A8CD286B4");

		// Token: 0x0400029F RID: 671
		private static readonly Guid InversenetSupportGuid = new Guid("D6CAAC94-CC9E-4C20-AE94-104F3B705803");

		// Token: 0x040002A0 RID: 672
		private static readonly Guid FreetelSupportGuid = new Guid("37EB525A-9379-43C8-9B48-B5833CFEBD10");

		// Token: 0x040002A1 RID: 673
		private static readonly Guid FunkerSupportGuid = new Guid("C56B7774-C84F-4FC6-BACD-5D80C035A936");

		// Token: 0x040002A2 RID: 674
		private static readonly Guid DiginnosSupportGuid = new Guid("B06E0686-FA6A-417B-B16F-B9E626CDAA71");

		// Token: 0x040002A3 RID: 675
		private static readonly Guid MicromaxSupportGuid = new Guid("60D7BEDB-5A6B-4407-B172-608E611D650E");

		// Token: 0x040002A4 RID: 676
		private static readonly Guid XOLOSupportGuid = new Guid("8AAF423B-EA22-43A4-9077-F9AAB42EFFDF");

		// Token: 0x040002A5 RID: 677
		private static readonly Guid KMSupportGuid = new Guid("BE6723FE-2BC1-462B-9B75-D82C06787989");

		// Token: 0x040002A6 RID: 678
		private static readonly Guid JenesisSupportGuid = new Guid("51E93B39-75D1-4F52-9F0F-D7BA579DAEE1");

		// Token: 0x040002A7 RID: 679
		private static readonly Guid GomobileSupportGuid = new Guid("FEF350C9-0D34-422C-95AA-6E42CF539B6D");

		// Token: 0x040002A8 RID: 680
		private static readonly Guid HPSupportGuid = new Guid("2B5046B7-716B-42C2-ACBA-E5D5F1624334");

		// Token: 0x040002A9 RID: 681
		private static readonly Guid LenovoSupportGuid = new Guid("3E21FD89-D879-4D23-9E4B-67E2155F074C");

		// Token: 0x040002AA RID: 682
		private static readonly Guid ZebraSupportGuid = new Guid("E41DFB86-BD53-46DF-88BD-BECE11D45A12");

		// Token: 0x040002AB RID: 683
		private static readonly Guid HoneywellSupportGuid = new Guid("426BA302-E393-40BA-9CBA-C041CDA02EF4");

		// Token: 0x040002AC RID: 684
		private static readonly Guid PanasonicSupportGuid = new Guid("4FB4AEC9-823A-424F-BB77-530FE89341CD");

		// Token: 0x040002AD RID: 685
		private static readonly Guid TrekStorSupportGuid = new Guid("C3D0AA61-0D19-41C8-AD30-99D46A4CAB60");

		// Token: 0x040002AE RID: 686
		private static readonly Guid WileyfoxSupportGuid = new Guid("A578DCBE-0781-45BE-AD4B-C1F8C018B8E1");

		// Token: 0x040002AF RID: 687
		private static readonly Dictionary<Guid, PhoneTypes> GuidToPhoneTypeDictionary = new Dictionary<Guid, PhoneTypes>
		{
			{
				PhoneFactory.HtcSupportGuid,
				PhoneTypes.Htc
			},
			{
				PhoneFactory.BluSupportGuid,
				PhoneTypes.Blu
			},
			{
				PhoneFactory.McjSupportGuid,
				PhoneTypes.Mcj
			},
			{
				PhoneFactory.LumiaSupportGuid,
				PhoneTypes.Lumia
			},
			{
				PhoneFactory.LgeSupportGuid,
				PhoneTypes.Lg
			},
			{
				PhoneFactory.AlcatelSupportGuid,
				PhoneTypes.Alcatel
			},
			{
				PhoneFactory.AnalogSupportGuid,
				PhoneTypes.Analog
			},
			{
				PhoneFactory.FawkesSupportGuid,
				PhoneTypes.HoloLensAccessory
			},
			{
				PhoneFactory.AcerSupportGuid,
				PhoneTypes.Acer
			},
			{
				PhoneFactory.TrinitySupportGuid,
				PhoneTypes.Trinity
			},
			{
				PhoneFactory.UnistrongSupportGuid,
				PhoneTypes.Unistrong
			},
			{
				PhoneFactory.YEZZSupportGuid,
				PhoneTypes.YEZZ
			},
			{
				PhoneFactory.VAIOSupportGuid,
				PhoneTypes.VAIO
			},
			{
				PhoneFactory.InversenetSupportGuid,
				PhoneTypes.Inversenet
			},
			{
				PhoneFactory.FreetelSupportGuid,
				PhoneTypes.Freetel
			},
			{
				PhoneFactory.FunkerSupportGuid,
				PhoneTypes.Funker
			},
			{
				PhoneFactory.DiginnosSupportGuid,
				PhoneTypes.Diginnos
			},
			{
				PhoneFactory.MicromaxSupportGuid,
				PhoneTypes.Micromax
			},
			{
				PhoneFactory.XOLOSupportGuid,
				PhoneTypes.XOLO
			},
			{
				PhoneFactory.KMSupportGuid,
				PhoneTypes.KM
			},
			{
				PhoneFactory.JenesisSupportGuid,
				PhoneTypes.Jenesis
			},
			{
				PhoneFactory.GomobileSupportGuid,
				PhoneTypes.Gomobile
			},
			{
				PhoneFactory.HPSupportGuid,
				PhoneTypes.HP
			},
			{
				PhoneFactory.LenovoSupportGuid,
				PhoneTypes.Lenovo
			},
			{
				PhoneFactory.ZebraSupportGuid,
				PhoneTypes.Zebra
			},
			{
				PhoneFactory.HoneywellSupportGuid,
				PhoneTypes.Honeywell
			},
			{
				PhoneFactory.PanasonicSupportGuid,
				PhoneTypes.Panasonic
			},
			{
				PhoneFactory.TrekStorSupportGuid,
				PhoneTypes.TrekStor
			},
			{
				PhoneFactory.WileyfoxSupportGuid,
				PhoneTypes.Wileyfox
			}
		};

		// Token: 0x040002B0 RID: 688
		private readonly AdaptationManager adaptationManager;

		// Token: 0x040002B1 RID: 689
		private readonly ILucidService lucidService;

		// Token: 0x040002B2 RID: 690
		private static readonly VidPidPair LegacyOrApolloVidPid = new VidPidPair("0421", "0661");

		// Token: 0x040002B3 RID: 691
		private static readonly Guid CareConnectivityGuid = new Guid("{0fd3b15c-d457-45d8-a779-c2b2c9f9d0fd}");

		// Token: 0x040002B4 RID: 692
		private static readonly Guid ApolloGuid = new Guid("{7eaff726-34cc-4204-b09d-f95471b873cf}");

		// Token: 0x040002B5 RID: 693
		private static readonly VidPidPair BluVidPid = new VidPidPair("0421", "06FC");

		// Token: 0x040002B6 RID: 694
		private static readonly Guid BluNcsdGuid = new Guid("{08324f9c-b621-435c-859b-ae4652481b7c}");

		// Token: 0x040002B7 RID: 695
		private static readonly VidPidPair MsftVidPid = new VidPidPair("045E", "0A00");

		// Token: 0x040002B8 RID: 696
		private static readonly Guid MsftNcsdGuid = new Guid("{08324f9c-b621-435c-859b-ae4652481b7c}");
	}
}
