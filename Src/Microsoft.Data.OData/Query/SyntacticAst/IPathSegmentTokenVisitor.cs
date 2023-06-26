using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x0200002F RID: 47
	internal interface IPathSegmentTokenVisitor<T>
	{
		// Token: 0x06000139 RID: 313
		T Visit(SystemToken tokenIn);

		// Token: 0x0600013A RID: 314
		T Visit(NonSystemToken tokenIn);
	}
}
