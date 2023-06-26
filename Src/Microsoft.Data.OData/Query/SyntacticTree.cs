using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000D4 RID: 212
	internal sealed class SyntacticTree
	{
		// Token: 0x06000530 RID: 1328 RVA: 0x00011B68 File Offset: 0x0000FD68
		public SyntacticTree(ICollection<string> path, QueryToken filter, IEnumerable<OrderByToken> orderByTokens, SelectToken select, ExpandToken expand, int? skip, int? top, InlineCountKind? inlineCount, string format, IEnumerable<CustomQueryOptionToken> queryOptions)
		{
			ExceptionUtils.CheckArgumentNotNull<ICollection<string>>(path, "path");
			this.path = path;
			this.filter = filter;
			this.orderByTokens = new ReadOnlyEnumerableForUriParser<OrderByToken>(orderByTokens ?? ((IEnumerable<OrderByToken>)new OrderByToken[0]));
			this.select = select;
			this.expand = expand;
			this.skip = skip;
			this.top = top;
			this.inlineCount = inlineCount;
			this.format = format;
			this.queryOptions = new ReadOnlyEnumerableForUriParser<CustomQueryOptionToken>(queryOptions ?? ((IEnumerable<CustomQueryOptionToken>)new CustomQueryOptionToken[0]));
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00011BFB File Offset: 0x0000FDFB
		public ICollection<string> Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00011C03 File Offset: 0x0000FE03
		public QueryToken Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00011C0B File Offset: 0x0000FE0B
		public IEnumerable<OrderByToken> OrderByTokens
		{
			get
			{
				return this.orderByTokens;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00011C13 File Offset: 0x0000FE13
		public SelectToken Select
		{
			get
			{
				return this.select;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00011C1B File Offset: 0x0000FE1B
		public ExpandToken Expand
		{
			get
			{
				return this.expand;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00011C23 File Offset: 0x0000FE23
		public int? Skip
		{
			get
			{
				return this.skip;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00011C2B File Offset: 0x0000FE2B
		public int? Top
		{
			get
			{
				return this.top;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00011C33 File Offset: 0x0000FE33
		public string Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00011C3B File Offset: 0x0000FE3B
		public InlineCountKind? InlineCount
		{
			get
			{
				return this.inlineCount;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x00011C43 File Offset: 0x0000FE43
		public IEnumerable<CustomQueryOptionToken> QueryOptions
		{
			get
			{
				return this.queryOptions;
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00011C4B File Offset: 0x0000FE4B
		public static SyntacticTree ParseUri(Uri queryUri, Uri serviceBaseUri)
		{
			return SyntacticTree.ParseUri(queryUri, serviceBaseUri, 800);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00011C5C File Offset: 0x0000FE5C
		public static SyntacticTree ParseUri(Uri queryUri, Uri serviceBaseUri, int maxDepth)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(queryUri, "queryUri");
			if (!queryUri.IsAbsoluteUri)
			{
				throw new ArgumentException(Strings.SyntacticTree_UriMustBeAbsolute(queryUri), "queryUri");
			}
			ExceptionUtils.CheckArgumentNotNull<Uri>(serviceBaseUri, "serviceBaseUri");
			if (!serviceBaseUri.IsAbsoluteUri)
			{
				throw new ArgumentException(Strings.SyntacticTree_UriMustBeAbsolute(serviceBaseUri), "serviceBaseUri");
			}
			if (maxDepth <= 0)
			{
				throw new ArgumentException(Strings.SyntacticTree_MaxDepthInvalid, "maxDepth");
			}
			UriPathParser uriPathParser = new UriPathParser(maxDepth);
			ICollection<string> collection = uriPathParser.ParsePathIntoSegments(queryUri, serviceBaseUri);
			List<CustomQueryOptionToken> list = UriUtils.ParseQueryOptions(queryUri);
			QueryToken queryToken = null;
			string queryOptionValueAndRemove = list.GetQueryOptionValueAndRemove("$filter");
			if (queryOptionValueAndRemove != null)
			{
				UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(maxDepth);
				queryToken = uriQueryExpressionParser.ParseFilter(queryOptionValueAndRemove);
			}
			IEnumerable<OrderByToken> enumerable = null;
			string queryOptionValueAndRemove2 = list.GetQueryOptionValueAndRemove("$orderby");
			if (queryOptionValueAndRemove2 != null)
			{
				UriQueryExpressionParser uriQueryExpressionParser2 = new UriQueryExpressionParser(maxDepth);
				enumerable = uriQueryExpressionParser2.ParseOrderBy(queryOptionValueAndRemove2);
			}
			SelectToken selectToken = null;
			string queryOptionValueAndRemove3 = list.GetQueryOptionValueAndRemove("$select");
			if (queryOptionValueAndRemove3 != null)
			{
				ISelectExpandTermParser selectExpandTermParser = SelectExpandTermParserFactory.Create(queryOptionValueAndRemove3);
				selectToken = selectExpandTermParser.ParseSelect();
			}
			ExpandToken expandToken = null;
			string queryOptionValueAndRemove4 = list.GetQueryOptionValueAndRemove("$expand");
			if (queryOptionValueAndRemove4 != null)
			{
				ISelectExpandTermParser selectExpandTermParser2 = SelectExpandTermParserFactory.Create(queryOptionValueAndRemove4);
				expandToken = selectExpandTermParser2.ParseExpand();
			}
			int? num = null;
			string queryOptionValueAndRemove5 = list.GetQueryOptionValueAndRemove("$skip");
			if (queryOptionValueAndRemove5 != null)
			{
				int num2;
				if (!UriPrimitiveTypeParser.TryUriStringToNonNegativeInteger(queryOptionValueAndRemove5, out num2))
				{
					throw new ODataException(Strings.SyntacticTree_InvalidSkipQueryOptionValue(queryOptionValueAndRemove5));
				}
				num = new int?(num2);
			}
			int? num3 = null;
			string queryOptionValueAndRemove6 = list.GetQueryOptionValueAndRemove("$top");
			if (queryOptionValueAndRemove6 != null)
			{
				int num4;
				if (!UriPrimitiveTypeParser.TryUriStringToNonNegativeInteger(queryOptionValueAndRemove6, out num4))
				{
					throw new ODataException(Strings.SyntacticTree_InvalidTopQueryOptionValue(queryOptionValueAndRemove6));
				}
				num3 = new int?(num4);
			}
			string queryOptionValueAndRemove7 = list.GetQueryOptionValueAndRemove("$inlinecount");
			InlineCountKind? inlineCountKind = QueryTokenUtils.ParseInlineCountKind(queryOptionValueAndRemove7);
			string queryOptionValueAndRemove8 = list.GetQueryOptionValueAndRemove("$format");
			return new SyntacticTree(collection, queryToken, enumerable, selectToken, expandToken, num, num3, inlineCountKind, queryOptionValueAndRemove8, (list.Count == 0) ? null : new ReadOnlyCollection<CustomQueryOptionToken>(list));
		}

		// Token: 0x040001ED RID: 493
		private const int DefaultMaxDepth = 800;

		// Token: 0x040001EE RID: 494
		private readonly ICollection<string> path;

		// Token: 0x040001EF RID: 495
		private readonly QueryToken filter;

		// Token: 0x040001F0 RID: 496
		private readonly IEnumerable<OrderByToken> orderByTokens;

		// Token: 0x040001F1 RID: 497
		private readonly SelectToken select;

		// Token: 0x040001F2 RID: 498
		private readonly ExpandToken expand;

		// Token: 0x040001F3 RID: 499
		private readonly int? skip;

		// Token: 0x040001F4 RID: 500
		private readonly int? top;

		// Token: 0x040001F5 RID: 501
		private readonly string format;

		// Token: 0x040001F6 RID: 502
		private readonly InlineCountKind? inlineCount;

		// Token: 0x040001F7 RID: 503
		private readonly IEnumerable<CustomQueryOptionToken> queryOptions;
	}
}
