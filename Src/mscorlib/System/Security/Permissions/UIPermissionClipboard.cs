using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of clipboard access that is allowed to the calling code.</summary>
	// Token: 0x0200030B RID: 779
	[ComVisible(true)]
	[Serializable]
	public enum UIPermissionClipboard
	{
		/// <summary>Clipboard cannot be used.</summary>
		// Token: 0x04000F58 RID: 3928
		NoClipboard,
		/// <summary>The ability to put data on the clipboard (<see langword="Copy" />, <see langword="Cut" />) is unrestricted. Intrinsic controls that accept <see langword="Paste" />, such as text box, can accept the clipboard data, but user controls that must programmatically read the clipboard cannot.</summary>
		// Token: 0x04000F59 RID: 3929
		OwnClipboard,
		/// <summary>Clipboard can be used without restriction.</summary>
		// Token: 0x04000F5A RID: 3930
		AllClipboard
	}
}
