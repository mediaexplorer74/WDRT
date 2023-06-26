using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the type of selection in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	// Token: 0x0200034B RID: 843
	[Flags]
	public enum RichTextBoxSelectionTypes
	{
		/// <summary>No text is selected in the current selection.</summary>
		// Token: 0x0400213E RID: 8510
		Empty = 0,
		/// <summary>The current selection contains only text.</summary>
		// Token: 0x0400213F RID: 8511
		Text = 1,
		/// <summary>At least one Object Linking and Embedding (OLE) object is selected.</summary>
		// Token: 0x04002140 RID: 8512
		Object = 2,
		/// <summary>More than one character is selected.</summary>
		// Token: 0x04002141 RID: 8513
		MultiChar = 4,
		/// <summary>More than one Object Linking and Embedding (OLE) object is selected.</summary>
		// Token: 0x04002142 RID: 8514
		MultiObject = 8
	}
}
