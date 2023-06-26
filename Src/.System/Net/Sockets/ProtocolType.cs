using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the protocols that the <see cref="T:System.Net.Sockets.Socket" /> class supports.</summary>
	// Token: 0x02000372 RID: 882
	public enum ProtocolType
	{
		/// <summary>Internet Protocol.</summary>
		// Token: 0x04001DFB RID: 7675
		IP,
		/// <summary>IPv6 Hop by Hop Options header.</summary>
		// Token: 0x04001DFC RID: 7676
		IPv6HopByHopOptions = 0,
		/// <summary>Internet Control Message Protocol.</summary>
		// Token: 0x04001DFD RID: 7677
		Icmp,
		/// <summary>Internet Group Management Protocol.</summary>
		// Token: 0x04001DFE RID: 7678
		Igmp,
		/// <summary>Gateway To Gateway Protocol.</summary>
		// Token: 0x04001DFF RID: 7679
		Ggp,
		/// <summary>Internet Protocol version 4.</summary>
		// Token: 0x04001E00 RID: 7680
		IPv4,
		/// <summary>Transmission Control Protocol.</summary>
		// Token: 0x04001E01 RID: 7681
		Tcp = 6,
		/// <summary>PARC Universal Packet Protocol.</summary>
		// Token: 0x04001E02 RID: 7682
		Pup = 12,
		/// <summary>User Datagram Protocol.</summary>
		// Token: 0x04001E03 RID: 7683
		Udp = 17,
		/// <summary>Internet Datagram Protocol.</summary>
		// Token: 0x04001E04 RID: 7684
		Idp = 22,
		/// <summary>Internet Protocol version 6 (IPv6).</summary>
		// Token: 0x04001E05 RID: 7685
		IPv6 = 41,
		/// <summary>IPv6 Routing header.</summary>
		// Token: 0x04001E06 RID: 7686
		IPv6RoutingHeader = 43,
		/// <summary>IPv6 Fragment header.</summary>
		// Token: 0x04001E07 RID: 7687
		IPv6FragmentHeader,
		/// <summary>IPv6 Encapsulating Security Payload header.</summary>
		// Token: 0x04001E08 RID: 7688
		IPSecEncapsulatingSecurityPayload = 50,
		/// <summary>IPv6 Authentication header. For details, see RFC 2292 section 2.2.1, available at https://www.ietf.org.</summary>
		// Token: 0x04001E09 RID: 7689
		IPSecAuthenticationHeader,
		/// <summary>Internet Control Message Protocol for IPv6.</summary>
		// Token: 0x04001E0A RID: 7690
		IcmpV6 = 58,
		/// <summary>IPv6 No next header.</summary>
		// Token: 0x04001E0B RID: 7691
		IPv6NoNextHeader,
		/// <summary>IPv6 Destination Options header.</summary>
		// Token: 0x04001E0C RID: 7692
		IPv6DestinationOptions,
		/// <summary>Net Disk Protocol (unofficial).</summary>
		// Token: 0x04001E0D RID: 7693
		ND = 77,
		/// <summary>Raw IP packet protocol.</summary>
		// Token: 0x04001E0E RID: 7694
		Raw = 255,
		/// <summary>Unspecified protocol.</summary>
		// Token: 0x04001E0F RID: 7695
		Unspecified = 0,
		/// <summary>Internet Packet Exchange Protocol.</summary>
		// Token: 0x04001E10 RID: 7696
		Ipx = 1000,
		/// <summary>Sequenced Packet Exchange protocol.</summary>
		// Token: 0x04001E11 RID: 7697
		Spx = 1256,
		/// <summary>Sequenced Packet Exchange version 2 protocol.</summary>
		// Token: 0x04001E12 RID: 7698
		SpxII,
		/// <summary>Unknown protocol.</summary>
		// Token: 0x04001E13 RID: 7699
		Unknown = -1
	}
}
