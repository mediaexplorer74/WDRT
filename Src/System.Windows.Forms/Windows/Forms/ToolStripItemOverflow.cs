using System;

namespace System.Windows.Forms
{
	/// <summary>Determines whether a <see cref="T:System.Windows.Forms.ToolStripItem" /> is placed in the overflow <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	// Token: 0x020003D7 RID: 983
	public enum ToolStripItemOverflow
	{
		/// <summary>Specifies that a <see cref="T:System.Windows.Forms.ToolStripItem" /> is never a candidate for the overflow <see cref="T:System.Windows.Forms.ToolStrip" />. If the <see cref="T:System.Windows.Forms.ToolStripItem" /> cannot fit on the main <see cref="T:System.Windows.Forms.ToolStrip" />, it will not be shown.</summary>
		// Token: 0x040025B0 RID: 9648
		Never,
		/// <summary>Specifies that a <see cref="T:System.Windows.Forms.ToolStripItem" /> is permanently shown in the overflow <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x040025B1 RID: 9649
		Always,
		/// <summary>Specifies that a <see cref="T:System.Windows.Forms.ToolStripItem" /> drifts between the main <see cref="T:System.Windows.Forms.ToolStrip" /> and the overflow <see cref="T:System.Windows.Forms.ToolStrip" /> as required if space is not available on the main <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x040025B2 RID: 9650
		AsNeeded
	}
}
