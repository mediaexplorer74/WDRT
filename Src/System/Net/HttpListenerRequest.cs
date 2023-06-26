using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	/// <summary>Describes an incoming HTTP request to an <see cref="T:System.Net.HttpListener" /> object. This class cannot be inherited.</summary>
	// Token: 0x020000FE RID: 254
	public sealed class HttpListenerRequest
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x00032FA8 File Offset: 0x000311A8
		internal unsafe HttpListenerRequest(HttpListenerContext httpContext, RequestContextBase memoryBlob)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, this, ".ctor", "httpContext#" + ValidationHelper.HashString(httpContext) + " memoryBlob# " + ValidationHelper.HashString((IntPtr)((void*)memoryBlob.RequestBlob)));
			}
			if (Logging.On)
			{
				Logging.Associate(Logging.HttpListener, this, httpContext);
			}
			this.m_HttpContext = httpContext;
			this.m_MemoryBlob = memoryBlob;
			this.m_BoundaryType = BoundaryType.None;
			this.m_RequestId = memoryBlob.RequestBlob->RequestId;
			this.m_ConnectionId = memoryBlob.RequestBlob->ConnectionId;
			this.m_SslStatus = ((memoryBlob.RequestBlob->pSslInfo == null) ? HttpListenerRequest.SslStatus.Insecure : ((memoryBlob.RequestBlob->pSslInfo->SslClientCertNegotiated == 0U) ? HttpListenerRequest.SslStatus.NoClientCert : HttpListenerRequest.SslStatus.ClientCert));
			if (memoryBlob.RequestBlob->pRawUrl != null && memoryBlob.RequestBlob->RawUrlLength > 0)
			{
				this.m_RawUrl = Marshal.PtrToStringAnsi((IntPtr)((void*)memoryBlob.RequestBlob->pRawUrl), (int)memoryBlob.RequestBlob->RawUrlLength);
			}
			UnsafeNclNativeMethods.HttpApi.HTTP_COOKED_URL cookedUrl = memoryBlob.RequestBlob->CookedUrl;
			if (cookedUrl.pHost != null && cookedUrl.HostLength > 0)
			{
				this.m_CookedUrlHost = Marshal.PtrToStringUni((IntPtr)((void*)cookedUrl.pHost), (int)(cookedUrl.HostLength / 2));
			}
			if (cookedUrl.pAbsPath != null && cookedUrl.AbsPathLength > 0)
			{
				this.m_CookedUrlPath = Marshal.PtrToStringUni((IntPtr)((void*)cookedUrl.pAbsPath), (int)(cookedUrl.AbsPathLength / 2));
			}
			if (cookedUrl.pQueryString != null && cookedUrl.QueryStringLength > 0)
			{
				this.m_CookedUrlQuery = Marshal.PtrToStringUni((IntPtr)((void*)cookedUrl.pQueryString), (int)(cookedUrl.QueryStringLength / 2));
			}
			this.m_Version = new Version((int)memoryBlob.RequestBlob->Version.MajorVersion, (int)memoryBlob.RequestBlob->Version.MinorVersion);
			this.m_ClientCertState = ListenerClientCertState.NotInitialized;
			this.m_KeepAlive = TriState.Unspecified;
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, this, ".ctor", string.Concat(new string[]
				{
					"httpContext#",
					ValidationHelper.HashString(httpContext),
					" RequestUri:",
					ValidationHelper.ToString(this.RequestUri),
					" Content-Length:",
					ValidationHelper.ToString(this.ContentLength64),
					" HTTP Method:",
					ValidationHelper.ToString(this.HttpMethod)
				}));
			}
			if (Logging.On)
			{
				StringBuilder stringBuilder = new StringBuilder("HttpListenerRequest Headers:\n");
				for (int i = 0; i < this.Headers.Count; i++)
				{
					stringBuilder.Append("\t");
					stringBuilder.Append(this.Headers.GetKey(i));
					stringBuilder.Append(" : ");
					stringBuilder.Append(this.Headers.Get(i));
					stringBuilder.Append("\n");
				}
				Logging.PrintInfo(Logging.HttpListener, this, ".ctor", stringBuilder.ToString());
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0003329F File Offset: 0x0003149F
		internal HttpListenerContext HttpListenerContext
		{
			get
			{
				return this.m_HttpContext;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x000332A7 File Offset: 0x000314A7
		internal byte[] RequestBuffer
		{
			get
			{
				this.CheckDisposed();
				return this.m_MemoryBlob.RequestBuffer;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x000332BA File Offset: 0x000314BA
		internal IntPtr OriginalBlobAddress
		{
			get
			{
				this.CheckDisposed();
				return this.m_MemoryBlob.OriginalBlobAddress;
			}
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000332CD File Offset: 0x000314CD
		internal void DetachBlob(RequestContextBase memoryBlob)
		{
			if (memoryBlob != null && memoryBlob == this.m_MemoryBlob)
			{
				this.m_MemoryBlob = null;
			}
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000332E2 File Offset: 0x000314E2
		internal void ReleasePins()
		{
			this.m_MemoryBlob.ReleasePins();
		}

		/// <summary>Gets the request identifier of the incoming HTTP request.</summary>
		/// <returns>A <see cref="T:System.Guid" /> object that contains the identifier of the HTTP request.</returns>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x000332F0 File Offset: 0x000314F0
		public unsafe Guid RequestTraceIdentifier
		{
			get
			{
				Guid guid = default(Guid);
				1[(long*)(&guid)] = (long)this.RequestId;
				return guid;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00033312 File Offset: 0x00031512
		internal ulong RequestId
		{
			get
			{
				return this.m_RequestId;
			}
		}

		/// <summary>Gets the MIME types accepted by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the type names specified in the request's <see langword="Accept" /> header or <see langword="null" /> if the client request did not include an <see langword="Accept" /> header.</returns>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0003331A File Offset: 0x0003151A
		public string[] AcceptTypes
		{
			get
			{
				return HttpListenerRequest.Helpers.ParseMultivalueHeader(this.GetKnownHeader(HttpRequestHeader.Accept));
			}
		}

		/// <summary>Gets the content encoding that can be used with data sent with the request</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object suitable for use with the data in the <see cref="P:System.Net.HttpListenerRequest.InputStream" /> property.</returns>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0003332C File Offset: 0x0003152C
		public Encoding ContentEncoding
		{
			get
			{
				if (this.UserAgent != null && CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this.UserAgent, "UP"))
				{
					string text = this.Headers["x-up-devcap-post-charset"];
					if (text != null && text.Length > 0)
					{
						try
						{
							return Encoding.GetEncoding(text);
						}
						catch (ArgumentException)
						{
						}
					}
				}
				if (this.HasEntityBody && this.ContentType != null)
				{
					string attributeFromHeader = HttpListenerRequest.Helpers.GetAttributeFromHeader(this.ContentType, "charset");
					if (attributeFromHeader != null)
					{
						try
						{
							return Encoding.GetEncoding(attributeFromHeader);
						}
						catch (ArgumentException)
						{
						}
					}
				}
				return Encoding.Default;
			}
		}

		/// <summary>Gets the length of the body data included in the request.</summary>
		/// <returns>The value from the request's <see langword="Content-Length" /> header. This value is -1 if the content length is not known.</returns>
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x000333DC File Offset: 0x000315DC
		public long ContentLength64
		{
			get
			{
				if (this.m_BoundaryType == BoundaryType.None)
				{
					if ("chunked".Equals(this.GetKnownHeader(HttpRequestHeader.TransferEncoding), StringComparison.OrdinalIgnoreCase))
					{
						this.m_BoundaryType = BoundaryType.Chunked;
						this.m_ContentLength = -1L;
					}
					else
					{
						this.m_ContentLength = 0L;
						this.m_BoundaryType = BoundaryType.ContentLength;
						string knownHeader = this.GetKnownHeader(HttpRequestHeader.ContentLength);
						if (knownHeader != null && !long.TryParse(knownHeader, NumberStyles.None, CultureInfo.InvariantCulture.NumberFormat, out this.m_ContentLength))
						{
							this.m_ContentLength = 0L;
							this.m_BoundaryType = BoundaryType.Invalid;
						}
					}
				}
				return this.m_ContentLength;
			}
		}

		/// <summary>Gets the MIME type of the body data included in the request.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the text of the request's <see langword="Content-Type" /> header.</returns>
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00033462 File Offset: 0x00031662
		public string ContentType
		{
			get
			{
				return this.GetKnownHeader(HttpRequestHeader.ContentType);
			}
		}

		/// <summary>Gets the collection of header name/value pairs sent in the request.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the HTTP headers included in the request.</returns>
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0003346C File Offset: 0x0003166C
		public NameValueCollection Headers
		{
			get
			{
				if (this.m_WebHeaders == null)
				{
					this.m_WebHeaders = UnsafeNclNativeMethods.HttpApi.GetHeaders(this.RequestBuffer, this.OriginalBlobAddress);
				}
				return this.m_WebHeaders;
			}
		}

		/// <summary>Gets the HTTP method specified by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the method used in the request.</returns>
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00033493 File Offset: 0x00031693
		public string HttpMethod
		{
			get
			{
				if (this.m_HttpMethod == null)
				{
					this.m_HttpMethod = UnsafeNclNativeMethods.HttpApi.GetVerb(this.RequestBuffer, this.OriginalBlobAddress);
				}
				return this.m_HttpMethod;
			}
		}

		/// <summary>Gets a stream that contains the body data sent by the client.</summary>
		/// <returns>A readable <see cref="T:System.IO.Stream" /> object that contains the bytes sent by the client in the body of the request. This property returns <see cref="F:System.IO.Stream.Null" /> if no data is sent with the request.</returns>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x000334BC File Offset: 0x000316BC
		public Stream InputStream
		{
			get
			{
				if (Logging.On)
				{
					Logging.Enter(Logging.HttpListener, this, "InputStream_get", "");
				}
				if (this.m_RequestStream == null)
				{
					this.m_RequestStream = (this.HasEntityBody ? new HttpRequestStream(this.HttpListenerContext) : Stream.Null);
				}
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "InputStream_get", "");
				}
				return this.m_RequestStream;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the client sending this request is authenticated.</summary>
		/// <returns>
		///   <see langword="true" /> if the client was authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00033530 File Offset: 0x00031730
		public bool IsAuthenticated
		{
			get
			{
				IPrincipal user = this.HttpListenerContext.User;
				return user != null && user.Identity != null && user.Identity.IsAuthenticated;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the request is sent from the local computer.</summary>
		/// <returns>
		///   <see langword="true" /> if the request originated on the same computer as the <see cref="T:System.Net.HttpListener" /> object that provided the request; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00033561 File Offset: 0x00031761
		public bool IsLocal
		{
			get
			{
				return this.LocalEndPoint.Address.Equals(this.RemoteEndPoint.Address);
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the TCP connection used to send the request is using the Secure Sockets Layer (SSL) protocol.</summary>
		/// <returns>
		///   <see langword="true" /> if the TCP connection is using SSL; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0003357E File Offset: 0x0003177E
		public bool IsSecureConnection
		{
			get
			{
				return this.m_SslStatus > HttpListenerRequest.SslStatus.Insecure;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the TCP connection was  a WebSocket request.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.  
		///  <see langword="true" /> if the TCP connection is a WebSocket request; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0003358C File Offset: 0x0003178C
		public bool IsWebSocketRequest
		{
			get
			{
				if (!WebSocketProtocolComponent.IsSupported)
				{
					return false;
				}
				bool flag = false;
				if (string.IsNullOrEmpty(this.Headers["Connection"]) || string.IsNullOrEmpty(this.Headers["Upgrade"]))
				{
					return false;
				}
				foreach (string text in this.Headers.GetValues("Connection"))
				{
					if (string.Compare(text, "Upgrade", StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
				foreach (string text2 in this.Headers.GetValues("Upgrade"))
				{
					if (string.Compare(text2, "websocket", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Gets the query string included in the request.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains the query data included in the request <see cref="P:System.Net.HttpListenerRequest.Url" />.</returns>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0003364C File Offset: 0x0003184C
		public NameValueCollection QueryString
		{
			get
			{
				NameValueCollection nameValueCollection = new NameValueCollection();
				HttpListenerRequest.Helpers.FillFromString(nameValueCollection, this.Url.Query, true, this.ContentEncoding);
				return nameValueCollection;
			}
		}

		/// <summary>Gets the URL information (without the host and port) requested by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the raw URL for this request.</returns>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00033678 File Offset: 0x00031878
		public string RawUrl
		{
			get
			{
				return this.m_RawUrl;
			}
		}

		/// <summary>Gets the Service Provider Name (SPN) that the client sent on the request.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the SPN the client sent on the request.</returns>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00033680 File Offset: 0x00031880
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x00033688 File Offset: 0x00031888
		public string ServiceName
		{
			get
			{
				return this.m_ServiceName;
			}
			internal set
			{
				this.m_ServiceName = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Uri" /> object requested by the client.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object that identifies the resource requested by the client.</returns>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00033691 File Offset: 0x00031891
		public Uri Url
		{
			get
			{
				return this.RequestUri;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the resource that referred the client to the server.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object that contains the text of the request's <see cref="F:System.Net.HttpRequestHeader.Referer" /> header, or <see langword="null" /> if the header was not included in the request.</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0003369C File Offset: 0x0003189C
		public Uri UrlReferrer
		{
			get
			{
				string knownHeader = this.GetKnownHeader(HttpRequestHeader.Referer);
				if (knownHeader == null)
				{
					return null;
				}
				Uri uri;
				if (!Uri.TryCreate(knownHeader, UriKind.RelativeOrAbsolute, out uri))
				{
					return null;
				}
				return uri;
			}
		}

		/// <summary>Gets the user agent presented by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the text of the request's <see langword="User-Agent" /> header.</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x000336C7 File Offset: 0x000318C7
		public string UserAgent
		{
			get
			{
				return this.GetKnownHeader(HttpRequestHeader.UserAgent);
			}
		}

		/// <summary>Gets the server IP address and port number to which the request is directed.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the host address information.</returns>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x000336D1 File Offset: 0x000318D1
		public string UserHostAddress
		{
			get
			{
				return this.LocalEndPoint.ToString();
			}
		}

		/// <summary>Gets the DNS name and, if provided, the port number specified by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the text of the request's <see langword="Host" /> header.</returns>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x000336DE File Offset: 0x000318DE
		public string UserHostName
		{
			get
			{
				return this.GetKnownHeader(HttpRequestHeader.Host);
			}
		}

		/// <summary>Gets the natural languages that are preferred for the response.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the languages specified in the request's <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header or <see langword="null" /> if the client request did not include an <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header.</returns>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x000336E8 File Offset: 0x000318E8
		public string[] UserLanguages
		{
			get
			{
				return HttpListenerRequest.Helpers.ParseMultivalueHeader(this.GetKnownHeader(HttpRequestHeader.AcceptLanguage));
			}
		}

		/// <summary>Gets an error code that identifies a problem with the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> provided by the client.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains a Windows error code.</returns>
		/// <exception cref="T:System.InvalidOperationException">The client certificate has not been initialized yet by a call to the <see cref="M:System.Net.HttpListenerRequest.BeginGetClientCertificate(System.AsyncCallback,System.Object)" /> or <see cref="M:System.Net.HttpListenerRequest.GetClientCertificate" /> methods  
		///  -or -  
		///  The operation is still in progress.</exception>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x000336F8 File Offset: 0x000318F8
		public int ClientCertificateError
		{
			get
			{
				if (this.m_ClientCertState == ListenerClientCertState.NotInitialized)
				{
					throw new InvalidOperationException(System.SR.GetString("net_listener_mustcall", new object[] { "GetClientCertificate()/BeginGetClientCertificate()" }));
				}
				if (this.m_ClientCertState == ListenerClientCertState.InProgress)
				{
					throw new InvalidOperationException(System.SR.GetString("net_listener_mustcompletecall", new object[] { "GetClientCertificate()/BeginGetClientCertificate()" }));
				}
				return this.m_ClientCertificateError;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x00033758 File Offset: 0x00031958
		internal X509Certificate2 ClientCertificate
		{
			set
			{
				this.m_ClientCertificate = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x00033761 File Offset: 0x00031961
		internal ListenerClientCertState ClientCertState
		{
			set
			{
				this.m_ClientCertState = value;
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0003376A File Offset: 0x0003196A
		internal void SetClientCertificateError(int clientCertificateError)
		{
			this.m_ClientCertificateError = clientCertificateError;
		}

		/// <summary>Retrieves the client's X.509 v.3 certificate.</summary>
		/// <returns>A <see cref="N:System.Security.Cryptography.X509Certificates" /> object that contains the client's X.509 v.3 certificate.</returns>
		/// <exception cref="T:System.InvalidOperationException">A call to this method to retrieve the client's X.509 v.3 certificate is in progress and therefore another call to this method cannot be made.</exception>
		// Token: 0x06000936 RID: 2358 RVA: 0x00033774 File Offset: 0x00031974
		public X509Certificate2 GetClientCertificate()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "GetClientCertificate", "");
			}
			try
			{
				this.ProcessClientCertificate();
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "GetClientCertificate", ValidationHelper.ToString(this.m_ClientCertificate));
				}
			}
			return this.m_ClientCertificate;
		}

		/// <summary>Begins an asynchronous request for the client's X.509 v.3 certificate.</summary>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the callback delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that indicates the status of the operation.</returns>
		// Token: 0x06000937 RID: 2359 RVA: 0x000337E0 File Offset: 0x000319E0
		public IAsyncResult BeginGetClientCertificate(AsyncCallback requestCallback, object state)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, this, "BeginGetClientCertificate", "");
			}
			return this.AsyncProcessClientCertificate(requestCallback, state);
		}

		/// <summary>Ends an asynchronous request for the client's X.509 v.3 certificate.</summary>
		/// <param name="asyncResult">The pending request for the certificate.</param>
		/// <returns>The <see cref="T:System.IAsyncResult" /> object that is returned when the operation started.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling <see cref="M:System.Net.HttpListenerRequest.BeginGetClientCertificate(System.AsyncCallback,System.Object)" /><paramref name="e." /></exception>
		/// <exception cref="T:System.InvalidOperationException">This method was already called for the operation identified by <paramref name="asyncResult" />.</exception>
		// Token: 0x06000938 RID: 2360 RVA: 0x00033808 File Offset: 0x00031A08
		public X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "EndGetClientCertificate", "");
			}
			X509Certificate2 x509Certificate = null;
			try
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				ListenerClientCertAsyncResult listenerClientCertAsyncResult = asyncResult as ListenerClientCertAsyncResult;
				if (listenerClientCertAsyncResult == null || listenerClientCertAsyncResult.AsyncObject != this)
				{
					throw new ArgumentException(System.SR.GetString("net_io_invalidasyncresult"), "asyncResult");
				}
				if (listenerClientCertAsyncResult.EndCalled)
				{
					throw new InvalidOperationException(System.SR.GetString("net_io_invalidendcall", new object[] { "EndGetClientCertificate" }));
				}
				listenerClientCertAsyncResult.EndCalled = true;
				x509Certificate = listenerClientCertAsyncResult.InternalWaitForCompletion() as X509Certificate2;
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "EndGetClientCertificate", ValidationHelper.HashString(x509Certificate));
				}
			}
			return x509Certificate;
		}

		/// <summary>Retrieves the client's X.509 v.3 certificate as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="N:System.Security.Cryptography.X509Certificates" /> object that contains the client's X.509 v.3 certificate.</returns>
		// Token: 0x06000939 RID: 2361 RVA: 0x000338D4 File Offset: 0x00031AD4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<X509Certificate2> GetClientCertificateAsync()
		{
			return Task<X509Certificate2>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetClientCertificate), new Func<IAsyncResult, X509Certificate2>(this.EndGetClientCertificate), null);
		}

		/// <summary>Gets the <see cref="T:System.Net.TransportContext" /> for the client request.</summary>
		/// <returns>A <see cref="T:System.Net.TransportContext" /> object for the client request.</returns>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x000338F9 File Offset: 0x00031AF9
		public TransportContext TransportContext
		{
			get
			{
				return new HttpListenerRequestContext(this);
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00033904 File Offset: 0x00031B04
		private CookieCollection ParseCookies(Uri uri, string setCookieHeader)
		{
			CookieCollection cookieCollection = new CookieCollection();
			CookieParser cookieParser = new CookieParser(setCookieHeader);
			for (;;)
			{
				Cookie server = cookieParser.GetServer();
				if (server == null)
				{
					break;
				}
				if (server.Name.Length != 0)
				{
					cookieCollection.InternalAdd(server, true);
				}
			}
			return cookieCollection;
		}

		/// <summary>Gets the cookies sent with the request.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains cookies that accompany the request. This property returns an empty collection if the request does not contain cookies.</returns>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00033944 File Offset: 0x00031B44
		public CookieCollection Cookies
		{
			get
			{
				if (this.m_Cookies == null)
				{
					string knownHeader = this.GetKnownHeader(HttpRequestHeader.Cookie);
					if (knownHeader != null && knownHeader.Length > 0)
					{
						this.m_Cookies = this.ParseCookies(this.RequestUri, knownHeader);
					}
					if (this.m_Cookies == null)
					{
						this.m_Cookies = new CookieCollection();
					}
					if (this.HttpListenerContext.PromoteCookiesToRfc2965)
					{
						for (int i = 0; i < this.m_Cookies.Count; i++)
						{
							if (this.m_Cookies[i].Variant == CookieVariant.Rfc2109)
							{
								this.m_Cookies[i].Variant = CookieVariant.Rfc2965;
							}
						}
					}
				}
				return this.m_Cookies;
			}
		}

		/// <summary>Gets the HTTP version used by the requesting client.</summary>
		/// <returns>A <see cref="T:System.Version" /> that identifies the client's version of HTTP.</returns>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x000339E6 File Offset: 0x00031BE6
		public Version ProtocolVersion
		{
			get
			{
				return this.m_Version;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the request has associated body data.</summary>
		/// <returns>
		///   <see langword="true" /> if the request has associated body data; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x000339EE File Offset: 0x00031BEE
		public bool HasEntityBody
		{
			get
			{
				return (this.ContentLength64 > 0L && this.m_BoundaryType == BoundaryType.ContentLength) || this.m_BoundaryType == BoundaryType.Chunked || this.m_BoundaryType == BoundaryType.Multipart;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the client requests a persistent connection.</summary>
		/// <returns>
		///   <see langword="true" /> if the connection should be kept open; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00033A18 File Offset: 0x00031C18
		public bool KeepAlive
		{
			get
			{
				if (this.m_KeepAlive == TriState.Unspecified)
				{
					string text = this.Headers["Proxy-Connection"];
					if (string.IsNullOrEmpty(text))
					{
						text = this.GetKnownHeader(HttpRequestHeader.Connection);
					}
					if (string.IsNullOrEmpty(text))
					{
						if (this.ProtocolVersion >= HttpVersion.Version11)
						{
							this.m_KeepAlive = TriState.True;
						}
						else
						{
							text = this.GetKnownHeader(HttpRequestHeader.KeepAlive);
							this.m_KeepAlive = (string.IsNullOrEmpty(text) ? TriState.False : TriState.True);
						}
					}
					else
					{
						text = text.ToLower(CultureInfo.InvariantCulture);
						this.m_KeepAlive = ((text.IndexOf("close") < 0 || text.IndexOf("keep-alive") >= 0) ? TriState.True : TriState.False);
					}
				}
				return this.m_KeepAlive == TriState.True;
			}
		}

		/// <summary>Gets the client IP address and port number from which the request originated.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> that represents the IP address and port number from which the request originated.</returns>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00033ACC File Offset: 0x00031CCC
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				if (this.m_RemoteEndPoint == null)
				{
					this.m_RemoteEndPoint = UnsafeNclNativeMethods.HttpApi.GetRemoteEndPoint(this.RequestBuffer, this.OriginalBlobAddress);
				}
				return this.m_RemoteEndPoint;
			}
		}

		/// <summary>Gets the server IP address and port number to which the request is directed.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> that represents the IP address that the request is sent to.</returns>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00033AF3 File Offset: 0x00031CF3
		public IPEndPoint LocalEndPoint
		{
			get
			{
				if (this.m_LocalEndPoint == null)
				{
					this.m_LocalEndPoint = UnsafeNclNativeMethods.HttpApi.GetLocalEndPoint(this.RequestBuffer, this.OriginalBlobAddress);
				}
				return this.m_LocalEndPoint;
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00033B1C File Offset: 0x00031D1C
		internal void Close()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Close", "");
			}
			RequestContextBase memoryBlob = this.m_MemoryBlob;
			if (memoryBlob != null)
			{
				memoryBlob.Close();
				this.m_MemoryBlob = null;
			}
			this.m_IsDisposed = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "Close", "");
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00033B80 File Offset: 0x00031D80
		private unsafe ListenerClientCertAsyncResult AsyncProcessClientCertificate(AsyncCallback requestCallback, object state)
		{
			if (this.m_ClientCertState == ListenerClientCertState.InProgress)
			{
				throw new InvalidOperationException(System.SR.GetString("net_listener_callinprogress", new object[] { "GetClientCertificate()/BeginGetClientCertificate()" }));
			}
			this.m_ClientCertState = ListenerClientCertState.InProgress;
			this.HttpListenerContext.EnsureBoundHandle();
			ListenerClientCertAsyncResult listenerClientCertAsyncResult = null;
			if (this.m_SslStatus != HttpListenerRequest.SslStatus.Insecure)
			{
				uint num = 1500U;
				listenerClientCertAsyncResult = new ListenerClientCertAsyncResult(this, state, requestCallback, num);
				try
				{
					uint num2;
					uint num3;
					for (;;)
					{
						num2 = 0U;
						num3 = UnsafeNclNativeMethods.HttpApi.HttpReceiveClientCertificate(this.HttpListenerContext.RequestQueueHandle, this.m_ConnectionId, 0U, listenerClientCertAsyncResult.RequestBlob, num, &num2, listenerClientCertAsyncResult.NativeOverlapped);
						if (num3 != 234U)
						{
							break;
						}
						UnsafeNclNativeMethods.HttpApi.HTTP_SSL_CLIENT_CERT_INFO* requestBlob = listenerClientCertAsyncResult.RequestBlob;
						num = num2 + requestBlob->CertEncodedSize;
						listenerClientCertAsyncResult.Reset(num);
					}
					if (num3 != 0U && num3 != 997U)
					{
						throw new HttpListenerException((int)num3);
					}
					if (num3 == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
					{
						listenerClientCertAsyncResult.IOCompleted(num3, num2);
					}
					return listenerClientCertAsyncResult;
				}
				catch
				{
					if (listenerClientCertAsyncResult != null)
					{
						listenerClientCertAsyncResult.InternalCleanup();
					}
					throw;
				}
			}
			listenerClientCertAsyncResult = new ListenerClientCertAsyncResult(this, state, requestCallback, 0U);
			listenerClientCertAsyncResult.InvokeCallback();
			return listenerClientCertAsyncResult;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00033C84 File Offset: 0x00031E84
		private unsafe void ProcessClientCertificate()
		{
			if (this.m_ClientCertState == ListenerClientCertState.InProgress)
			{
				throw new InvalidOperationException(System.SR.GetString("net_listener_callinprogress", new object[] { "GetClientCertificate()/BeginGetClientCertificate()" }));
			}
			this.m_ClientCertState = ListenerClientCertState.InProgress;
			if (this.m_SslStatus != HttpListenerRequest.SslStatus.Insecure)
			{
				uint num = 1500U;
				for (;;)
				{
					byte[] array = new byte[checked((int)num)];
					try
					{
						byte[] array2;
						byte* ptr;
						if ((array2 = array) == null || array2.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array2[0];
						}
						UnsafeNclNativeMethods.HttpApi.HTTP_SSL_CLIENT_CERT_INFO* ptr2 = (UnsafeNclNativeMethods.HttpApi.HTTP_SSL_CLIENT_CERT_INFO*)ptr;
						uint num2 = 0U;
						uint num3 = UnsafeNclNativeMethods.HttpApi.HttpReceiveClientCertificate(this.HttpListenerContext.RequestQueueHandle, this.m_ConnectionId, 0U, ptr2, num, &num2, null);
						if (num3 == 234U)
						{
							num = num2 + ptr2->CertEncodedSize;
							continue;
						}
						if (num3 == 0U && ptr2 != null)
						{
							if (ptr2->pCertEncoded != null)
							{
								try
								{
									byte[] array3 = new byte[ptr2->CertEncodedSize];
									Marshal.Copy((IntPtr)((void*)ptr2->pCertEncoded), array3, 0, array3.Length);
									this.m_ClientCertificate = new X509Certificate2(array3);
								}
								catch (CryptographicException ex)
								{
								}
								catch (SecurityException ex2)
								{
								}
							}
							this.m_ClientCertificateError = (int)ptr2->CertFlags;
						}
					}
					finally
					{
						byte[] array2 = null;
					}
					break;
				}
			}
			this.m_ClientCertState = ListenerClientCertState.Completed;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00033DC0 File Offset: 0x00031FC0
		private string RequestScheme
		{
			get
			{
				if (!this.IsSecureConnection)
				{
					return "http";
				}
				return "https";
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00033DD5 File Offset: 0x00031FD5
		private Uri RequestUri
		{
			get
			{
				if (this.m_RequestUri == null)
				{
					this.m_RequestUri = HttpListenerRequestUriBuilder.GetRequestUri(this.m_RawUrl, this.RequestScheme, this.m_CookedUrlHost, this.m_CookedUrlPath, this.m_CookedUrlQuery);
				}
				return this.m_RequestUri;
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00033E14 File Offset: 0x00032014
		private string GetKnownHeader(HttpRequestHeader header)
		{
			return UnsafeNclNativeMethods.HttpApi.GetKnownHeader(this.RequestBuffer, this.OriginalBlobAddress, (int)header);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00033E28 File Offset: 0x00032028
		internal ChannelBinding GetChannelBinding()
		{
			return this.HttpListenerContext.Listener.GetChannelBindingFromTls(this.m_ConnectionId);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00033E40 File Offset: 0x00032040
		internal IEnumerable<TokenBinding> GetTlsTokenBindings()
		{
			if (Volatile.Read<List<TokenBinding>>(ref this.m_TokenBindings) == null)
			{
				object @lock = this.m_Lock;
				lock (@lock)
				{
					if (Volatile.Read<List<TokenBinding>>(ref this.m_TokenBindings) == null)
					{
						if (UnsafeNclNativeMethods.TokenBindingOSHelper.SupportsTokenBinding)
						{
							this.ProcessTlsTokenBindings();
						}
						else
						{
							this.m_TokenBindings = new List<TokenBinding>();
						}
					}
				}
			}
			if (this.m_TokenBindingVerifyMessageStatus != 0)
			{
				throw new HttpListenerException(this.m_TokenBindingVerifyMessageStatus);
			}
			return this.m_TokenBindings.AsReadOnly();
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00033ED0 File Offset: 0x000320D0
		private unsafe void ProcessTlsTokenBindings()
		{
			if (this.m_TokenBindings != null)
			{
				return;
			}
			this.m_TokenBindings = new List<TokenBinding>();
			UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_TOKEN_BINDING_INFO* tlsTokenBindingRequestInfo = UnsafeNclNativeMethods.HttpApi.GetTlsTokenBindingRequestInfo(this.RequestBuffer, this.OriginalBlobAddress);
			UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_TOKEN_BINDING_INFO_V1* ptr = null;
			bool flag = false;
			if (tlsTokenBindingRequestInfo == null)
			{
				ptr = UnsafeNclNativeMethods.HttpApi.GetTlsTokenBindingRequestInfo_V1(this.RequestBuffer, this.OriginalBlobAddress);
				flag = true;
			}
			if (tlsTokenBindingRequestInfo == null && ptr == null)
			{
				return;
			}
			UnsafeNclNativeMethods.HttpApi.HeapAllocHandle heapAllocHandle = null;
			this.m_TokenBindingVerifyMessageStatus = -1;
			byte[] array;
			byte* ptr2;
			if ((array = this.RequestBuffer) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			long num = (long)((byte*)ptr2 - (byte*)(void*)this.OriginalBlobAddress);
			if (flag && ptr != null)
			{
				this.m_TokenBindingVerifyMessageStatus = UnsafeNclNativeMethods.HttpApi.TokenBindingVerifyMessage_V1(ptr->TokenBinding + num, ptr->TokenBindingSize, (IntPtr)((void*)((byte*)(void*)ptr->KeyType + num)), ptr->TlsUnique + num, ptr->TlsUniqueSize, out heapAllocHandle);
			}
			else
			{
				this.m_TokenBindingVerifyMessageStatus = UnsafeNclNativeMethods.HttpApi.TokenBindingVerifyMessage(tlsTokenBindingRequestInfo->TokenBinding + num, tlsTokenBindingRequestInfo->TokenBindingSize, tlsTokenBindingRequestInfo->KeyType, tlsTokenBindingRequestInfo->TlsUnique + num, tlsTokenBindingRequestInfo->TlsUniqueSize, out heapAllocHandle);
			}
			array = null;
			if (this.m_TokenBindingVerifyMessageStatus != 0)
			{
				throw new HttpListenerException(this.m_TokenBindingVerifyMessageStatus);
			}
			using (heapAllocHandle)
			{
				if (flag)
				{
					this.GenerateTokenBindings_V1(heapAllocHandle);
				}
				else
				{
					this.GenerateTokenBindings(heapAllocHandle);
				}
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00034038 File Offset: 0x00032238
		private unsafe void GenerateTokenBindings(UnsafeNclNativeMethods.HttpApi.HeapAllocHandle handle)
		{
			UnsafeNclNativeMethods.HttpApi.TOKENBINDING_RESULT_LIST* ptr = (UnsafeNclNativeMethods.HttpApi.TOKENBINDING_RESULT_LIST*)(void*)handle.DangerousGetHandle();
			int num = 0;
			while ((long)num < (long)((ulong)ptr->resultCount))
			{
				UnsafeNclNativeMethods.HttpApi.TOKENBINDING_RESULT_DATA* ptr2 = ptr->resultData + num;
				if (ptr2 != null)
				{
					byte[] array = new byte[ptr2->identifierSize];
					Marshal.Copy((IntPtr)((void*)ptr2->identifierData), array, 0, array.Length);
					if (ptr2->bindingType == UnsafeNclNativeMethods.HttpApi.TOKENBINDING_TYPE.TOKENBINDING_TYPE_PROVIDED)
					{
						this.m_TokenBindings.Add(new TokenBinding(TokenBindingType.Provided, array));
					}
					else if (ptr2->bindingType == UnsafeNclNativeMethods.HttpApi.TOKENBINDING_TYPE.TOKENBINDING_TYPE_REFERRED)
					{
						this.m_TokenBindings.Add(new TokenBinding(TokenBindingType.Referred, array));
					}
				}
				num++;
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000340D4 File Offset: 0x000322D4
		private unsafe void GenerateTokenBindings_V1(UnsafeNclNativeMethods.HttpApi.HeapAllocHandle handle)
		{
			UnsafeNclNativeMethods.HttpApi.TOKENBINDING_RESULT_LIST_V1* ptr = (UnsafeNclNativeMethods.HttpApi.TOKENBINDING_RESULT_LIST_V1*)(void*)handle.DangerousGetHandle();
			int num = 0;
			while ((long)num < (long)((ulong)ptr->resultCount))
			{
				UnsafeNclNativeMethods.HttpApi.TOKENBINDING_RESULT_DATA_V1* ptr2 = ptr->resultData + num;
				if (ptr2 != null)
				{
					byte[] array = new byte[ptr2->identifierSize - 1U];
					Marshal.Copy((IntPtr)((void*)(&ptr2->identifierData->hashAlgorithm)), array, 0, array.Length);
					if (ptr2->identifierData->bindingType == UnsafeNclNativeMethods.HttpApi.TOKENBINDING_TYPE.TOKENBINDING_TYPE_PROVIDED)
					{
						this.m_TokenBindings.Add(new TokenBinding(TokenBindingType.Provided, array));
					}
					else if (ptr2->identifierData->bindingType == UnsafeNclNativeMethods.HttpApi.TOKENBINDING_TYPE.TOKENBINDING_TYPE_REFERRED)
					{
						this.m_TokenBindings.Add(new TokenBinding(TokenBindingType.Referred, array));
					}
				}
				num++;
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00034187 File Offset: 0x00032387
		internal void CheckDisposed()
		{
			if (this.m_IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x04000E0F RID: 3599
		private Uri m_RequestUri;

		// Token: 0x04000E10 RID: 3600
		private ulong m_RequestId;

		// Token: 0x04000E11 RID: 3601
		internal ulong m_ConnectionId;

		// Token: 0x04000E12 RID: 3602
		private HttpListenerRequest.SslStatus m_SslStatus;

		// Token: 0x04000E13 RID: 3603
		private string m_RawUrl;

		// Token: 0x04000E14 RID: 3604
		private string m_CookedUrlHost;

		// Token: 0x04000E15 RID: 3605
		private string m_CookedUrlPath;

		// Token: 0x04000E16 RID: 3606
		private string m_CookedUrlQuery;

		// Token: 0x04000E17 RID: 3607
		private long m_ContentLength;

		// Token: 0x04000E18 RID: 3608
		private Stream m_RequestStream;

		// Token: 0x04000E19 RID: 3609
		private string m_HttpMethod;

		// Token: 0x04000E1A RID: 3610
		private TriState m_KeepAlive;

		// Token: 0x04000E1B RID: 3611
		private Version m_Version;

		// Token: 0x04000E1C RID: 3612
		private WebHeaderCollection m_WebHeaders;

		// Token: 0x04000E1D RID: 3613
		private IPEndPoint m_LocalEndPoint;

		// Token: 0x04000E1E RID: 3614
		private IPEndPoint m_RemoteEndPoint;

		// Token: 0x04000E1F RID: 3615
		private BoundaryType m_BoundaryType;

		// Token: 0x04000E20 RID: 3616
		private ListenerClientCertState m_ClientCertState;

		// Token: 0x04000E21 RID: 3617
		private X509Certificate2 m_ClientCertificate;

		// Token: 0x04000E22 RID: 3618
		private int m_ClientCertificateError;

		// Token: 0x04000E23 RID: 3619
		private RequestContextBase m_MemoryBlob;

		// Token: 0x04000E24 RID: 3620
		private CookieCollection m_Cookies;

		// Token: 0x04000E25 RID: 3621
		private HttpListenerContext m_HttpContext;

		// Token: 0x04000E26 RID: 3622
		private bool m_IsDisposed;

		// Token: 0x04000E27 RID: 3623
		internal const uint CertBoblSize = 1500U;

		// Token: 0x04000E28 RID: 3624
		private string m_ServiceName;

		// Token: 0x04000E29 RID: 3625
		private object m_Lock = new object();

		// Token: 0x04000E2A RID: 3626
		private List<TokenBinding> m_TokenBindings;

		// Token: 0x04000E2B RID: 3627
		private int m_TokenBindingVerifyMessageStatus;

		// Token: 0x02000701 RID: 1793
		private enum SslStatus : byte
		{
			// Token: 0x040030B5 RID: 12469
			Insecure,
			// Token: 0x040030B6 RID: 12470
			NoClientCert,
			// Token: 0x040030B7 RID: 12471
			ClientCert
		}

		// Token: 0x02000702 RID: 1794
		private static class Helpers
		{
			// Token: 0x06004079 RID: 16505 RVA: 0x0010DFF8 File Offset: 0x0010C1F8
			internal static string GetAttributeFromHeader(string headerValue, string attrName)
			{
				if (headerValue == null)
				{
					return null;
				}
				int length = headerValue.Length;
				int length2 = attrName.Length;
				int i;
				for (i = 1; i < length; i += length2)
				{
					i = CultureInfo.InvariantCulture.CompareInfo.IndexOf(headerValue, attrName, i, CompareOptions.IgnoreCase);
					if (i < 0 || i + length2 >= length)
					{
						break;
					}
					char c = headerValue[i - 1];
					char c2 = headerValue[i + length2];
					if ((c == ';' || c == ',' || char.IsWhiteSpace(c)) && (c2 == '=' || char.IsWhiteSpace(c2)))
					{
						break;
					}
				}
				if (i < 0 || i >= length)
				{
					return null;
				}
				i += length2;
				while (i < length && char.IsWhiteSpace(headerValue[i]))
				{
					i++;
				}
				if (i >= length || headerValue[i] != '=')
				{
					return null;
				}
				i++;
				while (i < length && char.IsWhiteSpace(headerValue[i]))
				{
					i++;
				}
				if (i >= length)
				{
					return null;
				}
				string text;
				if (i < length && headerValue[i] == '"')
				{
					if (i == length - 1)
					{
						return null;
					}
					int num = headerValue.IndexOf('"', i + 1);
					if (num < 0 || num == i + 1)
					{
						return null;
					}
					text = headerValue.Substring(i + 1, num - i - 1).Trim();
				}
				else
				{
					int num = i;
					while (num < length && headerValue[num] != ' ' && headerValue[num] != ',')
					{
						num++;
					}
					if (num == i)
					{
						return null;
					}
					text = headerValue.Substring(i, num - i).Trim();
				}
				return text;
			}

			// Token: 0x0600407A RID: 16506 RVA: 0x0010E164 File Offset: 0x0010C364
			internal static string[] ParseMultivalueHeader(string s)
			{
				if (s == null)
				{
					return null;
				}
				int length = s.Length;
				ArrayList arrayList = new ArrayList();
				int i = 0;
				while (i < length)
				{
					int num = s.IndexOf(',', i);
					if (num < 0)
					{
						num = length;
					}
					arrayList.Add(s.Substring(i, num - i));
					i = num + 1;
					if (i < length && s[i] == ' ')
					{
						i++;
					}
				}
				int count = arrayList.Count;
				string[] array;
				if (count == 0)
				{
					array = new string[] { string.Empty };
				}
				else
				{
					array = new string[count];
					arrayList.CopyTo(0, array, 0, count);
				}
				return array;
			}

			// Token: 0x0600407B RID: 16507 RVA: 0x0010E1FC File Offset: 0x0010C3FC
			private static string UrlDecodeStringFromStringInternal(string s, Encoding e)
			{
				int length = s.Length;
				HttpListenerRequest.Helpers.UrlDecoder urlDecoder = new HttpListenerRequest.Helpers.UrlDecoder(length, e);
				int i = 0;
				while (i < length)
				{
					char c = s[i];
					if (c == '+')
					{
						c = ' ';
						goto IL_106;
					}
					if (c != '%' || i >= length - 2)
					{
						goto IL_106;
					}
					if (s[i + 1] == 'u' && i < length - 5)
					{
						int num = HttpListenerRequest.Helpers.HexToInt(s[i + 2]);
						int num2 = HttpListenerRequest.Helpers.HexToInt(s[i + 3]);
						int num3 = HttpListenerRequest.Helpers.HexToInt(s[i + 4]);
						int num4 = HttpListenerRequest.Helpers.HexToInt(s[i + 5]);
						if (num < 0 || num2 < 0 || num3 < 0 || num4 < 0)
						{
							goto IL_106;
						}
						c = (char)((num << 12) | (num2 << 8) | (num3 << 4) | num4);
						i += 5;
						urlDecoder.AddChar(c);
					}
					else
					{
						int num5 = HttpListenerRequest.Helpers.HexToInt(s[i + 1]);
						int num6 = HttpListenerRequest.Helpers.HexToInt(s[i + 2]);
						if (num5 < 0 || num6 < 0)
						{
							goto IL_106;
						}
						byte b = (byte)((num5 << 4) | num6);
						i += 2;
						urlDecoder.AddByte(b);
					}
					IL_120:
					i++;
					continue;
					IL_106:
					if ((c & 'ﾀ') == '\0')
					{
						urlDecoder.AddByte((byte)c);
						goto IL_120;
					}
					urlDecoder.AddChar(c);
					goto IL_120;
				}
				return urlDecoder.GetString();
			}

			// Token: 0x0600407C RID: 16508 RVA: 0x0010E33A File Offset: 0x0010C53A
			private static int HexToInt(char h)
			{
				if (h >= '0' && h <= '9')
				{
					return (int)(h - '0');
				}
				if (h >= 'a' && h <= 'f')
				{
					return (int)(h - 'a' + '\n');
				}
				if (h < 'A' || h > 'F')
				{
					return -1;
				}
				return (int)(h - 'A' + '\n');
			}

			// Token: 0x0600407D RID: 16509 RVA: 0x0010E370 File Offset: 0x0010C570
			internal static void FillFromString(NameValueCollection nvc, string s, bool urlencoded, Encoding encoding)
			{
				int num = ((s != null) ? s.Length : 0);
				for (int i = ((s.Length > 0 && s[0] == '?') ? 1 : 0); i < num; i++)
				{
					int num2 = i;
					int num3 = -1;
					while (i < num)
					{
						char c = s[i];
						if (c == '=')
						{
							if (num3 < 0)
							{
								num3 = i;
							}
						}
						else if (c == '&')
						{
							break;
						}
						i++;
					}
					string text = null;
					string text2;
					if (num3 >= 0)
					{
						text = s.Substring(num2, num3 - num2);
						text2 = s.Substring(num3 + 1, i - num3 - 1);
					}
					else
					{
						text2 = s.Substring(num2, i - num2);
					}
					if (urlencoded)
					{
						nvc.Add((text == null) ? null : HttpListenerRequest.Helpers.UrlDecodeStringFromStringInternal(text, encoding), HttpListenerRequest.Helpers.UrlDecodeStringFromStringInternal(text2, encoding));
					}
					else
					{
						nvc.Add(text, text2);
					}
					if (i == num - 1 && s[i] == '&')
					{
						nvc.Add(null, "");
					}
				}
			}

			// Token: 0x020008CA RID: 2250
			private class UrlDecoder
			{
				// Token: 0x0600460B RID: 17931 RVA: 0x00124444 File Offset: 0x00122644
				private void FlushBytes()
				{
					if (this._numBytes > 0)
					{
						this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
						this._numBytes = 0;
					}
				}

				// Token: 0x0600460C RID: 17932 RVA: 0x00124492 File Offset: 0x00122692
				internal UrlDecoder(int bufferSize, Encoding encoding)
				{
					this._bufferSize = bufferSize;
					this._encoding = encoding;
					this._charBuffer = new char[bufferSize];
				}

				// Token: 0x0600460D RID: 17933 RVA: 0x001244B4 File Offset: 0x001226B4
				internal void AddChar(char ch)
				{
					if (this._numBytes > 0)
					{
						this.FlushBytes();
					}
					char[] charBuffer = this._charBuffer;
					int numChars = this._numChars;
					this._numChars = numChars + 1;
					charBuffer[numChars] = ch;
				}

				// Token: 0x0600460E RID: 17934 RVA: 0x001244EC File Offset: 0x001226EC
				internal void AddByte(byte b)
				{
					if (this._byteBuffer == null)
					{
						this._byteBuffer = new byte[this._bufferSize];
					}
					byte[] byteBuffer = this._byteBuffer;
					int numBytes = this._numBytes;
					this._numBytes = numBytes + 1;
					byteBuffer[numBytes] = b;
				}

				// Token: 0x0600460F RID: 17935 RVA: 0x0012452B File Offset: 0x0012272B
				internal string GetString()
				{
					if (this._numBytes > 0)
					{
						this.FlushBytes();
					}
					if (this._numChars > 0)
					{
						return new string(this._charBuffer, 0, this._numChars);
					}
					return string.Empty;
				}

				// Token: 0x04003B4B RID: 15179
				private int _bufferSize;

				// Token: 0x04003B4C RID: 15180
				private int _numChars;

				// Token: 0x04003B4D RID: 15181
				private char[] _charBuffer;

				// Token: 0x04003B4E RID: 15182
				private int _numBytes;

				// Token: 0x04003B4F RID: 15183
				private byte[] _byteBuffer;

				// Token: 0x04003B50 RID: 15184
				private Encoding _encoding;
			}
		}
	}
}
