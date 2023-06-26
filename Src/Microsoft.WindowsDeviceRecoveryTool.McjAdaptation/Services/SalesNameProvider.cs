using System;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;

namespace Microsoft.WindowsDeviceRecoveryTool.McjAdaptation.Services
{
	// Token: 0x02000006 RID: 6
	public class SalesNameProvider : BaseSalesNameProvider
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002BCC File Offset: 0x00000DCC
		public override string NameForVidPid(string vid, string pid)
		{
			return base.NameForVidPid(vid, pid);
		}
	}
}
