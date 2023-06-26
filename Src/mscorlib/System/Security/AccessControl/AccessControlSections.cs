using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies which sections of a security descriptor to save or load.</summary>
	// Token: 0x020001FC RID: 508
	[Flags]
	public enum AccessControlSections
	{
		/// <summary>No sections.</summary>
		// Token: 0x04000ABC RID: 2748
		None = 0,
		/// <summary>The system access control list (SACL).</summary>
		// Token: 0x04000ABD RID: 2749
		Audit = 1,
		/// <summary>The discretionary access control list (DACL).</summary>
		// Token: 0x04000ABE RID: 2750
		Access = 2,
		/// <summary>The owner.</summary>
		// Token: 0x04000ABF RID: 2751
		Owner = 4,
		/// <summary>The primary group.</summary>
		// Token: 0x04000AC0 RID: 2752
		Group = 8,
		/// <summary>The entire security descriptor.</summary>
		// Token: 0x04000AC1 RID: 2753
		All = 15
	}
}
