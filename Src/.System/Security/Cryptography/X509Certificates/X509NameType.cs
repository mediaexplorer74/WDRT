using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the type of name the X509 certificate contains.</summary>
	// Token: 0x02000463 RID: 1123
	public enum X509NameType
	{
		/// <summary>The simple name of a subject or issuer of an X509 certificate.</summary>
		// Token: 0x04002596 RID: 9622
		SimpleName,
		/// <summary>The email address of the subject or issuer associated of an X509 certificate.</summary>
		// Token: 0x04002597 RID: 9623
		EmailName,
		/// <summary>The UPN name of the subject or issuer of an X509 certificate.</summary>
		// Token: 0x04002598 RID: 9624
		UpnName,
		/// <summary>The DNS name associated with the alternative name of either the subject or issuer of an X509 certificate.</summary>
		// Token: 0x04002599 RID: 9625
		DnsName,
		/// <summary>The DNS name associated with the alternative name of either the subject or the issuer of an X.509 certificate.  This value is equivalent to the <see cref="F:System.Security.Cryptography.X509Certificates.X509NameType.DnsName" /> value.</summary>
		// Token: 0x0400259A RID: 9626
		DnsFromAlternativeName,
		/// <summary>The URL address associated with the alternative name of either the subject or issuer of an X509 certificate.</summary>
		// Token: 0x0400259B RID: 9627
		UrlName
	}
}
