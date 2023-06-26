using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a text search is carried out in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	// Token: 0x02000347 RID: 839
	[Flags]
	public enum RichTextBoxFinds
	{
		/// <summary>Locate all instances of the search text, whether the instances found in the search are whole words or not.</summary>
		// Token: 0x04002124 RID: 8484
		None = 0,
		/// <summary>Locate only instances of the search text that are whole words.</summary>
		// Token: 0x04002125 RID: 8485
		WholeWord = 2,
		/// <summary>Locate only instances of the search text that have the exact casing.</summary>
		// Token: 0x04002126 RID: 8486
		MatchCase = 4,
		/// <summary>The search text, if found, should not be highlighted.</summary>
		// Token: 0x04002127 RID: 8487
		NoHighlight = 8,
		/// <summary>The search starts at the end of the control's document and searches to the beginning of the document.</summary>
		// Token: 0x04002128 RID: 8488
		Reverse = 16
	}
}
