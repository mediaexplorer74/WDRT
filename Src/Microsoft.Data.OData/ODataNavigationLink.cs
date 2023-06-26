using System;
using System.Diagnostics;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData
{
	// Token: 0x020002AD RID: 685
	[DebuggerDisplay("{Name}")]
	public sealed class ODataNavigationLink : ODataItem
	{
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x00053D01 File Offset: 0x00051F01
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x00053D09 File Offset: 0x00051F09
		public bool? IsCollection { get; set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x00053D12 File Offset: 0x00051F12
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x00053D1A File Offset: 0x00051F1A
		public string Name { get; set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x00053D23 File Offset: 0x00051F23
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x00053D5D File Offset: 0x00051F5D
		public Uri Url
		{
			get
			{
				if (this.metadataBuilder != null)
				{
					this.url = this.metadataBuilder.GetNavigationLinkUri(this.Name, this.url, this.hasNavigationLink);
					this.hasNavigationLink = true;
				}
				return this.url;
			}
			set
			{
				this.url = value;
				this.hasNavigationLink = true;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00053D6D File Offset: 0x00051F6D
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x00053DA7 File Offset: 0x00051FA7
		public Uri AssociationLinkUrl
		{
			get
			{
				if (this.metadataBuilder != null)
				{
					this.associationLinkUrl = this.metadataBuilder.GetAssociationLinkUri(this.Name, this.associationLinkUrl, this.hasAssociationUrl);
					this.hasAssociationUrl = true;
				}
				return this.associationLinkUrl;
			}
			set
			{
				this.associationLinkUrl = value;
				this.hasAssociationUrl = true;
			}
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00053DB7 File Offset: 0x00051FB7
		internal void SetMetadataBuilder(ODataEntityMetadataBuilder builder)
		{
			this.metadataBuilder = builder;
		}

		// Token: 0x04000988 RID: 2440
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x04000989 RID: 2441
		private Uri url;

		// Token: 0x0400098A RID: 2442
		private bool hasNavigationLink;

		// Token: 0x0400098B RID: 2443
		private Uri associationLinkUrl;

		// Token: 0x0400098C RID: 2444
		private bool hasAssociationUrl;
	}
}
