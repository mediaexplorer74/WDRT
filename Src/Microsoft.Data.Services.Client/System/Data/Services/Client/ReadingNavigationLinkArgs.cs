using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000050 RID: 80
	public sealed class ReadingNavigationLinkArgs
	{
		// Token: 0x0600029F RID: 671 RVA: 0x0000CE8C File Offset: 0x0000B08C
		public ReadingNavigationLinkArgs(ODataNavigationLink link)
		{
			Util.CheckArgumentNull<ODataNavigationLink>(link, "link");
			this.Link = link;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000CEA7 File Offset: 0x0000B0A7
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000CEAF File Offset: 0x0000B0AF
		public ODataNavigationLink Link { get; private set; }
	}
}
