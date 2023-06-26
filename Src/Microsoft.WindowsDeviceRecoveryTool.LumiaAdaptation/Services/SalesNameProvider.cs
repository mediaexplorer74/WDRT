using System;
using System.Collections.Generic;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;

namespace Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Services
{
	// Token: 0x02000008 RID: 8
	public class SalesNameProvider : BaseSalesNameProvider
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00004F88 File Offset: 0x00003188
		public override string NameForVidPid(string vid, string pid)
		{
			bool flag = string.Compare(vid, "0421", StringComparison.OrdinalIgnoreCase) == 0 && (string.Compare(pid, "066E", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(pid, "0714", StringComparison.OrdinalIgnoreCase) == 0);
			string text;
			if (flag)
			{
				text = "DeviceInUefiMode";
			}
			else
			{
				text = base.NameForVidPid(vid, pid);
			}
			return text;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004FE0 File Offset: 0x000031E0
		public override string NameForTypeDesignator(string typeDesignator)
		{
			bool flag = this.lumia1020List.Contains(typeDesignator.ToUpper());
			string text;
			if (flag)
			{
				text = "Nokia Lumia 1020";
			}
			else
			{
				text = base.NameForTypeDesignator(typeDesignator);
			}
			return text;
		}

		// Token: 0x04000028 RID: 40
		private readonly List<string> lumia1020List = new List<string> { "RM-875", "RM-876", "RM-877" };
	}
}
