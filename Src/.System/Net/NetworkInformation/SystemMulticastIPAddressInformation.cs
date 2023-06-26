using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002FC RID: 764
	internal class SystemMulticastIPAddressInformation : MulticastIPAddressInformation
	{
		// Token: 0x06001AFC RID: 6908 RVA: 0x00081209 File Offset: 0x0007F409
		private SystemMulticastIPAddressInformation()
		{
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x00081211 File Offset: 0x0007F411
		public SystemMulticastIPAddressInformation(SystemIPAddressInformation addressInfo)
		{
			this.innerInfo = addressInfo;
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x00081220 File Offset: 0x0007F420
		public override IPAddress Address
		{
			get
			{
				return this.innerInfo.Address;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0008122D File Offset: 0x0007F42D
		public override bool IsTransient
		{
			get
			{
				return this.innerInfo.IsTransient;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x0008123A File Offset: 0x0007F43A
		public override bool IsDnsEligible
		{
			get
			{
				return this.innerInfo.IsDnsEligible;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x00081247 File Offset: 0x0007F447
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return PrefixOrigin.Other;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x0008124A File Offset: 0x0007F44A
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return SuffixOrigin.Other;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x0008124D File Offset: 0x0007F44D
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return DuplicateAddressDetectionState.Invalid;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x00081250 File Offset: 0x0007F450
		public override long AddressValidLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x00081254 File Offset: 0x0007F454
		public override long AddressPreferredLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x00081258 File Offset: 0x0007F458
		public override long DhcpLeaseLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0008125C File Offset: 0x0007F45C
		internal static MulticastIPAddressInformationCollection ToMulticastIpAddressInformationCollection(IPAddressInformationCollection addresses)
		{
			MulticastIPAddressInformationCollection multicastIPAddressInformationCollection = new MulticastIPAddressInformationCollection();
			foreach (IPAddressInformation ipaddressInformation in addresses)
			{
				multicastIPAddressInformationCollection.InternalAdd(new SystemMulticastIPAddressInformation((SystemIPAddressInformation)ipaddressInformation));
			}
			return multicastIPAddressInformationCollection;
		}

		// Token: 0x04001AB0 RID: 6832
		private SystemIPAddressInformation innerInfo;
	}
}
