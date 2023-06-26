using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x020000BF RID: 191
	[DebuggerDisplay("InputReferenceExpression -> {Type}")]
	internal sealed class InputReferenceExpression : Expression
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x0001890E File Offset: 0x00016B0E
		internal InputReferenceExpression(ResourceExpression target)
		{
			this.target = target;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0001891D File Offset: 0x00016B1D
		public override Type Type
		{
			get
			{
				return this.target.ResourceType;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001892A File Offset: 0x00016B2A
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10007;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00018931 File Offset: 0x00016B31
		internal ResourceExpression Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00018939 File Offset: 0x00016B39
		internal void OverrideTarget(ResourceSetExpression newTarget)
		{
			this.target = newTarget;
		}

		// Token: 0x040003F0 RID: 1008
		private ResourceExpression target;
	}
}
