using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x0200029A RID: 666
	internal static class WhitespaceReader
	{
		// Token: 0x060018B7 RID: 6327 RVA: 0x0007D8B0 File Offset: 0x0007BAB0
		internal static int ReadFwsReverse(string data, int index)
		{
			bool flag = false;
			while (index >= 0)
			{
				if (data[index] == MailBnfHelper.CR && flag)
				{
					flag = false;
				}
				else
				{
					if (data[index] == MailBnfHelper.CR || flag)
					{
						throw new FormatException(SR.GetString("MailAddressInvalidFormat"));
					}
					if (data[index] == MailBnfHelper.LF)
					{
						flag = true;
					}
					else if (data[index] != MailBnfHelper.Space && data[index] != MailBnfHelper.Tab)
					{
						break;
					}
				}
				index--;
			}
			if (flag)
			{
				throw new FormatException(SR.GetString("MailAddressInvalidFormat"));
			}
			return index;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0007D944 File Offset: 0x0007BB44
		internal static int ReadCfwsReverse(string data, int index)
		{
			int num = 0;
			for (index = WhitespaceReader.ReadFwsReverse(data, index); index >= 0; index = WhitespaceReader.ReadFwsReverse(data, index))
			{
				int num2 = QuotedPairReader.CountQuotedChars(data, index, true);
				if (num > 0 && num2 > 0)
				{
					index -= num2;
				}
				else if (data[index] == MailBnfHelper.EndComment)
				{
					num++;
					index--;
				}
				else if (data[index] == MailBnfHelper.StartComment)
				{
					num--;
					if (num < 0)
					{
						throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[] { MailBnfHelper.StartComment }));
					}
					index--;
				}
				else if (num > 0 && ((int)data[index] > MailBnfHelper.Ascii7bitMaxValue || MailBnfHelper.Ctext[(int)data[index]]))
				{
					index--;
				}
				else
				{
					if (num > 0)
					{
						throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[] { data[index] }));
					}
					break;
				}
			}
			if (num > 0)
			{
				throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[] { MailBnfHelper.EndComment }));
			}
			return index;
		}
	}
}
