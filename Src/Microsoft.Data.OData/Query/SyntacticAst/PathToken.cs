using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000095 RID: 149
	internal abstract class PathToken : QueryToken
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000386 RID: 902
		// (set) Token: 0x06000387 RID: 903
		public abstract QueryToken NextToken { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000388 RID: 904
		public abstract string Identifier { get; }
	}
}
