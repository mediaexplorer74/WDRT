using System;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000152 RID: 338
	public abstract class ODataParameterReader
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600091C RID: 2332
		public abstract ODataParameterReaderState State { get; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600091D RID: 2333
		public abstract string Name { get; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600091E RID: 2334
		public abstract object Value { get; }

		// Token: 0x0600091F RID: 2335
		public abstract ODataCollectionReader CreateCollectionReader();

		// Token: 0x06000920 RID: 2336
		public abstract bool Read();

		// Token: 0x06000921 RID: 2337
		public abstract Task<bool> ReadAsync();
	}
}
