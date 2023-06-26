using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200002E RID: 46
	internal sealed class IdentifierTokenizer
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00005E14 File Offset: 0x00004014
		public IdentifierTokenizer(HashSet<string> parameters, IFunctionCallParser functionCallParser)
		{
			ExceptionUtils.CheckArgumentNotNull<HashSet<string>>(parameters, "parameters");
			ExceptionUtils.CheckArgumentNotNull<IFunctionCallParser>(functionCallParser, "functionCallParser");
			this.lexer = functionCallParser.Lexer;
			this.parameters = parameters;
			this.functionCallParser = functionCallParser;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005E4C File Offset: 0x0000404C
		public QueryToken ParseIdentifier(QueryToken parent)
		{
			this.lexer.ValidateToken(ExpressionTokenKind.Identifier);
			bool flag = this.lexer.ExpandIdentifierAsFunction();
			if (flag)
			{
				return this.functionCallParser.ParseIdentifierAsFunction(parent);
			}
			if (this.lexer.PeekNextToken().Kind == ExpressionTokenKind.Dot)
			{
				string text = this.lexer.ReadDottedIdentifier(false);
				return new DottedIdentifierToken(text, parent);
			}
			return this.ParseMemberAccess(parent);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005EB4 File Offset: 0x000040B4
		public QueryToken ParseMemberAccess(QueryToken instance)
		{
			if (this.lexer.CurrentToken.Text == "*")
			{
				return this.ParseStarMemberAccess(instance);
			}
			string identifier = this.lexer.CurrentToken.GetIdentifier();
			if (instance == null && this.parameters.Contains(identifier))
			{
				this.lexer.NextToken();
				return new RangeVariableToken(identifier);
			}
			this.lexer.NextToken();
			return new EndPathToken(identifier, instance);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005F30 File Offset: 0x00004130
		public QueryToken ParseStarMemberAccess(QueryToken instance)
		{
			if (this.lexer.CurrentToken.Text != "*")
			{
				throw IdentifierTokenizer.ParseError(Strings.UriQueryExpressionParser_CannotCreateStarTokenFromNonStar(this.lexer.CurrentToken.Text));
			}
			this.lexer.NextToken();
			return new StarToken(instance);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005F86 File Offset: 0x00004186
		private static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x04000061 RID: 97
		private readonly ExpressionLexer lexer;

		// Token: 0x04000062 RID: 98
		private readonly HashSet<string> parameters;

		// Token: 0x04000063 RID: 99
		private readonly IFunctionCallParser functionCallParser;
	}
}
