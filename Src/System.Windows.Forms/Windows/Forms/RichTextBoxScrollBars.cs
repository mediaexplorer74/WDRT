using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the type of scroll bars to display in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	// Token: 0x02000349 RID: 841
	public enum RichTextBoxScrollBars
	{
		/// <summary>No scroll bars are displayed.</summary>
		// Token: 0x04002132 RID: 8498
		None,
		/// <summary>Display a horizontal scroll bar only when text is longer than the width of the control.</summary>
		// Token: 0x04002133 RID: 8499
		Horizontal,
		/// <summary>Display a vertical scroll bar only when text is longer than the height of the control.</summary>
		// Token: 0x04002134 RID: 8500
		Vertical,
		/// <summary>Display both a horizontal and a vertical scroll bar when needed.</summary>
		// Token: 0x04002135 RID: 8501
		Both,
		/// <summary>Always display a horizontal scroll bar.</summary>
		// Token: 0x04002136 RID: 8502
		ForcedHorizontal = 17,
		/// <summary>Always display a vertical scroll bar.</summary>
		// Token: 0x04002137 RID: 8503
		ForcedVertical,
		/// <summary>Always display both a horizontal and a vertical scroll bar.</summary>
		// Token: 0x04002138 RID: 8504
		ForcedBoth
	}
}
