using System;

namespace System.Net
{
	/// <summary>Provides the base authentication interface for Web client authentication modules.</summary>
	// Token: 0x0200010F RID: 271
	public interface IAuthenticationModule
	{
		/// <summary>Returns an instance of the <see cref="T:System.Net.Authorization" /> class in response to an authentication challenge from a server.</summary>
		/// <param name="challenge">The authentication challenge sent by the server.</param>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> instance associated with the challenge.</param>
		/// <param name="credentials">The credentials associated with the challenge.</param>
		/// <returns>An <see cref="T:System.Net.Authorization" /> instance containing the authorization message for the request, or <see langword="null" /> if the challenge cannot be handled.</returns>
		// Token: 0x06000AF7 RID: 2807
		Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials);

		/// <summary>Returns an instance of the <see cref="T:System.Net.Authorization" /> class for an authentication request to a server.</summary>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> instance associated with the authentication request.</param>
		/// <param name="credentials">The credentials associated with the authentication request.</param>
		/// <returns>An <see cref="T:System.Net.Authorization" /> instance containing the authorization message for the request.</returns>
		// Token: 0x06000AF8 RID: 2808
		Authorization PreAuthenticate(WebRequest request, ICredentials credentials);

		/// <summary>Gets a value indicating whether the authentication module supports preauthentication.</summary>
		/// <returns>
		///   <see langword="true" /> if the authorization module supports preauthentication; otherwise <see langword="false" />.</returns>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000AF9 RID: 2809
		bool CanPreAuthenticate { get; }

		/// <summary>Gets the authentication type provided by this authentication module.</summary>
		/// <returns>A string indicating the authentication type provided by this authentication module.</returns>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000AFA RID: 2810
		string AuthenticationType { get; }
	}
}
