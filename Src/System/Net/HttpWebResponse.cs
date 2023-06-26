using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Provides an HTTP-specific implementation of the <see cref="T:System.Net.WebResponse" /> class.</summary>
	// Token: 0x0200010B RID: 267
	[global::__DynamicallyInvokable]
	[Serializable]
	public class HttpWebResponse : WebResponse, ISerializable
	{
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0003BC7E File Offset: 0x00039E7E
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x0003BC86 File Offset: 0x00039E86
		internal bool IsWebSocketResponse
		{
			get
			{
				return this.m_IsWebSocketResponse;
			}
			set
			{
				this.m_IsWebSocketResponse = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0003BC8F File Offset: 0x00039E8F
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x0003BC97 File Offset: 0x00039E97
		internal string ConnectionGroupName
		{
			get
			{
				return this.m_ConnectionGroupName;
			}
			set
			{
				this.m_ConnectionGroupName = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0003BCA0 File Offset: 0x00039EA0
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x0003BCA8 File Offset: 0x00039EA8
		internal Stream ResponseStream
		{
			get
			{
				return this.m_ConnectStream;
			}
			set
			{
				this.m_ConnectStream = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0003BCB1 File Offset: 0x00039EB1
		internal CoreResponseData CoreResponseData
		{
			get
			{
				return this.m_CoreResponseData;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether both client and server were authenticated.</summary>
		/// <returns>
		///   <see langword="true" /> if mutual authentication occurred; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0003BCB9 File Offset: 0x00039EB9
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				this.CheckDisposed();
				return this.m_IsMutuallyAuthenticated;
			}
		}

		// Token: 0x17000271 RID: 625
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x0003BCC7 File Offset: 0x00039EC7
		internal bool InternalSetIsMutuallyAuthenticated
		{
			set
			{
				this.m_IsMutuallyAuthenticated = value;
			}
		}

		/// <summary>Gets or sets the cookies that are associated with this response.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains the cookies that are associated with this response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0003BCD0 File Offset: 0x00039ED0
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0003BCF1 File Offset: 0x00039EF1
		[global::__DynamicallyInvokable]
		public virtual CookieCollection Cookies
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				if (this.m_cookies == null)
				{
					this.m_cookies = new CookieCollection();
				}
				return this.m_cookies;
			}
			set
			{
				this.CheckDisposed();
				this.m_cookies = value;
			}
		}

		/// <summary>Gets the headers that are associated with this response from the server.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the header information returned with the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0003BD00 File Offset: 0x00039F00
		[global::__DynamicallyInvokable]
		public override WebHeaderCollection Headers
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.m_HttpResponseHeaders;
			}
		}

		/// <summary>Gets a value that indicates whether headers are supported.</summary>
		/// <returns>
		///   <see langword="true" /> if headers are supported; otherwise, <see langword="false" />. Always returns <see langword="true" />.</returns>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0003BD0E File Offset: 0x00039F0E
		[global::__DynamicallyInvokable]
		public override bool SupportsHeaders
		{
			[global::__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		/// <summary>Gets the length of the content returned by the request.</summary>
		/// <returns>The number of bytes returned by the request. Content length does not include header information.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0003BD11 File Offset: 0x00039F11
		[global::__DynamicallyInvokable]
		public override long ContentLength
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.m_ContentLength;
			}
		}

		/// <summary>Gets the method that is used to encode the body of the response.</summary>
		/// <returns>A string that describes the method that is used to encode the body of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0003BD20 File Offset: 0x00039F20
		public string ContentEncoding
		{
			get
			{
				this.CheckDisposed();
				string text = this.m_HttpResponseHeaders["Content-Encoding"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the content type of the response.</summary>
		/// <returns>A string that contains the content type of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0003BD50 File Offset: 0x00039F50
		[global::__DynamicallyInvokable]
		public override string ContentType
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				string contentType = this.m_HttpResponseHeaders.ContentType;
				if (contentType != null)
				{
					return contentType;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the character set of the response.</summary>
		/// <returns>A string that contains the character set of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0003BD7C File Offset: 0x00039F7C
		public string CharacterSet
		{
			get
			{
				this.CheckDisposed();
				string contentType = this.m_HttpResponseHeaders.ContentType;
				if (this.m_CharacterSet == null && !ValidationHelper.IsBlankString(contentType))
				{
					this.m_CharacterSet = string.Empty;
					string text = contentType.ToLower(CultureInfo.InvariantCulture);
					if (text.Trim().StartsWith("text/"))
					{
						this.m_CharacterSet = "ISO-8859-1";
					}
					int num = text.IndexOf(";");
					if (num > 0)
					{
						while ((num = text.IndexOf("charset", num)) >= 0)
						{
							num += 7;
							if (text[num - 8] != ';')
							{
								if (text[num - 8] != ' ')
								{
									continue;
								}
							}
							while (num < text.Length && text[num] == ' ')
							{
								num++;
							}
							if (num < text.Length - 1 && text[num] == '=')
							{
								num++;
								int num2 = text.IndexOf(';', num);
								if (num2 > num)
								{
									this.m_CharacterSet = contentType.Substring(num, num2 - num).Trim();
									break;
								}
								this.m_CharacterSet = contentType.Substring(num).Trim();
								break;
							}
						}
					}
				}
				return this.m_CharacterSet;
			}
		}

		/// <summary>Gets the name of the server that sent the response.</summary>
		/// <returns>A string that contains the name of the server that sent the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0003BEA4 File Offset: 0x0003A0A4
		public string Server
		{
			get
			{
				this.CheckDisposed();
				string server = this.m_HttpResponseHeaders.Server;
				if (server != null)
				{
					return server;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the last date and time that the contents of the response were modified.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the date and time that the contents of the response were modified.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0003BED0 File Offset: 0x0003A0D0
		public DateTime LastModified
		{
			get
			{
				this.CheckDisposed();
				string lastModified = this.m_HttpResponseHeaders.LastModified;
				if (lastModified == null)
				{
					return DateTime.Now;
				}
				return HttpProtocolUtils.string2date(lastModified);
			}
		}

		/// <summary>Gets the status of the response.</summary>
		/// <returns>One of the <see cref="T:System.Net.HttpStatusCode" /> values.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0003BEFE File Offset: 0x0003A0FE
		[global::__DynamicallyInvokable]
		public virtual HttpStatusCode StatusCode
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.m_StatusCode;
			}
		}

		/// <summary>Gets the status description returned with the response.</summary>
		/// <returns>A string that describes the status of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0003BF0C File Offset: 0x0003A10C
		[global::__DynamicallyInvokable]
		public virtual string StatusDescription
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.m_StatusDescription;
			}
		}

		/// <summary>Gets the version of the HTTP protocol that is used in the response.</summary>
		/// <returns>A <see cref="T:System.Version" /> that contains the HTTP protocol version of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0003BF1A File Offset: 0x0003A11A
		public Version ProtocolVersion
		{
			get
			{
				this.CheckDisposed();
				if (!this.m_IsVersionHttp11)
				{
					return HttpVersion.Version10;
				}
				return HttpVersion.Version11;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0003BF38 File Offset: 0x0003A138
		internal bool KeepAlive
		{
			get
			{
				if (this.m_UsesProxySemantics)
				{
					string text = this.Headers["Proxy-Connection"];
					if (text != null)
					{
						return text.ToLower(CultureInfo.InvariantCulture).IndexOf("close") < 0 || text.ToLower(CultureInfo.InvariantCulture).IndexOf("keep-alive") >= 0;
					}
				}
				string text2 = this.Headers["Connection"];
				if (text2 != null)
				{
					text2 = text2.ToLower(CultureInfo.InvariantCulture);
				}
				if (this.ProtocolVersion == HttpVersion.Version10)
				{
					return text2 != null && text2.IndexOf("keep-alive") >= 0;
				}
				return this.ProtocolVersion >= HttpVersion.Version11 && (text2 == null || text2.IndexOf("close") < 0 || text2.IndexOf("keep-alive") >= 0);
			}
		}

		/// <summary>Gets the stream that is used to read the body of the response from the server.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> containing the body of the response.</returns>
		/// <exception cref="T:System.Net.ProtocolViolationException">There is no response stream.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x06000AD5 RID: 2773 RVA: 0x0003C018 File Offset: 0x0003A218
		[global::__DynamicallyInvokable]
		public override Stream GetResponseStream()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "GetResponseStream", "");
			}
			this.CheckDisposed();
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, "ContentLength=" + this.m_ContentLength.ToString());
			}
			Stream stream;
			if (this.m_IsWebSocketResponse && this.m_StatusCode == HttpStatusCode.SwitchingProtocols)
			{
				if (this.m_WebSocketConnectionStream == null)
				{
					ConnectStream connectStream = this.m_ConnectStream as ConnectStream;
					this.m_WebSocketConnectionStream = new WebSocketConnectionStream(connectStream, this.ConnectionGroupName);
				}
				stream = this.m_WebSocketConnectionStream;
			}
			else
			{
				stream = this.m_ConnectStream;
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "GetResponseStream", stream);
			}
			return stream;
		}

		/// <summary>Closes the response stream.</summary>
		/// <exception cref="T:System.ObjectDisposedException">.NET Core only: This <see cref="T:System.Net.HttpWebResponse" /> object has been disposed.</exception>
		// Token: 0x06000AD6 RID: 2774 RVA: 0x0003C0D0 File Offset: 0x0003A2D0
		public override void Close()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "Close", "");
			}
			if (!this.m_disposed)
			{
				this.m_disposed = true;
				try
				{
					Stream connectStream = this.m_ConnectStream;
					ICloseEx closeEx = connectStream as ICloseEx;
					if (closeEx != null)
					{
						closeEx.CloseEx(CloseExState.Normal);
					}
					else if (connectStream != null)
					{
						connectStream.Close();
					}
				}
				finally
				{
					if (this.IsWebSocketResponse)
					{
						ConnectStream connectStream2 = this.m_ConnectStream as ConnectStream;
						if (connectStream2 != null && connectStream2.Connection != null)
						{
							connectStream2.Connection.ServicePoint.CloseConnectionGroup(this.ConnectionGroupName);
						}
					}
				}
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "Close", "");
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0003C190 File Offset: 0x0003A390
		internal void Abort()
		{
			Stream connectStream = this.m_ConnectStream;
			ICloseEx closeEx = connectStream as ICloseEx;
			try
			{
				if (closeEx != null)
				{
					closeEx.CloseEx(CloseExState.Abort);
				}
				else if (connectStream != null)
				{
					connectStream.Close();
				}
			}
			catch
			{
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.HttpWebResponse" />, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x06000AD8 RID: 2776 RVA: 0x0003C1D8 File Offset: 0x0003A3D8
		[global::__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			base.Dispose(true);
			this.m_propertiesDisposed = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebResponse" /> class.</summary>
		// Token: 0x06000AD9 RID: 2777 RVA: 0x0003C1EC File Offset: 0x0003A3EC
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public HttpWebResponse()
		{
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0003C1F4 File Offset: 0x0003A3F4
		internal HttpWebResponse(Uri responseUri, KnownHttpVerb verb, CoreResponseData coreData, string mediaType, bool usesProxySemantics, DecompressionMethods decompressionMethod, bool isWebSocketResponse, string connectionGroupName)
		{
			this.m_Uri = responseUri;
			this.m_Verb = verb;
			this.m_MediaType = mediaType;
			this.m_UsesProxySemantics = usesProxySemantics;
			this.m_CoreResponseData = coreData;
			this.m_ConnectStream = coreData.m_ConnectStream;
			this.m_HttpResponseHeaders = coreData.m_ResponseHeaders;
			this.m_ContentLength = coreData.m_ContentLength;
			this.m_StatusCode = coreData.m_StatusCode;
			this.m_StatusDescription = coreData.m_StatusDescription;
			this.m_IsVersionHttp11 = coreData.m_IsVersionHttp11;
			this.m_IsWebSocketResponse = isWebSocketResponse;
			this.m_ConnectionGroupName = connectionGroupName;
			if (this.m_ContentLength == 0L && this.m_ConnectStream is ConnectStream)
			{
				((ConnectStream)this.m_ConnectStream).CallDone();
			}
			string text = this.m_HttpResponseHeaders["Content-Location"];
			if (text != null)
			{
				try
				{
					this.m_Uri = new Uri(this.m_Uri, text);
				}
				catch (UriFormatException ex)
				{
				}
			}
			if (decompressionMethod != DecompressionMethods.None)
			{
				string text2 = this.m_HttpResponseHeaders["Content-Encoding"];
				if (text2 != null)
				{
					if ((decompressionMethod & DecompressionMethods.GZip) != DecompressionMethods.None && text2.IndexOf("gzip", StringComparison.CurrentCulture) != -1)
					{
						this.m_ConnectStream = new GZipWrapperStream(this.m_ConnectStream, CompressionMode.Decompress);
						this.m_ContentLength = -1L;
						this.m_HttpResponseHeaders["Content-Encoding"] = null;
						return;
					}
					if ((decompressionMethod & DecompressionMethods.Deflate) != DecompressionMethods.None && text2.IndexOf("deflate", StringComparison.CurrentCulture) != -1)
					{
						this.m_ConnectStream = new DeflateWrapperStream(this.m_ConnectStream, CompressionMode.Decompress);
						this.m_ContentLength = -1L;
						this.m_HttpResponseHeaders["Content-Encoding"] = null;
					}
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebResponse" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.HttpWebRequest" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.HttpWebRequest" />.</param>
		// Token: 0x06000ADB RID: 2779 RVA: 0x0003C384 File Offset: 0x0003A584
		[Obsolete("Serialization is obsoleted for this type.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected HttpWebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
			this.m_HttpResponseHeaders = (WebHeaderCollection)serializationInfo.GetValue("m_HttpResponseHeaders", typeof(WebHeaderCollection));
			this.m_Uri = (Uri)serializationInfo.GetValue("m_Uri", typeof(Uri));
			this.m_Certificate = (X509Certificate)serializationInfo.GetValue("m_Certificate", typeof(X509Certificate));
			Version version = (Version)serializationInfo.GetValue("m_Version", typeof(Version));
			this.m_IsVersionHttp11 = version.Equals(HttpVersion.Version11);
			this.m_StatusCode = (HttpStatusCode)serializationInfo.GetInt32("m_StatusCode");
			this.m_ContentLength = serializationInfo.GetInt64("m_ContentLength");
			this.m_Verb = KnownHttpVerb.Parse(serializationInfo.GetString("m_Verb"));
			this.m_StatusDescription = serializationInfo.GetString("m_StatusDescription");
			this.m_MediaType = serializationInfo.GetString("m_MediaType");
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.HttpWebResponse" /> will be serialized.</param>
		/// <param name="streamingContext">The destination of the serialization.</param>
		// Token: 0x06000ADC RID: 2780 RVA: 0x0003C47F File Offset: 0x0003A67F
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06000ADD RID: 2781 RVA: 0x0003C48C File Offset: 0x0003A68C
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("m_HttpResponseHeaders", this.m_HttpResponseHeaders, typeof(WebHeaderCollection));
			serializationInfo.AddValue("m_Uri", this.m_Uri, typeof(Uri));
			serializationInfo.AddValue("m_Certificate", this.m_Certificate, typeof(X509Certificate));
			serializationInfo.AddValue("m_Version", this.ProtocolVersion, typeof(Version));
			serializationInfo.AddValue("m_StatusCode", this.m_StatusCode);
			serializationInfo.AddValue("m_ContentLength", this.m_ContentLength);
			serializationInfo.AddValue("m_Verb", this.m_Verb.Name);
			serializationInfo.AddValue("m_StatusDescription", this.m_StatusDescription);
			serializationInfo.AddValue("m_MediaType", this.m_MediaType);
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Gets the contents of a header that was returned with the response.</summary>
		/// <param name="headerName">The header value to return.</param>
		/// <returns>The contents of the specified header.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x06000ADE RID: 2782 RVA: 0x0003C56C File Offset: 0x0003A76C
		public string GetResponseHeader(string headerName)
		{
			this.CheckDisposed();
			string text = this.m_HttpResponseHeaders[headerName];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}

		/// <summary>Gets the URI of the Internet resource that responded to the request.</summary>
		/// <returns>The URI of the Internet resource that responded to the request.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0003C596 File Offset: 0x0003A796
		[global::__DynamicallyInvokable]
		public override Uri ResponseUri
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.m_Uri;
			}
		}

		/// <summary>Gets the method that is used to return the response.</summary>
		/// <returns>A string that contains the HTTP method that is used to return the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0003C5A4 File Offset: 0x0003A7A4
		[global::__DynamicallyInvokable]
		public virtual string Method
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.m_Verb.Name;
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0003C5B7 File Offset: 0x0003A7B7
		private void CheckDisposed()
		{
			if (this.m_propertiesDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x04000F35 RID: 3893
		private Uri m_Uri;

		// Token: 0x04000F36 RID: 3894
		private KnownHttpVerb m_Verb;

		// Token: 0x04000F37 RID: 3895
		private HttpStatusCode m_StatusCode;

		// Token: 0x04000F38 RID: 3896
		private string m_StatusDescription;

		// Token: 0x04000F39 RID: 3897
		private Stream m_ConnectStream;

		// Token: 0x04000F3A RID: 3898
		private CoreResponseData m_CoreResponseData;

		// Token: 0x04000F3B RID: 3899
		private WebHeaderCollection m_HttpResponseHeaders;

		// Token: 0x04000F3C RID: 3900
		private long m_ContentLength;

		// Token: 0x04000F3D RID: 3901
		private string m_MediaType;

		// Token: 0x04000F3E RID: 3902
		private string m_CharacterSet;

		// Token: 0x04000F3F RID: 3903
		private bool m_IsVersionHttp11;

		// Token: 0x04000F40 RID: 3904
		internal X509Certificate m_Certificate;

		// Token: 0x04000F41 RID: 3905
		private CookieCollection m_cookies;

		// Token: 0x04000F42 RID: 3906
		private bool m_disposed;

		// Token: 0x04000F43 RID: 3907
		private bool m_propertiesDisposed;

		// Token: 0x04000F44 RID: 3908
		private bool m_UsesProxySemantics;

		// Token: 0x04000F45 RID: 3909
		private bool m_IsMutuallyAuthenticated;

		// Token: 0x04000F46 RID: 3910
		private bool m_IsWebSocketResponse;

		// Token: 0x04000F47 RID: 3911
		private string m_ConnectionGroupName;

		// Token: 0x04000F48 RID: 3912
		private Stream m_WebSocketConnectionStream;
	}
}
