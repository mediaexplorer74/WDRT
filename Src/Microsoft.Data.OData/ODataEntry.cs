using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData
{
	// Token: 0x020002B0 RID: 688
	[DebuggerDisplay("Id: {Id} TypeName: {TypeName}")]
	public sealed class ODataEntry : ODataItem
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x00053F69 File Offset: 0x00052169
		// (set) Token: 0x0600173D RID: 5949 RVA: 0x00053F76 File Offset: 0x00052176
		public string ETag
		{
			get
			{
				return this.MetadataBuilder.GetETag();
			}
			set
			{
				this.etag = value;
				this.hasNonComputedETag = true;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x00053F86 File Offset: 0x00052186
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x00053F93 File Offset: 0x00052193
		public string Id
		{
			get
			{
				return this.MetadataBuilder.GetId();
			}
			set
			{
				this.id = value;
				this.hasNonComputedId = true;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x00053FA3 File Offset: 0x000521A3
		// (set) Token: 0x06001741 RID: 5953 RVA: 0x00053FB0 File Offset: 0x000521B0
		public Uri EditLink
		{
			get
			{
				return this.MetadataBuilder.GetEditLink();
			}
			set
			{
				this.editLink = value;
				this.hasNonComputedEditLink = true;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00053FC0 File Offset: 0x000521C0
		// (set) Token: 0x06001743 RID: 5955 RVA: 0x00053FCD File Offset: 0x000521CD
		public Uri ReadLink
		{
			get
			{
				return this.MetadataBuilder.GetReadLink();
			}
			set
			{
				this.readLink = value;
				this.hasNonComputedReadLink = true;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x00053FDD File Offset: 0x000521DD
		// (set) Token: 0x06001745 RID: 5957 RVA: 0x00053FEA File Offset: 0x000521EA
		public ODataStreamReferenceValue MediaResource
		{
			get
			{
				return this.MetadataBuilder.GetMediaResource();
			}
			set
			{
				this.mediaResource = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x00053FF3 File Offset: 0x000521F3
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x00053FFB File Offset: 0x000521FB
		public IEnumerable<ODataAssociationLink> AssociationLinks { get; set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x00054004 File Offset: 0x00052204
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x00054011 File Offset: 0x00052211
		public IEnumerable<ODataAction> Actions
		{
			get
			{
				return this.MetadataBuilder.GetActions();
			}
			set
			{
				this.actions = value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x0005401A File Offset: 0x0005221A
		// (set) Token: 0x0600174B RID: 5963 RVA: 0x00054027 File Offset: 0x00052227
		public IEnumerable<ODataFunction> Functions
		{
			get
			{
				return this.MetadataBuilder.GetFunctions();
			}
			set
			{
				this.functions = value;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x00054030 File Offset: 0x00052230
		// (set) Token: 0x0600174D RID: 5965 RVA: 0x00054043 File Offset: 0x00052243
		public IEnumerable<ODataProperty> Properties
		{
			get
			{
				return this.MetadataBuilder.GetProperties(this.properties);
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x0005404C File Offset: 0x0005224C
		// (set) Token: 0x0600174F RID: 5967 RVA: 0x00054054 File Offset: 0x00052254
		public string TypeName { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x0005405D File Offset: 0x0005225D
		// (set) Token: 0x06001751 RID: 5969 RVA: 0x00054065 File Offset: 0x00052265
		public ICollection<ODataInstanceAnnotation> InstanceAnnotations
		{
			get
			{
				return base.GetInstanceAnnotations();
			}
			set
			{
				base.SetInstanceAnnotations(value);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x0005406E File Offset: 0x0005226E
		// (set) Token: 0x06001753 RID: 5971 RVA: 0x0005408A File Offset: 0x0005228A
		internal ODataEntityMetadataBuilder MetadataBuilder
		{
			get
			{
				if (this.metadataBuilder == null)
				{
					this.metadataBuilder = new NoOpEntityMetadataBuilder(this);
				}
				return this.metadataBuilder;
			}
			set
			{
				this.metadataBuilder = value;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x00054093 File Offset: 0x00052293
		internal string NonComputedId
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0005409B File Offset: 0x0005229B
		internal bool HasNonComputedId
		{
			get
			{
				return this.hasNonComputedId;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x000540A3 File Offset: 0x000522A3
		internal Uri NonComputedEditLink
		{
			get
			{
				return this.editLink;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x000540AB File Offset: 0x000522AB
		internal bool HasNonComputedEditLink
		{
			get
			{
				return this.hasNonComputedEditLink;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x000540B3 File Offset: 0x000522B3
		internal Uri NonComputedReadLink
		{
			get
			{
				return this.readLink;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x000540BB File Offset: 0x000522BB
		internal bool HasNonComputedReadLink
		{
			get
			{
				return this.hasNonComputedReadLink;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x000540C3 File Offset: 0x000522C3
		internal string NonComputedETag
		{
			get
			{
				return this.etag;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x000540CB File Offset: 0x000522CB
		internal bool HasNonComputedETag
		{
			get
			{
				return this.hasNonComputedETag;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x000540D3 File Offset: 0x000522D3
		internal ODataStreamReferenceValue NonComputedMediaResource
		{
			get
			{
				return this.mediaResource;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x000540DB File Offset: 0x000522DB
		internal IEnumerable<ODataProperty> NonComputedProperties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x000540E3 File Offset: 0x000522E3
		internal IEnumerable<ODataAction> NonComputedActions
		{
			get
			{
				return this.actions;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x000540EB File Offset: 0x000522EB
		internal IEnumerable<ODataFunction> NonComputedFunctions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x000540F3 File Offset: 0x000522F3
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x000540FB File Offset: 0x000522FB
		internal ODataFeedAndEntrySerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataFeedAndEntrySerializationInfo.Validate(value);
			}
		}

		// Token: 0x0400099C RID: 2460
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x0400099D RID: 2461
		private string etag;

		// Token: 0x0400099E RID: 2462
		private bool hasNonComputedETag;

		// Token: 0x0400099F RID: 2463
		private string id;

		// Token: 0x040009A0 RID: 2464
		private bool hasNonComputedId;

		// Token: 0x040009A1 RID: 2465
		private Uri editLink;

		// Token: 0x040009A2 RID: 2466
		private bool hasNonComputedEditLink;

		// Token: 0x040009A3 RID: 2467
		private Uri readLink;

		// Token: 0x040009A4 RID: 2468
		private bool hasNonComputedReadLink;

		// Token: 0x040009A5 RID: 2469
		private ODataStreamReferenceValue mediaResource;

		// Token: 0x040009A6 RID: 2470
		private IEnumerable<ODataProperty> properties;

		// Token: 0x040009A7 RID: 2471
		private IEnumerable<ODataAction> actions;

		// Token: 0x040009A8 RID: 2472
		private IEnumerable<ODataFunction> functions;

		// Token: 0x040009A9 RID: 2473
		private ODataFeedAndEntrySerializationInfo serializationInfo;
	}
}
