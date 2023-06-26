using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200004D RID: 77
	public sealed class WritingEntryArgs
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000C9F3 File Offset: 0x0000ABF3
		public WritingEntryArgs(ODataEntry entry, object entity)
		{
			Util.CheckArgumentNull<ODataEntry>(entry, "entry");
			Util.CheckArgumentNull<object>(entity, "entity");
			this.Entry = entry;
			this.Entity = entity;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000CA21 File Offset: 0x0000AC21
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000CA29 File Offset: 0x0000AC29
		public ODataEntry Entry { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000CA32 File Offset: 0x0000AC32
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0000CA3A File Offset: 0x0000AC3A
		public object Entity { get; private set; }
	}
}
