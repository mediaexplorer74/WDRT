using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Nokia.Lucid.DeviceInformation;

namespace Microsoft.WindowsDeviceRecoveryTool.Lucid.Mtp
{
	// Token: 0x0200000A RID: 10
	[Export]
	internal sealed class MtpDeviceInfoProvider : IMtpDeviceInfoProvider, IDeviceInformationProvider<MtpInterfaceInfo>
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000026EA File Offset: 0x000008EA
		[ImportingConstructor]
		public MtpDeviceInfoProvider(ILucidService lucidService)
		{
			this.lucidService = lucidService;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026FC File Offset: 0x000008FC
		public async Task<MtpInterfaceInfo> ReadInformationAsync(string devicePath, CancellationToken cancellationToken)
		{
			string text = await this.lucidService.TakeFirstDevicePathForDeviceAndInterfaceGuidsAsync(devicePath, WellKnownGuids.MtpInterfaceGuid, WellKnownGuids.WpdDeviceGuid, cancellationToken);
			DeviceInfo deviceInfoForInterfaceGuid = this.lucidService.GetDeviceInfoForInterfaceGuid(text, WellKnownGuids.MtpInterfaceGuid);
			string text2 = deviceInfoForInterfaceGuid.ReadManufacturer();
			string text3 = deviceInfoForInterfaceGuid.ReadDescription();
			Tracer<MtpDeviceInfoProvider>.WriteInformation("Read MTP info for device path: {0}. Manufacturer: {1}, Description: {2}", new object[] { text, text2, text3 });
			return new MtpInterfaceInfo(text3, text2);
		}

		// Token: 0x0400000A RID: 10
		private readonly ILucidService lucidService;
	}
}
