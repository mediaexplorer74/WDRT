using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the bounds of the control to use when defining a control's size and position.</summary>
	// Token: 0x02000140 RID: 320
	[Flags]
	public enum BoundsSpecified
	{
		/// <summary>The left edge of the control is defined.</summary>
		// Token: 0x04000717 RID: 1815
		X = 1,
		/// <summary>The top edge of the control is defined.</summary>
		// Token: 0x04000718 RID: 1816
		Y = 2,
		/// <summary>The width of the control is defined.</summary>
		// Token: 0x04000719 RID: 1817
		Width = 4,
		/// <summary>The height of the control is defined.</summary>
		// Token: 0x0400071A RID: 1818
		Height = 8,
		/// <summary>Both <see langword="X" /> and <see langword="Y" /> coordinates of the control are defined.</summary>
		// Token: 0x0400071B RID: 1819
		Location = 3,
		/// <summary>Both <see cref="P:System.Windows.Forms.Control.Width" /> and <see cref="P:System.Windows.Forms.Control.Height" /> property values of the control are defined.</summary>
		// Token: 0x0400071C RID: 1820
		Size = 12,
		/// <summary>Both <see cref="P:System.Windows.Forms.Control.Location" /> and <see cref="P:System.Windows.Forms.Control.Size" /> property values are defined.</summary>
		// Token: 0x0400071D RID: 1821
		All = 15,
		/// <summary>No bounds are specified.</summary>
		// Token: 0x0400071E RID: 1822
		None = 0
	}
}
