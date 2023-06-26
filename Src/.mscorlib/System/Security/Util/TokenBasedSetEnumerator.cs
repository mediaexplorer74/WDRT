using System;

namespace System.Security.Util
{
	// Token: 0x0200037D RID: 893
	internal struct TokenBasedSetEnumerator
	{
		// Token: 0x06002C8D RID: 11405 RVA: 0x000A776B File Offset: 0x000A596B
		public bool MoveNext()
		{
			return this._tb != null && this._tb.MoveNext(ref this);
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000A7783 File Offset: 0x000A5983
		public void Reset()
		{
			this.Index = -1;
			this.Current = null;
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000A7793 File Offset: 0x000A5993
		public TokenBasedSetEnumerator(TokenBasedSet tb)
		{
			this.Index = -1;
			this.Current = null;
			this._tb = tb;
		}

		// Token: 0x040011E0 RID: 4576
		public object Current;

		// Token: 0x040011E1 RID: 4577
		public int Index;

		// Token: 0x040011E2 RID: 4578
		private TokenBasedSet _tb;
	}
}
