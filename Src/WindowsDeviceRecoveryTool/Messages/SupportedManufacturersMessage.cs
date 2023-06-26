using System;
using System.Collections.Generic;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x02000097 RID: 151
	public class SupportedManufacturersMessage
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x0001B282 File Offset: 0x00019482
		public SupportedManufacturersMessage(List<ManufacturerInfo> adaptationsData)
		{
			this.Manufacturers = adaptationsData;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0001B294 File Offset: 0x00019494
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x0001B29C File Offset: 0x0001949C
		public List<ManufacturerInfo> Manufacturers { get; private set; }
	}
}
