using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the mode of a cryptographic stream.</summary>
	// Token: 0x02000256 RID: 598
	[ComVisible(true)]
	[Serializable]
	public enum CryptoStreamMode
	{
		/// <summary>Read access to a cryptographic stream.</summary>
		// Token: 0x04000C1A RID: 3098
		Read,
		/// <summary>Write access to a cryptographic stream.</summary>
		// Token: 0x04000C1B RID: 3099
		Write
	}
}
