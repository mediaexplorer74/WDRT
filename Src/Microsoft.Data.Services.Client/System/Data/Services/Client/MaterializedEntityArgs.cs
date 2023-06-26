using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200004A RID: 74
	public sealed class MaterializedEntityArgs
	{
		// Token: 0x0600025F RID: 607 RVA: 0x0000C90B File Offset: 0x0000AB0B
		public MaterializedEntityArgs(ODataEntry entry, object entity)
		{
			Util.CheckArgumentNull<ODataEntry>(entry, "entry");
			Util.CheckArgumentNull<object>(entity, "entity");
			this.Entry = entry;
			this.Entity = entity;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000C939 File Offset: 0x0000AB39
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000C941 File Offset: 0x0000AB41
		public ODataEntry Entry { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000C94A File Offset: 0x0000AB4A
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000C952 File Offset: 0x0000AB52
		public object Entity { get; private set; }
	}
}
