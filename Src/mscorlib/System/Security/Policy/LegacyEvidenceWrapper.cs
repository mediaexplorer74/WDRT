using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x0200034E RID: 846
	[Serializable]
	internal sealed class LegacyEvidenceWrapper : EvidenceBase, ILegacyEvidenceAdapter
	{
		// Token: 0x06002A58 RID: 10840 RVA: 0x0009E2EC File Offset: 0x0009C4EC
		internal LegacyEvidenceWrapper(object legacyEvidence)
		{
			this.m_legacyEvidence = legacyEvidence;
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x0009E2FB File Offset: 0x0009C4FB
		public object EvidenceObject
		{
			get
			{
				return this.m_legacyEvidence;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06002A5A RID: 10842 RVA: 0x0009E303 File Offset: 0x0009C503
		public Type EvidenceType
		{
			get
			{
				return this.m_legacyEvidence.GetType();
			}
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x0009E310 File Offset: 0x0009C510
		public override bool Equals(object obj)
		{
			return this.m_legacyEvidence.Equals(obj);
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x0009E31E File Offset: 0x0009C51E
		public override int GetHashCode()
		{
			return this.m_legacyEvidence.GetHashCode();
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x0009E32B File Offset: 0x0009C52B
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override EvidenceBase Clone()
		{
			return base.Clone();
		}

		// Token: 0x04001144 RID: 4420
		private object m_legacyEvidence;
	}
}
