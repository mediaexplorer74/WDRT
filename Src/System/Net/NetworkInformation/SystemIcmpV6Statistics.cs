using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F4 RID: 756
	internal class SystemIcmpV6Statistics : IcmpV6Statistics
	{
		// Token: 0x06001A75 RID: 6773 RVA: 0x0007FF04 File Offset: 0x0007E104
		internal SystemIcmpV6Statistics()
		{
			uint icmpStatisticsEx = UnsafeNetInfoNativeMethods.GetIcmpStatisticsEx(out this.stats, AddressFamily.InterNetworkV6);
			if (icmpStatisticsEx != 0U)
			{
				throw new NetworkInformationException((int)icmpStatisticsEx);
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0007FF2F File Offset: 0x0007E12F
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.dwMsgs);
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x0007FF42 File Offset: 0x0007E142
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.dwMsgs);
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x0007FF55 File Offset: 0x0007E155
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.dwErrors);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x0007FF68 File Offset: 0x0007E168
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.dwErrors);
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x0007FF7B File Offset: 0x0007E17B
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)1L))]);
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x0007FF92 File Offset: 0x0007E192
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)1L))]);
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x0007FFA9 File Offset: 0x0007E1A9
		public override long PacketTooBigMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)2L))]);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x0007FFC0 File Offset: 0x0007E1C0
		public override long PacketTooBigMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)2L))]);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x0007FFD7 File Offset: 0x0007E1D7
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)3L))]);
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0007FFEE File Offset: 0x0007E1EE
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)3L))]);
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00080005 File Offset: 0x0007E205
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)4L))]);
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0008001C File Offset: 0x0007E21C
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)4L))]);
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x00080033 File Offset: 0x0007E233
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)128L))]);
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x0008004E File Offset: 0x0007E24E
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)128L))]);
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x00080069 File Offset: 0x0007E269
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)129L))]);
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x00080084 File Offset: 0x0007E284
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)129L))]);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x0008009F File Offset: 0x0007E29F
		public override long MembershipQueriesSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)130L))]);
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x000800BA File Offset: 0x0007E2BA
		public override long MembershipQueriesReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)130L))]);
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x000800D5 File Offset: 0x0007E2D5
		public override long MembershipReportsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)131L))]);
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x000800F0 File Offset: 0x0007E2F0
		public override long MembershipReportsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)131L))]);
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x0008010B File Offset: 0x0007E30B
		public override long MembershipReductionsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)132L))]);
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x00080126 File Offset: 0x0007E326
		public override long MembershipReductionsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)132L))]);
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x00080141 File Offset: 0x0007E341
		public override long RouterAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)134L))]);
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x0008015C File Offset: 0x0007E35C
		public override long RouterAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)134L))]);
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x00080177 File Offset: 0x0007E377
		public override long RouterSolicitsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)133L))]);
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x00080192 File Offset: 0x0007E392
		public override long RouterSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)133L))]);
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x000801AD File Offset: 0x0007E3AD
		public override long NeighborAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)136L))]);
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x000801C8 File Offset: 0x0007E3C8
		public override long NeighborAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)136L))]);
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x000801E3 File Offset: 0x0007E3E3
		public override long NeighborSolicitsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)135L))]);
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x000801FE File Offset: 0x0007E3FE
		public override long NeighborSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)135L))]);
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x00080219 File Offset: 0x0007E419
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.stats.outStats.rgdwTypeCount[(int)(checked((IntPtr)137L))]);
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x00080234 File Offset: 0x0007E434
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.stats.inStats.rgdwTypeCount[(int)(checked((IntPtr)137L))]);
			}
		}

		// Token: 0x04001A96 RID: 6806
		private MibIcmpInfoEx stats;
	}
}
