using System;

namespace System.Security.Policy
{
	// Token: 0x0200034D RID: 845
	internal interface ILegacyEvidenceAdapter
	{
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002A56 RID: 10838
		object EvidenceObject { get; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06002A57 RID: 10839
		Type EvidenceType { get; }
	}
}
