using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Library.Expressions
{
	// Token: 0x0200019E RID: 414
	public class EdmLabeledExpression : EdmElement, IEdmLabeledExpression, IEdmNamedElement, IEdmExpression, IEdmElement
	{
		// Token: 0x06000907 RID: 2311 RVA: 0x000186E5 File Offset: 0x000168E5
		public EdmLabeledExpression(string name, IEdmExpression expression)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			this.name = name;
			this.expression = expression;
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00018713 File Offset: 0x00016913
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001871B File Offset: 0x0001691B
		public IEdmExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00018723 File Offset: 0x00016923
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x0400046C RID: 1132
		private readonly string name;

		// Token: 0x0400046D RID: 1133
		private readonly IEdmExpression expression;
	}
}
