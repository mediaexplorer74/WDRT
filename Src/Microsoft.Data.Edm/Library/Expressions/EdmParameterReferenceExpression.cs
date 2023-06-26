using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Library.Expressions
{
	// Token: 0x02000187 RID: 391
	public class EdmParameterReferenceExpression : EdmElement, IEdmParameterReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x000180D5 File Offset: 0x000162D5
		public EdmParameterReferenceExpression(IEdmFunctionParameter referencedParameter)
		{
			EdmUtil.CheckArgumentNull<IEdmFunctionParameter>(referencedParameter, "referencedParameter");
			this.referencedParameter = referencedParameter;
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x000180F0 File Offset: 0x000162F0
		public IEdmFunctionParameter ReferencedParameter
		{
			get
			{
				return this.referencedParameter;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x000180F8 File Offset: 0x000162F8
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.ParameterReference;
			}
		}

		// Token: 0x04000445 RID: 1093
		private readonly IEdmFunctionParameter referencedParameter;
	}
}
