using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace Microsoft.WindowsAzure.Storage.Core.Util
{
	// Token: 0x02000098 RID: 152
	internal static class HttpWebUtility
	{
		// Token: 0x06000FFC RID: 4092 RVA: 0x0003CC08 File Offset: 0x0003AE08
		public static IDictionary<string, string> ParseQueryString(string query)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (string.IsNullOrEmpty(query))
			{
				return dictionary;
			}
			if (query.StartsWith("?", StringComparison.Ordinal))
			{
				if (query.Length == 1)
				{
					return dictionary;
				}
				query = query.Substring(1);
			}
			string[] array = query.Split(new char[] { '&' });
			foreach (string text in array)
			{
				int num = text.IndexOf("=", StringComparison.Ordinal);
				string text2;
				string text3;
				if (num < 0)
				{
					text2 = string.Empty;
					text3 = Uri.UnescapeDataString(text);
				}
				else
				{
					text2 = Uri.UnescapeDataString(text.Substring(0, num));
					text3 = Uri.UnescapeDataString(text.Substring(num + 1));
				}
				string text4;
				if (dictionary.TryGetValue(text2, out text4))
				{
					dictionary[text2] = text4 + "," + text3;
				}
				else
				{
					dictionary[text2] = text3;
				}
			}
			return dictionary;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0003CCEC File Offset: 0x0003AEEC
		public static string ConvertDateTimeToHttpString(DateTimeOffset dateTime)
		{
			return dateTime.UtcDateTime.ToString("R", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0003CD14 File Offset: 0x0003AF14
		public static string TryGetHeader(HttpWebResponse resp, string headerName, string defaultValue)
		{
			string text = resp.Headers[headerName];
			if (string.IsNullOrEmpty(text))
			{
				return defaultValue;
			}
			return text;
		}
	}
}
