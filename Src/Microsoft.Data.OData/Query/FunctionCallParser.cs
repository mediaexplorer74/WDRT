using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200002D RID: 45
	internal sealed class FunctionCallParser : IFunctionCallParser
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public FunctionCallParser(ExpressionLexer lexer, UriQueryExpressionParser.Parser parseMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<ExpressionLexer>(lexer, "lexer");
			ExceptionUtils.CheckArgumentNotNull<UriQueryExpressionParser.Parser>(parseMethod, "parseMethod");
			this.lexer = lexer;
			this.parseMethod = parseMethod;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005C02 File Offset: 0x00003E02
		public ExpressionLexer Lexer
		{
			get
			{
				return this.lexer;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005C0C File Offset: 0x00003E0C
		public QueryToken ParseIdentifierAsFunction(QueryToken parent)
		{
			string text;
			if (this.Lexer.PeekNextToken().Kind == ExpressionTokenKind.Dot)
			{
				text = this.Lexer.ReadDottedIdentifier(false);
			}
			else
			{
				text = this.Lexer.CurrentToken.Text;
				this.Lexer.NextToken();
			}
			FunctionParameterToken[] array = this.ParseArgumentList();
			return new FunctionCallToken(text, array, parent);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005C68 File Offset: 0x00003E68
		public FunctionParameterToken[] ParseArgumentList()
		{
			if (this.Lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_OpenParenExpected(this.Lexer.CurrentToken.Position, this.Lexer.ExpressionText));
			}
			this.Lexer.NextToken();
			FunctionParameterToken[] array;
			if (this.Lexer.CurrentToken.Kind == ExpressionTokenKind.CloseParen)
			{
				array = FunctionParameterToken.EmptyParameterList;
			}
			else
			{
				array = this.ParseArguments();
			}
			if (this.Lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_CloseParenOrCommaExpected(this.Lexer.CurrentToken.Position, this.Lexer.ExpressionText));
			}
			this.Lexer.NextToken();
			return array;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005D30 File Offset: 0x00003F30
		public FunctionParameterToken[] ParseArguments()
		{
			ICollection<FunctionParameterToken> collection;
			if (this.TryReadArgumentsAsNamedValues(out collection))
			{
				return collection.ToArray<FunctionParameterToken>();
			}
			return this.ReadArgumentsAsPositionalValues().ToArray();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005D5C File Offset: 0x00003F5C
		private List<FunctionParameterToken> ReadArgumentsAsPositionalValues()
		{
			List<FunctionParameterToken> list = new List<FunctionParameterToken>();
			for (;;)
			{
				list.Add(new FunctionParameterToken(null, this.parseMethod()));
				if (this.Lexer.CurrentToken.Kind != ExpressionTokenKind.Comma)
				{
					break;
				}
				this.Lexer.NextToken();
			}
			return list;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005DB4 File Offset: 0x00003FB4
		private bool TryReadArgumentsAsNamedValues(out ICollection<FunctionParameterToken> argList)
		{
			if (!this.lexer.TrySplitFunctionParameters(out argList))
			{
				return false;
			}
			if (argList.Select((FunctionParameterToken t) => t.ParameterName).Distinct<string>().Count<string>() != argList.Count)
			{
				throw new ODataException(Strings.FunctionCallParser_DuplicateParameterName);
			}
			return true;
		}

		// Token: 0x0400005E RID: 94
		private readonly ExpressionLexer lexer;

		// Token: 0x0400005F RID: 95
		private readonly UriQueryExpressionParser.Parser parseMethod;
	}
}
