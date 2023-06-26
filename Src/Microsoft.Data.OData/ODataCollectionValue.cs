using System;
using System.Collections;

namespace Microsoft.Data.OData
{
	// Token: 0x02000297 RID: 663
	public sealed class ODataCollectionValue : ODataValue
	{
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x00051BAD File Offset: 0x0004FDAD
		// (set) Token: 0x0600165A RID: 5722 RVA: 0x00051BB5 File Offset: 0x0004FDB5
		public string TypeName { get; set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x00051BBE File Offset: 0x0004FDBE
		// (set) Token: 0x0600165C RID: 5724 RVA: 0x00051BC6 File Offset: 0x0004FDC6
		public IEnumerable Items { get; set; }
	}
}
