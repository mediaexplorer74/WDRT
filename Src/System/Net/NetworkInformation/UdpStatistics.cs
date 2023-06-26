using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides User Datagram Protocol (UDP) statistical data.</summary>
	// Token: 0x02000308 RID: 776
	[global::__DynamicallyInvokable]
	public abstract class UdpStatistics
	{
		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of datagrams that were delivered to UDP users.</returns>
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001B6B RID: 7019
		[global::__DynamicallyInvokable]
		public abstract long DatagramsReceived
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of datagrams that were sent.</returns>
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001B6C RID: 7020
		[global::__DynamicallyInvokable]
		public abstract long DatagramsSent
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received and discarded because of port errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of received UDP datagrams that were discarded because there was no listening application at the destination port.</returns>
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001B6D RID: 7021
		[global::__DynamicallyInvokable]
		public abstract long IncomingDatagramsDiscarded
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received and discarded because of errors other than bad port information.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of received UDP datagrams that could not be delivered for reasons other than the lack of an application at the destination port.</returns>
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001B6E RID: 7022
		[global::__DynamicallyInvokable]
		public abstract long IncomingDatagramsWithErrors
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of local endpoints that are listening for User Datagram Protocol (UDP) datagrams.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of sockets that are listening for UDP datagrams.</returns>
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001B6F RID: 7023
		[global::__DynamicallyInvokable]
		public abstract int UdpListeners
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.UdpStatistics" /> class.</summary>
		// Token: 0x06001B70 RID: 7024 RVA: 0x00081E1C File Offset: 0x0008001C
		[global::__DynamicallyInvokable]
		protected UdpStatistics()
		{
		}
	}
}
