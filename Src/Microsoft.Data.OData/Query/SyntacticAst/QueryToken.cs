using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000013 RID: 19
	internal abstract class QueryToken : ODataAnnotatable
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000074 RID: 116
		public abstract QueryTokenKind Kind { get; }

		// Token: 0x06000075 RID: 117
		public abstract T Accept<T>(ISyntacticTreeVisitor<T> visitor);

		// Token: 0x04000031 RID: 49
		public static readonly QueryToken[] EmptyTokens = new QueryToken[0];
	}
}
