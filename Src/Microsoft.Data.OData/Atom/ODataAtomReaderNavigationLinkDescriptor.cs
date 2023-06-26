using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000FC RID: 252
	internal sealed class ODataAtomReaderNavigationLinkDescriptor
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x00017E5F File Offset: 0x0001605F
		internal ODataAtomReaderNavigationLinkDescriptor(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty)
		{
			this.navigationLink = navigationLink;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00017E75 File Offset: 0x00016075
		internal ODataNavigationLink NavigationLink
		{
			get
			{
				return this.navigationLink;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00017E7D File Offset: 0x0001607D
		internal IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x04000298 RID: 664
		private ODataNavigationLink navigationLink;

		// Token: 0x04000299 RID: 665
		private IEdmNavigationProperty navigationProperty;
	}
}
