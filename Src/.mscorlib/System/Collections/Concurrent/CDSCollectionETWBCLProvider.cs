using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

namespace System.Collections.Concurrent
{
	// Token: 0x020004AB RID: 1195
	[FriendAccessAllowed]
	[EventSource(Name = "System.Collections.Concurrent.ConcurrentCollectionsEventSource", Guid = "35167F8E-49B2-4b96-AB86-435B59336B5E", LocalizationResources = "mscorlib")]
	internal sealed class CDSCollectionETWBCLProvider : EventSource
	{
		// Token: 0x06003940 RID: 14656 RVA: 0x000DC34E File Offset: 0x000DA54E
		private CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000DC356 File Offset: 0x000DA556
		[Event(1, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPushFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(1, spinCount);
			}
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000DC36B File Offset: 0x000DA56B
		[Event(2, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPopFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(2, spinCount);
			}
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000DC380 File Offset: 0x000DA580
		[Event(3, Level = EventLevel.Warning)]
		public void ConcurrentDictionary_AcquiringAllLocks(int numOfBuckets)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(3, numOfBuckets);
			}
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000DC395 File Offset: 0x000DA595
		[Event(4, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryTakeSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(4);
			}
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000DC3A9 File Offset: 0x000DA5A9
		[Event(5, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryPeekSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(5);
			}
		}

		// Token: 0x0400191C RID: 6428
		public static CDSCollectionETWBCLProvider Log = new CDSCollectionETWBCLProvider();

		// Token: 0x0400191D RID: 6429
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x0400191E RID: 6430
		private const int CONCURRENTSTACK_FASTPUSHFAILED_ID = 1;

		// Token: 0x0400191F RID: 6431
		private const int CONCURRENTSTACK_FASTPOPFAILED_ID = 2;

		// Token: 0x04001920 RID: 6432
		private const int CONCURRENTDICTIONARY_ACQUIRINGALLLOCKS_ID = 3;

		// Token: 0x04001921 RID: 6433
		private const int CONCURRENTBAG_TRYTAKESTEALS_ID = 4;

		// Token: 0x04001922 RID: 6434
		private const int CONCURRENTBAG_TRYPEEKSTEALS_ID = 5;
	}
}
