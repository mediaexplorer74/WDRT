using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000153 RID: 339
	internal interface IODataReaderWriterListener
	{
		// Token: 0x06000923 RID: 2339
		void OnException();

		// Token: 0x06000924 RID: 2340
		void OnCompleted();
	}
}
