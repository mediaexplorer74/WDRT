using System;

namespace System.Security.Cryptography
{
	/// <summary>Identifies Windows cryptographic object identifier (OID) groups.</summary>
	// Token: 0x0200045D RID: 1117
	public enum OidGroup
	{
		/// <summary>All the groups.</summary>
		// Token: 0x04002578 RID: 9592
		All,
		/// <summary>The Windows group that is represented by CRYPT_HASH_ALG_OID_GROUP_ID.</summary>
		// Token: 0x04002579 RID: 9593
		HashAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_ENCRYPT_ALG_OID_GROUP_ID.</summary>
		// Token: 0x0400257A RID: 9594
		EncryptionAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_PUBKEY_ALG_OID_GROUP_ID.</summary>
		// Token: 0x0400257B RID: 9595
		PublicKeyAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_SIGN_ALG_OID_GROUP_ID.</summary>
		// Token: 0x0400257C RID: 9596
		SignatureAlgorithm,
		/// <summary>The Windows group that is represented by CRYPT_RDN_ATTR_OID_GROUP_ID.</summary>
		// Token: 0x0400257D RID: 9597
		Attribute,
		/// <summary>The Windows group that is represented by CRYPT_EXT_OR_ATTR_OID_GROUP_ID.</summary>
		// Token: 0x0400257E RID: 9598
		ExtensionOrAttribute,
		/// <summary>The Windows group that is represented by CRYPT_ENHKEY_USAGE_OID_GROUP_ID.</summary>
		// Token: 0x0400257F RID: 9599
		EnhancedKeyUsage,
		/// <summary>The Windows group that is represented by CRYPT_POLICY_OID_GROUP_ID.</summary>
		// Token: 0x04002580 RID: 9600
		Policy,
		/// <summary>The Windows group that is represented by CRYPT_TEMPLATE_OID_GROUP_ID.</summary>
		// Token: 0x04002581 RID: 9601
		Template,
		/// <summary>The Windows group that is represented by CRYPT_KDF_OID_GROUP_ID.</summary>
		// Token: 0x04002582 RID: 9602
		KeyDerivationFunction
	}
}
