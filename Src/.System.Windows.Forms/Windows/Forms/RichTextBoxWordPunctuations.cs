using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the types of punctuation tables that can be used with the <see cref="T:System.Windows.Forms.RichTextBox" /> control's word-wrapping and word-breaking features.</summary>
	// Token: 0x0200034D RID: 845
	public enum RichTextBoxWordPunctuations
	{
		/// <summary>Use pre-defined Level 1 punctuation table as default.</summary>
		// Token: 0x0400214A RID: 8522
		Level1 = 128,
		/// <summary>Use pre-defined Level 2 punctuation table as default.</summary>
		// Token: 0x0400214B RID: 8523
		Level2 = 256,
		/// <summary>Use a custom defined punctuation table.</summary>
		// Token: 0x0400214C RID: 8524
		Custom = 512,
		/// <summary>Used as a mask.</summary>
		// Token: 0x0400214D RID: 8525
		All = 896
	}
}
