using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Provides a file system implementation of the <see cref="T:System.Net.WebResponse" /> class.</summary>
	// Token: 0x020000E9 RID: 233
	[Serializable]
	public class FileWebResponse : WebResponse, ISerializable, ICloseEx
	{
		// Token: 0x060007FA RID: 2042 RVA: 0x0002C164 File Offset: 0x0002A364
		internal FileWebResponse(FileWebRequest request, Uri uri, FileAccess access, bool asyncHint)
		{
			try
			{
				this.m_fileAccess = access;
				if (access == FileAccess.Write)
				{
					this.m_stream = Stream.Null;
				}
				else
				{
					this.m_stream = new FileWebStream(request, uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.Read, 8192, asyncHint);
					this.m_contentLength = this.m_stream.Length;
				}
				this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.FileWebResponse);
				this.m_headers.AddInternal("Content-Length", this.m_contentLength.ToString(NumberFormatInfo.InvariantInfo));
				this.m_headers.AddInternal("Content-Type", "application/octet-stream");
				this.m_uri = uri;
			}
			catch (Exception ex)
			{
				Exception ex2 = new WebException(ex.Message, ex, WebExceptionStatus.ConnectFailure, null);
				throw ex2;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.FileWebResponse" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to serialize the new <see cref="T:System.Net.FileWebResponse" /> instance.</param>
		/// <param name="streamingContext">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class that contains the source of the serialized stream associated with the new <see cref="T:System.Net.FileWebResponse" /> instance.</param>
		// Token: 0x060007FB RID: 2043 RVA: 0x0002C22C File Offset: 0x0002A42C
		[Obsolete("Serialization is obsoleted for this type. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected FileWebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
			this.m_headers = (WebHeaderCollection)serializationInfo.GetValue("headers", typeof(WebHeaderCollection));
			this.m_uri = (Uri)serializationInfo.GetValue("uri", typeof(Uri));
			this.m_contentLength = serializationInfo.GetInt64("contentLength");
			this.m_fileAccess = (FileAccess)serializationInfo.GetInt32("fileAccess");
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.FileWebResponse" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> , which will hold the serialized data for the <see cref="T:System.Net.FileWebResponse" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> containing the destination of the serialized stream associated with the new <see cref="T:System.Net.FileWebResponse" />.</param>
		// Token: 0x060007FC RID: 2044 RVA: 0x0002C2A3 File Offset: 0x0002A4A3
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x060007FD RID: 2045 RVA: 0x0002C2B0 File Offset: 0x0002A4B0
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("headers", this.m_headers, typeof(WebHeaderCollection));
			serializationInfo.AddValue("uri", this.m_uri, typeof(Uri));
			serializationInfo.AddValue("contentLength", this.m_contentLength);
			serializationInfo.AddValue("fileAccess", this.m_fileAccess);
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Gets the length of the content in the file system resource.</summary>
		/// <returns>The number of bytes returned from the file system resource.</returns>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0002C322 File Offset: 0x0002A522
		public override long ContentLength
		{
			get
			{
				this.CheckDisposed();
				return this.m_contentLength;
			}
		}

		/// <summary>Gets the content type of the file system resource.</summary>
		/// <returns>The value "binary/octet-stream".</returns>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0002C330 File Offset: 0x0002A530
		public override string ContentType
		{
			get
			{
				this.CheckDisposed();
				return "application/octet-stream";
			}
		}

		/// <summary>Gets a collection of header name/value pairs associated with the response.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the header name/value pairs associated with the response.</returns>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0002C33D File Offset: 0x0002A53D
		public override WebHeaderCollection Headers
		{
			get
			{
				this.CheckDisposed();
				return this.m_headers;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="P:System.Net.FileWebResponse.Headers" /> property is supported by the <see cref="T:System.Net.FileWebResponse" /> instance.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Net.FileWebResponse.Headers" /> property is supported by the <see cref="T:System.Net.FileWebResponse" /> instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0002C34B File Offset: 0x0002A54B
		public override bool SupportsHeaders
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the URI of the file system resource that provided the response.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the file system resource that provided the response.</returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0002C34E File Offset: 0x0002A54E
		public override Uri ResponseUri
		{
			get
			{
				this.CheckDisposed();
				return this.m_uri;
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0002C35C File Offset: 0x0002A55C
		private void CheckDisposed()
		{
			if (this.m_closed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		/// <summary>Closes the response stream.</summary>
		// Token: 0x06000804 RID: 2052 RVA: 0x0002C377 File Offset: 0x0002A577
		public override void Close()
		{
			((ICloseEx)this).CloseEx(CloseExState.Normal);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0002C380 File Offset: 0x0002A580
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			try
			{
				if (!this.m_closed)
				{
					this.m_closed = true;
					Stream stream = this.m_stream;
					if (stream != null)
					{
						if (stream is ICloseEx)
						{
							((ICloseEx)stream).CloseEx(closeState);
						}
						else
						{
							stream.Close();
						}
						this.m_stream = null;
					}
				}
			}
			finally
			{
			}
		}

		/// <summary>Returns the data stream from the file system resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> for reading data from the file system resource.</returns>
		// Token: 0x06000806 RID: 2054 RVA: 0x0002C3DC File Offset: 0x0002A5DC
		public override Stream GetResponseStream()
		{
			try
			{
				this.CheckDisposed();
			}
			finally
			{
			}
			return this.m_stream;
		}

		// Token: 0x04000D4D RID: 3405
		private const int DefaultFileStreamBufferSize = 8192;

		// Token: 0x04000D4E RID: 3406
		private const string DefaultFileContentType = "application/octet-stream";

		// Token: 0x04000D4F RID: 3407
		private bool m_closed;

		// Token: 0x04000D50 RID: 3408
		private long m_contentLength;

		// Token: 0x04000D51 RID: 3409
		private FileAccess m_fileAccess;

		// Token: 0x04000D52 RID: 3410
		private WebHeaderCollection m_headers;

		// Token: 0x04000D53 RID: 3411
		private Stream m_stream;

		// Token: 0x04000D54 RID: 3412
		private Uri m_uri;
	}
}
