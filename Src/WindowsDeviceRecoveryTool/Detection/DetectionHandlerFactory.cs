using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.Core.Cache;
using Nokia.Lucid;
using Nokia.Lucid.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.Detection
{
	// Token: 0x020000BF RID: 191
	[Export]
	internal sealed class DetectionHandlerFactory
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x0001BEBE File Offset: 0x0001A0BE
		[ImportingConstructor]
		public DetectionHandlerFactory([ImportMany] IEnumerable<IDeviceSupport> deviceSupports, IDeviceInformationCacheManager deviceInformationCacheManager)
		{
			this.deviceSupports = deviceSupports;
			this.deviceInformationCacheManager = deviceInformationCacheManager;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001BED8 File Offset: 0x0001A0D8
		public IDetectionHandler CreateDetectionHandler()
		{
			Expression<Func<DeviceIdentifier, bool>> expression = this.CreateDeviceIdentifierFilter();
			UsbDeviceMonitor usbDeviceMonitor = UsbDeviceMonitor.StartNew(DetectionHandlerFactory.DefaultDeviceTypeMap, expression);
			return new DetectionHandler(usbDeviceMonitor, this.deviceSupports, this.deviceInformationCacheManager);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001BF10 File Offset: 0x0001A110
		private Expression<Func<DeviceIdentifier, bool>> CreateDeviceIdentifierFilter()
		{
			DeviceDetectionInformation[] vidPidArray = this.deviceSupports.SelectMany((IDeviceSupport support) => support.GetDeviceDetectionInformation()).ToArray<DeviceDetectionInformation>();
			return (DeviceIdentifier id) => vidPidArray.Any((DeviceDetectionInformation p) => id.Vid(p.VidPidPair.Vid) && id.Pid(p.VidPidPair.Pid));
		}

		// Token: 0x0400028D RID: 653
		public static readonly Guid UsbDeviceInterfaceClassGuid = new Guid("a5dcbf10-6530-11d2-901f-00c04fb951ed");

		// Token: 0x0400028E RID: 654
		private static readonly DeviceTypeMap DefaultDeviceTypeMap = new DeviceTypeMap(new Dictionary<Guid, DeviceType> { 
		{
			DetectionHandlerFactory.UsbDeviceInterfaceClassGuid,
			DeviceType.PhysicalDevice
		} });

		// Token: 0x0400028F RID: 655
		private readonly IEnumerable<IDeviceSupport> deviceSupports;

		// Token: 0x04000290 RID: 656
		private readonly IDeviceInformationCacheManager deviceInformationCacheManager;
	}
}
