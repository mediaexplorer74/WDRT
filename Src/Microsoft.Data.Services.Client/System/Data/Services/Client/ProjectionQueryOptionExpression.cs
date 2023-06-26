using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x020000BB RID: 187
	internal class ProjectionQueryOptionExpression : QueryOptionExpression
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x00017446 File Offset: 0x00015646
		internal ProjectionQueryOptionExpression(Type type, LambdaExpression lambda, List<string> paths)
			: base(type)
		{
			this.lambda = lambda;
			this.paths = paths;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001745D File Offset: 0x0001565D
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10008;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00017464 File Offset: 0x00015664
		internal LambdaExpression Selector
		{
			get
			{
				return this.lambda;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001746C File Offset: 0x0001566C
		internal List<string> Paths
		{
			get
			{
				return this.paths;
			}
		}

		// Token: 0x04000335 RID: 821
		private readonly LambdaExpression lambda;

		// Token: 0x04000336 RID: 822
		private readonly List<string> paths;
	}
}
