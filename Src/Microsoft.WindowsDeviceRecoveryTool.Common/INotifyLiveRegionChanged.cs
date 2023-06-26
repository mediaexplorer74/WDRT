using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Common
{
	// Token: 0x02000006 RID: 6
	public interface INotifyLiveRegionChanged
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000E RID: 14
		// (remove) Token: 0x0600000F RID: 15
		event EventHandler LiveRegionChanged;
	}
}
