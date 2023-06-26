using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Configuration;
using System.Text;

namespace System.Net
{
	// Token: 0x020000FF RID: 255
	internal sealed class HttpListenerRequestUriBuilder
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x000341D4 File Offset: 0x000323D4
		private HttpListenerRequestUriBuilder(string rawUri, string cookedUriScheme, string cookedUriHost, string cookedUriPath, string cookedUriQuery)
		{
			this.rawUri = rawUri;
			this.cookedUriScheme = cookedUriScheme;
			this.cookedUriHost = cookedUriHost;
			this.cookedUriPath = HttpListenerRequestUriBuilder.AddSlashToAsteriskOnlyPath(cookedUriPath);
			if (cookedUriQuery == null)
			{
				this.cookedUriQuery = string.Empty;
				return;
			}
			this.cookedUriQuery = cookedUriQuery;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00034224 File Offset: 0x00032424
		public static Uri GetRequestUri(string rawUri, string cookedUriScheme, string cookedUriHost, string cookedUriPath, string cookedUriQuery)
		{
			HttpListenerRequestUriBuilder httpListenerRequestUriBuilder = new HttpListenerRequestUriBuilder(rawUri, cookedUriScheme, cookedUriHost, cookedUriPath, cookedUriQuery);
			return httpListenerRequestUriBuilder.Build();
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00034244 File Offset: 0x00032444
		private Uri Build()
		{
			if (HttpListenerRequestUriBuilder.useCookedRequestUrl)
			{
				this.BuildRequestUriUsingCookedPath();
				if (this.requestUri == null)
				{
					this.BuildRequestUriUsingRawPath();
				}
			}
			else
			{
				this.BuildRequestUriUsingRawPath();
				if (this.requestUri == null)
				{
					this.BuildRequestUriUsingCookedPath();
				}
			}
			return this.requestUri;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00034294 File Offset: 0x00032494
		private void BuildRequestUriUsingCookedPath()
		{
			if (!Uri.TryCreate(string.Concat(new string[]
			{
				this.cookedUriScheme,
				Uri.SchemeDelimiter,
				this.cookedUriHost,
				this.cookedUriPath,
				this.cookedUriQuery
			}), UriKind.Absolute, out this.requestUri))
			{
				this.LogWarning("BuildRequestUriUsingCookedPath", "net_log_listener_cant_create_uri", new object[] { this.cookedUriScheme, this.cookedUriHost, this.cookedUriPath, this.cookedUriQuery });
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00034324 File Offset: 0x00032524
		private void BuildRequestUriUsingRawPath()
		{
			this.rawPath = HttpListenerRequestUriBuilder.GetPath(this.rawUri);
			bool flag;
			if (!HttpSysSettings.EnableNonUtf8 || this.rawPath == string.Empty)
			{
				string text = this.rawPath;
				if (text == string.Empty)
				{
					text = "/";
				}
				flag = Uri.TryCreate(string.Concat(new string[]
				{
					this.cookedUriScheme,
					Uri.SchemeDelimiter,
					this.cookedUriHost,
					text,
					this.cookedUriQuery
				}), UriKind.Absolute, out this.requestUri);
			}
			else
			{
				HttpListenerRequestUriBuilder.ParsingResult parsingResult = this.BuildRequestUriUsingRawPath(HttpListenerRequestUriBuilder.GetEncoding(HttpListenerRequestUriBuilder.EncodingType.Primary));
				if (parsingResult == HttpListenerRequestUriBuilder.ParsingResult.EncodingError)
				{
					Encoding encoding = HttpListenerRequestUriBuilder.GetEncoding(HttpListenerRequestUriBuilder.EncodingType.Secondary);
					parsingResult = this.BuildRequestUriUsingRawPath(encoding);
				}
				flag = parsingResult == HttpListenerRequestUriBuilder.ParsingResult.Success;
			}
			if (!flag)
			{
				this.LogWarning("BuildRequestUriUsingRawPath", "net_log_listener_cant_create_uri", new object[] { this.cookedUriScheme, this.cookedUriHost, this.rawPath, this.cookedUriQuery });
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0003441D File Offset: 0x0003261D
		private static Encoding GetEncoding(HttpListenerRequestUriBuilder.EncodingType type)
		{
			if ((type == HttpListenerRequestUriBuilder.EncodingType.Primary && !HttpSysSettings.FavorUtf8) || (type == HttpListenerRequestUriBuilder.EncodingType.Secondary && HttpSysSettings.FavorUtf8))
			{
				return HttpListenerRequestUriBuilder.ansiEncoding;
			}
			return HttpListenerRequestUriBuilder.utf8Encoding;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00034440 File Offset: 0x00032640
		private HttpListenerRequestUriBuilder.ParsingResult BuildRequestUriUsingRawPath(Encoding encoding)
		{
			this.rawOctets = new List<byte>();
			this.requestUriString = new StringBuilder();
			this.requestUriString.Append(this.cookedUriScheme);
			this.requestUriString.Append(Uri.SchemeDelimiter);
			this.requestUriString.Append(this.cookedUriHost);
			HttpListenerRequestUriBuilder.ParsingResult parsingResult = this.ParseRawPath(encoding);
			if (parsingResult == HttpListenerRequestUriBuilder.ParsingResult.Success)
			{
				this.requestUriString.Append(this.cookedUriQuery);
				if (!Uri.TryCreate(this.requestUriString.ToString(), UriKind.Absolute, out this.requestUri))
				{
					parsingResult = HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
				}
			}
			if (parsingResult != HttpListenerRequestUriBuilder.ParsingResult.Success)
			{
				this.LogWarning("BuildRequestUriUsingRawPath", "net_log_listener_cant_convert_raw_path", new object[] { this.rawPath, encoding.EncodingName });
			}
			return parsingResult;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000344FC File Offset: 0x000326FC
		private HttpListenerRequestUriBuilder.ParsingResult ParseRawPath(Encoding encoding)
		{
			int i = 0;
			while (i < this.rawPath.Length)
			{
				char c = this.rawPath[i];
				if (c == '%')
				{
					i++;
					c = this.rawPath[i];
					if (c == 'u' || c == 'U')
					{
						if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
						}
						if (!this.AppendUnicodeCodePointValuePercentEncoded(this.rawPath.Substring(i + 1, 4)))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
						}
						i += 5;
					}
					else
					{
						if (!this.AddPercentEncodedOctetToRawOctetsList(encoding, this.rawPath.Substring(i, 2)))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
						}
						i += 2;
					}
				}
				else
				{
					if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
					{
						return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
					}
					this.requestUriString.Append(c);
					i++;
				}
			}
			if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
			{
				return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
			}
			return HttpListenerRequestUriBuilder.ParsingResult.Success;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000345C0 File Offset: 0x000327C0
		private bool AppendUnicodeCodePointValuePercentEncoded(string codePoint)
		{
			int num;
			if (!int.TryParse(codePoint, NumberStyles.HexNumber, null, out num))
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "net_log_listener_cant_convert_percent_value", new object[] { codePoint });
				return false;
			}
			string text = null;
			try
			{
				text = char.ConvertFromUtf32(num);
				HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, HttpListenerRequestUriBuilder.utf8Encoding.GetBytes(text));
				return true;
			}
			catch (ArgumentOutOfRangeException)
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "net_log_listener_cant_convert_percent_value", new object[] { codePoint });
			}
			catch (EncoderFallbackException ex)
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "net_log_listener_cant_convert_to_utf8", new object[] { text, ex.Message });
			}
			return false;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00034680 File Offset: 0x00032880
		private bool AddPercentEncodedOctetToRawOctetsList(Encoding encoding, string escapedCharacter)
		{
			byte b;
			if (!byte.TryParse(escapedCharacter, NumberStyles.HexNumber, null, out b))
			{
				this.LogWarning("AddPercentEncodedOctetToRawOctetsList", "net_log_listener_cant_convert_percent_value", new object[] { escapedCharacter });
				return false;
			}
			this.rawOctets.Add(b);
			return true;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000346C8 File Offset: 0x000328C8
		private bool EmptyDecodeAndAppendRawOctetsList(Encoding encoding)
		{
			if (this.rawOctets.Count == 0)
			{
				return true;
			}
			string text = null;
			try
			{
				text = encoding.GetString(this.rawOctets.ToArray());
				if (encoding == HttpListenerRequestUriBuilder.utf8Encoding)
				{
					HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, this.rawOctets.ToArray());
				}
				else
				{
					HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, HttpListenerRequestUriBuilder.utf8Encoding.GetBytes(text));
				}
				this.rawOctets.Clear();
				return true;
			}
			catch (DecoderFallbackException ex)
			{
				this.LogWarning("EmptyDecodeAndAppendRawOctetsList", "net_log_listener_cant_convert_bytes", new object[]
				{
					HttpListenerRequestUriBuilder.GetOctetsAsString(this.rawOctets),
					ex.Message
				});
			}
			catch (EncoderFallbackException ex2)
			{
				this.LogWarning("EmptyDecodeAndAppendRawOctetsList", "net_log_listener_cant_convert_to_utf8", new object[] { text, ex2.Message });
			}
			return false;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000347B4 File Offset: 0x000329B4
		private static void AppendOctetsPercentEncoded(StringBuilder target, IEnumerable<byte> octets)
		{
			foreach (byte b in octets)
			{
				target.Append('%');
				target.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00034818 File Offset: 0x00032A18
		private static string GetOctetsAsString(IEnumerable<byte> octets)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (byte b in octets)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00034894 File Offset: 0x00032A94
		private static string GetPath(string uriString)
		{
			int num = 0;
			if (uriString[0] != '/')
			{
				int num2 = 0;
				if (uriString.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
				{
					num2 = 7;
				}
				else if (uriString.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
				{
					num2 = 8;
				}
				if (num2 > 0)
				{
					num = uriString.IndexOf('/', num2);
					if (num == -1)
					{
						num = uriString.Length;
					}
				}
				else
				{
					uriString = "/" + uriString;
				}
			}
			int num3 = uriString.IndexOf('?');
			if (num3 == -1)
			{
				num3 = uriString.Length;
			}
			return HttpListenerRequestUriBuilder.AddSlashToAsteriskOnlyPath(uriString.Substring(num, num3 - num));
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0003491D File Offset: 0x00032B1D
		private static string AddSlashToAsteriskOnlyPath(string path)
		{
			if (path.Length == 1 && path[0] == '*')
			{
				return "/*";
			}
			return path;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0003493A File Offset: 0x00032B3A
		private void LogWarning(string methodName, string message, params object[] args)
		{
			if (Logging.On)
			{
				Logging.PrintWarning(Logging.HttpListener, this, methodName, SR.GetString(message, args));
			}
		}

		// Token: 0x04000E2C RID: 3628
		private static readonly bool useCookedRequestUrl = SettingsSectionInternal.Section.HttpListenerUnescapeRequestUrl;

		// Token: 0x04000E2D RID: 3629
		private static readonly Encoding utf8Encoding = new UTF8Encoding(false, true);

		// Token: 0x04000E2E RID: 3630
		private static readonly Encoding ansiEncoding = Encoding.GetEncoding(0, new EncoderExceptionFallback(), new DecoderExceptionFallback());

		// Token: 0x04000E2F RID: 3631
		private readonly string rawUri;

		// Token: 0x04000E30 RID: 3632
		private readonly string cookedUriScheme;

		// Token: 0x04000E31 RID: 3633
		private readonly string cookedUriHost;

		// Token: 0x04000E32 RID: 3634
		private readonly string cookedUriPath;

		// Token: 0x04000E33 RID: 3635
		private readonly string cookedUriQuery;

		// Token: 0x04000E34 RID: 3636
		private StringBuilder requestUriString;

		// Token: 0x04000E35 RID: 3637
		private List<byte> rawOctets;

		// Token: 0x04000E36 RID: 3638
		private string rawPath;

		// Token: 0x04000E37 RID: 3639
		private Uri requestUri;

		// Token: 0x02000703 RID: 1795
		private enum ParsingResult
		{
			// Token: 0x040030B9 RID: 12473
			Success,
			// Token: 0x040030BA RID: 12474
			InvalidString,
			// Token: 0x040030BB RID: 12475
			EncodingError
		}

		// Token: 0x02000704 RID: 1796
		private enum EncodingType
		{
			// Token: 0x040030BD RID: 12477
			Primary,
			// Token: 0x040030BE RID: 12478
			Secondary
		}
	}
}
