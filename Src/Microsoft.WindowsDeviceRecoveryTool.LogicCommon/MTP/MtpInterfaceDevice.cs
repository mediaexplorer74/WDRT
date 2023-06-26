using System;
using System.Linq.Expressions;
using Nokia.Lucid;
using Nokia.Lucid.DeviceInformation;
using Nokia.Lucid.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.MTP
{
	// Token: 0x02000024 RID: 36
	public sealed class MtpInterfaceDevice
	{
		// Token: 0x06000288 RID: 648 RVA: 0x00008F16 File Offset: 0x00007116
		public MtpInterfaceDevice(string devicePath)
		{
			this.devicePath = devicePath;
			this.deviceInfoSet = MtpInterfaceDevice.GetDeviceInfoSet();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00008F34 File Offset: 0x00007134
		public string ReadDescriptionFromDriver()
		{
			DeviceInfo device = this.deviceInfoSet.GetDevice(this.devicePath);
			return device.ReadDescription();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008F60 File Offset: 0x00007160
		public string ReadManufacturerFromDriver()
		{
			DeviceInfo device = this.deviceInfoSet.GetDevice(this.devicePath);
			return device.ReadManufacturer();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00008F8C File Offset: 0x0000718C
		private static DeviceInfoSet GetDeviceInfoSet()
		{
			return new DeviceInfoSet
			{
				DeviceTypeMap = MtpInterfaceDevice.GetDeviceTypeMap(),
				Filter = MtpInterfaceDevice.GetFilter()
			};
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00008FBC File Offset: 0x000071BC
		private static DeviceTypeMap GetDeviceTypeMap()
		{
			return new DeviceTypeMap(new Guid("6ac27878-a6fa-4155-ba85-f98f491d4f33"), DeviceType.Interface);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008FE0 File Offset: 0x000071E0
		private static Expression<Func<DeviceIdentifier, bool>> GetFilter()
		{
			return (DeviceIdentifier identifier) => true;
		}

		// Token: 0x04000112 RID: 274
		private readonly string devicePath;

		// Token: 0x04000113 RID: 275
		private readonly DeviceInfoSet deviceInfoSet;

		// Token: 0x04000114 RID: 276
		private const string MtpInterfaceGuid = "6ac27878-a6fa-4155-ba85-f98f491d4f33";
	}
}
