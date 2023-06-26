using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Library.Expressions
{
	// Token: 0x02000185 RID: 389
	public class EdmLabeledExpressionReferenceExpression : EdmElement, IEdmLabeledExpressionReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000896 RID: 2198 RVA: 0x00018061 File Offset: 0x00016261
		public EdmLabeledExpressionReferenceExpression()
		{
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00018069 File Offset: 0x00016269
		public EdmLabeledExpressionReferenceExpression(IEdmLabeledExpression referencedLabeledExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmLabeledExpression>(referencedLabeledExpression, "referencedLabeledExpression");
			this.referencedLabeledExpression = referencedLabeledExpression;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x00018084 File Offset: 0x00016284
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x0001808C File Offset: 0x0001628C
		public IEdmLabeledExpression ReferencedLabeledExpression
		{
			get
			{
				return this.referencedLabeledExpression;
			}
			set
			{
				EdmUtil.CheckArgumentNull<IEdmLabeledExpression>(value, "value");
				if (this.referencedLabeledExpression != null)
				{
					throw new InvalidOperationException(Strings.ValueHasAlreadyBeenSet);
				}
				this.referencedLabeledExpression = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x000180B4 File Offset: 0x000162B4
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.LabeledExpressionReference;
			}
		}

		// Token: 0x04000443 RID: 1091
		private IEdmLabeledExpression referencedLabeledExpression;
	}
}
