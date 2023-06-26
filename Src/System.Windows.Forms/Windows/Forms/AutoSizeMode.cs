using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a control will behave when its <see cref="P:System.Windows.Forms.Control.AutoSize" /> property is enabled.</summary>
	// Token: 0x0200012B RID: 299
	public enum AutoSizeMode
	{
		/// <summary>The control grows or shrinks to fit its contents. The control cannot be resized manually.</summary>
		// Token: 0x0400061B RID: 1563
		GrowAndShrink,
		/// <summary>The control grows as much as necessary to fit its contents but does not shrink smaller than the value of its <see cref="P:System.Windows.Forms.Control.Size" /> property. The form can be resized, but cannot be made so small that any of its contained controls are hidden.</summary>
		// Token: 0x0400061C RID: 1564
		GrowOnly
	}
}
