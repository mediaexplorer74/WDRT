using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Services.Client
{
	// Token: 0x0200010D RID: 269
	public sealed class DataServiceResponse : IEnumerable<OperationResponse>, IEnumerable
	{
		// Token: 0x060008D2 RID: 2258 RVA: 0x00024986 File Offset: 0x00022B86
		internal DataServiceResponse(HeaderCollection headers, int statusCode, IEnumerable<OperationResponse> response, bool batchResponse)
		{
			this.headers = headers ?? new HeaderCollection();
			this.statusCode = statusCode;
			this.batchResponse = batchResponse;
			this.response = response;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x000249B4 File Offset: 0x00022BB4
		public IDictionary<string, string> BatchHeaders
		{
			get
			{
				return this.headers.UnderlyingDictionary;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x000249C1 File Offset: 0x00022BC1
		public int BatchStatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x000249C9 File Offset: 0x00022BC9
		public bool IsBatchResponse
		{
			get
			{
				return this.batchResponse;
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x000249D1 File Offset: 0x00022BD1
		public IEnumerator<OperationResponse> GetEnumerator()
		{
			return this.response.GetEnumerator();
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000249DE File Offset: 0x00022BDE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000513 RID: 1299
		private readonly HeaderCollection headers;

		// Token: 0x04000514 RID: 1300
		private readonly int statusCode;

		// Token: 0x04000515 RID: 1301
		private readonly IEnumerable<OperationResponse> response;

		// Token: 0x04000516 RID: 1302
		private readonly bool batchResponse;
	}
}
