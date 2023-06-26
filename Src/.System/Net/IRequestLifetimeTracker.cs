using System;

namespace System.Net
{
	// Token: 0x020001B7 RID: 439
	internal interface IRequestLifetimeTracker
	{
		// Token: 0x06001138 RID: 4408
		void TrackRequestLifetime(long requestStartTimestamp);
	}
}
