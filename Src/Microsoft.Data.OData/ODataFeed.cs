using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x020002AC RID: 684
	public sealed class ODataFeed : ODataItem
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x00053C5C File Offset: 0x00051E5C
		// (set) Token: 0x06001713 RID: 5907 RVA: 0x00053C64 File Offset: 0x00051E64
		public long? Count { get; set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x00053C6D File Offset: 0x00051E6D
		// (set) Token: 0x06001715 RID: 5909 RVA: 0x00053C75 File Offset: 0x00051E75
		public string Id { get; set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x00053C7E File Offset: 0x00051E7E
		// (set) Token: 0x06001717 RID: 5911 RVA: 0x00053C86 File Offset: 0x00051E86
		public Uri NextPageLink
		{
			get
			{
				return this.nextPageLink;
			}
			set
			{
				if (this.DeltaLink != null)
				{
					throw new ODataException(Strings.ODataFeed_MustNotContainBothNextPageLinkAndDeltaLink);
				}
				this.nextPageLink = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x00053CA8 File Offset: 0x00051EA8
		// (set) Token: 0x06001719 RID: 5913 RVA: 0x00053CB0 File Offset: 0x00051EB0
		public Uri DeltaLink
		{
			get
			{
				return this.deltaLink;
			}
			set
			{
				if (this.NextPageLink != null)
				{
					throw new ODataException(Strings.ODataFeed_MustNotContainBothNextPageLinkAndDeltaLink);
				}
				this.deltaLink = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x00053CD2 File Offset: 0x00051ED2
		// (set) Token: 0x0600171B RID: 5915 RVA: 0x00053CDA File Offset: 0x00051EDA
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

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x00053CE3 File Offset: 0x00051EE3
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x00053CEB File Offset: 0x00051EEB
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

		// Token: 0x04000983 RID: 2435
		private Uri nextPageLink;

		// Token: 0x04000984 RID: 2436
		private Uri deltaLink;

		// Token: 0x04000985 RID: 2437
		private ODataFeedAndEntrySerializationInfo serializationInfo;
	}
}
