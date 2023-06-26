using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006AA RID: 1706
	internal struct StoreTransactionOperation
	{
		// Token: 0x0400226C RID: 8812
		[MarshalAs(UnmanagedType.U4)]
		public StoreTransactionOperationType Operation;

		// Token: 0x0400226D RID: 8813
		public StoreTransactionData Data;
	}
}
