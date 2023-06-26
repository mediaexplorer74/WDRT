using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000054 RID: 84
	internal abstract class PathSegmentTokenVisitor<T> : IPathSegmentTokenVisitor<T>
	{
		// Token: 0x0600022E RID: 558 RVA: 0x0000850A File Offset: 0x0000670A
		public virtual T Visit(SystemToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008511 File Offset: 0x00006711
		public virtual T Visit(NonSystemToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
