using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000030 RID: 48
	internal interface IPathSegmentTokenVisitor
	{
		// Token: 0x0600013B RID: 315
		void Visit(SystemToken tokenIn);

		// Token: 0x0600013C RID: 316
		void Visit(NonSystemToken tokenIn);
	}
}
