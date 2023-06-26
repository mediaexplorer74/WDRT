using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines the Unicode category of a character.</summary>
	// Token: 0x020003D9 RID: 985
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum UnicodeCategory
	{
		/// <summary>Uppercase letter. Signified by the Unicode designation "Lu" (letter, uppercase). The value is 0.</summary>
		// Token: 0x0400158C RID: 5516
		[__DynamicallyInvokable]
		UppercaseLetter,
		/// <summary>Lowercase letter. Signified by the Unicode designation "Ll" (letter, lowercase). The value is 1.</summary>
		// Token: 0x0400158D RID: 5517
		[__DynamicallyInvokable]
		LowercaseLetter,
		/// <summary>Titlecase letter. Signified by the Unicode designation "Lt" (letter, titlecase). The value is 2.</summary>
		// Token: 0x0400158E RID: 5518
		[__DynamicallyInvokable]
		TitlecaseLetter,
		/// <summary>Modifier letter character, which is free-standing spacing character that indicates modifications of a preceding letter. Signified by the Unicode designation "Lm" (letter, modifier). The value is 3.</summary>
		// Token: 0x0400158F RID: 5519
		[__DynamicallyInvokable]
		ModifierLetter,
		/// <summary>Letter that is not an uppercase letter, a lowercase letter, a titlecase letter, or a modifier letter. Signified by the Unicode designation "Lo" (letter, other). The value is 4.</summary>
		// Token: 0x04001590 RID: 5520
		[__DynamicallyInvokable]
		OtherLetter,
		/// <summary>Nonspacing character that indicates modifications of a base character. Signified by the Unicode designation "Mn" (mark, nonspacing). The value is 5.</summary>
		// Token: 0x04001591 RID: 5521
		[__DynamicallyInvokable]
		NonSpacingMark,
		/// <summary>Spacing character that indicates modifications of a base character and affects the width of the glyph for that base character. Signified by the Unicode designation "Mc" (mark, spacing combining). The value is 6.</summary>
		// Token: 0x04001592 RID: 5522
		[__DynamicallyInvokable]
		SpacingCombiningMark,
		/// <summary>Enclosing mark character, which is a nonspacing combining character that surrounds all previous characters up to and including a base character. Signified by the Unicode designation "Me" (mark, enclosing). The value is 7.</summary>
		// Token: 0x04001593 RID: 5523
		[__DynamicallyInvokable]
		EnclosingMark,
		/// <summary>Decimal digit character, that is, a character in the range 0 through 9. Signified by the Unicode designation "Nd" (number, decimal digit). The value is 8.</summary>
		// Token: 0x04001594 RID: 5524
		[__DynamicallyInvokable]
		DecimalDigitNumber,
		/// <summary>Number represented by a letter, instead of a decimal digit, for example, the Roman numeral for five, which is "V". The indicator is signified by the Unicode designation "Nl" (number, letter). The value is 9.</summary>
		// Token: 0x04001595 RID: 5525
		[__DynamicallyInvokable]
		LetterNumber,
		/// <summary>Number that is neither a decimal digit nor a letter number, for example, the fraction 1/2. The indicator is signified by the Unicode designation "No" (number, other). The value is 10.</summary>
		// Token: 0x04001596 RID: 5526
		[__DynamicallyInvokable]
		OtherNumber,
		/// <summary>Space character, which has no glyph but is not a control or format character. Signified by the Unicode designation "Zs" (separator, space). The value is 11.</summary>
		// Token: 0x04001597 RID: 5527
		[__DynamicallyInvokable]
		SpaceSeparator,
		/// <summary>Character that is used to separate lines of text. Signified by the Unicode designation "Zl" (separator, line). The value is 12.</summary>
		// Token: 0x04001598 RID: 5528
		[__DynamicallyInvokable]
		LineSeparator,
		/// <summary>Character used to separate paragraphs. Signified by the Unicode designation "Zp" (separator, paragraph). The value is 13.</summary>
		// Token: 0x04001599 RID: 5529
		[__DynamicallyInvokable]
		ParagraphSeparator,
		/// <summary>Control code character, with a Unicode value of U+007F or in the range U+0000 through U+001F or U+0080 through U+009F. Signified by the Unicode designation "Cc" (other, control). The value is 14.</summary>
		// Token: 0x0400159A RID: 5530
		[__DynamicallyInvokable]
		Control,
		/// <summary>Format character that affects the layout of text or the operation of text processes, but is not normally rendered. Signified by the Unicode designation "Cf" (other, format). The value is 15.</summary>
		// Token: 0x0400159B RID: 5531
		[__DynamicallyInvokable]
		Format,
		/// <summary>High surrogate or a low surrogate character. Surrogate code values are in the range U+D800 through U+DFFF. Signified by the Unicode designation "Cs" (other, surrogate). The value is 16.</summary>
		// Token: 0x0400159C RID: 5532
		[__DynamicallyInvokable]
		Surrogate,
		/// <summary>Private-use character, with a Unicode value in the range U+E000 through U+F8FF. Signified by the Unicode designation "Co" (other, private use). The value is 17.</summary>
		// Token: 0x0400159D RID: 5533
		[__DynamicallyInvokable]
		PrivateUse,
		/// <summary>Connector punctuation character that connects two characters. Signified by the Unicode designation "Pc" (punctuation, connector). The value is 18.</summary>
		// Token: 0x0400159E RID: 5534
		[__DynamicallyInvokable]
		ConnectorPunctuation,
		/// <summary>Dash or hyphen character. Signified by the Unicode designation "Pd" (punctuation, dash). The value is 19.</summary>
		// Token: 0x0400159F RID: 5535
		[__DynamicallyInvokable]
		DashPunctuation,
		/// <summary>Opening character of one of the paired punctuation marks, such as parentheses, square brackets, and braces. Signified by the Unicode designation "Ps" (punctuation, open). The value is 20.</summary>
		// Token: 0x040015A0 RID: 5536
		[__DynamicallyInvokable]
		OpenPunctuation,
		/// <summary>Closing character of one of the paired punctuation marks, such as parentheses, square brackets, and braces. Signified by the Unicode designation "Pe" (punctuation, close). The value is 21.</summary>
		// Token: 0x040015A1 RID: 5537
		[__DynamicallyInvokable]
		ClosePunctuation,
		/// <summary>Opening or initial quotation mark character. Signified by the Unicode designation "Pi" (punctuation, initial quote). The value is 22.</summary>
		// Token: 0x040015A2 RID: 5538
		[__DynamicallyInvokable]
		InitialQuotePunctuation,
		/// <summary>Closing or final quotation mark character. Signified by the Unicode designation "Pf" (punctuation, final quote). The value is 23.</summary>
		// Token: 0x040015A3 RID: 5539
		[__DynamicallyInvokable]
		FinalQuotePunctuation,
		/// <summary>Punctuation character that is not a connector, a dash, open punctuation, close punctuation, an initial quote, or a final quote. Signified by the Unicode designation "Po" (punctuation, other). The value is 24.</summary>
		// Token: 0x040015A4 RID: 5540
		[__DynamicallyInvokable]
		OtherPunctuation,
		/// <summary>Mathematical symbol character, such as "+" or "= ". Signified by the Unicode designation "Sm" (symbol, math). The value is 25.</summary>
		// Token: 0x040015A5 RID: 5541
		[__DynamicallyInvokable]
		MathSymbol,
		/// <summary>Currency symbol character. Signified by the Unicode designation "Sc" (symbol, currency). The value is 26.</summary>
		// Token: 0x040015A6 RID: 5542
		[__DynamicallyInvokable]
		CurrencySymbol,
		/// <summary>Modifier symbol character, which indicates modifications of surrounding characters. For example, the fraction slash indicates that the number to the left is the numerator and the number to the right is the denominator. The indicator is signified by the Unicode designation "Sk" (symbol, modifier). The value is 27.</summary>
		// Token: 0x040015A7 RID: 5543
		[__DynamicallyInvokable]
		ModifierSymbol,
		/// <summary>Symbol character that is not a mathematical symbol, a currency symbol or a modifier symbol. Signified by the Unicode designation "So" (symbol, other). The value is 28.</summary>
		// Token: 0x040015A8 RID: 5544
		[__DynamicallyInvokable]
		OtherSymbol,
		/// <summary>Character that is not assigned to any Unicode category. Signified by the Unicode designation "Cn" (other, not assigned). The value is 29.</summary>
		// Token: 0x040015A9 RID: 5545
		[__DynamicallyInvokable]
		OtherNotAssigned
	}
}
