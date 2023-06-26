using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000057 RID: 87
	internal abstract class PathSegmentTokenVisitor : IPathSegmentTokenVisitor
	{
		// Token: 0x0600023A RID: 570 RVA: 0x00008674 File Offset: 0x00006874
		public virtual void Visit(SystemToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000867B File Offset: 0x0000687B
		public virtual void Visit(NonSystemToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
