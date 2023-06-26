using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Library.Expressions
{
	// Token: 0x0200019B RID: 411
	public class EdmIfExpression : EdmElement, IEdmIfExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x000185EC File Offset: 0x000167EC
		public EdmIfExpression(IEdmExpression testExpression, IEdmExpression trueExpression, IEdmExpression falseExpression)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(testExpression, "testExpression");
			EdmUtil.CheckArgumentNull<IEdmExpression>(trueExpression, "trueExpression");
			EdmUtil.CheckArgumentNull<IEdmExpression>(falseExpression, "falseExpression");
			this.testExpression = testExpression;
			this.trueExpression = trueExpression;
			this.falseExpression = falseExpression;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00018638 File Offset: 0x00016838
		public IEdmExpression TestExpression
		{
			get
			{
				return this.testExpression;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00018640 File Offset: 0x00016840
		public IEdmExpression TrueExpression
		{
			get
			{
				return this.trueExpression;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00018648 File Offset: 0x00016848
		public IEdmExpression FalseExpression
		{
			get
			{
				return this.falseExpression;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00018650 File Offset: 0x00016850
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.If;
			}
		}

		// Token: 0x04000466 RID: 1126
		private readonly IEdmExpression testExpression;

		// Token: 0x04000467 RID: 1127
		private readonly IEdmExpression trueExpression;

		// Token: 0x04000468 RID: 1128
		private readonly IEdmExpression falseExpression;
	}
}
