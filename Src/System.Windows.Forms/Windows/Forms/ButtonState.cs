using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the appearance of a button.</summary>
	// Token: 0x02000145 RID: 325
	[Flags]
	public enum ButtonState
	{
		/// <summary>The button has a checked or latched appearance. Use this appearance to show that a toggle button has been pressed.</summary>
		// Token: 0x04000744 RID: 1860
		Checked = 1024,
		/// <summary>The button has a flat, two-dimensional appearance.</summary>
		// Token: 0x04000745 RID: 1861
		Flat = 16384,
		/// <summary>The button is inactive (grayed).</summary>
		// Token: 0x04000746 RID: 1862
		Inactive = 256,
		/// <summary>The button has its normal appearance (three-dimensional).</summary>
		// Token: 0x04000747 RID: 1863
		Normal = 0,
		/// <summary>The button appears pressed.</summary>
		// Token: 0x04000748 RID: 1864
		Pushed = 512,
		/// <summary>All flags except <see langword="Normal" /> are set.</summary>
		// Token: 0x04000749 RID: 1865
		All = 18176
	}
}
