using System;

namespace System.Security.Authentication
{
	/// <summary>Defines the possible cipher algorithms for the <see cref="T:System.Net.Security.SslStream" /> class.</summary>
	// Token: 0x0200043E RID: 1086
	[global::__DynamicallyInvokable]
	public enum CipherAlgorithmType
	{
		/// <summary>No encryption algorithm is used.</summary>
		// Token: 0x04002233 RID: 8755
		[global::__DynamicallyInvokable]
		None,
		/// <summary>Rivest's Code 2 (RC2) algorithm.</summary>
		// Token: 0x04002234 RID: 8756
		[global::__DynamicallyInvokable]
		Rc2 = 26114,
		/// <summary>Rivest's Code 4 (RC4) algorithm.</summary>
		// Token: 0x04002235 RID: 8757
		[global::__DynamicallyInvokable]
		Rc4 = 26625,
		/// <summary>The Data Encryption Standard (DES) algorithm.</summary>
		// Token: 0x04002236 RID: 8758
		[global::__DynamicallyInvokable]
		Des = 26113,
		/// <summary>The Triple Data Encryption Standard (3DES) algorithm.</summary>
		// Token: 0x04002237 RID: 8759
		[global::__DynamicallyInvokable]
		TripleDes = 26115,
		/// <summary>The Advanced Encryption Standard (AES) algorithm.</summary>
		// Token: 0x04002238 RID: 8760
		[global::__DynamicallyInvokable]
		Aes = 26129,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 128 bit key.</summary>
		// Token: 0x04002239 RID: 8761
		[global::__DynamicallyInvokable]
		Aes128 = 26126,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 192 bit key.</summary>
		// Token: 0x0400223A RID: 8762
		[global::__DynamicallyInvokable]
		Aes192,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 256 bit key.</summary>
		// Token: 0x0400223B RID: 8763
		[global::__DynamicallyInvokable]
		Aes256,
		/// <summary>No encryption is used with a Null cipher algorithm.</summary>
		// Token: 0x0400223C RID: 8764
		[global::__DynamicallyInvokable]
		Null = 24576
	}
}
