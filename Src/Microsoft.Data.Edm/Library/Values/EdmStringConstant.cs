using System;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Library.Values
{
	// Token: 0x020001DC RID: 476
	public class EdmStringConstant : EdmValue, IEdmStringConstantExpression, IEdmExpression, IEdmStringValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000B4F RID: 2895 RVA: 0x00020E89 File Offset: 0x0001F089
		public EdmStringConstant(string value)
			: this(null, value)
		{
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00020E93 File Offset: 0x0001F093
		public EdmStringConstant(IEdmStringTypeReference type, string value)
			: base(type)
		{
			EdmUtil.CheckArgumentNull<string>(value, "value");
			this.value = value;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00020EAF File Offset: 0x0001F0AF
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00020EB7 File Offset: 0x0001F0B7
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.StringConstant;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00020EBB File Offset: 0x0001F0BB
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.String;
			}
		}

		// Token: 0x0400054D RID: 1357
		private readonly string value;
	}
}
