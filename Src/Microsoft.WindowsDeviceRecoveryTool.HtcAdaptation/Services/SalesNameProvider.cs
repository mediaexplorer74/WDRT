using System;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;

namespace Microsoft.WindowsDeviceRecoveryTool.HtcAdaptation.Services
{
	// Token: 0x02000007 RID: 7
	public class SalesNameProvider : BaseSalesNameProvider
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000033FC File Offset: 0x000015FC
		public override string NameForString(string text)
		{
			bool flag = text.Equals("USB BLDR");
			string text2;
			if (flag)
			{
				text2 = "HTC";
			}
			else
			{
				text2 = base.NameForString(text);
			}
			return text2;
		}
	}
}
