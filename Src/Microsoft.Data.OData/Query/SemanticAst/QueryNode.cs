using System;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000017 RID: 23
	public abstract class QueryNode : ODataAnnotatable
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000088 RID: 136
		public abstract QueryNodeKind Kind { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000372B File Offset: 0x0000192B
		internal virtual InternalQueryNodeKind InternalKind
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003732 File Offset: 0x00001932
		public virtual T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}
	}
}
