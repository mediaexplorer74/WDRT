using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000226 RID: 550
	public sealed class AtomStreamReferenceMetadata : ODataAnnotatable
	{
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x00040C9B File Offset: 0x0003EE9B
		// (set) Token: 0x06001141 RID: 4417 RVA: 0x00040CA3 File Offset: 0x0003EEA3
		public AtomLinkMetadata SelfLink { get; set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x00040CAC File Offset: 0x0003EEAC
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x00040CB4 File Offset: 0x0003EEB4
		public AtomLinkMetadata EditLink { get; set; }
	}
}
