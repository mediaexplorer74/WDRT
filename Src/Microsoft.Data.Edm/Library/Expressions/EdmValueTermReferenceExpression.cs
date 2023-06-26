using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Library.Expressions
{
	// Token: 0x02000196 RID: 406
	public class EdmValueTermReferenceExpression : EdmElement, IEdmValueTermReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x00018471 File Offset: 0x00016671
		public EdmValueTermReferenceExpression(IEdmExpression baseExpression, IEdmValueTerm term)
			: this(baseExpression, term, null)
		{
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001847C File Offset: 0x0001667C
		public EdmValueTermReferenceExpression(IEdmExpression baseExpression, IEdmValueTerm term, string qualifier)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(baseExpression, "baseExpression");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			this.baseExpression = baseExpression;
			this.term = term;
			this.qualifier = qualifier;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x000184B1 File Offset: 0x000166B1
		public IEdmExpression Base
		{
			get
			{
				return this.baseExpression;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x000184B9 File Offset: 0x000166B9
		public IEdmValueTerm Term
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x000184C1 File Offset: 0x000166C1
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x000184C9 File Offset: 0x000166C9
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.ValueTermReference;
			}
		}

		// Token: 0x0400045B RID: 1115
		private readonly IEdmExpression baseExpression;

		// Token: 0x0400045C RID: 1116
		private readonly IEdmValueTerm term;

		// Token: 0x0400045D RID: 1117
		private readonly string qualifier;
	}
}
