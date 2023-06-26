using System;

namespace System.Net.Mail
{
	// Token: 0x0200025D RID: 605
	internal enum ServerState
	{
		// Token: 0x04001764 RID: 5988
		Starting = 1,
		// Token: 0x04001765 RID: 5989
		Started,
		// Token: 0x04001766 RID: 5990
		Stopping,
		// Token: 0x04001767 RID: 5991
		Stopped,
		// Token: 0x04001768 RID: 5992
		Pausing,
		// Token: 0x04001769 RID: 5993
		Paused,
		// Token: 0x0400176A RID: 5994
		Continuing
	}
}
