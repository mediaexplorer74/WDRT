using System;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Library.Values
{
	// Token: 0x02000191 RID: 401
	public class EdmBooleanConstant : EdmValue, IEdmBooleanConstantExpression, IEdmExpression, IEdmBooleanValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x060008CC RID: 2252 RVA: 0x000183C0 File Offset: 0x000165C0
		public EdmBooleanConstant(bool value)
			: this(null, value)
		{
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x000183CA File Offset: 0x000165CA
		public EdmBooleanConstant(IEdmPrimitiveTypeReference type, bool value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x000183DA File Offset: 0x000165DA
		public bool Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x000183E2 File Offset: 0x000165E2
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BooleanConstant;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x000183E5 File Offset: 0x000165E5
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Boolean;
			}
		}

		// Token: 0x04000457 RID: 1111
		private readonly bool value;
	}
}
