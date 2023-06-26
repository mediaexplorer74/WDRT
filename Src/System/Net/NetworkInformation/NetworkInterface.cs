using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides configuration and statistical information for a network interface.</summary>
	// Token: 0x020002E3 RID: 739
	[global::__DynamicallyInvokable]
	public abstract class NetworkInterface
	{
		/// <summary>Returns objects that describe the network interfaces on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> array that contains objects that describe the available network interfaces, or an empty array if no interfaces are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Windows system function call failed.</exception>
		// Token: 0x060019F3 RID: 6643 RVA: 0x0007E55B File Offset: 0x0007C75B
		[global::__DynamicallyInvokable]
		public static NetworkInterface[] GetAllNetworkInterfaces()
		{
			new NetworkInformationPermission(NetworkInformationAccess.Read).Demand();
			return SystemNetworkInterface.GetNetworkInterfaces();
		}

		/// <summary>Indicates whether any network connection is available.</summary>
		/// <returns>
		///   <see langword="true" /> if a network connection is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x060019F4 RID: 6644 RVA: 0x0007E56D File Offset: 0x0007C76D
		[global::__DynamicallyInvokable]
		public static bool GetIsNetworkAvailable()
		{
			return SystemNetworkInterface.InternalGetIsNetworkAvailable();
		}

		/// <summary>Gets the index of the IPv4 loopback interface.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that contains the index for the IPv4 loopback interface.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">This property is not valid on computers running only Ipv6.</exception>
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x0007E574 File Offset: 0x0007C774
		[global::__DynamicallyInvokable]
		public static int LoopbackInterfaceIndex
		{
			[global::__DynamicallyInvokable]
			get
			{
				return SystemNetworkInterface.InternalLoopbackInterfaceIndex;
			}
		}

		/// <summary>Gets the index of the IPv6 loopback interface.</summary>
		/// <returns>The index for the IPv6 loopback interface.</returns>
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x0007E57B File Offset: 0x0007C77B
		[global::__DynamicallyInvokable]
		public static int IPv6LoopbackInterfaceIndex
		{
			[global::__DynamicallyInvokable]
			get
			{
				return SystemNetworkInterface.InternalIPv6LoopbackInterfaceIndex;
			}
		}

		/// <summary>Gets the identifier of the network adapter.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the identifier.</returns>
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x0007E582 File Offset: 0x0007C782
		[global::__DynamicallyInvokable]
		public virtual string Id
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the network adapter.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the adapter name.</returns>
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x0007E589 File Offset: 0x0007C789
		[global::__DynamicallyInvokable]
		public virtual string Name
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the description of the interface.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes this interface.</returns>
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x0007E590 File Offset: 0x0007C790
		[global::__DynamicallyInvokable]
		public virtual string Description
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns an object that describes the configuration of this network interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPInterfaceProperties" /> object that describes this network interface.</returns>
		// Token: 0x060019FA RID: 6650 RVA: 0x0007E597 File Offset: 0x0007C797
		[global::__DynamicallyInvokable]
		public virtual IPInterfaceProperties GetIPProperties()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the IPv4 statistics for this <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPv4InterfaceStatistics" /> object.</returns>
		// Token: 0x060019FB RID: 6651 RVA: 0x0007E59E File Offset: 0x0007C79E
		[global::__DynamicallyInvokable]
		public virtual IPv4InterfaceStatistics GetIPv4Statistics()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the IP statistics for this <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> instance.</summary>
		/// <returns>The IP statistics.</returns>
		// Token: 0x060019FC RID: 6652 RVA: 0x0007E5A5 File Offset: 0x0007C7A5
		[global::__DynamicallyInvokable]
		public virtual IPInterfaceStatistics GetIPStatistics()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the current operational state of the network connection.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.OperationalStatus" /> values.</returns>
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x0007E5AC File Offset: 0x0007C7AC
		[global::__DynamicallyInvokable]
		public virtual OperationalStatus OperationalStatus
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the speed of the network interface.</summary>
		/// <returns>A <see cref="T:System.Int64" /> value that specifies the speed in bits per second.</returns>
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x0007E5B3 File Offset: 0x0007C7B3
		[global::__DynamicallyInvokable]
		public virtual long Speed
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the network interface is set to only receive data packets.</summary>
		/// <returns>
		///   <see langword="true" /> if the interface only receives network traffic; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0007E5BA File Offset: 0x0007C7BA
		[global::__DynamicallyInvokable]
		public virtual bool IsReceiveOnly
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the network interface is enabled to receive multicast packets.</summary>
		/// <returns>
		///   <see langword="true" /> if the interface receives multicast packets; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x0007E5C1 File Offset: 0x0007C7C1
		[global::__DynamicallyInvokable]
		public virtual bool SupportsMulticast
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns the Media Access Control (MAC) or physical address for this adapter.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> object that contains the physical address.</returns>
		// Token: 0x06001A01 RID: 6657 RVA: 0x0007E5C8 File Offset: 0x0007C7C8
		[global::__DynamicallyInvokable]
		public virtual PhysicalAddress GetPhysicalAddress()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the interface type.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.NetworkInterfaceType" /> value that specifies the network interface type.</returns>
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x0007E5CF File Offset: 0x0007C7CF
		[global::__DynamicallyInvokable]
		public virtual NetworkInterfaceType NetworkInterfaceType
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the interface supports the specified protocol.</summary>
		/// <param name="networkInterfaceComponent">A <see cref="T:System.Net.NetworkInformation.NetworkInterfaceComponent" /> value.</param>
		/// <returns>
		///   <see langword="true" /> if the specified protocol is supported; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001A03 RID: 6659 RVA: 0x0007E5D6 File Offset: 0x0007C7D6
		[global::__DynamicallyInvokable]
		public virtual bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> class.</summary>
		// Token: 0x06001A04 RID: 6660 RVA: 0x0007E5DD File Offset: 0x0007C7DD
		[global::__DynamicallyInvokable]
		protected NetworkInterface()
		{
		}
	}
}
