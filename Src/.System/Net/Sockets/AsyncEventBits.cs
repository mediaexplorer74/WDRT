using System;

namespace System.Net.Sockets
{
	// Token: 0x02000367 RID: 871
	[Flags]
	internal enum AsyncEventBits
	{
		// Token: 0x04001D87 RID: 7559
		FdNone = 0,
		// Token: 0x04001D88 RID: 7560
		FdRead = 1,
		// Token: 0x04001D89 RID: 7561
		FdWrite = 2,
		// Token: 0x04001D8A RID: 7562
		FdOob = 4,
		// Token: 0x04001D8B RID: 7563
		FdAccept = 8,
		// Token: 0x04001D8C RID: 7564
		FdConnect = 16,
		// Token: 0x04001D8D RID: 7565
		FdClose = 32,
		// Token: 0x04001D8E RID: 7566
		FdQos = 64,
		// Token: 0x04001D8F RID: 7567
		FdGroupQos = 128,
		// Token: 0x04001D90 RID: 7568
		FdRoutingInterfaceChange = 256,
		// Token: 0x04001D91 RID: 7569
		FdAddressListChange = 512,
		// Token: 0x04001D92 RID: 7570
		FdAllEvents = 1023
	}
}
