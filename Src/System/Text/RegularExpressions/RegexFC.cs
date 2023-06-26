using System;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000695 RID: 1685
	internal sealed class RegexFC
	{
		// Token: 0x06003EAA RID: 16042 RVA: 0x00104C84 File Offset: 0x00102E84
		internal RegexFC(bool nullable)
		{
			this._cc = new RegexCharClass();
			this._nullable = nullable;
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x00104CA0 File Offset: 0x00102EA0
		internal RegexFC(char ch, bool not, bool nullable, bool caseInsensitive)
		{
			this._cc = new RegexCharClass();
			if (not)
			{
				if (ch > '\0')
				{
					this._cc.AddRange('\0', ch - '\u0001');
				}
				if (ch < '\uffff')
				{
					this._cc.AddRange(ch + '\u0001', char.MaxValue);
				}
			}
			else
			{
				this._cc.AddRange(ch, ch);
			}
			this._caseInsensitive = caseInsensitive;
			this._nullable = nullable;
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x00104D0F File Offset: 0x00102F0F
		internal RegexFC(string charClass, bool nullable, bool caseInsensitive)
		{
			this._cc = RegexCharClass.Parse(charClass);
			this._nullable = nullable;
			this._caseInsensitive = caseInsensitive;
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x00104D34 File Offset: 0x00102F34
		internal bool AddFC(RegexFC fc, bool concatenate)
		{
			if (!this._cc.CanMerge || !fc._cc.CanMerge)
			{
				return false;
			}
			if (concatenate)
			{
				if (!this._nullable)
				{
					return true;
				}
				if (!fc._nullable)
				{
					this._nullable = false;
				}
			}
			else if (fc._nullable)
			{
				this._nullable = true;
			}
			this._caseInsensitive |= fc._caseInsensitive;
			this._cc.AddCharClass(fc._cc);
			return true;
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x00104DAF File Offset: 0x00102FAF
		internal string GetFirstChars(CultureInfo culture)
		{
			if (this._caseInsensitive)
			{
				this._cc.AddLowercase(culture);
			}
			return this._cc.ToStringClass();
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x00104DD0 File Offset: 0x00102FD0
		internal bool IsCaseInsensitive()
		{
			return this._caseInsensitive;
		}

		// Token: 0x04002DBA RID: 11706
		internal RegexCharClass _cc;

		// Token: 0x04002DBB RID: 11707
		internal bool _nullable;

		// Token: 0x04002DBC RID: 11708
		internal bool _caseInsensitive;
	}
}
