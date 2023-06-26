using System;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x0200014B RID: 331
	public abstract class ODataCollectionReader
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060008E7 RID: 2279
		public abstract ODataCollectionReaderState State { get; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060008E8 RID: 2280
		public abstract object Item { get; }

		// Token: 0x060008E9 RID: 2281
		public abstract bool Read();

		// Token: 0x060008EA RID: 2282
		public abstract Task<bool> ReadAsync();
	}
}
