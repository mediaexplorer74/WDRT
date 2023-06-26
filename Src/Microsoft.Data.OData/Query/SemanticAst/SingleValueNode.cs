using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200007E RID: 126
	public abstract class SingleValueNode : QueryNode
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002FF RID: 767
		public abstract IEdmTypeReference TypeReference { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000AF61 File Offset: 0x00009161
		public override QueryNodeKind Kind
		{
			get
			{
				return (QueryNodeKind)this.InternalKind;
			}
		}
	}
}
