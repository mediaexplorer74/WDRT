using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the section of a security descriptor to be queried or set.</summary>
	// Token: 0x020001FA RID: 506
	[Flags]
	public enum SecurityInfos
	{
		/// <summary>Specifies the owner identifier.</summary>
		// Token: 0x04000AA9 RID: 2729
		Owner = 1,
		/// <summary>Specifies the primary group identifier.</summary>
		// Token: 0x04000AAA RID: 2730
		Group = 2,
		/// <summary>Specifies the discretionary access control list (DACL).</summary>
		// Token: 0x04000AAB RID: 2731
		DiscretionaryAcl = 4,
		/// <summary>Specifies the system access control list (SACL).</summary>
		// Token: 0x04000AAC RID: 2732
		SystemAcl = 8
	}
}
