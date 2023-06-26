using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000164 RID: 356
	internal sealed class ODataJsonLightReaderNavigationLinkInfo
	{
		// Token: 0x060009C2 RID: 2498 RVA: 0x0001F016 File Offset: 0x0001D216
		private ODataJsonLightReaderNavigationLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty, bool isExpanded)
		{
			this.navigationLink = navigationLink;
			this.navigationProperty = navigationProperty;
			this.isExpanded = isExpanded;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0001F033 File Offset: 0x0001D233
		internal ODataNavigationLink NavigationLink
		{
			get
			{
				return this.navigationLink;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0001F03B File Offset: 0x0001D23B
		internal IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0001F043 File Offset: 0x0001D243
		internal bool IsExpanded
		{
			get
			{
				return this.isExpanded;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0001F04B File Offset: 0x0001D24B
		internal ODataFeed ExpandedFeed
		{
			get
			{
				return this.expandedFeed;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0001F053 File Offset: 0x0001D253
		internal bool HasEntityReferenceLink
		{
			get
			{
				return this.entityReferenceLinks != null && this.entityReferenceLinks.First != null;
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001F070 File Offset: 0x0001D270
		internal static ODataJsonLightReaderNavigationLinkInfo CreateDeferredLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty)
		{
			return new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, false);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0001F07C File Offset: 0x0001D27C
		internal static ODataJsonLightReaderNavigationLinkInfo CreateExpandedEntryLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty)
		{
			return new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, true);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0001F094 File Offset: 0x0001D294
		internal static ODataJsonLightReaderNavigationLinkInfo CreateExpandedFeedLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty, ODataFeed expandedFeed)
		{
			return new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, true)
			{
				expandedFeed = expandedFeed
			};
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0001F0B4 File Offset: 0x0001D2B4
		internal static ODataJsonLightReaderNavigationLinkInfo CreateSingletonEntityReferenceLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty, ODataEntityReferenceLink entityReferenceLink, bool isExpanded)
		{
			ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, isExpanded);
			if (entityReferenceLink != null)
			{
				odataJsonLightReaderNavigationLinkInfo.entityReferenceLinks = new LinkedList<ODataEntityReferenceLink>();
				odataJsonLightReaderNavigationLinkInfo.entityReferenceLinks.AddFirst(entityReferenceLink);
			}
			return odataJsonLightReaderNavigationLinkInfo;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0001F0E8 File Offset: 0x0001D2E8
		internal static ODataJsonLightReaderNavigationLinkInfo CreateCollectionEntityReferenceLinksInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty, LinkedList<ODataEntityReferenceLink> entityReferenceLinks, bool isExpanded)
		{
			return new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, isExpanded)
			{
				entityReferenceLinks = entityReferenceLinks
			};
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001F108 File Offset: 0x0001D308
		internal static ODataJsonLightReaderNavigationLinkInfo CreateProjectedNavigationLinkInfo(IEdmNavigationProperty navigationProperty)
		{
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = navigationProperty.Name,
				IsCollection = new bool?(navigationProperty.Type.IsCollection())
			};
			return new ODataJsonLightReaderNavigationLinkInfo(odataNavigationLink, navigationProperty, false);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0001F14C File Offset: 0x0001D34C
		internal ODataEntityReferenceLink ReportEntityReferenceLink()
		{
			if (this.entityReferenceLinks != null && this.entityReferenceLinks.First != null)
			{
				ODataEntityReferenceLink value = this.entityReferenceLinks.First.Value;
				this.entityReferenceLinks.RemoveFirst();
				return value;
			}
			return null;
		}

		// Token: 0x0400039A RID: 922
		private readonly ODataNavigationLink navigationLink;

		// Token: 0x0400039B RID: 923
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x0400039C RID: 924
		private readonly bool isExpanded;

		// Token: 0x0400039D RID: 925
		private ODataFeed expandedFeed;

		// Token: 0x0400039E RID: 926
		private LinkedList<ODataEntityReferenceLink> entityReferenceLinks;
	}
}
