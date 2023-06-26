using System;

namespace System.Net
{
	/// <summary>Provides the interface for retrieving credentials for a host, port, and authentication type.</summary>
	// Token: 0x02000112 RID: 274
	[global::__DynamicallyInvokable]
	public interface ICredentialsByHost
	{
		/// <summary>Returns the credential for the specified host, port, and authentication protocol.</summary>
		/// <param name="host">The host computer that is authenticating the client.</param>
		/// <param name="port">The port on <paramref name="host" /> that the client will communicate with.</param>
		/// <param name="authenticationType">The authentication protocol.</param>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> for the specified host, port, and authentication protocol, or <see langword="null" /> if there are no credentials available for the specified host, port, and authentication protocol.</returns>
		// Token: 0x06000AFD RID: 2813
		[global::__DynamicallyInvokable]
		NetworkCredential GetCredential(string host, int port, string authenticationType);
	}
}
