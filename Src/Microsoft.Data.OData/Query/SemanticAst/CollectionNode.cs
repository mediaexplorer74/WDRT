using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200001D RID: 29
	public abstract class CollectionNode : QueryNode
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B0 RID: 176
		public abstract IEdmTypeReference ItemType { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B1 RID: 177
		public abstract IEdmCollectionTypeReference CollectionType { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003F5B File Offset: 0x0000215B
		public override QueryNodeKind Kind
		{
			get
			{
				return (QueryNodeKind)this.InternalKind;
			}
		}
	}
}
