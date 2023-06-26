using System;
using System.Collections.Generic;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services
{
	// Token: 0x0200000C RID: 12
	public interface IManufacturerDataProvider
	{
		// Token: 0x06000082 RID: 130
		List<ManufacturerInfo> GetAdaptationsData();
	}
}
