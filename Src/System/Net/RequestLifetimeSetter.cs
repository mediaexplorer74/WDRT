using System;

namespace System.Net
{
	// Token: 0x020001E5 RID: 485
	internal class RequestLifetimeSetter
	{
		// Token: 0x060012D3 RID: 4819 RVA: 0x00063CF2 File Offset: 0x00061EF2
		internal RequestLifetimeSetter(long requestStartTimestamp)
		{
			this.m_RequestStartTimestamp = requestStartTimestamp;
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00063D01 File Offset: 0x00061F01
		internal static void Report(RequestLifetimeSetter tracker)
		{
			if (tracker != null)
			{
				NetworkingPerfCounters.Instance.IncrementAverage(NetworkingPerfCounterName.HttpWebRequestAvgLifeTime, tracker.m_RequestStartTimestamp);
			}
		}

		// Token: 0x0400151C RID: 5404
		private long m_RequestStartTimestamp;
	}
}
