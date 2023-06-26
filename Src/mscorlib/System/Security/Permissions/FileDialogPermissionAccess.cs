using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of access to files allowed through the File dialog boxes.</summary>
	// Token: 0x020002DD RID: 733
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FileDialogPermissionAccess
	{
		/// <summary>No access to files through the File dialog boxes.</summary>
		// Token: 0x04000E7F RID: 3711
		None = 0,
		/// <summary>Ability to open files through the File dialog boxes.</summary>
		// Token: 0x04000E80 RID: 3712
		Open = 1,
		/// <summary>Ability to save files through the File dialog boxes.</summary>
		// Token: 0x04000E81 RID: 3713
		Save = 2,
		/// <summary>Ability to open and save files through the File dialog boxes.</summary>
		// Token: 0x04000E82 RID: 3714
		OpenSave = 3
	}
}
