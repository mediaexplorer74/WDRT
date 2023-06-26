using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the inheritance and auditing behavior of an access control entry (ACE).</summary>
	// Token: 0x020001FF RID: 511
	[Flags]
	public enum AceFlags : byte
	{
		/// <summary>No ACE flags are set.</summary>
		// Token: 0x04000ADA RID: 2778
		None = 0,
		/// <summary>The access mask is propagated onto child leaf objects.</summary>
		// Token: 0x04000ADB RID: 2779
		ObjectInherit = 1,
		/// <summary>The access mask is propagated to child container objects.</summary>
		// Token: 0x04000ADC RID: 2780
		ContainerInherit = 2,
		/// <summary>The access checks do not apply to the object; they only apply to its children.</summary>
		// Token: 0x04000ADD RID: 2781
		NoPropagateInherit = 4,
		/// <summary>The access mask is propagated only to child objects. This includes both container and leaf child objects.</summary>
		// Token: 0x04000ADE RID: 2782
		InheritOnly = 8,
		/// <summary>An ACE is inherited from a parent container rather than being explicitly set for an object.</summary>
		// Token: 0x04000ADF RID: 2783
		Inherited = 16,
		/// <summary>Successful access attempts are audited.</summary>
		// Token: 0x04000AE0 RID: 2784
		SuccessfulAccess = 64,
		/// <summary>Failed access attempts are audited.</summary>
		// Token: 0x04000AE1 RID: 2785
		FailedAccess = 128,
		/// <summary>A logical <see langword="OR" /> of <see cref="F:System.Security.AccessControl.AceFlags.ObjectInherit" />, <see cref="F:System.Security.AccessControl.AceFlags.ContainerInherit" />, <see cref="F:System.Security.AccessControl.AceFlags.NoPropagateInherit" />, and <see cref="F:System.Security.AccessControl.AceFlags.InheritOnly" />.</summary>
		// Token: 0x04000AE2 RID: 2786
		InheritanceFlags = 15,
		/// <summary>All access attempts are audited.</summary>
		// Token: 0x04000AE3 RID: 2787
		AuditFlags = 192
	}
}
