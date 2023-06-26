using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000D1 RID: 209
	internal sealed class OrderByToken : QueryToken
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x00011A48 File Offset: 0x0000FC48
		public OrderByToken(QueryToken expression, OrderByDirection direction)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			this.expression = expression;
			this.direction = direction;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00011A69 File Offset: 0x0000FC69
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.OrderBy;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00011A6C File Offset: 0x0000FC6C
		public OrderByDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00011A74 File Offset: 0x0000FC74
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00011A7C File Offset: 0x0000FC7C
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040001E1 RID: 481
		private readonly OrderByDirection direction;

		// Token: 0x040001E2 RID: 482
		private readonly QueryToken expression;
	}
}
