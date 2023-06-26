using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides statistical data for a network interface on the local computer.</summary>
	// Token: 0x020002A6 RID: 678
	[global::__DynamicallyInvokable]
	public abstract class IPv4InterfaceStatistics
	{
		/// <summary>Gets the number of bytes that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of bytes that were received on the interface.</returns>
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600194D RID: 6477
		[global::__DynamicallyInvokable]
		public abstract long BytesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of bytes that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of bytes that were transmitted on the interface.</returns>
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600194E RID: 6478
		[global::__DynamicallyInvokable]
		public abstract long BytesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of incoming packets that were discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of discarded incoming packets.</returns>
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600194F RID: 6479
		[global::__DynamicallyInvokable]
		public abstract long IncomingPacketsDiscarded
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of incoming packets with errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets with errors.</returns>
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001950 RID: 6480
		[global::__DynamicallyInvokable]
		public abstract long IncomingPacketsWithErrors
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of incoming packets with an unknown protocol that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets with an unknown protocol.</returns>
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001951 RID: 6481
		[global::__DynamicallyInvokable]
		public abstract long IncomingUnknownProtocolPackets
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of non-unicast packets that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of non-unicast packets that were received on the interface.</returns>
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001952 RID: 6482
		[global::__DynamicallyInvokable]
		public abstract long NonUnicastPacketsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of non-unicast packets that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of non-unicast packets that were sent on the interface.</returns>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001953 RID: 6483
		[global::__DynamicallyInvokable]
		public abstract long NonUnicastPacketsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of outgoing packets that were discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of discarded outgoing packets.</returns>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001954 RID: 6484
		[global::__DynamicallyInvokable]
		public abstract long OutgoingPacketsDiscarded
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of outgoing packets with errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of outgoing packets with errors.</returns>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001955 RID: 6485
		[global::__DynamicallyInvokable]
		public abstract long OutgoingPacketsWithErrors
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the length of the output queue.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packets in the output queue.</returns>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001956 RID: 6486
		[global::__DynamicallyInvokable]
		public abstract long OutputQueueLength
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of unicast packets that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of unicast packets that were received on the interface.</returns>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001957 RID: 6487
		[global::__DynamicallyInvokable]
		public abstract long UnicastPacketsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of unicast packets that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of unicast packets that were sent on the interface.</returns>
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001958 RID: 6488
		[global::__DynamicallyInvokable]
		public abstract long UnicastPacketsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPv4InterfaceStatistics" /> class.</summary>
		// Token: 0x06001959 RID: 6489 RVA: 0x0007DB92 File Offset: 0x0007BD92
		[global::__DynamicallyInvokable]
		protected IPv4InterfaceStatistics()
		{
		}
	}
}
