using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that represent the possible states of a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	// Token: 0x020002E0 RID: 736
	[Flags]
	public enum ListViewItemStates
	{
		/// <summary>The item is checked.</summary>
		// Token: 0x04001336 RID: 4918
		Checked = 8,
		/// <summary>The item is in its default state.</summary>
		// Token: 0x04001337 RID: 4919
		Default = 32,
		/// <summary>The item has focus.</summary>
		// Token: 0x04001338 RID: 4920
		Focused = 16,
		/// <summary>The item is disabled.</summary>
		// Token: 0x04001339 RID: 4921
		Grayed = 2,
		/// <summary>The item is currently under the mouse pointer.</summary>
		// Token: 0x0400133A RID: 4922
		Hot = 64,
		/// <summary>The item is in an indeterminate state.</summary>
		// Token: 0x0400133B RID: 4923
		Indeterminate = 256,
		/// <summary>The item is marked.</summary>
		// Token: 0x0400133C RID: 4924
		Marked = 128,
		/// <summary>The item is selected.</summary>
		// Token: 0x0400133D RID: 4925
		Selected = 1,
		/// <summary>The item should indicate a keyboard shortcut.</summary>
		// Token: 0x0400133E RID: 4926
		ShowKeyboardCues = 512
	}
}
