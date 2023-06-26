using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	/// <summary>Provides common methods for sending data to and receiving data from a resource identified by a URI.</summary>
	// Token: 0x02000167 RID: 359
	[ComVisible(true)]
	public class WebClient : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebClient" /> class.</summary>
		// Token: 0x06000CFA RID: 3322 RVA: 0x00045979 File Offset: 0x00043B79
		public WebClient()
		{
			if (base.GetType() == typeof(WebClient))
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000459B4 File Offset: 0x00043BB4
		private void InitWebClientAsync()
		{
			if (!this.m_InitWebClientAsync)
			{
				this.openReadOperationCompleted = new SendOrPostCallback(this.OpenReadOperationCompleted);
				this.openWriteOperationCompleted = new SendOrPostCallback(this.OpenWriteOperationCompleted);
				this.downloadStringOperationCompleted = new SendOrPostCallback(this.DownloadStringOperationCompleted);
				this.downloadDataOperationCompleted = new SendOrPostCallback(this.DownloadDataOperationCompleted);
				this.downloadFileOperationCompleted = new SendOrPostCallback(this.DownloadFileOperationCompleted);
				this.uploadStringOperationCompleted = new SendOrPostCallback(this.UploadStringOperationCompleted);
				this.uploadDataOperationCompleted = new SendOrPostCallback(this.UploadDataOperationCompleted);
				this.uploadFileOperationCompleted = new SendOrPostCallback(this.UploadFileOperationCompleted);
				this.uploadValuesOperationCompleted = new SendOrPostCallback(this.UploadValuesOperationCompleted);
				this.reportDownloadProgressChanged = new SendOrPostCallback(this.ReportDownloadProgressChanged);
				this.reportUploadProgressChanged = new SendOrPostCallback(this.ReportUploadProgressChanged);
				this.m_Progress = new WebClient.ProgressData();
				this.m_InitWebClientAsync = true;
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00045AA4 File Offset: 0x00043CA4
		private void ClearWebClientState()
		{
			if (this.AnotherCallInProgress(Interlocked.Increment(ref this.m_CallNesting)))
			{
				this.CompleteWebClientState();
				throw new NotSupportedException(SR.GetString("net_webclient_no_concurrent_io_allowed"));
			}
			this.m_ContentLength = -1L;
			this.m_WebResponse = null;
			this.m_WebRequest = null;
			this.m_Method = null;
			this.m_Cancelled = false;
			if (this.m_Progress != null)
			{
				this.m_Progress.Reset();
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00045B11 File Offset: 0x00043D11
		private void CompleteWebClientState()
		{
			Interlocked.Decrement(ref this.m_CallNesting);
		}

		/// <summary>Gets or sets a value that indicates whether to buffer the data read from the Internet resource for a <see cref="T:System.Net.WebClient" /> instance.</summary>
		/// <returns>
		///   <see langword="true" /> to enable buffering of the data received from the Internet resource; <see langword="false" /> to disable buffering. The default is <see langword="true" />.</returns>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00045B1F File Offset: 0x00043D1F
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x00045B27 File Offset: 0x00043D27
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool AllowReadStreamBuffering { get; set; }

		/// <summary>Gets or sets a value that indicates whether to buffer the data written to the Internet resource for a <see cref="T:System.Net.WebClient" /> instance.</summary>
		/// <returns>
		///   <see langword="true" /> to enable buffering of the data written to the Internet resource; <see langword="false" /> to disable buffering. The default is <see langword="true" />.</returns>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00045B30 File Offset: 0x00043D30
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x00045B38 File Offset: 0x00043D38
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool AllowWriteStreamBuffering { get; set; }

		/// <summary>Occurs when an asynchronous operation to write data to a resource using a write stream is closed.</summary>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000D02 RID: 3330 RVA: 0x00045B41 File Offset: 0x00043D41
		// (remove) Token: 0x06000D03 RID: 3331 RVA: 0x00045B43 File Offset: 0x00043D43
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event WriteStreamClosedEventHandler WriteStreamClosed
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.WriteStreamClosed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.WriteStreamClosedEventArgs" /> object containing event data.</param>
		// Token: 0x06000D04 RID: 3332 RVA: 0x00045B45 File Offset: 0x00043D45
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnWriteStreamClosed(WriteStreamClosedEventArgs e)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.Encoding" /> used to upload and download strings.</summary>
		/// <returns>A <see cref="T:System.Text.Encoding" /> that is used to encode strings. The default value of this property is the encoding returned by <see cref="P:System.Text.Encoding.Default" />.</returns>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00045B47 File Offset: 0x00043D47
		// (set) Token: 0x06000D06 RID: 3334 RVA: 0x00045B4F File Offset: 0x00043D4F
		public Encoding Encoding
		{
			get
			{
				return this.m_Encoding;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Encoding");
				}
				this.m_Encoding = value;
			}
		}

		/// <summary>Gets or sets the base URI for requests made by a <see cref="T:System.Net.WebClient" />.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the base URI for requests made by a <see cref="T:System.Net.WebClient" /> or <see cref="F:System.String.Empty" /> if no base address has been specified.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.WebClient.BaseAddress" /> is set to an invalid URI. The inner exception may contain information that will help you locate the error.</exception>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00045B66 File Offset: 0x00043D66
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x00045B88 File Offset: 0x00043D88
		public string BaseAddress
		{
			get
			{
				if (!(this.m_baseAddress == null))
				{
					return this.m_baseAddress.ToString();
				}
				return string.Empty;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.m_baseAddress = null;
					return;
				}
				try
				{
					this.m_baseAddress = new Uri(value);
				}
				catch (UriFormatException ex)
				{
					throw new ArgumentException(SR.GetString("net_webclient_invalid_baseaddress"), "value", ex);
				}
			}
		}

		/// <summary>Gets or sets the network credentials that are sent to the host and used to authenticate the request.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> containing the authentication credentials for the request. The default is <see langword="null" />.</returns>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00045BE0 File Offset: 0x00043DE0
		// (set) Token: 0x06000D0A RID: 3338 RVA: 0x00045BE8 File Offset: 0x00043DE8
		public ICredentials Credentials
		{
			get
			{
				return this.m_credentials;
			}
			set
			{
				this.m_credentials = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>
		///   <see langword="true" /> if the default credentials are used; otherwise <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00045BF1 File Offset: 0x00043DF1
		// (set) Token: 0x06000D0C RID: 3340 RVA: 0x00045C03 File Offset: 0x00043E03
		public bool UseDefaultCredentials
		{
			get
			{
				return this.m_credentials is SystemNetworkCredential;
			}
			set
			{
				this.m_credentials = (value ? CredentialCache.DefaultCredentials : null);
			}
		}

		/// <summary>Gets or sets a collection of header name/value pairs associated with the request.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> containing header name/value pairs associated with this request.</returns>
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00045C16 File Offset: 0x00043E16
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x00045C32 File Offset: 0x00043E32
		public WebHeaderCollection Headers
		{
			get
			{
				if (this.m_headers == null)
				{
					this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.WebRequest);
				}
				return this.m_headers;
			}
			set
			{
				this.m_headers = value;
			}
		}

		/// <summary>Gets or sets a collection of query name/value pairs associated with the request.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains query name/value pairs associated with the request. If no pairs are associated with the request, the value is an empty <see cref="T:System.Collections.Specialized.NameValueCollection" />.</returns>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00045C3B File Offset: 0x00043E3B
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x00045C56 File Offset: 0x00043E56
		public NameValueCollection QueryString
		{
			get
			{
				if (this.m_requestParameters == null)
				{
					this.m_requestParameters = new NameValueCollection();
				}
				return this.m_requestParameters;
			}
			set
			{
				this.m_requestParameters = value;
			}
		}

		/// <summary>Gets a collection of header name/value pairs associated with the response.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> containing header name/value pairs associated with the response, or <see langword="null" /> if no response has been received.</returns>
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00045C5F File Offset: 0x00043E5F
		public WebHeaderCollection ResponseHeaders
		{
			get
			{
				if (this.m_WebResponse != null)
				{
					return this.m_WebResponse.Headers;
				}
				return null;
			}
		}

		/// <summary>Gets or sets the proxy used by this <see cref="T:System.Net.WebClient" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> instance used to send requests.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Net.WebClient.Proxy" /> is set to <see langword="null" />.</exception>
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00045C76 File Offset: 0x00043E76
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x00045C96 File Offset: 0x00043E96
		public IWebProxy Proxy
		{
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				if (!this.m_ProxySet)
				{
					return WebRequest.InternalDefaultWebProxy;
				}
				return this.m_Proxy;
			}
			set
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				this.m_Proxy = value;
				this.m_ProxySet = true;
			}
		}

		/// <summary>Gets or sets the application's cache policy for any resources obtained by this WebClient instance using <see cref="T:System.Net.WebRequest" /> objects.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCachePolicy" /> object that represents the application's caching requirements.</returns>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00045CB0 File Offset: 0x00043EB0
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x00045CB8 File Offset: 0x00043EB8
		public RequestCachePolicy CachePolicy
		{
			get
			{
				return this.m_CachePolicy;
			}
			set
			{
				this.m_CachePolicy = value;
			}
		}

		/// <summary>Gets whether a Web request is in progress.</summary>
		/// <returns>
		///   <see langword="true" /> if the Web request is still in progress; otherwise <see langword="false" />.</returns>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00045CC1 File Offset: 0x00043EC1
		public bool IsBusy
		{
			get
			{
				return this.m_AsyncOp != null;
			}
		}

		/// <summary>Returns a <see cref="T:System.Net.WebRequest" /> object for the specified resource.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> that identifies the resource to request.</param>
		/// <returns>A new <see cref="T:System.Net.WebRequest" /> object for the specified resource.</returns>
		// Token: 0x06000D17 RID: 3351 RVA: 0x00045CCC File Offset: 0x00043ECC
		protected virtual WebRequest GetWebRequest(Uri address)
		{
			WebRequest webRequest = WebRequest.Create(address);
			this.CopyHeadersTo(webRequest);
			if (this.Credentials != null)
			{
				webRequest.Credentials = this.Credentials;
			}
			if (this.m_Method != null)
			{
				webRequest.Method = this.m_Method;
			}
			if (this.m_ContentLength != -1L)
			{
				webRequest.ContentLength = this.m_ContentLength;
			}
			if (this.m_ProxySet)
			{
				webRequest.Proxy = this.m_Proxy;
			}
			if (this.m_CachePolicy != null)
			{
				webRequest.CachePolicy = this.m_CachePolicy;
			}
			return webRequest;
		}

		/// <summary>Returns the <see cref="T:System.Net.WebResponse" /> for the specified <see cref="T:System.Net.WebRequest" />.</summary>
		/// <param name="request">A <see cref="T:System.Net.WebRequest" /> that is used to obtain the response.</param>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> containing the response for the specified <see cref="T:System.Net.WebRequest" />.</returns>
		// Token: 0x06000D18 RID: 3352 RVA: 0x00045D50 File Offset: 0x00043F50
		protected virtual WebResponse GetWebResponse(WebRequest request)
		{
			WebResponse response = request.GetResponse();
			this.m_WebResponse = response;
			return response;
		}

		/// <summary>Returns the <see cref="T:System.Net.WebResponse" /> for the specified <see cref="T:System.Net.WebRequest" /> using the specified <see cref="T:System.IAsyncResult" />.</summary>
		/// <param name="request">A <see cref="T:System.Net.WebRequest" /> that is used to obtain the response.</param>
		/// <param name="result">An <see cref="T:System.IAsyncResult" /> object obtained from a previous call to <see cref="M:System.Net.WebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> .</param>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> containing the response for the specified <see cref="T:System.Net.WebRequest" />.</returns>
		// Token: 0x06000D19 RID: 3353 RVA: 0x00045D6C File Offset: 0x00043F6C
		protected virtual WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
		{
			WebResponse webResponse = request.EndGetResponse(result);
			this.m_WebResponse = webResponse;
			return webResponse;
		}

		/// <summary>Downloads the resource as a <see cref="T:System.Byte" /> array from the URI specified.</summary>
		/// <param name="address">The URI from which to download data.</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading data.</exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		// Token: 0x06000D1A RID: 3354 RVA: 0x00045D89 File Offset: 0x00043F89
		public byte[] DownloadData(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.DownloadData(this.GetUri(address));
		}

		/// <summary>Downloads the resource as a <see cref="T:System.Byte" /> array from the URI specified.</summary>
		/// <param name="address">The URI represented by the <see cref="T:System.Uri" /> object, from which to download data.</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000D1B RID: 3355 RVA: 0x00045DA8 File Offset: 0x00043FA8
		public byte[] DownloadData(Uri address)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadData", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.ClearWebClientState();
			byte[] array2;
			try
			{
				WebRequest webRequest;
				byte[] array = this.DownloadDataInternal(address, out webRequest);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "DownloadData", array);
				}
				array2 = array;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return array2;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00045E28 File Offset: 0x00044028
		private byte[] DownloadDataInternal(Uri address, out WebRequest request)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadData", address);
			}
			request = null;
			byte[] array2;
			try
			{
				request = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				byte[] array = this.DownloadBits(request, null, null, null);
				array2 = array;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(request);
				throw ex;
			}
			return array2;
		}

		/// <summary>Downloads the resource with the specified URI to a local file.</summary>
		/// <param name="address">The URI from which to download data.</param>
		/// <param name="fileName">The name of the local file that is to receive the data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="filename" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.  
		///  -or-  
		///  The file does not exist.  
		///  -or- An error occurred while downloading data.</exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		// Token: 0x06000D1D RID: 3357 RVA: 0x00045ED0 File Offset: 0x000440D0
		public void DownloadFile(string address, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.DownloadFile(this.GetUri(address), fileName);
		}

		/// <summary>Downloads the resource with the specified URI to a local file.</summary>
		/// <param name="address">The URI specified as a <see cref="T:System.String" />, from which to download data.</param>
		/// <param name="fileName">The name of the local file that is to receive the data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="filename" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.  
		///  -or-  
		///  The file does not exist.  
		///  -or-  
		///  An error occurred while downloading data.</exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		// Token: 0x06000D1E RID: 3358 RVA: 0x00045EF0 File Offset: 0x000440F0
		public void DownloadFile(Uri address, string fileName)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadFile", ((address != null) ? address.ToString() : null) + ", " + fileName);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			WebRequest webRequest = null;
			FileStream fileStream = null;
			bool flag = false;
			this.ClearWebClientState();
			try
			{
				fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
				webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.DownloadBits(webRequest, fileStream, null, null);
				flag = true;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(webRequest);
				throw ex;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
					if (!flag)
					{
						File.Delete(fileName);
					}
					fileStream = null;
				}
				this.CompleteWebClientState();
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "DownloadFile", "");
			}
		}

		/// <summary>Opens a readable stream for the data downloaded from a resource with the URI specified as a <see cref="T:System.String" />.</summary>
		/// <param name="address">The URI specified as a <see cref="T:System.String" /> from which to download data.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to read data from a resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading data.</exception>
		// Token: 0x06000D1F RID: 3359 RVA: 0x0004602C File Offset: 0x0004422C
		public Stream OpenRead(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenRead(this.GetUri(address));
		}

		/// <summary>Opens a readable stream for the data downloaded from a resource with the URI specified as a <see cref="T:System.Uri" /></summary>
		/// <param name="address">The URI specified as a <see cref="T:System.Uri" /> from which to download data.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to read data from a resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading data.</exception>
		// Token: 0x06000D20 RID: 3360 RVA: 0x0004604C File Offset: 0x0004424C
		public Stream OpenRead(Uri address)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "OpenRead", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			WebRequest webRequest = null;
			this.ClearWebClientState();
			Stream stream;
			try
			{
				webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				WebResponse webResponse = (this.m_WebResponse = this.GetWebResponse(webRequest));
				Stream responseStream = webResponse.GetResponseStream();
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "OpenRead", responseStream);
				}
				stream = responseStream;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(webRequest);
				throw ex;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return stream;
		}

		/// <summary>Opens a stream for writing data to the specified resource.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.</exception>
		// Token: 0x06000D21 RID: 3361 RVA: 0x00046150 File Offset: 0x00044350
		public Stream OpenWrite(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenWrite(this.GetUri(address), null);
		}

		/// <summary>Opens a stream for writing data to the specified resource.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.</exception>
		// Token: 0x06000D22 RID: 3362 RVA: 0x0004616E File Offset: 0x0004436E
		public Stream OpenWrite(Uri address)
		{
			return this.OpenWrite(address, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource, using the specified method.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.</exception>
		// Token: 0x06000D23 RID: 3363 RVA: 0x00046178 File Offset: 0x00044378
		public Stream OpenWrite(string address, string method)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenWrite(this.GetUri(address), method);
		}

		/// <summary>Opens a stream for writing data to the specified resource, by using the specified method.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.</exception>
		// Token: 0x06000D24 RID: 3364 RVA: 0x00046198 File Offset: 0x00044398
		public Stream OpenWrite(Uri address, string method)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "OpenWrite", ((address != null) ? address.ToString() : null) + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			WebRequest webRequest = null;
			this.ClearWebClientState();
			Stream stream;
			try
			{
				this.m_Method = method;
				webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				WebClient.WebClientWriteStream webClientWriteStream = new WebClient.WebClientWriteStream(webRequest.GetRequestStream(), webRequest, this);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "OpenWrite", webClientWriteStream);
				}
				stream = webClientWriteStream;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(webRequest);
				throw ex;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return stream;
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="data" /> is <see langword="null" />.  
		///  -or-  
		///  An error occurred while sending the data.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000D25 RID: 3365 RVA: 0x000462B8 File Offset: 0x000444B8
		public byte[] UploadData(string address, byte[] data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadData(this.GetUri(address), null, data);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="data" /> is <see langword="null" />.  
		///  -or-  
		///  An error occurred while sending the data.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000D26 RID: 3366 RVA: 0x000462D7 File Offset: 0x000444D7
		public byte[] UploadData(Uri address, byte[] data)
		{
			return this.UploadData(address, null, data);
		}

		/// <summary>Uploads a data buffer to the specified resource, using the specified method.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The HTTP method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="data" /> is <see langword="null" />.  
		///  -or-  
		///  An error occurred while uploading the data.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000D27 RID: 3367 RVA: 0x000462E2 File Offset: 0x000444E2
		public byte[] UploadData(string address, string method, byte[] data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadData(this.GetUri(address), method, data);
		}

		/// <summary>Uploads a data buffer to the specified resource, using the specified method.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The HTTP method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="data" /> is <see langword="null" />.  
		///  -or-  
		///  An error occurred while uploading the data.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000D28 RID: 3368 RVA: 0x00046304 File Offset: 0x00044504
		public byte[] UploadData(Uri address, string method, byte[] data)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadData", ((address != null) ? address.ToString() : null) + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.ClearWebClientState();
			byte[] array2;
			try
			{
				WebRequest webRequest;
				byte[] array = this.UploadDataInternal(address, method, data, out webRequest);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "UploadData", array);
				}
				array2 = array;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return array2;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x000463B4 File Offset: 0x000445B4
		private byte[] UploadDataInternal(Uri address, string method, byte[] data, out WebRequest request)
		{
			request = null;
			byte[] array2;
			try
			{
				this.m_Method = method;
				this.m_ContentLength = (long)data.Length;
				request = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.UploadBits(request, null, data, 0, null, null, null, null, null);
				byte[] array = this.DownloadBits(request, null, null, null);
				array2 = array;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(request);
				throw ex;
			}
			return array2;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00046468 File Offset: 0x00044668
		private void OpenFileInternal(bool needsHeaderAndBoundary, string fileName, ref FileStream fs, ref byte[] buffer, ref byte[] formHeaderBytes, ref byte[] boundaryBytes)
		{
			fileName = Path.GetFullPath(fileName);
			if (this.m_headers == null)
			{
				this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.WebRequest);
			}
			string text = this.m_headers["Content-Type"];
			if (text != null)
			{
				if (text.ToLower(CultureInfo.InvariantCulture).StartsWith("multipart/"))
				{
					throw new WebException(SR.GetString("net_webclient_Multipart"));
				}
			}
			else
			{
				text = "application/octet-stream";
			}
			fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			int num = 8192;
			this.m_ContentLength = -1L;
			if (this.m_Method.ToUpper(CultureInfo.InvariantCulture) == "POST")
			{
				if (needsHeaderAndBoundary)
				{
					string text2 = "---------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
					this.m_headers["Content-Type"] = "multipart/form-data; boundary=" + text2;
					string text3 = string.Concat(new string[]
					{
						"--",
						text2,
						"\r\nContent-Disposition: form-data; name=\"file\"; filename=\"",
						Path.GetFileName(fileName),
						"\"\r\nContent-Type: ",
						text,
						"\r\n\r\n"
					});
					formHeaderBytes = Encoding.UTF8.GetBytes(text3);
					boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + text2 + "--\r\n");
				}
				else
				{
					formHeaderBytes = new byte[0];
					boundaryBytes = new byte[0];
				}
				if (fs.CanSeek)
				{
					this.m_ContentLength = fs.Length + (long)formHeaderBytes.Length + (long)boundaryBytes.Length;
					num = (int)Math.Min(8192L, fs.Length);
				}
			}
			else
			{
				this.m_headers["Content-Type"] = text;
				formHeaderBytes = null;
				boundaryBytes = null;
				if (fs.CanSeek)
				{
					this.m_ContentLength = fs.Length;
					num = (int)Math.Min(8192L, fs.Length);
				}
			}
			buffer = new byte[num];
		}

		/// <summary>Uploads the specified local file to a resource with the specified URI.</summary>
		/// <param name="address">The URI of the resource to receive the file. For example, ftp://localhost/samplefile.txt.</param>
		/// <param name="fileName">The file to send to the resource. For example, "samplefile.txt".</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="fileName" /> is <see langword="null" />, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.  
		///  -or-  
		///  An error occurred while uploading the file.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header begins with <see langword="multipart" />.</exception>
		// Token: 0x06000D2B RID: 3371 RVA: 0x00046658 File Offset: 0x00044858
		public byte[] UploadFile(string address, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadFile(this.GetUri(address), fileName);
		}

		/// <summary>Uploads the specified local file to a resource with the specified URI.</summary>
		/// <param name="address">The URI of the resource to receive the file. For example, ftp://localhost/samplefile.txt.</param>
		/// <param name="fileName">The file to send to the resource. For example, "samplefile.txt".</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="fileName" /> is <see langword="null" />, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.  
		///  -or-  
		///  An error occurred while uploading the file.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header begins with <see langword="multipart" />.</exception>
		// Token: 0x06000D2C RID: 3372 RVA: 0x00046676 File Offset: 0x00044876
		public byte[] UploadFile(Uri address, string fileName)
		{
			return this.UploadFile(address, null, fileName);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the specified method.</summary>
		/// <param name="address">The URI of the resource to receive the file.</param>
		/// <param name="method">The method used to send the file to the resource. If <see langword="null" />, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource.</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="fileName" /> is <see langword="null" />, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.  
		///  -or-  
		///  An error occurred while uploading the file.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header begins with <see langword="multipart" />.</exception>
		// Token: 0x06000D2D RID: 3373 RVA: 0x00046681 File Offset: 0x00044881
		public byte[] UploadFile(string address, string method, string fileName)
		{
			return this.UploadFile(this.GetUri(address), method, fileName);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the specified method.</summary>
		/// <param name="address">The URI of the resource to receive the file.</param>
		/// <param name="method">The method used to send the file to the resource. If <see langword="null" />, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource.</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="fileName" /> is <see langword="null" />, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.  
		///  -or-  
		///  An error occurred while uploading the file.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header begins with <see langword="multipart" />.</exception>
		// Token: 0x06000D2E RID: 3374 RVA: 0x00046694 File Offset: 0x00044894
		public byte[] UploadFile(Uri address, string method, string fileName)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadFile", ((address != null) ? address.ToString() : null) + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			FileStream fileStream = null;
			WebRequest webRequest = null;
			this.ClearWebClientState();
			byte[] array5;
			try
			{
				this.m_Method = method;
				byte[] array = null;
				byte[] array2 = null;
				byte[] array3 = null;
				Uri uri = this.GetUri(address);
				bool flag = uri.Scheme != Uri.UriSchemeFile;
				this.OpenFileInternal(flag, fileName, ref fileStream, ref array3, ref array, ref array2);
				webRequest = (this.m_WebRequest = this.GetWebRequest(uri));
				this.UploadBits(webRequest, fileStream, array3, 0, array, array2, null, null, null);
				byte[] array4 = this.DownloadBits(webRequest, null, null, null);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "UploadFile", array4);
				}
				array5 = array4;
			}
			catch (Exception ex)
			{
				if (fileStream != null)
				{
					fileStream.Close();
					fileStream = null;
				}
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(webRequest);
				throw ex;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return array5;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00046810 File Offset: 0x00044A10
		private byte[] UploadValuesInternal(NameValueCollection data)
		{
			if (this.m_headers == null)
			{
				this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.WebRequest);
			}
			string text = this.m_headers["Content-Type"];
			if (text != null && string.Compare(text, "application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new WebException(SR.GetString("net_webclient_ContentType"));
			}
			this.m_headers["Content-Type"] = "application/x-www-form-urlencoded";
			string text2 = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text3 in data.AllKeys)
			{
				stringBuilder.Append(text2);
				stringBuilder.Append(WebClient.UrlEncode(text3));
				stringBuilder.Append("=");
				stringBuilder.Append(WebClient.UrlEncode(data[text3]));
				text2 = "&";
			}
			byte[] bytes = Encoding.ASCII.GetBytes(stringBuilder.ToString());
			this.m_ContentLength = (long)bytes.Length;
			return bytes;
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI.</summary>
		/// <param name="address">The URI of the resource to receive the collection.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="data" /> is <see langword="null" />.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  The <see langword="Content-type" /> header is not <see langword="null" /> or "application/x-www-form-urlencoded".</exception>
		// Token: 0x06000D30 RID: 3376 RVA: 0x000468FE File Offset: 0x00044AFE
		public byte[] UploadValues(string address, NameValueCollection data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadValues(this.GetUri(address), null, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI.</summary>
		/// <param name="address">The URI of the resource to receive the collection.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="data" /> is <see langword="null" />.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  The <see langword="Content-type" /> header is not <see langword="null" /> or "application/x-www-form-urlencoded".</exception>
		// Token: 0x06000D31 RID: 3377 RVA: 0x0004691D File Offset: 0x00044B1D
		public byte[] UploadValues(Uri address, NameValueCollection data)
		{
			return this.UploadValues(address, null, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI, using the specified method.</summary>
		/// <param name="address">The URI of the resource to receive the collection.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="data" /> is <see langword="null" />.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header value is not <see langword="null" /> and is not <see langword="application/x-www-form-urlencoded" />.</exception>
		// Token: 0x06000D32 RID: 3378 RVA: 0x00046928 File Offset: 0x00044B28
		public byte[] UploadValues(string address, string method, NameValueCollection data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadValues(this.GetUri(address), method, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI, using the specified method.</summary>
		/// <param name="address">The URI of the resource to receive the collection.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="data" /> is <see langword="null" />.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header value is not <see langword="null" /> and is not <see langword="application/x-www-form-urlencoded" />.</exception>
		// Token: 0x06000D33 RID: 3379 RVA: 0x00046948 File Offset: 0x00044B48
		public byte[] UploadValues(Uri address, string method, NameValueCollection data)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadValues", ((address != null) ? address.ToString() : null) + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			WebRequest webRequest = null;
			this.ClearWebClientState();
			byte[] array3;
			try
			{
				byte[] array = this.UploadValuesInternal(data);
				this.m_Method = method;
				webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.UploadBits(webRequest, null, array, 0, null, null, null, null, null);
				byte[] array2 = this.DownloadBits(webRequest, null, null, null);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "UploadValues", ((address != null) ? address.ToString() : null) + ", " + method);
				}
				array3 = array2;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(webRequest);
				throw ex;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return array3;
		}

		/// <summary>Uploads the specified string to the specified resource, using the POST method.</summary>
		/// <param name="address">The URI of the resource to receive the string. For Http resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000D34 RID: 3380 RVA: 0x00046AA0 File Offset: 0x00044CA0
		public string UploadString(string address, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadString(this.GetUri(address), null, data);
		}

		/// <summary>Uploads the specified string to the specified resource, using the POST method.</summary>
		/// <param name="address">The URI of the resource to receive the string. For Http resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000D35 RID: 3381 RVA: 0x00046ABF File Offset: 0x00044CBF
		public string UploadString(Uri address, string data)
		{
			return this.UploadString(address, null, data);
		}

		/// <summary>Uploads the specified string to the specified resource, using the specified method.</summary>
		/// <param name="address">The URI of the resource to receive the string. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method.</param>
		/// <param name="method">The HTTP method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  <paramref name="method" /> cannot be used to send content.</exception>
		// Token: 0x06000D36 RID: 3382 RVA: 0x00046ACA File Offset: 0x00044CCA
		public string UploadString(string address, string method, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadString(this.GetUri(address), method, data);
		}

		/// <summary>Uploads the specified string to the specified resource, using the specified method.</summary>
		/// <param name="address">The URI of the resource to receive the string. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method.</param>
		/// <param name="method">The HTTP method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  <paramref name="method" /> cannot be used to send content.</exception>
		// Token: 0x06000D37 RID: 3383 RVA: 0x00046AEC File Offset: 0x00044CEC
		public string UploadString(Uri address, string method, string data)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadString", ((address != null) ? address.ToString() : null) + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.ClearWebClientState();
			string text;
			try
			{
				byte[] bytes = this.Encoding.GetBytes(data);
				WebRequest webRequest;
				byte[] array = this.UploadDataInternal(address, method, bytes, out webRequest);
				string stringUsingEncoding = this.GetStringUsingEncoding(webRequest, array);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "UploadString", stringUsingEncoding);
				}
				text = stringUsingEncoding;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return text;
		}

		/// <summary>Downloads the requested resource as a <see cref="T:System.String" />. The resource to download is specified as a <see cref="T:System.String" /> containing the URI.</summary>
		/// <param name="address">A <see cref="T:System.String" /> containing the URI to download.</param>
		/// <returns>A <see cref="T:System.String" /> containing the requested resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		// Token: 0x06000D38 RID: 3384 RVA: 0x00046BB4 File Offset: 0x00044DB4
		public string DownloadString(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.DownloadString(this.GetUri(address));
		}

		/// <summary>Downloads the requested resource as a <see cref="T:System.String" />. The resource to download is specified as a <see cref="T:System.Uri" />.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> object containing the URI to download.</param>
		/// <returns>A <see cref="T:System.String" /> containing the requested resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		// Token: 0x06000D39 RID: 3385 RVA: 0x00046BD4 File Offset: 0x00044DD4
		public string DownloadString(Uri address)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadString", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.ClearWebClientState();
			string text;
			try
			{
				WebRequest webRequest;
				byte[] array = this.DownloadDataInternal(address, out webRequest);
				string stringUsingEncoding = this.GetStringUsingEncoding(webRequest, array);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "DownloadString", stringUsingEncoding);
				}
				text = stringUsingEncoding;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return text;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00046C5C File Offset: 0x00044E5C
		private static void AbortRequest(WebRequest request)
		{
			try
			{
				if (request != null)
				{
					request.Abort();
				}
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException)
				{
					throw;
				}
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00046CA4 File Offset: 0x00044EA4
		private void CopyHeadersTo(WebRequest request)
		{
			if (this.m_headers != null && request is HttpWebRequest)
			{
				string text = this.m_headers["Accept"];
				string text2 = this.m_headers["Connection"];
				string text3 = this.m_headers["Content-Type"];
				string text4 = this.m_headers["Expect"];
				string text5 = this.m_headers["Referer"];
				string text6 = this.m_headers["User-Agent"];
				string text7 = this.m_headers["Host"];
				this.m_headers.RemoveInternal("Accept");
				this.m_headers.RemoveInternal("Connection");
				this.m_headers.RemoveInternal("Content-Type");
				this.m_headers.RemoveInternal("Expect");
				this.m_headers.RemoveInternal("Referer");
				this.m_headers.RemoveInternal("User-Agent");
				this.m_headers.RemoveInternal("Host");
				request.Headers = this.m_headers;
				if (text != null && text.Length > 0)
				{
					((HttpWebRequest)request).Accept = text;
				}
				if (text2 != null && text2.Length > 0)
				{
					((HttpWebRequest)request).Connection = text2;
				}
				if (text3 != null && text3.Length > 0)
				{
					((HttpWebRequest)request).ContentType = text3;
				}
				if (text4 != null && text4.Length > 0)
				{
					((HttpWebRequest)request).Expect = text4;
				}
				if (text5 != null && text5.Length > 0)
				{
					((HttpWebRequest)request).Referer = text5;
				}
				if (text6 != null && text6.Length > 0)
				{
					((HttpWebRequest)request).UserAgent = text6;
				}
				if (!string.IsNullOrEmpty(text7))
				{
					((HttpWebRequest)request).Host = text7;
				}
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00046E6C File Offset: 0x0004506C
		private Uri GetUri(string path)
		{
			Uri uri;
			if (this.m_baseAddress != null)
			{
				if (!Uri.TryCreate(this.m_baseAddress, path, out uri))
				{
					return new Uri(Path.GetFullPath(path));
				}
			}
			else if (!Uri.TryCreate(path, UriKind.Absolute, out uri))
			{
				return new Uri(Path.GetFullPath(path));
			}
			return this.GetUri(uri);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00046EC4 File Offset: 0x000450C4
		private Uri GetUri(Uri address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			Uri uri = address;
			if (!address.IsAbsoluteUri && this.m_baseAddress != null && !Uri.TryCreate(this.m_baseAddress, address, out uri))
			{
				return address;
			}
			if ((uri.Query == null || uri.Query == string.Empty) && this.m_requestParameters != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = string.Empty;
				for (int i = 0; i < this.m_requestParameters.Count; i++)
				{
					stringBuilder.Append(text + this.m_requestParameters.AllKeys[i] + "=" + this.m_requestParameters[i]);
					text = "&";
				}
				uri = new UriBuilder(uri)
				{
					Query = stringBuilder.ToString()
				}.Uri;
			}
			return uri;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00046FA8 File Offset: 0x000451A8
		private static void DownloadBitsResponseCallback(IAsyncResult result)
		{
			WebClient.DownloadBitsState downloadBitsState = (WebClient.DownloadBitsState)result.AsyncState;
			WebRequest request = downloadBitsState.Request;
			Exception ex = null;
			try
			{
				WebResponse webResponse = downloadBitsState.WebClient.GetWebResponse(request, result);
				downloadBitsState.WebClient.m_WebResponse = webResponse;
				downloadBitsState.SetResponse(webResponse);
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex2);
				}
				WebClient.AbortRequest(request);
				if (downloadBitsState != null && downloadBitsState.WriteStream != null)
				{
					downloadBitsState.WriteStream.Close();
				}
			}
			finally
			{
				if (ex != null)
				{
					downloadBitsState.CompletionDelegate(null, ex, downloadBitsState.AsyncOp);
				}
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00047088 File Offset: 0x00045288
		private static void DownloadBitsReadCallback(IAsyncResult result)
		{
			WebClient.DownloadBitsState downloadBitsState = (WebClient.DownloadBitsState)result.AsyncState;
			WebClient.DownloadBitsReadCallbackState(downloadBitsState, result);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x000470A8 File Offset: 0x000452A8
		private static void DownloadBitsReadCallbackState(WebClient.DownloadBitsState state, IAsyncResult result)
		{
			Stream readStream = state.ReadStream;
			Exception ex = null;
			bool flag = false;
			try
			{
				int num = 0;
				if (readStream != null && readStream != Stream.Null)
				{
					num = readStream.EndRead(result);
				}
				flag = state.RetrieveBytes(ref num);
			}
			catch (Exception ex2)
			{
				flag = true;
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				state.InnerBuffer = null;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex2);
				}
				WebClient.AbortRequest(state.Request);
				if (state != null && state.WriteStream != null)
				{
					state.WriteStream.Close();
				}
			}
			finally
			{
				if (flag)
				{
					if (ex == null)
					{
						state.Close();
					}
					state.CompletionDelegate(state.InnerBuffer, ex, state.AsyncOp);
				}
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00047198 File Offset: 0x00045398
		private byte[] DownloadBits(WebRequest request, Stream writeStream, CompletionDelegate completionDelegate, AsyncOperation asyncOp)
		{
			WebClient.DownloadBitsState downloadBitsState = new WebClient.DownloadBitsState(request, writeStream, completionDelegate, asyncOp, this.m_Progress, this);
			if (downloadBitsState.Async)
			{
				request.BeginGetResponse(new AsyncCallback(WebClient.DownloadBitsResponseCallback), downloadBitsState);
				return null;
			}
			WebResponse webResponse = (this.m_WebResponse = this.GetWebResponse(request));
			int num = downloadBitsState.SetResponse(webResponse);
			bool flag;
			do
			{
				flag = downloadBitsState.RetrieveBytes(ref num);
			}
			while (!flag);
			downloadBitsState.Close();
			return downloadBitsState.InnerBuffer;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0004720C File Offset: 0x0004540C
		private static void UploadBitsRequestCallback(IAsyncResult result)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)result.AsyncState;
			WebRequest request = uploadBitsState.Request;
			Exception ex = null;
			try
			{
				Stream stream = request.EndGetRequestStream(result);
				uploadBitsState.SetRequestStream(stream);
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex2);
				}
				WebClient.AbortRequest(request);
				if (uploadBitsState != null && uploadBitsState.ReadStream != null)
				{
					uploadBitsState.ReadStream.Close();
				}
			}
			finally
			{
				if (ex != null)
				{
					uploadBitsState.UploadCompletionDelegate(null, ex, uploadBitsState);
				}
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x000472D4 File Offset: 0x000454D4
		private static void UploadBitsWriteCallback(IAsyncResult result)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)result.AsyncState;
			Stream writeStream = uploadBitsState.WriteStream;
			Exception ex = null;
			bool flag = false;
			try
			{
				writeStream.EndWrite(result);
				flag = uploadBitsState.WriteBytes();
			}
			catch (Exception ex2)
			{
				flag = true;
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex2);
				}
				WebClient.AbortRequest(uploadBitsState.Request);
				if (uploadBitsState != null && uploadBitsState.ReadStream != null)
				{
					uploadBitsState.ReadStream.Close();
				}
			}
			finally
			{
				if (flag)
				{
					if (ex == null)
					{
						uploadBitsState.Close();
					}
					uploadBitsState.UploadCompletionDelegate(null, ex, uploadBitsState);
				}
			}
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x000473B0 File Offset: 0x000455B0
		private void UploadBits(WebRequest request, Stream readStream, byte[] buffer, int chunkSize, byte[] header, byte[] footer, CompletionDelegate uploadCompletionDelegate, CompletionDelegate downloadCompletionDelegate, AsyncOperation asyncOp)
		{
			if (request.RequestUri.Scheme == Uri.UriSchemeFile)
			{
				footer = (header = null);
			}
			WebClient.UploadBitsState uploadBitsState = new WebClient.UploadBitsState(request, readStream, buffer, chunkSize, header, footer, uploadCompletionDelegate, downloadCompletionDelegate, asyncOp, this.m_Progress, this);
			if (uploadBitsState.Async)
			{
				request.BeginGetRequestStream(new AsyncCallback(WebClient.UploadBitsRequestCallback), uploadBitsState);
				return;
			}
			Stream requestStream = request.GetRequestStream();
			uploadBitsState.SetRequestStream(requestStream);
			while (!uploadBitsState.WriteBytes())
			{
			}
			uploadBitsState.Close();
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00047430 File Offset: 0x00045630
		private bool ByteArrayHasPrefix(byte[] prefix, byte[] byteArray)
		{
			if (prefix == null || byteArray == null || prefix.Length > byteArray.Length)
			{
				return false;
			}
			for (int i = 0; i < prefix.Length; i++)
			{
				if (prefix[i] != byteArray[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00047468 File Offset: 0x00045668
		private string GetStringUsingEncoding(WebRequest request, byte[] data)
		{
			Encoding encoding = null;
			int num = -1;
			string text;
			try
			{
				text = request.ContentType;
			}
			catch (NotImplementedException)
			{
				text = null;
			}
			catch (NotSupportedException)
			{
				text = null;
			}
			if (text != null)
			{
				text = text.ToLower(CultureInfo.InvariantCulture);
				string[] array = text.Split(new char[] { ';', '=', ' ' });
				bool flag = false;
				foreach (string text2 in array)
				{
					if (text2 == "charset")
					{
						flag = true;
					}
					else if (flag)
					{
						try
						{
							encoding = Encoding.GetEncoding(text2);
						}
						catch (ArgumentException)
						{
							break;
						}
					}
				}
			}
			if (encoding == null)
			{
				Encoding[] array3 = new Encoding[]
				{
					Encoding.UTF8,
					Encoding.UTF32,
					Encoding.Unicode,
					Encoding.BigEndianUnicode
				};
				for (int j = 0; j < array3.Length; j++)
				{
					byte[] preamble = array3[j].GetPreamble();
					if (this.ByteArrayHasPrefix(preamble, data))
					{
						encoding = array3[j];
						num = preamble.Length;
						break;
					}
				}
			}
			if (encoding == null)
			{
				encoding = this.Encoding;
			}
			if (num == -1)
			{
				byte[] preamble2 = encoding.GetPreamble();
				if (this.ByteArrayHasPrefix(preamble2, data))
				{
					num = preamble2.Length;
				}
				else
				{
					num = 0;
				}
			}
			return encoding.GetString(data, num, data.Length - num);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x000475B8 File Offset: 0x000457B8
		private string MapToDefaultMethod(Uri address)
		{
			Uri uri;
			if (!address.IsAbsoluteUri && this.m_baseAddress != null)
			{
				uri = new Uri(this.m_baseAddress, address);
			}
			else
			{
				uri = address;
			}
			if (uri.Scheme.ToLower(CultureInfo.InvariantCulture) == "ftp")
			{
				return "STOR";
			}
			return "POST";
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00047613 File Offset: 0x00045813
		private static string UrlEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return WebClient.UrlEncode(str, Encoding.UTF8);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00047625 File Offset: 0x00045825
		private static string UrlEncode(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(WebClient.UrlEncodeToBytes(str, e));
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00047640 File Offset: 0x00045840
		private static byte[] UrlEncodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			byte[] bytes = e.GetBytes(str);
			return WebClient.UrlEncodeBytesToBytesInternal(bytes, 0, bytes.Length, false);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00047668 File Offset: 0x00045868
		private static byte[] UrlEncodeBytesToBytesInternal(byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				char c = (char)bytes[offset + i];
				if (c == ' ')
				{
					num++;
				}
				else if (!WebClient.IsSafe(c))
				{
					num2++;
				}
			}
			if (!alwaysCreateReturnValue && num == 0 && num2 == 0)
			{
				return bytes;
			}
			byte[] array = new byte[count + num2 * 2];
			int num3 = 0;
			for (int j = 0; j < count; j++)
			{
				byte b = bytes[offset + j];
				char c2 = (char)b;
				if (WebClient.IsSafe(c2))
				{
					array[num3++] = b;
				}
				else if (c2 == ' ')
				{
					array[num3++] = 43;
				}
				else
				{
					array[num3++] = 37;
					array[num3++] = (byte)WebClient.IntToHex((b >> 4) & 15);
					array[num3++] = (byte)WebClient.IntToHex((int)(b & 15));
				}
			}
			return array;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00047733 File Offset: 0x00045933
		private static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00047748 File Offset: 0x00045948
		private static bool IsSafe(char ch)
		{
			if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
			{
				return true;
			}
			if (ch != '!')
			{
				switch (ch)
				{
				case '\'':
				case '(':
				case ')':
				case '*':
				case '-':
				case '.':
					return true;
				case '+':
				case ',':
					break;
				default:
					if (ch == '_')
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000477AB File Offset: 0x000459AB
		private void InvokeOperationCompleted(AsyncOperation asyncOp, SendOrPostCallback callback, AsyncCompletedEventArgs eventArgs)
		{
			if (Interlocked.CompareExchange<AsyncOperation>(ref this.m_AsyncOp, null, asyncOp) == asyncOp)
			{
				this.CompleteWebClientState();
				asyncOp.PostOperationCompleted(callback, eventArgs);
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000477CB File Offset: 0x000459CB
		private bool AnotherCallInProgress(int callNesting)
		{
			return callNesting > 1;
		}

		/// <summary>Occurs when an asynchronous operation to open a stream containing a resource completes.</summary>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000D50 RID: 3408 RVA: 0x000477D4 File Offset: 0x000459D4
		// (remove) Token: 0x06000D51 RID: 3409 RVA: 0x0004780C File Offset: 0x00045A0C
		public event OpenReadCompletedEventHandler OpenReadCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.OpenReadCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.OpenReadCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06000D52 RID: 3410 RVA: 0x00047841 File Offset: 0x00045A41
		protected virtual void OnOpenReadCompleted(OpenReadCompletedEventArgs e)
		{
			if (this.OpenReadCompleted != null)
			{
				this.OpenReadCompleted(this, e);
			}
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00047858 File Offset: 0x00045A58
		private void OpenReadOperationCompleted(object arg)
		{
			this.OnOpenReadCompleted((OpenReadCompletedEventArgs)arg);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00047868 File Offset: 0x00045A68
		private void OpenReadAsyncCallback(IAsyncResult result)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)result;
			AsyncOperation asyncOperation = (AsyncOperation)lazyAsyncResult.AsyncState;
			WebRequest webRequest = (WebRequest)lazyAsyncResult.AsyncObject;
			Stream stream = null;
			Exception ex = null;
			try
			{
				WebResponse webResponse = (this.m_WebResponse = this.GetWebResponse(webRequest, result));
				stream = webResponse.GetResponseStream();
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex2);
				}
			}
			OpenReadCompletedEventArgs openReadCompletedEventArgs = new OpenReadCompletedEventArgs(stream, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.openReadOperationCompleted, openReadCompletedEventArgs);
		}

		/// <summary>Opens a readable stream containing the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and address is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.  
		///  -or-  
		///  An error occurred while opening the stream.</exception>
		// Token: 0x06000D55 RID: 3413 RVA: 0x00047938 File Offset: 0x00045B38
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenReadAsync(Uri address)
		{
			this.OpenReadAsync(address, null);
		}

		/// <summary>Opens a readable stream containing the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to retrieve.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and address is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.  
		///  -or-  
		///  An error occurred while opening the stream.</exception>
		// Token: 0x06000D56 RID: 3414 RVA: 0x00047944 File Offset: 0x00045B44
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenReadAsync(Uri address, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "OpenReadAsync", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				webRequest.BeginGetResponse(new AsyncCallback(this.OpenReadAsyncCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				OpenReadCompletedEventArgs openReadCompletedEventArgs = new OpenReadCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.openReadOperationCompleted, openReadCompletedEventArgs);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "OpenReadAsync", null);
			}
		}

		/// <summary>Occurs when an asynchronous operation to open a stream to write data to a resource completes.</summary>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000D57 RID: 3415 RVA: 0x00047A50 File Offset: 0x00045C50
		// (remove) Token: 0x06000D58 RID: 3416 RVA: 0x00047A88 File Offset: 0x00045C88
		public event OpenWriteCompletedEventHandler OpenWriteCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.OpenWriteCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.OpenWriteCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06000D59 RID: 3417 RVA: 0x00047ABD File Offset: 0x00045CBD
		protected virtual void OnOpenWriteCompleted(OpenWriteCompletedEventArgs e)
		{
			if (this.OpenWriteCompleted != null)
			{
				this.OpenWriteCompleted(this, e);
			}
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00047AD4 File Offset: 0x00045CD4
		private void OpenWriteOperationCompleted(object arg)
		{
			this.OnOpenWriteCompleted((OpenWriteCompletedEventArgs)arg);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00047AE4 File Offset: 0x00045CE4
		private void OpenWriteAsyncCallback(IAsyncResult result)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)result;
			AsyncOperation asyncOperation = (AsyncOperation)lazyAsyncResult.AsyncState;
			WebRequest webRequest = (WebRequest)lazyAsyncResult.AsyncObject;
			WebClient.WebClientWriteStream webClientWriteStream = null;
			Exception ex = null;
			try
			{
				webClientWriteStream = new WebClient.WebClientWriteStream(webRequest.EndGetRequestStream(result), webRequest, this);
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex2);
				}
			}
			OpenWriteCompletedEventArgs openWriteCompletedEventArgs = new OpenWriteCompletedEventArgs(webClientWriteStream, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.openWriteOperationCompleted, openWriteCompletedEventArgs);
		}

		/// <summary>Opens a stream for writing data to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000D5C RID: 3420 RVA: 0x00047BA8 File Offset: 0x00045DA8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenWriteAsync(Uri address)
		{
			this.OpenWriteAsync(address, null, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000D5D RID: 3421 RVA: 0x00047BB3 File Offset: 0x00045DB3
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenWriteAsync(Uri address, string method)
		{
			this.OpenWriteAsync(address, method, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource, using the specified method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.</exception>
		// Token: 0x06000D5E RID: 3422 RVA: 0x00047BC0 File Offset: 0x00045DC0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenWriteAsync(Uri address, string method, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "OpenWriteAsync", ((address != null) ? address.ToString() : null) + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				this.m_Method = method;
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				webRequest.BeginGetRequestStream(new AsyncCallback(this.OpenWriteAsyncCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				OpenWriteCompletedEventArgs openWriteCompletedEventArgs = new OpenWriteCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.openWriteOperationCompleted, openWriteCompletedEventArgs);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "OpenWriteAsync", null);
			}
		}

		/// <summary>Occurs when an asynchronous resource-download operation completes.</summary>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000D5F RID: 3423 RVA: 0x00047CF4 File Offset: 0x00045EF4
		// (remove) Token: 0x06000D60 RID: 3424 RVA: 0x00047D2C File Offset: 0x00045F2C
		public event DownloadStringCompletedEventHandler DownloadStringCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadStringCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.DownloadStringCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06000D61 RID: 3425 RVA: 0x00047D61 File Offset: 0x00045F61
		protected virtual void OnDownloadStringCompleted(DownloadStringCompletedEventArgs e)
		{
			if (this.DownloadStringCompleted != null)
			{
				this.DownloadStringCompleted(this, e);
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00047D78 File Offset: 0x00045F78
		private void DownloadStringOperationCompleted(object arg)
		{
			this.OnDownloadStringCompleted((DownloadStringCompletedEventArgs)arg);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00047D88 File Offset: 0x00045F88
		private void DownloadStringAsyncCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			string text = null;
			try
			{
				if (returnBytes != null)
				{
					text = this.GetStringUsingEncoding(this.m_WebRequest, returnBytes);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				exception = ex;
			}
			DownloadStringCompletedEventArgs downloadStringCompletedEventArgs = new DownloadStringCompletedEventArgs(text, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.downloadStringOperationCompleted, downloadStringCompletedEventArgs);
		}

		/// <summary>Downloads the resource specified as a <see cref="T:System.Uri" />. This method does not block the calling thread.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		// Token: 0x06000D64 RID: 3428 RVA: 0x00047E04 File Offset: 0x00046004
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadStringAsync(Uri address)
		{
			this.DownloadStringAsync(address, null);
		}

		/// <summary>Downloads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		// Token: 0x06000D65 RID: 3429 RVA: 0x00047E10 File Offset: 0x00046010
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadStringAsync(Uri address, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadStringAsync", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.DownloadBits(webRequest, null, new CompletionDelegate(this.DownloadStringAsyncCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				this.DownloadStringAsyncCallback(null, ex, asyncOperation);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "DownloadStringAsync", "");
			}
		}

		/// <summary>Occurs when an asynchronous data download operation completes.</summary>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000D66 RID: 3430 RVA: 0x00047F08 File Offset: 0x00046108
		// (remove) Token: 0x06000D67 RID: 3431 RVA: 0x00047F40 File Offset: 0x00046140
		public event DownloadDataCompletedEventHandler DownloadDataCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadDataCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.DownloadDataCompletedEventArgs" /> object that contains event data.</param>
		// Token: 0x06000D68 RID: 3432 RVA: 0x00047F75 File Offset: 0x00046175
		protected virtual void OnDownloadDataCompleted(DownloadDataCompletedEventArgs e)
		{
			if (this.DownloadDataCompleted != null)
			{
				this.DownloadDataCompleted(this, e);
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00047F8C File Offset: 0x0004618C
		private void DownloadDataOperationCompleted(object arg)
		{
			this.OnDownloadDataCompleted((DownloadDataCompletedEventArgs)arg);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00047F9C File Offset: 0x0004619C
		private void DownloadDataAsyncCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			DownloadDataCompletedEventArgs downloadDataCompletedEventArgs = new DownloadDataCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.downloadDataOperationCompleted, downloadDataCompletedEventArgs);
		}

		/// <summary>Downloads the resource as a <see cref="T:System.Byte" /> array from the URI specified as an asynchronous operation.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		// Token: 0x06000D6B RID: 3435 RVA: 0x00047FD2 File Offset: 0x000461D2
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadDataAsync(Uri address)
		{
			this.DownloadDataAsync(address, null);
		}

		/// <summary>Downloads the resource as a <see cref="T:System.Byte" /> array from the URI specified as an asynchronous operation.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		// Token: 0x06000D6C RID: 3436 RVA: 0x00047FDC File Offset: 0x000461DC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadDataAsync(Uri address, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadDataAsync", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.DownloadBits(webRequest, null, new CompletionDelegate(this.DownloadDataAsyncCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				this.DownloadDataAsyncCallback(null, ex, asyncOperation);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "DownloadDataAsync", null);
			}
		}

		/// <summary>Occurs when an asynchronous file download operation completes.</summary>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000D6D RID: 3437 RVA: 0x000480D0 File Offset: 0x000462D0
		// (remove) Token: 0x06000D6E RID: 3438 RVA: 0x00048108 File Offset: 0x00046308
		public event AsyncCompletedEventHandler DownloadFileCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadFileCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06000D6F RID: 3439 RVA: 0x0004813D File Offset: 0x0004633D
		protected virtual void OnDownloadFileCompleted(AsyncCompletedEventArgs e)
		{
			if (this.DownloadFileCompleted != null)
			{
				this.DownloadFileCompleted(this, e);
			}
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00048154 File Offset: 0x00046354
		private void DownloadFileOperationCompleted(object arg)
		{
			this.OnDownloadFileCompleted((AsyncCompletedEventArgs)arg);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00048164 File Offset: 0x00046364
		private void DownloadFileAsyncCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			AsyncCompletedEventArgs asyncCompletedEventArgs = new AsyncCompletedEventArgs(exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.downloadFileOperationCompleted, asyncCompletedEventArgs);
		}

		/// <summary>Downloads, to a local file, the resource with the specified URI. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to download.</param>
		/// <param name="fileName">The name of the file to be placed on the local computer.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		/// <exception cref="T:System.InvalidOperationException">The local file specified by <paramref name="fileName" /> is in use by another thread.</exception>
		// Token: 0x06000D72 RID: 3442 RVA: 0x00048199 File Offset: 0x00046399
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadFileAsync(Uri address, string fileName)
		{
			this.DownloadFileAsync(address, fileName, null);
		}

		/// <summary>Downloads, to a local file, the resource with the specified URI. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to download.</param>
		/// <param name="fileName">The name of the file to be placed on the local computer.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		/// <exception cref="T:System.InvalidOperationException">The local file specified by <paramref name="fileName" /> is in use by another thread.</exception>
		// Token: 0x06000D73 RID: 3443 RVA: 0x000481A4 File Offset: 0x000463A4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadFileAsync(Uri address, string fileName, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadFileAsync", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			FileStream fileStream = null;
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.DownloadBits(webRequest, fileStream, new CompletionDelegate(this.DownloadFileAsyncCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (fileStream != null)
				{
					fileStream.Close();
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				this.DownloadFileAsyncCallback(null, ex, asyncOperation);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "DownloadFileAsync", null);
			}
		}

		/// <summary>Occurs when an asynchronous string-upload operation completes.</summary>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000D74 RID: 3444 RVA: 0x000482C0 File Offset: 0x000464C0
		// (remove) Token: 0x06000D75 RID: 3445 RVA: 0x000482F8 File Offset: 0x000464F8
		public event UploadStringCompletedEventHandler UploadStringCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadStringCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Net.UploadStringCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06000D76 RID: 3446 RVA: 0x0004832D File Offset: 0x0004652D
		protected virtual void OnUploadStringCompleted(UploadStringCompletedEventArgs e)
		{
			if (this.UploadStringCompleted != null)
			{
				this.UploadStringCompleted(this, e);
			}
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00048344 File Offset: 0x00046544
		private void UploadStringOperationCompleted(object arg)
		{
			this.OnUploadStringCompleted((UploadStringCompletedEventArgs)arg);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00048354 File Offset: 0x00046554
		private void StartDownloadAsync(WebClient.UploadBitsState state)
		{
			try
			{
				this.DownloadBits(state.Request, null, state.DownloadCompletionDelegate, state.AsyncOp);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				state.DownloadCompletionDelegate(null, ex, state.AsyncOp);
			}
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x000483E0 File Offset: 0x000465E0
		private void UploadStringAsyncWriteCallback(byte[] returnBytes, Exception exception, object state)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)state;
			if (exception != null)
			{
				UploadStringCompletedEventArgs uploadStringCompletedEventArgs = new UploadStringCompletedEventArgs(null, exception, this.m_Cancelled, uploadBitsState.AsyncOp.UserSuppliedState);
				this.InvokeOperationCompleted(uploadBitsState.AsyncOp, this.uploadStringOperationCompleted, uploadStringCompletedEventArgs);
				return;
			}
			this.StartDownloadAsync(uploadBitsState);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0004842C File Offset: 0x0004662C
		private void UploadStringAsyncReadCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			string text = null;
			try
			{
				if (returnBytes != null)
				{
					text = this.GetStringUsingEncoding(this.m_WebRequest, returnBytes);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				exception = ex;
			}
			UploadStringCompletedEventArgs uploadStringCompletedEventArgs = new UploadStringCompletedEventArgs(text, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.uploadStringOperationCompleted, uploadStringCompletedEventArgs);
		}

		/// <summary>Uploads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000D7B RID: 3451 RVA: 0x000484A8 File Offset: 0x000466A8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadStringAsync(Uri address, string data)
		{
			this.UploadStringAsync(address, null, data, null);
		}

		/// <summary>Uploads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="method" /> cannot be used to send content.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000D7C RID: 3452 RVA: 0x000484B4 File Offset: 0x000466B4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadStringAsync(Uri address, string method, string data)
		{
			this.UploadStringAsync(address, method, data, null);
		}

		/// <summary>Uploads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="method" /> cannot be used to send content.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000D7D RID: 3453 RVA: 0x000484C0 File Offset: 0x000466C0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadStringAsync(Uri address, string method, string data, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadStringAsync", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				byte[] bytes = this.Encoding.GetBytes(data);
				this.m_Method = method;
				this.m_ContentLength = (long)bytes.Length;
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.UploadBits(webRequest, null, bytes, 0, null, null, new CompletionDelegate(this.UploadStringAsyncWriteCallback), new CompletionDelegate(this.UploadStringAsyncReadCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				UploadStringCompletedEventArgs uploadStringCompletedEventArgs = new UploadStringCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.uploadStringOperationCompleted, uploadStringCompletedEventArgs);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "UploadStringAsync", null);
			}
		}

		/// <summary>Occurs when an asynchronous data-upload operation completes.</summary>
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000D7E RID: 3454 RVA: 0x00048620 File Offset: 0x00046820
		// (remove) Token: 0x06000D7F RID: 3455 RVA: 0x00048658 File Offset: 0x00046858
		public event UploadDataCompletedEventHandler UploadDataCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadDataCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.UploadDataCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06000D80 RID: 3456 RVA: 0x0004868D File Offset: 0x0004688D
		protected virtual void OnUploadDataCompleted(UploadDataCompletedEventArgs e)
		{
			if (this.UploadDataCompleted != null)
			{
				this.UploadDataCompleted(this, e);
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000486A4 File Offset: 0x000468A4
		private void UploadDataOperationCompleted(object arg)
		{
			this.OnUploadDataCompleted((UploadDataCompletedEventArgs)arg);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000486B4 File Offset: 0x000468B4
		private void UploadDataAsyncWriteCallback(byte[] returnBytes, Exception exception, object state)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)state;
			if (exception != null)
			{
				UploadDataCompletedEventArgs uploadDataCompletedEventArgs = new UploadDataCompletedEventArgs(returnBytes, exception, this.m_Cancelled, uploadBitsState.AsyncOp.UserSuppliedState);
				this.InvokeOperationCompleted(uploadBitsState.AsyncOp, this.uploadDataOperationCompleted, uploadDataCompletedEventArgs);
				return;
			}
			this.StartDownloadAsync(uploadBitsState);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00048700 File Offset: 0x00046900
		private void UploadDataAsyncReadCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			UploadDataCompletedEventArgs uploadDataCompletedEventArgs = new UploadDataCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.uploadDataOperationCompleted, uploadDataCompletedEventArgs);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000D84 RID: 3460 RVA: 0x00048736 File Offset: 0x00046936
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadDataAsync(Uri address, byte[] data)
		{
			this.UploadDataAsync(address, null, data, null);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI, using the specified method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If <see langword="null" />, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000D85 RID: 3461 RVA: 0x00048742 File Offset: 0x00046942
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadDataAsync(Uri address, string method, byte[] data)
		{
			this.UploadDataAsync(address, method, data, null);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI, using the specified method and identifying token.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If <see langword="null" />, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000D86 RID: 3462 RVA: 0x00048750 File Offset: 0x00046950
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadDataAsync(Uri address, string method, byte[] data, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadDataAsync", ((address != null) ? address.ToString() : null) + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			int num = 0;
			try
			{
				this.m_Method = method;
				this.m_ContentLength = (long)data.Length;
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				if (this.UploadProgressChanged != null)
				{
					num = (int)Math.Min(8192L, (long)data.Length);
				}
				this.UploadBits(webRequest, null, data, num, null, null, new CompletionDelegate(this.UploadDataAsyncWriteCallback), new CompletionDelegate(this.UploadDataAsyncReadCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				UploadDataCompletedEventArgs uploadDataCompletedEventArgs = new UploadDataCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.uploadDataOperationCompleted, uploadDataCompletedEventArgs);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "UploadDataAsync", null);
			}
		}

		/// <summary>Occurs when an asynchronous file-upload operation completes.</summary>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000D87 RID: 3463 RVA: 0x000488D4 File Offset: 0x00046AD4
		// (remove) Token: 0x06000D88 RID: 3464 RVA: 0x0004890C File Offset: 0x00046B0C
		public event UploadFileCompletedEventHandler UploadFileCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadFileCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Net.UploadFileCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06000D89 RID: 3465 RVA: 0x00048941 File Offset: 0x00046B41
		protected virtual void OnUploadFileCompleted(UploadFileCompletedEventArgs e)
		{
			if (this.UploadFileCompleted != null)
			{
				this.UploadFileCompleted(this, e);
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00048958 File Offset: 0x00046B58
		private void UploadFileOperationCompleted(object arg)
		{
			this.OnUploadFileCompleted((UploadFileCompletedEventArgs)arg);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00048968 File Offset: 0x00046B68
		private void UploadFileAsyncWriteCallback(byte[] returnBytes, Exception exception, object state)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)state;
			if (exception != null)
			{
				UploadFileCompletedEventArgs uploadFileCompletedEventArgs = new UploadFileCompletedEventArgs(returnBytes, exception, this.m_Cancelled, uploadBitsState.AsyncOp.UserSuppliedState);
				this.InvokeOperationCompleted(uploadBitsState.AsyncOp, this.uploadFileOperationCompleted, uploadFileCompletedEventArgs);
				return;
			}
			this.StartDownloadAsync(uploadBitsState);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000489B4 File Offset: 0x00046BB4
		private void UploadFileAsyncReadCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			UploadFileCompletedEventArgs uploadFileCompletedEventArgs = new UploadFileCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.uploadFileOperationCompleted, uploadFileCompletedEventArgs);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="fileName">The file to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="fileName" /> is <see langword="null" />, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header begins with <see langword="multipart" />.</exception>
		// Token: 0x06000D8D RID: 3469 RVA: 0x000489EA File Offset: 0x00046BEA
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadFileAsync(Uri address, string fileName)
		{
			this.UploadFileAsync(address, null, fileName, null);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The method used to send the data to the resource. If <see langword="null" />, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="fileName" /> is <see langword="null" />, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header begins with <see langword="multipart" />.</exception>
		// Token: 0x06000D8E RID: 3470 RVA: 0x000489F6 File Offset: 0x00046BF6
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadFileAsync(Uri address, string method, string fileName)
		{
			this.UploadFileAsync(address, method, fileName, null);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The method used to send the data to the resource. If <see langword="null" />, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="fileName" /> is <see langword="null" />, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header begins with <see langword="multipart" />.</exception>
		// Token: 0x06000D8F RID: 3471 RVA: 0x00048A04 File Offset: 0x00046C04
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadFileAsync(Uri address, string method, string fileName, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadFileAsync", ((address != null) ? address.ToString() : null) + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			FileStream fileStream = null;
			try
			{
				this.m_Method = method;
				byte[] array = null;
				byte[] array2 = null;
				byte[] array3 = null;
				Uri uri = this.GetUri(address);
				bool flag = uri.Scheme != Uri.UriSchemeFile;
				this.OpenFileInternal(flag, fileName, ref fileStream, ref array3, ref array, ref array2);
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(uri));
				this.UploadBits(webRequest, fileStream, array3, 0, array, array2, new CompletionDelegate(this.UploadFileAsyncWriteCallback), new CompletionDelegate(this.UploadFileAsyncReadCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (fileStream != null)
				{
					fileStream.Close();
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				UploadFileCompletedEventArgs uploadFileCompletedEventArgs = new UploadFileCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.uploadFileOperationCompleted, uploadFileCompletedEventArgs);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "UploadFileAsync", null);
			}
		}

		/// <summary>Occurs when an asynchronous upload of a name/value collection completes.</summary>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000D90 RID: 3472 RVA: 0x00048BA0 File Offset: 0x00046DA0
		// (remove) Token: 0x06000D91 RID: 3473 RVA: 0x00048BD8 File Offset: 0x00046DD8
		public event UploadValuesCompletedEventHandler UploadValuesCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadValuesCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.UploadValuesCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06000D92 RID: 3474 RVA: 0x00048C0D File Offset: 0x00046E0D
		protected virtual void OnUploadValuesCompleted(UploadValuesCompletedEventArgs e)
		{
			if (this.UploadValuesCompleted != null)
			{
				this.UploadValuesCompleted(this, e);
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00048C24 File Offset: 0x00046E24
		private void UploadValuesOperationCompleted(object arg)
		{
			this.OnUploadValuesCompleted((UploadValuesCompletedEventArgs)arg);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00048C34 File Offset: 0x00046E34
		private void UploadValuesAsyncWriteCallback(byte[] returnBytes, Exception exception, object state)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)state;
			if (exception != null)
			{
				UploadValuesCompletedEventArgs uploadValuesCompletedEventArgs = new UploadValuesCompletedEventArgs(returnBytes, exception, this.m_Cancelled, uploadBitsState.AsyncOp.UserSuppliedState);
				this.InvokeOperationCompleted(uploadBitsState.AsyncOp, this.uploadValuesOperationCompleted, uploadValuesCompletedEventArgs);
				return;
			}
			this.StartDownloadAsync(uploadBitsState);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00048C80 File Offset: 0x00046E80
		private void UploadValuesAsyncReadCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			UploadValuesCompletedEventArgs uploadValuesCompletedEventArgs = new UploadValuesCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.uploadValuesOperationCompleted, uploadValuesCompletedEventArgs);
		}

		/// <summary>Uploads the data in the specified name/value collection to the resource identified by the specified URI. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the collection. This URI must identify a resource that can accept a request sent with the default method.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000D96 RID: 3478 RVA: 0x00048CB6 File Offset: 0x00046EB6
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadValuesAsync(Uri address, NameValueCollection data)
		{
			this.UploadValuesAsync(address, null, data, null);
		}

		/// <summary>Uploads the data in the specified name/value collection to the resource identified by the specified URI, using the specified method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the collection. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method.</param>
		/// <param name="method">The method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  <paramref name="method" /> cannot be used to send content.</exception>
		// Token: 0x06000D97 RID: 3479 RVA: 0x00048CC2 File Offset: 0x00046EC2
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadValuesAsync(Uri address, string method, NameValueCollection data)
		{
			this.UploadValuesAsync(address, method, data, null);
		}

		/// <summary>Uploads the data in the specified name/value collection to the resource identified by the specified URI, using the specified method. This method does not block the calling thread, and allows the caller to pass an object to the method that is invoked when the operation completes.</summary>
		/// <param name="address">The URI of the resource to receive the collection. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method.</param>
		/// <param name="method">The HTTP method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  <paramref name="method" /> cannot be used to send content.</exception>
		// Token: 0x06000D98 RID: 3480 RVA: 0x00048CD0 File Offset: 0x00046ED0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadValuesAsync(Uri address, string method, NameValueCollection data, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadValuesAsync", ((address != null) ? address.ToString() : null) + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			int num = 0;
			try
			{
				byte[] array = this.UploadValuesInternal(data);
				this.m_Method = method;
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				if (this.UploadProgressChanged != null)
				{
					num = (int)Math.Min(8192L, (long)array.Length);
				}
				this.UploadBits(webRequest, null, array, num, null, null, new CompletionDelegate(this.UploadValuesAsyncWriteCallback), new CompletionDelegate(this.UploadValuesAsyncReadCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				UploadValuesCompletedEventArgs uploadValuesCompletedEventArgs = new UploadValuesCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.uploadValuesOperationCompleted, uploadValuesCompletedEventArgs);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "UploadValuesAsync", null);
			}
		}

		/// <summary>Cancels a pending asynchronous operation.</summary>
		// Token: 0x06000D99 RID: 3481 RVA: 0x00048E54 File Offset: 0x00047054
		public void CancelAsync()
		{
			WebRequest webRequest = this.m_WebRequest;
			this.m_Cancelled = true;
			WebClient.AbortRequest(webRequest);
		}

		/// <summary>Downloads the resource as a <see cref="T:System.String" /> from the URI specified as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to download.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		// Token: 0x06000D9A RID: 3482 RVA: 0x00048E75 File Offset: 0x00047075
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<string> DownloadStringTaskAsync(string address)
		{
			return this.DownloadStringTaskAsync(this.GetUri(address));
		}

		/// <summary>Downloads the resource as a <see cref="T:System.String" /> from the URI specified as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to download.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		// Token: 0x06000D9B RID: 3483 RVA: 0x00048E84 File Offset: 0x00047084
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<string> DownloadStringTaskAsync(Uri address)
		{
			TaskCompletionSource<string> tcs = new TaskCompletionSource<string>(address);
			DownloadStringCompletedEventHandler handler = null;
			handler = delegate(object sender, DownloadStringCompletedEventArgs e)
			{
				this.HandleCompletion<DownloadStringCompletedEventArgs, DownloadStringCompletedEventHandler, string>(tcs, e, (DownloadStringCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, DownloadStringCompletedEventHandler completion)
				{
					webClient.DownloadStringCompleted -= completion;
				});
			};
			this.DownloadStringCompleted += handler;
			try
			{
				this.DownloadStringAsync(address, tcs);
			}
			catch
			{
				this.DownloadStringCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Opens a readable stream containing the specified resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to retrieve.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.IO.Stream" /> used to read data from a resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and address is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.  
		///  -or-  
		///  An error occurred while opening the stream.</exception>
		// Token: 0x06000D9C RID: 3484 RVA: 0x00048F08 File Offset: 0x00047108
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Stream> OpenReadTaskAsync(string address)
		{
			return this.OpenReadTaskAsync(this.GetUri(address));
		}

		/// <summary>Opens a readable stream containing the specified resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to retrieve.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.IO.Stream" /> used to read data from a resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and address is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.  
		///  -or-  
		///  An error occurred while opening the stream.</exception>
		// Token: 0x06000D9D RID: 3485 RVA: 0x00048F18 File Offset: 0x00047118
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Stream> OpenReadTaskAsync(Uri address)
		{
			TaskCompletionSource<Stream> tcs = new TaskCompletionSource<Stream>(address);
			OpenReadCompletedEventHandler handler = null;
			handler = delegate(object sender, OpenReadCompletedEventArgs e)
			{
				this.HandleCompletion<OpenReadCompletedEventArgs, OpenReadCompletedEventHandler, Stream>(tcs, e, (OpenReadCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, OpenReadCompletedEventHandler completion)
				{
					webClient.OpenReadCompleted -= completion;
				});
			};
			this.OpenReadCompleted += handler;
			try
			{
				this.OpenReadAsync(address, tcs);
			}
			catch
			{
				this.OpenReadCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Opens a stream for writing data to the specified resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.</exception>
		// Token: 0x06000D9E RID: 3486 RVA: 0x00048F9C File Offset: 0x0004719C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Stream> OpenWriteTaskAsync(string address)
		{
			return this.OpenWriteTaskAsync(this.GetUri(address), null);
		}

		/// <summary>Opens a stream for writing data to the specified resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.</exception>
		// Token: 0x06000D9F RID: 3487 RVA: 0x00048FAC File Offset: 0x000471AC
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Stream> OpenWriteTaskAsync(Uri address)
		{
			return this.OpenWriteTaskAsync(address, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.</exception>
		// Token: 0x06000DA0 RID: 3488 RVA: 0x00048FB6 File Offset: 0x000471B6
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Stream> OpenWriteTaskAsync(string address, string method)
		{
			return this.OpenWriteTaskAsync(this.GetUri(address), method);
		}

		/// <summary>Opens a stream for writing data to the specified resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.</exception>
		// Token: 0x06000DA1 RID: 3489 RVA: 0x00048FC8 File Offset: 0x000471C8
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Stream> OpenWriteTaskAsync(Uri address, string method)
		{
			TaskCompletionSource<Stream> tcs = new TaskCompletionSource<Stream>(address);
			OpenWriteCompletedEventHandler handler = null;
			handler = delegate(object sender, OpenWriteCompletedEventArgs e)
			{
				this.HandleCompletion<OpenWriteCompletedEventArgs, OpenWriteCompletedEventHandler, Stream>(tcs, e, (OpenWriteCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, OpenWriteCompletedEventHandler completion)
				{
					webClient.OpenWriteCompleted -= completion;
				});
			};
			this.OpenWriteCompleted += handler;
			try
			{
				this.OpenWriteAsync(address, method, tcs);
			}
			catch
			{
				this.OpenWriteCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Uploads the specified string to the specified resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000DA2 RID: 3490 RVA: 0x00049050 File Offset: 0x00047250
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<string> UploadStringTaskAsync(string address, string data)
		{
			return this.UploadStringTaskAsync(address, null, data);
		}

		/// <summary>Uploads the specified string to the specified resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000DA3 RID: 3491 RVA: 0x0004905B File Offset: 0x0004725B
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<string> UploadStringTaskAsync(Uri address, string data)
		{
			return this.UploadStringTaskAsync(address, null, data);
		}

		/// <summary>Uploads the specified string to the specified resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="method" /> cannot be used to send content.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000DA4 RID: 3492 RVA: 0x00049066 File Offset: 0x00047266
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<string> UploadStringTaskAsync(string address, string method, string data)
		{
			return this.UploadStringTaskAsync(this.GetUri(address), method, data);
		}

		/// <summary>Uploads the specified string to the specified resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="method" /> cannot be used to send content.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000DA5 RID: 3493 RVA: 0x00049078 File Offset: 0x00047278
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<string> UploadStringTaskAsync(Uri address, string method, string data)
		{
			TaskCompletionSource<string> tcs = new TaskCompletionSource<string>(address);
			UploadStringCompletedEventHandler handler = null;
			handler = delegate(object sender, UploadStringCompletedEventArgs e)
			{
				this.HandleCompletion<UploadStringCompletedEventArgs, UploadStringCompletedEventHandler, string>(tcs, e, (UploadStringCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, UploadStringCompletedEventHandler completion)
				{
					webClient.UploadStringCompleted -= completion;
				});
			};
			this.UploadStringCompleted += handler;
			try
			{
				this.UploadStringAsync(address, method, data, tcs);
			}
			catch
			{
				this.UploadStringCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Downloads the resource as a <see cref="T:System.Byte" /> array from the URI specified as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to download.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		// Token: 0x06000DA6 RID: 3494 RVA: 0x00049100 File Offset: 0x00047300
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> DownloadDataTaskAsync(string address)
		{
			return this.DownloadDataTaskAsync(this.GetUri(address));
		}

		/// <summary>Downloads the resource as a <see cref="T:System.Byte" /> array from the URI specified as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to download.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		// Token: 0x06000DA7 RID: 3495 RVA: 0x00049110 File Offset: 0x00047310
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> DownloadDataTaskAsync(Uri address)
		{
			TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>(address);
			DownloadDataCompletedEventHandler handler = null;
			handler = delegate(object sender, DownloadDataCompletedEventArgs e)
			{
				this.HandleCompletion<DownloadDataCompletedEventArgs, DownloadDataCompletedEventHandler, byte[]>(tcs, e, (DownloadDataCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, DownloadDataCompletedEventHandler completion)
				{
					webClient.DownloadDataCompleted -= completion;
				});
			};
			this.DownloadDataCompleted += handler;
			try
			{
				this.DownloadDataAsync(address, tcs);
			}
			catch
			{
				this.DownloadDataCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Downloads the specified resource to a local file as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to download.</param>
		/// <param name="fileName">The name of the file to be placed on the local computer.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		/// <exception cref="T:System.InvalidOperationException">The local file specified by <paramref name="fileName" /> is in use by another thread.</exception>
		// Token: 0x06000DA8 RID: 3496 RVA: 0x00049194 File Offset: 0x00047394
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task DownloadFileTaskAsync(string address, string fileName)
		{
			return this.DownloadFileTaskAsync(this.GetUri(address), fileName);
		}

		/// <summary>Downloads the specified resource to a local file as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to download.</param>
		/// <param name="fileName">The name of the file to be placed on the local computer.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while downloading the resource.</exception>
		/// <exception cref="T:System.InvalidOperationException">The local file specified by <paramref name="fileName" /> is in use by another thread.</exception>
		// Token: 0x06000DA9 RID: 3497 RVA: 0x000491A4 File Offset: 0x000473A4
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task DownloadFileTaskAsync(Uri address, string fileName)
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>(address);
			AsyncCompletedEventHandler handler = null;
			handler = delegate(object sender, AsyncCompletedEventArgs e)
			{
				this.HandleCompletion<AsyncCompletedEventArgs, AsyncCompletedEventHandler, object>(tcs, e, (AsyncCompletedEventArgs args) => null, handler, delegate(WebClient webClient, AsyncCompletedEventHandler completion)
				{
					webClient.DownloadFileCompleted -= completion;
				});
			};
			this.DownloadFileCompleted += handler;
			try
			{
				this.DownloadFileAsync(address, fileName, tcs);
			}
			catch
			{
				this.DownloadFileCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Uploads a data buffer that contains a <see cref="T:System.Byte" /> array to the URI specified as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the data buffer was uploaded.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000DAA RID: 3498 RVA: 0x0004922C File Offset: 0x0004742C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadDataTaskAsync(string address, byte[] data)
		{
			return this.UploadDataTaskAsync(this.GetUri(address), null, data);
		}

		/// <summary>Uploads a data buffer that contains a <see cref="T:System.Byte" /> array to the URI specified as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the data buffer was uploaded.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000DAB RID: 3499 RVA: 0x0004923D File Offset: 0x0004743D
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadDataTaskAsync(Uri address, byte[] data)
		{
			return this.UploadDataTaskAsync(address, null, data);
		}

		/// <summary>Uploads a data buffer that contains a <see cref="T:System.Byte" /> array to the URI specified as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If <see langword="null" />, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the data buffer was uploaded.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000DAC RID: 3500 RVA: 0x00049248 File Offset: 0x00047448
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadDataTaskAsync(string address, string method, byte[] data)
		{
			return this.UploadDataTaskAsync(this.GetUri(address), method, data);
		}

		/// <summary>Uploads a data buffer that contains a <see cref="T:System.Byte" /> array to the URI specified as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If <see langword="null" />, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the data buffer was uploaded.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.</exception>
		// Token: 0x06000DAD RID: 3501 RVA: 0x0004925C File Offset: 0x0004745C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadDataTaskAsync(Uri address, string method, byte[] data)
		{
			TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>(address);
			UploadDataCompletedEventHandler handler = null;
			handler = delegate(object sender, UploadDataCompletedEventArgs e)
			{
				this.HandleCompletion<UploadDataCompletedEventArgs, UploadDataCompletedEventHandler, byte[]>(tcs, e, (UploadDataCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, UploadDataCompletedEventHandler completion)
				{
					webClient.UploadDataCompleted -= completion;
				});
			};
			this.UploadDataCompleted += handler;
			try
			{
				this.UploadDataAsync(address, method, data, tcs);
			}
			catch
			{
				this.UploadDataCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Uploads the specified local file to a resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="fileName">The local file to send to the resource.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the file was uploaded.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="fileName" /> is <see langword="null" />, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header begins with <see langword="multipart" />.</exception>
		// Token: 0x06000DAE RID: 3502 RVA: 0x000492E4 File Offset: 0x000474E4
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadFileTaskAsync(string address, string fileName)
		{
			return this.UploadFileTaskAsync(this.GetUri(address), null, fileName);
		}

		/// <summary>Uploads the specified local file to a resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="fileName">The local file to send to the resource.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the file was uploaded.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="fileName" /> is <see langword="null" />, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header begins with <see langword="multipart" />.</exception>
		// Token: 0x06000DAF RID: 3503 RVA: 0x000492F5 File Offset: 0x000474F5
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadFileTaskAsync(Uri address, string fileName)
		{
			return this.UploadFileTaskAsync(address, null, fileName);
		}

		/// <summary>Uploads the specified local file to a resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The method used to send the data to the resource. If <see langword="null" />, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The local file to send to the resource.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the file was uploaded.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="fileName" /> is <see langword="null" />, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header begins with <see langword="multipart" />.</exception>
		// Token: 0x06000DB0 RID: 3504 RVA: 0x00049300 File Offset: 0x00047500
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadFileTaskAsync(string address, string method, string fileName)
		{
			return this.UploadFileTaskAsync(this.GetUri(address), method, fileName);
		}

		/// <summary>Uploads the specified local file to a resource as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The method used to send the data to the resource. If <see langword="null" />, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The local file to send to the resource.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the file was uploaded.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="fileName" /> is <see langword="null" />, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header begins with <see langword="multipart" />.</exception>
		// Token: 0x06000DB1 RID: 3505 RVA: 0x00049314 File Offset: 0x00047514
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadFileTaskAsync(Uri address, string method, string fileName)
		{
			TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>(address);
			UploadFileCompletedEventHandler handler = null;
			handler = delegate(object sender, UploadFileCompletedEventArgs e)
			{
				this.HandleCompletion<UploadFileCompletedEventArgs, UploadFileCompletedEventHandler, byte[]>(tcs, e, (UploadFileCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, UploadFileCompletedEventHandler completion)
				{
					webClient.UploadFileCompleted -= completion;
				});
			};
			this.UploadFileCompleted += handler;
			try
			{
				this.UploadFileAsync(address, method, fileName, tcs);
			}
			catch
			{
				this.UploadFileCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the collection.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the response sent by the server.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  The <see langword="Content-type" /> header is not <see langword="null" /> or "application/x-www-form-urlencoded".</exception>
		// Token: 0x06000DB2 RID: 3506 RVA: 0x0004939C File Offset: 0x0004759C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadValuesTaskAsync(string address, NameValueCollection data)
		{
			return this.UploadValuesTaskAsync(this.GetUri(address), null, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the collection.</param>
		/// <param name="method">The HTTP method used to send the collection to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the response sent by the server.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="method" /> cannot be used to send content.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  The <see langword="Content-type" /> header is not <see langword="null" /> or "application/x-www-form-urlencoded".</exception>
		// Token: 0x06000DB3 RID: 3507 RVA: 0x000493AD File Offset: 0x000475AD
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadValuesTaskAsync(string address, string method, NameValueCollection data)
		{
			return this.UploadValuesTaskAsync(this.GetUri(address), method, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the collection.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the response sent by the server.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  The <see langword="Content-type" /> header value is not <see langword="null" /> and is not <see langword="application/x-www-form-urlencoded" />.</exception>
		// Token: 0x06000DB4 RID: 3508 RVA: 0x000493BE File Offset: 0x000475BE
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadValuesTaskAsync(Uri address, NameValueCollection data)
		{
			return this.UploadValuesTaskAsync(address, null, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI as an asynchronous operation using a task object.</summary>
		/// <param name="address">The URI of the resource to receive the collection.</param>
		/// <param name="method">The HTTP method used to send the collection to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the response sent by the server.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.  
		///  -or-  
		///  <paramref name="method" /> cannot be used to send content.  
		///  -or-  
		///  There was no response from the server hosting the resource.  
		///  -or-  
		///  An error occurred while opening the stream.  
		///  -or-  
		///  The <see langword="Content-type" /> header is not <see langword="null" /> or "application/x-www-form-urlencoded".</exception>
		// Token: 0x06000DB5 RID: 3509 RVA: 0x000493CC File Offset: 0x000475CC
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadValuesTaskAsync(Uri address, string method, NameValueCollection data)
		{
			TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>(address);
			UploadValuesCompletedEventHandler handler = null;
			handler = delegate(object sender, UploadValuesCompletedEventArgs e)
			{
				this.HandleCompletion<UploadValuesCompletedEventArgs, UploadValuesCompletedEventHandler, byte[]>(tcs, e, (UploadValuesCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, UploadValuesCompletedEventHandler completion)
				{
					webClient.UploadValuesCompleted -= completion;
				});
			};
			this.UploadValuesCompleted += handler;
			try
			{
				this.UploadValuesAsync(address, method, data, tcs);
			}
			catch
			{
				this.UploadValuesCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00049454 File Offset: 0x00047654
		private void HandleCompletion<TAsyncCompletedEventArgs, TCompletionDelegate, T>(TaskCompletionSource<T> tcs, TAsyncCompletedEventArgs e, Func<TAsyncCompletedEventArgs, T> getResult, TCompletionDelegate handler, Action<WebClient, TCompletionDelegate> unregisterHandler) where TAsyncCompletedEventArgs : AsyncCompletedEventArgs
		{
			if (e.UserState == tcs)
			{
				try
				{
					unregisterHandler(this, handler);
				}
				finally
				{
					if (e.Error != null)
					{
						tcs.TrySetException(e.Error);
					}
					else if (e.Cancelled)
					{
						tcs.TrySetCanceled();
					}
					else
					{
						tcs.TrySetResult(getResult(e));
					}
				}
			}
		}

		/// <summary>Occurs when an asynchronous download operation successfully transfers some or all of the data.</summary>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000DB7 RID: 3511 RVA: 0x000494D4 File Offset: 0x000476D4
		// (remove) Token: 0x06000DB8 RID: 3512 RVA: 0x0004950C File Offset: 0x0004770C
		public event DownloadProgressChangedEventHandler DownloadProgressChanged;

		/// <summary>Occurs when an asynchronous upload operation successfully transfers some or all of the data.</summary>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000DB9 RID: 3513 RVA: 0x00049544 File Offset: 0x00047744
		// (remove) Token: 0x06000DBA RID: 3514 RVA: 0x0004957C File Offset: 0x0004777C
		public event UploadProgressChangedEventHandler UploadProgressChanged;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadProgressChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.DownloadProgressChangedEventArgs" /> object containing event data.</param>
		// Token: 0x06000DBB RID: 3515 RVA: 0x000495B1 File Offset: 0x000477B1
		protected virtual void OnDownloadProgressChanged(DownloadProgressChangedEventArgs e)
		{
			if (this.DownloadProgressChanged != null)
			{
				this.DownloadProgressChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadProgressChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Net.UploadProgressChangedEventArgs" /> object containing event data.</param>
		// Token: 0x06000DBC RID: 3516 RVA: 0x000495C8 File Offset: 0x000477C8
		protected virtual void OnUploadProgressChanged(UploadProgressChangedEventArgs e)
		{
			if (this.UploadProgressChanged != null)
			{
				this.UploadProgressChanged(this, e);
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000495DF File Offset: 0x000477DF
		private void ReportDownloadProgressChanged(object arg)
		{
			this.OnDownloadProgressChanged((DownloadProgressChangedEventArgs)arg);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000495ED File Offset: 0x000477ED
		private void ReportUploadProgressChanged(object arg)
		{
			this.OnUploadProgressChanged((UploadProgressChangedEventArgs)arg);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000495FC File Offset: 0x000477FC
		private void PostProgressChanged(AsyncOperation asyncOp, WebClient.ProgressData progress)
		{
			if (asyncOp != null && progress.BytesSent + progress.BytesReceived > 0L)
			{
				int num;
				if (progress.HasUploadPhase)
				{
					if (progress.TotalBytesToReceive < 0L && progress.BytesReceived == 0L)
					{
						num = ((progress.TotalBytesToSend < 0L) ? 0 : ((progress.TotalBytesToSend == 0L) ? 50 : ((int)(50L * progress.BytesSent / progress.TotalBytesToSend))));
					}
					else
					{
						num = ((progress.TotalBytesToSend < 0L) ? 50 : ((progress.TotalBytesToReceive == 0L) ? 100 : ((int)(50L * progress.BytesReceived / progress.TotalBytesToReceive + 50L))));
					}
					asyncOp.Post(this.reportUploadProgressChanged, new UploadProgressChangedEventArgs(num, asyncOp.UserSuppliedState, progress.BytesSent, progress.TotalBytesToSend, progress.BytesReceived, progress.TotalBytesToReceive));
					return;
				}
				num = ((progress.TotalBytesToReceive < 0L) ? 0 : ((progress.TotalBytesToReceive == 0L) ? 100 : ((int)(100L * progress.BytesReceived / progress.TotalBytesToReceive))));
				asyncOp.Post(this.reportDownloadProgressChanged, new DownloadProgressChangedEventArgs(num, asyncOp.UserSuppliedState, progress.BytesReceived, progress.TotalBytesToReceive));
			}
		}

		// Token: 0x040011D8 RID: 4568
		private const int DefaultCopyBufferLength = 8192;

		// Token: 0x040011D9 RID: 4569
		private const int DefaultDownloadBufferLength = 65536;

		// Token: 0x040011DA RID: 4570
		private const string DefaultUploadFileContentType = "application/octet-stream";

		// Token: 0x040011DB RID: 4571
		private const string UploadFileContentType = "multipart/form-data";

		// Token: 0x040011DC RID: 4572
		private const string UploadValuesContentType = "application/x-www-form-urlencoded";

		// Token: 0x040011DD RID: 4573
		private Uri m_baseAddress;

		// Token: 0x040011DE RID: 4574
		private ICredentials m_credentials;

		// Token: 0x040011DF RID: 4575
		private WebHeaderCollection m_headers;

		// Token: 0x040011E0 RID: 4576
		private NameValueCollection m_requestParameters;

		// Token: 0x040011E1 RID: 4577
		private WebResponse m_WebResponse;

		// Token: 0x040011E2 RID: 4578
		private WebRequest m_WebRequest;

		// Token: 0x040011E3 RID: 4579
		private Encoding m_Encoding = Encoding.Default;

		// Token: 0x040011E4 RID: 4580
		private string m_Method;

		// Token: 0x040011E5 RID: 4581
		private long m_ContentLength = -1L;

		// Token: 0x040011E6 RID: 4582
		private bool m_InitWebClientAsync;

		// Token: 0x040011E7 RID: 4583
		private bool m_Cancelled;

		// Token: 0x040011E8 RID: 4584
		private WebClient.ProgressData m_Progress;

		// Token: 0x040011E9 RID: 4585
		private IWebProxy m_Proxy;

		// Token: 0x040011EA RID: 4586
		private bool m_ProxySet;

		// Token: 0x040011EB RID: 4587
		private RequestCachePolicy m_CachePolicy;

		// Token: 0x040011EE RID: 4590
		private int m_CallNesting;

		// Token: 0x040011EF RID: 4591
		private AsyncOperation m_AsyncOp;

		// Token: 0x040011F1 RID: 4593
		private SendOrPostCallback openReadOperationCompleted;

		// Token: 0x040011F3 RID: 4595
		private SendOrPostCallback openWriteOperationCompleted;

		// Token: 0x040011F5 RID: 4597
		private SendOrPostCallback downloadStringOperationCompleted;

		// Token: 0x040011F7 RID: 4599
		private SendOrPostCallback downloadDataOperationCompleted;

		// Token: 0x040011F9 RID: 4601
		private SendOrPostCallback downloadFileOperationCompleted;

		// Token: 0x040011FB RID: 4603
		private SendOrPostCallback uploadStringOperationCompleted;

		// Token: 0x040011FD RID: 4605
		private SendOrPostCallback uploadDataOperationCompleted;

		// Token: 0x040011FF RID: 4607
		private SendOrPostCallback uploadFileOperationCompleted;

		// Token: 0x04001201 RID: 4609
		private SendOrPostCallback uploadValuesOperationCompleted;

		// Token: 0x04001204 RID: 4612
		private SendOrPostCallback reportDownloadProgressChanged;

		// Token: 0x04001205 RID: 4613
		private SendOrPostCallback reportUploadProgressChanged;

		// Token: 0x02000724 RID: 1828
		private class ProgressData
		{
			// Token: 0x06004138 RID: 16696 RVA: 0x0010F3C2 File Offset: 0x0010D5C2
			internal void Reset()
			{
				this.BytesSent = 0L;
				this.TotalBytesToSend = -1L;
				this.BytesReceived = 0L;
				this.TotalBytesToReceive = -1L;
				this.HasUploadPhase = false;
			}

			// Token: 0x0400313B RID: 12603
			internal long BytesSent;

			// Token: 0x0400313C RID: 12604
			internal long TotalBytesToSend = -1L;

			// Token: 0x0400313D RID: 12605
			internal long BytesReceived;

			// Token: 0x0400313E RID: 12606
			internal long TotalBytesToReceive = -1L;

			// Token: 0x0400313F RID: 12607
			internal bool HasUploadPhase;
		}

		// Token: 0x02000725 RID: 1829
		private class DownloadBitsState
		{
			// Token: 0x0600413A RID: 16698 RVA: 0x0010F403 File Offset: 0x0010D603
			internal DownloadBitsState(WebRequest request, Stream writeStream, CompletionDelegate completionDelegate, AsyncOperation asyncOp, WebClient.ProgressData progress, WebClient webClient)
			{
				this.WriteStream = writeStream;
				this.Request = request;
				this.AsyncOp = asyncOp;
				this.CompletionDelegate = completionDelegate;
				this.WebClient = webClient;
				this.Progress = progress;
			}

			// Token: 0x17000EF2 RID: 3826
			// (get) Token: 0x0600413B RID: 16699 RVA: 0x0010F438 File Offset: 0x0010D638
			internal bool Async
			{
				get
				{
					return this.AsyncOp != null;
				}
			}

			// Token: 0x0600413C RID: 16700 RVA: 0x0010F444 File Offset: 0x0010D644
			internal int SetResponse(WebResponse response)
			{
				this.ContentLength = response.ContentLength;
				if (this.ContentLength == -1L || this.ContentLength > 65536L)
				{
					this.Length = 65536L;
				}
				else
				{
					this.Length = this.ContentLength;
				}
				if (this.WriteStream == null)
				{
					if (this.ContentLength > 2147483647L)
					{
						throw new WebException(SR.GetString("net_webstatus_MessageLengthLimitExceeded"), WebExceptionStatus.MessageLengthLimitExceeded);
					}
					this.SgBuffers = new ScatterGatherBuffers(this.Length);
				}
				this.InnerBuffer = new byte[(int)this.Length];
				this.ReadStream = response.GetResponseStream();
				if (this.Async && response.ContentLength >= 0L)
				{
					this.Progress.TotalBytesToReceive = response.ContentLength;
				}
				if (this.Async)
				{
					if (this.ReadStream == null || this.ReadStream == Stream.Null)
					{
						WebClient.DownloadBitsReadCallbackState(this, null);
					}
					else
					{
						this.ReadStream.BeginRead(this.InnerBuffer, this.Offset, (int)this.Length - this.Offset, new AsyncCallback(WebClient.DownloadBitsReadCallback), this);
					}
					return -1;
				}
				if (this.ReadStream == null || this.ReadStream == Stream.Null)
				{
					return 0;
				}
				return this.ReadStream.Read(this.InnerBuffer, this.Offset, (int)this.Length - this.Offset);
			}

			// Token: 0x0600413D RID: 16701 RVA: 0x0010F5A4 File Offset: 0x0010D7A4
			internal bool RetrieveBytes(ref int bytesRetrieved)
			{
				if (bytesRetrieved > 0)
				{
					if (this.WriteStream != null)
					{
						this.WriteStream.Write(this.InnerBuffer, 0, bytesRetrieved);
					}
					else
					{
						this.SgBuffers.Write(this.InnerBuffer, 0, bytesRetrieved);
					}
					if (this.Async)
					{
						this.Progress.BytesReceived += (long)bytesRetrieved;
					}
					if ((long)this.Offset != this.ContentLength)
					{
						if (this.Async)
						{
							this.WebClient.PostProgressChanged(this.AsyncOp, this.Progress);
							this.ReadStream.BeginRead(this.InnerBuffer, this.Offset, (int)this.Length - this.Offset, new AsyncCallback(WebClient.DownloadBitsReadCallback), this);
						}
						else
						{
							bytesRetrieved = this.ReadStream.Read(this.InnerBuffer, this.Offset, (int)this.Length - this.Offset);
						}
						return false;
					}
				}
				if (this.Async)
				{
					if (this.Progress.TotalBytesToReceive < 0L)
					{
						this.Progress.TotalBytesToReceive = this.Progress.BytesReceived;
					}
					this.WebClient.PostProgressChanged(this.AsyncOp, this.Progress);
				}
				if (this.ReadStream != null)
				{
					this.ReadStream.Close();
				}
				if (this.WriteStream != null)
				{
					this.WriteStream.Close();
				}
				else if (this.WriteStream == null)
				{
					byte[] array = new byte[this.SgBuffers.Length];
					if (this.SgBuffers.Length > 0)
					{
						BufferOffsetSize[] buffers = this.SgBuffers.GetBuffers();
						int num = 0;
						foreach (BufferOffsetSize bufferOffsetSize in buffers)
						{
							Buffer.BlockCopy(bufferOffsetSize.Buffer, 0, array, num, bufferOffsetSize.Size);
							num += bufferOffsetSize.Size;
						}
					}
					this.InnerBuffer = array;
				}
				return true;
			}

			// Token: 0x0600413E RID: 16702 RVA: 0x0010F770 File Offset: 0x0010D970
			internal void Close()
			{
				if (this.WriteStream != null)
				{
					this.WriteStream.Close();
				}
				if (this.ReadStream != null)
				{
					this.ReadStream.Close();
				}
			}

			// Token: 0x04003140 RID: 12608
			internal WebClient WebClient;

			// Token: 0x04003141 RID: 12609
			internal Stream WriteStream;

			// Token: 0x04003142 RID: 12610
			internal byte[] InnerBuffer;

			// Token: 0x04003143 RID: 12611
			internal AsyncOperation AsyncOp;

			// Token: 0x04003144 RID: 12612
			internal WebRequest Request;

			// Token: 0x04003145 RID: 12613
			internal CompletionDelegate CompletionDelegate;

			// Token: 0x04003146 RID: 12614
			internal Stream ReadStream;

			// Token: 0x04003147 RID: 12615
			internal ScatterGatherBuffers SgBuffers;

			// Token: 0x04003148 RID: 12616
			internal long ContentLength;

			// Token: 0x04003149 RID: 12617
			internal long Length;

			// Token: 0x0400314A RID: 12618
			internal int Offset;

			// Token: 0x0400314B RID: 12619
			internal WebClient.ProgressData Progress;
		}

		// Token: 0x02000726 RID: 1830
		private class UploadBitsState
		{
			// Token: 0x0600413F RID: 16703 RVA: 0x0010F798 File Offset: 0x0010D998
			internal UploadBitsState(WebRequest request, Stream readStream, byte[] buffer, int chunkSize, byte[] header, byte[] footer, CompletionDelegate uploadCompletionDelegate, CompletionDelegate downloadCompletionDelegate, AsyncOperation asyncOp, WebClient.ProgressData progress, WebClient webClient)
			{
				this.InnerBuffer = buffer;
				this.m_ChunkSize = chunkSize;
				this.m_BufferWritePosition = 0;
				this.Header = header;
				this.Footer = footer;
				this.ReadStream = readStream;
				this.Request = request;
				this.AsyncOp = asyncOp;
				this.UploadCompletionDelegate = uploadCompletionDelegate;
				this.DownloadCompletionDelegate = downloadCompletionDelegate;
				if (this.AsyncOp != null)
				{
					this.Progress = progress;
					this.Progress.HasUploadPhase = true;
					this.Progress.TotalBytesToSend = ((request.ContentLength < 0L) ? (-1L) : request.ContentLength);
				}
				this.WebClient = webClient;
			}

			// Token: 0x17000EF3 RID: 3827
			// (get) Token: 0x06004140 RID: 16704 RVA: 0x0010F83A File Offset: 0x0010DA3A
			internal bool FileUpload
			{
				get
				{
					return this.ReadStream != null;
				}
			}

			// Token: 0x17000EF4 RID: 3828
			// (get) Token: 0x06004141 RID: 16705 RVA: 0x0010F845 File Offset: 0x0010DA45
			internal bool Async
			{
				get
				{
					return this.AsyncOp != null;
				}
			}

			// Token: 0x06004142 RID: 16706 RVA: 0x0010F850 File Offset: 0x0010DA50
			internal void SetRequestStream(Stream writeStream)
			{
				this.WriteStream = writeStream;
				byte[] array;
				if (this.Header != null)
				{
					array = this.Header;
					this.Header = null;
				}
				else
				{
					array = new byte[0];
				}
				if (this.Async)
				{
					this.Progress.BytesSent += (long)array.Length;
					this.WriteStream.BeginWrite(array, 0, array.Length, new AsyncCallback(WebClient.UploadBitsWriteCallback), this);
					return;
				}
				this.WriteStream.Write(array, 0, array.Length);
			}

			// Token: 0x06004143 RID: 16707 RVA: 0x0010F8D4 File Offset: 0x0010DAD4
			internal bool WriteBytes()
			{
				int num = 0;
				if (this.Async)
				{
					this.WebClient.PostProgressChanged(this.AsyncOp, this.Progress);
				}
				int num3;
				byte[] array;
				if (this.FileUpload)
				{
					int num2 = 0;
					if (this.InnerBuffer != null)
					{
						num2 = this.ReadStream.Read(this.InnerBuffer, 0, this.InnerBuffer.Length);
						if (num2 <= 0)
						{
							this.ReadStream.Close();
							this.InnerBuffer = null;
						}
					}
					if (this.InnerBuffer != null)
					{
						num3 = num2;
						array = this.InnerBuffer;
					}
					else
					{
						if (this.Footer == null)
						{
							return true;
						}
						num3 = this.Footer.Length;
						array = this.Footer;
						this.Footer = null;
					}
				}
				else
				{
					if (this.InnerBuffer == null)
					{
						return true;
					}
					array = this.InnerBuffer;
					if (this.m_ChunkSize != 0)
					{
						num = this.m_BufferWritePosition;
						this.m_BufferWritePosition += this.m_ChunkSize;
						num3 = this.m_ChunkSize;
						if (this.m_BufferWritePosition >= this.InnerBuffer.Length)
						{
							num3 = this.InnerBuffer.Length - num;
							this.InnerBuffer = null;
						}
					}
					else
					{
						num3 = this.InnerBuffer.Length;
						this.InnerBuffer = null;
					}
				}
				if (this.Async)
				{
					this.Progress.BytesSent += (long)num3;
					this.WriteStream.BeginWrite(array, num, num3, new AsyncCallback(WebClient.UploadBitsWriteCallback), this);
				}
				else
				{
					this.WriteStream.Write(array, 0, num3);
				}
				return false;
			}

			// Token: 0x06004144 RID: 16708 RVA: 0x0010FA3B File Offset: 0x0010DC3B
			internal void Close()
			{
				if (this.WriteStream != null)
				{
					this.WriteStream.Close();
				}
				if (this.ReadStream != null)
				{
					this.ReadStream.Close();
				}
			}

			// Token: 0x0400314C RID: 12620
			private int m_ChunkSize;

			// Token: 0x0400314D RID: 12621
			private int m_BufferWritePosition;

			// Token: 0x0400314E RID: 12622
			internal WebClient WebClient;

			// Token: 0x0400314F RID: 12623
			internal Stream WriteStream;

			// Token: 0x04003150 RID: 12624
			internal byte[] InnerBuffer;

			// Token: 0x04003151 RID: 12625
			internal byte[] Header;

			// Token: 0x04003152 RID: 12626
			internal byte[] Footer;

			// Token: 0x04003153 RID: 12627
			internal AsyncOperation AsyncOp;

			// Token: 0x04003154 RID: 12628
			internal WebRequest Request;

			// Token: 0x04003155 RID: 12629
			internal CompletionDelegate UploadCompletionDelegate;

			// Token: 0x04003156 RID: 12630
			internal CompletionDelegate DownloadCompletionDelegate;

			// Token: 0x04003157 RID: 12631
			internal Stream ReadStream;

			// Token: 0x04003158 RID: 12632
			internal long Length;

			// Token: 0x04003159 RID: 12633
			internal int Offset;

			// Token: 0x0400315A RID: 12634
			internal WebClient.ProgressData Progress;
		}

		// Token: 0x02000727 RID: 1831
		private class WebClientWriteStream : Stream
		{
			// Token: 0x06004145 RID: 16709 RVA: 0x0010FA63 File Offset: 0x0010DC63
			public WebClientWriteStream(Stream stream, WebRequest request, WebClient webClient)
			{
				this.m_request = request;
				this.m_stream = stream;
				this.m_WebClient = webClient;
			}

			// Token: 0x17000EF5 RID: 3829
			// (get) Token: 0x06004146 RID: 16710 RVA: 0x0010FA80 File Offset: 0x0010DC80
			public override bool CanRead
			{
				get
				{
					return this.m_stream.CanRead;
				}
			}

			// Token: 0x17000EF6 RID: 3830
			// (get) Token: 0x06004147 RID: 16711 RVA: 0x0010FA8D File Offset: 0x0010DC8D
			public override bool CanSeek
			{
				get
				{
					return this.m_stream.CanSeek;
				}
			}

			// Token: 0x17000EF7 RID: 3831
			// (get) Token: 0x06004148 RID: 16712 RVA: 0x0010FA9A File Offset: 0x0010DC9A
			public override bool CanWrite
			{
				get
				{
					return this.m_stream.CanWrite;
				}
			}

			// Token: 0x17000EF8 RID: 3832
			// (get) Token: 0x06004149 RID: 16713 RVA: 0x0010FAA7 File Offset: 0x0010DCA7
			public override bool CanTimeout
			{
				get
				{
					return this.m_stream.CanTimeout;
				}
			}

			// Token: 0x17000EF9 RID: 3833
			// (get) Token: 0x0600414A RID: 16714 RVA: 0x0010FAB4 File Offset: 0x0010DCB4
			// (set) Token: 0x0600414B RID: 16715 RVA: 0x0010FAC1 File Offset: 0x0010DCC1
			public override int ReadTimeout
			{
				get
				{
					return this.m_stream.ReadTimeout;
				}
				set
				{
					this.m_stream.ReadTimeout = value;
				}
			}

			// Token: 0x17000EFA RID: 3834
			// (get) Token: 0x0600414C RID: 16716 RVA: 0x0010FACF File Offset: 0x0010DCCF
			// (set) Token: 0x0600414D RID: 16717 RVA: 0x0010FADC File Offset: 0x0010DCDC
			public override int WriteTimeout
			{
				get
				{
					return this.m_stream.WriteTimeout;
				}
				set
				{
					this.m_stream.WriteTimeout = value;
				}
			}

			// Token: 0x17000EFB RID: 3835
			// (get) Token: 0x0600414E RID: 16718 RVA: 0x0010FAEA File Offset: 0x0010DCEA
			public override long Length
			{
				get
				{
					return this.m_stream.Length;
				}
			}

			// Token: 0x17000EFC RID: 3836
			// (get) Token: 0x0600414F RID: 16719 RVA: 0x0010FAF7 File Offset: 0x0010DCF7
			// (set) Token: 0x06004150 RID: 16720 RVA: 0x0010FB04 File Offset: 0x0010DD04
			public override long Position
			{
				get
				{
					return this.m_stream.Position;
				}
				set
				{
					this.m_stream.Position = value;
				}
			}

			// Token: 0x06004151 RID: 16721 RVA: 0x0010FB12 File Offset: 0x0010DD12
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
			{
				return this.m_stream.BeginRead(buffer, offset, size, callback, state);
			}

			// Token: 0x06004152 RID: 16722 RVA: 0x0010FB26 File Offset: 0x0010DD26
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
			{
				return this.m_stream.BeginWrite(buffer, offset, size, callback, state);
			}

			// Token: 0x06004153 RID: 16723 RVA: 0x0010FB3C File Offset: 0x0010DD3C
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (disposing)
					{
						this.m_stream.Close();
						this.m_WebClient.GetWebResponse(this.m_request).Close();
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x06004154 RID: 16724 RVA: 0x0010FB88 File Offset: 0x0010DD88
			public override int EndRead(IAsyncResult result)
			{
				return this.m_stream.EndRead(result);
			}

			// Token: 0x06004155 RID: 16725 RVA: 0x0010FB96 File Offset: 0x0010DD96
			public override void EndWrite(IAsyncResult result)
			{
				this.m_stream.EndWrite(result);
			}

			// Token: 0x06004156 RID: 16726 RVA: 0x0010FBA4 File Offset: 0x0010DDA4
			public override void Flush()
			{
				this.m_stream.Flush();
			}

			// Token: 0x06004157 RID: 16727 RVA: 0x0010FBB1 File Offset: 0x0010DDB1
			public override int Read(byte[] buffer, int offset, int count)
			{
				return this.m_stream.Read(buffer, offset, count);
			}

			// Token: 0x06004158 RID: 16728 RVA: 0x0010FBC1 File Offset: 0x0010DDC1
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this.m_stream.Seek(offset, origin);
			}

			// Token: 0x06004159 RID: 16729 RVA: 0x0010FBD0 File Offset: 0x0010DDD0
			public override void SetLength(long value)
			{
				this.m_stream.SetLength(value);
			}

			// Token: 0x0600415A RID: 16730 RVA: 0x0010FBDE File Offset: 0x0010DDDE
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.m_stream.Write(buffer, offset, count);
			}

			// Token: 0x0400315B RID: 12635
			private WebRequest m_request;

			// Token: 0x0400315C RID: 12636
			private Stream m_stream;

			// Token: 0x0400315D RID: 12637
			private WebClient m_WebClient;
		}
	}
}
