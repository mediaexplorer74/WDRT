using System;

namespace System.Net.Security
{
	/// <summary>Indicates the security services requested for an authenticated stream.</summary>
	// Token: 0x02000356 RID: 854
	public enum ProtectionLevel
	{
		/// <summary>Authentication only.</summary>
		// Token: 0x04001CDE RID: 7390
		None,
		/// <summary>Sign data to help ensure the integrity of transmitted data.</summary>
		// Token: 0x04001CDF RID: 7391
		Sign,
		/// <summary>Encrypt and sign data to help ensure the confidentiality and integrity of transmitted data.</summary>
		// Token: 0x04001CE0 RID: 7392
		EncryptAndSign
	}
}
