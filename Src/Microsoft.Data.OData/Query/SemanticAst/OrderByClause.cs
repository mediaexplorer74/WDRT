using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200007B RID: 123
	public sealed class OrderByClause
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0000AE3A File Offset: 0x0000903A
		public OrderByClause(OrderByClause thenBy, SingleValueNode expression, OrderByDirection direction, RangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<RangeVariable>(rangeVariable, "parameter");
			this.thenBy = thenBy;
			this.expression = expression;
			this.direction = direction;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000AE76 File Offset: 0x00009076
		public OrderByClause ThenBy
		{
			get
			{
				return this.thenBy;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000AE7E File Offset: 0x0000907E
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000AE86 File Offset: 0x00009086
		public OrderByDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000AE8E File Offset: 0x0000908E
		public RangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000AE96 File Offset: 0x00009096
		public IEdmTypeReference ItemType
		{
			get
			{
				return this.RangeVariable.TypeReference;
			}
		}

		// Token: 0x040000D3 RID: 211
		private readonly SingleValueNode expression;

		// Token: 0x040000D4 RID: 212
		private readonly OrderByDirection direction;

		// Token: 0x040000D5 RID: 213
		private readonly RangeVariable rangeVariable;

		// Token: 0x040000D6 RID: 214
		private readonly OrderByClause thenBy;
	}
}
