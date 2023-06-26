using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000CB RID: 203
	public sealed class KeyPropertyValue
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00011575 File Offset: 0x0000F775
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x0001157D File Offset: 0x0000F77D
		public IEdmProperty KeyProperty { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00011586 File Offset: 0x0000F786
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0001158E File Offset: 0x0000F78E
		public SingleValueNode KeyValue { get; set; }
	}
}
