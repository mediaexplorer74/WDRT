using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200004E RID: 78
	public sealed class WritingNavigationLinkArgs
	{
		// Token: 0x06000286 RID: 646 RVA: 0x0000CA44 File Offset: 0x0000AC44
		public WritingNavigationLinkArgs(ODataNavigationLink link, object source, object target)
		{
			Util.CheckArgumentNull<ODataNavigationLink>(link, "link");
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNull<object>(target, "target");
			this.Link = link;
			this.Source = source;
			this.Target = target;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000CA90 File Offset: 0x0000AC90
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000CA98 File Offset: 0x0000AC98
		public ODataNavigationLink Link { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000CAA1 File Offset: 0x0000ACA1
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000CAA9 File Offset: 0x0000ACA9
		public object Source { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000CAB2 File Offset: 0x0000ACB2
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000CABA File Offset: 0x0000ACBA
		public object Target { get; private set; }
	}
}
