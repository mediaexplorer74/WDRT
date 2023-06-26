using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the Transmission Control Protocol (TCP) connections on the local computer.</summary>
	// Token: 0x02000305 RID: 773
	[global::__DynamicallyInvokable]
	public abstract class TcpConnectionInformation
	{
		/// <summary>Gets the local endpoint of a Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> instance that contains the IP address and port on the local computer.</returns>
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001B58 RID: 7000
		[global::__DynamicallyInvokable]
		public abstract IPEndPoint LocalEndPoint
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the remote endpoint of a Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> instance that contains the IP address and port on the remote computer.</returns>
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001B59 RID: 7001
		[global::__DynamicallyInvokable]
		public abstract IPEndPoint RemoteEndPoint
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the state of this Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.TcpState" /> enumeration values.</returns>
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001B5A RID: 7002
		[global::__DynamicallyInvokable]
		public abstract TcpState State
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.TcpConnectionInformation" /> class.</summary>
		// Token: 0x06001B5B RID: 7003 RVA: 0x00081E0C File Offset: 0x0008000C
		[global::__DynamicallyInvokable]
		protected TcpConnectionInformation()
		{
		}
	}
}
