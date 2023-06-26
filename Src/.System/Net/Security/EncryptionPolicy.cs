using System;

namespace System.Net.Security
{
	/// <summary>The EncryptionPolicy to use.</summary>
	// Token: 0x02000359 RID: 857
	public enum EncryptionPolicy
	{
		/// <summary>Require encryption and never allow a NULL cipher.</summary>
		// Token: 0x04001CF3 RID: 7411
		RequireEncryption,
		/// <summary>Prefer that full encryption be used, but allow a NULL cipher (no encryption) if the server agrees.</summary>
		// Token: 0x04001CF4 RID: 7412
		AllowNoEncryption,
		/// <summary>Allow no encryption and request that a NULL cipher be used if the other endpoint can handle a NULL cipher.</summary>
		// Token: 0x04001CF5 RID: 7413
		NoEncryption
	}
}
