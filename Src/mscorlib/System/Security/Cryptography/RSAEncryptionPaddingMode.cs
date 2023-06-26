using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the padding mode to use with RSA encryption or decryption operations.</summary>
	// Token: 0x02000281 RID: 641
	public enum RSAEncryptionPaddingMode
	{
		/// <summary>PKCS #1 v1.5.</summary>
		// Token: 0x04000CA9 RID: 3241
		Pkcs1,
		/// <summary>Optimal Asymmetric Encryption Padding.</summary>
		// Token: 0x04000CAA RID: 3242
		Oaep
	}
}
