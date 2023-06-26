using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Net
{
	/// <summary>Represents a response to a request being handled by an <see cref="T:System.Net.HttpListener" /> object.</summary>
	// Token: 0x02000100 RID: 256
	public sealed class HttpListenerResponse : IDisposable
	{
		// Token: 0x0600095F RID: 2399 RVA: 0x00034958 File Offset: 0x00032B58
		internal HttpListenerResponse()
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, this, ".ctor", "");
			}
			this.m_NativeResponse = default(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE);
			this.m_WebHeaders = new WebHeaderCollection(WebHeaderCollectionType.HttpListenerResponse);
			this.m_BoundaryType = BoundaryType.None;
			this.m_NativeResponse.StatusCode = 200;
			this.m_NativeResponse.Version.MajorVersion = 1;
			this.m_NativeResponse.Version.MinorVersion = 1;
			this.m_KeepAlive = true;
			this.m_ResponseState = HttpListenerResponse.ResponseState.Created;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x000349E6 File Offset: 0x00032BE6
		internal HttpListenerResponse(HttpListenerContext httpContext)
			: this()
		{
			if (Logging.On)
			{
				Logging.Associate(Logging.HttpListener, this, httpContext);
			}
			this.m_HttpContext = httpContext;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00034A08 File Offset: 0x00032C08
		private HttpListenerContext HttpListenerContext
		{
			get
			{
				return this.m_HttpContext;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00034A10 File Offset: 0x00032C10
		private HttpListenerRequest HttpListenerRequest
		{
			get
			{
				return this.HttpListenerContext.Request;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.Encoding" /> for this response's <see cref="P:System.Net.HttpListenerResponse.OutputStream" />.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object suitable for use with the data in the <see cref="P:System.Net.HttpListenerResponse.OutputStream" /> property, or <see langword="null" /> if no encoding is specified.</returns>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00034A1D File Offset: 0x00032C1D
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x00034A25 File Offset: 0x00032C25
		public Encoding ContentEncoding
		{
			get
			{
				return this.m_ContentEncoding;
			}
			set
			{
				this.m_ContentEncoding = value;
			}
		}

		/// <summary>Gets or sets the MIME type of the content returned.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the text of the response's <see langword="Content-Type" /> header.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is an empty string ("").</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00034A2E File Offset: 0x00032C2E
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x00034A3D File Offset: 0x00032C3D
		public string ContentType
		{
			get
			{
				return this.Headers[HttpResponseHeader.ContentType];
			}
			set
			{
				this.CheckDisposed();
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(HttpResponseHeader.ContentType);
					return;
				}
				this.Headers.Set(HttpResponseHeader.ContentType, value);
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to which a response can be written.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object to which a response can be written.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00034A69 File Offset: 0x00032C69
		public Stream OutputStream
		{
			get
			{
				this.CheckDisposed();
				this.EnsureResponseStream();
				return this.m_ResponseStream;
			}
		}

		/// <summary>Gets or sets the value of the HTTP <see langword="Location" /> header in this response.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the absolute URL to be sent to the client in the <see langword="Location" /> header.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is an empty string ("").</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00034A7D File Offset: 0x00032C7D
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x00034A8C File Offset: 0x00032C8C
		public string RedirectLocation
		{
			get
			{
				return this.Headers[HttpResponseHeader.Location];
			}
			set
			{
				this.CheckDisposed();
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(HttpResponseHeader.Location);
					return;
				}
				this.Headers.Set(HttpResponseHeader.Location, value);
			}
		}

		/// <summary>Gets or sets the HTTP status code to be returned to the client.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the HTTP status code for the requested resource. The default is <see cref="F:System.Net.HttpStatusCode.OK" />, indicating that the server successfully processed the client's request and included the requested resource in the response body.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The value specified for a set operation is not valid. Valid values are between 100 and 999 inclusive.</exception>
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00034AB8 File Offset: 0x00032CB8
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x00034AC5 File Offset: 0x00032CC5
		public int StatusCode
		{
			get
			{
				return (int)this.m_NativeResponse.StatusCode;
			}
			set
			{
				this.CheckDisposed();
				if (value < 100 || value > 999)
				{
					throw new ProtocolViolationException(SR.GetString("net_invalidstatus"));
				}
				this.m_NativeResponse.StatusCode = (ushort)value;
			}
		}

		/// <summary>Gets or sets a text description of the HTTP status code returned to the client.</summary>
		/// <returns>The text description of the HTTP status code returned to the client. The default is the RFC 2616 description for the <see cref="P:System.Net.HttpListenerResponse.StatusCode" /> property value, or an empty string ("") if an RFC 2616 description does not exist.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation contains non-printable characters.</exception>
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00034AF7 File Offset: 0x00032CF7
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x00034B2C File Offset: 0x00032D2C
		public string StatusDescription
		{
			get
			{
				if (this.m_StatusDescription == null)
				{
					this.m_StatusDescription = HttpStatusDescription.Get(this.StatusCode);
				}
				if (this.m_StatusDescription == null)
				{
					this.m_StatusDescription = string.Empty;
				}
				return this.m_StatusDescription;
			}
			set
			{
				this.CheckDisposed();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				for (int i = 0; i < value.Length; i++)
				{
					char c = 'ÿ' & value[i];
					if ((c <= '\u001f' && c != '\t') || c == '\u007f')
					{
						throw new ArgumentException(SR.GetString("net_WebHeaderInvalidControlChars"), "name");
					}
				}
				this.m_StatusDescription = value;
			}
		}

		/// <summary>Gets or sets the collection of cookies returned with the response.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains cookies to accompany the response. The collection is empty if no cookies have been added to the response.</returns>
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00034B98 File Offset: 0x00032D98
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x00034BB4 File Offset: 0x00032DB4
		public CookieCollection Cookies
		{
			get
			{
				if (this.m_Cookies == null)
				{
					this.m_Cookies = new CookieCollection(false);
				}
				return this.m_Cookies;
			}
			set
			{
				this.m_Cookies = value;
			}
		}

		/// <summary>Copies properties from the specified <see cref="T:System.Net.HttpListenerResponse" /> to this response.</summary>
		/// <param name="templateResponse">The <see cref="T:System.Net.HttpListenerResponse" /> instance to copy.</param>
		// Token: 0x06000970 RID: 2416 RVA: 0x00034BC0 File Offset: 0x00032DC0
		public void CopyFrom(HttpListenerResponse templateResponse)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, this, "CopyFrom", "templateResponse#" + ValidationHelper.HashString(templateResponse));
			}
			this.m_NativeResponse = default(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE);
			this.m_ResponseState = HttpListenerResponse.ResponseState.Created;
			this.m_WebHeaders = templateResponse.m_WebHeaders;
			this.m_BoundaryType = templateResponse.m_BoundaryType;
			this.m_ContentLength = templateResponse.m_ContentLength;
			this.m_NativeResponse.StatusCode = templateResponse.m_NativeResponse.StatusCode;
			this.m_NativeResponse.Version.MajorVersion = templateResponse.m_NativeResponse.Version.MajorVersion;
			this.m_NativeResponse.Version.MinorVersion = templateResponse.m_NativeResponse.Version.MinorVersion;
			this.m_StatusDescription = templateResponse.m_StatusDescription;
			this.m_KeepAlive = templateResponse.m_KeepAlive;
		}

		/// <summary>Gets or sets whether the response uses chunked transfer encoding.</summary>
		/// <returns>
		///   <see langword="true" /> if the response is set to use chunked transfer encoding; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00034C99 File Offset: 0x00032E99
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x00034CA4 File Offset: 0x00032EA4
		public bool SendChunked
		{
			get
			{
				return this.EntitySendFormat == EntitySendFormat.Chunked;
			}
			set
			{
				if (value)
				{
					this.EntitySendFormat = EntitySendFormat.Chunked;
					return;
				}
				this.EntitySendFormat = EntitySendFormat.ContentLength;
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00034CB8 File Offset: 0x00032EB8
		private bool CanSendResponseBody(int responseCode)
		{
			for (int i = 0; i < HttpListenerResponse.s_NoResponseBody.Length; i++)
			{
				if (responseCode == HttpListenerResponse.s_NoResponseBody[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00034CE4 File Offset: 0x00032EE4
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x00034CEC File Offset: 0x00032EEC
		internal EntitySendFormat EntitySendFormat
		{
			get
			{
				return (EntitySendFormat)this.m_BoundaryType;
			}
			set
			{
				this.CheckDisposed();
				if (this.m_ResponseState >= HttpListenerResponse.ResponseState.SentHeaders)
				{
					throw new InvalidOperationException(SR.GetString("net_rspsubmitted"));
				}
				if (value == EntitySendFormat.Chunked && this.HttpListenerRequest.ProtocolVersion.Minor == 0)
				{
					throw new ProtocolViolationException(SR.GetString("net_nochunkuploadonhttp10"));
				}
				this.m_BoundaryType = (BoundaryType)value;
				if (value != EntitySendFormat.ContentLength)
				{
					this.m_ContentLength = -1L;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the server requests a persistent connection.</summary>
		/// <returns>
		///   <see langword="true" /> if the server requests a persistent connection; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00034D50 File Offset: 0x00032F50
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x00034D58 File Offset: 0x00032F58
		public bool KeepAlive
		{
			get
			{
				return this.m_KeepAlive;
			}
			set
			{
				this.CheckDisposed();
				this.m_KeepAlive = value;
			}
		}

		/// <summary>Gets or sets the collection of header name/value pairs returned by the server.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> instance that contains all the explicitly set HTTP headers to be included in the response.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.WebHeaderCollection" /> instance specified for a set operation is not valid for a response.</exception>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00034D67 File Offset: 0x00032F67
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x00034D70 File Offset: 0x00032F70
		public WebHeaderCollection Headers
		{
			get
			{
				return this.m_WebHeaders;
			}
			set
			{
				this.m_WebHeaders.Clear();
				foreach (string text in value.AllKeys)
				{
					this.m_WebHeaders.Add(text, value[text]);
				}
			}
		}

		/// <summary>Adds the specified header and value to the HTTP headers for this response.</summary>
		/// <param name="name">The name of the HTTP header to set.</param>
		/// <param name="value">The value for the <paramref name="name" /> header.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" /> or an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentException">You are not allowed to specify a value for the specified header.  
		///  -or-  
		///  <paramref name="name" /> or <paramref name="value" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65,535 characters.</exception>
		// Token: 0x0600097A RID: 2426 RVA: 0x00034DB4 File Offset: 0x00032FB4
		public void AddHeader(string name, string value)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, this, "AddHeader", " name=" + name + " value=" + value);
			}
			this.Headers.SetInternal(name, value);
		}

		/// <summary>Appends a value to the specified HTTP header to be sent with this response.</summary>
		/// <param name="name">The name of the HTTP header to append <paramref name="value" /> to.</param>
		/// <param name="value">The value to append to the <paramref name="name" /> header.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is <see langword="null" /> or an empty string ("").  
		/// -or-  
		/// You are not allowed to specify a value for the specified header.  
		/// -or-  
		/// <paramref name="name" /> or <paramref name="value" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65,535 characters.</exception>
		// Token: 0x0600097B RID: 2427 RVA: 0x00034DEB File Offset: 0x00032FEB
		public void AppendHeader(string name, string value)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, this, "AppendHeader", " name=" + name + " value=" + value);
			}
			this.Headers.Add(name, value);
		}

		/// <summary>Configures the response to redirect the client to the specified URL.</summary>
		/// <param name="url">The URL that the client should use to locate the requested resource.</param>
		// Token: 0x0600097C RID: 2428 RVA: 0x00034E24 File Offset: 0x00033024
		public void Redirect(string url)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, this, "Redirect", " url=" + url);
			}
			this.Headers.SetInternal(HttpResponseHeader.Location, url);
			this.StatusCode = 302;
			this.StatusDescription = HttpStatusDescription.Get(this.StatusCode);
		}

		/// <summary>Adds the specified <see cref="T:System.Net.Cookie" /> to the collection of cookies for this response.</summary>
		/// <param name="cookie">The <see cref="T:System.Net.Cookie" /> to add to the collection to be sent with this response.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is <see langword="null" />.</exception>
		// Token: 0x0600097D RID: 2429 RVA: 0x00034E80 File Offset: 0x00033080
		public void AppendCookie(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, this, "AppendCookie", " cookie#" + ValidationHelper.HashString(cookie));
			}
			this.Cookies.Add(cookie);
		}

		/// <summary>Adds or updates a <see cref="T:System.Net.Cookie" /> in the collection of cookies sent with this response.</summary>
		/// <param name="cookie">A <see cref="T:System.Net.Cookie" /> for this response.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The cookie already exists in the collection and could not be replaced.</exception>
		// Token: 0x0600097E RID: 2430 RVA: 0x00034ED0 File Offset: 0x000330D0
		public void SetCookie(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			Cookie cookie2 = cookie.Clone();
			int num = this.Cookies.InternalAdd(cookie2, true);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, this, "SetCookie", " cookie#" + ValidationHelper.HashString(cookie));
			}
			if (num != 1)
			{
				throw new ArgumentException(SR.GetString("net_cookie_exists"), "cookie");
			}
		}

		/// <summary>Gets or sets the number of bytes in the body data included in the response.</summary>
		/// <returns>The value of the response's <see langword="Content-Length" /> header.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The response is already being sent.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00034F40 File Offset: 0x00033140
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x00034F48 File Offset: 0x00033148
		public long ContentLength64
		{
			get
			{
				return this.m_ContentLength;
			}
			set
			{
				this.CheckDisposed();
				if (this.m_ResponseState >= HttpListenerResponse.ResponseState.SentHeaders)
				{
					throw new InvalidOperationException(SR.GetString("net_rspsubmitted"));
				}
				if (value >= 0L)
				{
					this.m_ContentLength = value;
					this.m_BoundaryType = BoundaryType.ContentLength;
					return;
				}
				throw new ArgumentOutOfRangeException("value", SR.GetString("net_clsmall"));
			}
		}

		/// <summary>Gets or sets the HTTP version used for the response.</summary>
		/// <returns>A <see cref="T:System.Version" /> object indicating the version of HTTP used when responding to the client. Note that this property is now obsolete.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation does not have its <see cref="P:System.Version.Major" /> property set to 1 or does not have its <see cref="P:System.Version.Minor" /> property set to either 0 or 1.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00034F9C File Offset: 0x0003319C
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x00034FC4 File Offset: 0x000331C4
		public Version ProtocolVersion
		{
			get
			{
				return new Version((int)this.m_NativeResponse.Version.MajorVersion, (int)this.m_NativeResponse.Version.MinorVersion);
			}
			set
			{
				this.CheckDisposed();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Major != 1 || (value.Minor != 0 && value.Minor != 1))
				{
					throw new ArgumentException(SR.GetString("net_wrongversion"), "value");
				}
				this.m_NativeResponse.Version.MajorVersion = (ushort)value.Major;
				this.m_NativeResponse.Version.MinorVersion = (ushort)value.Minor;
			}
		}

		/// <summary>Closes the connection to the client without sending a response.</summary>
		// Token: 0x06000983 RID: 2435 RVA: 0x00035048 File Offset: 0x00033248
		public void Abort()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "abort", "");
			}
			try
			{
				if (this.m_ResponseState < HttpListenerResponse.ResponseState.Closed)
				{
					this.m_ResponseState = HttpListenerResponse.ResponseState.Closed;
					this.HttpListenerContext.Abort();
				}
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "abort", "");
				}
			}
		}

		/// <summary>Returns the specified byte array to the client and releases the resources held by this <see cref="T:System.Net.HttpListenerResponse" /> instance.</summary>
		/// <param name="responseEntity">A <see cref="T:System.Byte" /> array that contains the response to send to the client.</param>
		/// <param name="willBlock">
		///   <see langword="true" /> to block execution while flushing the stream to the client; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="responseEntity" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x06000984 RID: 2436 RVA: 0x000350C0 File Offset: 0x000332C0
		public void Close(byte[] responseEntity, bool willBlock)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Close", " responseEntity=" + ValidationHelper.HashString(responseEntity) + " willBlock=" + willBlock.ToString());
			}
			try
			{
				this.CheckDisposed();
				if (responseEntity == null)
				{
					throw new ArgumentNullException("responseEntity");
				}
				if (this.m_ResponseState < HttpListenerResponse.ResponseState.SentHeaders && this.m_BoundaryType != BoundaryType.Chunked)
				{
					this.ContentLength64 = (long)responseEntity.Length;
				}
				this.EnsureResponseStream();
				if (willBlock)
				{
					try
					{
						this.m_ResponseStream.Write(responseEntity, 0, responseEntity.Length);
						return;
					}
					catch (Win32Exception)
					{
						return;
					}
					finally
					{
						this.m_ResponseStream.Close();
						this.m_ResponseState = HttpListenerResponse.ResponseState.Closed;
						this.HttpListenerContext.Close();
					}
				}
				this.m_ResponseStream.BeginWrite(responseEntity, 0, responseEntity.Length, new AsyncCallback(this.NonBlockingCloseCallback), null);
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "Close", "");
				}
			}
		}

		/// <summary>Sends the response to the client and releases the resources held by this <see cref="T:System.Net.HttpListenerResponse" /> instance.</summary>
		// Token: 0x06000985 RID: 2437 RVA: 0x000351D4 File Offset: 0x000333D4
		public void Close()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Close", "");
			}
			try
			{
				((IDisposable)this).Dispose();
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "Close", "");
				}
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00035234 File Offset: 0x00033434
		private void Dispose(bool disposing)
		{
			if (this.m_ResponseState >= HttpListenerResponse.ResponseState.Closed)
			{
				return;
			}
			this.EnsureResponseStream();
			this.m_ResponseStream.Close();
			this.m_ResponseState = HttpListenerResponse.ResponseState.Closed;
			this.HttpListenerContext.Close();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.HttpListenerResponse" />.</summary>
		// Token: 0x06000987 RID: 2439 RVA: 0x00035263 File Offset: 0x00033463
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00035272 File Offset: 0x00033472
		internal BoundaryType BoundaryType
		{
			get
			{
				return this.m_BoundaryType;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0003527A File Offset: 0x0003347A
		internal bool SentHeaders
		{
			get
			{
				return this.m_ResponseState >= HttpListenerResponse.ResponseState.SentHeaders;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00035288 File Offset: 0x00033488
		internal bool ComputedHeaders
		{
			get
			{
				return this.m_ResponseState >= HttpListenerResponse.ResponseState.ComputedHeaders;
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00035296 File Offset: 0x00033496
		private void EnsureResponseStream()
		{
			if (this.m_ResponseStream == null)
			{
				this.m_ResponseStream = new HttpResponseStream(this.HttpListenerContext);
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000352B4 File Offset: 0x000334B4
		private void NonBlockingCloseCallback(IAsyncResult asyncResult)
		{
			try
			{
				this.m_ResponseStream.EndWrite(asyncResult);
			}
			catch (Win32Exception)
			{
			}
			finally
			{
				this.m_ResponseStream.Close();
				this.HttpListenerContext.Close();
				this.m_ResponseState = HttpListenerResponse.ResponseState.Closed;
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0003530C File Offset: 0x0003350C
		internal unsafe uint SendHeaders(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK* pDataChunk, HttpResponseStreamAsyncResult asyncResult, UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS flags, bool isWebSocketHandshake)
		{
			if (this.StatusCode == 401)
			{
				this.HttpListenerContext.SetAuthenticationHeaders();
			}
			if (Logging.On)
			{
				StringBuilder stringBuilder = new StringBuilder("HttpListenerResponse Headers:\n");
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
			this.m_ResponseState = HttpListenerResponse.ResponseState.SentHeaders;
			List<GCHandle> list = this.SerializeHeaders(ref this.m_NativeResponse.Headers, isWebSocketHandshake);
			uint num;
			try
			{
				if (pDataChunk != null)
				{
					this.m_NativeResponse.EntityChunkCount = 1;
					this.m_NativeResponse.pEntityChunks = pDataChunk;
				}
				else if (asyncResult != null && asyncResult.pDataChunks != null)
				{
					this.m_NativeResponse.EntityChunkCount = asyncResult.dataChunkCount;
					this.m_NativeResponse.pEntityChunks = asyncResult.pDataChunks;
				}
				else
				{
					this.m_NativeResponse.EntityChunkCount = 0;
					this.m_NativeResponse.pEntityChunks = null;
				}
				uint num2;
				if (this.StatusDescription.Length > 0)
				{
					byte[] array = new byte[WebHeaderCollection.HeaderEncoding.GetByteCount(this.StatusDescription)];
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
						this.m_NativeResponse.ReasonLength = (ushort)array.Length;
						WebHeaderCollection.HeaderEncoding.GetBytes(this.StatusDescription, 0, array.Length, array, 0);
						this.m_NativeResponse.pReason = (sbyte*)ptr;
						try
						{
							fixed (UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE* ptr2 = &this.m_NativeResponse)
							{
								UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE* ptr3 = ptr2;
								if (asyncResult != null)
								{
									this.HttpListenerContext.EnsureBoundHandle();
								}
								num = UnsafeNclNativeMethods.HttpApi.HttpSendHttpResponse(this.HttpListenerContext.RequestQueueHandle, this.HttpListenerRequest.RequestId, (uint)flags, ptr3, null, &num2, SafeLocalFree.Zero, 0U, (asyncResult == null) ? null : asyncResult.m_pOverlapped, null);
								if (asyncResult != null && num == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
								{
									asyncResult.IOCompleted(num, num2);
								}
								return num;
							}
						}
						finally
						{
							UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE* ptr2 = null;
						}
					}
					finally
					{
						byte[] array2 = null;
					}
				}
				try
				{
					fixed (UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE* ptr4 = &this.m_NativeResponse)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE* ptr5 = ptr4;
						if (asyncResult != null)
						{
							this.HttpListenerContext.EnsureBoundHandle();
						}
						num = UnsafeNclNativeMethods.HttpApi.HttpSendHttpResponse(this.HttpListenerContext.RequestQueueHandle, this.HttpListenerRequest.RequestId, (uint)flags, ptr5, null, &num2, SafeLocalFree.Zero, 0U, (asyncResult == null) ? null : asyncResult.m_pOverlapped, null);
						if (asyncResult != null && num == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
						{
							asyncResult.IOCompleted(num, num2);
						}
					}
				}
				finally
				{
					UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE* ptr4 = null;
				}
			}
			finally
			{
				this.FreePinnedHeaders(list);
			}
			return num;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0003560C File Offset: 0x0003380C
		internal void ComputeCookies()
		{
			if (this.m_Cookies != null)
			{
				string text = null;
				string text2 = null;
				for (int i = 0; i < this.m_Cookies.Count; i++)
				{
					Cookie cookie = this.m_Cookies[i];
					string text3 = cookie.ToServerString();
					if (text3 != null && text3.Length != 0)
					{
						if (cookie.Variant == CookieVariant.Rfc2965 || (this.HttpListenerContext.PromoteCookiesToRfc2965 && cookie.Variant == CookieVariant.Rfc2109))
						{
							text = ((text == null) ? text3 : (text + ", " + text3));
						}
						else
						{
							text2 = ((text2 == null) ? text3 : (text2 + ", " + text3));
						}
					}
				}
				if (!string.IsNullOrEmpty(text2))
				{
					this.Headers.Set(HttpResponseHeader.SetCookie, text2);
					if (string.IsNullOrEmpty(text))
					{
						this.Headers.Remove("Set-Cookie2");
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.Headers.Set("Set-Cookie2", text);
					if (string.IsNullOrEmpty(text2))
					{
						this.Headers.Remove("Set-Cookie");
					}
				}
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0003570C File Offset: 0x0003390C
		internal UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS ComputeHeaders()
		{
			UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS http_FLAGS = UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE;
			this.m_ResponseState = HttpListenerResponse.ResponseState.ComputedHeaders;
			this.ComputeCoreHeaders();
			if (this.m_BoundaryType == BoundaryType.None)
			{
				if (this.HttpListenerRequest.ProtocolVersion.Minor == 0)
				{
					this.m_KeepAlive = false;
				}
				else
				{
					this.m_BoundaryType = BoundaryType.Chunked;
				}
				if (this.CanSendResponseBody(this.m_HttpContext.Response.StatusCode))
				{
					this.m_ContentLength = -1L;
				}
				else
				{
					this.ContentLength64 = 0L;
				}
			}
			if (this.m_BoundaryType == BoundaryType.ContentLength)
			{
				this.Headers.SetInternal(HttpResponseHeader.ContentLength, this.m_ContentLength.ToString("D", NumberFormatInfo.InvariantInfo));
				if (this.m_ContentLength == 0L)
				{
					http_FLAGS = UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE;
				}
			}
			else if (this.m_BoundaryType == BoundaryType.Chunked)
			{
				this.Headers.SetInternal(HttpResponseHeader.TransferEncoding, "chunked");
			}
			else if (this.m_BoundaryType == BoundaryType.None)
			{
				http_FLAGS = UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE;
			}
			else
			{
				this.m_KeepAlive = false;
			}
			if (!this.m_KeepAlive)
			{
				this.Headers.Add(HttpResponseHeader.Connection, "close");
				if (http_FLAGS == UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE)
				{
					http_FLAGS = UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_RECEIVE_REQUEST_FLAG_COPY_BODY;
				}
			}
			else if (this.HttpListenerRequest.ProtocolVersion.Minor == 0)
			{
				this.Headers.SetInternal(HttpResponseHeader.KeepAlive, "true");
			}
			return http_FLAGS;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00035827 File Offset: 0x00033A27
		internal void ComputeCoreHeaders()
		{
			if (this.HttpListenerContext.MutualAuthentication != null && this.HttpListenerContext.MutualAuthentication.Length > 0)
			{
				this.Headers.SetInternal(HttpResponseHeader.WwwAuthenticate, this.HttpListenerContext.MutualAuthentication);
			}
			this.ComputeCookies();
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00035868 File Offset: 0x00033A68
		private unsafe List<GCHandle> SerializeHeaders(ref UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADERS headers, bool isWebSocketHandshake)
		{
			UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER[] array = null;
			if (this.Headers.Count == 0)
			{
				return null;
			}
			List<GCHandle> list = new List<GCHandle>();
			int num = 0;
			for (int i = 0; i < this.Headers.Count; i++)
			{
				string text = this.Headers.GetKey(i);
				int num2 = UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.IndexOfKnownHeader(text);
				if (num2 == 27 || (isWebSocketHandshake && num2 == 1))
				{
					num2 = -1;
				}
				if (num2 == -1)
				{
					string[] values = this.Headers.GetValues(i);
					num += values.Length;
				}
			}
			try
			{
				try
				{
					fixed (UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER* ptr = &headers.KnownHeaders)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER* ptr2 = ptr;
						for (int j = 0; j < this.Headers.Count; j++)
						{
							string text = this.Headers.GetKey(j);
							string text2 = this.Headers.Get(j);
							int num2 = UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.IndexOfKnownHeader(text);
							if (num2 == 27 || (isWebSocketHandshake && num2 == 1))
							{
								num2 = -1;
							}
							if (num2 == -1)
							{
								if (array == null)
								{
									array = new UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER[num];
									GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
									list.Add(gchandle);
									headers.pUnknownHeaders = (UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER*)(void*)gchandle.AddrOfPinnedObject();
								}
								string[] values2 = this.Headers.GetValues(j);
								for (int k = 0; k < values2.Length; k++)
								{
									byte[] array2 = new byte[WebHeaderCollection.HeaderEncoding.GetByteCount(text)];
									array[(int)headers.UnknownHeaderCount].NameLength = (ushort)array2.Length;
									WebHeaderCollection.HeaderEncoding.GetBytes(text, 0, array2.Length, array2, 0);
									GCHandle gchandle = GCHandle.Alloc(array2, GCHandleType.Pinned);
									list.Add(gchandle);
									array[(int)headers.UnknownHeaderCount].pName = (sbyte*)(void*)gchandle.AddrOfPinnedObject();
									text2 = values2[k];
									array2 = new byte[WebHeaderCollection.HeaderEncoding.GetByteCount(text2)];
									array[(int)headers.UnknownHeaderCount].RawValueLength = (ushort)array2.Length;
									WebHeaderCollection.HeaderEncoding.GetBytes(text2, 0, array2.Length, array2, 0);
									gchandle = GCHandle.Alloc(array2, GCHandleType.Pinned);
									list.Add(gchandle);
									array[(int)headers.UnknownHeaderCount].pRawValue = (sbyte*)(void*)gchandle.AddrOfPinnedObject();
									headers.UnknownHeaderCount += 1;
								}
							}
							else if (text2 != null)
							{
								byte[] array2 = new byte[WebHeaderCollection.HeaderEncoding.GetByteCount(text2)];
								ptr2[num2].RawValueLength = (ushort)array2.Length;
								WebHeaderCollection.HeaderEncoding.GetBytes(text2, 0, array2.Length, array2, 0);
								GCHandle gchandle = GCHandle.Alloc(array2, GCHandleType.Pinned);
								list.Add(gchandle);
								ptr2[num2].pRawValue = (sbyte*)(void*)gchandle.AddrOfPinnedObject();
							}
						}
					}
				}
				finally
				{
					UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER* ptr = null;
				}
			}
			catch
			{
				this.FreePinnedHeaders(list);
				throw;
			}
			return list;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00035B34 File Offset: 0x00033D34
		private void FreePinnedHeaders(List<GCHandle> pinnedHeaders)
		{
			if (pinnedHeaders != null)
			{
				foreach (GCHandle gchandle in pinnedHeaders)
				{
					if (gchandle.IsAllocated)
					{
						gchandle.Free();
					}
				}
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00035B90 File Offset: 0x00033D90
		private void CheckDisposed()
		{
			if (this.m_ResponseState >= HttpListenerResponse.ResponseState.Closed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00035BAC File Offset: 0x00033DAC
		internal void CancelLastWrite(CriticalHandle requestQueueHandle)
		{
			if (this.m_ResponseStream != null)
			{
				this.m_ResponseStream.CancelLastWrite(requestQueueHandle);
			}
		}

		// Token: 0x04000E38 RID: 3640
		private Encoding m_ContentEncoding;

		// Token: 0x04000E39 RID: 3641
		private CookieCollection m_Cookies;

		// Token: 0x04000E3A RID: 3642
		private string m_StatusDescription;

		// Token: 0x04000E3B RID: 3643
		private bool m_KeepAlive;

		// Token: 0x04000E3C RID: 3644
		private HttpListenerResponse.ResponseState m_ResponseState;

		// Token: 0x04000E3D RID: 3645
		private WebHeaderCollection m_WebHeaders;

		// Token: 0x04000E3E RID: 3646
		private HttpResponseStream m_ResponseStream;

		// Token: 0x04000E3F RID: 3647
		private long m_ContentLength;

		// Token: 0x04000E40 RID: 3648
		private BoundaryType m_BoundaryType;

		// Token: 0x04000E41 RID: 3649
		private UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE m_NativeResponse;

		// Token: 0x04000E42 RID: 3650
		private HttpListenerContext m_HttpContext;

		// Token: 0x04000E43 RID: 3651
		private static readonly int[] s_NoResponseBody = new int[] { 100, 101, 204, 205, 304 };

		// Token: 0x02000705 RID: 1797
		private enum ResponseState
		{
			// Token: 0x040030C0 RID: 12480
			Created,
			// Token: 0x040030C1 RID: 12481
			ComputedHeaders,
			// Token: 0x040030C2 RID: 12482
			SentHeaders,
			// Token: 0x040030C3 RID: 12483
			Closed
		}
	}
}
