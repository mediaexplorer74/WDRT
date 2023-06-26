using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies whether a permission should have all or no access to resources at creation.</summary>
	// Token: 0x020002EB RID: 747
	[ComVisible(true)]
	[Serializable]
	public enum PermissionState
	{
		/// <summary>Full access to the resource protected by the permission.</summary>
		// Token: 0x04000EE2 RID: 3810
		Unrestricted = 1,
		/// <summary>No access to the resource protected by the permission.</summary>
		// Token: 0x04000EE3 RID: 3811
		None = 0
	}
}
