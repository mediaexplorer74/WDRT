using System;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData
{
	// Token: 0x020002AE RID: 686
	public sealed class ODataStreamReferenceValue : ODataValue
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x00053DC8 File Offset: 0x00051FC8
		// (set) Token: 0x0600172A RID: 5930 RVA: 0x00053E12 File Offset: 0x00052012
		public Uri EditLink
		{
			get
			{
				Uri uri;
				if (!this.hasNonComputedEditLink)
				{
					if ((uri = this.computedEditLink) == null)
					{
						if (this.metadataBuilder != null)
						{
							return this.computedEditLink = this.metadataBuilder.GetStreamEditLink(this.edmPropertyName);
						}
						return null;
					}
				}
				else
				{
					uri = this.editLink;
				}
				return uri;
			}
			set
			{
				this.editLink = value;
				this.hasNonComputedEditLink = true;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x00053E24 File Offset: 0x00052024
		// (set) Token: 0x0600172C RID: 5932 RVA: 0x00053E6E File Offset: 0x0005206E
		public Uri ReadLink
		{
			get
			{
				Uri uri;
				if (!this.hasNonComputedReadLink)
				{
					if ((uri = this.computedReadLink) == null)
					{
						if (this.metadataBuilder != null)
						{
							return this.computedReadLink = this.metadataBuilder.GetStreamReadLink(this.edmPropertyName);
						}
						return null;
					}
				}
				else
				{
					uri = this.readLink;
				}
				return uri;
			}
			set
			{
				this.readLink = value;
				this.hasNonComputedReadLink = true;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x00053E7E File Offset: 0x0005207E
		// (set) Token: 0x0600172E RID: 5934 RVA: 0x00053E86 File Offset: 0x00052086
		public string ContentType { get; set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x00053E8F File Offset: 0x0005208F
		// (set) Token: 0x06001730 RID: 5936 RVA: 0x00053E97 File Offset: 0x00052097
		public string ETag { get; set; }

		// Token: 0x06001731 RID: 5937 RVA: 0x00053EA0 File Offset: 0x000520A0
		internal void SetMetadataBuilder(ODataEntityMetadataBuilder builder, string propertyName)
		{
			this.metadataBuilder = builder;
			this.edmPropertyName = propertyName;
			this.computedEditLink = null;
			this.computedReadLink = null;
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00053EBE File Offset: 0x000520BE
		internal ODataEntityMetadataBuilder GetMetadataBuilder()
		{
			return this.metadataBuilder;
		}

		// Token: 0x0400098F RID: 2447
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x04000990 RID: 2448
		private string edmPropertyName;

		// Token: 0x04000991 RID: 2449
		private Uri editLink;

		// Token: 0x04000992 RID: 2450
		private Uri computedEditLink;

		// Token: 0x04000993 RID: 2451
		private bool hasNonComputedEditLink;

		// Token: 0x04000994 RID: 2452
		private Uri readLink;

		// Token: 0x04000995 RID: 2453
		private Uri computedReadLink;

		// Token: 0x04000996 RID: 2454
		private bool hasNonComputedReadLink;
	}
}
