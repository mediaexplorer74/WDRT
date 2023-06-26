using System;

namespace System.Globalization
{
	// Token: 0x020003AC RID: 940
	[Flags]
	internal enum DateTimeFormatFlags
	{
		// Token: 0x04001379 RID: 4985
		None = 0,
		// Token: 0x0400137A RID: 4986
		UseGenitiveMonth = 1,
		// Token: 0x0400137B RID: 4987
		UseLeapYearMonth = 2,
		// Token: 0x0400137C RID: 4988
		UseSpacesInMonthNames = 4,
		// Token: 0x0400137D RID: 4989
		UseHebrewRule = 8,
		// Token: 0x0400137E RID: 4990
		UseSpacesInDayNames = 16,
		// Token: 0x0400137F RID: 4991
		UseDigitPrefixInTokens = 32,
		// Token: 0x04001380 RID: 4992
		NotInitialized = -1
	}
}
