using System;
using System.IO;

namespace System.Net
{
	/// <summary>Encapsulates a File Transfer Protocol (FTP) server's response to a request.</summary>
	// Token: 0x020000F1 RID: 241
	public class FtpWebResponse : WebResponse, IDisposable
	{
		// Token: 0x06000862 RID: 2146 RVA: 0x0002F1F8 File Offset: 0x0002D3F8
		internal FtpWebResponse(Stream responseStream, long contentLength, Uri responseUri, FtpStatusCode statusCode, string statusLine, DateTime lastModified, string bannerMessage, string welcomeMessage, string exitMessage)
		{
			this.m_ResponseStream = responseStream;
			if (responseStream == null && contentLength < 0L)
			{
				contentLength = 0L;
			}
			this.m_ContentLength = contentLength;
			this.m_ResponseUri = responseUri;
			this.m_StatusCode = statusCode;
			this.m_StatusLine = statusLine;
			this.m_LastModified = lastModified;
			this.m_BannerMessage = bannerMessage;
			this.m_WelcomeMessage = welcomeMessage;
			this.m_ExitMessage = exitMessage;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0002F25C File Offset: 0x0002D45C
		internal FtpWebResponse(HttpWebResponse httpWebResponse)
		{
			this.m_HttpWebResponse = httpWebResponse;
			base.InternalSetFromCache = this.m_HttpWebResponse.IsFromCache;
			base.InternalSetIsCacheFresh = this.m_HttpWebResponse.IsCacheFresh;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0002F28D File Offset: 0x0002D48D
		internal void UpdateStatus(FtpStatusCode statusCode, string statusLine, string exitMessage)
		{
			this.m_StatusCode = statusCode;
			this.m_StatusLine = statusLine;
			this.m_ExitMessage = exitMessage;
		}

		/// <summary>Retrieves the stream that contains response data sent from an FTP server.</summary>
		/// <returns>A readable <see cref="T:System.IO.Stream" /> instance that contains data returned with the response; otherwise, <see cref="F:System.IO.Stream.Null" /> if no response data was returned by the server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The response did not return a data stream.</exception>
		// Token: 0x06000865 RID: 2149 RVA: 0x0002F2A4 File Offset: 0x0002D4A4
		public override Stream GetResponseStream()
		{
			Stream stream;
			if (this.HttpProxyMode)
			{
				stream = this.m_HttpWebResponse.GetResponseStream();
			}
			else if (this.m_ResponseStream != null)
			{
				stream = this.m_ResponseStream;
			}
			else
			{
				stream = (this.m_ResponseStream = new FtpWebResponse.EmptyStream());
			}
			return stream;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002F2EA File Offset: 0x0002D4EA
		internal void SetResponseStream(Stream stream)
		{
			if (stream == null || stream == Stream.Null || stream is FtpWebResponse.EmptyStream)
			{
				return;
			}
			this.m_ResponseStream = stream;
		}

		/// <summary>Frees the resources held by the response.</summary>
		// Token: 0x06000867 RID: 2151 RVA: 0x0002F308 File Offset: 0x0002D508
		public override void Close()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "Close", "");
			}
			if (this.HttpProxyMode)
			{
				this.m_HttpWebResponse.Close();
			}
			else
			{
				Stream responseStream = this.m_ResponseStream;
				if (responseStream != null)
				{
					responseStream.Close();
				}
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "Close", "");
			}
		}

		/// <summary>Gets the length of the data received from the FTP server.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that contains the number of bytes of data received from the FTP server.</returns>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0002F372 File Offset: 0x0002D572
		public override long ContentLength
		{
			get
			{
				if (this.HttpProxyMode)
				{
					return this.m_HttpWebResponse.ContentLength;
				}
				return this.m_ContentLength;
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0002F38E File Offset: 0x0002D58E
		internal void SetContentLength(long value)
		{
			if (this.HttpProxyMode)
			{
				return;
			}
			this.m_ContentLength = value;
		}

		/// <summary>Gets an empty <see cref="T:System.Net.WebHeaderCollection" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Net.WebHeaderCollection" /> object.</returns>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0002F3A0 File Offset: 0x0002D5A0
		public override WebHeaderCollection Headers
		{
			get
			{
				if (this.HttpProxyMode)
				{
					return this.m_HttpWebResponse.Headers;
				}
				if (this.m_FtpRequestHeaders == null)
				{
					lock (this)
					{
						if (this.m_FtpRequestHeaders == null)
						{
							this.m_FtpRequestHeaders = new WebHeaderCollection(WebHeaderCollectionType.FtpWebResponse);
						}
					}
				}
				return this.m_FtpRequestHeaders;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="P:System.Net.FtpWebResponse.Headers" /> property is supported by the <see cref="T:System.Net.FtpWebResponse" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.  
		///  <see langword="true" /> if the <see cref="P:System.Net.FtpWebResponse.Headers" /> property is supported by the <see cref="T:System.Net.FtpWebResponse" /> instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0002F40C File Offset: 0x0002D60C
		public override bool SupportsHeaders
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the URI that sent the response to the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that identifies the resource associated with this response.</returns>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0002F40F File Offset: 0x0002D60F
		public override Uri ResponseUri
		{
			get
			{
				if (this.HttpProxyMode)
				{
					return this.m_HttpWebResponse.ResponseUri;
				}
				return this.m_ResponseUri;
			}
		}

		/// <summary>Gets the most recent status code sent from the FTP server.</summary>
		/// <returns>An <see cref="T:System.Net.FtpStatusCode" /> value that indicates the most recent status code returned with this response.</returns>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0002F42B File Offset: 0x0002D62B
		public FtpStatusCode StatusCode
		{
			get
			{
				if (this.HttpProxyMode)
				{
					return (FtpStatusCode)this.m_HttpWebResponse.StatusCode;
				}
				return this.m_StatusCode;
			}
		}

		/// <summary>Gets text that describes a status code sent from the FTP server.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the status code and message returned with this response.</returns>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0002F447 File Offset: 0x0002D647
		public string StatusDescription
		{
			get
			{
				if (this.HttpProxyMode)
				{
					return this.m_HttpWebResponse.StatusDescription;
				}
				return this.m_StatusLine;
			}
		}

		/// <summary>Gets the date and time that a file on an FTP server was last modified.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the last modified date and time for a file.</returns>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0002F463 File Offset: 0x0002D663
		public DateTime LastModified
		{
			get
			{
				if (this.HttpProxyMode)
				{
					return this.m_HttpWebResponse.LastModified;
				}
				return this.m_LastModified;
			}
		}

		/// <summary>Gets the message sent by the FTP server when a connection is established prior to logon.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the banner message sent by the server; otherwise, <see cref="F:System.String.Empty" /> if no message is sent.</returns>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x0002F47F File Offset: 0x0002D67F
		public string BannerMessage
		{
			get
			{
				return this.m_BannerMessage;
			}
		}

		/// <summary>Gets the message sent by the FTP server when authentication is complete.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the welcome message sent by the server; otherwise, <see cref="F:System.String.Empty" /> if no message is sent.</returns>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0002F487 File Offset: 0x0002D687
		public string WelcomeMessage
		{
			get
			{
				return this.m_WelcomeMessage;
			}
		}

		/// <summary>Gets the message sent by the server when the FTP session is ending.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the exit message sent by the server; otherwise, <see cref="F:System.String.Empty" /> if no message is sent.</returns>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0002F48F File Offset: 0x0002D68F
		public string ExitMessage
		{
			get
			{
				return this.m_ExitMessage;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0002F497 File Offset: 0x0002D697
		private bool HttpProxyMode
		{
			get
			{
				return this.m_HttpWebResponse != null;
			}
		}

		// Token: 0x04000DC7 RID: 3527
		internal Stream m_ResponseStream;

		// Token: 0x04000DC8 RID: 3528
		private long m_ContentLength;

		// Token: 0x04000DC9 RID: 3529
		private Uri m_ResponseUri;

		// Token: 0x04000DCA RID: 3530
		private FtpStatusCode m_StatusCode;

		// Token: 0x04000DCB RID: 3531
		private string m_StatusLine;

		// Token: 0x04000DCC RID: 3532
		private WebHeaderCollection m_FtpRequestHeaders;

		// Token: 0x04000DCD RID: 3533
		private HttpWebResponse m_HttpWebResponse;

		// Token: 0x04000DCE RID: 3534
		private DateTime m_LastModified;

		// Token: 0x04000DCF RID: 3535
		private string m_BannerMessage;

		// Token: 0x04000DD0 RID: 3536
		private string m_WelcomeMessage;

		// Token: 0x04000DD1 RID: 3537
		private string m_ExitMessage;

		// Token: 0x020006FB RID: 1787
		internal class EmptyStream : MemoryStream
		{
			// Token: 0x06004060 RID: 16480 RVA: 0x0010DDC7 File Offset: 0x0010BFC7
			internal EmptyStream()
				: base(new byte[0], false)
			{
			}
		}
	}
}
