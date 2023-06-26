using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies how Access Control Entries (ACEs) are propagated to child objects.  These flags are significant only if inheritance flags are present.</summary>
	// Token: 0x020001F8 RID: 504
	[Flags]
	public enum PropagationFlags
	{
		/// <summary>Specifies that no inheritance flags are set.</summary>
		// Token: 0x04000AA1 RID: 2721
		None = 0,
		/// <summary>Specifies that the ACE is not propagated to child objects.</summary>
		// Token: 0x04000AA2 RID: 2722
		NoPropagateInherit = 1,
		/// <summary>Specifies that the ACE is propagated only to child objects. This includes both container and leaf child objects.</summary>
		// Token: 0x04000AA3 RID: 2723
		InheritOnly = 2
	}
}
