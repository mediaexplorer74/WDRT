using System;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x02000186 RID: 390
	[Serializable]
	internal class DelayedRegex
	{
		// Token: 0x06000E75 RID: 3701 RVA: 0x0004BA00 File Offset: 0x00049C00
		internal DelayedRegex(string regexString)
		{
			if (regexString == null)
			{
				throw new ArgumentNullException("regexString");
			}
			this._AsString = regexString;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0004BA1D File Offset: 0x00049C1D
		internal DelayedRegex(Regex regex)
		{
			if (regex == null)
			{
				throw new ArgumentNullException("regex");
			}
			this._AsRegex = regex;
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0004BA3A File Offset: 0x00049C3A
		internal Regex AsRegex
		{
			get
			{
				if (this._AsRegex == null)
				{
					this._AsRegex = new Regex(this._AsString + "[/]?", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
				}
				return this._AsRegex;
			}
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0004BA6C File Offset: 0x00049C6C
		public override string ToString()
		{
			if (this._AsString == null)
			{
				return this._AsString = this._AsRegex.ToString();
			}
			return this._AsString;
		}

		// Token: 0x04001263 RID: 4707
		private Regex _AsRegex;

		// Token: 0x04001264 RID: 4708
		private string _AsString;
	}
}
