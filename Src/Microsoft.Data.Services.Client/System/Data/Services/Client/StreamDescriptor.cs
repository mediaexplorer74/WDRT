using System;

namespace System.Data.Services.Client
{
	// Token: 0x02000124 RID: 292
	public sealed class StreamDescriptor : Descriptor
	{
		// Token: 0x060009B6 RID: 2486 RVA: 0x00027C41 File Offset: 0x00025E41
		internal StreamDescriptor(string name, EntityDescriptor entityDescriptor)
			: base(EntityStates.Unchanged)
		{
			this.streamLink = new DataServiceStreamLink(name);
			this.entityDescriptor = entityDescriptor;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00027C5D File Offset: 0x00025E5D
		internal StreamDescriptor(EntityDescriptor entityDescriptor)
			: base(EntityStates.Unchanged)
		{
			this.streamLink = new DataServiceStreamLink(null);
			this.entityDescriptor = entityDescriptor;
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00027C79 File Offset: 0x00025E79
		public DataServiceStreamLink StreamLink
		{
			get
			{
				return this.streamLink;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00027C81 File Offset: 0x00025E81
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x00027C89 File Offset: 0x00025E89
		public EntityDescriptor EntityDescriptor
		{
			get
			{
				return this.entityDescriptor;
			}
			set
			{
				this.entityDescriptor = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00027C92 File Offset: 0x00025E92
		internal string Name
		{
			get
			{
				return this.streamLink.Name;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00027C9F File Offset: 0x00025E9F
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x00027CAC File Offset: 0x00025EAC
		internal Uri SelfLink
		{
			get
			{
				return this.streamLink.SelfLink;
			}
			set
			{
				this.streamLink.SelfLink = value;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x00027CBA File Offset: 0x00025EBA
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x00027CC7 File Offset: 0x00025EC7
		internal Uri EditLink
		{
			get
			{
				return this.streamLink.EditLink;
			}
			set
			{
				this.streamLink.EditLink = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00027CD5 File Offset: 0x00025ED5
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x00027CE2 File Offset: 0x00025EE2
		internal string ContentType
		{
			get
			{
				return this.streamLink.ContentType;
			}
			set
			{
				this.streamLink.ContentType = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00027CF0 File Offset: 0x00025EF0
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x00027CFD File Offset: 0x00025EFD
		internal string ETag
		{
			get
			{
				return this.streamLink.ETag;
			}
			set
			{
				this.streamLink.ETag = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00027D0B File Offset: 0x00025F0B
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00027D13 File Offset: 0x00025F13
		internal DataServiceSaveStream SaveStream { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00027D1C File Offset: 0x00025F1C
		internal override DescriptorKind DescriptorKind
		{
			get
			{
				return DescriptorKind.NamedStream;
			}
		}

		// Token: 0x17000257 RID: 599
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x00027D1F File Offset: 0x00025F1F
		internal StreamDescriptor TransientNamedStreamInfo
		{
			set
			{
				if (this.transientNamedStreamInfo == null)
				{
					this.transientNamedStreamInfo = value;
					return;
				}
				StreamDescriptor.MergeStreamDescriptor(this.transientNamedStreamInfo, value);
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00027D40 File Offset: 0x00025F40
		internal static void MergeStreamDescriptor(StreamDescriptor existingStreamDescriptor, StreamDescriptor newStreamDescriptor)
		{
			if (newStreamDescriptor.SelfLink != null)
			{
				existingStreamDescriptor.SelfLink = newStreamDescriptor.SelfLink;
			}
			if (newStreamDescriptor.EditLink != null)
			{
				existingStreamDescriptor.EditLink = newStreamDescriptor.EditLink;
			}
			if (newStreamDescriptor.ContentType != null)
			{
				existingStreamDescriptor.ContentType = newStreamDescriptor.ContentType;
			}
			if (newStreamDescriptor.ETag != null)
			{
				existingStreamDescriptor.ETag = newStreamDescriptor.ETag;
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00027DA9 File Offset: 0x00025FA9
		internal override void ClearChanges()
		{
			this.transientNamedStreamInfo = null;
			this.CloseSaveStream();
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00027DB8 File Offset: 0x00025FB8
		internal Uri GetLatestEditLink()
		{
			if (this.transientNamedStreamInfo != null && this.transientNamedStreamInfo.EditLink != null)
			{
				return this.transientNamedStreamInfo.EditLink;
			}
			return this.EditLink;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00027DE7 File Offset: 0x00025FE7
		internal string GetLatestETag()
		{
			if (this.transientNamedStreamInfo != null && this.transientNamedStreamInfo.ETag != null)
			{
				return this.transientNamedStreamInfo.ETag;
			}
			return this.ETag;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00027E10 File Offset: 0x00026010
		internal void CloseSaveStream()
		{
			if (this.SaveStream != null)
			{
				DataServiceSaveStream saveStream = this.SaveStream;
				this.SaveStream = null;
				saveStream.Close();
			}
		}

		// Token: 0x0400059C RID: 1436
		private DataServiceStreamLink streamLink;

		// Token: 0x0400059D RID: 1437
		private EntityDescriptor entityDescriptor;

		// Token: 0x0400059E RID: 1438
		private StreamDescriptor transientNamedStreamInfo;
	}
}
