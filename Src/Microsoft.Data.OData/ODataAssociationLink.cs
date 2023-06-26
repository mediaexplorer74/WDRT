using System;
using System.Diagnostics;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData
{
	// Token: 0x02000290 RID: 656
	[DebuggerDisplay("{Name}")]
	public sealed class ODataAssociationLink : ODataAnnotatable
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x00051379 File Offset: 0x0004F579
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x00051381 File Offset: 0x0004F581
		public string Name { get; set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0005138A File Offset: 0x0004F58A
		// (set) Token: 0x06001621 RID: 5665 RVA: 0x000513C4 File Offset: 0x0004F5C4
		public Uri Url
		{
			get
			{
				if (this.metadataBuilder != null)
				{
					this.url = this.metadataBuilder.GetAssociationLinkUri(this.Name, this.url, this.hasAssociationLinkUrl);
					this.hasAssociationLinkUrl = true;
				}
				return this.url;
			}
			set
			{
				this.url = value;
				this.hasAssociationLinkUrl = true;
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x000513D4 File Offset: 0x0004F5D4
		internal void SetMetadataBuilder(ODataEntityMetadataBuilder builder)
		{
			this.metadataBuilder = builder;
		}

		// Token: 0x04000869 RID: 2153
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x0400086A RID: 2154
		private Uri url;

		// Token: 0x0400086B RID: 2155
		private bool hasAssociationLinkUrl;
	}
}
