using System;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon
{
	// Token: 0x02000006 RID: 6
	public class BaseSalesNameProvider : ISalesNameProvider
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002768 File Offset: 0x00000968
		public virtual string NameForVidPid(string vid, string pid)
		{
			return string.Empty;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002780 File Offset: 0x00000980
		public virtual string NameForString(string text)
		{
			return string.Empty;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002798 File Offset: 0x00000998
		public virtual string NameForTypeDesignator(string typeDesignator)
		{
			return string.Empty;
		}
	}
}
