using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Reports the status of sending an Internet Control Message Protocol (ICMP) echo message to a computer.</summary>
	// Token: 0x020002A9 RID: 681
	public enum IPStatus
	{
		/// <summary>The ICMP echo request succeeded; an ICMP echo reply was received. When you get this status code, the other <see cref="T:System.Net.NetworkInformation.PingReply" /> properties contain valid data.</summary>
		// Token: 0x040018DE RID: 6366
		Success,
		/// <summary>The ICMP echo request failed because the network that contains the destination computer is not reachable.</summary>
		// Token: 0x040018DF RID: 6367
		DestinationNetworkUnreachable = 11002,
		/// <summary>The ICMP echo request failed because the destination computer is not reachable.</summary>
		// Token: 0x040018E0 RID: 6368
		DestinationHostUnreachable,
		/// <summary>The ICMP echo request failed because the destination computer that is specified in an ICMP echo message is not reachable, because it does not support the packet's protocol. This value applies only to IPv4. This value is described in IETF RFC 1812 as Communication Administratively Prohibited.</summary>
		// Token: 0x040018E1 RID: 6369
		DestinationProtocolUnreachable,
		/// <summary>The ICMP echo request failed because the port on the destination computer is not available.</summary>
		// Token: 0x040018E2 RID: 6370
		DestinationPortUnreachable,
		/// <summary>The ICMPv6 echo request failed because contact with the destination computer is administratively prohibited. This value applies only to IPv6.</summary>
		// Token: 0x040018E3 RID: 6371
		DestinationProhibited = 11004,
		/// <summary>The ICMP echo request failed because of insufficient network resources.</summary>
		// Token: 0x040018E4 RID: 6372
		NoResources = 11006,
		/// <summary>The ICMP echo request failed because it contains an invalid option.</summary>
		// Token: 0x040018E5 RID: 6373
		BadOption,
		/// <summary>The ICMP echo request failed because of a hardware error.</summary>
		// Token: 0x040018E6 RID: 6374
		HardwareError,
		/// <summary>The ICMP echo request failed because the packet containing the request is larger than the maximum transmission unit (MTU) of a node (router or gateway) located between the source and destination. The MTU defines the maximum size of a transmittable packet.</summary>
		// Token: 0x040018E7 RID: 6375
		PacketTooBig,
		/// <summary>The ICMP echo Reply was not received within the allotted time. The default time allowed for replies is 5 seconds. You can change this value using the <see cref="Overload:System.Net.NetworkInformation.Ping.Send" /> or <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> methods that take a <paramref name="timeout" /> parameter.</summary>
		// Token: 0x040018E8 RID: 6376
		TimedOut,
		/// <summary>The ICMP echo request failed because there is no valid route between the source and destination computers.</summary>
		// Token: 0x040018E9 RID: 6377
		BadRoute = 11012,
		/// <summary>The ICMP echo request failed because its Time to Live (TTL) value reached zero, causing the forwarding node (router or gateway) to discard the packet.</summary>
		// Token: 0x040018EA RID: 6378
		TtlExpired,
		/// <summary>The ICMP echo request failed because the packet was divided into fragments for transmission and all of the fragments were not received within the time allotted for reassembly. RFC 2460 (available at www.ietf.org) specifies 60 seconds as the time limit within which all packet fragments must be received.</summary>
		// Token: 0x040018EB RID: 6379
		TtlReassemblyTimeExceeded,
		/// <summary>The ICMP echo request failed because a node (router or gateway) encountered problems while processing the packet header. This is the status if, for example, the header contains invalid field data or an unrecognized option.</summary>
		// Token: 0x040018EC RID: 6380
		ParameterProblem,
		/// <summary>The ICMP echo request failed because the packet was discarded. This occurs when the source computer's output queue has insufficient storage space, or when packets arrive at the destination too quickly to be processed.</summary>
		// Token: 0x040018ED RID: 6381
		SourceQuench,
		/// <summary>The ICMP echo request failed because the destination IP address cannot receive ICMP echo requests or should never appear in the destination address field of any IP datagram. For example, calling <see cref="Overload:System.Net.NetworkInformation.Ping.Send" /> and specifying IP address "000.0.0.0" returns this status.</summary>
		// Token: 0x040018EE RID: 6382
		BadDestination = 11018,
		/// <summary>The ICMP echo request failed because the destination computer that is specified in an ICMP echo message is not reachable; the exact cause of problem is unknown.</summary>
		// Token: 0x040018EF RID: 6383
		DestinationUnreachable = 11040,
		/// <summary>The ICMP echo request failed because its Time to Live (TTL) value reached zero, causing the forwarding node (router or gateway) to discard the packet.</summary>
		// Token: 0x040018F0 RID: 6384
		TimeExceeded,
		/// <summary>The ICMP echo request failed because the header is invalid.</summary>
		// Token: 0x040018F1 RID: 6385
		BadHeader,
		/// <summary>The ICMP echo request failed because the Next Header field does not contain a recognized value. The Next Header field indicates the extension header type (if present) or the protocol above the IP layer, for example, TCP or UDP.</summary>
		// Token: 0x040018F2 RID: 6386
		UnrecognizedNextHeader,
		/// <summary>The ICMP echo request failed because of an ICMP protocol error.</summary>
		// Token: 0x040018F3 RID: 6387
		IcmpError,
		/// <summary>The ICMP echo request failed because the source address and destination address that are specified in an ICMP echo message are not in the same scope. This is typically caused by a router forwarding a packet using an interface that is outside the scope of the source address. Address scopes (link-local, site-local, and global scope) determine where on the network an address is valid.</summary>
		// Token: 0x040018F4 RID: 6388
		DestinationScopeMismatch,
		/// <summary>The ICMP echo request failed for an unknown reason.</summary>
		// Token: 0x040018F5 RID: 6389
		Unknown = -1
	}
}
