using System;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Library.Values
{
	// Token: 0x02000190 RID: 400
	public class EdmBinaryConstant : EdmValue, IEdmBinaryConstantExpression, IEdmExpression, IEdmBinaryValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x0001838C File Offset: 0x0001658C
		public EdmBinaryConstant(byte[] value)
			: this(null, value)
		{
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00018396 File Offset: 0x00016596
		public EdmBinaryConstant(IEdmBinaryTypeReference type, byte[] value)
			: base(type)
		{
			EdmUtil.CheckArgumentNull<byte[]>(value, "value");
			this.value = value;
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x000183B2 File Offset: 0x000165B2
		public byte[] Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x000183BA File Offset: 0x000165BA
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BinaryConstant;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x000183BD File Offset: 0x000165BD
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Binary;
			}
		}

		// Token: 0x04000456 RID: 1110
		private readonly byte[] value;
	}
}
