using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies that <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />, <see cref="P:System.Windows.Forms.SplitContainer.Panel2" />, or neither panel is fixed.</summary>
	// Token: 0x02000251 RID: 593
	public enum FixedPanel
	{
		/// <summary>Specifies that neither <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />, <see cref="P:System.Windows.Forms.SplitContainer.Panel2" /> is fixed. A <see cref="E:System.Windows.Forms.Control.Resize" /> event affects both panels.</summary>
		// Token: 0x04000F98 RID: 3992
		None,
		/// <summary>Specifies that <see cref="P:System.Windows.Forms.SplitContainer.Panel1" /> is fixed. A <see cref="E:System.Windows.Forms.Control.Resize" /> event affects only <see cref="P:System.Windows.Forms.SplitContainer.Panel2" />.</summary>
		// Token: 0x04000F99 RID: 3993
		Panel1,
		/// <summary>Specifies that <see cref="P:System.Windows.Forms.SplitContainer.Panel2" /> is fixed. A <see cref="E:System.Windows.Forms.Control.Resize" /> event affects only <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />.</summary>
		// Token: 0x04000F9A RID: 3994
		Panel2
	}
}
