using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000302 RID: 770
	internal class SystemTcpStatistics : TcpStatistics
	{
		// Token: 0x06001B3B RID: 6971 RVA: 0x00081A57 File Offset: 0x0007FC57
		private SystemTcpStatistics()
		{
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00081A60 File Offset: 0x0007FC60
		internal SystemTcpStatistics(AddressFamily family)
		{
			uint tcpStatisticsEx = UnsafeNetInfoNativeMethods.GetTcpStatisticsEx(out this.stats, family);
			if (tcpStatisticsEx != 0U)
			{
				throw new NetworkInformationException((int)tcpStatisticsEx);
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x00081A8A File Offset: 0x0007FC8A
		public override long MinimumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.stats.minimumRetransmissionTimeOut);
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x00081A98 File Offset: 0x0007FC98
		public override long MaximumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.stats.maximumRetransmissionTimeOut);
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00081AA6 File Offset: 0x0007FCA6
		public override long MaximumConnections
		{
			get
			{
				return (long)((ulong)this.stats.maximumConnections);
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x00081AB4 File Offset: 0x0007FCB4
		public override long ConnectionsInitiated
		{
			get
			{
				return (long)((ulong)this.stats.activeOpens);
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x00081AC2 File Offset: 0x0007FCC2
		public override long ConnectionsAccepted
		{
			get
			{
				return (long)((ulong)this.stats.passiveOpens);
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x00081AD0 File Offset: 0x0007FCD0
		public override long FailedConnectionAttempts
		{
			get
			{
				return (long)((ulong)this.stats.failedConnectionAttempts);
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x00081ADE File Offset: 0x0007FCDE
		public override long ResetConnections
		{
			get
			{
				return (long)((ulong)this.stats.resetConnections);
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x00081AEC File Offset: 0x0007FCEC
		public override long CurrentConnections
		{
			get
			{
				return (long)((ulong)this.stats.currentConnections);
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x00081AFA File Offset: 0x0007FCFA
		public override long SegmentsReceived
		{
			get
			{
				return (long)((ulong)this.stats.segmentsReceived);
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x00081B08 File Offset: 0x0007FD08
		public override long SegmentsSent
		{
			get
			{
				return (long)((ulong)this.stats.segmentsSent);
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x00081B16 File Offset: 0x0007FD16
		public override long SegmentsResent
		{
			get
			{
				return (long)((ulong)this.stats.segmentsResent);
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x00081B24 File Offset: 0x0007FD24
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.stats.errorsReceived);
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x00081B32 File Offset: 0x0007FD32
		public override long ResetsSent
		{
			get
			{
				return (long)((ulong)this.stats.segmentsSentWithReset);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x00081B40 File Offset: 0x0007FD40
		public override long CumulativeConnections
		{
			get
			{
				return (long)((ulong)this.stats.cumulativeConnections);
			}
		}

		// Token: 0x04001AD3 RID: 6867
		private MibTcpStats stats;
	}
}
