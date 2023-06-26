using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Protocol (IP) statistical data for an network interface on the local computer.</summary>
	// Token: 0x020002A5 RID: 677
	[global::__DynamicallyInvokable]
	public abstract class IPInterfaceStatistics
	{
		/// <summary>Gets the number of bytes that were received on the interface.</summary>
		/// <returns>The total number of bytes that were received on the interface.</returns>
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001940 RID: 6464
		[global::__DynamicallyInvokable]
		public abstract long BytesReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of bytes that were sent on the interface.</summary>
		/// <returns>The total number of bytes that were sent on the interface.</returns>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001941 RID: 6465
		[global::__DynamicallyInvokable]
		public abstract long BytesSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of incoming packets that were discarded.</summary>
		/// <returns>The total number of incoming packets that were discarded.</returns>
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001942 RID: 6466
		[global::__DynamicallyInvokable]
		public abstract long IncomingPacketsDiscarded
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of incoming packets with errors.</summary>
		/// <returns>The total number of incoming packets with errors.</returns>
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001943 RID: 6467
		[global::__DynamicallyInvokable]
		public abstract long IncomingPacketsWithErrors
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of incoming packets with an unknown protocol that were received on the interface.</summary>
		/// <returns>The total number of incoming packets with an unknown protocol that were received on the interface.</returns>
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001944 RID: 6468
		[global::__DynamicallyInvokable]
		public abstract long IncomingUnknownProtocolPackets
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of non-unicast packets that were received on the interface.</summary>
		/// <returns>The total number of incoming non-unicast packets received on the interface.</returns>
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001945 RID: 6469
		[global::__DynamicallyInvokable]
		public abstract long NonUnicastPacketsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of non-unicast packets that were sent on the interface.</summary>
		/// <returns>The total number of non-unicast packets that were sent on the interface.</returns>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001946 RID: 6470
		[global::__DynamicallyInvokable]
		public abstract long NonUnicastPacketsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of outgoing packets that were discarded.</summary>
		/// <returns>The total number of outgoing packets that were discarded.</returns>
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001947 RID: 6471
		[global::__DynamicallyInvokable]
		public abstract long OutgoingPacketsDiscarded
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of outgoing packets with errors.</summary>
		/// <returns>The total number of outgoing packets with errors.</returns>
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001948 RID: 6472
		[global::__DynamicallyInvokable]
		public abstract long OutgoingPacketsWithErrors
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the length of the output queue.</summary>
		/// <returns>The total number of packets in the output queue.</returns>
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001949 RID: 6473
		[global::__DynamicallyInvokable]
		public abstract long OutputQueueLength
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of unicast packets that were received on the interface.</summary>
		/// <returns>The total number of unicast packets that were received on the interface.</returns>
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600194A RID: 6474
		[global::__DynamicallyInvokable]
		public abstract long UnicastPacketsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of unicast packets that were sent on the interface.</summary>
		/// <returns>The total number of unicast packets that were sent on the interface.</returns>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600194B RID: 6475
		[global::__DynamicallyInvokable]
		public abstract long UnicastPacketsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPInterfaceStatistics" /> class.</summary>
		// Token: 0x0600194C RID: 6476 RVA: 0x0007DB8A File Offset: 0x0007BD8A
		[global::__DynamicallyInvokable]
		protected IPInterfaceStatistics()
		{
		}
	}
}
