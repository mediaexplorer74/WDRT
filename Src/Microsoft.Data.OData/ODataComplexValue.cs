using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x020002AB RID: 683
	public sealed class ODataComplexValue : ODataValue
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x00053C32 File Offset: 0x00051E32
		// (set) Token: 0x0600170E RID: 5902 RVA: 0x00053C3A File Offset: 0x00051E3A
		public IEnumerable<ODataProperty> Properties { get; set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00053C43 File Offset: 0x00051E43
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x00053C4B File Offset: 0x00051E4B
		public string TypeName { get; set; }
	}
}
