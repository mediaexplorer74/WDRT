using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200004B RID: 75
	public abstract class DataServiceClientRequestMessage : IODataRequestMessage
	{
		// Token: 0x06000264 RID: 612 RVA: 0x0000C95B File Offset: 0x0000AB5B
		public DataServiceClientRequestMessage(string actualMethod)
		{
			this.actualHttpMethod = actualMethod;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000265 RID: 613
		public abstract IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000266 RID: 614
		// (set) Token: 0x06000267 RID: 615
		public abstract Uri Url { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000268 RID: 616
		// (set) Token: 0x06000269 RID: 617
		public abstract string Method { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600026A RID: 618
		// (set) Token: 0x0600026B RID: 619
		public abstract ICredentials Credentials { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600026C RID: 620
		// (set) Token: 0x0600026D RID: 621
		public abstract int Timeout { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600026E RID: 622
		// (set) Token: 0x0600026F RID: 623
		public abstract bool SendChunked { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000C96A File Offset: 0x0000AB6A
		protected virtual string ActualMethod
		{
			get
			{
				return this.actualHttpMethod;
			}
		}

		// Token: 0x06000271 RID: 625
		public abstract string GetHeader(string headerName);

		// Token: 0x06000272 RID: 626
		public abstract void SetHeader(string headerName, string headerValue);

		// Token: 0x06000273 RID: 627
		public abstract Stream GetStream();

		// Token: 0x06000274 RID: 628
		public abstract void Abort();

		// Token: 0x06000275 RID: 629
		public abstract IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state);

		// Token: 0x06000276 RID: 630
		public abstract Stream EndGetRequestStream(IAsyncResult asyncResult);

		// Token: 0x06000277 RID: 631
		public abstract IAsyncResult BeginGetResponse(AsyncCallback callback, object state);

		// Token: 0x06000278 RID: 632
		public abstract IODataResponseMessage EndGetResponse(IAsyncResult asyncResult);

		// Token: 0x06000279 RID: 633
		public abstract IODataResponseMessage GetResponse();

		// Token: 0x04000241 RID: 577
		private readonly string actualHttpMethod;
	}
}
