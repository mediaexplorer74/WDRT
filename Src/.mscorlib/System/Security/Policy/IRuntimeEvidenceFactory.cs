using System;
using System.Collections.Generic;

namespace System.Security.Policy
{
	// Token: 0x02000358 RID: 856
	internal interface IRuntimeEvidenceFactory
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06002A90 RID: 10896
		IEvidenceFactory Target { get; }

		// Token: 0x06002A91 RID: 10897
		IEnumerable<EvidenceBase> GetFactorySuppliedEvidence();

		// Token: 0x06002A92 RID: 10898
		EvidenceBase GenerateEvidence(Type evidenceType);
	}
}
