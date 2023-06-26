using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the cryptographic key operation for which an authorization rule controls access or auditing.</summary>
	// Token: 0x02000210 RID: 528
	[Flags]
	public enum CryptoKeyRights
	{
		/// <summary>Read the key data.</summary>
		// Token: 0x04000B15 RID: 2837
		ReadData = 1,
		/// <summary>Write key data.</summary>
		// Token: 0x04000B16 RID: 2838
		WriteData = 2,
		/// <summary>Read extended attributes of the key.</summary>
		// Token: 0x04000B17 RID: 2839
		ReadExtendedAttributes = 8,
		/// <summary>Write extended attributes of the key.</summary>
		// Token: 0x04000B18 RID: 2840
		WriteExtendedAttributes = 16,
		/// <summary>Read attributes of the key.</summary>
		// Token: 0x04000B19 RID: 2841
		ReadAttributes = 128,
		/// <summary>Write attributes of the key.</summary>
		// Token: 0x04000B1A RID: 2842
		WriteAttributes = 256,
		/// <summary>Delete the key.</summary>
		// Token: 0x04000B1B RID: 2843
		Delete = 65536,
		/// <summary>Read permissions for the key.</summary>
		// Token: 0x04000B1C RID: 2844
		ReadPermissions = 131072,
		/// <summary>Change permissions for the key.</summary>
		// Token: 0x04000B1D RID: 2845
		ChangePermissions = 262144,
		/// <summary>Take ownership of the key.</summary>
		// Token: 0x04000B1E RID: 2846
		TakeOwnership = 524288,
		/// <summary>Use the key for synchronization.</summary>
		// Token: 0x04000B1F RID: 2847
		Synchronize = 1048576,
		/// <summary>Full control of the key.</summary>
		// Token: 0x04000B20 RID: 2848
		FullControl = 2032027,
		/// <summary>A combination of <see cref="F:System.Security.AccessControl.CryptoKeyRights.GenericRead" /> and <see cref="F:System.Security.AccessControl.CryptoKeyRights.GenericWrite" />.</summary>
		// Token: 0x04000B21 RID: 2849
		GenericAll = 268435456,
		/// <summary>Not used.</summary>
		// Token: 0x04000B22 RID: 2850
		GenericExecute = 536870912,
		/// <summary>Write the key data, extended attributes of the key, attributes of the key, and permissions for the key.</summary>
		// Token: 0x04000B23 RID: 2851
		GenericWrite = 1073741824,
		/// <summary>Read the key data, extended attributes of the key, attributes of the key, and permissions for the key.</summary>
		// Token: 0x04000B24 RID: 2852
		GenericRead = -2147483648
	}
}
