using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system mutex objects.</summary>
	// Token: 0x0200021E RID: 542
	[Flags]
	public enum MutexRights
	{
		/// <summary>The right to release a named mutex.</summary>
		// Token: 0x04000B48 RID: 2888
		Modify = 1,
		/// <summary>The right to delete a named mutex.</summary>
		// Token: 0x04000B49 RID: 2889
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named mutex.</summary>
		// Token: 0x04000B4A RID: 2890
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named mutex.</summary>
		// Token: 0x04000B4B RID: 2891
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named mutex.</summary>
		// Token: 0x04000B4C RID: 2892
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named mutex.</summary>
		// Token: 0x04000B4D RID: 2893
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named mutex, and to modify its access rules and audit rules.</summary>
		// Token: 0x04000B4E RID: 2894
		FullControl = 2031617
	}
}
