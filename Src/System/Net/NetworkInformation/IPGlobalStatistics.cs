using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Protocol (IP) statistical data.</summary>
	// Token: 0x020002A2 RID: 674
	[global::__DynamicallyInvokable]
	public abstract class IPGlobalStatistics
	{
		/// <summary>Gets the default time-to-live (TTL) value for Internet Protocol (IP) packets.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the TTL.</returns>
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600191C RID: 6428
		[global::__DynamicallyInvokable]
		public abstract int DefaultTtl
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that specifies whether Internet Protocol (IP) packet forwarding is enabled.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that specifies whether packet forwarding is enabled.</returns>
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600191D RID: 6429
		[global::__DynamicallyInvokable]
		public abstract bool ForwardingEnabled
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of network interfaces.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value containing the number of network interfaces for the address family used to obtain this <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> instance.</returns>
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600191E RID: 6430
		[global::__DynamicallyInvokable]
		public abstract int NumberOfInterfaces
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) addresses assigned to the local computer.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of IP addresses assigned to the address family (Internet Protocol version 4 or Internet Protocol version 6) described by this object.</returns>
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600191F RID: 6431
		[global::__DynamicallyInvokable]
		public abstract int NumberOfIPAddresses
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of outbound Internet Protocol (IP) packets.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of outgoing packets.</returns>
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001920 RID: 6432
		[global::__DynamicallyInvokable]
		public abstract long OutputPacketRequests
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of routes that have been discarded from the routing table.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of valid routes that have been discarded.</returns>
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001921 RID: 6433
		[global::__DynamicallyInvokable]
		public abstract long OutputPacketRoutingDiscards
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of transmitted Internet Protocol (IP) packets that have been discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of outgoing packets that have been discarded.</returns>
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001922 RID: 6434
		[global::__DynamicallyInvokable]
		public abstract long OutputPacketsDiscarded
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) packets for which the local computer could not determine a route to the destination address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of packets that could not be sent because a route could not be found.</returns>
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001923 RID: 6435
		[global::__DynamicallyInvokable]
		public abstract long OutputPacketsWithNoRoute
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) packets that could not be fragmented.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packets that required fragmentation but had the "Don't Fragment" bit set.</returns>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001924 RID: 6436
		[global::__DynamicallyInvokable]
		public abstract long PacketFragmentFailures
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) packets that required reassembly.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packet reassemblies required.</returns>
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001925 RID: 6437
		[global::__DynamicallyInvokable]
		public abstract long PacketReassembliesRequired
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) packets that were not successfully reassembled.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packets that could not be reassembled.</returns>
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001926 RID: 6438
		[global::__DynamicallyInvokable]
		public abstract long PacketReassemblyFailures
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the maximum amount of time within which all fragments of an Internet Protocol (IP) packet must arrive.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the maximum number of milliseconds within which all fragments of a packet must arrive to avoid being discarded.</returns>
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001927 RID: 6439
		[global::__DynamicallyInvokable]
		public abstract long PacketReassemblyTimeout
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) packets fragmented.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of fragmented packets.</returns>
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001928 RID: 6440
		[global::__DynamicallyInvokable]
		public abstract long PacketsFragmented
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) packets reassembled.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of fragmented packets that have been successfully reassembled.</returns>
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001929 RID: 6441
		[global::__DynamicallyInvokable]
		public abstract long PacketsReassembled
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) packets received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets received.</returns>
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600192A RID: 6442
		[global::__DynamicallyInvokable]
		public abstract long ReceivedPackets
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) packets delivered.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets delivered.</returns>
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600192B RID: 6443
		[global::__DynamicallyInvokable]
		public abstract long ReceivedPacketsDelivered
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) packets that have been received and discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets that have been discarded.</returns>
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600192C RID: 6444
		[global::__DynamicallyInvokable]
		public abstract long ReceivedPacketsDiscarded
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) packets forwarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of forwarded packets.</returns>
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600192D RID: 6445
		[global::__DynamicallyInvokable]
		public abstract long ReceivedPacketsForwarded
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) packets with address errors that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets received with errors in the address portion of the header.</returns>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600192E RID: 6446
		[global::__DynamicallyInvokable]
		public abstract long ReceivedPacketsWithAddressErrors
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) packets with header errors that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets received and discarded due to errors in the header.</returns>
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600192F RID: 6447
		[global::__DynamicallyInvokable]
		public abstract long ReceivedPacketsWithHeadersErrors
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of Internet Protocol (IP) packets received on the local machine with an unknown protocol in the header.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the total number of IP packets received with an unknown protocol.</returns>
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001930 RID: 6448
		[global::__DynamicallyInvokable]
		public abstract long ReceivedPacketsWithUnknownProtocol
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of routes in the Internet Protocol (IP) routing table.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of routes in the routing table.</returns>
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001931 RID: 6449
		[global::__DynamicallyInvokable]
		public abstract int NumberOfRoutes
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> class.</summary>
		// Token: 0x06001932 RID: 6450 RVA: 0x0007DB7A File Offset: 0x0007BD7A
		[global::__DynamicallyInvokable]
		protected IPGlobalStatistics()
		{
		}
	}
}
