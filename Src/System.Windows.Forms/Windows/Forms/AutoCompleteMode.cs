using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the mode for the automatic completion feature used in the <see cref="T:System.Windows.Forms.ComboBox" /> and <see cref="T:System.Windows.Forms.TextBox" /> controls.</summary>
	// Token: 0x02000126 RID: 294
	public enum AutoCompleteMode
	{
		/// <summary>Disables the automatic completion feature for the <see cref="T:System.Windows.Forms.ComboBox" /> and <see cref="T:System.Windows.Forms.TextBox" /> controls.</summary>
		// Token: 0x04000600 RID: 1536
		None,
		/// <summary>Displays the auxiliary drop-down list associated with the edit control. This drop-down is populated with one or more suggested completion strings.</summary>
		// Token: 0x04000601 RID: 1537
		Suggest,
		/// <summary>Appends the remainder of the most likely candidate string to the existing characters, highlighting the appended characters.</summary>
		// Token: 0x04000602 RID: 1538
		Append,
		/// <summary>Applies both <see langword="Suggest" /> and <see langword="Append" /> options.</summary>
		// Token: 0x04000603 RID: 1539
		SuggestAppend
	}
}
