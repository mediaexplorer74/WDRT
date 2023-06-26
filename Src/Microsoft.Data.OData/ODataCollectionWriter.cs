using System;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000185 RID: 389
	public abstract class ODataCollectionWriter
	{
		// Token: 0x06000AF1 RID: 2801
		public abstract void WriteStart(ODataCollectionStart collectionStart);

		// Token: 0x06000AF2 RID: 2802
		public abstract Task WriteStartAsync(ODataCollectionStart collectionStart);

		// Token: 0x06000AF3 RID: 2803
		public abstract void WriteItem(object item);

		// Token: 0x06000AF4 RID: 2804
		public abstract Task WriteItemAsync(object item);

		// Token: 0x06000AF5 RID: 2805
		public abstract void WriteEnd();

		// Token: 0x06000AF6 RID: 2806
		public abstract Task WriteEndAsync();

		// Token: 0x06000AF7 RID: 2807
		public abstract void Flush();

		// Token: 0x06000AF8 RID: 2808
		public abstract Task FlushAsync();
	}
}
