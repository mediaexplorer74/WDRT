using System;

namespace System
{
	/// <summary>Defines host name types for the <see cref="M:System.Uri.CheckHostName(System.String)" /> method.</summary>
	// Token: 0x02000040 RID: 64
	[global::__DynamicallyInvokable]
	public enum UriHostNameType
	{
		/// <summary>The type of the host name is not supplied.</summary>
		// Token: 0x0400043E RID: 1086
		[global::__DynamicallyInvokable]
		Unknown,
		/// <summary>The host is set, but the type cannot be determined.</summary>
		// Token: 0x0400043F RID: 1087
		[global::__DynamicallyInvokable]
		Basic,
		/// <summary>The host name is a domain name system (DNS) style host name.</summary>
		// Token: 0x04000440 RID: 1088
		[global::__DynamicallyInvokable]
		Dns,
		/// <summary>The host name is an Internet Protocol (IP) version 4 host address.</summary>
		// Token: 0x04000441 RID: 1089
		[global::__DynamicallyInvokable]
		IPv4,
		/// <summary>The host name is an Internet Protocol (IP) version 6 host address.</summary>
		// Token: 0x04000442 RID: 1090
		[global::__DynamicallyInvokable]
		IPv6
	}
}
