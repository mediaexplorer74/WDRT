using System;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x0200024C RID: 588
	internal interface IODataBatchOperationListener
	{
		// Token: 0x060012DE RID: 4830
		void BatchOperationContentStreamRequested();

		// Token: 0x060012DF RID: 4831
		Task BatchOperationContentStreamRequestedAsync();

		// Token: 0x060012E0 RID: 4832
		void BatchOperationContentStreamDisposed();
	}
}
