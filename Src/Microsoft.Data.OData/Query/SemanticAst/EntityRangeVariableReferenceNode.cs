using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000080 RID: 128
	public sealed class EntityRangeVariableReferenceNode : SingleEntityNode
	{
		// Token: 0x06000305 RID: 773 RVA: 0x0000AF7C File Offset: 0x0000917C
		public EntityRangeVariableReferenceNode(string name, EntityRangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<EntityRangeVariable>(rangeVariable, "rangeVariable");
			this.name = name;
			this.entitySet = rangeVariable.EntitySet;
			this.entityTypeReference = rangeVariable.EntityTypeReference;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000AFCB File Offset: 0x000091CB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000AFD3 File Offset: 0x000091D3
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000AFDB File Offset: 0x000091DB
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000AFE3 File Offset: 0x000091E3
		public EntityRangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000AFEB File Offset: 0x000091EB
		public override IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000AFF3 File Offset: 0x000091F3
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.EntityRangeVariableReference;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000AFF7 File Offset: 0x000091F7
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040000DB RID: 219
		private readonly string name;

		// Token: 0x040000DC RID: 220
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x040000DD RID: 221
		private readonly EntityRangeVariable rangeVariable;

		// Token: 0x040000DE RID: 222
		private readonly IEdmEntitySet entitySet;
	}
}
