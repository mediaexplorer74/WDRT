using System;

namespace System.Security.Policy
{
	// Token: 0x02000102 RID: 258
	internal class HostContextInternal
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x0000DCDC File Offset: 0x0000BEDC
		public HostContextInternal(TrustManagerContext trustManagerContext)
		{
			if (trustManagerContext == null)
			{
				this.persist = true;
				return;
			}
			this.ignorePersistedDecision = trustManagerContext.IgnorePersistedDecision;
			this.noPrompt = trustManagerContext.NoPrompt;
			this.persist = trustManagerContext.Persist;
			this.previousAppId = trustManagerContext.PreviousApplicationIdentity;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000DD2A File Offset: 0x0000BF2A
		public bool IgnorePersistedDecision
		{
			get
			{
				return this.ignorePersistedDecision;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000DD32 File Offset: 0x0000BF32
		public bool NoPrompt
		{
			get
			{
				return this.noPrompt;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000DD3A File Offset: 0x0000BF3A
		public bool Persist
		{
			get
			{
				return this.persist;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000DD42 File Offset: 0x0000BF42
		public ApplicationIdentity PreviousAppId
		{
			get
			{
				return this.previousAppId;
			}
		}

		// Token: 0x04000444 RID: 1092
		private bool ignorePersistedDecision;

		// Token: 0x04000445 RID: 1093
		private bool noPrompt;

		// Token: 0x04000446 RID: 1094
		private bool persist;

		// Token: 0x04000447 RID: 1095
		private ApplicationIdentity previousAppId;
	}
}
