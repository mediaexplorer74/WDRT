using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x0200004E RID: 78
	internal static class IriHelper
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x0001C078 File Offset: 0x0001A278
		internal static bool CheckIriUnicodeRange(char unicode, bool isQuery)
		{
			return (unicode >= '\u00a0' && unicode <= '\ud7ff') || (unicode >= '豈' && unicode <= '\ufdcf') || (unicode >= 'ﷰ' && unicode <= '\uffef') || (isQuery && unicode >= '\ue000' && unicode <= '\uf8ff');
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001C0D0 File Offset: 0x0001A2D0
		internal static bool CheckIriUnicodeRange(char highSurr, char lowSurr, ref bool surrogatePair, bool isQuery)
		{
			bool flag = false;
			surrogatePair = false;
			if (char.IsSurrogatePair(highSurr, lowSurr))
			{
				surrogatePair = true;
				char[] array = new char[] { highSurr, lowSurr };
				string text = new string(array);
				if ((string.CompareOrdinal(text, "\ud800\udc00") >= 0 && string.CompareOrdinal(text, "\ud83f\udffd") <= 0) || (string.CompareOrdinal(text, "\ud840\udc00") >= 0 && string.CompareOrdinal(text, "\ud87f\udffd") <= 0) || (string.CompareOrdinal(text, "\ud880\udc00") >= 0 && string.CompareOrdinal(text, "\ud8bf\udffd") <= 0) || (string.CompareOrdinal(text, "\ud8c0\udc00") >= 0 && string.CompareOrdinal(text, "\ud8ff\udffd") <= 0) || (string.CompareOrdinal(text, "\ud900\udc00") >= 0 && string.CompareOrdinal(text, "\ud93f\udffd") <= 0) || (string.CompareOrdinal(text, "\ud940\udc00") >= 0 && string.CompareOrdinal(text, "\ud97f\udffd") <= 0) || (string.CompareOrdinal(text, "\ud980\udc00") >= 0 && string.CompareOrdinal(text, "\ud9bf\udffd") <= 0) || (string.CompareOrdinal(text, "\ud9c0\udc00") >= 0 && string.CompareOrdinal(text, "\ud9ff\udffd") <= 0) || (string.CompareOrdinal(text, "\uda00\udc00") >= 0 && string.CompareOrdinal(text, "\uda3f\udffd") <= 0) || (string.CompareOrdinal(text, "\uda40\udc00") >= 0 && string.CompareOrdinal(text, "\uda7f\udffd") <= 0) || (string.CompareOrdinal(text, "\uda80\udc00") >= 0 && string.CompareOrdinal(text, "\udabf\udffd") <= 0) || (string.CompareOrdinal(text, "\udac0\udc00") >= 0 && string.CompareOrdinal(text, "\udaff\udffd") <= 0) || (string.CompareOrdinal(text, "\udb00\udc00") >= 0 && string.CompareOrdinal(text, "\udb3f\udffd") <= 0) || (string.CompareOrdinal(text, "\udb44\udc00") >= 0 && string.CompareOrdinal(text, "\udb7f\udffd") <= 0) || (isQuery && ((string.CompareOrdinal(text, "\udb80\udc00") >= 0 && string.CompareOrdinal(text, "\udbbf\udffd") <= 0) || (string.CompareOrdinal(text, "\udbc0\udc00") >= 0 && string.CompareOrdinal(text, "\udbff\udffd") <= 0))))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
		internal static bool CheckIsReserved(char ch, UriComponents component)
		{
			if (component != UriComponents.Scheme && component != UriComponents.UserInfo && component != UriComponents.Host && component != UriComponents.Port && component != UriComponents.Path && component != UriComponents.Query && component != UriComponents.Fragment)
			{
				return component == (UriComponents)0 && Uri.IsGenDelim(ch);
			}
			if (UriParser.DontEnableStrictRFC3986ReservedCharacterSets)
			{
				if (component <= UriComponents.Host)
				{
					if (component != UriComponents.UserInfo)
					{
						if (component == UriComponents.Host)
						{
							if (ch == ':' || ch == '/' || ch == '?' || ch == '#' || ch == '[' || ch == ']' || ch == '@')
							{
								return true;
							}
						}
					}
					else if (ch == '/' || ch == '?' || ch == '#' || ch == '[' || ch == ']' || ch == '@')
					{
						return true;
					}
				}
				else if (component != UriComponents.Path)
				{
					if (component != UriComponents.Query)
					{
						if (component == UriComponents.Fragment)
						{
							if (ch == '#' || ch == '[' || ch == ']')
							{
								return true;
							}
						}
					}
					else if (ch == '#' || ch == '[' || ch == ']')
					{
						return true;
					}
				}
				else if (ch == '/' || ch == '?' || ch == '#' || ch == '[' || ch == ']')
				{
					return true;
				}
				return false;
			}
			return ";/?:@&=+$,#[]!'()*".IndexOf(ch) >= 0;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001C3EC File Offset: 0x0001A5EC
		internal unsafe static string EscapeUnescapeIri(char* pInput, int start, int end, UriComponents component)
		{
			char[] array = new char[end - start];
			byte[] array2 = null;
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			char* ptr = (char*)(void*)gchandle.AddrOfPinnedObject();
			int num = 0;
			int i = start;
			int num2 = 0;
			bool flag = false;
			while (i < end)
			{
				bool flag2 = false;
				flag = false;
				char c;
				if ((c = pInput[i]) == '%')
				{
					if (i + 2 >= end)
					{
						ptr[(IntPtr)(num2++) * 2] = pInput[i];
						goto IL_2F6;
					}
					c = UriHelper.EscapedAscii(pInput[i + 1], pInput[i + 2]);
					if (c == '\uffff' || c == '%' || IriHelper.CheckIsReserved(c, component) || UriHelper.IsNotSafeForUnescape(c))
					{
						ptr[(IntPtr)(num2++) * 2] = pInput[i++];
						ptr[(IntPtr)(num2++) * 2] = pInput[i++];
						ptr[(IntPtr)(num2++) * 2] = pInput[i];
					}
					else if (c <= '\u007f')
					{
						ptr[(IntPtr)(num2++) * 2] = c;
						i += 2;
					}
					else
					{
						int num3 = i;
						int num4 = 1;
						if (array2 == null)
						{
							array2 = new byte[end - i];
						}
						array2[0] = (byte)c;
						i += 3;
						while (i < end && pInput[i] == '%' && i + 2 < end)
						{
							c = UriHelper.EscapedAscii(pInput[i + 1], pInput[i + 2]);
							if (c == '\uffff' || c < '\u0080')
							{
								break;
							}
							array2[num4++] = (byte)c;
							i += 3;
						}
						i--;
						Encoding encoding = (Encoding)Encoding.UTF8.Clone();
						encoding.EncoderFallback = new EncoderReplacementFallback("");
						encoding.DecoderFallback = new DecoderReplacementFallback("");
						char[] array3 = new char[array2.Length];
						int chars = encoding.GetChars(array2, 0, num4, array3, 0);
						if (chars != 0)
						{
							UriHelper.MatchUTF8Sequence(ptr, array, ref num2, array3, chars, array2, num4, component == UriComponents.Query, true);
							goto IL_2F6;
						}
						for (int j = num3; j <= i; j++)
						{
							ptr[(IntPtr)(num2++) * 2] = pInput[j];
						}
						goto IL_2F6;
					}
				}
				else
				{
					if (c <= '\u007f')
					{
						ptr[(IntPtr)(num2++) * 2] = pInput[i];
						goto IL_2F6;
					}
					if (char.IsHighSurrogate(c) && i + 1 < end)
					{
						char c2 = pInput[i + 1];
						flag2 = !IriHelper.CheckIriUnicodeRange(c, c2, ref flag, component == UriComponents.Query);
						if (!flag2)
						{
							ptr[(IntPtr)(num2++) * 2] = pInput[i++];
							ptr[(IntPtr)(num2++) * 2] = pInput[i];
							goto IL_2F6;
						}
						goto IL_2F6;
					}
					else
					{
						if (!IriHelper.CheckIriUnicodeRange(c, component == UriComponents.Query))
						{
							flag2 = true;
							goto IL_2F6;
						}
						if (!Uri.IsBidiControlCharacter(c) || !UriParser.DontKeepUnicodeBidiFormattingCharacters)
						{
							ptr[(IntPtr)(num2++) * 2] = pInput[i];
							goto IL_2F6;
						}
						goto IL_2F6;
					}
				}
				IL_3E1:
				i++;
				continue;
				IL_2F6:
				if (flag2)
				{
					if (num < 12)
					{
						char[] array4;
						char[] array5;
						char* ptr2;
						checked
						{
							int num5 = array.Length + 90;
							num += 90;
							array4 = new char[num5];
							if ((array5 = array4) == null || array5.Length == 0)
							{
								ptr2 = null;
							}
							else
							{
								ptr2 = &array5[0];
							}
						}
						Buffer.Memcpy((byte*)ptr2, (byte*)ptr, num2 * 2);
						array5 = null;
						if (gchandle.IsAllocated)
						{
							gchandle.Free();
						}
						array = array4;
						gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
						ptr = (char*)(void*)gchandle.AddrOfPinnedObject();
					}
					byte[] array6 = new byte[4];
					byte[] array7;
					byte* ptr3;
					if ((array7 = array6) == null || array7.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array7[0];
					}
					int bytes = Encoding.UTF8.GetBytes(pInput + i, flag ? 2 : 1, ptr3, 4);
					num -= bytes * 3;
					for (int k = 0; k < bytes; k++)
					{
						UriHelper.EscapeAsciiChar((char)array6[k], array, ref num2);
					}
					array7 = null;
					goto IL_3E1;
				}
				goto IL_3E1;
			}
			if (gchandle.IsAllocated)
			{
				gchandle.Free();
			}
			return new string(array, 0, num2);
		}
	}
}
