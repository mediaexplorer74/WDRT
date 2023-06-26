using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000078 RID: 120
	public sealed class EntityCollectionCastNode : EntityCollectionNode
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0000AC4C File Offset: 0x00008E4C
		public EntityCollectionCastNode(EntityCollectionNode source, IEdmEntityType entityType)
		{
			ExceptionUtils.CheckArgumentNotNull<EntityCollectionNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			this.source = source;
			this.edmTypeReference = new EdmEntityTypeReference(entityType, false);
			this.entitySet = source.EntitySet;
			this.collectionTypeReference = EdmCoreModel.GetCollection(this.edmTypeReference);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000ACA6 File Offset: 0x00008EA6
		public EntityCollectionNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000ACAE File Offset: 0x00008EAE
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.edmTypeReference;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000ACB6 File Offset: 0x00008EB6
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000ACBE File Offset: 0x00008EBE
		public override IEdmEntityTypeReference EntityItemType
		{
			get
			{
				return this.edmTypeReference;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000ACC6 File Offset: 0x00008EC6
		public override IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000ACCE File Offset: 0x00008ECE
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.EntityCollectionCast;
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000ACD2 File Offset: 0x00008ED2
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040000C8 RID: 200
		private readonly EntityCollectionNode source;

		// Token: 0x040000C9 RID: 201
		private readonly IEdmEntityTypeReference edmTypeReference;

		// Token: 0x040000CA RID: 202
		private readonly IEdmCollectionTypeReference collectionTypeReference;

		// Token: 0x040000CB RID: 203
		private readonly IEdmEntitySet entitySet;
	}
}
