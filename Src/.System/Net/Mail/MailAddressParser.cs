using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x0200026C RID: 620
	internal static class MailAddressParser
	{
		// Token: 0x06001740 RID: 5952 RVA: 0x00076BB4 File Offset: 0x00074DB4
		internal static MailAddress ParseAddress(string data)
		{
			int num = data.Length - 1;
			return MailAddressParser.ParseAddress(data, false, ref num);
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00076BD8 File Offset: 0x00074DD8
		internal static IList<MailAddress> ParseMultipleAddresses(string data)
		{
			IList<MailAddress> list = new List<MailAddress>();
			for (int i = data.Length - 1; i >= 0; i--)
			{
				list.Insert(0, MailAddressParser.ParseAddress(data, true, ref i));
			}
			return list;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00076C10 File Offset: 0x00074E10
		private static MailAddress ParseAddress(string data, bool expectMultipleAddresses, ref int index)
		{
			index = MailAddressParser.ReadCfwsAndThrowIfIncomplete(data, index);
			bool flag = false;
			if (data[index] == MailBnfHelper.EndAngleBracket)
			{
				flag = true;
				index--;
			}
			string text = MailAddressParser.ParseDomain(data, ref index);
			if (data[index] != MailBnfHelper.At)
			{
				throw new FormatException(SR.GetString("MailAddressInvalidFormat"));
			}
			index--;
			string text2 = MailAddressParser.ParseLocalPart(data, ref index, flag, expectMultipleAddresses);
			if (flag)
			{
				if (index < 0 || data[index] != MailBnfHelper.StartAngleBracket)
				{
					throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[] { (index >= 0) ? data[index] : MailBnfHelper.EndAngleBracket }));
				}
				index--;
				index = WhitespaceReader.ReadFwsReverse(data, index);
			}
			string text3;
			if (index >= 0 && (!expectMultipleAddresses || data[index] != MailBnfHelper.Comma))
			{
				text3 = MailAddressParser.ParseDisplayName(data, ref index, expectMultipleAddresses);
			}
			else
			{
				text3 = string.Empty;
			}
			return new MailAddress(text3, text2, text);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00076D08 File Offset: 0x00074F08
		private static int ReadCfwsAndThrowIfIncomplete(string data, int index)
		{
			index = WhitespaceReader.ReadCfwsReverse(data, index);
			if (index < 0)
			{
				throw new FormatException(SR.GetString("MailAddressInvalidFormat"));
			}
			return index;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00076D28 File Offset: 0x00074F28
		private static string ParseDomain(string data, ref int index)
		{
			index = MailAddressParser.ReadCfwsAndThrowIfIncomplete(data, index);
			int num = index;
			if (data[index] == MailBnfHelper.EndSquareBracket)
			{
				index = DomainLiteralReader.ReadReverse(data, index);
			}
			else
			{
				index = DotAtomReader.ReadReverse(data, index);
			}
			string text = data.Substring(index + 1, num - index);
			index = MailAddressParser.ReadCfwsAndThrowIfIncomplete(data, index);
			return MailAddressParser.NormalizeOrThrow(text);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00076D88 File Offset: 0x00074F88
		private static string ParseLocalPart(string data, ref int index, bool expectAngleBracket, bool expectMultipleAddresses)
		{
			index = MailAddressParser.ReadCfwsAndThrowIfIncomplete(data, index);
			int num = index;
			if (data[index] == MailBnfHelper.Quote)
			{
				index = QuotedStringFormatReader.ReadReverseQuoted(data, index, true);
			}
			else
			{
				index = DotAtomReader.ReadReverse(data, index);
				if (index >= 0 && !MailBnfHelper.Whitespace.Contains(data[index]) && data[index] != MailBnfHelper.EndComment && (!expectAngleBracket || data[index] != MailBnfHelper.StartAngleBracket) && (!expectMultipleAddresses || data[index] != MailBnfHelper.Comma) && data[index] != MailBnfHelper.Quote)
				{
					throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[] { data[index] }));
				}
			}
			string text = data.Substring(index + 1, num - index);
			index = WhitespaceReader.ReadCfwsReverse(data, index);
			return MailAddressParser.NormalizeOrThrow(text);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00076E6C File Offset: 0x0007506C
		private static string ParseDisplayName(string data, ref int index, bool expectMultipleAddresses)
		{
			int num = WhitespaceReader.ReadCfwsReverse(data, index);
			string text;
			if (num >= 0 && data[num] == MailBnfHelper.Quote)
			{
				index = QuotedStringFormatReader.ReadReverseQuoted(data, num, true);
				int num2 = index + 2;
				text = data.Substring(num2, num - num2);
				index = WhitespaceReader.ReadCfwsReverse(data, index);
				if (index >= 0 && (!expectMultipleAddresses || data[index] != MailBnfHelper.Comma))
				{
					throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[] { data[index] }));
				}
			}
			else
			{
				int num3 = index;
				index = QuotedStringFormatReader.ReadReverseUnQuoted(data, index, true, expectMultipleAddresses);
				text = data.Substring(index + 1, num3 - index);
				text = text.Trim();
			}
			return MailAddressParser.NormalizeOrThrow(text);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00076F20 File Offset: 0x00075120
		internal static string NormalizeOrThrow(string input)
		{
			string text;
			try
			{
				text = input.Normalize(NormalizationForm.FormC);
			}
			catch (ArgumentException ex)
			{
				throw new FormatException(SR.GetString("MailAddressInvalidFormat"), ex);
			}
			return text;
		}
	}
}
