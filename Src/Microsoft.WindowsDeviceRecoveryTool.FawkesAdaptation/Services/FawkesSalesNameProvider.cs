using System;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;

namespace Microsoft.WindowsDeviceRecoveryTool.FawkesAdaptation.Services
{
	// Token: 0x0200000B RID: 11
	public class FawkesSalesNameProvider : BaseSalesNameProvider
	{
		// Token: 0x0600004E RID: 78 RVA: 0x000033CE File Offset: 0x000015CE
		public override string NameForString(string text)
		{
			return base.NameForString(text);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000033D7 File Offset: 0x000015D7
		public override string NameForTypeDesignator(string typeDesignator)
		{
			return base.NameForTypeDesignator(typeDesignator);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000033E0 File Offset: 0x000015E0
		public override string NameForVidPid(string vid, string pid)
		{
			return base.NameForVidPid(vid, pid);
		}
	}
}
