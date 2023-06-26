using System;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x020000BA RID: 186
	internal abstract class QueryOptionExpression : Expression
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x0001742C File Offset: 0x0001562C
		internal QueryOptionExpression(Type type)
		{
			this.type = type;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001743B File Offset: 0x0001563B
		public override Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00017443 File Offset: 0x00015643
		internal virtual QueryOptionExpression ComposeMultipleSpecification(QueryOptionExpression previous)
		{
			return this;
		}

		// Token: 0x04000334 RID: 820
		private Type type;
	}
}
