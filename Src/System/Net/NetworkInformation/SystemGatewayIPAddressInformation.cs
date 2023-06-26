using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002AF RID: 687
	internal class SystemGatewayIPAddressInformation : GatewayIPAddressInformation
	{
		// Token: 0x0600198E RID: 6542 RVA: 0x0007DD9D File Offset: 0x0007BF9D
		private SystemGatewayIPAddressInformation(IPAddress address)
		{
			this.address = address;
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x0007DDAC File Offset: 0x0007BFAC
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0007DDB4 File Offset: 0x0007BFB4
		internal static GatewayIPAddressInformationCollection ToGatewayIpAddressInformationCollection(IPAddressCollection addresses)
		{
			GatewayIPAddressInformationCollection gatewayIPAddressInformationCollection = new GatewayIPAddressInformationCollection();
			foreach (IPAddress ipaddress in addresses)
			{
				gatewayIPAddressInformationCollection.InternalAdd(new SystemGatewayIPAddressInformation(ipaddress));
			}
			return gatewayIPAddressInformationCollection;
		}

		// Token: 0x040018F9 RID: 6393
		private IPAddress address;
	}
}
