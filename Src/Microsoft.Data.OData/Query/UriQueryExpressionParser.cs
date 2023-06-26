using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000DE RID: 222
	internal sealed class UriQueryExpressionParser
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x00012870 File Offset: 0x00010A70
		internal UriQueryExpressionParser(int maxDepth)
		{
			this.maxDepth = maxDepth;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000128A8 File Offset: 0x00010AA8
		internal static LiteralToken TryParseLiteral(ExpressionLexer lexer)
		{
			switch (lexer.CurrentToken.Kind)
			{
			case ExpressionTokenKind.NullLiteral:
				return UriQueryExpressionParser.ParseNullLiteral(lexer);
			case ExpressionTokenKind.BooleanLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetBoolean(false), "Edm.Boolean");
			case ExpressionTokenKind.StringLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetString(true), "Edm.String");
			case ExpressionTokenKind.IntegerLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetInt32(false), "Edm.Int32");
			case ExpressionTokenKind.Int64Literal:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetInt64(false), "Edm.Int64");
			case ExpressionTokenKind.SingleLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetSingle(false), "Edm.Single");
			case ExpressionTokenKind.DateTimeLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTime, false), "Edm.DateTime");
			case ExpressionTokenKind.DateTimeOffsetLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false), "Edm.DateTimeOffset");
			case ExpressionTokenKind.TimeLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Time, false), "Edm.Time");
			case ExpressionTokenKind.DecimalLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetDecimal(false), "Edm.Decimal");
			case ExpressionTokenKind.DoubleLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetDouble(false), "Edm.Double");
			case ExpressionTokenKind.GuidLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetGuid(false), "Edm.Guid");
			case ExpressionTokenKind.BinaryLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetBinary(true), "Edm.Binary");
			case ExpressionTokenKind.GeographyLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.Geography, false), "Edm.Geography");
			case ExpressionTokenKind.GeometryLiteral:
				return UriQueryExpressionParser.ParseTypedLiteral(lexer, EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.Geometry, false), "Edm.Geometry");
			default:
				return null;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00012A5C File Offset: 0x00010C5C
		internal QueryToken ParseFilter(string filter)
		{
			this.recursionDepth = 0;
			this.lexer = UriQueryExpressionParser.CreateLexerForFilterOrOrderByExpression(filter);
			QueryToken queryToken = this.ParseExpression();
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return queryToken;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00012A90 File Offset: 0x00010C90
		internal IEnumerable<OrderByToken> ParseOrderBy(string orderBy)
		{
			this.recursionDepth = 0;
			this.lexer = UriQueryExpressionParser.CreateLexerForFilterOrOrderByExpression(orderBy);
			List<OrderByToken> list = new List<OrderByToken>();
			for (;;)
			{
				QueryToken queryToken = this.ParseExpression();
				bool flag = true;
				if (this.TokenIdentifierIs("asc"))
				{
					this.lexer.NextToken();
				}
				else if (this.TokenIdentifierIs("desc"))
				{
					this.lexer.NextToken();
					flag = false;
				}
				OrderByToken orderByToken = new OrderByToken(queryToken, flag ? OrderByDirection.Ascending : OrderByDirection.Descending);
				list.Add(orderByToken);
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Comma)
				{
					break;
				}
				this.lexer.NextToken();
			}
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return new ReadOnlyCollection<OrderByToken>(list);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00012B3E File Offset: 0x00010D3E
		private static ExpressionLexer CreateLexerForFilterOrOrderByExpression(string expression)
		{
			return new ExpressionLexer(expression, true, false, true);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00012B49 File Offset: 0x00010D49
		private static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00012B54 File Offset: 0x00010D54
		private static LiteralToken ParseTypedLiteral(ExpressionLexer lexer, IEdmPrimitiveTypeReference targetTypeReference, string targetTypeName)
		{
			object obj;
			if (!UriPrimitiveTypeParser.TryUriStringToPrimitive(lexer.CurrentToken.Text, targetTypeReference, out obj))
			{
				string text = Strings.UriQueryExpressionParser_UnrecognizedLiteral(targetTypeName, lexer.CurrentToken.Text, lexer.CurrentToken.Position, lexer.ExpressionText);
				throw UriQueryExpressionParser.ParseError(text);
			}
			LiteralToken literalToken = new LiteralToken(obj, lexer.CurrentToken.Text);
			lexer.NextToken();
			return literalToken;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00012BC0 File Offset: 0x00010DC0
		private static LiteralToken ParseNullLiteral(ExpressionLexer lexer)
		{
			LiteralToken literalToken = new LiteralToken(null, lexer.CurrentToken.Text);
			lexer.NextToken();
			return literalToken;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00012BE8 File Offset: 0x00010DE8
		private QueryToken ParseExpression()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseLogicalOr();
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00012C0C File Offset: 0x00010E0C
		private QueryToken ParseLogicalOr()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseLogicalAnd();
			while (this.TokenIdentifierIs("or"))
			{
				this.lexer.NextToken();
				QueryToken queryToken2 = this.ParseLogicalAnd();
				queryToken = new BinaryOperatorToken(BinaryOperatorKind.Or, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00012C58 File Offset: 0x00010E58
		private QueryToken ParseLogicalAnd()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseComparison();
			while (this.TokenIdentifierIs("and"))
			{
				this.lexer.NextToken();
				QueryToken queryToken2 = this.ParseComparison();
				queryToken = new BinaryOperatorToken(BinaryOperatorKind.And, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00012CA4 File Offset: 0x00010EA4
		private QueryToken ParseComparison()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseAdditive();
			while (this.lexer.CurrentToken.IsComparisonOperator)
			{
				string text;
				if ((text = this.lexer.CurrentToken.Text) != null)
				{
					if (<PrivateImplementationDetails>{E8251738-41A8-451C-AA83-3F2B33F0CD5E}.$$method0x6000538-1 == null)
					{
						<PrivateImplementationDetails>{E8251738-41A8-451C-AA83-3F2B33F0CD5E}.$$method0x6000538-1 = new Dictionary<string, int>(6)
						{
							{ "eq", 0 },
							{ "ne", 1 },
							{ "gt", 2 },
							{ "ge", 3 },
							{ "lt", 4 },
							{ "le", 5 }
						};
					}
					int num;
					if (<PrivateImplementationDetails>{E8251738-41A8-451C-AA83-3F2B33F0CD5E}.$$method0x6000538-1.TryGetValue(text, out num))
					{
						BinaryOperatorKind binaryOperatorKind;
						switch (num)
						{
						case 0:
							binaryOperatorKind = BinaryOperatorKind.Equal;
							break;
						case 1:
							binaryOperatorKind = BinaryOperatorKind.NotEqual;
							break;
						case 2:
							binaryOperatorKind = BinaryOperatorKind.GreaterThan;
							break;
						case 3:
							binaryOperatorKind = BinaryOperatorKind.GreaterThanOrEqual;
							break;
						case 4:
							binaryOperatorKind = BinaryOperatorKind.LessThan;
							break;
						case 5:
							binaryOperatorKind = BinaryOperatorKind.LessThanOrEqual;
							break;
						default:
							goto IL_D1;
						}
						this.lexer.NextToken();
						QueryToken queryToken2 = this.ParseAdditive();
						queryToken = new BinaryOperatorToken(binaryOperatorKind, queryToken, queryToken2);
						continue;
					}
				}
				IL_D1:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.UriQueryExpressionParser_ParseComparison));
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00012DD0 File Offset: 0x00010FD0
		private QueryToken ParseAdditive()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseMultiplicative();
			while (this.lexer.CurrentToken.IdentifierIs("add") || this.lexer.CurrentToken.IdentifierIs("sub"))
			{
				BinaryOperatorKind binaryOperatorKind;
				if (this.lexer.CurrentToken.IdentifierIs("add"))
				{
					binaryOperatorKind = BinaryOperatorKind.Add;
				}
				else
				{
					binaryOperatorKind = BinaryOperatorKind.Subtract;
				}
				this.lexer.NextToken();
				QueryToken queryToken2 = this.ParseMultiplicative();
				queryToken = new BinaryOperatorToken(binaryOperatorKind, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00012E68 File Offset: 0x00011068
		private QueryToken ParseMultiplicative()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseUnary();
			while (this.lexer.CurrentToken.IdentifierIs("mul") || this.lexer.CurrentToken.IdentifierIs("div") || this.lexer.CurrentToken.IdentifierIs("mod"))
			{
				BinaryOperatorKind binaryOperatorKind;
				if (this.lexer.CurrentToken.IdentifierIs("mul"))
				{
					binaryOperatorKind = BinaryOperatorKind.Multiply;
				}
				else if (this.lexer.CurrentToken.IdentifierIs("div"))
				{
					binaryOperatorKind = BinaryOperatorKind.Divide;
				}
				else
				{
					binaryOperatorKind = BinaryOperatorKind.Modulo;
				}
				this.lexer.NextToken();
				QueryToken queryToken2 = this.ParseUnary();
				queryToken = new BinaryOperatorToken(binaryOperatorKind, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00012F40 File Offset: 0x00011140
		private QueryToken ParseUnary()
		{
			this.RecurseEnter();
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.Minus && !this.lexer.CurrentToken.IdentifierIs("not"))
			{
				this.RecurseLeave();
				return this.ParsePrimary();
			}
			ExpressionToken currentToken = this.lexer.CurrentToken;
			this.lexer.NextToken();
			if (currentToken.Kind == ExpressionTokenKind.Minus && ExpressionLexerUtils.IsNumeric(this.lexer.CurrentToken.Kind))
			{
				ExpressionToken currentToken2 = this.lexer.CurrentToken;
				currentToken2.Text = "-" + currentToken2.Text;
				currentToken2.Position = currentToken.Position;
				this.lexer.CurrentToken = currentToken2;
				this.RecurseLeave();
				return this.ParsePrimary();
			}
			QueryToken queryToken = this.ParseUnary();
			UnaryOperatorKind unaryOperatorKind;
			if (currentToken.Kind == ExpressionTokenKind.Minus)
			{
				unaryOperatorKind = UnaryOperatorKind.Negate;
			}
			else
			{
				unaryOperatorKind = UnaryOperatorKind.Not;
			}
			this.RecurseLeave();
			return new UnaryOperatorToken(unaryOperatorKind, queryToken);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001303C File Offset: 0x0001123C
		private QueryToken ParsePrimary()
		{
			this.RecurseEnter();
			QueryToken queryToken;
			if (this.lexer.PeekNextToken().Kind == ExpressionTokenKind.Slash)
			{
				queryToken = this.ParseSegment(null);
			}
			else
			{
				queryToken = this.ParsePrimaryStart();
			}
			while (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Slash)
			{
				this.lexer.NextToken();
				if (this.lexer.CurrentToken.Text == "any")
				{
					queryToken = this.ParseAny(queryToken);
				}
				else if (this.lexer.CurrentToken.Text == "all")
				{
					queryToken = this.ParseAll(queryToken);
				}
				else if (this.lexer.PeekNextToken().Kind == ExpressionTokenKind.Slash)
				{
					queryToken = this.ParseSegment(queryToken);
				}
				else
				{
					IdentifierTokenizer identifierTokenizer = new IdentifierTokenizer(this.parameters, new FunctionCallParser(this.lexer, new UriQueryExpressionParser.Parser(this.ParseExpression)));
					queryToken = identifierTokenizer.ParseIdentifier(queryToken);
				}
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001313C File Offset: 0x0001133C
		private QueryToken ParsePrimaryStart()
		{
			ExpressionTokenKind kind = this.lexer.CurrentToken.Kind;
			if (kind == ExpressionTokenKind.Identifier)
			{
				IdentifierTokenizer identifierTokenizer = new IdentifierTokenizer(this.parameters, new FunctionCallParser(this.lexer, new UriQueryExpressionParser.Parser(this.ParseExpression)));
				return identifierTokenizer.ParseIdentifier(null);
			}
			if (kind == ExpressionTokenKind.OpenParen)
			{
				return this.ParseParenExpression();
			}
			if (kind == ExpressionTokenKind.Star)
			{
				IdentifierTokenizer identifierTokenizer2 = new IdentifierTokenizer(this.parameters, new FunctionCallParser(this.lexer, new UriQueryExpressionParser.Parser(this.ParseExpression)));
				return identifierTokenizer2.ParseStarMemberAccess(null);
			}
			QueryToken queryToken = UriQueryExpressionParser.TryParseLiteral(this.lexer);
			if (queryToken == null)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_ExpressionExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			return queryToken;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00013204 File Offset: 0x00011404
		private QueryToken ParseParenExpression()
		{
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_OpenParenExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			QueryToken queryToken = this.ParseExpression();
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_CloseParenOrOperatorExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			return queryToken;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000132AF File Offset: 0x000114AF
		private QueryToken ParseAny(QueryToken parent)
		{
			return this.ParseAnyAll(parent, true);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000132B9 File Offset: 0x000114B9
		private QueryToken ParseAll(QueryToken parent)
		{
			return this.ParseAnyAll(parent, false);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000132C4 File Offset: 0x000114C4
		private QueryToken ParseAnyAll(QueryToken parent, bool isAny)
		{
			this.lexer.NextToken();
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_OpenParenExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.CloseParen)
			{
				this.lexer.NextToken();
				if (isAny)
				{
					return new AnyToken(new LiteralToken(true, "True"), null, parent);
				}
				return new AllToken(new LiteralToken(true, "True"), null, parent);
			}
			else
			{
				string identifier = this.lexer.CurrentToken.GetIdentifier();
				if (!this.parameters.Add(identifier))
				{
					throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_RangeVariableAlreadyDeclared(identifier));
				}
				this.lexer.NextToken();
				this.lexer.ValidateToken(ExpressionTokenKind.Colon);
				this.lexer.NextToken();
				QueryToken queryToken = this.ParseExpression();
				if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
				{
					throw UriQueryExpressionParser.ParseError(Strings.UriQueryExpressionParser_CloseParenOrCommaExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
				}
				this.parameters.Remove(identifier);
				this.lexer.NextToken();
				if (isAny)
				{
					return new AnyToken(queryToken, identifier, parent);
				}
				return new AllToken(queryToken, identifier, parent);
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00013444 File Offset: 0x00011644
		private QueryToken ParseSegment(QueryToken parent)
		{
			string identifier = this.lexer.CurrentToken.GetIdentifier();
			this.lexer.NextToken();
			if (this.parameters.Contains(identifier) && parent == null)
			{
				return new RangeVariableToken(identifier);
			}
			return new InnerPathToken(identifier, parent, null);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00013494 File Offset: 0x00011694
		private bool TokenIdentifierIs(string id)
		{
			return this.lexer.CurrentToken.IdentifierIs(id);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000134B5 File Offset: 0x000116B5
		private void RecurseEnter()
		{
			this.recursionDepth++;
			if (this.recursionDepth > this.maxDepth)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000134DE File Offset: 0x000116DE
		private void RecurseLeave()
		{
			this.recursionDepth--;
		}

		// Token: 0x04000252 RID: 594
		private readonly int maxDepth;

		// Token: 0x04000253 RID: 595
		private readonly HashSet<string> parameters = new HashSet<string>(StringComparer.Ordinal) { "$it" };

		// Token: 0x04000254 RID: 596
		private int recursionDepth;

		// Token: 0x04000255 RID: 597
		private ExpressionLexer lexer;

		// Token: 0x020000DF RID: 223
		// (Invoke) Token: 0x06000576 RID: 1398
		internal delegate QueryToken Parser();
	}
}
