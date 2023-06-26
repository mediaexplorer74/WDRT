using System;

namespace System.Net
{
	// Token: 0x0200020D RID: 525
	internal class SpnToken
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00066E60 File Offset: 0x00065060
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x00066E68 File Offset: 0x00065068
		internal bool IsTrusted
		{
			get
			{
				return this.isTrusted;
			}
			set
			{
				this.isTrusted = false;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00066E71 File Offset: 0x00065071
		internal string Spn
		{
			get
			{
				return this.spn;
			}
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00066E79 File Offset: 0x00065079
		internal SpnToken(string spn)
			: this(spn, true)
		{
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00066E83 File Offset: 0x00065083
		internal SpnToken(string spn, bool trusted)
		{
			this.spn = spn;
			this.isTrusted = trusted;
		}

		// Token: 0x04001556 RID: 5462
		private readonly string spn;

		// Token: 0x04001557 RID: 5463
		private bool isTrusted;
	}
}
