using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 4 (IPv4) or Internet Protocol version 6 (IPv6).</summary>
	// Token: 0x020002A4 RID: 676
	[global::__DynamicallyInvokable]
	public abstract class IPInterfaceProperties
	{
		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether NetBt is configured to use DNS name resolution on this interface.</summary>
		/// <returns>
		///   <see langword="true" /> if NetBt is configured to use DNS name resolution on this interface; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001933 RID: 6451
		[global::__DynamicallyInvokable]
		public abstract bool IsDnsEnabled
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the Domain Name System (DNS) suffix associated with this interface.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the DNS suffix for this interface, or <see cref="F:System.String.Empty" /> if there is no DNS suffix for the interface.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows 2000.</exception>
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001934 RID: 6452
		[global::__DynamicallyInvokable]
		public abstract string DnsSuffix
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface is configured to automatically register its IP address information with the Domain Name System (DNS).</summary>
		/// <returns>
		///   <see langword="true" /> if this interface is configured to automatically register a mapping between its dynamic IP address and static domain names; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001935 RID: 6453
		[global::__DynamicallyInvokable]
		public abstract bool IsDynamicDnsEnabled
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the unicast addresses assigned to this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> that contains the unicast addresses for this interface.</returns>
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001936 RID: 6454
		[global::__DynamicallyInvokable]
		public abstract UnicastIPAddressInformationCollection UnicastAddresses
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the multicast addresses assigned to this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformationCollection" /> that contains the multicast addresses for this interface.</returns>
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001937 RID: 6455
		[global::__DynamicallyInvokable]
		public abstract MulticastIPAddressInformationCollection MulticastAddresses
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the anycast IP addresses assigned to this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPAddressInformationCollection" /> that contains the anycast addresses for this interface.</returns>
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001938 RID: 6456
		[global::__DynamicallyInvokable]
		public abstract IPAddressInformationCollection AnycastAddresses
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the addresses of Domain Name System (DNS) servers for this interface.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> that contains the DNS server addresses.</returns>
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001939 RID: 6457
		[global::__DynamicallyInvokable]
		public abstract IPAddressCollection DnsAddresses
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the IPv4 network gateway addresses for this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformationCollection" /> that contains the address information for network gateways, or an empty array if no gateways are found.</returns>
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600193A RID: 6458
		[global::__DynamicallyInvokable]
		public abstract GatewayIPAddressInformationCollection GatewayAddresses
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the addresses of Dynamic Host Configuration Protocol (DHCP) servers for this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> that contains the address information for DHCP servers, or an empty array if no servers are found.</returns>
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600193B RID: 6459
		[global::__DynamicallyInvokable]
		public abstract IPAddressCollection DhcpServerAddresses
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the addresses of Windows Internet Name Service (WINS) servers.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> that contains the address information for WINS servers, or an empty array if no servers are found.</returns>
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600193C RID: 6460
		[global::__DynamicallyInvokable]
		public abstract IPAddressCollection WinsServersAddresses
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Provides Internet Protocol version 4 (IPv4) configuration data for this network interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPv4InterfaceProperties" /> object that contains IPv4 configuration data, or <see langword="null" /> if no data is available for the interface.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The interface does not support the IPv4 protocol.</exception>
		// Token: 0x0600193D RID: 6461
		[global::__DynamicallyInvokable]
		public abstract IPv4InterfaceProperties GetIPv4Properties();

		/// <summary>Provides Internet Protocol version 6 (IPv6) configuration data for this network interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPv6InterfaceProperties" /> object that contains IPv6 configuration data.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The interface does not support the IPv6 protocol.</exception>
		// Token: 0x0600193E RID: 6462
		[global::__DynamicallyInvokable]
		public abstract IPv6InterfaceProperties GetIPv6Properties();

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPInterfaceProperties" /> class.</summary>
		// Token: 0x0600193F RID: 6463 RVA: 0x0007DB82 File Offset: 0x0007BD82
		[global::__DynamicallyInvokable]
		protected IPInterfaceProperties()
		{
		}
	}
}
