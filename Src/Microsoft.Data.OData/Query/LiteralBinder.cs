using System;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000035 RID: 53
	internal sealed class LiteralBinder
	{
		// Token: 0x06000165 RID: 357 RVA: 0x000066B8 File Offset: 0x000048B8
		internal static ConstantNode BindLiteral(LiteralToken literalToken)
		{
			ExceptionUtils.CheckArgumentNotNull<LiteralToken>(literalToken, "literalToken");
			if (!string.IsNullOrEmpty(literalToken.OriginalText))
			{
				return new ConstantNode(literalToken.Value, literalToken.OriginalText);
			}
			return new ConstantNode(literalToken.Value);
		}
	}
}
