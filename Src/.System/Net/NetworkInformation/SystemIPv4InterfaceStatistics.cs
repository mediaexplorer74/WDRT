using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002FA RID: 762
	internal class SystemIPv4InterfaceStatistics : IPv4InterfaceStatistics
	{
		// Token: 0x06001AD7 RID: 6871 RVA: 0x00080FF6 File Offset: 0x0007F1F6
		internal SystemIPv4InterfaceStatistics(long index)
		{
			this.ifRow = SystemIPInterfaceStatistics.GetIfEntry2(index);
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x0008100A File Offset: 0x0007F20A
		public override long OutputQueueLength
		{
			get
			{
				return (long)this.ifRow.outQLen;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x00081017 File Offset: 0x0007F217
		public override long BytesSent
		{
			get
			{
				return (long)this.ifRow.outOctets;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x00081024 File Offset: 0x0007F224
		public override long BytesReceived
		{
			get
			{
				return (long)this.ifRow.inOctets;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x00081031 File Offset: 0x0007F231
		public override long UnicastPacketsSent
		{
			get
			{
				return (long)this.ifRow.outUcastPkts;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x0008103E File Offset: 0x0007F23E
		public override long UnicastPacketsReceived
		{
			get
			{
				return (long)this.ifRow.inUcastPkts;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x0008104B File Offset: 0x0007F24B
		public override long NonUnicastPacketsSent
		{
			get
			{
				return (long)this.ifRow.outNUcastPkts;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x00081058 File Offset: 0x0007F258
		public override long NonUnicastPacketsReceived
		{
			get
			{
				return (long)this.ifRow.inNUcastPkts;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x00081065 File Offset: 0x0007F265
		public override long IncomingPacketsDiscarded
		{
			get
			{
				return (long)this.ifRow.inDiscards;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x00081072 File Offset: 0x0007F272
		public override long OutgoingPacketsDiscarded
		{
			get
			{
				return (long)this.ifRow.outDiscards;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x0008107F File Offset: 0x0007F27F
		public override long IncomingPacketsWithErrors
		{
			get
			{
				return (long)this.ifRow.inErrors;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x0008108C File Offset: 0x0007F28C
		public override long OutgoingPacketsWithErrors
		{
			get
			{
				return (long)this.ifRow.outErrors;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00081099 File Offset: 0x0007F299
		public override long IncomingUnknownProtocolPackets
		{
			get
			{
				return (long)this.ifRow.inUnknownProtos;
			}
		}

		// Token: 0x04001AAE RID: 6830
		private MibIfRow2 ifRow;
	}
}
