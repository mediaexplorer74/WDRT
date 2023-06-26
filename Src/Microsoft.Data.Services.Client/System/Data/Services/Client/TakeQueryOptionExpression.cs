using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x020000DA RID: 218
	[DebuggerDisplay("TakeQueryOptionExpression {TakeAmount}")]
	internal class TakeQueryOptionExpression : QueryOptionExpression
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x0001CF22 File Offset: 0x0001B122
		internal TakeQueryOptionExpression(Type type, ConstantExpression takeAmount)
			: base(type)
		{
			this.takeAmount = takeAmount;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001CF32 File Offset: 0x0001B132
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10003;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001CF39 File Offset: 0x0001B139
		internal ConstantExpression TakeAmount
		{
			get
			{
				return this.takeAmount;
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001CF44 File Offset: 0x0001B144
		internal override QueryOptionExpression ComposeMultipleSpecification(QueryOptionExpression previous)
		{
			int num = (int)this.takeAmount.Value;
			int num2 = (int)((TakeQueryOptionExpression)previous).takeAmount.Value;
			if (num >= num2)
			{
				return previous;
			}
			return this;
		}

		// Token: 0x04000444 RID: 1092
		private ConstantExpression takeAmount;
	}
}
