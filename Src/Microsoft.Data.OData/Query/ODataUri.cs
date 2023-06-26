using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000D3 RID: 211
	internal sealed class ODataUri
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x00011ACC File Offset: 0x0000FCCC
		public ODataUri(ODataPath path, IEnumerable<QueryNode> customQueryOptions, SelectExpandClause selectAndExpand, FilterClause filter, OrderByClause orderby, long? skip, long? top, InlineCountKind? inlineCount)
		{
			this.path = path;
			this.customQueryOptions = new ReadOnlyCollection<QueryNode>(customQueryOptions.ToList<QueryNode>());
			this.selectAndExpand = selectAndExpand;
			this.filter = filter;
			this.orderBy = orderby;
			this.skip = skip;
			this.top = top;
			this.inlineCount = inlineCount;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00011B26 File Offset: 0x0000FD26
		public ODataPath Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00011B2E File Offset: 0x0000FD2E
		public IEnumerable<QueryNode> CustomQueryOptions
		{
			get
			{
				return this.customQueryOptions;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00011B36 File Offset: 0x0000FD36
		public SelectExpandClause SelectAndExpand
		{
			get
			{
				return this.selectAndExpand;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00011B3E File Offset: 0x0000FD3E
		public FilterClause Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00011B46 File Offset: 0x0000FD46
		public OrderByClause OrderBy
		{
			get
			{
				return this.orderBy;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00011B4E File Offset: 0x0000FD4E
		public long? Skip
		{
			get
			{
				return this.skip;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00011B56 File Offset: 0x0000FD56
		public long? Top
		{
			get
			{
				return this.top;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00011B5E File Offset: 0x0000FD5E
		public InlineCountKind? InlineCount
		{
			get
			{
				return this.inlineCount;
			}
		}

		// Token: 0x040001E5 RID: 485
		private readonly ODataPath path;

		// Token: 0x040001E6 RID: 486
		private readonly IEnumerable<QueryNode> customQueryOptions;

		// Token: 0x040001E7 RID: 487
		private readonly SelectExpandClause selectAndExpand;

		// Token: 0x040001E8 RID: 488
		private readonly FilterClause filter;

		// Token: 0x040001E9 RID: 489
		private readonly OrderByClause orderBy;

		// Token: 0x040001EA RID: 490
		private readonly long? skip;

		// Token: 0x040001EB RID: 491
		private readonly long? top;

		// Token: 0x040001EC RID: 492
		private readonly InlineCountKind? inlineCount;
	}
}
