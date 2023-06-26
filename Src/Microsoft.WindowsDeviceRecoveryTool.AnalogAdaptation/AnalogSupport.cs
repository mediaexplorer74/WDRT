using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.AnalogAdaptation.Properties;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.Ffu;

namespace Microsoft.WindowsDeviceRecoveryTool.AnalogAdaptation
{
	// Token: 0x02000002 RID: 2
	[Export(typeof(IDeviceSupport))]
	internal class AnalogSupport : IDeviceSupport
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[ImportingConstructor]
		public AnalogSupport(IFfuDeviceInformationProvider ffuDeviceInformationProvider)
		{
			this.ffuDeviceInformationProvider = ffuDeviceInformationProvider;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
		public Guid Id
		{
			get
			{
				return AnalogSupport.SupportGuid;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000207C File Offset: 0x0000027C
		public DeviceDetectionInformation[] GetDeviceDetectionInformation()
		{
			return AnalogSupport.NormalModeDetectionInfos.Union(AnalogSupport.FlashModeDetectionInfos).ToArray<DeviceDetectionInformation>();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020A4 File Offset: 0x000002A4
		public async Task UpdateDeviceDetectionDataAsync(DeviceDetectionData detectionData, CancellationToken cancellationToken)
		{
			bool isDeviceSupported = detectionData.IsDeviceSupported;
			if (isDeviceSupported)
			{
				throw new InvalidOperationException("Device is already supported.");
			}
			VidPidPair vidPidPair = detectionData.VidPidPair;
			string devicePath = detectionData.UsbDeviceInterfaceDevicePath;
			bool flag = AnalogSupport.NormalModeDetectionInfos.Any((DeviceDetectionInformation di) => di.VidPidPair == vidPidPair);
			if (flag)
			{
				AnalogSupport.MarkDeviceDetectionDataAsSupported(detectionData);
			}
			else
			{
				bool flag2 = AnalogSupport.FlashModeDetectionInfos.Any((DeviceDetectionInformation di) => di.VidPidPair == vidPidPair);
				if (flag2)
				{
					AnalogSupport.<>c__DisplayClass9_1 CS$<>8__locals2 = new AnalogSupport.<>c__DisplayClass9_1();
					await Task.Delay(2000);
					FfuDeviceInformation ffuDeviceInformation = await this.ffuDeviceInformationProvider.ReadInformationAsync(devicePath, cancellationToken);
					FfuDeviceInformation information = ffuDeviceInformation;
					ffuDeviceInformation = null;
					CS$<>8__locals2.friendlyName = information.DeviceFriendlyName;
					if (AnalogSupport.PlatformIdMatches.Any((string match) => CS$<>8__locals2.friendlyName.IndexOf(match, StringComparison.OrdinalIgnoreCase) >= 0))
					{
						AnalogSupport.MarkDeviceDetectionDataAsSupported(detectionData);
					}
					CS$<>8__locals2 = null;
					information = null;
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F6 File Offset: 0x000002F6
		private static void MarkDeviceDetectionDataAsSupported(DeviceDetectionData detectionData)
		{
			detectionData.IsDeviceSupported = true;
			detectionData.DeviceBitmapBytes = Resources.Analog.ToBytes();
			detectionData.DeviceSalesName = Resources.HoloLens_SalesName;
		}

		// Token: 0x04000001 RID: 1
		private readonly IFfuDeviceInformationProvider ffuDeviceInformationProvider;

		// Token: 0x04000002 RID: 2
		private static readonly DeviceDetectionInformation[] NormalModeDetectionInfos = new DeviceDetectionInformation[]
		{
			new DeviceDetectionInformation(new VidPidPair("045E", "FFE5"), false),
			new DeviceDetectionInformation(new VidPidPair("045E", "0653"), false),
			new DeviceDetectionInformation(new VidPidPair("045E", "0652"), false)
		};

		// Token: 0x04000003 RID: 3
		private static readonly DeviceDetectionInformation[] FlashModeDetectionInfos = new DeviceDetectionInformation[]
		{
			new DeviceDetectionInformation(new VidPidPair("045E", "062A"), true)
		};

		// Token: 0x04000004 RID: 4
		private static readonly string[] PlatformIdMatches = new string[] { "sakura", "hololens" };

		// Token: 0x04000005 RID: 5
		private static readonly Guid SupportGuid = new Guid("60DAA760-9C63-46E1-B284-0B282D2A307A");
	}
}
