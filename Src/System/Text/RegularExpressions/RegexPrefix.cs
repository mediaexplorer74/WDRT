using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000696 RID: 1686
	internal sealed class RegexPrefix
	{
		// Token: 0x06003EB0 RID: 16048 RVA: 0x00104DD8 File Offset: 0x00102FD8
		internal RegexPrefix(string prefix, bool ci)
		{
			this._prefix = prefix;
			this._caseInsensitive = ci;
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06003EB1 RID: 16049 RVA: 0x00104DEE File Offset: 0x00102FEE
		internal string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x00104DF6 File Offset: 0x00102FF6
		internal bool CaseInsensitive
		{
			get
			{
				return this._caseInsensitive;
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06003EB3 RID: 16051 RVA: 0x00104DFE File Offset: 0x00102FFE
		internal static RegexPrefix Empty
		{
			get
			{
				return RegexPrefix._empty;
			}
		}

		// Token: 0x04002DBD RID: 11709
		internal string _prefix;

		// Token: 0x04002DBE RID: 11710
		internal bool _caseInsensitive;

		// Token: 0x04002DBF RID: 11711
		internal static RegexPrefix _empty = new RegexPrefix(string.Empty, false);
	}
}
