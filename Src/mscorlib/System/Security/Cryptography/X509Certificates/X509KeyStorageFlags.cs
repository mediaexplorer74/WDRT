using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines where and how to import the private key of an X.509 certificate.</summary>
	// Token: 0x020002AD RID: 685
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum X509KeyStorageFlags
	{
		/// <summary>The default key set is used.  The user key set is usually the default.</summary>
		// Token: 0x04000D9E RID: 3486
		DefaultKeySet = 0,
		/// <summary>Private keys are stored in the current user store rather than the local computer store. This occurs even if the certificate specifies that the keys should go in the local computer store.</summary>
		// Token: 0x04000D9F RID: 3487
		UserKeySet = 1,
		/// <summary>Private keys are stored in the local computer store rather than the current user store.</summary>
		// Token: 0x04000DA0 RID: 3488
		MachineKeySet = 2,
		/// <summary>Imported keys are marked as exportable.</summary>
		// Token: 0x04000DA1 RID: 3489
		Exportable = 4,
		/// <summary>Notify the user through a dialog box or other method that the key is accessed.  The Cryptographic Service Provider (CSP) in use defines the precise behavior.</summary>
		// Token: 0x04000DA2 RID: 3490
		UserProtected = 8,
		/// <summary>The key associated with a PFX file is persisted when importing a certificate.</summary>
		// Token: 0x04000DA3 RID: 3491
		PersistKeySet = 16,
		/// <summary>The key associated with a PFX file is created in memory and not persisted on disk when importing a certificate.</summary>
		// Token: 0x04000DA4 RID: 3492
		EphemeralKeySet = 32
	}
}
