using System;
using System.Collections.Generic;

namespace Microsoft.WindowsDeviceRecoveryTool.Core
{
	// Token: 0x02000007 RID: 7
	public class VidPidPairEqualityComparer : IEqualityComparer<VidPidPair>
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002268 File Offset: 0x00000468
		public bool Equals(VidPidPair x, VidPidPair y)
		{
			return (x == null && y == null) || (!(x == null) && !(y == null) && string.Equals(x.Pid, y.Pid, StringComparison.InvariantCultureIgnoreCase) && string.Equals(x.Vid, y.Vid, StringComparison.CurrentCultureIgnoreCase));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000022C5 File Offset: 0x000004C5
		public int GetHashCode(VidPidPair obj)
		{
			return (obj.Pid + obj.Vid).GetHashCode();
		}
	}
}
