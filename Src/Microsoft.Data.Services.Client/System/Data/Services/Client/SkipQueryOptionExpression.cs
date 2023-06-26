using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x020000D9 RID: 217
	[DebuggerDisplay("SkipQueryOptionExpression {SkipAmount}")]
	internal class SkipQueryOptionExpression : QueryOptionExpression
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x0001CEAD File Offset: 0x0001B0AD
		internal SkipQueryOptionExpression(Type type, ConstantExpression skipAmount)
			: base(type)
		{
			this.skipAmount = skipAmount;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001CEBD File Offset: 0x0001B0BD
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10004;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001CEC4 File Offset: 0x0001B0C4
		internal ConstantExpression SkipAmount
		{
			get
			{
				return this.skipAmount;
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001CECC File Offset: 0x0001B0CC
		internal override QueryOptionExpression ComposeMultipleSpecification(QueryOptionExpression previous)
		{
			int num = (int)this.skipAmount.Value;
			int num2 = (int)((SkipQueryOptionExpression)previous).skipAmount.Value;
			return new SkipQueryOptionExpression(this.Type, Expression.Constant(num + num2, typeof(int)));
		}

		// Token: 0x04000443 RID: 1091
		private ConstantExpression skipAmount;
	}
}
