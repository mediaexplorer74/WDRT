using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000118 RID: 280
	internal class MaterializerNavigationLink
	{
		// Token: 0x0600093A RID: 2362 RVA: 0x000258C7 File Offset: 0x00023AC7
		private MaterializerNavigationLink(ODataNavigationLink link, object materializedFeedOrEntry)
		{
			this.link = link;
			this.feedOrEntry = materializedFeedOrEntry;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x000258DD File Offset: 0x00023ADD
		public ODataNavigationLink Link
		{
			get
			{
				return this.link;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x000258E5 File Offset: 0x00023AE5
		public MaterializerEntry Entry
		{
			get
			{
				return this.feedOrEntry as MaterializerEntry;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x000258F2 File Offset: 0x00023AF2
		public ODataFeed Feed
		{
			get
			{
				return this.feedOrEntry as ODataFeed;
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00025900 File Offset: 0x00023B00
		public static MaterializerNavigationLink CreateLink(ODataNavigationLink link, MaterializerEntry entry)
		{
			MaterializerNavigationLink materializerNavigationLink = new MaterializerNavigationLink(link, entry);
			link.SetAnnotation<MaterializerNavigationLink>(materializerNavigationLink);
			return materializerNavigationLink;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00025920 File Offset: 0x00023B20
		public static MaterializerNavigationLink CreateLink(ODataNavigationLink link, ODataFeed feed)
		{
			MaterializerNavigationLink materializerNavigationLink = new MaterializerNavigationLink(link, feed);
			link.SetAnnotation<MaterializerNavigationLink>(materializerNavigationLink);
			return materializerNavigationLink;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0002593D File Offset: 0x00023B3D
		public static MaterializerNavigationLink GetLink(ODataNavigationLink link)
		{
			return link.GetAnnotation<MaterializerNavigationLink>();
		}

		// Token: 0x04000561 RID: 1377
		private readonly ODataNavigationLink link;

		// Token: 0x04000562 RID: 1378
		private readonly object feedOrEntry;
	}
}
