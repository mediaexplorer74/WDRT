using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Specifies the position and manner in which a control is docked.</summary>
	// Token: 0x02000230 RID: 560
	[Editor("System.Windows.Forms.Design.DockEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public enum DockStyle
	{
		/// <summary>The control is not docked.</summary>
		// Token: 0x04000EF7 RID: 3831
		None,
		/// <summary>The control's top edge is docked to the top of its containing control.</summary>
		// Token: 0x04000EF8 RID: 3832
		Top,
		/// <summary>The control's bottom edge is docked to the bottom of its containing control.</summary>
		// Token: 0x04000EF9 RID: 3833
		Bottom,
		/// <summary>The control's left edge is docked to the left edge of its containing control.</summary>
		// Token: 0x04000EFA RID: 3834
		Left,
		/// <summary>The control's right edge is docked to the right edge of its containing control.</summary>
		// Token: 0x04000EFB RID: 3835
		Right,
		/// <summary>All the control's edges are docked to the all edges of its containing control and sized appropriately.</summary>
		// Token: 0x04000EFC RID: 3836
		Fill
	}
}
