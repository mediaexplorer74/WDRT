using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies how an IP address host suffix was located.</summary>
	// Token: 0x020002F1 RID: 753
	[global::__DynamicallyInvokable]
	public enum SuffixOrigin
	{
		/// <summary>The suffix was located using an unspecified source.</summary>
		// Token: 0x04001A80 RID: 6784
		[global::__DynamicallyInvokable]
		Other,
		/// <summary>The suffix was manually configured.</summary>
		// Token: 0x04001A81 RID: 6785
		[global::__DynamicallyInvokable]
		Manual,
		/// <summary>The suffix is a well-known suffix. Well-known suffixes are specified in standard-track Request for Comments (RFC) documents and assigned by the Internet Assigned Numbers Authority (Iana) or an address registry. Such suffixes are reserved for special purposes.</summary>
		// Token: 0x04001A82 RID: 6786
		[global::__DynamicallyInvokable]
		WellKnown,
		/// <summary>The suffix was supplied by a Dynamic Host Configuration Protocol (DHCP) server.</summary>
		// Token: 0x04001A83 RID: 6787
		[global::__DynamicallyInvokable]
		OriginDhcp,
		/// <summary>The suffix is a link-local suffix.</summary>
		// Token: 0x04001A84 RID: 6788
		[global::__DynamicallyInvokable]
		LinkLayerAddress,
		/// <summary>The suffix was randomly assigned.</summary>
		// Token: 0x04001A85 RID: 6789
		[global::__DynamicallyInvokable]
		Random
	}
}
