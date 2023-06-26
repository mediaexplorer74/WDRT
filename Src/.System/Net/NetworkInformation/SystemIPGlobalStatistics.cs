using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002FB RID: 763
	internal class SystemIPGlobalStatistics : IPGlobalStatistics
	{
		// Token: 0x06001AE4 RID: 6884 RVA: 0x000810A6 File Offset: 0x0007F2A6
		private SystemIPGlobalStatistics()
		{
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x000810B0 File Offset: 0x0007F2B0
		internal SystemIPGlobalStatistics(AddressFamily family)
		{
			uint ipStatisticsEx = UnsafeNetInfoNativeMethods.GetIpStatisticsEx(out this.stats, family);
			if (ipStatisticsEx != 0U)
			{
				throw new NetworkInformationException((int)ipStatisticsEx);
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x000810DA File Offset: 0x0007F2DA
		public override bool ForwardingEnabled
		{
			get
			{
				return this.stats.forwardingEnabled;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x000810E7 File Offset: 0x0007F2E7
		public override int DefaultTtl
		{
			get
			{
				return (int)this.stats.defaultTtl;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x000810F4 File Offset: 0x0007F2F4
		public override long ReceivedPackets
		{
			get
			{
				return (long)((ulong)this.stats.packetsReceived);
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x00081102 File Offset: 0x0007F302
		public override long ReceivedPacketsWithHeadersErrors
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsWithHeaderErrors);
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x00081110 File Offset: 0x0007F310
		public override long ReceivedPacketsWithAddressErrors
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsWithAddressErrors);
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x0008111E File Offset: 0x0007F31E
		public override long ReceivedPacketsForwarded
		{
			get
			{
				return (long)((ulong)this.stats.packetsForwarded);
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x0008112C File Offset: 0x0007F32C
		public override long ReceivedPacketsWithUnknownProtocol
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsWithUnknownProtocols);
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x0008113A File Offset: 0x0007F33A
		public override long ReceivedPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsDiscarded);
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x00081148 File Offset: 0x0007F348
		public override long ReceivedPacketsDelivered
		{
			get
			{
				return (long)((ulong)this.stats.receivedPacketsDelivered);
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x00081156 File Offset: 0x0007F356
		public override long OutputPacketRequests
		{
			get
			{
				return (long)((ulong)this.stats.packetOutputRequests);
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00081164 File Offset: 0x0007F364
		public override long OutputPacketRoutingDiscards
		{
			get
			{
				return (long)((ulong)this.stats.outputPacketRoutingDiscards);
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x00081172 File Offset: 0x0007F372
		public override long OutputPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.stats.outputPacketsDiscarded);
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x00081180 File Offset: 0x0007F380
		public override long OutputPacketsWithNoRoute
		{
			get
			{
				return (long)((ulong)this.stats.outputPacketsWithNoRoute);
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0008118E File Offset: 0x0007F38E
		public override long PacketReassemblyTimeout
		{
			get
			{
				return (long)((ulong)this.stats.packetReassemblyTimeout);
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x0008119C File Offset: 0x0007F39C
		public override long PacketReassembliesRequired
		{
			get
			{
				return (long)((ulong)this.stats.packetsReassemblyRequired);
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x000811AA File Offset: 0x0007F3AA
		public override long PacketsReassembled
		{
			get
			{
				return (long)((ulong)this.stats.packetsReassembled);
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x000811B8 File Offset: 0x0007F3B8
		public override long PacketReassemblyFailures
		{
			get
			{
				return (long)((ulong)this.stats.packetsReassemblyFailed);
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x000811C6 File Offset: 0x0007F3C6
		public override long PacketsFragmented
		{
			get
			{
				return (long)((ulong)this.stats.packetsFragmented);
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x000811D4 File Offset: 0x0007F3D4
		public override long PacketFragmentFailures
		{
			get
			{
				return (long)((ulong)this.stats.packetsFragmentFailed);
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x000811E2 File Offset: 0x0007F3E2
		public override int NumberOfInterfaces
		{
			get
			{
				return (int)this.stats.interfaces;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x000811EF File Offset: 0x0007F3EF
		public override int NumberOfIPAddresses
		{
			get
			{
				return (int)this.stats.ipAddresses;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x000811FC File Offset: 0x0007F3FC
		public override int NumberOfRoutes
		{
			get
			{
				return (int)this.stats.routes;
			}
		}

		// Token: 0x04001AAF RID: 6831
		private MibIpStats stats;
	}
}
