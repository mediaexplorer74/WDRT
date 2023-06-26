using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system event objects.</summary>
	// Token: 0x02000214 RID: 532
	[Flags]
	public enum EventWaitHandleRights
	{
		/// <summary>The right to set or reset the signaled state of a named event.</summary>
		// Token: 0x04000B27 RID: 2855
		Modify = 2,
		/// <summary>The right to delete a named event.</summary>
		// Token: 0x04000B28 RID: 2856
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named event.</summary>
		// Token: 0x04000B29 RID: 2857
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named event.</summary>
		// Token: 0x04000B2A RID: 2858
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named event.</summary>
		// Token: 0x04000B2B RID: 2859
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named event.</summary>
		// Token: 0x04000B2C RID: 2860
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named event, and to modify its access rules and audit rules.</summary>
		// Token: 0x04000B2D RID: 2861
		FullControl = 2031619
	}
}
