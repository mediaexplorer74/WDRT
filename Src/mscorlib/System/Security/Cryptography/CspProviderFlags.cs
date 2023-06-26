using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies flags that modify the behavior of the cryptographic service providers (CSP).</summary>
	// Token: 0x02000253 RID: 595
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum CspProviderFlags
	{
		/// <summary>Do not specify any settings.</summary>
		// Token: 0x04000BFD RID: 3069
		NoFlags = 0,
		/// <summary>Use key information from the computer's key store.</summary>
		// Token: 0x04000BFE RID: 3070
		UseMachineKeyStore = 1,
		/// <summary>Use key information from the default key container.</summary>
		// Token: 0x04000BFF RID: 3071
		UseDefaultKeyContainer = 2,
		/// <summary>Use key information that cannot be exported.</summary>
		// Token: 0x04000C00 RID: 3072
		UseNonExportableKey = 4,
		/// <summary>Use key information from the current key.</summary>
		// Token: 0x04000C01 RID: 3073
		UseExistingKey = 8,
		/// <summary>Allow a key to be exported for archival or recovery.</summary>
		// Token: 0x04000C02 RID: 3074
		UseArchivableKey = 16,
		/// <summary>Notify the user through a dialog box or another method when certain actions are attempting to use a key.  This flag is not compatible with the <see cref="F:System.Security.Cryptography.CspProviderFlags.NoPrompt" /> flag.</summary>
		// Token: 0x04000C03 RID: 3075
		UseUserProtectedKey = 32,
		/// <summary>Prevent the CSP from displaying any user interface (UI) for this context.</summary>
		// Token: 0x04000C04 RID: 3076
		NoPrompt = 64,
		/// <summary>Create a temporary key that is released when the associated Rivest-Shamir-Adleman (RSA) object is closed. Do not use this flag if you want your key to be independent of the RSA object.</summary>
		// Token: 0x04000C05 RID: 3077
		CreateEphemeralKey = 128
	}
}
