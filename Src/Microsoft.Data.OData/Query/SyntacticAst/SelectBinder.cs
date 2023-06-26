using System;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x0200008F RID: 143
	internal sealed class SelectBinder
	{
		// Token: 0x06000351 RID: 849 RVA: 0x0000B7E7 File Offset: 0x000099E7
		public SelectBinder(IEdmModel model, IEdmEntityType entityType, int maxDepth, SelectExpandClause expandClauseToDecorate)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "tokenIn");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			this.visitor = new SelectPropertyVisitor(model, entityType, maxDepth, expandClauseToDecorate);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000B818 File Offset: 0x00009A18
		public SelectExpandClause Bind(SelectToken tokenIn)
		{
			if (tokenIn == null || !tokenIn.Properties.Any<PathSegmentToken>())
			{
				this.visitor.DecoratedExpandClause.SetAllSelectionRecursively();
			}
			else
			{
				foreach (PathSegmentToken pathSegmentToken in tokenIn.Properties)
				{
					pathSegmentToken.Accept(this.visitor);
				}
			}
			return this.visitor.DecoratedExpandClause;
		}

		// Token: 0x040000FE RID: 254
		private readonly SelectPropertyVisitor visitor;
	}
}
