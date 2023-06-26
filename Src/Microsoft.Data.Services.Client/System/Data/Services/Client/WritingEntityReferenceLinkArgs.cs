using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200004C RID: 76
	public sealed class WritingEntityReferenceLinkArgs
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000C974 File Offset: 0x0000AB74
		public WritingEntityReferenceLinkArgs(ODataEntityReferenceLink entityReferenceLink, object source, object target)
		{
			Util.CheckArgumentNull<ODataEntityReferenceLink>(entityReferenceLink, "entityReferenceLink");
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNull<object>(target, "target");
			this.EntityReferenceLink = entityReferenceLink;
			this.Source = source;
			this.Target = target;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000C9C0 File Offset: 0x0000ABC0
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		public ODataEntityReferenceLink EntityReferenceLink { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000C9D1 File Offset: 0x0000ABD1
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000C9D9 File Offset: 0x0000ABD9
		public object Source { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000C9E2 File Offset: 0x0000ABE2
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000C9EA File Offset: 0x0000ABEA
		public object Target { get; private set; }
	}
}
