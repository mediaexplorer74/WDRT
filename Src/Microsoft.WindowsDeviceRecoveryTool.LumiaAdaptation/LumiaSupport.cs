using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.Lucid;
using Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Properties;
using Nokia.Lucid.DeviceInformation;

namespace Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation
{
	// Token: 0x02000003 RID: 3
	[Export(typeof(IDeviceSupport))]
	internal class LumiaSupport : IDeviceSupport
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020A4 File Offset: 0x000002A4
		[ImportingConstructor]
		public LumiaSupport(ILucidService lucidService)
		{
			this.lucidService = lucidService;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		public Guid Id
		{
			get
			{
				return LumiaSupport.SupportGuid;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public DeviceDetectionInformation[] GetDeviceDetectionInformation()
		{
			return new VidPidPair[]
			{
				LumiaSupport.LegacyOrApolloVidPid,
				LumiaSupport.BluVidPid,
				LumiaSupport.MsftVidPid
			}.Select((VidPidPair vp) => new DeviceDetectionInformation(vp, false)).ToArray<DeviceDetectionInformation>();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000212C File Offset: 0x0000032C
		public async Task UpdateDeviceDetectionDataAsync(DeviceDetectionData detectionData, CancellationToken cancellationToken)
		{
			bool isDeviceSupported = detectionData.IsDeviceSupported;
			if (isDeviceSupported)
			{
				throw new InvalidOperationException("Device is already supported.");
			}
			VidPidPair vidPid = detectionData.VidPidPair;
			string devicePath = detectionData.UsbDeviceInterfaceDevicePath;
			bool flag = vidPid == LumiaSupport.MsftVidPid;
			Bitmap bitmap;
			if (flag)
			{
				bitmap = Resources.MicrosoftLumia;
			}
			else
			{
				bool flag2 = vidPid == LumiaSupport.BluVidPid || vidPid == LumiaSupport.LegacyOrApolloVidPid;
				if (!flag2)
				{
					return;
				}
				bitmap = Resources.NokiaLumia;
			}
			DeviceInfo deviceInfo = this.lucidService.GetDeviceInfoForInterfaceGuid(devicePath, WellKnownGuids.UsbDeviceInterfaceGuid);
			string busReportedDeviceDescription = deviceInfo.ReadBusReportedDeviceDescription();
			string productType;
			string salesName;
			LumiaSupport.ParseTypeDesignatorAndSalesName(busReportedDeviceDescription, out productType, out salesName);
			string hardcodedSalesName;
			bool flag3 = LumiaSupport.SalesNamesDictionary.TryGetValue(productType, out hardcodedSalesName);
			if (flag3)
			{
				salesName = hardcodedSalesName;
			}
			byte[] imageBytes = bitmap.ToBytes();
			detectionData.DeviceSalesName = salesName;
			detectionData.DeviceBitmapBytes = imageBytes;
			detectionData.IsDeviceSupported = true;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002180 File Offset: 0x00000380
		private static void ParseTypeDesignatorAndSalesName(string busReportedDeviceDescription, out string productType, out string salesName)
		{
			productType = string.Empty;
			salesName = string.Empty;
			bool flag = busReportedDeviceDescription.Contains("|");
			if (flag)
			{
				string[] array = busReportedDeviceDescription.Split(new char[] { '|' });
				productType = ((array.Length != 0) ? array[0] : string.Empty);
				salesName = ((array.Length > 1) ? array[1] : string.Empty);
			}
			else
			{
				bool flag2 = busReportedDeviceDescription.Contains("(") && busReportedDeviceDescription.Contains(")");
				if (flag2)
				{
					int num = busReportedDeviceDescription.LastIndexOf('(');
					int num2 = busReportedDeviceDescription.IndexOf(')', Math.Max(num, 0));
					int num3 = busReportedDeviceDescription.IndexOf(" (", StringComparison.InvariantCulture);
					bool flag3 = num > -1 && num2 > num;
					if (flag3)
					{
						productType = busReportedDeviceDescription.Substring(num + 1, num2 - num - 1);
					}
					bool flag4 = num3 > -1;
					if (flag4)
					{
						salesName = busReportedDeviceDescription.Substring(0, num3);
					}
				}
				else
				{
					salesName = busReportedDeviceDescription;
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly ILucidService lucidService;

		// Token: 0x04000002 RID: 2
		private static readonly Dictionary<string, string> SalesNamesDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{ "RM-875", "Nokia Lumia 1020" },
			{ "RM-876", "Nokia Lumia 1020" },
			{ "RM-877", "Nokia Lumia 1020" }
		};

		// Token: 0x04000003 RID: 3
		private static readonly Guid SupportGuid = new Guid("AC04B553-A566-4D49-A097-4CC73ED820F3");

		// Token: 0x04000004 RID: 4
		private static readonly VidPidPair LegacyOrApolloVidPid = new VidPidPair("0421", "0661");

		// Token: 0x04000005 RID: 5
		private static readonly VidPidPair BluVidPid = new VidPidPair("0421", "06FC");

		// Token: 0x04000006 RID: 6
		private static readonly VidPidPair MsftVidPid = new VidPidPair("045E", "0A00");
	}
}
