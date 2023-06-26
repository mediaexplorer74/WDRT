using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x020000CA RID: 202
	internal sealed class KeyLookupNode : SingleEntityNode
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x00011510 File Offset: 0x0000F710
		public KeyLookupNode(EntityCollectionNode source, IEnumerable<KeyPropertyValue> keyPropertyValues)
		{
			ExceptionUtils.CheckArgumentNotNull<EntityCollectionNode>(source, "source");
			this.source = source;
			this.entitySet = source.EntitySet;
			this.entityTypeReference = source.EntityItemType;
			this.keyPropertyValues = keyPropertyValues;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00011549 File Offset: 0x0000F749
		public EntityCollectionNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00011551 File Offset: 0x0000F751
		public IEnumerable<KeyPropertyValue> KeyPropertyValues
		{
			get
			{
				return this.keyPropertyValues;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00011559 File Offset: 0x0000F759
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00011561 File Offset: 0x0000F761
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00011569 File Offset: 0x0000F769
		public override IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00011571 File Offset: 0x0000F771
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.KeyLookup;
			}
		}

		// Token: 0x040001D3 RID: 467
		private readonly EntityCollectionNode source;

		// Token: 0x040001D4 RID: 468
		private readonly IEdmEntitySet entitySet;

		// Token: 0x040001D5 RID: 469
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x040001D6 RID: 470
		private readonly IEnumerable<KeyPropertyValue> keyPropertyValues;
	}
}
