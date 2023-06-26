using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F5 RID: 757
	internal class SystemIPAddressInformation : IPAddressInformation
	{
		// Token: 0x06001A96 RID: 6806 RVA: 0x0008024F File Offset: 0x0007E44F
		internal SystemIPAddressInformation(IPAddress address, AdapterAddressFlags flags)
		{
			this.address = address;
			this.transient = (flags & AdapterAddressFlags.Transient) > (AdapterAddressFlags)0;
			this.dnsEligible = (flags & AdapterAddressFlags.DnsEligible) > (AdapterAddressFlags)0;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x0008027D File Offset: 0x0007E47D
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x00080285 File Offset: 0x0007E485
		public override bool IsTransient
		{
			get
			{
				return this.transient;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0008028D File Offset: 0x0007E48D
		public override bool IsDnsEligible
		{
			get
			{
				return this.dnsEligible;
			}
		}

		// Token: 0x04001A97 RID: 6807
		private IPAddress address;

		// Token: 0x04001A98 RID: 6808
		internal bool transient;

		// Token: 0x04001A99 RID: 6809
		internal bool dnsEligible = true;
	}
}
