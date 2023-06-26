using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200069C RID: 1692
	internal class MatchSparse : Match
	{
		// Token: 0x06003F06 RID: 16134 RVA: 0x00106EA6 File Offset: 0x001050A6
		internal MatchSparse(Regex regex, Hashtable caps, int capcount, string text, int begpos, int len, int startpos)
			: base(regex, capcount, text, begpos, len, startpos)
		{
			this._caps = caps;
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06003F07 RID: 16135 RVA: 0x00106EBF File Offset: 0x001050BF
		public override GroupCollection Groups
		{
			get
			{
				if (this._groupcoll == null)
				{
					this._groupcoll = new GroupCollection(this, this._caps);
				}
				return this._groupcoll;
			}
		}

		// Token: 0x04002DE1 RID: 11745
		internal new Hashtable _caps;
	}
}
