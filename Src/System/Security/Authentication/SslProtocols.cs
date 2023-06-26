using System;

namespace System.Security.Authentication
{
	/// <summary>Defines the possible versions of <see cref="T:System.Security.Authentication.SslProtocols" />.</summary>
	// Token: 0x0200043C RID: 1084
	[Flags]
	[global::__DynamicallyInvokable]
	public enum SslProtocols
	{
		/// <summary>Allows the operating system to choose the best protocol to use, and to block protocols that are not secure. Unless your app has a specific reason not to, you should use this field.</summary>
		// Token: 0x04002225 RID: 8741
		[global::__DynamicallyInvokable]
		None = 0,
		/// <summary>Specifies the SSL 2.0 protocol. SSL 2.0 has been superseded by the TLS protocol and is provided for backward compatibility only.</summary>
		// Token: 0x04002226 RID: 8742
		[global::__DynamicallyInvokable]
		Ssl2 = 12,
		/// <summary>Specifies the SSL 3.0 protocol. SSL 3.0 has been superseded by the TLS protocol and is provided for backward compatibility only.</summary>
		// Token: 0x04002227 RID: 8743
		[global::__DynamicallyInvokable]
		Ssl3 = 48,
		/// <summary>Specifies the TLS 1.0 security protocol. The TLS protocol is defined in IETF RFC 2246.</summary>
		// Token: 0x04002228 RID: 8744
		[global::__DynamicallyInvokable]
		Tls = 192,
		/// <summary>Specifies the TLS 1.1 security protocol. The TLS protocol is defined in IETF RFC 4346.</summary>
		// Token: 0x04002229 RID: 8745
		[global::__DynamicallyInvokable]
		Tls11 = 768,
		/// <summary>Specifies the TLS 1.2 security protocol. The TLS protocol is defined in IETF RFC 5246.</summary>
		// Token: 0x0400222A RID: 8746
		[global::__DynamicallyInvokable]
		Tls12 = 3072,
		/// <summary>Specifies the TLS 1.3 security protocol. The TLS protocol is defined in IETF RFC 8446.</summary>
		// Token: 0x0400222B RID: 8747
		Tls13 = 12288,
		/// <summary>Use None instead of Default. Default permits only the Secure Sockets Layer (SSL) 3.0 or Transport Layer Security (TLS) 1.0 protocols to be negotiated, and those options are now considered obsolete. Consequently, Default is not allowed in many organizations. Despite the name of this field, <see cref="T:System.Net.Security.SslStream" /> does not use it as a default except under special circumstances.</summary>
		// Token: 0x0400222C RID: 8748
		Default = 240
	}
}
