using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying the parts of a <see cref="T:System.Windows.Forms.DataGridViewCell" /> that are to be painted.</summary>
	// Token: 0x02000205 RID: 517
	[Flags]
	public enum DataGridViewPaintParts
	{
		/// <summary>Nothing should be painted.</summary>
		// Token: 0x04000E0C RID: 3596
		None = 0,
		/// <summary>All parts of the cell should be painted.</summary>
		// Token: 0x04000E0D RID: 3597
		All = 127,
		/// <summary>The background of the cell should be painted.</summary>
		// Token: 0x04000E0E RID: 3598
		Background = 1,
		/// <summary>The border of the cell should be painted.</summary>
		// Token: 0x04000E0F RID: 3599
		Border = 2,
		/// <summary>The background of the cell content should be painted.</summary>
		// Token: 0x04000E10 RID: 3600
		ContentBackground = 4,
		/// <summary>The foreground of the cell content should be painted.</summary>
		// Token: 0x04000E11 RID: 3601
		ContentForeground = 8,
		/// <summary>The cell error icon should be painted.</summary>
		// Token: 0x04000E12 RID: 3602
		ErrorIcon = 16,
		/// <summary>The focus rectangle should be painted around the cell.</summary>
		// Token: 0x04000E13 RID: 3603
		Focus = 32,
		/// <summary>The background of the cell should be painted when the cell is selected.</summary>
		// Token: 0x04000E14 RID: 3604
		SelectionBackground = 64
	}
}
