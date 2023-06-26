using System;

namespace System.Windows.Forms.Design
{
	/// <summary>Specifies controls that are visible in the designer.</summary>
	// Token: 0x0200048C RID: 1164
	[Flags]
	public enum ToolStripItemDesignerAvailability
	{
		/// <summary>Specifies that no controls are visible.</summary>
		// Token: 0x040033EF RID: 13295
		None = 0,
		/// <summary>Specifies that <see cref="T:System.Windows.Forms.ToolStrip" /> is visible.</summary>
		// Token: 0x040033F0 RID: 13296
		ToolStrip = 1,
		/// <summary>Specifies that <see cref="T:System.Windows.Forms.MenuStrip" /> is visible.</summary>
		// Token: 0x040033F1 RID: 13297
		MenuStrip = 2,
		/// <summary>Specifies that <see cref="T:System.Windows.Forms.ContextMenuStrip" /> is visible.</summary>
		// Token: 0x040033F2 RID: 13298
		ContextMenuStrip = 4,
		/// <summary>Specifies that <see cref="T:System.Windows.Forms.StatusStrip" /> is visible.</summary>
		// Token: 0x040033F3 RID: 13299
		StatusStrip = 8,
		/// <summary>Specifies that all controls are visible.</summary>
		// Token: 0x040033F4 RID: 13300
		All = 15
	}
}
