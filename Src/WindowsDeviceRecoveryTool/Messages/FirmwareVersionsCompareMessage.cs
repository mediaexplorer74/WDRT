using System;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000A4 RID: 164
	public class FirmwareVersionsCompareMessage
	{
		// Token: 0x06000558 RID: 1368 RVA: 0x0001B494 File Offset: 0x00019694
		public FirmwareVersionsCompareMessage(SwVersionComparisonResult status)
		{
			this.Status = status;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001B4A6 File Offset: 0x000196A6
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0001B4AE File Offset: 0x000196AE
		public SwVersionComparisonResult Status { get; set; }
	}
}
