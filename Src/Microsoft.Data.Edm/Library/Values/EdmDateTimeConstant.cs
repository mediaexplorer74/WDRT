using System;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Library.Values
{
	// Token: 0x02000192 RID: 402
	public class EdmDateTimeConstant : EdmValue, IEdmDateTimeConstantExpression, IEdmExpression, IEdmDateTimeValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x000183E8 File Offset: 0x000165E8
		public EdmDateTimeConstant(DateTime value)
			: this(null, value)
		{
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x000183F2 File Offset: 0x000165F2
		public EdmDateTimeConstant(IEdmTemporalTypeReference type, DateTime value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00018402 File Offset: 0x00016602
		public DateTime Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0001840A File Offset: 0x0001660A
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateTimeConstant;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0001840D File Offset: 0x0001660D
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.DateTime;
			}
		}

		// Token: 0x04000458 RID: 1112
		private readonly DateTime value;
	}
}
