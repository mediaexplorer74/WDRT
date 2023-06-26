using System;
using System.Linq;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000026 RID: 38
	internal sealed class ExpandOptionSelectExpandTermParser : SelectExpandTermParser
	{
		// Token: 0x060000FB RID: 251 RVA: 0x000048CB File Offset: 0x00002ACB
		public ExpandOptionSelectExpandTermParser(string clauseToParse, int maxDepth)
			: base(clauseToParse, maxDepth)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000048D8 File Offset: 0x00002AD8
		internal override ExpandTermToken BuildExpandTermToken(bool isInnerTerm, PathSegmentToken pathToken)
		{
			QueryToken queryToken = null;
			OrderByToken orderByToken = null;
			long? num = null;
			long? num2 = null;
			InlineCountKind? inlineCountKind = null;
			SelectToken selectToken = null;
			ExpandToken expandToken = null;
			if (this.Lexer.CurrentToken.Kind == ExpressionTokenKind.OpenParen)
			{
				while (this.Lexer.PeekNextToken().Kind != ExpressionTokenKind.CloseParen)
				{
					string text;
					switch (text = this.Lexer.NextToken().Text)
					{
					case "$filter":
					{
						this.Lexer.NextToken();
						string text2 = this.ReadQueryOption();
						UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(base.MaxDepth);
						queryToken = uriQueryExpressionParser.ParseFilter(text2);
						continue;
					}
					case "$orderby":
					{
						this.Lexer.NextToken();
						string text3 = this.ReadQueryOption();
						UriQueryExpressionParser uriQueryExpressionParser2 = new UriQueryExpressionParser(base.MaxDepth);
						orderByToken = uriQueryExpressionParser2.ParseOrderBy(text3).Single<OrderByToken>();
						continue;
					}
					case "$top":
					{
						this.Lexer.NextToken();
						string text4 = this.ReadQueryOption();
						long num4;
						if (!long.TryParse(text4, out num4))
						{
							throw new ODataException(Strings.UriSelectParser_InvalidTopOption(text4));
						}
						num = new long?(num4);
						continue;
					}
					case "$skip":
					{
						this.Lexer.NextToken();
						string text5 = this.ReadQueryOption();
						long num5;
						if (!long.TryParse(text5, out num5))
						{
							throw new ODataException(Strings.UriSelectParser_InvalidSkipOption(text5));
						}
						num2 = new long?(num5);
						continue;
					}
					case "$inlinecount":
					{
						this.Lexer.NextToken();
						string text6 = this.ReadQueryOption();
						string text7;
						if ((text7 = text6) != null)
						{
							if (text7 == "none")
							{
								inlineCountKind = new InlineCountKind?(InlineCountKind.None);
								continue;
							}
							if (text7 == "allpages")
							{
								inlineCountKind = new InlineCountKind?(InlineCountKind.AllPages);
								continue;
							}
						}
						throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.Lexer.ExpressionText));
					}
					case "$select":
						this.Lexer.NextToken();
						selectToken = base.ParseSelect();
						continue;
					case "$expand":
						this.Lexer.NextToken();
						expandToken = base.ParseExpand();
						continue;
					}
					throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.Lexer.ExpressionText));
				}
			}
			else if (this.IsNotEndOfTerm(isInnerTerm))
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.Lexer.ExpressionText));
			}
			return new ExpandTermToken(pathToken, queryToken, orderByToken, num, num2, inlineCountKind, selectToken, expandToken);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004BA8 File Offset: 0x00002DA8
		internal override bool IsNotEndOfTerm(bool isInnerTerm)
		{
			if (!isInnerTerm)
			{
				return this.Lexer.CurrentToken.Kind != ExpressionTokenKind.End && this.Lexer.CurrentToken.Kind != ExpressionTokenKind.Comma;
			}
			return this.Lexer.CurrentToken.Kind != ExpressionTokenKind.End && this.Lexer.CurrentToken.Kind != ExpressionTokenKind.Comma && this.Lexer.CurrentToken.Kind != ExpressionTokenKind.SemiColon;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004C28 File Offset: 0x00002E28
		private string ReadQueryOption()
		{
			if (this.Lexer.CurrentToken.Kind != ExpressionTokenKind.Equal)
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.Lexer.ExpressionText));
			}
			this.Lexer.NextToken();
			string text = this.Lexer.ExpressionText.Substring(this.Lexer.Position);
			text = text.Split(new char[] { ';' }).First<string>();
			while (this.Lexer.PeekNextToken().Kind != ExpressionTokenKind.SemiColon)
			{
				this.Lexer.NextToken();
			}
			this.Lexer.NextToken();
			return text;
		}
	}
}
