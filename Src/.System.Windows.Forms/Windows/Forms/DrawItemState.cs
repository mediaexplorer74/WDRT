using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the state of an item that is being drawn.</summary>
	// Token: 0x0200023A RID: 570
	[Flags]
	public enum DrawItemState
	{
		/// <summary>The item is checked. Only menu controls use this value.</summary>
		// Token: 0x04000F22 RID: 3874
		Checked = 8,
		/// <summary>The item is the editing portion of a <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		// Token: 0x04000F23 RID: 3875
		ComboBoxEdit = 4096,
		/// <summary>The item is in its default visual state.</summary>
		// Token: 0x04000F24 RID: 3876
		Default = 32,
		/// <summary>The item is unavailable.</summary>
		// Token: 0x04000F25 RID: 3877
		Disabled = 4,
		/// <summary>The item has focus.</summary>
		// Token: 0x04000F26 RID: 3878
		Focus = 16,
		/// <summary>The item is grayed. Only menu controls use this value.</summary>
		// Token: 0x04000F27 RID: 3879
		Grayed = 2,
		/// <summary>The item is being hot-tracked, that is, the item is highlighted as the mouse pointer passes over it.</summary>
		// Token: 0x04000F28 RID: 3880
		HotLight = 64,
		/// <summary>The item is inactive.</summary>
		// Token: 0x04000F29 RID: 3881
		Inactive = 128,
		/// <summary>The item displays without a keyboard accelerator.</summary>
		// Token: 0x04000F2A RID: 3882
		NoAccelerator = 256,
		/// <summary>The item displays without the visual cue that indicates it has focus.</summary>
		// Token: 0x04000F2B RID: 3883
		NoFocusRect = 512,
		/// <summary>The item is selected.</summary>
		// Token: 0x04000F2C RID: 3884
		Selected = 1,
		/// <summary>The item currently has no state.</summary>
		// Token: 0x04000F2D RID: 3885
		None = 0
	}
}
