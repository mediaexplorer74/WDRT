using System;

namespace System.Data.Services.Client
{
	// Token: 0x0200005B RID: 91
	public sealed class DataServiceQueryContinuation<T> : DataServiceQueryContinuation
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000E034 File Offset: 0x0000C234
		internal DataServiceQueryContinuation(Uri nextLinkUri, ProjectionPlan plan)
			: base(nextLinkUri, plan)
		{
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000E03E File Offset: 0x0000C23E
		internal override Type ElementType
		{
			get
			{
				return typeof(T);
			}
		}
	}
}
