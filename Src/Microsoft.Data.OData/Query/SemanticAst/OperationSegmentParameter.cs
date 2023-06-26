using System;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000073 RID: 115
	public sealed class OperationSegmentParameter : ODataAnnotatable
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000A7D9 File Offset: 0x000089D9
		public OperationSegmentParameter(string name, object value)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000A7FA File Offset: 0x000089FA
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000A802 File Offset: 0x00008A02
		public string Name { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000A80B File Offset: 0x00008A0B
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000A813 File Offset: 0x00008A13
		public object Value { get; private set; }
	}
}
