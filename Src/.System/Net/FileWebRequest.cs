using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Net
{
	/// <summary>Provides a file system implementation of the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x020000E6 RID: 230
	[Serializable]
	public class FileWebRequest : WebRequest, ISerializable
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x0002B438 File Offset: 0x00029638
		internal FileWebRequest(Uri uri)
		{
			if (uri.Scheme != Uri.UriSchemeFile)
			{
				throw new ArgumentOutOfRangeException("uri");
			}
			this.m_uri = uri;
			this.m_fileAccess = FileAccess.Read;
			this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.FileWebRequest);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.FileWebRequest" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the new <see cref="T:System.Net.FileWebRequest" /> object.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.FileWebRequest" /> object.</param>
		// Token: 0x060007C9 RID: 1993 RVA: 0x0002B494 File Offset: 0x00029694
		[Obsolete("Serialization is obsoleted for this type. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected FileWebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
			this.m_headers = (WebHeaderCollection)serializationInfo.GetValue("headers", typeof(WebHeaderCollection));
			this.m_proxy = (IWebProxy)serializationInfo.GetValue("proxy", typeof(IWebProxy));
			this.m_uri = (Uri)serializationInfo.GetValue("uri", typeof(Uri));
			this.m_connectionGroupName = serializationInfo.GetString("connectionGroupName");
			this.m_method = serializationInfo.GetString("method");
			this.m_contentLength = serializationInfo.GetInt64("contentLength");
			this.m_timeout = serializationInfo.GetInt32("timeout");
			this.m_fileAccess = (FileAccess)serializationInfo.GetInt32("fileAccess");
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the required data to serialize the <see cref="T:System.Net.FileWebRequest" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized data for the <see cref="T:System.Net.FileWebRequest" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Net.FileWebRequest" />.</param>
		// Token: 0x060007CA RID: 1994 RVA: 0x0002B574 File Offset: 0x00029774
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x060007CB RID: 1995 RVA: 0x0002B580 File Offset: 0x00029780
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("headers", this.m_headers, typeof(WebHeaderCollection));
			serializationInfo.AddValue("proxy", this.m_proxy, typeof(IWebProxy));
			serializationInfo.AddValue("uri", this.m_uri, typeof(Uri));
			serializationInfo.AddValue("connectionGroupName", this.m_connectionGroupName);
			serializationInfo.AddValue("method", this.m_method);
			serializationInfo.AddValue("contentLength", this.m_contentLength);
			serializationInfo.AddValue("timeout", this.m_timeout);
			serializationInfo.AddValue("fileAccess", this.m_fileAccess);
			serializationInfo.AddValue("preauthenticate", false);
			base.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0002B64C File Offset: 0x0002984C
		internal bool Aborted
		{
			get
			{
				return this.m_Aborted != 0;
			}
		}

		/// <summary>Gets or sets the name of the connection group for the request. This property is reserved for future use.</summary>
		/// <returns>The name of the connection group for the request.</returns>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x0002B657 File Offset: 0x00029857
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x0002B65F File Offset: 0x0002985F
		public override string ConnectionGroupName
		{
			get
			{
				return this.m_connectionGroupName;
			}
			set
			{
				this.m_connectionGroupName = value;
			}
		}

		/// <summary>Gets or sets the content length of the data being sent.</summary>
		/// <returns>The number of bytes of request data being sent.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.FileWebRequest.ContentLength" /> is less than 0.</exception>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0002B668 File Offset: 0x00029868
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x0002B670 File Offset: 0x00029870
		public override long ContentLength
		{
			get
			{
				return this.m_contentLength;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentException(SR.GetString("net_clsmall"), "value");
				}
				this.m_contentLength = value;
			}
		}

		/// <summary>Gets or sets the content type of the data being sent. This property is reserved for future use.</summary>
		/// <returns>The content type of the data being sent.</returns>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0002B693 File Offset: 0x00029893
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x0002B6A5 File Offset: 0x000298A5
		public override string ContentType
		{
			get
			{
				return this.m_headers["Content-Type"];
			}
			set
			{
				this.m_headers["Content-Type"] = value;
			}
		}

		/// <summary>Gets or sets the credentials that are associated with this request. This property is reserved for future use.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> that contains the authentication credentials that are associated with this request. The default is <see langword="null" />.</returns>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x0002B6B8 File Offset: 0x000298B8
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x0002B6C0 File Offset: 0x000298C0
		public override ICredentials Credentials
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

		/// <summary>Gets a collection of the name/value pairs that are associated with the request. This property is reserved for future use.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains header name/value pairs associated with this request.</returns>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x0002B6C9 File Offset: 0x000298C9
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.m_headers;
			}
		}

		/// <summary>Gets or sets the protocol method used for the request. This property is reserved for future use.</summary>
		/// <returns>The protocol method to use in this request.</returns>
		/// <exception cref="T:System.ArgumentException">The method is invalid.  
		/// -or-
		///  The method is not supported.  
		/// -or-
		///  Multiple methods were specified.</exception>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0002B6D1 File Offset: 0x000298D1
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0002B6D9 File Offset: 0x000298D9
		public override string Method
		{
			get
			{
				return this.m_method;
			}
			set
			{
				if (ValidationHelper.IsBlankString(value))
				{
					throw new ArgumentException(SR.GetString("net_badmethod"), "value");
				}
				this.m_method = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to preauthenticate a request. This property is reserved for future use.</summary>
		/// <returns>
		///   <see langword="true" /> to preauthenticate; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0002B6FF File Offset: 0x000298FF
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0002B707 File Offset: 0x00029907
		public override bool PreAuthenticate
		{
			get
			{
				return this.m_preauthenticate;
			}
			set
			{
				this.m_preauthenticate = true;
			}
		}

		/// <summary>Gets or sets the network proxy to use for this request. This property is reserved for future use.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that indicates the network proxy to use for this request.</returns>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0002B710 File Offset: 0x00029910
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0002B718 File Offset: 0x00029918
		public override IWebProxy Proxy
		{
			get
			{
				return this.m_proxy;
			}
			set
			{
				this.m_proxy = value;
			}
		}

		/// <summary>Gets or sets the length of time until the request times out.</summary>
		/// <returns>The time, in milliseconds, until the request times out, or the value <see cref="F:System.Threading.Timeout.Infinite" /> to indicate that the request does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0002B721 File Offset: 0x00029921
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x0002B729 File Offset: 0x00029929
		public override int Timeout
		{
			get
			{
				return this.m_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_ge_zero"));
				}
				this.m_timeout = value;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the request.</returns>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0002B74F File Offset: 0x0002994F
		public override Uri RequestUri
		{
			get
			{
				return this.m_uri;
			}
		}

		/// <summary>Begins an asynchronous request for a <see cref="T:System.IO.Stream" /> object to use to write data.</summary>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.FileWebRequest.Method" /> property is <c>GET</c> and the application writes to the stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is being used by a previous call to <see cref="M:System.Net.FileWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.ApplicationException">No write stream is available.</exception>
		/// <exception cref="T:System.Net.WebException">The <see cref="T:System.Net.FileWebRequest" /> was aborted.</exception>
		// Token: 0x060007DF RID: 2015 RVA: 0x0002B758 File Offset: 0x00029958
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			bool flag = true;
			try
			{
				if (this.Aborted)
				{
					throw ExceptionHelper.RequestAbortedException;
				}
				if (!this.CanGetRequestStream())
				{
					Exception ex = new ProtocolViolationException(SR.GetString("net_nouploadonget"));
					throw ex;
				}
				if (this.m_response != null)
				{
					Exception ex2 = new InvalidOperationException(SR.GetString("net_reqsubmitted"));
					throw ex2;
				}
				lock (this)
				{
					if (this.m_writePending)
					{
						Exception ex3 = new InvalidOperationException(SR.GetString("net_repcall"));
						throw ex3;
					}
					this.m_writePending = true;
				}
				this.m_ReadAResult = new LazyAsyncResult(this, state, callback);
				ThreadPool.QueueUserWorkItem(FileWebRequest.s_GetRequestStreamCallback, this.m_ReadAResult);
			}
			catch (Exception ex4)
			{
				flag = false;
				if (Logging.On)
				{
					Logging.Exception(Logging.Web, this, "BeginGetRequestStream", ex4);
				}
				throw;
			}
			finally
			{
				if (FrameworkEventSource.Log.IsEnabled())
				{
					base.LogBeginGetRequestStream(flag, false);
				}
			}
			return this.m_ReadAResult;
		}

		/// <summary>Begins an asynchronous request for a file system resource.</summary>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <exception cref="T:System.InvalidOperationException">The stream is already in use by a previous call to <see cref="M:System.Net.FileWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.Net.WebException">The <see cref="T:System.Net.FileWebRequest" /> was aborted.</exception>
		// Token: 0x060007E0 RID: 2016 RVA: 0x0002B86C File Offset: 0x00029A6C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			bool flag = true;
			try
			{
				if (this.Aborted)
				{
					throw ExceptionHelper.RequestAbortedException;
				}
				lock (this)
				{
					if (this.m_readPending)
					{
						Exception ex = new InvalidOperationException(SR.GetString("net_repcall"));
						throw ex;
					}
					this.m_readPending = true;
				}
				this.m_WriteAResult = new LazyAsyncResult(this, state, callback);
				ThreadPool.QueueUserWorkItem(FileWebRequest.s_GetResponseCallback, this.m_WriteAResult);
			}
			catch (Exception ex2)
			{
				flag = false;
				if (Logging.On)
				{
					Logging.Exception(Logging.Web, this, "BeginGetResponse", ex2);
				}
				throw;
			}
			finally
			{
				if (FrameworkEventSource.Log.IsEnabled())
				{
					base.LogBeginGetResponse(flag, false);
				}
			}
			return this.m_WriteAResult;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0002B948 File Offset: 0x00029B48
		private bool CanGetRequestStream()
		{
			return !KnownHttpVerb.Parse(this.m_method).ContentBodyNotAllowed;
		}

		/// <summary>Ends an asynchronous request for a <see cref="T:System.IO.Stream" /> instance that the application uses to write data.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references the pending request for a stream.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that the application uses to write data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		// Token: 0x060007E2 RID: 2018 RVA: 0x0002B960 File Offset: 0x00029B60
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			bool flag = false;
			Stream stream;
			try
			{
				LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
				if (asyncResult == null || lazyAsyncResult == null)
				{
					Exception ex = ((asyncResult == null) ? new ArgumentNullException("asyncResult") : new ArgumentException(SR.GetString("InvalidAsyncResult"), "asyncResult"));
					throw ex;
				}
				object obj = lazyAsyncResult.InternalWaitForCompletion();
				if (obj is Exception)
				{
					throw (Exception)obj;
				}
				stream = (Stream)obj;
				this.m_writePending = false;
				flag = true;
			}
			catch (Exception ex2)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.Web, this, "EndGetRequestStream", ex2);
				}
				throw;
			}
			finally
			{
				if (FrameworkEventSource.Log.IsEnabled())
				{
					base.LogEndGetRequestStream(flag, false);
				}
			}
			return stream;
		}

		/// <summary>Ends an asynchronous request for a file system resource.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references the pending request for a response.</param>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the file system resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		// Token: 0x060007E3 RID: 2019 RVA: 0x0002BA1C File Offset: 0x00029C1C
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			bool flag = false;
			WebResponse webResponse;
			try
			{
				LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
				if (asyncResult == null || lazyAsyncResult == null)
				{
					Exception ex = ((asyncResult == null) ? new ArgumentNullException("asyncResult") : new ArgumentException(SR.GetString("InvalidAsyncResult"), "asyncResult"));
					throw ex;
				}
				object obj = lazyAsyncResult.InternalWaitForCompletion();
				if (obj is Exception)
				{
					throw (Exception)obj;
				}
				webResponse = (WebResponse)obj;
				this.m_readPending = false;
				flag = true;
			}
			catch (Exception ex2)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.Web, this, "EndGetResponse", ex2);
				}
				throw;
			}
			finally
			{
				if (FrameworkEventSource.Log.IsEnabled())
				{
					base.LogEndGetResponse(flag, false, 0);
				}
			}
			return webResponse;
		}

		/// <summary>Returns a <see cref="T:System.IO.Stream" /> object for writing data to the file system resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> for writing data to the file system resource.</returns>
		/// <exception cref="T:System.Net.WebException">The request times out.</exception>
		// Token: 0x060007E4 RID: 2020 RVA: 0x0002BAD8 File Offset: 0x00029CD8
		public override Stream GetRequestStream()
		{
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.BeginGetRequestStream(null, null);
				if (this.Timeout != -1 && !asyncResult.IsCompleted && (!asyncResult.AsyncWaitHandle.WaitOne(this.Timeout, false) || !asyncResult.IsCompleted))
				{
					if (this.m_stream != null)
					{
						this.m_stream.Close();
					}
					Exception ex = new WebException(NetRes.GetWebStatusString(WebExceptionStatus.Timeout), WebExceptionStatus.Timeout);
					throw ex;
				}
			}
			catch (Exception ex2)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.Web, this, "GetRequestStream", ex2);
				}
				throw;
			}
			finally
			{
			}
			return this.EndGetRequestStream(asyncResult);
		}

		/// <summary>Returns a response to a file system request.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the file system resource.</returns>
		/// <exception cref="T:System.Net.WebException">The request timed out.</exception>
		// Token: 0x060007E5 RID: 2021 RVA: 0x0002BB80 File Offset: 0x00029D80
		public override WebResponse GetResponse()
		{
			this.m_syncHint = true;
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.BeginGetResponse(null, null);
				if (this.Timeout != -1 && !asyncResult.IsCompleted && (!asyncResult.AsyncWaitHandle.WaitOne(this.Timeout, false) || !asyncResult.IsCompleted))
				{
					if (this.m_response != null)
					{
						this.m_response.Close();
					}
					Exception ex = new WebException(NetRes.GetWebStatusString(WebExceptionStatus.Timeout), WebExceptionStatus.Timeout);
					throw ex;
				}
			}
			catch (Exception ex2)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.Web, this, "GetResponse", ex2);
				}
				throw;
			}
			finally
			{
			}
			return this.EndGetResponse(asyncResult);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0002BC30 File Offset: 0x00029E30
		private static void GetRequestStreamCallback(object state)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)state;
			FileWebRequest fileWebRequest = (FileWebRequest)lazyAsyncResult.AsyncObject;
			try
			{
				if (fileWebRequest.m_stream == null)
				{
					fileWebRequest.m_stream = new FileWebStream(fileWebRequest, fileWebRequest.m_uri.LocalPath, FileMode.Create, FileAccess.Write, FileShare.Read);
					fileWebRequest.m_fileAccess = FileAccess.Write;
					fileWebRequest.m_writing = true;
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = new WebException(ex.Message, ex);
				lazyAsyncResult.InvokeCallback(ex2);
				return;
			}
			lazyAsyncResult.InvokeCallback(fileWebRequest.m_stream);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0002BCB8 File Offset: 0x00029EB8
		private static void GetResponseCallback(object state)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)state;
			FileWebRequest fileWebRequest = (FileWebRequest)lazyAsyncResult.AsyncObject;
			if (fileWebRequest.m_writePending || fileWebRequest.m_writing)
			{
				FileWebRequest fileWebRequest2 = fileWebRequest;
				lock (fileWebRequest2)
				{
					if (fileWebRequest.m_writePending || fileWebRequest.m_writing)
					{
						fileWebRequest.m_readerEvent = new ManualResetEvent(false);
					}
				}
			}
			if (fileWebRequest.m_readerEvent != null)
			{
				fileWebRequest.m_readerEvent.WaitOne();
			}
			try
			{
				if (fileWebRequest.m_response == null)
				{
					fileWebRequest.m_response = new FileWebResponse(fileWebRequest, fileWebRequest.m_uri, fileWebRequest.m_fileAccess, !fileWebRequest.m_syncHint);
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = new WebException(ex.Message, ex);
				lazyAsyncResult.InvokeCallback(ex2);
				return;
			}
			lazyAsyncResult.InvokeCallback(fileWebRequest.m_response);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0002BDA0 File Offset: 0x00029FA0
		internal void UnblockReader()
		{
			lock (this)
			{
				if (this.m_readerEvent != null)
				{
					this.m_readerEvent.Set();
				}
			}
			this.m_writing = false;
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Default credentials are not supported for file Uniform Resource Identifiers (URIs).</exception>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0002BDF0 File Offset: 0x00029FF0
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x0002BDF7 File Offset: 0x00029FF7
		public override bool UseDefaultCredentials
		{
			get
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
		}

		/// <summary>Cancels a request to an Internet resource.</summary>
		// Token: 0x060007EB RID: 2027 RVA: 0x0002BE00 File Offset: 0x0002A000
		public override void Abort()
		{
			if (Logging.On)
			{
				Logging.PrintWarning(Logging.Web, NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled));
			}
			try
			{
				if (Interlocked.Increment(ref this.m_Aborted) == 1)
				{
					LazyAsyncResult readAResult = this.m_ReadAResult;
					LazyAsyncResult writeAResult = this.m_WriteAResult;
					WebException ex = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
					Stream stream = this.m_stream;
					if (readAResult != null && !readAResult.IsCompleted)
					{
						readAResult.InvokeCallback(ex);
					}
					if (writeAResult != null && !writeAResult.IsCompleted)
					{
						writeAResult.InvokeCallback(ex);
					}
					if (stream != null)
					{
						if (stream is ICloseEx)
						{
							((ICloseEx)stream).CloseEx(CloseExState.Abort);
						}
						else
						{
							stream.Close();
						}
					}
					if (this.m_response != null)
					{
						((ICloseEx)this.m_response).CloseEx(CloseExState.Abort);
					}
				}
			}
			catch (Exception ex2)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.Web, this, "Abort", ex2);
				}
				throw;
			}
			finally
			{
			}
		}

		// Token: 0x04000D34 RID: 3380
		private static WaitCallback s_GetRequestStreamCallback = new WaitCallback(FileWebRequest.GetRequestStreamCallback);

		// Token: 0x04000D35 RID: 3381
		private static WaitCallback s_GetResponseCallback = new WaitCallback(FileWebRequest.GetResponseCallback);

		// Token: 0x04000D36 RID: 3382
		private static ContextCallback s_WrappedGetRequestStreamCallback = new ContextCallback(FileWebRequest.GetRequestStreamCallback);

		// Token: 0x04000D37 RID: 3383
		private static ContextCallback s_WrappedResponseCallback = new ContextCallback(FileWebRequest.GetResponseCallback);

		// Token: 0x04000D38 RID: 3384
		private string m_connectionGroupName;

		// Token: 0x04000D39 RID: 3385
		private long m_contentLength;

		// Token: 0x04000D3A RID: 3386
		private ICredentials m_credentials;

		// Token: 0x04000D3B RID: 3387
		private FileAccess m_fileAccess;

		// Token: 0x04000D3C RID: 3388
		private WebHeaderCollection m_headers;

		// Token: 0x04000D3D RID: 3389
		private string m_method = "GET";

		// Token: 0x04000D3E RID: 3390
		private bool m_preauthenticate;

		// Token: 0x04000D3F RID: 3391
		private IWebProxy m_proxy;

		// Token: 0x04000D40 RID: 3392
		private ManualResetEvent m_readerEvent;

		// Token: 0x04000D41 RID: 3393
		private bool m_readPending;

		// Token: 0x04000D42 RID: 3394
		private WebResponse m_response;

		// Token: 0x04000D43 RID: 3395
		private Stream m_stream;

		// Token: 0x04000D44 RID: 3396
		private bool m_syncHint;

		// Token: 0x04000D45 RID: 3397
		private int m_timeout = 100000;

		// Token: 0x04000D46 RID: 3398
		private Uri m_uri;

		// Token: 0x04000D47 RID: 3399
		private bool m_writePending;

		// Token: 0x04000D48 RID: 3400
		private bool m_writing;

		// Token: 0x04000D49 RID: 3401
		private LazyAsyncResult m_WriteAResult;

		// Token: 0x04000D4A RID: 3402
		private LazyAsyncResult m_ReadAResult;

		// Token: 0x04000D4B RID: 3403
		private int m_Aborted;
	}
}
