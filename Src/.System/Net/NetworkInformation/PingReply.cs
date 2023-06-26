using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the status and data resulting from a <see cref="Overload:System.Net.NetworkInformation.Ping.Send" /> or <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> operation.</summary>
	// Token: 0x020002ED RID: 749
	public class PingReply
	{
		// Token: 0x06001A4B RID: 6731 RVA: 0x0007FA3A File Offset: 0x0007DC3A
		internal PingReply()
		{
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0007FA42 File Offset: 0x0007DC42
		internal PingReply(IPStatus ipStatus)
		{
			this.ipStatus = ipStatus;
			this.buffer = new byte[0];
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0007FA60 File Offset: 0x0007DC60
		internal PingReply(byte[] data, int dataLength, IPAddress address, int time)
		{
			this.address = address;
			this.rtt = (long)time;
			this.ipStatus = this.GetIPStatus((IcmpV4Type)data[20], (IcmpV4Code)data[21]);
			if (this.ipStatus == IPStatus.Success)
			{
				this.buffer = new byte[dataLength - 28];
				Array.Copy(data, 28, this.buffer, 0, dataLength - 28);
				return;
			}
			this.buffer = new byte[0];
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0007FAD0 File Offset: 0x0007DCD0
		internal PingReply(IcmpEchoReply reply)
		{
			this.address = new IPAddress((long)((ulong)reply.address));
			this.ipStatus = (IPStatus)reply.status;
			if (this.ipStatus == IPStatus.Success)
			{
				this.rtt = (long)((ulong)reply.roundTripTime);
				this.buffer = new byte[(int)reply.dataSize];
				Marshal.Copy(reply.data, this.buffer, 0, (int)reply.dataSize);
				this.options = new PingOptions(reply.options);
				return;
			}
			this.buffer = new byte[0];
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0007FB60 File Offset: 0x0007DD60
		internal PingReply(Icmp6EchoReply reply, IntPtr dataPtr, int sendSize)
		{
			this.address = new IPAddress(reply.Address.Address, (long)((ulong)reply.Address.ScopeID));
			this.ipStatus = (IPStatus)reply.Status;
			if (this.ipStatus == IPStatus.Success)
			{
				this.rtt = (long)((ulong)reply.RoundTripTime);
				this.buffer = new byte[sendSize];
				Marshal.Copy(IntPtrHelper.Add(dataPtr, 36), this.buffer, 0, sendSize);
				return;
			}
			this.buffer = new byte[0];
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0007FBE4 File Offset: 0x0007DDE4
		private IPStatus GetIPStatus(IcmpV4Type type, IcmpV4Code code)
		{
			switch (type)
			{
			case IcmpV4Type.ICMP4_ECHO_REPLY:
				return IPStatus.Success;
			case (IcmpV4Type)1:
			case (IcmpV4Type)2:
				break;
			case IcmpV4Type.ICMP4_DST_UNREACH:
				switch (code)
				{
				case IcmpV4Code.ICMP4_UNREACH_NET:
					return IPStatus.DestinationNetworkUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_HOST:
					return IPStatus.DestinationHostUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_PROTOCOL:
					return IPStatus.DestinationProtocolUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_PORT:
					return IPStatus.DestinationPortUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_FRAG_NEEDED:
					return IPStatus.PacketTooBig;
				default:
					return IPStatus.DestinationUnreachable;
				}
				break;
			case IcmpV4Type.ICMP4_SOURCE_QUENCH:
				return IPStatus.SourceQuench;
			default:
				if (type == IcmpV4Type.ICMP4_TIME_EXCEEDED)
				{
					return IPStatus.TtlExpired;
				}
				if (type == IcmpV4Type.ICMP4_PARAM_PROB)
				{
					return IPStatus.ParameterProblem;
				}
				break;
			}
			return IPStatus.Unknown;
		}

		/// <summary>Gets the status of an attempt to send an Internet Control Message Protocol (ICMP) echo request and receive the corresponding ICMP echo reply message.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPStatus" /> value indicating the result of the request.</returns>
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0007FC6C File Offset: 0x0007DE6C
		public IPStatus Status
		{
			get
			{
				return this.ipStatus;
			}
		}

		/// <summary>Gets the address of the host that sends the Internet Control Message Protocol (ICMP) echo reply.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> containing the destination for the ICMP echo message.</returns>
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x0007FC74 File Offset: 0x0007DE74
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		/// <summary>Gets the number of milliseconds taken to send an Internet Control Message Protocol (ICMP) echo request and receive the corresponding ICMP echo reply message.</summary>
		/// <returns>An <see cref="T:System.Int64" /> that specifies the round trip time, in milliseconds.</returns>
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x0007FC7C File Offset: 0x0007DE7C
		public long RoundtripTime
		{
			get
			{
				return this.rtt;
			}
		}

		/// <summary>Gets the options used to transmit the reply to an Internet Control Message Protocol (ICMP) echo request.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingOptions" /> object that contains the Time to Live (TTL) and the fragmentation directive used for transmitting the reply if <see cref="P:System.Net.NetworkInformation.PingReply.Status" /> is <see cref="F:System.Net.NetworkInformation.IPStatus.Success" />; otherwise, <see langword="null" />.</returns>
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x0007FC84 File Offset: 0x0007DE84
		public PingOptions Options
		{
			get
			{
				return this.options;
			}
		}

		/// <summary>Gets the buffer of data received in an Internet Control Message Protocol (ICMP) echo reply message.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the data received in an ICMP echo reply message, or an empty array, if no reply was received.</returns>
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x0007FC8C File Offset: 0x0007DE8C
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x04001A74 RID: 6772
		private IPAddress address;

		// Token: 0x04001A75 RID: 6773
		private PingOptions options;

		// Token: 0x04001A76 RID: 6774
		private IPStatus ipStatus;

		// Token: 0x04001A77 RID: 6775
		private long rtt;

		// Token: 0x04001A78 RID: 6776
		private byte[] buffer;
	}
}
