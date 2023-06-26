using System;
using System.ComponentModel;

namespace System.Data.Services.Client
{
	// Token: 0x020000F6 RID: 246
	public sealed class LoadCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x000232C4 File Offset: 0x000214C4
		internal LoadCompletedEventArgs(QueryOperationResponse queryOperationResponse, Exception error)
			: this(queryOperationResponse, error, false)
		{
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000232CF File Offset: 0x000214CF
		internal LoadCompletedEventArgs(QueryOperationResponse queryOperationResponse, Exception error, bool cancelled)
			: base(error, cancelled, null)
		{
			this.queryOperationResponse = queryOperationResponse;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x000232E1 File Offset: 0x000214E1
		public QueryOperationResponse QueryOperationResponse
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.queryOperationResponse;
			}
		}

		// Token: 0x040004E3 RID: 1251
		private QueryOperationResponse queryOperationResponse;
	}
}
