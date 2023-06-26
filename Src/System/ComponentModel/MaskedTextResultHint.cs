using System;

namespace System.ComponentModel
{
	/// <summary>Specifies values that succinctly describe the results of a masked text parsing operation.</summary>
	// Token: 0x0200058E RID: 1422
	public enum MaskedTextResultHint
	{
		/// <summary>Unknown. The result of the operation could not be determined.</summary>
		// Token: 0x040029FE RID: 10750
		Unknown,
		/// <summary>Success. The operation succeeded because a literal, prompt or space character was an escaped character. For more information about escaped characters, see the <see cref="M:System.ComponentModel.MaskedTextProvider.VerifyEscapeChar(System.Char,System.Int32)" /> method.</summary>
		// Token: 0x040029FF RID: 10751
		CharacterEscaped,
		/// <summary>Success. The primary operation was not performed because it was not needed; therefore, no side effect was produced.</summary>
		// Token: 0x04002A00 RID: 10752
		NoEffect,
		/// <summary>Success. The primary operation was not performed because it was not needed, but the method produced a side effect. For example, the <see cref="Overload:System.ComponentModel.MaskedTextProvider.RemoveAt" /> method can delete an unassigned edit position, which causes left-shifting of subsequent characters in the formatted string.</summary>
		// Token: 0x04002A01 RID: 10753
		SideEffect,
		/// <summary>Success. The primary operation succeeded.</summary>
		// Token: 0x04002A02 RID: 10754
		Success,
		/// <summary>Operation did not succeed.An input character was encountered that was not a member of the ASCII character set.</summary>
		// Token: 0x04002A03 RID: 10755
		AsciiCharacterExpected = -1,
		/// <summary>Operation did not succeed.An input character was encountered that was not alphanumeric. .</summary>
		// Token: 0x04002A04 RID: 10756
		AlphanumericCharacterExpected = -2,
		/// <summary>Operation did not succeed. An input character was encountered that was not a digit.</summary>
		// Token: 0x04002A05 RID: 10757
		DigitExpected = -3,
		/// <summary>Operation did not succeed. An input character was encountered that was not a letter.</summary>
		// Token: 0x04002A06 RID: 10758
		LetterExpected = -4,
		/// <summary>Operation did not succeed. An input character was encountered that was not a signed digit.</summary>
		// Token: 0x04002A07 RID: 10759
		SignedDigitExpected = -5,
		/// <summary>Operation did not succeed. The program encountered an  input character that was not valid. For more information about characters that are not valid, see the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidInputChar(System.Char)" /> method.</summary>
		// Token: 0x04002A08 RID: 10760
		InvalidInput = -51,
		/// <summary>Operation did not succeed. The prompt character is not valid at input, perhaps because the <see cref="P:System.ComponentModel.MaskedTextProvider.AllowPromptAsInput" /> property is set to <see langword="false" />.</summary>
		// Token: 0x04002A09 RID: 10761
		PromptCharNotAllowed = -52,
		/// <summary>Operation did not succeed. There were not enough edit positions available to fulfill the request.</summary>
		// Token: 0x04002A0A RID: 10762
		UnavailableEditPosition = -53,
		/// <summary>Operation did not succeed. The current position in the formatted string is a literal character.</summary>
		// Token: 0x04002A0B RID: 10763
		NonEditPosition = -54,
		/// <summary>Operation did not succeed. The specified position is not in the range of the target string; typically it is either less than zero or greater then the length of the target string.</summary>
		// Token: 0x04002A0C RID: 10764
		PositionOutOfRange = -55
	}
}
