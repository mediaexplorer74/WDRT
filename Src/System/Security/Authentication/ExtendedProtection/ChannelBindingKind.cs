using System;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBindingKind" /> enumeration represents the kinds of channel bindings that can be queried from secure channels.</summary>
	// Token: 0x02000441 RID: 1089
	[global::__DynamicallyInvokable]
	public enum ChannelBindingKind
	{
		/// <summary>An unknown channel binding type.</summary>
		// Token: 0x04002245 RID: 8773
		[global::__DynamicallyInvokable]
		Unknown,
		/// <summary>A channel binding completely unique to a given channel (a TLS session key, for example).</summary>
		// Token: 0x04002246 RID: 8774
		[global::__DynamicallyInvokable]
		Unique = 25,
		/// <summary>A channel binding unique to a given endpoint (a TLS server certificate, for example).</summary>
		// Token: 0x04002247 RID: 8775
		[global::__DynamicallyInvokable]
		Endpoint
	}
}
