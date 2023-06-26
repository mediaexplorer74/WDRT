using System;

namespace System.Net.Security
{
	/// <summary>Specifies client requirements for authentication and impersonation when using the <see cref="T:System.Net.WebRequest" /> class and derived classes to request a resource.</summary>
	// Token: 0x02000355 RID: 853
	[global::__DynamicallyInvokable]
	public enum AuthenticationLevel
	{
		/// <summary>No authentication is required for the client and server.</summary>
		// Token: 0x04001CDA RID: 7386
		[global::__DynamicallyInvokable]
		None,
		/// <summary>The client and server should be authenticated. The request does not fail if the server is not authenticated. To determine whether mutual authentication occurred, check the value of the <see cref="P:System.Net.WebResponse.IsMutuallyAuthenticated" /> property.</summary>
		// Token: 0x04001CDB RID: 7387
		[global::__DynamicallyInvokable]
		MutualAuthRequested,
		/// <summary>The client and server should be authenticated. If the server is not authenticated, your application will receive an <see cref="T:System.IO.IOException" /> with a <see cref="T:System.Net.ProtocolViolationException" /> inner exception that indicates that mutual authentication failed</summary>
		// Token: 0x04001CDC RID: 7388
		[global::__DynamicallyInvokable]
		MutualAuthRequired
	}
}
