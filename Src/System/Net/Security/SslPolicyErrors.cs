using System;

namespace System.Net.Security
{
	/// <summary>Enumerates Secure Socket Layer (SSL) policy errors.</summary>
	// Token: 0x02000358 RID: 856
	[Flags]
	[global::__DynamicallyInvokable]
	public enum SslPolicyErrors
	{
		/// <summary>No SSL policy errors.</summary>
		// Token: 0x04001CEE RID: 7406
		[global::__DynamicallyInvokable]
		None = 0,
		/// <summary>Certificate not available.</summary>
		// Token: 0x04001CEF RID: 7407
		[global::__DynamicallyInvokable]
		RemoteCertificateNotAvailable = 1,
		/// <summary>Certificate name mismatch.</summary>
		// Token: 0x04001CF0 RID: 7408
		[global::__DynamicallyInvokable]
		RemoteCertificateNameMismatch = 2,
		/// <summary>
		///   <see cref="P:System.Security.Cryptography.X509Certificates.X509Chain.ChainStatus" /> has returned a non empty array.</summary>
		// Token: 0x04001CF1 RID: 7409
		[global::__DynamicallyInvokable]
		RemoteCertificateChainErrors = 4
	}
}
