﻿using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies which sides of a <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> have borders.</summary>
	// Token: 0x02000405 RID: 1029
	[ComVisible(true)]
	[Editor("System.Windows.Forms.Design.BorderSidesEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Flags]
	public enum ToolStripStatusLabelBorderSides
	{
		/// <summary>All sides of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> have borders.</summary>
		// Token: 0x040026D1 RID: 9937
		All = 15,
		/// <summary>Only the bottom side of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> has borders.</summary>
		// Token: 0x040026D2 RID: 9938
		Bottom = 8,
		/// <summary>Only the left side of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> has borders.</summary>
		// Token: 0x040026D3 RID: 9939
		Left = 1,
		/// <summary>Only the right side of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> has borders.</summary>
		// Token: 0x040026D4 RID: 9940
		Right = 4,
		/// <summary>Only the top side of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> has borders.</summary>
		// Token: 0x040026D5 RID: 9941
		Top = 2,
		/// <summary>The <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> has no borders.</summary>
		// Token: 0x040026D6 RID: 9942
		None = 0
	}
}
