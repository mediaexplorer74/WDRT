using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the formats used with text-related methods of the <see cref="T:System.Windows.Forms.Clipboard" /> and <see cref="T:System.Windows.Forms.DataObject" /> classes.</summary>
	// Token: 0x020003A2 RID: 930
	public enum TextDataFormat
	{
		/// <summary>Specifies the standard ANSI text format.</summary>
		// Token: 0x040023AF RID: 9135
		Text,
		/// <summary>Specifies the standard Windows Unicode text format.</summary>
		// Token: 0x040023B0 RID: 9136
		UnicodeText,
		/// <summary>Specifies text consisting of rich text format (RTF) data.</summary>
		// Token: 0x040023B1 RID: 9137
		Rtf,
		/// <summary>Specifies text consisting of HTML data.</summary>
		// Token: 0x040023B2 RID: 9138
		Html,
		/// <summary>Specifies a comma-separated value (CSV) format, which is a common interchange format used by spreadsheets.</summary>
		// Token: 0x040023B3 RID: 9139
		CommaSeparatedValue
	}
}
