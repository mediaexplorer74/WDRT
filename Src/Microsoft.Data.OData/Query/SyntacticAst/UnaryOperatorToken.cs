using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000DB RID: 219
	internal sealed class UnaryOperatorToken : QueryToken
	{
		// Token: 0x06000557 RID: 1367 RVA: 0x00012830 File Offset: 0x00010A30
		public UnaryOperatorToken(UnaryOperatorKind operatorKind, QueryToken operand)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(operand, "operand");
			this.operatorKind = operatorKind;
			this.operand = operand;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00012851 File Offset: 0x00010A51
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.UnaryOperator;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00012854 File Offset: 0x00010A54
		public UnaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001285C File Offset: 0x00010A5C
		public QueryToken Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00012864 File Offset: 0x00010A64
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400023F RID: 575
		private readonly UnaryOperatorKind operatorKind;

		// Token: 0x04000240 RID: 576
		private readonly QueryToken operand;
	}
}
