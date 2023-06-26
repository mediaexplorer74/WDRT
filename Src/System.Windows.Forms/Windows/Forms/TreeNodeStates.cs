using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that represent the possible states of a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	// Token: 0x02000415 RID: 1045
	[Flags]
	public enum TreeNodeStates
	{
		/// <summary>The node is checked.</summary>
		// Token: 0x0400273E RID: 10046
		Checked = 8,
		/// <summary>The node is in its default state.</summary>
		// Token: 0x0400273F RID: 10047
		Default = 32,
		/// <summary>The node has focus.</summary>
		// Token: 0x04002740 RID: 10048
		Focused = 16,
		/// <summary>The node is disabled.</summary>
		// Token: 0x04002741 RID: 10049
		Grayed = 2,
		/// <summary>The node is hot. This state occurs when the <see cref="P:System.Windows.Forms.TreeView.HotTracking" /> property is set to <see langword="true" /> and the mouse pointer is over the node.</summary>
		// Token: 0x04002742 RID: 10050
		Hot = 64,
		/// <summary>The node in an indeterminate state.</summary>
		// Token: 0x04002743 RID: 10051
		Indeterminate = 256,
		/// <summary>The node is marked.</summary>
		// Token: 0x04002744 RID: 10052
		Marked = 128,
		/// <summary>The node is selected.</summary>
		// Token: 0x04002745 RID: 10053
		Selected = 1,
		/// <summary>The node should indicate a keyboard shortcut.</summary>
		// Token: 0x04002746 RID: 10054
		ShowKeyboardCues = 512
	}
}
