using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020000D4 RID: 212
	internal class CookieParser
	{
		// Token: 0x0600072D RID: 1837 RVA: 0x0002784C File Offset: 0x00025A4C
		internal CookieParser(string cookieString)
		{
			this.m_tokenizer = new CookieTokenizer(cookieString);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00027860 File Offset: 0x00025A60
		internal Cookie Get()
		{
			Cookie cookie = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			do
			{
				CookieToken cookieToken = this.m_tokenizer.Next(cookie == null, true);
				if (cookie == null && (cookieToken == CookieToken.NameValuePair || cookieToken == CookieToken.Attribute))
				{
					cookie = new Cookie();
					if (!cookie.InternalSetName(this.m_tokenizer.Name))
					{
						cookie.InternalSetName(string.Empty);
					}
					cookie.Value = this.m_tokenizer.Value;
				}
				else if (cookieToken != CookieToken.NameValuePair)
				{
					if (cookieToken == CookieToken.Attribute)
					{
						CookieToken token = this.m_tokenizer.Token;
						if (token != CookieToken.Discard)
						{
							switch (token)
							{
							case CookieToken.Port:
								if (!flag6)
								{
									flag6 = true;
									cookie.Port = string.Empty;
								}
								break;
							case CookieToken.Secure:
								if (!flag8)
								{
									flag8 = true;
									cookie.Secure = true;
								}
								break;
							case CookieToken.HttpOnly:
								cookie.HttpOnly = true;
								break;
							}
						}
						else if (!flag9)
						{
							flag9 = true;
							cookie.Discard = true;
						}
					}
				}
				else
				{
					switch (this.m_tokenizer.Token)
					{
					case CookieToken.Comment:
						if (!flag)
						{
							flag = true;
							cookie.Comment = this.m_tokenizer.Value;
							goto IL_2F4;
						}
						goto IL_2F4;
					case CookieToken.CommentUrl:
					{
						if (flag2)
						{
							goto IL_2F4;
						}
						flag2 = true;
						Uri uri;
						if (Uri.TryCreate(CookieParser.CheckQuoted(this.m_tokenizer.Value), UriKind.Absolute, out uri))
						{
							cookie.CommentUri = uri;
							goto IL_2F4;
						}
						goto IL_2F4;
					}
					case CookieToken.CookieName:
					case CookieToken.Discard:
					case CookieToken.Secure:
					case CookieToken.HttpOnly:
					case CookieToken.Unknown:
						goto IL_2F4;
					case CookieToken.Domain:
						if (!flag3)
						{
							flag3 = true;
							cookie.Domain = CookieParser.CheckQuoted(this.m_tokenizer.Value);
							cookie.IsQuotedDomain = this.m_tokenizer.Quoted;
							goto IL_2F4;
						}
						goto IL_2F4;
					case CookieToken.Expires:
					{
						if (flag4)
						{
							goto IL_2F4;
						}
						flag4 = true;
						DateTime dateTime;
						if (DateTime.TryParse(CookieParser.CheckQuoted(this.m_tokenizer.Value), CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dateTime))
						{
							cookie.Expires = dateTime;
							goto IL_2F4;
						}
						cookie.InternalSetName(string.Empty);
						goto IL_2F4;
					}
					case CookieToken.MaxAge:
					{
						if (flag4)
						{
							goto IL_2F4;
						}
						flag4 = true;
						int num;
						if (int.TryParse(CookieParser.CheckQuoted(this.m_tokenizer.Value), out num))
						{
							cookie.Expires = DateTime.Now.AddSeconds((double)num);
							goto IL_2F4;
						}
						cookie.InternalSetName(string.Empty);
						goto IL_2F4;
					}
					case CookieToken.Path:
						if (!flag5)
						{
							flag5 = true;
							cookie.Path = this.m_tokenizer.Value;
							goto IL_2F4;
						}
						goto IL_2F4;
					case CookieToken.Port:
						if (flag6)
						{
							goto IL_2F4;
						}
						flag6 = true;
						try
						{
							cookie.Port = this.m_tokenizer.Value;
							goto IL_2F4;
						}
						catch
						{
							cookie.InternalSetName(string.Empty);
							goto IL_2F4;
						}
						break;
					case CookieToken.Version:
						break;
					default:
						goto IL_2F4;
					}
					if (!flag7)
					{
						flag7 = true;
						int num2;
						if (int.TryParse(CookieParser.CheckQuoted(this.m_tokenizer.Value), out num2))
						{
							cookie.Version = num2;
							cookie.IsQuotedVersion = this.m_tokenizer.Quoted;
						}
						else
						{
							cookie.InternalSetName(string.Empty);
						}
					}
				}
				IL_2F4:;
			}
			while (!this.m_tokenizer.Eof && !this.m_tokenizer.EndOfCookie);
			return cookie;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00027B90 File Offset: 0x00025D90
		internal Cookie GetServer()
		{
			Cookie cookie = this.m_savedCookie;
			this.m_savedCookie = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (;;)
			{
				bool flag4 = cookie == null || cookie.Name == null || cookie.Name.Length == 0;
				CookieToken cookieToken = this.m_tokenizer.Next(flag4, false);
				if (flag4 && (cookieToken == CookieToken.NameValuePair || cookieToken == CookieToken.Attribute))
				{
					if (cookie == null)
					{
						cookie = new Cookie();
					}
					if (!cookie.InternalSetName(this.m_tokenizer.Name))
					{
						cookie.InternalSetName(string.Empty);
					}
					cookie.Value = this.m_tokenizer.Value;
				}
				else if (cookieToken != CookieToken.NameValuePair)
				{
					if (cookieToken == CookieToken.Attribute)
					{
						CookieToken token = this.m_tokenizer.Token;
						if (token == CookieToken.Port && !flag3)
						{
							flag3 = true;
							cookie.Port = string.Empty;
						}
					}
				}
				else
				{
					switch (this.m_tokenizer.Token)
					{
					case CookieToken.Domain:
						if (!flag)
						{
							flag = true;
							cookie.Domain = CookieParser.CheckQuoted(this.m_tokenizer.Value);
							cookie.IsQuotedDomain = this.m_tokenizer.Quoted;
						}
						break;
					case CookieToken.Path:
						if (!flag2)
						{
							flag2 = true;
							cookie.Path = this.m_tokenizer.Value;
						}
						break;
					case CookieToken.Port:
						if (!flag3)
						{
							flag3 = true;
							try
							{
								cookie.Port = this.m_tokenizer.Value;
								break;
							}
							catch (CookieException)
							{
								cookie.InternalSetName(string.Empty);
								break;
							}
							goto IL_162;
						}
						break;
					case CookieToken.Unknown:
						goto IL_190;
					case CookieToken.Version:
						goto IL_162;
					}
				}
				if (this.m_tokenizer.Eof || this.m_tokenizer.EndOfCookie)
				{
					return cookie;
				}
			}
			IL_162:
			this.m_savedCookie = new Cookie();
			int num;
			if (int.TryParse(this.m_tokenizer.Value, out num))
			{
				this.m_savedCookie.Version = num;
			}
			return cookie;
			IL_190:
			this.m_savedCookie = new Cookie();
			if (!this.m_savedCookie.InternalSetName(this.m_tokenizer.Name))
			{
				this.m_savedCookie.InternalSetName(string.Empty);
			}
			this.m_savedCookie.Value = this.m_tokenizer.Value;
			return cookie;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00027DCC File Offset: 0x00025FCC
		internal static string CheckQuoted(string value)
		{
			if (value.Length < 2 || value[0] != '"' || value[value.Length - 1] != '"')
			{
				return value;
			}
			if (value.Length != 2)
			{
				return value.Substring(1, value.Length - 2);
			}
			return string.Empty;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00027E1F File Offset: 0x0002601F
		internal bool EndofHeader()
		{
			return this.m_tokenizer.Eof;
		}

		// Token: 0x04000D00 RID: 3328
		private CookieTokenizer m_tokenizer;

		// Token: 0x04000D01 RID: 3329
		private Cookie m_savedCookie;
	}
}
