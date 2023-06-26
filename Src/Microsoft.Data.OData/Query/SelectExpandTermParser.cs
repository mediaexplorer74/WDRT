using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000025 RID: 37
	internal abstract class SelectExpandTermParser : ISelectExpandTermParser
	{
		// Token: 0x060000EE RID: 238 RVA: 0x0000456E File Offset: 0x0000276E
		protected SelectExpandTermParser(string clauseToParse, int maxDepth)
		{
			this.maxDepth = maxDepth;
			this.recursionDepth = 0;
			this.Lexer = ((clauseToParse != null) ? new ExpressionLexer(clauseToParse, false, true) : null);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004598 File Offset: 0x00002798
		public int MaxDepth
		{
			get
			{
				return this.maxDepth;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000045A0 File Offset: 0x000027A0
		public SelectToken ParseSelect()
		{
			this.isSelect = true;
			if (this.Lexer == null)
			{
				return new SelectToken(new List<PathSegmentToken>());
			}
			List<PathSegmentToken> list = new List<PathSegmentToken>();
			bool flag = this.Lexer.CurrentToken.Kind == ExpressionTokenKind.Equal;
			while (this.Lexer.PeekNextToken().Kind != ExpressionTokenKind.End && this.Lexer.PeekNextToken().Kind != ExpressionTokenKind.CloseParen)
			{
				list.Add(this.ParseSingleSelectTerm(flag));
			}
			return new SelectToken(list);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004620 File Offset: 0x00002820
		public ExpandToken ParseExpand()
		{
			this.isSelect = false;
			if (this.Lexer == null)
			{
				return new ExpandToken(new List<ExpandTermToken>());
			}
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			bool flag = this.Lexer.CurrentToken.Kind == ExpressionTokenKind.Equal;
			while (this.Lexer.PeekNextToken().Kind != ExpressionTokenKind.End && this.Lexer.PeekNextToken().Kind != ExpressionTokenKind.CloseParen)
			{
				list.Add(this.ParseSingleExpandTerm(flag));
			}
			return new ExpandToken(list);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000046A0 File Offset: 0x000028A0
		public PathSegmentToken ParseSingleSelectTerm(bool isInnerTerm)
		{
			this.isSelect = true;
			PathSegmentToken pathSegmentToken = this.ParseSelectExpandProperty();
			if (this.IsNotEndOfTerm(isInnerTerm))
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.Lexer.ExpressionText));
			}
			return pathSegmentToken;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000046DC File Offset: 0x000028DC
		public ExpandTermToken ParseSingleExpandTerm(bool isInnerTerm)
		{
			this.isSelect = false;
			this.RecurseEnter();
			PathSegmentToken pathSegmentToken = this.ParseSelectExpandProperty();
			this.RecurseLeave();
			return this.BuildExpandTermToken(isInnerTerm, pathSegmentToken);
		}

		// Token: 0x060000F4 RID: 244
		internal abstract ExpandTermToken BuildExpandTermToken(bool isInnerTerm, PathSegmentToken pathToken);

		// Token: 0x060000F5 RID: 245
		internal abstract bool IsNotEndOfTerm(bool isInnerTerm);

		// Token: 0x060000F6 RID: 246 RVA: 0x0000470C File Offset: 0x0000290C
		private PathSegmentToken ParseSelectExpandProperty()
		{
			PathSegmentToken pathSegmentToken = null;
			int num = 0;
			for (;;)
			{
				num++;
				if (num > this.maxDepth)
				{
					break;
				}
				this.Lexer.NextToken();
				if (num > 1 && this.Lexer.CurrentToken.Kind == ExpressionTokenKind.End)
				{
					return pathSegmentToken;
				}
				pathSegmentToken = this.ParseNext(pathSegmentToken);
				if (this.Lexer.CurrentToken.Kind != ExpressionTokenKind.Slash)
				{
					return pathSegmentToken;
				}
			}
			throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004778 File Offset: 0x00002978
		private PathSegmentToken ParseNext(PathSegmentToken previousToken)
		{
			if (this.Lexer.CurrentToken.Text.StartsWith("$", StringComparison.CurrentCulture))
			{
				throw new ODataException(Strings.UriSelectParser_SystemTokenInSelectExpand(this.Lexer.CurrentToken.Text, this.Lexer.ExpressionText));
			}
			return this.ParseSegment(previousToken);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000047D0 File Offset: 0x000029D0
		private PathSegmentToken ParseSegment(PathSegmentToken parent)
		{
			string text;
			if (this.Lexer.PeekNextToken().Kind == ExpressionTokenKind.Dot)
			{
				text = this.Lexer.ReadDottedIdentifier(this.isSelect);
			}
			else if (this.Lexer.CurrentToken.Kind == ExpressionTokenKind.Star)
			{
				if (this.Lexer.PeekNextToken().Kind == ExpressionTokenKind.Slash)
				{
					throw new ODataException(Strings.ExpressionToken_IdentifierExpected(this.Lexer.Position));
				}
				text = this.Lexer.CurrentToken.Text;
				this.Lexer.NextToken();
			}
			else
			{
				text = this.Lexer.CurrentToken.GetIdentifier();
				this.Lexer.NextToken();
			}
			return new NonSystemToken(text, null, parent);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004892 File Offset: 0x00002A92
		private void RecurseEnter()
		{
			this.recursionDepth++;
			if (this.recursionDepth > this.maxDepth)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000048BB File Offset: 0x00002ABB
		private void RecurseLeave()
		{
			this.recursionDepth--;
		}

		// Token: 0x04000052 RID: 82
		public ExpressionLexer Lexer;

		// Token: 0x04000053 RID: 83
		private bool isSelect;

		// Token: 0x04000054 RID: 84
		private int maxDepth;

		// Token: 0x04000055 RID: 85
		private int recursionDepth;
	}
}
