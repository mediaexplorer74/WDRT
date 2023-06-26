using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000117 RID: 279
	internal struct MaterializerFeed
	{
		// Token: 0x06000934 RID: 2356 RVA: 0x00025863 File Offset: 0x00023A63
		private MaterializerFeed(ODataFeed feed, IEnumerable<ODataEntry> entries)
		{
			this.feed = feed;
			this.entries = entries;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00025873 File Offset: 0x00023A73
		public ODataFeed Feed
		{
			get
			{
				return this.feed;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0002587B File Offset: 0x00023A7B
		public IEnumerable<ODataEntry> Entries
		{
			get
			{
				return this.entries;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00025883 File Offset: 0x00023A83
		public Uri NextPageLink
		{
			get
			{
				return this.feed.NextPageLink;
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00025890 File Offset: 0x00023A90
		public static MaterializerFeed CreateFeed(ODataFeed feed, IEnumerable<ODataEntry> entries)
		{
			if (entries == null)
			{
				entries = Enumerable.Empty<ODataEntry>();
			}
			else
			{
				feed.SetAnnotation<IEnumerable<ODataEntry>>(entries);
			}
			return new MaterializerFeed(feed, entries);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000258AC File Offset: 0x00023AAC
		public static MaterializerFeed GetFeed(ODataFeed feed)
		{
			IEnumerable<ODataEntry> annotation = feed.GetAnnotation<IEnumerable<ODataEntry>>();
			return new MaterializerFeed(feed, annotation);
		}

		// Token: 0x0400055F RID: 1375
		private readonly ODataFeed feed;

		// Token: 0x04000560 RID: 1376
		private readonly IEnumerable<ODataEntry> entries;
	}
}
