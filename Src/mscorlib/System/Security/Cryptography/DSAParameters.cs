using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Contains the typical parameters for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
	// Token: 0x0200025B RID: 603
	[ComVisible(true)]
	[Serializable]
	public struct DSAParameters
	{
		/// <summary>Specifies the <see langword="P" /> parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000C2B RID: 3115
		public byte[] P;

		/// <summary>Specifies the <see langword="Q" /> parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000C2C RID: 3116
		public byte[] Q;

		/// <summary>Specifies the <see langword="G" /> parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000C2D RID: 3117
		public byte[] G;

		/// <summary>Specifies the <see langword="Y" /> parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000C2E RID: 3118
		public byte[] Y;

		/// <summary>Specifies the <see langword="J" /> parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000C2F RID: 3119
		public byte[] J;

		/// <summary>Specifies the <see langword="X" /> parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000C30 RID: 3120
		[NonSerialized]
		public byte[] X;

		/// <summary>Specifies the seed for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000C31 RID: 3121
		public byte[] Seed;

		/// <summary>Specifies the counter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000C32 RID: 3122
		public int Counter;
	}
}
