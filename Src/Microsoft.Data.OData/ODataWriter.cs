using System;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x020001A4 RID: 420
	public abstract class ODataWriter
	{
		// Token: 0x06000CC6 RID: 3270
		public abstract void WriteStart(ODataFeed feed);

		// Token: 0x06000CC7 RID: 3271
		public abstract Task WriteStartAsync(ODataFeed feed);

		// Token: 0x06000CC8 RID: 3272
		public abstract void WriteStart(ODataEntry entry);

		// Token: 0x06000CC9 RID: 3273
		public abstract Task WriteStartAsync(ODataEntry entry);

		// Token: 0x06000CCA RID: 3274
		public abstract void WriteStart(ODataNavigationLink navigationLink);

		// Token: 0x06000CCB RID: 3275
		public abstract Task WriteStartAsync(ODataNavigationLink navigationLink);

		// Token: 0x06000CCC RID: 3276
		public abstract void WriteEnd();

		// Token: 0x06000CCD RID: 3277
		public abstract Task WriteEndAsync();

		// Token: 0x06000CCE RID: 3278
		public abstract void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x06000CCF RID: 3279
		public abstract Task WriteEntityReferenceLinkAsync(ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x06000CD0 RID: 3280
		public abstract void Flush();

		// Token: 0x06000CD1 RID: 3281
		public abstract Task FlushAsync();
	}
}
