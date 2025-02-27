﻿using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A75 RID: 2677
	[Serializable]
	internal class ISCIIEncoding : EncodingNLS, ISerializable
	{
		// Token: 0x060068B0 RID: 26800 RVA: 0x00162BA8 File Offset: 0x00160DA8
		public ISCIIEncoding(int codePage)
			: base(codePage)
		{
			this.defaultCodePage = codePage - 57000;
			if (this.defaultCodePage < 2 || this.defaultCodePage > 11)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_CodepageNotSupported", new object[] { codePage }), "codePage");
			}
		}

		// Token: 0x060068B1 RID: 26801 RVA: 0x00162C00 File Offset: 0x00160E00
		internal ISCIIEncoding(SerializationInfo info, StreamingContext context)
			: base(0)
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
		}

		// Token: 0x060068B2 RID: 26802 RVA: 0x00162C18 File Offset: 0x00160E18
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.SerializeEncoding(info, context);
			info.AddValue("m_maxByteSize", 2);
			info.SetType(typeof(MLangCodePageEncoding));
		}

		// Token: 0x060068B3 RID: 26803 RVA: 0x00162C40 File Offset: 0x00160E40
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			long num = (long)charCount + 1L;
			if (base.EncoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.EncoderFallback.MaxCharCount;
			}
			num *= 4L;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
			}
			return (int)num;
		}

		// Token: 0x060068B4 RID: 26804 RVA: 0x00162CB0 File Offset: 0x00160EB0
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			long num = (long)byteCount + 1L;
			if (base.DecoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.DecoderFallback.MaxCharCount;
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
			}
			return (int)num;
		}

		// Token: 0x060068B5 RID: 26805 RVA: 0x00162D19 File Offset: 0x00160F19
		[SecurityCritical]
		internal unsafe override int GetByteCount(char* chars, int count, EncoderNLS baseEncoder)
		{
			return this.GetBytes(chars, count, null, 0, baseEncoder);
		}

		// Token: 0x060068B6 RID: 26806 RVA: 0x00162D28 File Offset: 0x00160F28
		[SecurityCritical]
		internal unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS baseEncoder)
		{
			ISCIIEncoding.ISCIIEncoder isciiencoder = (ISCIIEncoding.ISCIIEncoder)baseEncoder;
			Encoding.EncodingByteBuffer encodingByteBuffer = new Encoding.EncodingByteBuffer(this, isciiencoder, bytes, byteCount, chars, charCount);
			int num = this.defaultCodePage;
			bool flag = false;
			if (isciiencoder != null)
			{
				num = isciiencoder.currentCodePage;
				flag = isciiencoder.bLastVirama;
				if (isciiencoder.charLeftOver > '\0')
				{
					encodingByteBuffer.Fallback(isciiencoder.charLeftOver);
					flag = false;
				}
			}
			while (encodingByteBuffer.MoreData)
			{
				char nextChar = encodingByteBuffer.GetNextChar();
				if (nextChar < '\u00a0')
				{
					if (!encodingByteBuffer.AddByte((byte)nextChar))
					{
						break;
					}
					flag = false;
				}
				else if (nextChar < '\u0901' || nextChar > '൯')
				{
					if (flag && (nextChar == '\u200c' || nextChar == '\u200d'))
					{
						if (nextChar == '\u200c')
						{
							if (!encodingByteBuffer.AddByte(232))
							{
								break;
							}
						}
						else if (!encodingByteBuffer.AddByte(233))
						{
							break;
						}
						flag = false;
					}
					else
					{
						encodingByteBuffer.Fallback(nextChar);
						flag = false;
					}
				}
				else
				{
					int num2 = ISCIIEncoding.UnicodeToIndicChar[(int)(nextChar - '\u0901')];
					byte b = (byte)num2;
					int num3 = 15 & (num2 >> 8);
					int num4 = 61440 & num2;
					if (num2 == 0)
					{
						encodingByteBuffer.Fallback(nextChar);
						flag = false;
					}
					else
					{
						if (num3 != num)
						{
							if (!encodingByteBuffer.AddByte(239, (byte)(num3 | 64)))
							{
								break;
							}
							num = num3;
						}
						if (!encodingByteBuffer.AddByte(b, (num4 != 0) ? 1 : 0))
						{
							break;
						}
						flag = b == 232;
						if (num4 != 0 && !encodingByteBuffer.AddByte(ISCIIEncoding.SecondIndicByte[num4 >> 12]))
						{
							break;
						}
					}
				}
			}
			if (num != this.defaultCodePage && (isciiencoder == null || isciiencoder.MustFlush))
			{
				if (encodingByteBuffer.AddByte(239, (byte)(this.defaultCodePage | 64)))
				{
					num = this.defaultCodePage;
				}
				else
				{
					encodingByteBuffer.GetNextChar();
				}
				flag = false;
			}
			if (isciiencoder != null && bytes != null)
			{
				if (!encodingByteBuffer.fallbackBuffer.bUsedEncoder)
				{
					isciiencoder.charLeftOver = '\0';
				}
				isciiencoder.currentCodePage = num;
				isciiencoder.bLastVirama = flag;
				isciiencoder.m_charsUsed = encodingByteBuffer.CharsUsed;
			}
			return encodingByteBuffer.Count;
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x00162F21 File Offset: 0x00161121
		[SecurityCritical]
		internal unsafe override int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
		{
			return this.GetChars(bytes, count, null, 0, baseDecoder);
		}

		// Token: 0x060068B8 RID: 26808 RVA: 0x00162F30 File Offset: 0x00161130
		[SecurityCritical]
		internal unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
		{
			ISCIIEncoding.ISCIIDecoder isciidecoder = (ISCIIEncoding.ISCIIDecoder)baseDecoder;
			Encoding.EncodingCharBuffer encodingCharBuffer = new Encoding.EncodingCharBuffer(this, isciidecoder, chars, charCount, bytes, byteCount);
			int num = this.defaultCodePage;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			char c = '\0';
			char c2 = '\0';
			if (isciidecoder != null)
			{
				num = isciidecoder.currentCodePage;
				flag = isciidecoder.bLastATR;
				flag2 = isciidecoder.bLastVirama;
				flag3 = isciidecoder.bLastDevenagariStressAbbr;
				c = isciidecoder.cLastCharForNextNukta;
				c2 = isciidecoder.cLastCharForNoNextNukta;
			}
			bool flag4 = (flag2 || flag || flag3) | (c > '\0');
			int num2 = -1;
			if (num >= 2 && num <= 11)
			{
				num2 = ISCIIEncoding.IndicMappingIndex[num];
			}
			while (encodingCharBuffer.MoreData)
			{
				byte nextByte = encodingCharBuffer.GetNextByte();
				if (flag4)
				{
					flag4 = false;
					if (flag)
					{
						if (nextByte >= 66 && nextByte <= 75)
						{
							num = (int)(nextByte & 15);
							num2 = ISCIIEncoding.IndicMappingIndex[num];
							flag = false;
							continue;
						}
						if (nextByte == 64)
						{
							num = this.defaultCodePage;
							num2 = -1;
							if (num >= 2 && num <= 11)
							{
								num2 = ISCIIEncoding.IndicMappingIndex[num];
							}
							flag = false;
							continue;
						}
						if (nextByte == 65)
						{
							num = this.defaultCodePage;
							num2 = -1;
							if (num >= 2 && num <= 11)
							{
								num2 = ISCIIEncoding.IndicMappingIndex[num];
							}
							flag = false;
							continue;
						}
						if (!encodingCharBuffer.Fallback(239))
						{
							break;
						}
						flag = false;
					}
					else if (flag2)
					{
						if (nextByte == 232)
						{
							if (encodingCharBuffer.AddChar('\u200c'))
							{
								flag2 = false;
								continue;
							}
							break;
						}
						else if (nextByte == 233)
						{
							if (encodingCharBuffer.AddChar('\u200d'))
							{
								flag2 = false;
								continue;
							}
							break;
						}
						else
						{
							flag2 = false;
						}
					}
					else if (flag3)
					{
						if (nextByte == 184)
						{
							if (encodingCharBuffer.AddChar('\u0952'))
							{
								flag3 = false;
								continue;
							}
							break;
						}
						else if (nextByte == 191)
						{
							if (encodingCharBuffer.AddChar('॰'))
							{
								flag3 = false;
								continue;
							}
							break;
						}
						else
						{
							if (!encodingCharBuffer.Fallback(240))
							{
								break;
							}
							flag3 = false;
						}
					}
					else if (nextByte == 233)
					{
						if (encodingCharBuffer.AddChar(c))
						{
							c2 = (c = '\0');
							continue;
						}
						break;
					}
					else
					{
						if (!encodingCharBuffer.AddChar(c2))
						{
							break;
						}
						c2 = (c = '\0');
					}
				}
				if (nextByte < 160)
				{
					if (!encodingCharBuffer.AddChar((char)nextByte))
					{
						break;
					}
				}
				else if (nextByte == 239)
				{
					flag4 = (flag = true);
				}
				else
				{
					char c3 = ISCIIEncoding.IndicMapping[num2, 0, (int)(nextByte - 160)];
					char c4 = ISCIIEncoding.IndicMapping[num2, 1, (int)(nextByte - 160)];
					if (c4 == '\0' || nextByte == 233)
					{
						if (c3 == '\0')
						{
							if (!encodingCharBuffer.Fallback(nextByte))
							{
								break;
							}
						}
						else if (!encodingCharBuffer.AddChar(c3))
						{
							break;
						}
					}
					else if (nextByte == 232)
					{
						if (!encodingCharBuffer.AddChar(c3))
						{
							break;
						}
						flag4 = (flag2 = true);
					}
					else if ((c4 & '\uf000') == '\0')
					{
						flag4 = true;
						c = c4;
						c2 = c3;
					}
					else
					{
						flag4 = (flag3 = true);
					}
				}
			}
			if (isciidecoder == null || isciidecoder.MustFlush)
			{
				if (flag)
				{
					if (encodingCharBuffer.Fallback(239))
					{
						flag = false;
					}
					else
					{
						encodingCharBuffer.GetNextByte();
					}
				}
				else if (flag3)
				{
					if (encodingCharBuffer.Fallback(240))
					{
						flag3 = false;
					}
					else
					{
						encodingCharBuffer.GetNextByte();
					}
				}
				else if (c2 != '\0')
				{
					if (encodingCharBuffer.AddChar(c2))
					{
						c = (c2 = '\0');
					}
					else
					{
						encodingCharBuffer.GetNextByte();
					}
				}
			}
			if (isciidecoder != null && chars != null)
			{
				if (!isciidecoder.MustFlush || c2 > '\0' || flag || flag3)
				{
					isciidecoder.currentCodePage = num;
					isciidecoder.bLastVirama = flag2;
					isciidecoder.bLastATR = flag;
					isciidecoder.bLastDevenagariStressAbbr = flag3;
					isciidecoder.cLastCharForNextNukta = c;
					isciidecoder.cLastCharForNoNextNukta = c2;
				}
				else
				{
					isciidecoder.currentCodePage = this.defaultCodePage;
					isciidecoder.bLastVirama = false;
					isciidecoder.bLastATR = false;
					isciidecoder.bLastDevenagariStressAbbr = false;
					isciidecoder.cLastCharForNextNukta = '\0';
					isciidecoder.cLastCharForNoNextNukta = '\0';
				}
				isciidecoder.m_bytesUsed = encodingCharBuffer.BytesUsed;
			}
			return encodingCharBuffer.Count;
		}

		// Token: 0x060068B9 RID: 26809 RVA: 0x00163309 File Offset: 0x00161509
		public override Decoder GetDecoder()
		{
			return new ISCIIEncoding.ISCIIDecoder(this);
		}

		// Token: 0x060068BA RID: 26810 RVA: 0x00163311 File Offset: 0x00161511
		public override Encoder GetEncoder()
		{
			return new ISCIIEncoding.ISCIIEncoder(this);
		}

		// Token: 0x060068BB RID: 26811 RVA: 0x00163319 File Offset: 0x00161519
		public override int GetHashCode()
		{
			return this.defaultCodePage + base.EncoderFallback.GetHashCode() + base.DecoderFallback.GetHashCode();
		}

		// Token: 0x04002EC2 RID: 11970
		private const int CodeDefault = 0;

		// Token: 0x04002EC3 RID: 11971
		private const int CodeRoman = 1;

		// Token: 0x04002EC4 RID: 11972
		private const int CodeDevanagari = 2;

		// Token: 0x04002EC5 RID: 11973
		private const int CodeBengali = 3;

		// Token: 0x04002EC6 RID: 11974
		private const int CodeTamil = 4;

		// Token: 0x04002EC7 RID: 11975
		private const int CodeTelugu = 5;

		// Token: 0x04002EC8 RID: 11976
		private const int CodeAssamese = 6;

		// Token: 0x04002EC9 RID: 11977
		private const int CodeOriya = 7;

		// Token: 0x04002ECA RID: 11978
		private const int CodeKannada = 8;

		// Token: 0x04002ECB RID: 11979
		private const int CodeMalayalam = 9;

		// Token: 0x04002ECC RID: 11980
		private const int CodeGujarati = 10;

		// Token: 0x04002ECD RID: 11981
		private const int CodePunjabi = 11;

		// Token: 0x04002ECE RID: 11982
		private const int MultiByteBegin = 160;

		// Token: 0x04002ECF RID: 11983
		private const int IndicBegin = 2305;

		// Token: 0x04002ED0 RID: 11984
		private const int IndicEnd = 3439;

		// Token: 0x04002ED1 RID: 11985
		private const byte ControlATR = 239;

		// Token: 0x04002ED2 RID: 11986
		private const byte ControlCodePageStart = 64;

		// Token: 0x04002ED3 RID: 11987
		private const byte Virama = 232;

		// Token: 0x04002ED4 RID: 11988
		private const byte Nukta = 233;

		// Token: 0x04002ED5 RID: 11989
		private const byte DevenagariExt = 240;

		// Token: 0x04002ED6 RID: 11990
		private const char ZWNJ = '\u200c';

		// Token: 0x04002ED7 RID: 11991
		private const char ZWJ = '\u200d';

		// Token: 0x04002ED8 RID: 11992
		private int defaultCodePage;

		// Token: 0x04002ED9 RID: 11993
		private static int[] UnicodeToIndicChar = new int[]
		{
			673, 674, 675, 0, 676, 677, 678, 679, 680, 681,
			682, 4774, 686, 683, 684, 685, 690, 687, 688, 689,
			691, 692, 693, 694, 695, 696, 697, 698, 699, 700,
			701, 702, 703, 704, 705, 706, 707, 708, 709, 710,
			711, 712, 713, 714, 715, 716, 717, 719, 720, 721,
			722, 723, 724, 725, 726, 727, 728, 0, 0, 745,
			4842, 730, 731, 732, 733, 734, 735, 4831, 739, 736,
			737, 738, 743, 740, 741, 742, 744, 0, 0, 4769,
			0, 8944, 0, 0, 0, 0, 0, 4787, 4788, 4789,
			4794, 4799, 4800, 4809, 718, 4778, 4775, 4827, 4828, 746,
			0, 753, 754, 755, 756, 757, 758, 759, 760, 761,
			762, 13040, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 929, 930,
			931, 0, 932, 933, 934, 935, 936, 937, 938, 5030,
			0, 0, 939, 941, 0, 0, 943, 945, 947, 948,
			949, 950, 951, 952, 953, 954, 955, 956, 957, 958,
			959, 960, 961, 962, 963, 964, 965, 966, 0, 968,
			969, 970, 971, 972, 973, 975, 0, 977, 0, 0,
			0, 981, 982, 983, 984, 0, 0, 1001, 0, 986,
			987, 988, 989, 990, 991, 5087, 0, 0, 992, 994,
			0, 0, 996, 998, 1000, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 5055,
			5056, 0, 974, 5034, 5031, 5083, 5084, 0, 0, 1009,
			1010, 1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 2978, 0, 0,
			2980, 2981, 2982, 2983, 2984, 2985, 0, 0, 0, 0,
			2987, 2989, 0, 0, 2992, 2993, 2995, 2996, 2997, 2998,
			2999, 3000, 3001, 3002, 3003, 3004, 3005, 3006, 3007, 3008,
			3009, 3010, 3011, 3012, 3013, 3014, 0, 3016, 3017, 3018,
			3019, 3020, 3021, 3023, 0, 3025, 3026, 0, 3028, 3029,
			0, 3031, 3032, 0, 0, 3049, 0, 3034, 3035, 3036,
			3037, 3038, 0, 0, 0, 0, 3040, 3042, 0, 0,
			3044, 3046, 3048, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 7092, 7093, 7098, 7104, 0, 7113,
			0, 0, 0, 0, 0, 0, 0, 3057, 3058, 3059,
			3060, 3061, 3062, 3063, 3064, 3065, 3066, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 2721, 2722, 2723, 0, 2724, 2725,
			2726, 2727, 2728, 2729, 2730, 0, 2734, 0, 2731, 2733,
			2738, 0, 2736, 2737, 2739, 2740, 2741, 2742, 2743, 2744,
			2745, 2746, 2747, 2748, 2749, 2750, 2751, 2752, 2753, 2754,
			2755, 2756, 2757, 2758, 0, 2760, 2761, 2762, 2763, 2764,
			2765, 2767, 0, 2769, 2770, 0, 2772, 2773, 2774, 2775,
			2776, 0, 0, 2793, 6890, 2778, 2779, 2780, 2781, 2782,
			2783, 6879, 2787, 0, 2784, 2786, 2791, 0, 2788, 2790,
			2792, 0, 0, 6817, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 6826,
			0, 0, 0, 0, 0, 2801, 2802, 2803, 2804, 2805,
			2806, 2807, 2808, 2809, 2810, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 1953, 1954, 1955, 0, 1956, 1957, 1958, 1959,
			1960, 1961, 1962, 6054, 0, 0, 1963, 1965, 0, 0,
			1968, 1969, 1971, 1972, 1973, 1974, 1975, 1976, 1977, 1978,
			1979, 1980, 1981, 1982, 1983, 1984, 1985, 1986, 1987, 1988,
			1989, 1990, 0, 1992, 1993, 1994, 1995, 1996, 1997, 1999,
			0, 2001, 2002, 0, 0, 2005, 2006, 2007, 2008, 0,
			0, 2025, 6122, 2010, 2011, 2012, 2013, 2014, 2015, 0,
			0, 0, 2016, 2018, 0, 0, 2020, 2022, 2024, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 6079, 6080, 0, 1998, 6058, 6055, 0,
			0, 0, 0, 2033, 2034, 2035, 2036, 2037, 2038, 2039,
			2040, 2041, 2042, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 1186, 1187, 0, 1188, 1189, 1190, 1191, 1192, 1193,
			0, 0, 0, 0, 1195, 1197, 0, 1199, 1200, 1201,
			1203, 0, 0, 0, 1207, 1208, 0, 1210, 0, 1212,
			1213, 0, 0, 0, 1217, 1218, 0, 0, 0, 1222,
			1223, 1224, 0, 0, 0, 1228, 1229, 1231, 1232, 1233,
			1234, 1235, 1236, 0, 1237, 1239, 1240, 0, 0, 0,
			0, 1242, 1243, 1244, 1245, 1246, 0, 0, 0, 1248,
			1249, 1250, 0, 1252, 1253, 1254, 1256, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 1266, 1267, 1268, 1269, 1270, 1271, 1272, 1273,
			1274, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 1441, 1442,
			1443, 0, 1444, 1445, 1446, 1447, 1448, 1449, 1450, 5542,
			0, 1451, 1452, 1453, 0, 1455, 1456, 1457, 1459, 1460,
			1461, 1462, 1463, 1464, 1465, 1466, 1467, 1468, 1469, 1470,
			1471, 1472, 1473, 1474, 1475, 1476, 1477, 1478, 0, 1480,
			1481, 1482, 1483, 1484, 1485, 1487, 1488, 1489, 1490, 0,
			1492, 1493, 1494, 1495, 1496, 0, 0, 0, 0, 1498,
			1499, 1500, 1501, 1502, 1503, 5599, 0, 1504, 1505, 1506,
			0, 1508, 1509, 1510, 1512, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 5546, 5543, 0, 0, 0, 0, 1521,
			1522, 1523, 1524, 1525, 1526, 1527, 1528, 1529, 1530, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 2210, 2211, 0,
			2212, 2213, 2214, 2215, 2216, 2217, 2218, 6310, 0, 2219,
			2220, 2221, 0, 2223, 2224, 2225, 2227, 2228, 2229, 2230,
			2231, 2232, 2233, 2234, 2235, 2236, 2237, 2238, 2239, 2240,
			2241, 2242, 2243, 2244, 2245, 2246, 0, 2248, 2249, 2250,
			2251, 2252, 2253, 2255, 2256, 2257, 2258, 0, 2260, 2261,
			2262, 2263, 2264, 0, 0, 0, 0, 2266, 2267, 2268,
			2269, 2270, 2271, 6367, 0, 2272, 2273, 2274, 0, 2276,
			2277, 2278, 2280, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 6345,
			0, 6314, 6311, 0, 0, 0, 0, 2289, 2290, 2291,
			2292, 2293, 2294, 2295, 2296, 2297, 2298, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 2466, 2467, 0, 2468, 2469,
			2470, 2471, 2472, 2473, 2474, 6566, 0, 2475, 2476, 2477,
			0, 2479, 2480, 2481, 2483, 2484, 2485, 2486, 2487, 2488,
			2489, 2490, 2491, 2492, 2493, 2494, 2495, 2496, 2497, 2498,
			2499, 2500, 2501, 2502, 0, 2504, 2505, 2506, 2507, 2508,
			2509, 2511, 2512, 2513, 2514, 2515, 2516, 2517, 2518, 2519,
			2520, 0, 0, 0, 0, 2522, 2523, 2524, 2525, 2526,
			2527, 0, 0, 2528, 2529, 2530, 0, 2532, 2533, 2534,
			2536, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 6570,
			6567, 0, 0, 0, 0, 2545, 2546, 2547, 2548, 2549,
			2550, 2551, 2552, 2553, 2554
		};

		// Token: 0x04002EDA RID: 11994
		private static byte[] SecondIndicByte = new byte[] { 0, 233, 184, 191 };

		// Token: 0x04002EDB RID: 11995
		private static int[] IndicMappingIndex = new int[]
		{
			-1, -1, 0, 1, 2, 3, 1, 4, 5, 6,
			7, 8
		};

		// Token: 0x04002EDC RID: 11996
		private static char[,,] IndicMapping = new char[,,]
		{
			{
				{
					'\0', '\u0901', '\u0902', '\u0903', 'अ', 'आ', 'इ', 'ई', 'उ', 'ऊ',
					'ऋ', 'ऎ', 'ए', 'ऐ', 'ऍ', 'ऒ', 'ओ', 'औ', 'ऑ', 'क',
					'ख', 'ग', 'घ', 'ङ', 'च', 'छ', 'ज', 'झ', 'ञ', 'ट',
					'ठ', 'ड', 'ढ', 'ण', 'त', 'थ', 'द', 'ध', 'न', 'ऩ',
					'प', 'फ', 'ब', 'भ', 'म', 'य', 'य़', 'र', 'ऱ', 'ल',
					'ळ', 'ऴ', 'व', 'श', 'ष', 'स', 'ह', '\0', '\u093e', '\u093f',
					'\u0940', '\u0941', '\u0942', '\u0943', '\u0946', '\u0947', '\u0948', '\u0945', '\u094a', '\u094b',
					'\u094c', '\u0949', '\u094d', '\u093c', '।', '\0', '\0', '\0', '\0', '\0',
					'\0', '०', '१', '२', '३', '४', '५', '६', '७', '८',
					'९', '\0', '\0', '\0', '\0', '\0'
				},
				{
					'\0', 'ॐ', '\0', '\0', '\0', '\0', 'ऌ', 'ॡ', '\0', '\0',
					'ॠ', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'क़',
					'ख़', 'ग़', '\0', '\0', '\0', '\0', 'ज़', '\0', '\0', '\0',
					'\0', 'ड़', 'ढ़', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', 'फ़', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\u0962',
					'\u0963', '\0', '\0', '\u0944', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\u200c', '\u200d', 'ऽ', '\0', '\0', '\0', '\0', '\0',
					'뢿', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0'
				}
			},
			{
				{
					'\0', '\u0981', '\u0982', '\u0983', 'অ', 'আ', 'ই', 'ঈ', 'উ', 'ঊ',
					'ঋ', 'এ', 'এ', 'ঐ', 'ঐ', 'ও', 'ও', 'ঔ', 'ঔ', 'ক',
					'খ', 'গ', 'ঘ', 'ঙ', 'চ', 'ছ', 'জ', 'ঝ', 'ঞ', 'ট',
					'ঠ', 'ড', 'ঢ', 'ণ', 'ত', 'থ', 'দ', 'ধ', 'ন', 'ন',
					'প', 'ফ', 'ব', 'ভ', 'ম', 'য', 'য়', 'র', 'র', 'ল',
					'ল', 'ল', 'ব', 'শ', 'ষ', 'স', 'হ', '\0', '\u09be', '\u09bf',
					'\u09c0', '\u09c1', '\u09c2', '\u09c3', '\u09c7', '\u09c7', '\u09c8', '\u09c8', '\u09cb', '\u09cb',
					'\u09cc', '\u09cc', '\u09cd', '\u09bc', '.', '\0', '\0', '\0', '\0', '\0',
					'\0', '০', '১', '২', '৩', '৪', '৫', '৬', '৭', '৮',
					'৯', '\0', '\0', '\0', '\0', '\0'
				},
				{
					'\0', '\0', '\0', '\0', '\0', '\0', 'ঌ', 'ৡ', '\0', '\0',
					'ৠ', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', 'ড়', 'ঢ়', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\u09e2',
					'\u09e3', '\0', '\0', '\u09c4', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\u200c', '\u200d', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0'
				}
			},
			{
				{
					'\0', '\0', '\u0b82', 'ஃ', 'அ', 'ஆ', 'இ', 'ஈ', 'உ', 'ஊ',
					'\0', 'ஏ', 'ஏ', 'ஐ', 'ஐ', 'ஒ', 'ஓ', 'ஔ', 'ஔ', 'க',
					'க', 'க', 'க', 'ங', 'ச', 'ச', 'ஜ', 'ஜ', 'ஞ', 'ட',
					'ட', 'ட', 'ட', 'ண', 'த', 'த', 'த', 'த', 'ந', 'ன',
					'ப', 'ப', 'ப', 'ப', 'ம', 'ய', 'ய', 'ர', 'ற', 'ல',
					'ள', 'ழ', 'வ', 'ஷ', 'ஷ', 'ஸ', 'ஹ', '\0', '\u0bbe', '\u0bbf',
					'\u0bc0', '\u0bc1', '\u0bc2', '\0', '\u0bc6', '\u0bc7', '\u0bc8', '\u0bc8', '\u0bca', '\u0bcb',
					'\u0bcc', '\u0bcc', '\u0bcd', '\0', '.', '\0', '\0', '\0', '\0', '\0',
					'\0', '0', '௧', '௨', '௩', '௪', '௫', '௬', '௭', '௮',
					'௯', '\0', '\0', '\0', '\0', '\0'
				},
				{
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\u200c', '\u200d', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0'
				}
			},
			{
				{
					'\0', '\u0c01', '\u0c02', '\u0c03', 'అ', 'ఆ', 'ఇ', 'ఈ', 'ఉ', 'ఊ',
					'ఋ', 'ఎ', 'ఏ', 'ఐ', 'ఐ', 'ఒ', 'ఓ', 'ఔ', 'ఔ', 'క',
					'ఖ', 'గ', 'ఘ', 'ఙ', 'చ', 'ఛ', 'జ', 'ఝ', 'ఞ', 'ట',
					'ఠ', 'డ', 'ఢ', 'ణ', 'త', 'థ', 'ద', 'ధ', 'న', 'న',
					'ప', 'ఫ', 'బ', 'భ', 'మ', 'య', 'య', 'ర', 'ఱ', 'ల',
					'ళ', 'ళ', 'వ', 'శ', 'ష', 'స', 'హ', '\0', '\u0c3e', '\u0c3f',
					'\u0c40', '\u0c41', '\u0c42', '\u0c43', '\u0c46', '\u0c47', '\u0c48', '\u0c48', '\u0c4a', '\u0c4b',
					'\u0c4c', '\u0c4c', '\u0c4d', '\0', '.', '\0', '\0', '\0', '\0', '\0',
					'\0', '౦', '౧', '౨', '౩', '౪', '౫', '౬', '౭', '౮',
					'౯', '\0', '\0', '\0', '\0', '\0'
				},
				{
					'\0', '\0', '\0', '\0', '\0', '\0', 'ఌ', 'ౡ', '\0', '\0',
					'ౠ', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\u0c44', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\u200c', '\u200d', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0'
				}
			},
			{
				{
					'\0', '\u0b01', '\u0b02', '\u0b03', 'ଅ', 'ଆ', 'ଇ', 'ଈ', 'ଉ', 'ଊ',
					'ଋ', 'ଏ', 'ଏ', 'ଐ', 'ଐ', 'ଐ', 'ଓ', 'ଔ', 'ଔ', 'କ',
					'ଖ', 'ଗ', 'ଘ', 'ଙ', 'ଚ', 'ଛ', 'ଜ', 'ଝ', 'ଞ', 'ଟ',
					'ଠ', 'ଡ', 'ଢ', 'ଣ', 'ତ', 'ଥ', 'ଦ', 'ଧ', 'ନ', 'ନ',
					'ପ', 'ଫ', 'ବ', 'ଭ', 'ମ', 'ଯ', 'ୟ', 'ର', 'ର', 'ଲ',
					'ଳ', 'ଳ', 'ବ', 'ଶ', 'ଷ', 'ସ', 'ହ', '\0', '\u0b3e', '\u0b3f',
					'\u0b40', '\u0b41', '\u0b42', '\u0b43', '\u0b47', '\u0b47', '\u0b48', '\u0b48', '\u0b4b', '\u0b4b',
					'\u0b4c', '\u0b4c', '\u0b4d', '\u0b3c', '.', '\0', '\0', '\0', '\0', '\0',
					'\0', '୦', '୧', '୨', '୩', '୪', '୫', '୬', '୭', '୮',
					'୯', '\0', '\0', '\0', '\0', '\0'
				},
				{
					'\0', '\0', '\0', '\0', '\0', '\0', 'ఌ', 'ౡ', '\0', '\0',
					'ౠ', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', 'ଡ଼', 'ଢ଼', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\u0c44', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\u200c', '\u200d', 'ଽ', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0'
				}
			},
			{
				{
					'\0', '\0', '\u0c82', '\u0c83', 'ಅ', 'ಆ', 'ಇ', 'ಈ', 'ಉ', 'ಊ',
					'ಋ', 'ಎ', 'ಏ', 'ಐ', 'ಐ', 'ಒ', 'ಓ', 'ಔ', 'ಔ', 'ಕ',
					'ಖ', 'ಗ', 'ಘ', 'ಙ', 'ಚ', 'ಛ', 'ಜ', 'ಝ', 'ಞ', 'ಟ',
					'ಠ', 'ಡ', 'ಢ', 'ಣ', 'ತ', 'ಥ', 'ದ', 'ಧ', 'ನ', 'ನ',
					'ಪ', 'ಫ', 'ಬ', 'ಭ', 'ಮ', 'ಯ', 'ಯ', 'ರ', 'ಱ', 'ಲ',
					'ಳ', 'ಳ', 'ವ', 'ಶ', 'ಷ', 'ಸ', 'ಹ', '\0', '\u0cbe', '\u0cbf',
					'\u0cc0', '\u0cc1', '\u0cc2', '\u0cc3', '\u0cc6', '\u0cc7', '\u0cc8', '\u0cc8', '\u0cca', '\u0ccb',
					'\u0ccc', '\u0ccc', '\u0ccd', '\0', '.', '\0', '\0', '\0', '\0', '\0',
					'\0', '೦', '೧', '೨', '೩', '೪', '೫', '೬', '೭', '೮',
					'೯', '\0', '\0', '\0', '\0', '\0'
				},
				{
					'\0', '\0', '\0', '\0', '\0', '\0', 'ಌ', 'ೡ', '\0', '\0',
					'ೠ', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', 'ೞ', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\u0cc4', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\u200c', '\u200d', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0'
				}
			},
			{
				{
					'\0', '\0', '\u0d02', '\u0d03', 'അ', 'ആ', 'ഇ', 'ഈ', 'ഉ', 'ഊ',
					'ഋ', 'എ', 'ഏ', 'ഐ', 'ഐ', 'ഒ', 'ഓ', 'ഔ', 'ഔ', 'ക',
					'ഖ', 'ഗ', 'ഘ', 'ങ', 'ച', 'ഛ', 'ജ', 'ഝ', 'ഞ', 'ട',
					'ഠ', 'ഡ', 'ഢ', 'ണ', 'ത', 'ഥ', 'ദ', 'ധ', 'ന', 'ന',
					'പ', 'ഫ', 'ബ', 'ഭ', 'മ', 'യ', 'യ', 'ര', 'റ', 'ല',
					'ള', 'ഴ', 'വ', 'ശ', 'ഷ', 'സ', 'ഹ', '\0', '\u0d3e', '\u0d3f',
					'\u0d40', '\u0d41', '\u0d42', '\u0d43', '\u0d46', '\u0d47', '\u0d48', '\u0d48', '\u0d4a', '\u0d4b',
					'\u0d4c', '\u0d4c', '\u0d4d', '\0', '.', '\0', '\0', '\0', '\0', '\0',
					'\0', '൦', '൧', '൨', '൩', '൪', '൫', '൬', '൭', '൮',
					'൯', '\0', '\0', '\0', '\0', '\0'
				},
				{
					'\0', '\0', '\0', '\0', '\0', '\0', 'ഌ', 'ൡ', '\0', '\0',
					'ൠ', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\u200c', '\u200d', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0'
				}
			},
			{
				{
					'\0', '\u0a81', '\u0a82', '\u0a83', 'અ', 'આ', 'ઇ', 'ઈ', 'ઉ', 'ઊ',
					'ઋ', 'એ', 'એ', 'ઐ', 'ઍ', 'ઍ', 'ઓ', 'ઔ', 'ઑ', 'ક',
					'ખ', 'ગ', 'ઘ', 'ઙ', 'ચ', 'છ', 'જ', 'ઝ', 'ઞ', 'ટ',
					'ઠ', 'ડ', 'ઢ', 'ણ', 'ત', 'થ', 'દ', 'ધ', 'ન', 'ન',
					'પ', 'ફ', 'બ', 'ભ', 'મ', 'ય', 'ય', 'ર', 'ર', 'લ',
					'ળ', 'ળ', 'વ', 'શ', 'ષ', 'સ', 'હ', '\0', '\u0abe', '\u0abf',
					'\u0ac0', '\u0ac1', '\u0ac2', '\u0ac3', '\u0ac7', '\u0ac7', '\u0ac8', '\u0ac5', '\u0acb', '\u0acb',
					'\u0acc', '\u0ac9', '\u0acd', '\u0abc', '.', '\0', '\0', '\0', '\0', '\0',
					'\0', '૦', '૧', '૨', '૩', '૪', '૫', '૬', '૭', '૮',
					'૯', '\0', '\0', '\0', '\0', '\0'
				},
				{
					'\0', 'ૐ', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'ૠ', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\u0ac4', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\u200c', '\u200d', 'ઽ', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0'
				}
			},
			{
				{
					'\0', '\0', '\u0a02', '\0', 'ਅ', 'ਆ', 'ਇ', 'ਈ', 'ਉ', 'ਊ',
					'\0', 'ਏ', 'ਏ', 'ਐ', 'ਐ', 'ਐ', 'ਓ', 'ਔ', 'ਔ', 'ਕ',
					'ਖ', 'ਗ', 'ਘ', 'ਙ', 'ਚ', 'ਛ', 'ਜ', 'ਝ', 'ਞ', 'ਟ',
					'ਠ', 'ਡ', 'ਢ', 'ਣ', 'ਤ', 'ਥ', 'ਦ', 'ਧ', 'ਨ', 'ਨ',
					'ਪ', 'ਫ', 'ਬ', 'ਭ', 'ਮ', 'ਯ', 'ਯ', 'ਰ', 'ਰ', 'ਲ',
					'ਲ਼', 'ਲ਼', 'ਵ', 'ਸ਼', 'ਸ਼', 'ਸ', 'ਹ', '\0', '\u0a3e', '\u0a3f',
					'\u0a40', '\u0a41', '\u0a42', '\0', '\u0a47', '\u0a47', '\u0a48', '\u0a48', '\u0a4b', '\u0a4b',
					'\u0a4c', '\u0a4c', '\u0a4d', '\u0a3c', '.', '\0', '\0', '\0', '\0', '\0',
					'\0', '੦', '੧', '੨', '੩', '੪', '੫', '੬', '੭', '੮',
					'੯', '\0', '\0', '\0', '\0', '\0'
				},
				{
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'ਖ਼', 'ਗ਼', '\0', '\0', '\0', '\0', 'ਜ਼', '\0', '\0', '\0',
					'\0', '\0', 'ੜ', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', 'ਫ਼', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\u200c', '\u200d', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
					'\0', '\0', '\0', '\0', '\0', '\0'
				}
			}
		};

		// Token: 0x02000CAE RID: 3246
		[Serializable]
		internal class ISCIIEncoder : EncoderNLS
		{
			// Token: 0x0600719D RID: 29085 RVA: 0x00188064 File Offset: 0x00186264
			public ISCIIEncoder(Encoding encoding)
				: base(encoding)
			{
				this.currentCodePage = (this.defaultCodePage = encoding.CodePage - 57000);
			}

			// Token: 0x0600719E RID: 29086 RVA: 0x00188093 File Offset: 0x00186293
			public override void Reset()
			{
				this.bLastVirama = false;
				this.charLeftOver = '\0';
				if (this.m_fallbackBuffer != null)
				{
					this.m_fallbackBuffer.Reset();
				}
			}

			// Token: 0x17001375 RID: 4981
			// (get) Token: 0x0600719F RID: 29087 RVA: 0x001880B6 File Offset: 0x001862B6
			internal override bool HasState
			{
				get
				{
					return this.charLeftOver != '\0' || this.currentCodePage != this.defaultCodePage;
				}
			}

			// Token: 0x040038B9 RID: 14521
			internal int defaultCodePage;

			// Token: 0x040038BA RID: 14522
			internal int currentCodePage;

			// Token: 0x040038BB RID: 14523
			internal bool bLastVirama;
		}

		// Token: 0x02000CAF RID: 3247
		[Serializable]
		internal class ISCIIDecoder : DecoderNLS
		{
			// Token: 0x060071A0 RID: 29088 RVA: 0x001880D3 File Offset: 0x001862D3
			public ISCIIDecoder(Encoding encoding)
				: base(encoding)
			{
				this.currentCodePage = encoding.CodePage - 57000;
			}

			// Token: 0x060071A1 RID: 29089 RVA: 0x001880EE File Offset: 0x001862EE
			public override void Reset()
			{
				this.bLastATR = false;
				this.bLastVirama = false;
				this.bLastDevenagariStressAbbr = false;
				this.cLastCharForNextNukta = '\0';
				this.cLastCharForNoNextNukta = '\0';
				if (this.m_fallbackBuffer != null)
				{
					this.m_fallbackBuffer.Reset();
				}
			}

			// Token: 0x17001376 RID: 4982
			// (get) Token: 0x060071A2 RID: 29090 RVA: 0x00188126 File Offset: 0x00186326
			internal override bool HasState
			{
				get
				{
					return this.cLastCharForNextNukta != '\0' || this.cLastCharForNoNextNukta != '\0' || this.bLastATR || this.bLastDevenagariStressAbbr;
				}
			}

			// Token: 0x040038BC RID: 14524
			internal int currentCodePage;

			// Token: 0x040038BD RID: 14525
			internal bool bLastATR;

			// Token: 0x040038BE RID: 14526
			internal bool bLastVirama;

			// Token: 0x040038BF RID: 14527
			internal bool bLastDevenagariStressAbbr;

			// Token: 0x040038C0 RID: 14528
			internal char cLastCharForNextNukta;

			// Token: 0x040038C1 RID: 14529
			internal char cLastCharForNoNextNukta;
		}
	}
}
