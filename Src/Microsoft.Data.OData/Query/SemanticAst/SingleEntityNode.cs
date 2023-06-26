using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200007F RID: 127
	public abstract class SingleEntityNode : SingleValueNode
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000302 RID: 770
		public abstract IEdmEntitySet EntitySet { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000303 RID: 771
		public abstract IEdmEntityTypeReference EntityTypeReference { get; }
	}
}
