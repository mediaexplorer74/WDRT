using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Query.Metadata;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x020000D8 RID: 216
	internal sealed class EntitySetNode : EntityCollectionNode
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x00011E98 File Offset: 0x00010098
		public EntitySetNode(IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmEntitySet>(entitySet, "entitySet");
			this.entitySet = entitySet;
			this.entityType = new EdmEntityTypeReference(UriEdmHelpers.GetEntitySetElementType(this.EntitySet), false);
			this.collectionTypeReference = EdmCoreModel.GetCollection(this.entityType);
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00011EE5 File Offset: 0x000100E5
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00011EED File Offset: 0x000100ED
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00011EF5 File Offset: 0x000100F5
		public override IEdmEntityTypeReference EntityItemType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00011EFD File Offset: 0x000100FD
		public override IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00011F05 File Offset: 0x00010105
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.EntitySet;
			}
		}

		// Token: 0x04000226 RID: 550
		private readonly IEdmEntitySet entitySet;

		// Token: 0x04000227 RID: 551
		private readonly IEdmEntityTypeReference entityType;

		// Token: 0x04000228 RID: 552
		private readonly IEdmCollectionTypeReference collectionTypeReference;
	}
}
