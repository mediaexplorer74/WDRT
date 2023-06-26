using System;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.InputLanguageChanged" /> event.</summary>
	// Token: 0x02000299 RID: 665
	public class InputLanguageChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InputLanguageChangedEventArgs" /> class with the specified locale and character set.</summary>
		/// <param name="culture">The locale of the input language.</param>
		/// <param name="charSet">The character set associated with the new input language.</param>
		// Token: 0x060029FE RID: 10750 RVA: 0x000BF258 File Offset: 0x000BD458
		public InputLanguageChangedEventArgs(CultureInfo culture, byte charSet)
		{
			this.inputLanguage = InputLanguage.FromCulture(culture);
			this.culture = culture;
			this.charSet = charSet;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InputLanguageChangedEventArgs" /> class with the specified input language and character set.</summary>
		/// <param name="inputLanguage">The input language.</param>
		/// <param name="charSet">The character set associated with the new input language.</param>
		// Token: 0x060029FF RID: 10751 RVA: 0x000BF27A File Offset: 0x000BD47A
		public InputLanguageChangedEventArgs(InputLanguage inputLanguage, byte charSet)
		{
			this.inputLanguage = inputLanguage;
			this.culture = inputLanguage.Culture;
			this.charSet = charSet;
		}

		/// <summary>Gets a value indicating the input language.</summary>
		/// <returns>The input language associated with the object.</returns>
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x000BF29C File Offset: 0x000BD49C
		public InputLanguage InputLanguage
		{
			get
			{
				return this.inputLanguage;
			}
		}

		/// <summary>Gets the locale of the input language.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that specifies the locale of the input language.</returns>
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x000BF2A4 File Offset: 0x000BD4A4
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		/// <summary>Gets the character set associated with the new input language.</summary>
		/// <returns>An 8-bit unsigned integer that corresponds to the character set, as shown in the following table.  
		///   Character Set  
		///
		///   Value  
		///
		///   ANSI_CHARSET  
		///
		///   0  
		///
		///   DEFAULT_CHARSET  
		///
		///   1  
		///
		///   SYMBOL_CHARSET  
		///
		///   2  
		///
		///   MAC_CHARSET  
		///
		///   77  
		///
		///   SHIFTJI_CHARSET  
		///
		///   128  
		///
		///   HANGEUL_CHARSET  
		///
		///   129  
		///
		///   HANGUL_CHARSET  
		///
		///   129  
		///
		///   JOHAB_CHARSET  
		///
		///   130  
		///
		///   GB2312_CHARSET  
		///
		///   134  
		///
		///   CHINESEBIG5_CHARSET  
		///
		///   136  
		///
		///   GREEK_CHARSET  
		///
		///   161  
		///
		///   TURKISH_CHARSET  
		///
		///   162  
		///
		///   VIETNAMESE_CHARSET  
		///
		///   163  
		///
		///   HEBREW_CHARSET  
		///
		///   177  
		///
		///   ARABIC_CHARSET  
		///
		///   178  
		///
		///   BALTIC_CHARSET  
		///
		///   186  
		///
		///   RUSSIAN_CHARSET  
		///
		///   204  
		///
		///   THAI_CHARSET  
		///
		///   222  
		///
		///   EASTEUROPE_CHARSET  
		///
		///   238  
		///
		///   OEM_CHARSET  
		///
		///   255</returns>
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x000BF2AC File Offset: 0x000BD4AC
		public byte CharSet
		{
			get
			{
				return this.charSet;
			}
		}

		// Token: 0x0400110E RID: 4366
		private readonly InputLanguage inputLanguage;

		// Token: 0x0400110F RID: 4367
		private readonly CultureInfo culture;

		// Token: 0x04001110 RID: 4368
		private readonly byte charSet;
	}
}
