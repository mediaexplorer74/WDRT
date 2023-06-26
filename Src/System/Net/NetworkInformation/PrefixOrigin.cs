using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies how an IP address network prefix was located.</summary>
	// Token: 0x020002EE RID: 750
	[global::__DynamicallyInvokable]
	public enum PrefixOrigin
	{
		/// <summary>The prefix was located using an unspecified source.</summary>
		// Token: 0x04001A7A RID: 6778
		[global::__DynamicallyInvokable]
		Other,
		/// <summary>The prefix was manually configured.</summary>
		// Token: 0x04001A7B RID: 6779
		[global::__DynamicallyInvokable]
		Manual,
		/// <summary>The prefix is a well-known prefix. Well-known prefixes are specified in standard-track Request for Comments (RFC) documents and assigned by the Internet Assigned Numbers Authority (Iana) or an address registry. Such prefixes are reserved for special purposes.</summary>
		// Token: 0x04001A7C RID: 6780
		[global::__DynamicallyInvokable]
		WellKnown,
		/// <summary>The prefix was supplied by a Dynamic Host Configuration Protocol (DHCP) server.</summary>
		// Token: 0x04001A7D RID: 6781
		[global::__DynamicallyInvokable]
		Dhcp,
		/// <summary>The prefix was supplied by a router advertisement.</summary>
		// Token: 0x04001A7E RID: 6782
		[global::__DynamicallyInvokable]
		RouterAdvertisement
	}
}
