using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the action that raised a <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> event.</summary>
	// Token: 0x02000417 RID: 1047
	public enum TreeViewAction
	{
		/// <summary>The action that caused the event is unknown.</summary>
		// Token: 0x04002789 RID: 10121
		Unknown,
		/// <summary>The event was caused by a keystroke.</summary>
		// Token: 0x0400278A RID: 10122
		ByKeyboard,
		/// <summary>The event was caused by a mouse operation.</summary>
		// Token: 0x0400278B RID: 10123
		ByMouse,
		/// <summary>The event was caused by the <see cref="T:System.Windows.Forms.TreeNode" /> collapsing.</summary>
		// Token: 0x0400278C RID: 10124
		Collapse,
		/// <summary>The event was caused by the <see cref="T:System.Windows.Forms.TreeNode" /> expanding.</summary>
		// Token: 0x0400278D RID: 10125
		Expand
	}
}
