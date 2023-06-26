using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the standard parameters for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
	// Token: 0x0200027A RID: 634
	[ComVisible(true)]
	[Serializable]
	public struct RSAParameters
	{
		/// <summary>Represents the <see langword="Exponent" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000C85 RID: 3205
		public byte[] Exponent;

		/// <summary>Represents the <see langword="Modulus" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000C86 RID: 3206
		public byte[] Modulus;

		/// <summary>Represents the <see langword="P" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000C87 RID: 3207
		[NonSerialized]
		public byte[] P;

		/// <summary>Represents the <see langword="Q" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000C88 RID: 3208
		[NonSerialized]
		public byte[] Q;

		/// <summary>Represents the <see langword="DP" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000C89 RID: 3209
		[NonSerialized]
		public byte[] DP;

		/// <summary>Represents the <see langword="DQ" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000C8A RID: 3210
		[NonSerialized]
		public byte[] DQ;

		/// <summary>Represents the <see langword="InverseQ" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000C8B RID: 3211
		[NonSerialized]
		public byte[] InverseQ;

		/// <summary>Represents the <see langword="D" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000C8C RID: 3212
		[NonSerialized]
		public byte[] D;
	}
}
