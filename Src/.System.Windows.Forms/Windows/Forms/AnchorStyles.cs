using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a control anchors to the edges of its container.</summary>
	// Token: 0x0200011E RID: 286
	[Editor("System.Windows.Forms.Design.AnchorEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Flags]
	public enum AnchorStyles
	{
		/// <summary>The control is anchored to the top edge of its container.</summary>
		// Token: 0x040005C2 RID: 1474
		Top = 1,
		/// <summary>The control is anchored to the bottom edge of its container.</summary>
		// Token: 0x040005C3 RID: 1475
		Bottom = 2,
		/// <summary>The control is anchored to the left edge of its container.</summary>
		// Token: 0x040005C4 RID: 1476
		Left = 4,
		/// <summary>The control is anchored to the right edge of its container.</summary>
		// Token: 0x040005C5 RID: 1477
		Right = 8,
		/// <summary>The control is not anchored to any edges of its container.</summary>
		// Token: 0x040005C6 RID: 1478
		None = 0
	}
}
