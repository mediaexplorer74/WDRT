using System;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000041 RID: 65
	internal sealed class NonOptionSelectExpandTermParser : SelectExpandTermParser
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00007289 File Offset: 0x00005489
		public NonOptionSelectExpandTermParser(string clauseToParse, int maxDepth)
			: base(clauseToParse, maxDepth)
		{
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007293 File Offset: 0x00005493
		internal override ExpandTermToken BuildExpandTermToken(bool isInnerTerm, PathSegmentToken pathToken)
		{
			if (this.IsNotEndOfTerm(false))
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.Lexer.ExpressionText));
			}
			return new ExpandTermToken(pathToken);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000072BA File Offset: 0x000054BA
		internal override bool IsNotEndOfTerm(bool isInnerTerm)
		{
			return this.Lexer.CurrentToken.Kind != ExpressionTokenKind.End && this.Lexer.CurrentToken.Kind != ExpressionTokenKind.Comma;
		}
	}
}
