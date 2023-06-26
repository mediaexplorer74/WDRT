using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines how the certificate key can be used. If this value is not defined, the key can be used for any purpose.</summary>
	// Token: 0x02000475 RID: 1141
	[Flags]
	public enum X509KeyUsageFlags
	{
		/// <summary>No key usage parameters.</summary>
		// Token: 0x04002614 RID: 9748
		None = 0,
		/// <summary>The key can be used for encryption only.</summary>
		// Token: 0x04002615 RID: 9749
		EncipherOnly = 1,
		/// <summary>The key can be used to sign a certificate revocation list (CRL).</summary>
		// Token: 0x04002616 RID: 9750
		CrlSign = 2,
		/// <summary>The key can be used to sign certificates.</summary>
		// Token: 0x04002617 RID: 9751
		KeyCertSign = 4,
		/// <summary>The key can be used to determine key agreement, such as a key created using the Diffie-Hellman key agreement algorithm.</summary>
		// Token: 0x04002618 RID: 9752
		KeyAgreement = 8,
		/// <summary>The key can be used for data encryption.</summary>
		// Token: 0x04002619 RID: 9753
		DataEncipherment = 16,
		/// <summary>The key can be used for key encryption.</summary>
		// Token: 0x0400261A RID: 9754
		KeyEncipherment = 32,
		/// <summary>The key can be used for authentication.</summary>
		// Token: 0x0400261B RID: 9755
		NonRepudiation = 64,
		/// <summary>The key can be used as a digital signature.</summary>
		// Token: 0x0400261C RID: 9756
		DigitalSignature = 128,
		/// <summary>The key can be used for decryption only.</summary>
		// Token: 0x0400261D RID: 9757
		DecipherOnly = 32768
	}
}
