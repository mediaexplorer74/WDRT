using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F9 RID: 761
	internal class SystemIPInterfaceStatistics : IPInterfaceStatistics
	{
		// Token: 0x06001AC9 RID: 6857 RVA: 0x00080F10 File Offset: 0x0007F110
		internal SystemIPInterfaceStatistics(long index)
		{
			this.ifRow = SystemIPInterfaceStatistics.GetIfEntry2(index);
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00080F24 File Offset: 0x0007F124
		public override long OutputQueueLength
		{
			get
			{
				return (long)this.ifRow.outQLen;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x00080F31 File Offset: 0x0007F131
		public override long BytesSent
		{
			get
			{
				return (long)this.ifRow.outOctets;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00080F3E File Offset: 0x0007F13E
		public override long BytesReceived
		{
			get
			{
				return (long)this.ifRow.inOctets;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x00080F4B File Offset: 0x0007F14B
		public override long UnicastPacketsSent
		{
			get
			{
				return (long)this.ifRow.outUcastPkts;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00080F58 File Offset: 0x0007F158
		public override long UnicastPacketsReceived
		{
			get
			{
				return (long)this.ifRow.inUcastPkts;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x00080F65 File Offset: 0x0007F165
		public override long NonUnicastPacketsSent
		{
			get
			{
				return (long)this.ifRow.outNUcastPkts;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00080F72 File Offset: 0x0007F172
		public override long NonUnicastPacketsReceived
		{
			get
			{
				return (long)this.ifRow.inNUcastPkts;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x00080F7F File Offset: 0x0007F17F
		public override long IncomingPacketsDiscarded
		{
			get
			{
				return (long)this.ifRow.inDiscards;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x00080F8C File Offset: 0x0007F18C
		public override long OutgoingPacketsDiscarded
		{
			get
			{
				return (long)this.ifRow.outDiscards;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x00080F99 File Offset: 0x0007F199
		public override long IncomingPacketsWithErrors
		{
			get
			{
				return (long)this.ifRow.inErrors;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x00080FA6 File Offset: 0x0007F1A6
		public override long OutgoingPacketsWithErrors
		{
			get
			{
				return (long)this.ifRow.outErrors;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x00080FB3 File Offset: 0x0007F1B3
		public override long IncomingUnknownProtocolPackets
		{
			get
			{
				return (long)this.ifRow.inUnknownProtos;
			}
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00080FC0 File Offset: 0x0007F1C0
		internal static MibIfRow2 GetIfEntry2(long index)
		{
			MibIfRow2 mibIfRow = default(MibIfRow2);
			if (index == 0L)
			{
				return mibIfRow;
			}
			mibIfRow.interfaceIndex = (uint)index;
			uint ifEntry = UnsafeNetInfoNativeMethods.GetIfEntry2(ref mibIfRow);
			if (ifEntry != 0U)
			{
				throw new NetworkInformationException((int)ifEntry);
			}
			return mibIfRow;
		}

		// Token: 0x04001AAD RID: 6829
		private MibIfRow2 ifRow;
	}
}
