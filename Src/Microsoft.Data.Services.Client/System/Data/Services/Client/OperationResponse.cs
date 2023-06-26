using System;
using System.Collections.Generic;

namespace System.Data.Services.Client
{
	// Token: 0x02000065 RID: 101
	public abstract class OperationResponse
	{
		// Token: 0x06000364 RID: 868 RVA: 0x0000ED01 File Offset: 0x0000CF01
		internal OperationResponse(HeaderCollection headers)
		{
			this.headers = headers;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000ED10 File Offset: 0x0000CF10
		public IDictionary<string, string> Headers
		{
			get
			{
				return this.headers.UnderlyingDictionary;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000ED1D File Offset: 0x0000CF1D
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0000ED25 File Offset: 0x0000CF25
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
			internal set
			{
				this.statusCode = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000ED2E File Offset: 0x0000CF2E
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000ED36 File Offset: 0x0000CF36
		public Exception Error
		{
			get
			{
				return this.innerException;
			}
			set
			{
				this.innerException = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000ED3F File Offset: 0x0000CF3F
		internal HeaderCollection HeaderCollection
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x04000293 RID: 659
		private readonly HeaderCollection headers;

		// Token: 0x04000294 RID: 660
		private int statusCode;

		// Token: 0x04000295 RID: 661
		private Exception innerException;
	}
}
