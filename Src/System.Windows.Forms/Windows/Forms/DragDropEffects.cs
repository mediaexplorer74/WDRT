using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the possible effects of a drag-and-drop operation.</summary>
	// Token: 0x02000235 RID: 565
	[Flags]
	public enum DragDropEffects
	{
		/// <summary>The drop target does not accept the data.</summary>
		// Token: 0x04000F0E RID: 3854
		None = 0,
		/// <summary>The data from the drag source is copied to the drop target.</summary>
		// Token: 0x04000F0F RID: 3855
		Copy = 1,
		/// <summary>The data from the drag source is moved to the drop target.</summary>
		// Token: 0x04000F10 RID: 3856
		Move = 2,
		/// <summary>The data from the drag source is linked to the drop target.</summary>
		// Token: 0x04000F11 RID: 3857
		Link = 4,
		/// <summary>The target can be scrolled while dragging to locate a drop position that is not currently visible in the target.</summary>
		// Token: 0x04000F12 RID: 3858
		Scroll = -2147483648,
		/// <summary>The combination of the <see cref="F:System.Windows.DragDropEffects.Copy" />, <see cref="F:System.Windows.Forms.DragDropEffects.Move" />, and <see cref="F:System.Windows.Forms.DragDropEffects.Scroll" /> effects.</summary>
		// Token: 0x04000F13 RID: 3859
		All = -2147483645
	}
}
