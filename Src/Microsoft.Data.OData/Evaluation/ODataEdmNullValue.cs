using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x0200017E RID: 382
	internal sealed class ODataEdmNullValue : EdmValue, IEdmNullValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000AC5 RID: 2757 RVA: 0x00024024 File Offset: 0x00022224
		internal ODataEdmNullValue(IEdmTypeReference type)
			: base(type)
		{
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0002402D File Offset: 0x0002222D
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Null;
			}
		}

		// Token: 0x040003FF RID: 1023
		internal static ODataEdmNullValue UntypedInstance = new ODataEdmNullValue(null);
	}
}
