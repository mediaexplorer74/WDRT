using System;

namespace System.Security.AccessControl
{
	/// <summary>Inheritance flags specify the semantics of inheritance for access control entries (ACEs).</summary>
	// Token: 0x020001F7 RID: 503
	[Flags]
	public enum InheritanceFlags
	{
		/// <summary>The ACE is not inherited by child objects.</summary>
		// Token: 0x04000A9D RID: 2717
		None = 0,
		/// <summary>The ACE is inherited by child container objects.</summary>
		// Token: 0x04000A9E RID: 2718
		ContainerInherit = 1,
		/// <summary>The ACE is inherited by child leaf objects.</summary>
		// Token: 0x04000A9F RID: 2719
		ObjectInherit = 2
	}
}
