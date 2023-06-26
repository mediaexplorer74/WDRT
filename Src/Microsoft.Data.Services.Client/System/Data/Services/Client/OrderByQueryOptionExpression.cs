using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x020000CB RID: 203
	internal class OrderByQueryOptionExpression : QueryOptionExpression
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x00019D25 File Offset: 0x00017F25
		internal OrderByQueryOptionExpression(Type type, List<OrderByQueryOptionExpression.Selector> selectors)
			: base(type)
		{
			this.selectors = selectors;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00019D35 File Offset: 0x00017F35
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10005;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00019D3C File Offset: 0x00017F3C
		internal List<OrderByQueryOptionExpression.Selector> Selectors
		{
			get
			{
				return this.selectors;
			}
		}

		// Token: 0x0400040F RID: 1039
		private List<OrderByQueryOptionExpression.Selector> selectors;

		// Token: 0x020000CC RID: 204
		internal struct Selector
		{
			// Token: 0x06000676 RID: 1654 RVA: 0x00019D44 File Offset: 0x00017F44
			internal Selector(Expression e, bool descending)
			{
				this.Expression = e;
				this.Descending = descending;
			}

			// Token: 0x04000410 RID: 1040
			internal readonly Expression Expression;

			// Token: 0x04000411 RID: 1041
			internal readonly bool Descending;
		}
	}
}
