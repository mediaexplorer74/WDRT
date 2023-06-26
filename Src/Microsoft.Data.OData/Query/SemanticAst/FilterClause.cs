using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200007A RID: 122
	public sealed class FilterClause
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000ADF1 File Offset: 0x00008FF1
		public FilterClause(SingleValueNode expression, RangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<RangeVariable>(rangeVariable, "parameter");
			this.expression = expression;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000AE1D File Offset: 0x0000901D
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000AE25 File Offset: 0x00009025
		public RangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000AE2D File Offset: 0x0000902D
		public IEdmTypeReference ItemType
		{
			get
			{
				return this.RangeVariable.TypeReference;
			}
		}

		// Token: 0x040000D1 RID: 209
		private readonly SingleValueNode expression;

		// Token: 0x040000D2 RID: 210
		private readonly RangeVariable rangeVariable;
	}
}
