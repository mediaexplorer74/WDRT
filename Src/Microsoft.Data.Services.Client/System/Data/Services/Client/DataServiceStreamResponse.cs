using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000100 RID: 256
	public sealed class DataServiceStreamResponse : IDisposable
	{
		// Token: 0x06000868 RID: 2152 RVA: 0x00023532 File Offset: 0x00021732
		internal DataServiceStreamResponse(IODataResponseMessage response)
		{
			this.responseMessage = response;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00023541 File Offset: 0x00021741
		public string ContentType
		{
			get
			{
				this.CheckDisposed();
				return this.responseMessage.GetHeader("Content-Type");
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00023559 File Offset: 0x00021759
		public string ContentDisposition
		{
			get
			{
				this.CheckDisposed();
				return this.responseMessage.GetHeader("Content-Disposition");
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00023571 File Offset: 0x00021771
		public Dictionary<string, string> Headers
		{
			get
			{
				this.CheckDisposed();
				if (this.headers == null)
				{
					this.headers = (Dictionary<string, string>)new HeaderCollection(this.responseMessage).UnderlyingDictionary;
				}
				return this.headers;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x000235A2 File Offset: 0x000217A2
		public Stream Stream
		{
			get
			{
				this.CheckDisposed();
				if (this.responseStream == null)
				{
					this.responseStream = this.responseMessage.GetStream();
				}
				return this.responseStream;
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000235C9 File Offset: 0x000217C9
		public void Dispose()
		{
			WebUtil.DisposeMessage(this.responseMessage);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000235D6 File Offset: 0x000217D6
		private void CheckDisposed()
		{
			if (this.responseMessage == null)
			{
				Error.ThrowObjectDisposed(base.GetType());
			}
		}

		// Token: 0x040004EF RID: 1263
		private IODataResponseMessage responseMessage;

		// Token: 0x040004F0 RID: 1264
		private Dictionary<string, string> headers;

		// Token: 0x040004F1 RID: 1265
		private Stream responseStream;
	}
}
