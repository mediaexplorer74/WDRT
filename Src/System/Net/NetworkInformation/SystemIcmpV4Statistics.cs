using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F2 RID: 754
	internal class SystemIcmpV4Statistics : IcmpV4Statistics
	{
		// Token: 0x06001A5A RID: 6746 RVA: 0x0007FCEC File Offset: 0x0007DEEC
		internal SystemIcmpV4Statistics()
		{
			uint icmpStatistics = UnsafeNetInfoNativeMethods.GetIcmpStatistics(out this.stats);
			if (icmpStatistics != 0U)
			{
				throw new NetworkInformationException((int)icmpStatistics);
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x0007FD15 File Offset: 0x0007DF15
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.messages);
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0007FD28 File Offset: 0x0007DF28
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.messages);
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x0007FD3B File Offset: 0x0007DF3B
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.errors);
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x0007FD4E File Offset: 0x0007DF4E
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.errors);
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x0007FD61 File Offset: 0x0007DF61
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.destinationUnreachables);
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x0007FD74 File Offset: 0x0007DF74
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.destinationUnreachables);
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x0007FD87 File Offset: 0x0007DF87
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.timeExceeds);
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x0007FD9A File Offset: 0x0007DF9A
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.timeExceeds);
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0007FDAD File Offset: 0x0007DFAD
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.parameterProblems);
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x0007FDC0 File Offset: 0x0007DFC0
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.parameterProblems);
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x0007FDD3 File Offset: 0x0007DFD3
		public override long SourceQuenchesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.sourceQuenches);
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x0007FDE6 File Offset: 0x0007DFE6
		public override long SourceQuenchesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.sourceQuenches);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x0007FDF9 File Offset: 0x0007DFF9
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.redirects);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0007FE0C File Offset: 0x0007E00C
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.redirects);
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0007FE1F File Offset: 0x0007E01F
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.echoRequests);
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0007FE32 File Offset: 0x0007E032
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.echoRequests);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x0007FE45 File Offset: 0x0007E045
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.echoReplies);
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x0007FE58 File Offset: 0x0007E058
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.echoReplies);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x0007FE6B File Offset: 0x0007E06B
		public override long TimestampRequestsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.timestampRequests);
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x0007FE7E File Offset: 0x0007E07E
		public override long TimestampRequestsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.timestampRequests);
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x0007FE91 File Offset: 0x0007E091
		public override long TimestampRepliesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.timestampReplies);
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0007FEA4 File Offset: 0x0007E0A4
		public override long TimestampRepliesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.timestampReplies);
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x0007FEB7 File Offset: 0x0007E0B7
		public override long AddressMaskRequestsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.addressMaskRequests);
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x0007FECA File Offset: 0x0007E0CA
		public override long AddressMaskRequestsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.addressMaskRequests);
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x0007FEDD File Offset: 0x0007E0DD
		public override long AddressMaskRepliesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.addressMaskReplies);
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0007FEF0 File Offset: 0x0007E0F0
		public override long AddressMaskRepliesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.addressMaskReplies);
			}
		}

		// Token: 0x04001A86 RID: 6790
		private MibIcmpInfo stats;
	}
}
