using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F8 RID: 760
	internal class SystemIPInterfaceProperties : IPInterfaceProperties
	{
		// Token: 0x06001ABC RID: 6844 RVA: 0x00080D18 File Offset: 0x0007EF18
		internal SystemIPInterfaceProperties(FixedInfo fixedInfo, IpAdapterAddresses ipAdapterAddresses)
		{
			this.adapterFlags = ipAdapterAddresses.flags;
			this.dnsSuffix = ipAdapterAddresses.dnsSuffix;
			this.dnsEnabled = fixedInfo.EnableDns;
			this.dynamicDnsEnabled = (ipAdapterAddresses.flags & AdapterFlags.DnsEnabled) > (AdapterFlags)0;
			this.multicastAddresses = SystemMulticastIPAddressInformation.ToMulticastIpAddressInformationCollection(IpAdapterAddress.MarshalIpAddressInformationCollection(ipAdapterAddresses.firstMulticastAddress));
			this.dnsAddresses = IpAdapterAddress.MarshalIpAddressCollection(ipAdapterAddresses.firstDnsServerAddress);
			this.anycastAddresses = IpAdapterAddress.MarshalIpAddressInformationCollection(ipAdapterAddresses.firstAnycastAddress);
			this.unicastAddresses = SystemUnicastIPAddressInformation.MarshalUnicastIpAddressInformationCollection(ipAdapterAddresses.firstUnicastAddress);
			this.winsServersAddresses = IpAdapterAddress.MarshalIpAddressCollection(ipAdapterAddresses.firstWinsServerAddress);
			this.gatewayAddresses = SystemGatewayIPAddressInformation.ToGatewayIpAddressInformationCollection(IpAdapterAddress.MarshalIpAddressCollection(ipAdapterAddresses.firstGatewayAddress));
			this.dhcpServers = new IPAddressCollection();
			if (ipAdapterAddresses.dhcpv4Server.address != IntPtr.Zero)
			{
				this.dhcpServers.InternalAdd(ipAdapterAddresses.dhcpv4Server.MarshalIPAddress());
			}
			if (ipAdapterAddresses.dhcpv6Server.address != IntPtr.Zero)
			{
				this.dhcpServers.InternalAdd(ipAdapterAddresses.dhcpv6Server.MarshalIPAddress());
			}
			if ((this.adapterFlags & AdapterFlags.IPv4Enabled) != (AdapterFlags)0)
			{
				this.ipv4Properties = new SystemIPv4InterfaceProperties(fixedInfo, ipAdapterAddresses);
			}
			if ((this.adapterFlags & AdapterFlags.IPv6Enabled) != (AdapterFlags)0)
			{
				this.ipv6Properties = new SystemIPv6InterfaceProperties(ipAdapterAddresses.ipv6Index, ipAdapterAddresses.mtu, ipAdapterAddresses.zoneIndices);
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x00080E7E File Offset: 0x0007F07E
		public override bool IsDnsEnabled
		{
			get
			{
				return this.dnsEnabled;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x00080E86 File Offset: 0x0007F086
		public override bool IsDynamicDnsEnabled
		{
			get
			{
				return this.dynamicDnsEnabled;
			}
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00080E8E File Offset: 0x0007F08E
		public override IPv4InterfaceProperties GetIPv4Properties()
		{
			if ((this.adapterFlags & AdapterFlags.IPv4Enabled) == (AdapterFlags)0)
			{
				throw new NetworkInformationException(SocketError.ProtocolNotSupported);
			}
			return this.ipv4Properties;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00080EAF File Offset: 0x0007F0AF
		public override IPv6InterfaceProperties GetIPv6Properties()
		{
			if ((this.adapterFlags & AdapterFlags.IPv6Enabled) == (AdapterFlags)0)
			{
				throw new NetworkInformationException(SocketError.ProtocolNotSupported);
			}
			return this.ipv6Properties;
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x00080ED0 File Offset: 0x0007F0D0
		public override string DnsSuffix
		{
			get
			{
				return this.dnsSuffix;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x00080ED8 File Offset: 0x0007F0D8
		public override IPAddressInformationCollection AnycastAddresses
		{
			get
			{
				return this.anycastAddresses;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x00080EE0 File Offset: 0x0007F0E0
		public override UnicastIPAddressInformationCollection UnicastAddresses
		{
			get
			{
				return this.unicastAddresses;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x00080EE8 File Offset: 0x0007F0E8
		public override MulticastIPAddressInformationCollection MulticastAddresses
		{
			get
			{
				return this.multicastAddresses;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x00080EF0 File Offset: 0x0007F0F0
		public override IPAddressCollection DnsAddresses
		{
			get
			{
				return this.dnsAddresses;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x00080EF8 File Offset: 0x0007F0F8
		public override GatewayIPAddressInformationCollection GatewayAddresses
		{
			get
			{
				return this.gatewayAddresses;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x00080F00 File Offset: 0x0007F100
		public override IPAddressCollection DhcpServerAddresses
		{
			get
			{
				return this.dhcpServers;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x00080F08 File Offset: 0x0007F108
		public override IPAddressCollection WinsServersAddresses
		{
			get
			{
				return this.winsServersAddresses;
			}
		}

		// Token: 0x04001AA0 RID: 6816
		private bool dnsEnabled;

		// Token: 0x04001AA1 RID: 6817
		private bool dynamicDnsEnabled;

		// Token: 0x04001AA2 RID: 6818
		private IPAddressCollection dnsAddresses;

		// Token: 0x04001AA3 RID: 6819
		private UnicastIPAddressInformationCollection unicastAddresses;

		// Token: 0x04001AA4 RID: 6820
		private MulticastIPAddressInformationCollection multicastAddresses;

		// Token: 0x04001AA5 RID: 6821
		private IPAddressInformationCollection anycastAddresses;

		// Token: 0x04001AA6 RID: 6822
		private AdapterFlags adapterFlags;

		// Token: 0x04001AA7 RID: 6823
		private string dnsSuffix;

		// Token: 0x04001AA8 RID: 6824
		private SystemIPv4InterfaceProperties ipv4Properties;

		// Token: 0x04001AA9 RID: 6825
		private SystemIPv6InterfaceProperties ipv6Properties;

		// Token: 0x04001AAA RID: 6826
		private IPAddressCollection winsServersAddresses;

		// Token: 0x04001AAB RID: 6827
		private GatewayIPAddressInformationCollection gatewayAddresses;

		// Token: 0x04001AAC RID: 6828
		private IPAddressCollection dhcpServers;
	}
}
