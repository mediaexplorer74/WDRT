using System;

namespace System.Security.AccessControl
{
	/// <summary>These flags affect the security descriptor behavior.</summary>
	// Token: 0x02000237 RID: 567
	[Flags]
	public enum ControlFlags
	{
		/// <summary>No control flags.</summary>
		// Token: 0x04000BB0 RID: 2992
		None = 0,
		/// <summary>Specifies that the owner <see cref="T:System.Security.Principal.SecurityIdentifier" /> was obtained by a defaulting mechanism. Set by resource managers only; should not be set by callers.</summary>
		// Token: 0x04000BB1 RID: 2993
		OwnerDefaulted = 1,
		/// <summary>Specifies that the group <see cref="T:System.Security.Principal.SecurityIdentifier" /> was obtained by a defaulting mechanism. Set by resource managers only; should not be set by callers.</summary>
		// Token: 0x04000BB2 RID: 2994
		GroupDefaulted = 2,
		/// <summary>Specifies that the DACL is not <see langword="null" />. Set by resource managers or users.</summary>
		// Token: 0x04000BB3 RID: 2995
		DiscretionaryAclPresent = 4,
		/// <summary>Specifies that the DACL was obtained by a defaulting mechanism. Set by resource managers only.</summary>
		// Token: 0x04000BB4 RID: 2996
		DiscretionaryAclDefaulted = 8,
		/// <summary>Specifies that the SACL is not <see langword="null" />. Set by resource managers or users.</summary>
		// Token: 0x04000BB5 RID: 2997
		SystemAclPresent = 16,
		/// <summary>Specifies that the SACL was obtained by a defaulting mechanism. Set by resource managers only.</summary>
		// Token: 0x04000BB6 RID: 2998
		SystemAclDefaulted = 32,
		/// <summary>Ignored.</summary>
		// Token: 0x04000BB7 RID: 2999
		DiscretionaryAclUntrusted = 64,
		/// <summary>Ignored.</summary>
		// Token: 0x04000BB8 RID: 3000
		ServerSecurity = 128,
		/// <summary>Ignored.</summary>
		// Token: 0x04000BB9 RID: 3001
		DiscretionaryAclAutoInheritRequired = 256,
		/// <summary>Ignored.</summary>
		// Token: 0x04000BBA RID: 3002
		SystemAclAutoInheritRequired = 512,
		/// <summary>Specifies that the Discretionary Access Control List (DACL) has been automatically inherited from the parent. Set by resource managers only.</summary>
		// Token: 0x04000BBB RID: 3003
		DiscretionaryAclAutoInherited = 1024,
		/// <summary>Specifies that the System Access Control List (SACL) has been automatically inherited from the parent. Set by resource managers only.</summary>
		// Token: 0x04000BBC RID: 3004
		SystemAclAutoInherited = 2048,
		/// <summary>Specifies that the resource manager prevents auto-inheritance. Set by resource managers or users.</summary>
		// Token: 0x04000BBD RID: 3005
		DiscretionaryAclProtected = 4096,
		/// <summary>Specifies that the resource manager prevents auto-inheritance. Set by resource managers or users.</summary>
		// Token: 0x04000BBE RID: 3006
		SystemAclProtected = 8192,
		/// <summary>Specifies that the contents of the Reserved field are valid.</summary>
		// Token: 0x04000BBF RID: 3007
		RMControlValid = 16384,
		/// <summary>Specifies that the security descriptor binary representation is in the self-relative format.  This flag is always set.</summary>
		// Token: 0x04000BC0 RID: 3008
		SelfRelative = 32768
	}
}
