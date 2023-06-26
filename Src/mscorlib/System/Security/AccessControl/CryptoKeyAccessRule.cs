using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an access rule for a cryptographic key. An access rule represents a combination of a user's identity, an access mask, and an access control type (allow or deny). An access rule object also contains information about the how the rule is inherited by child objects and how that inheritance is propagated.</summary>
	// Token: 0x02000211 RID: 529
	public sealed class CryptoKeyAccessRule : AccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeyAccessRule" /> class using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies. This parameter must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="cryptoKeyRights">The cryptographic key operation to which this access rule controls access.</param>
		/// <param name="type">The valid access control type.</param>
		// Token: 0x06001EF7 RID: 7927 RVA: 0x0006D0ED File Offset: 0x0006B2ED
		public CryptoKeyAccessRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AccessControlType type)
			: this(identity, CryptoKeyAccessRule.AccessMaskFromRights(cryptoKeyRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeyAccessRule" /> class using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies.</param>
		/// <param name="cryptoKeyRights">The cryptographic key operation to which this access rule controls access.</param>
		/// <param name="type">The valid access control type.</param>
		// Token: 0x06001EF8 RID: 7928 RVA: 0x0006D101 File Offset: 0x0006B301
		public CryptoKeyAccessRule(string identity, CryptoKeyRights cryptoKeyRights, AccessControlType type)
			: this(new NTAccount(identity), CryptoKeyAccessRule.AccessMaskFromRights(cryptoKeyRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0006D11A File Offset: 0x0006B31A
		private CryptoKeyAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		/// <summary>Gets the cryptographic key operation to which this access rule controls access.</summary>
		/// <returns>The cryptographic key operation to which this access rule controls access.</returns>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x0006D12B File Offset: 0x0006B32B
		public CryptoKeyRights CryptoKeyRights
		{
			get
			{
				return CryptoKeyAccessRule.RightsFromAccessMask(base.AccessMask);
			}
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x0006D138 File Offset: 0x0006B338
		private static int AccessMaskFromRights(CryptoKeyRights cryptoKeyRights, AccessControlType controlType)
		{
			if (controlType == AccessControlType.Allow)
			{
				cryptoKeyRights |= CryptoKeyRights.Synchronize;
			}
			else
			{
				if (controlType != AccessControlType.Deny)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidEnumValue", new object[] { controlType, "controlType" }), "controlType");
				}
				if (cryptoKeyRights != CryptoKeyRights.FullControl)
				{
					cryptoKeyRights &= ~CryptoKeyRights.Synchronize;
				}
			}
			return (int)cryptoKeyRights;
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x0006D197 File Offset: 0x0006B397
		internal static CryptoKeyRights RightsFromAccessMask(int accessMask)
		{
			return (CryptoKeyRights)accessMask;
		}
	}
}
