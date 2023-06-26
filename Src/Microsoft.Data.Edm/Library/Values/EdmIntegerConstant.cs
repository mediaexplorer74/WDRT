using System;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Library.Values
{
	// Token: 0x020001D8 RID: 472
	public class EdmIntegerConstant : EdmValue, IEdmIntegerConstantExpression, IEdmExpression, IEdmIntegerValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000B3E RID: 2878 RVA: 0x00020D8F File Offset: 0x0001EF8F
		public EdmIntegerConstant(long value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00020DA0 File Offset: 0x0001EFA0
		public EdmIntegerConstant(IEdmPrimitiveTypeReference type, long value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00020DB0 File Offset: 0x0001EFB0
		public long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00020DB8 File Offset: 0x0001EFB8
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IntegerConstant;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00020DBB File Offset: 0x0001EFBB
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Integer;
			}
		}

		// Token: 0x04000548 RID: 1352
		private readonly long value;
	}
}
