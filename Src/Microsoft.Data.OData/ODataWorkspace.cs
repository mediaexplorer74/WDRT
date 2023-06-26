using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x02000279 RID: 633
	public sealed class ODataWorkspace : ODataAnnotatable
	{
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x0004E2AF File Offset: 0x0004C4AF
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x0004E2B7 File Offset: 0x0004C4B7
		public IEnumerable<ODataResourceCollectionInfo> Collections { get; set; }
	}
}
