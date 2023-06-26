using System;

namespace System.IO.Compression
{
	// Token: 0x02000435 RID: 1077
	internal class Match
	{
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x000B9706 File Offset: 0x000B7906
		// (set) Token: 0x0600284C RID: 10316 RVA: 0x000B970E File Offset: 0x000B790E
		internal MatchState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x000B9717 File Offset: 0x000B7917
		// (set) Token: 0x0600284E RID: 10318 RVA: 0x000B971F File Offset: 0x000B791F
		internal int Position
		{
			get
			{
				return this.pos;
			}
			set
			{
				this.pos = value;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x000B9728 File Offset: 0x000B7928
		// (set) Token: 0x06002850 RID: 10320 RVA: 0x000B9730 File Offset: 0x000B7930
		internal int Length
		{
			get
			{
				return this.len;
			}
			set
			{
				this.len = value;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x000B9739 File Offset: 0x000B7939
		// (set) Token: 0x06002852 RID: 10322 RVA: 0x000B9741 File Offset: 0x000B7941
		internal byte Symbol
		{
			get
			{
				return this.symbol;
			}
			set
			{
				this.symbol = value;
			}
		}

		// Token: 0x04002217 RID: 8727
		private MatchState state;

		// Token: 0x04002218 RID: 8728
		private int pos;

		// Token: 0x04002219 RID: 8729
		private int len;

		// Token: 0x0400221A RID: 8730
		private byte symbol;
	}
}
