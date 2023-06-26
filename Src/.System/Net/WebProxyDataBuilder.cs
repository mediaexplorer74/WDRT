using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x020001E3 RID: 483
	internal abstract class WebProxyDataBuilder
	{
		// Token: 0x060012C2 RID: 4802 RVA: 0x0006362C File Offset: 0x0006182C
		public WebProxyData Build()
		{
			this.m_Result = new WebProxyData();
			this.BuildInternal();
			return this.m_Result;
		}

		// Token: 0x060012C3 RID: 4803
		protected abstract void BuildInternal();

		// Token: 0x060012C4 RID: 4804 RVA: 0x00063648 File Offset: 0x00061848
		protected void SetProxyAndBypassList(string addressString, string bypassListString)
		{
			if (addressString != null)
			{
				addressString = addressString.Trim();
				if (addressString != string.Empty)
				{
					if (addressString.IndexOf('=') == -1)
					{
						this.m_Result.proxyAddress = WebProxyDataBuilder.ParseProxyUri(addressString);
					}
					else
					{
						this.m_Result.proxyHostAddresses = WebProxyDataBuilder.ParseProtocolProxies(addressString);
					}
					if (bypassListString != null)
					{
						bypassListString = bypassListString.Trim();
						if (bypassListString != string.Empty)
						{
							bool flag = false;
							this.m_Result.bypassList = WebProxyDataBuilder.ParseBypassList(bypassListString, out flag);
							this.m_Result.bypassOnLocal = flag;
						}
					}
				}
			}
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x000636D8 File Offset: 0x000618D8
		protected void SetAutoProxyUrl(string autoConfigUrl)
		{
			if (!string.IsNullOrEmpty(autoConfigUrl))
			{
				Uri uri = null;
				if (Uri.TryCreate(autoConfigUrl, UriKind.Absolute, out uri))
				{
					this.m_Result.scriptLocation = uri;
				}
			}
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00063706 File Offset: 0x00061906
		protected void SetAutoDetectSettings(bool value)
		{
			this.m_Result.automaticallyDetectSettings = value;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00063714 File Offset: 0x00061914
		private static Uri ParseProxyUri(string proxyString)
		{
			if (proxyString.IndexOf("://") == -1)
			{
				proxyString = "http://" + proxyString;
			}
			Uri uri;
			try
			{
				uri = new Uri(proxyString);
			}
			catch (UriFormatException ex)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, ex.Message);
				}
				throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyString);
			}
			return uri;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00063778 File Offset: 0x00061978
		private static Hashtable ParseProtocolProxies(string proxyListString)
		{
			string[] array = proxyListString.Split(new char[] { ';' });
			Hashtable hashtable = new Hashtable(CaseInsensitiveAscii.StaticInstance);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (!(text == string.Empty))
				{
					string[] array2 = text.Split(new char[] { '=' });
					if (array2.Length != 2)
					{
						throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyListString);
					}
					array2[0] = array2[0].Trim();
					array2[1] = array2[1].Trim();
					if (array2[0] == string.Empty || array2[1] == string.Empty)
					{
						throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyListString);
					}
					hashtable[array2[0]] = WebProxyDataBuilder.ParseProxyUri(array2[1]);
				}
			}
			return hashtable;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00063844 File Offset: 0x00061A44
		private static FormatException CreateInvalidProxyStringException(string originalProxyString)
		{
			string @string = SR.GetString("net_proxy_invalid_url_format", new object[] { originalProxyString });
			if (Logging.On)
			{
				Logging.PrintError(Logging.Web, @string);
			}
			return new FormatException(@string);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00063880 File Offset: 0x00061A80
		private static string BypassStringEscape(string rawString)
		{
			Regex regex = new Regex("^(?<scheme>.*://)?(?<host>[^:]*)(?<port>:[0-9]{1,5})?$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
			Match match = regex.Match(rawString);
			string text;
			string text2;
			string text3;
			if (match.Success)
			{
				text = match.Groups["scheme"].Value;
				text2 = match.Groups["host"].Value;
				text3 = match.Groups["port"].Value;
			}
			else
			{
				text = string.Empty;
				text2 = rawString;
				text3 = string.Empty;
			}
			text = WebProxyDataBuilder.ConvertRegexReservedChars(text);
			text2 = WebProxyDataBuilder.ConvertRegexReservedChars(text2);
			text3 = WebProxyDataBuilder.ConvertRegexReservedChars(text3);
			if (text == string.Empty)
			{
				text = "(?:.*://)?";
			}
			if (text3 == string.Empty)
			{
				text3 = "(?::[0-9]{1,5})?";
			}
			return string.Concat(new string[] { "^", text, text2, text3, "$" });
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00063968 File Offset: 0x00061B68
		private static string ConvertRegexReservedChars(string rawString)
		{
			if (rawString.Length == 0)
			{
				return rawString;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in rawString)
			{
				if ("#$()+.?[\\^{|".IndexOf(c) != -1)
				{
					stringBuilder.Append('\\');
				}
				else if (c == '*')
				{
					stringBuilder.Append('.');
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x000639D8 File Offset: 0x00061BD8
		private static ArrayList ParseBypassList(string bypassListString, out bool bypassOnLocal)
		{
			string[] array = bypassListString.Split(new char[] { ';' });
			bypassOnLocal = false;
			if (array.Length == 0)
			{
				return null;
			}
			ArrayList arrayList = null;
			foreach (string text in array)
			{
				if (text != null)
				{
					string text2 = text.Trim();
					if (text2.Length > 0)
					{
						if (string.Compare(text2, "<local>", StringComparison.OrdinalIgnoreCase) == 0)
						{
							bypassOnLocal = true;
						}
						else
						{
							text2 = WebProxyDataBuilder.BypassStringEscape(text2);
							if (arrayList == null)
							{
								arrayList = new ArrayList();
							}
							if (!arrayList.Contains(text2))
							{
								arrayList.Add(text2);
							}
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x0400150E RID: 5390
		private const char addressListDelimiter = ';';

		// Token: 0x0400150F RID: 5391
		private const char addressListSchemeValueDelimiter = '=';

		// Token: 0x04001510 RID: 5392
		private const char bypassListDelimiter = ';';

		// Token: 0x04001511 RID: 5393
		private WebProxyData m_Result;

		// Token: 0x04001512 RID: 5394
		private const string regexReserved = "#$()+.?[\\^{|";
	}
}
