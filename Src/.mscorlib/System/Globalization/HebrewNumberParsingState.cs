using System;

namespace System.Globalization
{
	// Token: 0x020003DC RID: 988
	internal enum HebrewNumberParsingState
	{
		// Token: 0x04001696 RID: 5782
		InvalidHebrewNumber,
		// Token: 0x04001697 RID: 5783
		NotHebrewDigit,
		// Token: 0x04001698 RID: 5784
		FoundEndOfHebrewNumber,
		// Token: 0x04001699 RID: 5785
		ContinueParsing
	}
}
