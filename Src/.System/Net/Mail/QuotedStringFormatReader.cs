using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x02000275 RID: 629
	internal static class QuotedStringFormatReader
	{
		// Token: 0x060017A2 RID: 6050 RVA: 0x00078914 File Offset: 0x00076B14
		internal static int ReadReverseQuoted(string data, int index, bool permitUnicode)
		{
			index--;
			for (;;)
			{
				index = WhitespaceReader.ReadFwsReverse(data, index);
				if (index < 0)
				{
					goto IL_75;
				}
				int num = QuotedPairReader.CountQuotedChars(data, index, permitUnicode);
				if (num > 0)
				{
					index -= num;
				}
				else
				{
					if (data[index] == MailBnfHelper.Quote)
					{
						break;
					}
					if (!QuotedStringFormatReader.IsValidQtext(permitUnicode, data[index]))
					{
						goto Block_4;
					}
					index--;
				}
				if (index < 0)
				{
					goto IL_75;
				}
			}
			return index - 1;
			Block_4:
			throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[] { data[index] }));
			IL_75:
			throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[] { MailBnfHelper.Quote }));
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x000789B8 File Offset: 0x00076BB8
		internal static int ReadReverseUnQuoted(string data, int index, bool permitUnicode, bool expectCommaDelimiter)
		{
			for (;;)
			{
				index = WhitespaceReader.ReadFwsReverse(data, index);
				if (index < 0)
				{
					return index;
				}
				int num = QuotedPairReader.CountQuotedChars(data, index, permitUnicode);
				if (num > 0)
				{
					index -= num;
				}
				else
				{
					if (expectCommaDelimiter && data[index] == MailBnfHelper.Comma)
					{
						return index;
					}
					if (!QuotedStringFormatReader.IsValidQtext(permitUnicode, data[index]))
					{
						break;
					}
					index--;
				}
				if (index < 0)
				{
					return index;
				}
			}
			throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[] { data[index] }));
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00078A35 File Offset: 0x00076C35
		private static bool IsValidQtext(bool allowUnicode, char ch)
		{
			if ((int)ch > MailBnfHelper.Ascii7bitMaxValue)
			{
				return allowUnicode;
			}
			return MailBnfHelper.Qtext[(int)ch];
		}
	}
}
