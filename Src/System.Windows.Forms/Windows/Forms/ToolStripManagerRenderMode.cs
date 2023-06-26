using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Specifies the painting style applied to multiple <see cref="T:System.Windows.Forms.ToolStrip" /> objects contained in a form.</summary>
	// Token: 0x020003E4 RID: 996
	public enum ToolStripManagerRenderMode
	{
		/// <summary>Indicates the use of a <see cref="T:System.Windows.Forms.ToolStripRenderer" /> other than <see cref="T:System.Windows.Forms.ToolStripProfessionalRenderer" /> or <see cref="T:System.Windows.Forms.ToolStripSystemRenderer" />.</summary>
		// Token: 0x040025F0 RID: 9712
		[Browsable(false)]
		Custom,
		/// <summary>Indicates the use of a <see cref="T:System.Windows.Forms.ToolStripSystemRenderer" /> to paint.</summary>
		// Token: 0x040025F1 RID: 9713
		System,
		/// <summary>Indicates the use of a <see cref="T:System.Windows.Forms.ToolStripProfessionalRenderer" /> to paint.</summary>
		// Token: 0x040025F2 RID: 9714
		Professional
	}
}
