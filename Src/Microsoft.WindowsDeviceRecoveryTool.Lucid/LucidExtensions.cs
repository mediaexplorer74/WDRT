using System;
using Nokia.Lucid.DeviceInformation;

namespace Microsoft.WindowsDeviceRecoveryTool.Lucid
{
	// Token: 0x02000003 RID: 3
	internal static class LucidExtensions
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
		internal static Guid ReadClassGuid(this IDevicePropertySet propertySet)
		{
			return (Guid)propertySet.ReadProperty(LucidExtensions.DEVPKEY_Device_ClassGuid, PropertyValueFormatter.Default);
		}

		// Token: 0x04000001 RID: 1
		private static readonly PropertyKey DEVPKEY_Device_ClassGuid = new PropertyKey(2757502286U, 57116, 20221, 128, 32, 103, 209, 70, 168, 80, 224, 10);
	}
}
