using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200057B RID: 1403
	[FriendAccessAllowed]
	internal enum AsyncCausalityStatus
	{
		// Token: 0x04001B86 RID: 7046
		Canceled = 2,
		// Token: 0x04001B87 RID: 7047
		Completed = 1,
		// Token: 0x04001B88 RID: 7048
		Error = 3,
		// Token: 0x04001B89 RID: 7049
		Started = 0
	}
}
