using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x020001B5 RID: 437
	internal class HeaderInfoTable
	{
		// Token: 0x0600112D RID: 4397 RVA: 0x0005D6DF File Offset: 0x0005B8DF
		private static string[] ParseSingleValue(string value)
		{
			return new string[] { value };
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0005D6EB File Offset: 0x0005B8EB
		private static string[] ParseMultiValue(string value)
		{
			return HeaderInfoTable.ParseValueHelper(value, false);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0005D6F4 File Offset: 0x0005B8F4
		private static string[] ParseSetCookieValue(string value)
		{
			return HeaderInfoTable.ParseValueHelper(value, true);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0005D700 File Offset: 0x0005B900
		private static string[] ParseValueHelper(string value, bool isSetCookie)
		{
			StringCollection stringCollection = new StringCollection();
			bool flag = false;
			int num = 0;
			char[] array = new char[value.Length];
			int i = 0;
			while (i < value.Length)
			{
				if (value[i] == '"')
				{
					flag = !flag;
					goto IL_65;
				}
				if (value[i] != ',' || flag)
				{
					goto IL_65;
				}
				string text = new string(array, 0, num);
				if (isSetCookie && HeaderInfoTable.IsDuringExpiresAttributeParsing(text))
				{
					goto IL_65;
				}
				stringCollection.Add(text.Trim());
				num = 0;
				IL_74:
				i++;
				continue;
				IL_65:
				array[num++] = value[i];
				goto IL_74;
			}
			if (num != 0)
			{
				string text = new string(array, 0, num);
				stringCollection.Add(text.Trim());
			}
			string[] array2 = new string[stringCollection.Count];
			stringCollection.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0005D7C4 File Offset: 0x0005B9C4
		private static bool IsDuringExpiresAttributeParsing(string singleValue)
		{
			string[] array = singleValue.Split(new char[] { ';' });
			string text = array[array.Length - 1].Trim();
			bool flag = text.IndexOf(',') < 0;
			string text2 = text.Split(new char[] { '=' })[0].Trim();
			bool flag2 = text2.IndexOf("Expires", StringComparison.OrdinalIgnoreCase) >= 0 && text2.Length == 7;
			return flag2 && flag;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0005D838 File Offset: 0x0005BA38
		static HeaderInfoTable()
		{
			HeaderInfo[] array = new HeaderInfo[]
			{
				new HeaderInfo("Age", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Allow", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Accept", true, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Authorization", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Accept-Ranges", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Accept-Charset", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Accept-Encoding", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Accept-Language", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Cookie", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Connection", true, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Content-MD5", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Content-Type", true, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Cache-Control", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Content-Range", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Content-Length", true, true, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Content-Encoding", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Content-Language", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Content-Location", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Date", true, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("ETag", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Expect", true, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Expires", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("From", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Host", true, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("If-Match", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("If-Range", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("If-None-Match", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("If-Modified-Since", true, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("If-Unmodified-Since", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Keep-Alive", false, true, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Location", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Last-Modified", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Max-Forwards", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Pragma", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Proxy-Authenticate", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Proxy-Authorization", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Proxy-Connection", true, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Range", true, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Referer", true, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Retry-After", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Server", false, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Set-Cookie", false, false, true, HeaderInfoTable.SetCookieParser),
				new HeaderInfo("Set-Cookie2", false, false, true, HeaderInfoTable.SetCookieParser),
				new HeaderInfo("TE", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Trailer", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Transfer-Encoding", true, true, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Upgrade", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("User-Agent", true, false, false, HeaderInfoTable.SingleParser),
				new HeaderInfo("Via", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Vary", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("Warning", false, false, true, HeaderInfoTable.MultiParser),
				new HeaderInfo("WWW-Authenticate", false, true, true, HeaderInfoTable.SingleParser)
			};
			HeaderInfoTable.HeaderHashTable = new Hashtable(array.Length * 2, CaseInsensitiveAscii.StaticInstance);
			for (int i = 0; i < array.Length; i++)
			{
				HeaderInfoTable.HeaderHashTable[array[i].HeaderName] = array[i];
			}
		}

		// Token: 0x170003B8 RID: 952
		internal HeaderInfo this[string name]
		{
			get
			{
				HeaderInfo headerInfo = (HeaderInfo)HeaderInfoTable.HeaderHashTable[name];
				if (headerInfo == null)
				{
					return HeaderInfoTable.UnknownHeaderInfo;
				}
				return headerInfo;
			}
		}

		// Token: 0x04001403 RID: 5123
		private static Hashtable HeaderHashTable;

		// Token: 0x04001404 RID: 5124
		private static HeaderInfo UnknownHeaderInfo = new HeaderInfo(string.Empty, false, false, false, HeaderInfoTable.SingleParser);

		// Token: 0x04001405 RID: 5125
		private static HeaderParser SingleParser = new HeaderParser(HeaderInfoTable.ParseSingleValue);

		// Token: 0x04001406 RID: 5126
		private static HeaderParser MultiParser = new HeaderParser(HeaderInfoTable.ParseMultiValue);

		// Token: 0x04001407 RID: 5127
		private static HeaderParser SetCookieParser = new HeaderParser(HeaderInfoTable.ParseSetCookieValue);
	}
}
