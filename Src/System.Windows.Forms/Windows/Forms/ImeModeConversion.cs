using System;
using System.Collections.Generic;

namespace System.Windows.Forms
{
	/// <summary>Helper class that provides information about the IME conversion mode.</summary>
	// Token: 0x0200016A RID: 362
	public struct ImeModeConversion
	{
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x0003CC70 File Offset: 0x0003AE70
		internal static ImeMode[] ChineseTable
		{
			get
			{
				return ImeModeConversion.chineseTable;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x0003CC77 File Offset: 0x0003AE77
		internal static ImeMode[] JapaneseTable
		{
			get
			{
				return ImeModeConversion.japaneseTable;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x0003CC7E File Offset: 0x0003AE7E
		internal static ImeMode[] KoreanTable
		{
			get
			{
				return ImeModeConversion.koreanTable;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x0003CC85 File Offset: 0x0003AE85
		internal static ImeMode[] UnsupportedTable
		{
			get
			{
				return ImeModeConversion.unsupportedTable;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x0003CC8C File Offset: 0x0003AE8C
		internal static ImeMode[] InputLanguageTable
		{
			get
			{
				InputLanguage currentInputLanguage = InputLanguage.CurrentInputLanguage;
				int num = (int)((long)currentInputLanguage.Handle & 65535L);
				if (num <= 2052)
				{
					if (num <= 1041)
					{
						if (num != 1028)
						{
							if (num != 1041)
							{
								goto IL_8A;
							}
							return ImeModeConversion.japaneseTable;
						}
					}
					else
					{
						if (num == 1042)
						{
							goto IL_7E;
						}
						if (num != 2052)
						{
							goto IL_8A;
						}
					}
				}
				else if (num <= 3076)
				{
					if (num == 2066)
					{
						goto IL_7E;
					}
					if (num != 3076)
					{
						goto IL_8A;
					}
				}
				else if (num != 4100 && num != 5124)
				{
					goto IL_8A;
				}
				return ImeModeConversion.chineseTable;
				IL_7E:
				return ImeModeConversion.koreanTable;
				IL_8A:
				return ImeModeConversion.unsupportedTable;
			}
		}

		/// <summary>Gets a dictionary that contains the conversion mode flags corresponding to each <see cref="T:System.Windows.Forms.ImeMode" />.</summary>
		/// <returns>A dictionary that contains the flags to <see cref="T:System.Windows.Forms.ImeMode" /> mapping.</returns>
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0003CD28 File Offset: 0x0003AF28
		public static Dictionary<ImeMode, ImeModeConversion> ImeModeConversionBits
		{
			get
			{
				if (ImeModeConversion.imeModeConversionBits == null)
				{
					ImeModeConversion.imeModeConversionBits = new Dictionary<ImeMode, ImeModeConversion>(7);
					ImeModeConversion imeModeConversion;
					imeModeConversion.setBits = 9;
					imeModeConversion.clearBits = 2;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.Hiragana, imeModeConversion);
					imeModeConversion.setBits = 11;
					imeModeConversion.clearBits = 0;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.Katakana, imeModeConversion);
					imeModeConversion.setBits = 3;
					imeModeConversion.clearBits = 8;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.KatakanaHalf, imeModeConversion);
					imeModeConversion.setBits = 8;
					imeModeConversion.clearBits = 3;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.AlphaFull, imeModeConversion);
					imeModeConversion.setBits = 0;
					imeModeConversion.clearBits = 11;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.Alpha, imeModeConversion);
					imeModeConversion.setBits = 9;
					imeModeConversion.clearBits = 0;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.HangulFull, imeModeConversion);
					imeModeConversion.setBits = 1;
					imeModeConversion.clearBits = 8;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.Hangul, imeModeConversion);
					imeModeConversion.setBits = 1;
					imeModeConversion.clearBits = 10;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.OnHalf, imeModeConversion);
				}
				return ImeModeConversion.imeModeConversionBits;
			}
		}

		/// <summary>Gets a value that indicates whether the current language table is supported.</summary>
		/// <returns>
		///   <see langword="true" /> if the language table is supported; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0003CE37 File Offset: 0x0003B037
		public static bool IsCurrentConversionTableSupported
		{
			get
			{
				return ImeModeConversion.InputLanguageTable != ImeModeConversion.UnsupportedTable;
			}
		}

		// Token: 0x040008ED RID: 2285
		private static Dictionary<ImeMode, ImeModeConversion> imeModeConversionBits;

		// Token: 0x040008EE RID: 2286
		internal int setBits;

		// Token: 0x040008EF RID: 2287
		internal int clearBits;

		// Token: 0x040008F0 RID: 2288
		internal const int ImeDisabled = 1;

		// Token: 0x040008F1 RID: 2289
		internal const int ImeDirectInput = 2;

		// Token: 0x040008F2 RID: 2290
		internal const int ImeClosed = 3;

		// Token: 0x040008F3 RID: 2291
		internal const int ImeNativeInput = 4;

		// Token: 0x040008F4 RID: 2292
		internal const int ImeNativeFullHiragana = 4;

		// Token: 0x040008F5 RID: 2293
		internal const int ImeNativeHalfHiragana = 5;

		// Token: 0x040008F6 RID: 2294
		internal const int ImeNativeFullKatakana = 6;

		// Token: 0x040008F7 RID: 2295
		internal const int ImeNativeHalfKatakana = 7;

		// Token: 0x040008F8 RID: 2296
		internal const int ImeAlphaFull = 8;

		// Token: 0x040008F9 RID: 2297
		internal const int ImeAlphaHalf = 9;

		// Token: 0x040008FA RID: 2298
		private static ImeMode[] japaneseTable = new ImeMode[]
		{
			ImeMode.Inherit,
			ImeMode.Disable,
			ImeMode.Off,
			ImeMode.Off,
			ImeMode.Hiragana,
			ImeMode.Hiragana,
			ImeMode.Katakana,
			ImeMode.KatakanaHalf,
			ImeMode.AlphaFull,
			ImeMode.Alpha
		};

		// Token: 0x040008FB RID: 2299
		private static ImeMode[] koreanTable = new ImeMode[]
		{
			ImeMode.Inherit,
			ImeMode.Disable,
			ImeMode.Alpha,
			ImeMode.Alpha,
			ImeMode.HangulFull,
			ImeMode.Hangul,
			ImeMode.HangulFull,
			ImeMode.Hangul,
			ImeMode.AlphaFull,
			ImeMode.Alpha
		};

		// Token: 0x040008FC RID: 2300
		private static ImeMode[] chineseTable = new ImeMode[]
		{
			ImeMode.Inherit,
			ImeMode.Disable,
			ImeMode.Off,
			ImeMode.Close,
			ImeMode.On,
			ImeMode.OnHalf,
			ImeMode.On,
			ImeMode.OnHalf,
			ImeMode.Off,
			ImeMode.Off
		};

		// Token: 0x040008FD RID: 2301
		private static ImeMode[] unsupportedTable = new ImeMode[0];
	}
}
