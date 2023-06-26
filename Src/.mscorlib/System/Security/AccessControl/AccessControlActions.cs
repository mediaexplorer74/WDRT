using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the actions that are permitted for securable objects.</summary>
	// Token: 0x020001FD RID: 509
	[Flags]
	public enum AccessControlActions
	{
		/// <summary>Specifies no access.</summary>
		// Token: 0x04000AC3 RID: 2755
		None = 0,
		/// <summary>Specifies read-only access.</summary>
		// Token: 0x04000AC4 RID: 2756
		View = 1,
		/// <summary>Specifies write-only access.</summary>
		// Token: 0x04000AC5 RID: 2757
		Change = 2
	}
}
