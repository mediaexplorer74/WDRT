using System;
using System.Runtime.InteropServices;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system semaphore objects.</summary>
	// Token: 0x0200048B RID: 1163
	[Flags]
	[ComVisible(false)]
	public enum SemaphoreRights
	{
		/// <summary>The right to release a named semaphore.</summary>
		// Token: 0x04002660 RID: 9824
		Modify = 2,
		/// <summary>The right to delete a named semaphore.</summary>
		// Token: 0x04002661 RID: 9825
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named semaphore.</summary>
		// Token: 0x04002662 RID: 9826
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named semaphore.</summary>
		// Token: 0x04002663 RID: 9827
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named semaphore.</summary>
		// Token: 0x04002664 RID: 9828
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named semaphore.</summary>
		// Token: 0x04002665 RID: 9829
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named semaphore, and to modify its access rules and audit rules.</summary>
		// Token: 0x04002666 RID: 9830
		FullControl = 2031619
	}
}
