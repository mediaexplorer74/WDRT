using System;

namespace System.Security.AccessControl
{
	/// <summary>Defines the available access control entry (ACE) types.</summary>
	// Token: 0x020001FE RID: 510
	public enum AceType : byte
	{
		/// <summary>Allows access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x04000AC7 RID: 2759
		AccessAllowed,
		/// <summary>Denies access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x04000AC8 RID: 2760
		AccessDenied,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object. The trustee is identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x04000AC9 RID: 2761
		SystemAudit,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04000ACA RID: 2762
		SystemAlarm,
		/// <summary>Defined but never used. Included here for completeness.</summary>
		// Token: 0x04000ACB RID: 2763
		AccessAllowedCompound,
		/// <summary>Allows access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x04000ACC RID: 2764
		AccessAllowedObject,
		/// <summary>Denies access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x04000ACD RID: 2765
		AccessDeniedObject,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object or subobjects such as property sets or properties. The ACE contains a set of access rights, a GUID that identifies the type of object or subobject, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee for whom the system will audit access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x04000ACE RID: 2766
		SystemAuditObject,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04000ACF RID: 2767
		SystemAlarmObject,
		/// <summary>Allows access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type may contain optional callback data. The callback data is a resource manager-specific BLOB that is not interpreted.</summary>
		// Token: 0x04000AD0 RID: 2768
		AccessAllowedCallback,
		/// <summary>Denies access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type can contain optional callback data. The callback data is a resource manager-specific BLOB that is not interpreted.</summary>
		// Token: 0x04000AD1 RID: 2769
		AccessDeniedCallback,
		/// <summary>Allows access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type may contain optional callback data. The callback data is a resource manager-specific BLOB that is not interpreted.</summary>
		// Token: 0x04000AD2 RID: 2770
		AccessAllowedCallbackObject,
		/// <summary>Denies access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type can contain optional callback data. The callback data is a resource manager-specific BLOB that is not interpreted.</summary>
		// Token: 0x04000AD3 RID: 2771
		AccessDeniedCallbackObject,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object. The trustee is identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type can contain optional callback data. The callback data is a resource manager-specific BLOB that is not interpreted.</summary>
		// Token: 0x04000AD4 RID: 2772
		SystemAuditCallback,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04000AD5 RID: 2773
		SystemAlarmCallback,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object or subobjects such as property sets or properties. The ACE contains a set of access rights, a GUID that identifies the type of object or subobject, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee for whom the system will audit access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type can contain optional callback data. The callback data is a resource manager-specific BLOB that is not interpreted.</summary>
		// Token: 0x04000AD6 RID: 2774
		SystemAuditCallbackObject,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04000AD7 RID: 2775
		SystemAlarmCallbackObject,
		/// <summary>Tracks the maximum defined ACE type in the enumeration.</summary>
		// Token: 0x04000AD8 RID: 2776
		MaxDefinedAceType = 16
	}
}
