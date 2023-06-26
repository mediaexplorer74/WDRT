using System;

namespace System.Net.Sockets
{
	/// <summary>Defines configuration option names.</summary>
	// Token: 0x02000380 RID: 896
	public enum SocketOptionName
	{
		/// <summary>Record debugging information.</summary>
		// Token: 0x04001EEB RID: 7915
		Debug = 1,
		/// <summary>The socket is listening.</summary>
		// Token: 0x04001EEC RID: 7916
		AcceptConnection,
		/// <summary>Allows the socket to be bound to an address that is already in use.</summary>
		// Token: 0x04001EED RID: 7917
		ReuseAddress = 4,
		/// <summary>Use keep-alives.</summary>
		// Token: 0x04001EEE RID: 7918
		KeepAlive = 8,
		/// <summary>Do not route; send the packet directly to the interface addresses.</summary>
		// Token: 0x04001EEF RID: 7919
		DontRoute = 16,
		/// <summary>Permit sending broadcast messages on the socket.</summary>
		// Token: 0x04001EF0 RID: 7920
		Broadcast = 32,
		/// <summary>Bypass hardware when possible.</summary>
		// Token: 0x04001EF1 RID: 7921
		UseLoopback = 64,
		/// <summary>Linger on close if unsent data is present.</summary>
		// Token: 0x04001EF2 RID: 7922
		Linger = 128,
		/// <summary>Receives out-of-band data in the normal data stream.</summary>
		// Token: 0x04001EF3 RID: 7923
		OutOfBandInline = 256,
		/// <summary>Close the socket gracefully without lingering.</summary>
		// Token: 0x04001EF4 RID: 7924
		DontLinger = -129,
		/// <summary>Enables a socket to be bound for exclusive access.</summary>
		// Token: 0x04001EF5 RID: 7925
		ExclusiveAddressUse = -5,
		/// <summary>Specifies the total per-socket buffer space reserved for sends. This is unrelated to the maximum message size or the size of a TCP window.</summary>
		// Token: 0x04001EF6 RID: 7926
		SendBuffer = 4097,
		/// <summary>Specifies the total per-socket buffer space reserved for receives. This is unrelated to the maximum message size or the size of a TCP window.</summary>
		// Token: 0x04001EF7 RID: 7927
		ReceiveBuffer,
		/// <summary>Specifies the low water mark for <see cref="Overload:System.Net.Sockets.Socket.Send" /> operations.</summary>
		// Token: 0x04001EF8 RID: 7928
		SendLowWater,
		/// <summary>Specifies the low water mark for <see cref="Overload:System.Net.Sockets.Socket.Receive" /> operations.</summary>
		// Token: 0x04001EF9 RID: 7929
		ReceiveLowWater,
		/// <summary>Send a time-out. This option applies only to synchronous methods; it has no effect on asynchronous methods such as the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</summary>
		// Token: 0x04001EFA RID: 7930
		SendTimeout,
		/// <summary>Receive a time-out. This option applies only to synchronous methods; it has no effect on asynchronous methods such as the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</summary>
		// Token: 0x04001EFB RID: 7931
		ReceiveTimeout,
		/// <summary>Gets the error status and clear.</summary>
		// Token: 0x04001EFC RID: 7932
		Error,
		/// <summary>Gets the socket type.</summary>
		// Token: 0x04001EFD RID: 7933
		Type,
		/// <summary>Indicates that the system should defer ephemeral port allocation for outbound connections. This is equivalent to using the Winsock2 SO_REUSE_UNICASTPORT socket option.</summary>
		// Token: 0x04001EFE RID: 7934
		ReuseUnicastPort = 12295,
		/// <summary>Not supported; will throw a <see cref="T:System.Net.Sockets.SocketException" /> if used.</summary>
		// Token: 0x04001EFF RID: 7935
		MaxConnections = 2147483647,
		/// <summary>Specifies the IP options to be inserted into outgoing datagrams.</summary>
		// Token: 0x04001F00 RID: 7936
		IPOptions = 1,
		/// <summary>Indicates that the application provides the IP header for outgoing datagrams.</summary>
		// Token: 0x04001F01 RID: 7937
		HeaderIncluded,
		/// <summary>Change the IP header type of the service field.</summary>
		// Token: 0x04001F02 RID: 7938
		TypeOfService,
		/// <summary>Set the IP header Time-to-Live field.</summary>
		// Token: 0x04001F03 RID: 7939
		IpTimeToLive,
		/// <summary>Set the interface for outgoing multicast packets.</summary>
		// Token: 0x04001F04 RID: 7940
		MulticastInterface = 9,
		/// <summary>An IP multicast Time to Live.</summary>
		// Token: 0x04001F05 RID: 7941
		MulticastTimeToLive,
		/// <summary>An IP multicast loopback.</summary>
		// Token: 0x04001F06 RID: 7942
		MulticastLoopback,
		/// <summary>Add an IP group membership.</summary>
		// Token: 0x04001F07 RID: 7943
		AddMembership,
		/// <summary>Drop an IP group membership.</summary>
		// Token: 0x04001F08 RID: 7944
		DropMembership,
		/// <summary>Do not fragment IP datagrams.</summary>
		// Token: 0x04001F09 RID: 7945
		DontFragment,
		/// <summary>Join a source group.</summary>
		// Token: 0x04001F0A RID: 7946
		AddSourceMembership,
		/// <summary>Drop a source group.</summary>
		// Token: 0x04001F0B RID: 7947
		DropSourceMembership,
		/// <summary>Block data from a source.</summary>
		// Token: 0x04001F0C RID: 7948
		BlockSource,
		/// <summary>Unblock a previously blocked source.</summary>
		// Token: 0x04001F0D RID: 7949
		UnblockSource,
		/// <summary>Return information about received packets.</summary>
		// Token: 0x04001F0E RID: 7950
		PacketInformation,
		/// <summary>Specifies the maximum number of router hops for an Internet Protocol version 6 (IPv6) packet. This is similar to Time to Live (TTL) for Internet Protocol version 4.</summary>
		// Token: 0x04001F0F RID: 7951
		HopLimit = 21,
		/// <summary>Enables restriction of a IPv6 socket to a specified scope, such as addresses with the same link local or site local prefix.This socket option enables applications to place access restrictions on IPv6 sockets. Such restrictions enable an application running on a private LAN to simply and robustly harden itself against external attacks. This socket option widens or narrows the scope of a listening socket, enabling unrestricted access from public and private users when appropriate, or restricting access only to the same site, as required. This socket option has defined protection levels specified in the <see cref="T:System.Net.Sockets.IPProtectionLevel" /> enumeration.</summary>
		// Token: 0x04001F10 RID: 7952
		IPProtectionLevel = 23,
		/// <summary>Indicates if a socket created for the AF_INET6 address family is restricted to IPv6 communications only. Sockets created for the AF_INET6 address family may be used for both IPv6 and IPv4 communications. Some applications may want to restrict their use of a socket created for the AF_INET6 address family to IPv6 communications only. When this value is non-zero (the default on Windows), a socket created for the AF_INET6 address family can be used to send and receive IPv6 packets only. When this value is zero, a socket created for the AF_INET6 address family can be used to send and receive packets to and from an IPv6 address or an IPv4 address. Note that the ability to interact with an IPv4 address requires the use of IPv4 mapped addresses. This socket option is supported on Windows Vista or later.</summary>
		// Token: 0x04001F11 RID: 7953
		IPv6Only = 27,
		/// <summary>Disables the Nagle algorithm for send coalescing.</summary>
		// Token: 0x04001F12 RID: 7954
		NoDelay = 1,
		/// <summary>Use urgent data as defined in RFC-1222. This option can be set only once; after it is set, it cannot be turned off.</summary>
		// Token: 0x04001F13 RID: 7955
		BsdUrgent,
		/// <summary>Use expedited data as defined in RFC-1222. This option can be set only once; after it is set, it cannot be turned off.</summary>
		// Token: 0x04001F14 RID: 7956
		Expedited = 2,
		/// <summary>Send UDP datagrams with checksum set to zero.</summary>
		// Token: 0x04001F15 RID: 7957
		NoChecksum = 1,
		/// <summary>Set or get the UDP checksum coverage.</summary>
		// Token: 0x04001F16 RID: 7958
		ChecksumCoverage = 20,
		/// <summary>Updates an accepted socket's properties by using those of an existing socket. This is equivalent to using the Winsock2 SO_UPDATE_ACCEPT_CONTEXT socket option and is supported only on connection-oriented sockets.</summary>
		// Token: 0x04001F17 RID: 7959
		UpdateAcceptContext = 28683,
		/// <summary>Updates a connected socket's properties by using those of an existing socket. This is equivalent to using the Winsock2 SO_UPDATE_CONNECT_CONTEXT socket option and is supported only on connection-oriented sockets.</summary>
		// Token: 0x04001F18 RID: 7960
		UpdateConnectContext = 28688
	}
}
