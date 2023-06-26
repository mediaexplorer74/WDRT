using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines the different language versions of the Gregorian calendar.</summary>
	// Token: 0x020003BC RID: 956
	[ComVisible(true)]
	[Serializable]
	public enum GregorianCalendarTypes
	{
		/// <summary>Refers to the localized version of the Gregorian calendar, based on the language of the <see cref="T:System.Globalization.CultureInfo" /> that uses the <see cref="T:System.Globalization.DateTimeFormatInfo" />.</summary>
		// Token: 0x04001436 RID: 5174
		Localized = 1,
		/// <summary>Refers to the U.S. English version of the Gregorian calendar.</summary>
		// Token: 0x04001437 RID: 5175
		USEnglish,
		/// <summary>Refers to the Middle East French version of the Gregorian calendar.</summary>
		// Token: 0x04001438 RID: 5176
		MiddleEastFrench = 9,
		/// <summary>Refers to the Arabic version of the Gregorian calendar.</summary>
		// Token: 0x04001439 RID: 5177
		Arabic,
		/// <summary>Refers to the transliterated English version of the Gregorian calendar.</summary>
		// Token: 0x0400143A RID: 5178
		TransliteratedEnglish,
		/// <summary>Refers to the transliterated French version of the Gregorian calendar.</summary>
		// Token: 0x0400143B RID: 5179
		TransliteratedFrench
	}
}
