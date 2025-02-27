﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Spatial;
using System.Text;
using System.Xml;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000B1 RID: 177
	internal sealed class ODataUriBuilder
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x0000D552 File Offset: 0x0000B752
		private ODataUriBuilder(SyntacticTree query)
		{
			this.query = query;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000D56C File Offset: 0x0000B76C
		public static Uri CreateUri(Uri baseUri, SyntacticTree queryDescriptor)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(baseUri, "baseUri");
			ExceptionUtils.CheckArgumentNotNull<SyntacticTree>(queryDescriptor, "queryDescriptor");
			ODataUriBuilder odataUriBuilder = new ODataUriBuilder(queryDescriptor);
			string text = odataUriBuilder.Build();
			if (text.StartsWith("?", StringComparison.Ordinal))
			{
				return new UriBuilder(baseUri)
				{
					Query = text
				}.Uri;
			}
			return new Uri(baseUri, new Uri(text, UriKind.RelativeOrAbsolute));
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		public static string GetUriRepresentation(object clrLiteral)
		{
			StringBuilder stringBuilder = new StringBuilder();
			ODataUriBuilder.WriteClrLiteral(stringBuilder, clrLiteral);
			return stringBuilder.ToString();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000D5F0 File Offset: 0x0000B7F0
		public void WriteQueryDescriptor(SyntacticTree queryDescriptor)
		{
			ExceptionUtils.CheckArgumentNotNull<SyntacticTree>(queryDescriptor, "queryDescriptor");
			this.WritePath(queryDescriptor.Path);
			bool flag = true;
			if (queryDescriptor.Filter != null)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.builder.Append("$filter");
				this.builder.Append("=");
				this.WriteQuery(queryDescriptor.Filter);
			}
			if (queryDescriptor.Select != null && queryDescriptor.Select.Properties.Count<PathSegmentToken>() > 0)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.WriteSelect(queryDescriptor.Select);
			}
			if (queryDescriptor.Expand != null && queryDescriptor.Expand.ExpandTerms.Count<ExpandTermToken>() > 0)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.WriteExpand(queryDescriptor.Expand);
			}
			if (queryDescriptor.OrderByTokens.Count<OrderByToken>() > 0)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.builder.Append("$orderby");
				this.builder.Append("=");
				this.WriteOrderBys(queryDescriptor.OrderByTokens);
			}
			foreach (CustomQueryOptionToken customQueryOptionToken in queryDescriptor.QueryOptions)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.WriteQueryOption(customQueryOptionToken);
			}
			if (queryDescriptor.Top != null)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.builder.Append("$top");
				this.builder.Append("=");
				this.builder.Append(queryDescriptor.Top);
			}
			if (queryDescriptor.Skip != null)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.builder.Append("$skip");
				this.builder.Append("=");
				this.builder.Append(queryDescriptor.Skip);
			}
			if (queryDescriptor.Format != null)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.builder.Append("$format");
				this.builder.Append("=");
				this.builder.Append(queryDescriptor.Format);
			}
			if (queryDescriptor.InlineCount != null)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.builder.Append("$inlinecount");
				this.builder.Append("=");
				this.builder.Append(queryDescriptor.InlineCount.Value.ToText());
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000D88C File Offset: 0x0000BA8C
		internal void Append(string text)
		{
			this.builder.Append(text);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000D89C File Offset: 0x0000BA9C
		internal void WriteQuery(QueryToken queryPart)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(queryPart, "query");
			switch (queryPart.Kind)
			{
			case QueryTokenKind.BinaryOperator:
				this.WriteBinary((BinaryOperatorToken)queryPart);
				return;
			case QueryTokenKind.UnaryOperator:
				this.WriteUnary((UnaryOperatorToken)queryPart);
				return;
			case QueryTokenKind.Literal:
				this.WriteLiteral((LiteralToken)queryPart);
				return;
			case QueryTokenKind.FunctionCall:
				this.WriteFunctionCall((FunctionCallToken)queryPart);
				return;
			case QueryTokenKind.EndPath:
				this.WritePropertyAccess((EndPathToken)queryPart);
				return;
			case QueryTokenKind.OrderBy:
				this.WriteOrderBy((OrderByToken)queryPart);
				return;
			case QueryTokenKind.CustomQueryOption:
				this.WriteQueryOption((CustomQueryOptionToken)queryPart);
				return;
			case QueryTokenKind.Select:
				this.WriteSelect((SelectToken)queryPart);
				return;
			case QueryTokenKind.Star:
				this.WriteStar((StarToken)queryPart);
				return;
			case QueryTokenKind.InnerPath:
				this.WriteNavigationProperty((InnerPathToken)queryPart);
				return;
			}
			ODataUriBuilderUtils.NotSupportedQueryTokenKind(queryPart.Kind);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000D990 File Offset: 0x0000BB90
		private static void WriteClrLiteral(StringBuilder builder, object clrLiteral)
		{
			if (clrLiteral == null)
			{
				builder.Append("null");
				return;
			}
			switch (PlatformHelper.GetTypeCode(clrLiteral.GetType()))
			{
			case TypeCode.Boolean:
				builder.Append(((bool)clrLiteral) ? "true" : "false");
				return;
			case TypeCode.SByte:
				builder.Append(((sbyte)clrLiteral).ToString("D", CultureInfo.InvariantCulture));
				return;
			case TypeCode.Byte:
				builder.Append(((byte)clrLiteral).ToString("D", CultureInfo.InvariantCulture));
				return;
			case TypeCode.Int16:
				builder.Append(((short)clrLiteral).ToString("D", CultureInfo.InvariantCulture));
				return;
			case TypeCode.Int32:
				builder.Append(((int)clrLiteral).ToString("D", CultureInfo.InvariantCulture));
				return;
			case TypeCode.Int64:
				builder.Append(((long)clrLiteral).ToString("D", CultureInfo.InvariantCulture));
				builder.Append("L");
				return;
			case TypeCode.Single:
				builder.Append(((float)clrLiteral).ToString("F", CultureInfo.InvariantCulture));
				builder.Append("f");
				return;
			case TypeCode.Double:
				builder.Append(((double)clrLiteral).ToString("R", ODataUriBuilderUtils.DoubleFormatInfo));
				return;
			case TypeCode.Decimal:
				builder.Append(((decimal)clrLiteral).ToString(ODataUriBuilderUtils.DecimalFormatInfo));
				builder.Append("M");
				return;
			case TypeCode.DateTime:
				builder.Append("datetime");
				builder.Append("'");
				builder.Append(((DateTime)clrLiteral).ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFF", CultureInfo.InvariantCulture));
				builder.Append("'");
				return;
			case TypeCode.String:
				builder.Append("'");
				builder.Append(Uri.EscapeDataString(ODataUriBuilderUtils.Escape(clrLiteral.ToString())));
				builder.Append("'");
				return;
			}
			if (clrLiteral is DateTimeOffset)
			{
				builder.Append("datetimeoffset");
				builder.Append("'");
				builder.Append(((DateTimeOffset)clrLiteral).ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFFzzzzzzz", CultureInfo.InvariantCulture));
				builder.Append("'");
				return;
			}
			if (clrLiteral is TimeSpan)
			{
				builder.Append("time");
				builder.Append("'");
				builder.Append(XmlConvert.ToString((TimeSpan)clrLiteral));
				builder.Append("'");
				return;
			}
			if (clrLiteral is Guid)
			{
				builder.Append("guid");
				builder.Append("'");
				builder.Append(((Guid)clrLiteral).ToString("D"));
				builder.Append("'");
				return;
			}
			byte[] array = clrLiteral as byte[];
			if (array != null)
			{
				builder.Append("binary");
				builder.Append("'");
				foreach (byte b in array)
				{
					builder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
				}
				builder.Append("'");
				return;
			}
			Geography geography = clrLiteral as Geography;
			if (geography != null)
			{
				builder.Append("geography");
				builder.Append("'");
				builder.Append(LiteralUtils.ToWellKnownText(geography));
				builder.Append("'");
				return;
			}
			Geometry geometry = clrLiteral as Geometry;
			if (geometry != null)
			{
				builder.Append("geometry");
				builder.Append("'");
				builder.Append(LiteralUtils.ToWellKnownText(geometry));
				builder.Append("'");
				return;
			}
			ODataUriBuilderUtils.NotSupported(clrLiteral.GetType());
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000DD7F File Offset: 0x0000BF7F
		private string Build()
		{
			this.WriteQueryDescriptor(this.query);
			return this.builder.ToString();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000DD98 File Offset: 0x0000BF98
		private void WriteBinary(BinaryOperatorToken binary)
		{
			ExceptionUtils.CheckArgumentNotNull<BinaryOperatorToken>(binary, "binary");
			BinaryOperatorUriBuilder binaryOperatorUriBuilder = new BinaryOperatorUriBuilder(this);
			binaryOperatorUriBuilder.Write(binary);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		private void WriteFunctionCall(FunctionCallToken functionToken)
		{
			ExceptionUtils.CheckArgumentNotNull<FunctionCallToken>(functionToken, "functionToken");
			this.builder.Append(functionToken.Name);
			this.builder.Append("(");
			bool flag = false;
			foreach (QueryToken queryToken in functionToken.Arguments)
			{
				if (flag)
				{
					this.builder.Append(",");
				}
				else
				{
					flag = true;
				}
				this.WriteQuery(queryToken);
			}
			this.builder.Append(")");
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000DE68 File Offset: 0x0000C068
		private void WritePath(IEnumerable<string> path)
		{
			bool flag = true;
			foreach (string text in path)
			{
				if (!flag)
				{
					this.builder.Append("/");
				}
				this.builder.Append(text);
				flag = false;
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000DED0 File Offset: 0x0000C0D0
		private void WriteLiteral(LiteralToken literal)
		{
			ExceptionUtils.CheckArgumentNotNull<LiteralToken>(literal, "literal");
			ODataUriBuilder.WriteClrLiteral(this.builder, literal.Value);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000DEF0 File Offset: 0x0000C0F0
		private void WriteOrderBys(IEnumerable<OrderByToken> orderBys)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<OrderByToken>>(orderBys, "orderBys");
			bool flag = false;
			foreach (OrderByToken orderByToken in orderBys)
			{
				if (flag)
				{
					this.builder.Append(",");
				}
				this.WriteOrderBy(orderByToken);
				flag = true;
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000DF5C File Offset: 0x0000C15C
		private void WriteOrderBy(OrderByToken orderBy)
		{
			ExceptionUtils.CheckArgumentNotNull<OrderByToken>(orderBy, "orderBy");
			this.WriteQuery(orderBy.Expression);
			if (orderBy.Direction == OrderByDirection.Descending)
			{
				this.builder.Append("%20");
				this.builder.Append("desc");
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000DFAC File Offset: 0x0000C1AC
		private void WritePathSegment(PathSegmentToken segmentToken)
		{
			NonSystemToken nonSystemToken = segmentToken as NonSystemToken;
			if (nonSystemToken != null)
			{
				if (string.IsNullOrEmpty(nonSystemToken.Identifier))
				{
					return;
				}
				if (nonSystemToken.NextToken != null)
				{
					this.WritePathSegment(nonSystemToken.NextToken);
					this.builder.Append("/");
				}
				this.builder.Append(nonSystemToken.Identifier);
				if (nonSystemToken.NamedValues != null)
				{
					this.builder.Append("(");
					bool flag = false;
					foreach (NamedValue namedValue in nonSystemToken.NamedValues)
					{
						if (flag)
						{
							this.builder.Append(",");
						}
						this.builder.Append(namedValue.Name);
						this.builder.Append("=");
						this.WriteLiteral(namedValue.Value);
						flag = true;
					}
					this.builder.Append(")");
				}
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000E0B8 File Offset: 0x0000C2B8
		private void WritePropertyAccess(EndPathToken endPath)
		{
			ExceptionUtils.CheckArgumentNotNull<EndPathToken>(endPath, "endPath");
			if (endPath.NextToken != null)
			{
				this.WriteQuery(endPath.NextToken);
				this.builder.Append("/");
			}
			this.builder.Append(endPath.Identifier);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000E108 File Offset: 0x0000C308
		private void WriteNavigationProperty(InnerPathToken navigation)
		{
			ExceptionUtils.CheckArgumentNotNull<InnerPathToken>(navigation, "navigation");
			if (navigation.NextToken != null)
			{
				this.WriteQuery(navigation.NextToken);
				this.builder.Append("/");
			}
			this.builder.Append(navigation.Identifier);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000E158 File Offset: 0x0000C358
		private void WriteQueryOption(CustomQueryOptionToken queryOption)
		{
			ExceptionUtils.CheckArgumentNotNull<CustomQueryOptionToken>(queryOption, "queryOption");
			this.builder.Append(queryOption.Name);
			this.builder.Append("=");
			this.builder.Append(queryOption.Value);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000E1A5 File Offset: 0x0000C3A5
		private void WriteQueryPrefixOrSeparator(bool writeQueryPrefix)
		{
			if (writeQueryPrefix)
			{
				this.builder.Append("?");
				return;
			}
			this.builder.Append("&");
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000E1D0 File Offset: 0x0000C3D0
		private void WriteSelect(SelectToken selectToken)
		{
			ExceptionUtils.CheckArgumentNotNull<SelectToken>(selectToken, "SelectToken");
			this.builder.Append("$select");
			this.builder.Append("=");
			bool flag = false;
			foreach (PathSegmentToken pathSegmentToken in selectToken.Properties)
			{
				if (flag)
				{
					this.builder.Append(",");
				}
				this.WritePathSegment(pathSegmentToken);
				flag = true;
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000E264 File Offset: 0x0000C464
		private void WriteExpand(ExpandToken expand)
		{
			ExceptionUtils.CheckArgumentNotNull<ExpandToken>(expand, "expandQueryToken");
			this.builder.Append("$expand");
			this.builder.Append("=");
			bool flag = false;
			foreach (ExpandTermToken expandTermToken in expand.ExpandTerms)
			{
				if (flag)
				{
					this.builder.Append(",");
				}
				this.WritePathSegment(expandTermToken.PathToNavProp);
				flag = true;
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		private void WriteStar(StarToken star)
		{
			ExceptionUtils.CheckArgumentNotNull<StarToken>(star, "star");
			if (star.NextToken != null)
			{
				this.WriteQuery(star.NextToken);
				this.builder.Append("/");
			}
			this.builder.Append("*");
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000E34C File Offset: 0x0000C54C
		private void WriteUnary(UnaryOperatorToken unary)
		{
			ExceptionUtils.CheckArgumentNotNull<UnaryOperatorToken>(unary, "unary");
			switch (unary.OperatorKind)
			{
			case UnaryOperatorKind.Negate:
				this.builder.Append("-");
				break;
			case UnaryOperatorKind.Not:
				this.builder.Append("not");
				this.builder.Append("%20");
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataUriBuilder_WriteUnary_UnreachableCodePath));
			}
			this.WriteQuery(unary.Operand);
		}

		// Token: 0x04000162 RID: 354
		private readonly SyntacticTree query;

		// Token: 0x04000163 RID: 355
		private readonly StringBuilder builder = new StringBuilder();
	}
}
