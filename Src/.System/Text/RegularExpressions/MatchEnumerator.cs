using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200069E RID: 1694
	[Serializable]
	internal class MatchEnumerator : IEnumerator
	{
		// Token: 0x06003F12 RID: 16146 RVA: 0x001070DC File Offset: 0x001052DC
		internal MatchEnumerator(MatchCollection matchcoll)
		{
			this._matchcoll = matchcoll;
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x001070EC File Offset: 0x001052EC
		public bool MoveNext()
		{
			if (this._done)
			{
				return false;
			}
			this._match = this._matchcoll.GetMatch(this._curindex);
			this._curindex++;
			if (this._match == null)
			{
				this._done = true;
				return false;
			}
			return true;
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06003F14 RID: 16148 RVA: 0x0010713A File Offset: 0x0010533A
		public object Current
		{
			get
			{
				if (this._match == null)
				{
					throw new InvalidOperationException(SR.GetString("EnumNotStarted"));
				}
				return this._match;
			}
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x0010715A File Offset: 0x0010535A
		public void Reset()
		{
			this._curindex = 0;
			this._done = false;
			this._match = null;
		}

		// Token: 0x04002DEB RID: 11755
		internal MatchCollection _matchcoll;

		// Token: 0x04002DEC RID: 11756
		internal Match _match;

		// Token: 0x04002DED RID: 11757
		internal int _curindex;

		// Token: 0x04002DEE RID: 11758
		internal bool _done;
	}
}
