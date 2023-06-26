using System;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000159 RID: 345
	public abstract class ODataReader
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600094D RID: 2381
		public abstract ODataReaderState State { get; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600094E RID: 2382
		public abstract ODataItem Item { get; }

		// Token: 0x0600094F RID: 2383
		public abstract bool Read();

		// Token: 0x06000950 RID: 2384
		public abstract Task<bool> ReadAsync();
	}
}
