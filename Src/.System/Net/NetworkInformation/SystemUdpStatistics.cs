using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000303 RID: 771
	internal class SystemUdpStatistics : UdpStatistics
	{
		// Token: 0x06001B4B RID: 6987 RVA: 0x00081B4E File Offset: 0x0007FD4E
		private SystemUdpStatistics()
		{
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00081B58 File Offset: 0x0007FD58
		internal SystemUdpStatistics(AddressFamily family)
		{
			uint udpStatisticsEx = UnsafeNetInfoNativeMethods.GetUdpStatisticsEx(out this.stats, family);
			if (udpStatisticsEx != 0U)
			{
				throw new NetworkInformationException((int)udpStatisticsEx);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x00081B82 File Offset: 0x0007FD82
		public override long DatagramsReceived
		{
			get
			{
				return (long)((ulong)this.stats.datagramsReceived);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x00081B90 File Offset: 0x0007FD90
		public override long IncomingDatagramsDiscarded
		{
			get
			{
				return (long)((ulong)this.stats.incomingDatagramsDiscarded);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x00081B9E File Offset: 0x0007FD9E
		public override long IncomingDatagramsWithErrors
		{
			get
			{
				return (long)((ulong)this.stats.incomingDatagramsWithErrors);
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x00081BAC File Offset: 0x0007FDAC
		public override long DatagramsSent
		{
			get
			{
				return (long)((ulong)this.stats.datagramsSent);
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00081BBA File Offset: 0x0007FDBA
		public override int UdpListeners
		{
			get
			{
				return (int)this.stats.udpListeners;
			}
		}

		// Token: 0x04001AD4 RID: 6868
		private MibUdpStats stats;
	}
}
