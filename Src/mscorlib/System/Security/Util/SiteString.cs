using System;
using System.Collections;
using System.Globalization;

namespace System.Security.Util
{
	// Token: 0x0200037A RID: 890
	[Serializable]
	internal class SiteString
	{
		// Token: 0x06002C46 RID: 11334 RVA: 0x000A5E84 File Offset: 0x000A4084
		protected internal SiteString()
		{
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000A5E8C File Offset: 0x000A408C
		public SiteString(string site)
		{
			this.m_separatedSite = SiteString.CreateSeparatedSite(site);
			this.m_site = site;
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000A5EA7 File Offset: 0x000A40A7
		private SiteString(string site, ArrayList separatedSite)
		{
			this.m_separatedSite = separatedSite;
			this.m_site = site;
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000A5EC0 File Offset: 0x000A40C0
		private static ArrayList CreateSeparatedSite(string site)
		{
			if (site == null || site.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
			}
			ArrayList arrayList = new ArrayList();
			int num = -1;
			int num2 = site.IndexOf('[');
			if (num2 == 0)
			{
				num = site.IndexOf(']', num2 + 1);
			}
			if (num != -1)
			{
				string text = site.Substring(num2 + 1, num - num2 - 1);
				arrayList.Add(text);
				return arrayList;
			}
			string[] array = site.Split(SiteString.m_separators);
			for (int i = array.Length - 1; i > -1; i--)
			{
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
				}
				if (array[i].Equals(""))
				{
					if (i != array.Length - 1)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
					}
				}
				else if (array[i].Equals("*"))
				{
					if (i != 0)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
					}
					arrayList.Add(array[i]);
				}
				else
				{
					if (!SiteString.AllLegalCharacters(array[i]))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
					}
					arrayList.Add(array[i]);
				}
			}
			return arrayList;
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000A5FE8 File Offset: 0x000A41E8
		private static bool AllLegalCharacters(string str)
		{
			foreach (char c in str)
			{
				if (!SiteString.IsLegalDNSChar(c) && !SiteString.IsNetbiosSplChar(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000A6021 File Offset: 0x000A4221
		private static bool IsLegalDNSChar(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '-';
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000A604C File Offset: 0x000A424C
		private static bool IsNetbiosSplChar(char c)
		{
			if (c <= '@')
			{
				switch (c)
				{
				case '!':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '-':
				case '.':
					break;
				case '"':
				case '*':
				case '+':
				case ',':
					return false;
				default:
					if (c != '@')
					{
						return false;
					}
					break;
				}
			}
			else if (c != '^' && c != '_')
			{
				switch (c)
				{
				case '{':
				case '}':
				case '~':
					break;
				case '|':
					return false;
				default:
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000A60CE File Offset: 0x000A42CE
		public override string ToString()
		{
			return this.m_site;
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000A60D6 File Offset: 0x000A42D6
		public override bool Equals(object o)
		{
			return o != null && o is SiteString && this.Equals((SiteString)o, true);
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000A60F4 File Offset: 0x000A42F4
		public override int GetHashCode()
		{
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			return textInfo.GetCaseInsensitiveHashCode(this.m_site);
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000A6118 File Offset: 0x000A4318
		internal bool Equals(SiteString ss, bool ignoreCase)
		{
			if (this.m_site == null)
			{
				return ss.m_site == null;
			}
			return ss.m_site != null && this.IsSubsetOf(ss, ignoreCase) && ss.IsSubsetOf(this, ignoreCase);
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000A614A File Offset: 0x000A434A
		public virtual SiteString Copy()
		{
			return new SiteString(this.m_site, this.m_separatedSite);
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000A615D File Offset: 0x000A435D
		public virtual bool IsSubsetOf(SiteString operand)
		{
			return this.IsSubsetOf(operand, true);
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000A6168 File Offset: 0x000A4368
		public virtual bool IsSubsetOf(SiteString operand, bool ignoreCase)
		{
			StringComparison stringComparison = (ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			if (operand == null)
			{
				return false;
			}
			if (this.m_separatedSite.Count == operand.m_separatedSite.Count && this.m_separatedSite.Count == 0)
			{
				return true;
			}
			if (this.m_separatedSite.Count < operand.m_separatedSite.Count - 1)
			{
				return false;
			}
			if (this.m_separatedSite.Count > operand.m_separatedSite.Count && operand.m_separatedSite.Count > 0 && !operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals("*"))
			{
				return false;
			}
			if (string.Compare(this.m_site, operand.m_site, stringComparison) == 0)
			{
				return true;
			}
			for (int i = 0; i < operand.m_separatedSite.Count - 1; i++)
			{
				if (string.Compare((string)this.m_separatedSite[i], (string)operand.m_separatedSite[i], stringComparison) != 0)
				{
					return false;
				}
			}
			if (this.m_separatedSite.Count < operand.m_separatedSite.Count)
			{
				return operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals("*");
			}
			return this.m_separatedSite.Count != operand.m_separatedSite.Count || string.Compare((string)this.m_separatedSite[this.m_separatedSite.Count - 1], (string)operand.m_separatedSite[this.m_separatedSite.Count - 1], stringComparison) == 0 || operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals("*");
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000A6326 File Offset: 0x000A4526
		public virtual SiteString Intersect(SiteString operand)
		{
			if (operand == null)
			{
				return null;
			}
			if (this.IsSubsetOf(operand))
			{
				return this.Copy();
			}
			if (operand.IsSubsetOf(this))
			{
				return operand.Copy();
			}
			return null;
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000A634E File Offset: 0x000A454E
		public virtual SiteString Union(SiteString operand)
		{
			if (operand == null)
			{
				return this;
			}
			if (this.IsSubsetOf(operand))
			{
				return operand.Copy();
			}
			if (operand.IsSubsetOf(this))
			{
				return this.Copy();
			}
			return null;
		}

		// Token: 0x040011CD RID: 4557
		protected string m_site;

		// Token: 0x040011CE RID: 4558
		protected ArrayList m_separatedSite;

		// Token: 0x040011CF RID: 4559
		protected static char[] m_separators = new char[] { '.' };
	}
}
