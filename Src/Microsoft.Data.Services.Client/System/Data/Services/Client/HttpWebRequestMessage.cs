using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200008B RID: 139
	public class HttpWebRequestMessage : DataServiceClientRequestMessage
	{
		// Token: 0x060004F9 RID: 1273 RVA: 0x00014070 File Offset: 0x00012270
		public HttpWebRequestMessage(DataServiceClientRequestMessageArgs args)
			: base(args.ActualMethod)
		{
			Util.CheckArgumentNull<DataServiceClientRequestMessageArgs>(args, "args");
			this.effectiveHttpMethod = args.Method;
			this.requestUrl = args.RequestUri;
			this.httpRequest = HttpWebRequestMessage.CreateRequest(this.ActualMethod, this.Url, args);
			foreach (KeyValuePair<string, string> keyValuePair in args.Headers)
			{
				this.SetHeader(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00014114 File Offset: 0x00012314
		internal HttpWebRequestMessage(DataServiceClientRequestMessageArgs args, RequestInfo requestInfo)
			: this(args)
		{
			this.requestInfo = requestInfo;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00014124 File Offset: 0x00012324
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0001412C File Offset: 0x0001232C
		public override Uri Url
		{
			get
			{
				return this.requestUrl;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00014133 File Offset: 0x00012333
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0001413B File Offset: 0x0001233B
		public override string Method
		{
			get
			{
				return this.effectiveHttpMethod;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00014144 File Offset: 0x00012344
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>(this.httpRequest.Headers.Count);
				foreach (string text in this.httpRequest.Headers.AllKeys)
				{
					string text2 = this.httpRequest.Headers[text];
					list.Add(new KeyValuePair<string, string>(text, text2));
				}
				return list;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x000141AF File Offset: 0x000123AF
		public HttpWebRequest HttpWebRequest
		{
			get
			{
				return this.httpRequest;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000141B7 File Offset: 0x000123B7
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x000141C4 File Offset: 0x000123C4
		public override ICredentials Credentials
		{
			get
			{
				return this.httpRequest.Credentials;
			}
			set
			{
				this.httpRequest.Credentials = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000141D2 File Offset: 0x000123D2
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x000141E0 File Offset: 0x000123E0
		public override int Timeout
		{
			get
			{
				return this.httpRequest.Timeout;
			}
			set
			{
				this.httpRequest.Timeout = (int)Math.Min(2147483647.0, new TimeSpan(0, 0, value).TotalMilliseconds);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00014217 File Offset: 0x00012417
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x00014224 File Offset: 0x00012424
		public override bool SendChunked
		{
			get
			{
				return this.httpRequest.SendChunked;
			}
			set
			{
				this.httpRequest.SendChunked = value;
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00014232 File Offset: 0x00012432
		public override string GetHeader(string headerName)
		{
			Util.CheckArgumentNullAndEmpty(headerName, "headerName");
			return HttpWebRequestMessage.GetHeaderValue(this.httpRequest, headerName);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001424B File Offset: 0x0001244B
		public override void SetHeader(string headerName, string headerValue)
		{
			Util.CheckArgumentNullAndEmpty(headerName, "headerName");
			HttpWebRequestMessage.SetHeaderValue(this.httpRequest, headerName, headerValue);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00014265 File Offset: 0x00012465
		public override Stream GetStream()
		{
			if (this.inSendingRequest2Event)
			{
				throw new NotSupportedException(Strings.ODataRequestMessage_GetStreamMethodNotSupported);
			}
			this.FireSendingRequest();
			return this.httpRequest.GetRequestStream();
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001428B File Offset: 0x0001248B
		public override void Abort()
		{
			this.httpRequest.Abort();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00014298 File Offset: 0x00012498
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			if (this.inSendingRequest2Event)
			{
				throw new NotSupportedException(Strings.ODataRequestMessage_GetStreamMethodNotSupported);
			}
			this.FireSendingRequest();
			return this.httpRequest.BeginGetRequestStream(callback, state);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000142C0 File Offset: 0x000124C0
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			return this.httpRequest.EndGetRequestStream(asyncResult);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000142CE File Offset: 0x000124CE
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			this.FireSendingRequest();
			return this.httpRequest.BeginGetResponse(callback, state);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000142E4 File Offset: 0x000124E4
		public override IODataResponseMessage EndGetResponse(IAsyncResult asyncResult)
		{
			IODataResponseMessage iodataResponseMessage;
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)this.httpRequest.EndGetResponse(asyncResult);
				iodataResponseMessage = new HttpWebResponseMessage(httpWebResponse);
			}
			catch (WebException ex)
			{
				throw HttpWebRequestMessage.ConvertToDataServiceWebException(ex);
			}
			return iodataResponseMessage;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00014328 File Offset: 0x00012528
		public override IODataResponseMessage GetResponse()
		{
			this.FireSendingRequest();
			IODataResponseMessage iodataResponseMessage;
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)this.httpRequest.GetResponse();
				iodataResponseMessage = new HttpWebResponseMessage(httpWebResponse);
			}
			catch (WebException ex)
			{
				throw HttpWebRequestMessage.ConvertToDataServiceWebException(ex);
			}
			return iodataResponseMessage;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00014370 File Offset: 0x00012570
		internal static void SetHttpWebRequestContentLength(HttpWebRequest httpWebRequest, long contentLength)
		{
			httpWebRequest.ContentLength = contentLength;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00014379 File Offset: 0x00012579
		internal static void SetAcceptCharset(HttpWebRequest httpWebRequest, string headerValue)
		{
			httpWebRequest.Headers["Accept-Charset"] = headerValue;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001438C File Offset: 0x0001258C
		internal static void SetUserAgentHeader(HttpWebRequest httpWebRequest, string headerValue)
		{
			httpWebRequest.UserAgent = headerValue;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00014395 File Offset: 0x00012595
		internal void AddHeadersToReset(IEnumerable<string> headerNames)
		{
			if (this.headersToReset == null)
			{
				this.headersToReset = new List<string>();
			}
			this.headersToReset.AddRange(headerNames);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000143B6 File Offset: 0x000125B6
		internal void BeforeSendingRequest2Event()
		{
			this.inSendingRequest2Event = true;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000143BF File Offset: 0x000125BF
		internal void AfterSendingRequest2Event()
		{
			this.inSendingRequest2Event = false;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000143C8 File Offset: 0x000125C8
		private static void SetHeaderValues(HttpWebRequestMessage requestMessage, HeaderCollection cachedHeaders, string effectiveHttpMethod)
		{
			bool flag = true;
			HttpWebRequest httpWebRequest = requestMessage.httpRequest;
			string text = requestMessage.Method;
			string text2 = null;
			cachedHeaders.TryGetHeader("Content-Type", out text2);
			if (string.CompareOrdinal(effectiveHttpMethod, "GET") != 0)
			{
				if (string.CompareOrdinal(effectiveHttpMethod, "DELETE") == 0)
				{
					httpWebRequest.ContentType = null;
					HttpWebRequestMessage.SetHttpWebRequestContentLength(httpWebRequest, 0L);
				}
				else
				{
					httpWebRequest.ContentType = text2;
				}
				if (requestMessage.requestInfo.UsePostTunneling && string.CompareOrdinal(effectiveHttpMethod, "POST") != 0)
				{
					httpWebRequest.Headers["X-HTTP-Method"] = effectiveHttpMethod;
					text = "POST";
					flag = false;
				}
			}
			httpWebRequest.Headers.Remove(HttpRequestHeader.IfMatch);
			if (flag)
			{
				httpWebRequest.Headers.Remove("X-HTTP-Method");
			}
			httpWebRequest.Method = text;
			if (requestMessage.headersToReset != null)
			{
				foreach (string text3 in requestMessage.headersToReset)
				{
					HttpWebRequestMessage.SetHeaderValue(httpWebRequest, text3, cachedHeaders.GetHeader(text3));
				}
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000144DC File Offset: 0x000126DC
		private static HttpWebRequest CreateRequest(string method, Uri requestUrl, DataServiceClientRequestMessageArgs args)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
			httpWebRequest.KeepAlive = true;
			httpWebRequest.Method = method;
			return httpWebRequest;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00014504 File Offset: 0x00012704
		private static void SetHeaderValue(HttpWebRequest request, string headerName, string headerValue)
		{
			if (string.Equals(headerName, "Accept", StringComparison.OrdinalIgnoreCase))
			{
				request.Accept = headerValue;
				return;
			}
			if (string.Equals(headerName, "Content-Type", StringComparison.OrdinalIgnoreCase))
			{
				request.ContentType = headerValue;
				return;
			}
			if (string.Equals(headerName, "Content-Length", StringComparison.OrdinalIgnoreCase))
			{
				HttpWebRequestMessage.SetHttpWebRequestContentLength(request, long.Parse(headerValue, CultureInfo.InvariantCulture));
				return;
			}
			if (string.Equals(headerName, "User-Agent", StringComparison.OrdinalIgnoreCase))
			{
				HttpWebRequestMessage.SetUserAgentHeader(request, headerValue);
				return;
			}
			if (string.Equals(headerName, "Accept-Charset", StringComparison.OrdinalIgnoreCase))
			{
				HttpWebRequestMessage.SetAcceptCharset(request, headerValue);
				return;
			}
			request.Headers[headerName] = headerValue;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00014598 File Offset: 0x00012798
		private static string GetHeaderValue(HttpWebRequest request, string headerName)
		{
			if (string.Equals(headerName, "Accept", StringComparison.OrdinalIgnoreCase))
			{
				return request.Accept;
			}
			if (string.Equals(headerName, "Content-Type", StringComparison.OrdinalIgnoreCase))
			{
				return request.ContentType;
			}
			if (string.Equals(headerName, "Content-Length", StringComparison.OrdinalIgnoreCase))
			{
				return request.ContentLength.ToString(CultureInfo.InvariantCulture);
			}
			return request.Headers[headerName];
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00014600 File Offset: 0x00012800
		private static DataServiceTransportException ConvertToDataServiceWebException(WebException webException)
		{
			HttpWebResponseMessage httpWebResponseMessage = null;
			if (webException.Response != null)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)webException.Response;
				httpWebResponseMessage = new HttpWebResponseMessage(httpWebResponse);
			}
			return new DataServiceTransportException(httpWebResponseMessage, webException);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00014634 File Offset: 0x00012834
		private void FireSendingRequest()
		{
			if (this.fireSendingRequestMethodCalled || this.requestInfo == null)
			{
				return;
			}
			this.fireSendingRequestMethodCalled = true;
			HeaderCollection headerCollection = null;
			if (this.requestInfo.HasSendingRequestEventHandlers)
			{
				headerCollection = new HeaderCollection();
				foreach (KeyValuePair<string, string> keyValuePair in this.Headers)
				{
					headerCollection.SetHeader(keyValuePair.Key, keyValuePair.Value);
				}
				headerCollection.SetHeader("Content-Length", this.httpRequest.ContentLength.ToString(CultureInfo.InvariantCulture));
			}
			if (this.requestInfo.HasSendingRequestEventHandlers)
			{
				WebHeaderCollection headers = this.httpRequest.Headers;
				SendingRequestEventArgs sendingRequestEventArgs = new SendingRequestEventArgs(this.httpRequest, headers);
				this.requestInfo.FireSendingRequest(sendingRequestEventArgs);
				if (!object.ReferenceEquals(sendingRequestEventArgs.Request, this.httpRequest))
				{
					this.httpRequest = (HttpWebRequest)sendingRequestEventArgs.Request;
				}
				HttpWebRequestMessage.SetHeaderValues(this, headerCollection, this.Method);
			}
		}

		// Token: 0x040002F9 RID: 761
		private readonly Uri requestUrl;

		// Token: 0x040002FA RID: 762
		private readonly string effectiveHttpMethod;

		// Token: 0x040002FB RID: 763
		private readonly RequestInfo requestInfo;

		// Token: 0x040002FC RID: 764
		private HttpWebRequest httpRequest;

		// Token: 0x040002FD RID: 765
		private List<string> headersToReset;

		// Token: 0x040002FE RID: 766
		private bool fireSendingRequestMethodCalled;

		// Token: 0x040002FF RID: 767
		private bool inSendingRequest2Event;
	}
}
