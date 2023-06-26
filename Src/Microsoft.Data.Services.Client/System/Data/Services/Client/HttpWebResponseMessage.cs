using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200008C RID: 140
	public class HttpWebResponseMessage : IODataResponseMessage, IDisposable
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x00014748 File Offset: 0x00012948
		public HttpWebResponseMessage(IDictionary<string, string> headers, int statusCode, Func<Stream> getResponseStream)
		{
			this.headers = new HeaderCollection(headers);
			this.statusCode = statusCode;
			this.getResponseStream = getResponseStream;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001476C File Offset: 0x0001296C
		public HttpWebResponseMessage(HttpWebResponse httpResponse)
		{
			Util.CheckArgumentNull<HttpWebResponse>(httpResponse, "httpResponse");
			this.headers = new HeaderCollection(httpResponse.Headers);
			this.statusCode = (int)httpResponse.StatusCode;
			this.getResponseStream = new Func<Stream>(httpResponse.GetResponseStream);
			this.httpWebResponse = httpResponse;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000147C2 File Offset: 0x000129C2
		internal HttpWebResponseMessage(HeaderCollection headers, int statusCode, Func<Stream> getResponseStream)
		{
			this.headers = headers;
			this.statusCode = statusCode;
			this.getResponseStream = getResponseStream;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000147DF File Offset: 0x000129DF
		internal HttpWebResponseMessage(HeaderCollection headers, int statusCode, Func<Stream> getResponseStream, IODataResponseMessage underlyingResponseMessage)
			: this(headers, statusCode, getResponseStream)
		{
			this.underlyingResponseMessage = underlyingResponseMessage;
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000147F2 File Offset: 0x000129F2
		public virtual IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.headers.AsEnumerable();
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000147FF File Offset: 0x000129FF
		public HttpWebResponse Response
		{
			get
			{
				return this.httpWebResponse;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00014807 File Offset: 0x00012A07
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x0001480F File Offset: 0x00012A0F
		public virtual int StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00014818 File Offset: 0x00012A18
		public virtual string GetHeader(string headerName)
		{
			Util.CheckArgumentNullAndEmpty(headerName, "headerName");
			string text;
			if (this.headers.TryGetHeader(headerName, out text))
			{
				return text;
			}
			if (string.Equals(headerName, "Content-Length", StringComparison.Ordinal))
			{
				return "-1";
			}
			return null;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00014857 File Offset: 0x00012A57
		public virtual void SetHeader(string headerName, string headerValue)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001485E File Offset: 0x00012A5E
		public virtual Stream GetStream()
		{
			return this.getResponseStream();
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001486B File Offset: 0x00012A6B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001487C File Offset: 0x00012A7C
		protected virtual void Dispose(bool disposing)
		{
			HttpWebResponse httpWebResponse = this.httpWebResponse;
			this.httpWebResponse = null;
			if (httpWebResponse != null)
			{
				((IDisposable)httpWebResponse).Dispose();
			}
			WebUtil.DisposeMessage(this.underlyingResponseMessage);
			this.underlyingResponseMessage = null;
		}

		// Token: 0x04000300 RID: 768
		private readonly HeaderCollection headers;

		// Token: 0x04000301 RID: 769
		private readonly Func<Stream> getResponseStream;

		// Token: 0x04000302 RID: 770
		private readonly int statusCode;

		// Token: 0x04000303 RID: 771
		private HttpWebResponse httpWebResponse;

		// Token: 0x04000304 RID: 772
		private IODataResponseMessage underlyingResponseMessage;
	}
}
