using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Security.Policy
{
	// Token: 0x0200033E RID: 830
	internal sealed class AppDomainEvidenceFactory : IRuntimeEvidenceFactory
	{
		// Token: 0x06002980 RID: 10624 RVA: 0x0009A70C File Offset: 0x0009890C
		internal AppDomainEvidenceFactory(AppDomain target)
		{
			this.m_targetDomain = target;
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x0009A71B File Offset: 0x0009891B
		public IEvidenceFactory Target
		{
			get
			{
				return this.m_targetDomain;
			}
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x0009A723 File Offset: 0x00098923
		public IEnumerable<EvidenceBase> GetFactorySuppliedEvidence()
		{
			return new EvidenceBase[0];
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x0009A72C File Offset: 0x0009892C
		[SecuritySafeCritical]
		public EvidenceBase GenerateEvidence(Type evidenceType)
		{
			if (!this.m_targetDomain.IsDefaultAppDomain())
			{
				AppDomain defaultDomain = AppDomain.GetDefaultDomain();
				return defaultDomain.GetHostEvidence(evidenceType);
			}
			if (this.m_entryPointEvidence == null)
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				RuntimeAssembly runtimeAssembly = entryAssembly as RuntimeAssembly;
				if (runtimeAssembly != null)
				{
					this.m_entryPointEvidence = runtimeAssembly.EvidenceNoDemand.Clone();
				}
				else if (entryAssembly != null)
				{
					this.m_entryPointEvidence = entryAssembly.Evidence;
				}
			}
			if (this.m_entryPointEvidence != null)
			{
				return this.m_entryPointEvidence.GetHostEvidence(evidenceType);
			}
			return null;
		}

		// Token: 0x0400110F RID: 4367
		private AppDomain m_targetDomain;

		// Token: 0x04001110 RID: 4368
		private Evidence m_entryPointEvidence;
	}
}
