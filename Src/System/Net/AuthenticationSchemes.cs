using System;

namespace System.Net
{
	/// <summary>Specifies protocols for authentication.</summary>
	// Token: 0x020000C4 RID: 196
	[Flags]
	[global::__DynamicallyInvokable]
	public enum AuthenticationSchemes
	{
		/// <summary>No authentication is allowed. A client requesting an <see cref="T:System.Net.HttpListener" /> object with this flag set will always receive a 403 Forbidden status. Use this flag when a resource should never be served to a client.</summary>
		// Token: 0x04000C7A RID: 3194
		[global::__DynamicallyInvokable]
		None = 0,
		/// <summary>Specifies digest authentication.</summary>
		// Token: 0x04000C7B RID: 3195
		[global::__DynamicallyInvokable]
		Digest = 1,
		/// <summary>Negotiates with the client to determine the authentication scheme. If both client and server support Kerberos, it is used; otherwise, NTLM is used.</summary>
		// Token: 0x04000C7C RID: 3196
		[global::__DynamicallyInvokable]
		Negotiate = 2,
		/// <summary>Specifies NTLM authentication.</summary>
		// Token: 0x04000C7D RID: 3197
		[global::__DynamicallyInvokable]
		Ntlm = 4,
		/// <summary>Specifies basic authentication.</summary>
		// Token: 0x04000C7E RID: 3198
		[global::__DynamicallyInvokable]
		Basic = 8,
		/// <summary>Specifies anonymous authentication.</summary>
		// Token: 0x04000C7F RID: 3199
		[global::__DynamicallyInvokable]
		Anonymous = 32768,
		/// <summary>Specifies Windows authentication.</summary>
		// Token: 0x04000C80 RID: 3200
		[global::__DynamicallyInvokable]
		IntegratedWindowsAuthentication = 6
	}
}
