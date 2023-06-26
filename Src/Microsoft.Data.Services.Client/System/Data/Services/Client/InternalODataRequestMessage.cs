using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000064 RID: 100
	internal class InternalODataRequestMessage : DataServiceClientRequestMessage
	{
		// Token: 0x0600034E RID: 846 RVA: 0x0000EBEA File Offset: 0x0000CDEA
		internal InternalODataRequestMessage(IODataRequestMessage requestMessage, bool allowGetStream)
			: base(requestMessage.Method)
		{
			this.requestMessage = requestMessage;
			this.allowGetStream = allowGetStream;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000EC06 File Offset: 0x0000CE06
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.HeaderCollection.AsEnumerable();
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000EC13 File Offset: 0x0000CE13
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000EC20 File Offset: 0x0000CE20
		public override Uri Url
		{
			get
			{
				return this.requestMessage.Url;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000EC27 File Offset: 0x0000CE27
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public override string Method
		{
			get
			{
				return this.requestMessage.Method;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000EC3B File Offset: 0x0000CE3B
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000EC42 File Offset: 0x0000CE42
		public override ICredentials Credentials
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000EC49 File Offset: 0x0000CE49
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000EC50 File Offset: 0x0000CE50
		public override int Timeout
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000EC57 File Offset: 0x0000CE57
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000EC5E File Offset: 0x0000CE5E
		public override bool SendChunked
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000EC65 File Offset: 0x0000CE65
		private HeaderCollection HeaderCollection
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = new HeaderCollection(this.requestMessage.Headers);
				}
				return this.headers;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000EC8B File Offset: 0x0000CE8B
		public override string GetHeader(string headerName)
		{
			return this.HeaderCollection.GetHeader(headerName);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000EC99 File Offset: 0x0000CE99
		public override void SetHeader(string headerName, string headerValue)
		{
			this.requestMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
		public override Stream GetStream()
		{
			if (!this.allowGetStream)
			{
				throw new NotImplementedException();
			}
			if (this.cachedRequestStream == null)
			{
				this.cachedRequestStream = this.requestMessage.GetStream();
			}
			return this.cachedRequestStream;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000ECD7 File Offset: 0x0000CED7
		public override void Abort()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000ECDE File Offset: 0x0000CEDE
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000ECE5 File Offset: 0x0000CEE5
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000ECF3 File Offset: 0x0000CEF3
		public override IODataResponseMessage EndGetResponse(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000ECFA File Offset: 0x0000CEFA
		public override IODataResponseMessage GetResponse()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400028F RID: 655
		private readonly IODataRequestMessage requestMessage;

		// Token: 0x04000290 RID: 656
		private readonly bool allowGetStream;

		// Token: 0x04000291 RID: 657
		private Stream cachedRequestStream;

		// Token: 0x04000292 RID: 658
		private HeaderCollection headers;
	}
}
