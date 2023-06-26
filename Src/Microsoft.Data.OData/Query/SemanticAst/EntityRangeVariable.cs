using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200007D RID: 125
	public sealed class EntityRangeVariable : RangeVariable
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x0000AEAC File Offset: 0x000090AC
		public EntityRangeVariable(string name, IEdmEntityTypeReference entityType, EntityCollectionNode entityCollectionNode)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityTypeReference>(entityType, "entityType");
			this.name = name;
			this.entityTypeReference = entityType;
			this.entityCollectionNode = entityCollectionNode;
			this.entitySet = ((entityCollectionNode != null) ? entityCollectionNode.EntitySet : null);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000AEFC File Offset: 0x000090FC
		public EntityRangeVariable(string name, IEdmEntityTypeReference entityType, IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityTypeReference>(entityType, "entityType");
			this.name = name;
			this.entityTypeReference = entityType;
			this.entityCollectionNode = null;
			this.entitySet = entitySet;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000AF36 File Offset: 0x00009136
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000AF3E File Offset: 0x0000913E
		public EntityCollectionNode EntityCollectionNode
		{
			get
			{
				return this.entityCollectionNode;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000AF46 File Offset: 0x00009146
		public IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000AF4E File Offset: 0x0000914E
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000AF56 File Offset: 0x00009156
		public IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000AF5E File Offset: 0x0000915E
		public override int Kind
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x040000D7 RID: 215
		private readonly string name;

		// Token: 0x040000D8 RID: 216
		private readonly EntityCollectionNode entityCollectionNode;

		// Token: 0x040000D9 RID: 217
		private readonly IEdmEntitySet entitySet;

		// Token: 0x040000DA RID: 218
		private readonly IEdmEntityTypeReference entityTypeReference;
	}
}
