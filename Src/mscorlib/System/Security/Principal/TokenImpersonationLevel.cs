using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines security impersonation levels. Security impersonation levels govern the degree to which a server process can act on behalf of a client process.</summary>
	// Token: 0x02000326 RID: 806
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum TokenImpersonationLevel
	{
		/// <summary>An impersonation level is not assigned.</summary>
		// Token: 0x04001029 RID: 4137
		[__DynamicallyInvokable]
		None,
		/// <summary>The server process cannot obtain identification information about the client, and it cannot impersonate the client.</summary>
		// Token: 0x0400102A RID: 4138
		[__DynamicallyInvokable]
		Anonymous,
		/// <summary>The server process can obtain information about the client, such as security identifiers and privileges, but it cannot impersonate the client. This is useful for servers that export their own objects, for example, database products that export tables and views. Using the retrieved client-security information, the server can make access-validation decisions without being able to use other services that are using the client's security context.</summary>
		// Token: 0x0400102B RID: 4139
		[__DynamicallyInvokable]
		Identification,
		/// <summary>The server process can impersonate the client's security context on its local system. The server cannot impersonate the client on remote systems.</summary>
		// Token: 0x0400102C RID: 4140
		[__DynamicallyInvokable]
		Impersonation,
		/// <summary>The server process can impersonate the client's security context on remote systems.</summary>
		// Token: 0x0400102D RID: 4141
		[__DynamicallyInvokable]
		Delegation
	}
}
