using System;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;

namespace Microsoft.WindowsDeviceRecoveryTool.LgeAdaptation.Services
{
	// Token: 0x02000005 RID: 5
	public class SalesNameProvider : BaseSalesNameProvider
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002860 File Offset: 0x00000A60
		public override string NameForVidPid(string vid, string pid)
		{
			bool flag = string.Compare(vid, "1004", StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(pid, "627E", StringComparison.OrdinalIgnoreCase) == 0;
			string text;
			if (flag)
			{
				text = "LG Lancet";
			}
			else
			{
				text = base.NameForVidPid(vid, pid);
			}
			return text;
		}
	}
}
