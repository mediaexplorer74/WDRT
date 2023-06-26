using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Base class that represents an email attachment. Classes <see cref="T:System.Net.Mail.Attachment" />, <see cref="T:System.Net.Mail.AlternateView" />, and <see cref="T:System.Net.Mail.LinkedResource" /> derive from this class.</summary>
	// Token: 0x02000256 RID: 598
	public abstract class AttachmentBase : IDisposable
	{
		// Token: 0x060016B0 RID: 5808 RVA: 0x00075415 File Offset: 0x00073615
		internal AttachmentBase()
		{
		}

		/// <summary>Instantiates an <see cref="T:System.Net.Mail.AttachmentBase" /> with the specified file name.</summary>
		/// <param name="fileName">The file name holding the content for this attachment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		// Token: 0x060016B1 RID: 5809 RVA: 0x00075428 File Offset: 0x00073628
		protected AttachmentBase(string fileName)
		{
			this.SetContentFromFile(fileName, string.Empty);
		}

		/// <summary>Instantiates an <see cref="T:System.Net.Mail.AttachmentBase" /> with the specified file name and media type.</summary>
		/// <param name="fileName">The file name holding the content for this attachment.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		// Token: 0x060016B2 RID: 5810 RVA: 0x00075447 File Offset: 0x00073647
		protected AttachmentBase(string fileName, string mediaType)
		{
			this.SetContentFromFile(fileName, mediaType);
		}

		/// <summary>Instantiates an <see cref="T:System.Net.Mail.AttachmentBase" /> with the specified file name and content type.</summary>
		/// <param name="fileName">The file name holding the content for this attachment.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		// Token: 0x060016B3 RID: 5811 RVA: 0x00075462 File Offset: 0x00073662
		protected AttachmentBase(string fileName, ContentType contentType)
		{
			this.SetContentFromFile(fileName, contentType);
		}

		/// <summary>Instantiates an <see cref="T:System.Net.Mail.AttachmentBase" /> with the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="contentStream">A stream containing the content for this attachment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		// Token: 0x060016B4 RID: 5812 RVA: 0x0007547D File Offset: 0x0007367D
		protected AttachmentBase(Stream contentStream)
		{
			this.part.SetContent(contentStream);
		}

		/// <summary>Instantiates an <see cref="T:System.Net.Mail.AttachmentBase" /> with the specified <see cref="T:System.IO.Stream" /> and media type.</summary>
		/// <param name="contentStream">A stream containing the content for this attachment.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		// Token: 0x060016B5 RID: 5813 RVA: 0x0007549C File Offset: 0x0007369C
		protected AttachmentBase(Stream contentStream, string mediaType)
		{
			this.part.SetContent(contentStream, null, mediaType);
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x000754BD File Offset: 0x000736BD
		internal AttachmentBase(Stream contentStream, string name, string mediaType)
		{
			this.part.SetContent(contentStream, name, mediaType);
		}

		/// <summary>Instantiates an <see cref="T:System.Net.Mail.AttachmentBase" /> with the specified <see cref="T:System.IO.Stream" /> and <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="contentStream">A stream containing the content for this attachment.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		// Token: 0x060016B7 RID: 5815 RVA: 0x000754DE File Offset: 0x000736DE
		protected AttachmentBase(Stream contentStream, ContentType contentType)
		{
			this.part.SetContent(contentStream, contentType);
		}

		/// <summary>Releases the resources used by the <see cref="T:System.Net.Mail.AttachmentBase" />.</summary>
		// Token: 0x060016B8 RID: 5816 RVA: 0x000754FE File Offset: 0x000736FE
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Mail.AttachmentBase" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060016B9 RID: 5817 RVA: 0x00075507 File Offset: 0x00073707
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				this.part.Dispose();
			}
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00075528 File Offset: 0x00073728
		internal static string ShortNameFromFile(string fileName)
		{
			int num = fileName.LastIndexOfAny(new char[] { '\\', ':' }, fileName.Length - 1, fileName.Length);
			string text;
			if (num > 0)
			{
				text = fileName.Substring(num + 1, fileName.Length - num - 1);
			}
			else
			{
				text = fileName;
			}
			return text;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00075578 File Offset: 0x00073778
		internal void SetContentFromFile(string fileName, ContentType contentType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[] { "fileName" }), "fileName");
			}
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.part.SetContent(stream, contentType);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000755DC File Offset: 0x000737DC
		internal void SetContentFromFile(string fileName, string mediaType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[] { "fileName" }), "fileName");
			}
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.part.SetContent(stream, null, mediaType);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00075640 File Offset: 0x00073840
		internal void SetContentFromString(string contentString, ContentType contentType)
		{
			if (contentString == null)
			{
				throw new ArgumentNullException("content");
			}
			if (this.part.Stream != null)
			{
				this.part.Stream.Close();
			}
			Encoding encoding;
			if (contentType != null && contentType.CharSet != null)
			{
				encoding = Encoding.GetEncoding(contentType.CharSet);
			}
			else if (MimeBasePart.IsAscii(contentString, false))
			{
				encoding = Encoding.ASCII;
			}
			else
			{
				encoding = Encoding.GetEncoding("utf-8");
			}
			byte[] bytes = encoding.GetBytes(contentString);
			this.part.SetContent(new MemoryStream(bytes), contentType);
			if (MimeBasePart.ShouldUseBase64Encoding(encoding))
			{
				this.part.TransferEncoding = TransferEncoding.Base64;
				return;
			}
			this.part.TransferEncoding = TransferEncoding.QuotedPrintable;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x000756E8 File Offset: 0x000738E8
		internal void SetContentFromString(string contentString, Encoding encoding, string mediaType)
		{
			if (contentString == null)
			{
				throw new ArgumentNullException("content");
			}
			if (this.part.Stream != null)
			{
				this.part.Stream.Close();
			}
			if (mediaType == null || mediaType == string.Empty)
			{
				mediaType = "text/plain";
			}
			int num = 0;
			try
			{
				string text = MailBnfHelper.ReadToken(mediaType, ref num, null);
				if (text.Length == 0 || num >= mediaType.Length || mediaType[num++] != '/')
				{
					throw new ArgumentException(SR.GetString("MediaTypeInvalid"), "mediaType");
				}
				text = MailBnfHelper.ReadToken(mediaType, ref num, null);
				if (text.Length == 0 || num < mediaType.Length)
				{
					throw new ArgumentException(SR.GetString("MediaTypeInvalid"), "mediaType");
				}
			}
			catch (FormatException)
			{
				throw new ArgumentException(SR.GetString("MediaTypeInvalid"), "mediaType");
			}
			ContentType contentType = new ContentType(mediaType);
			if (encoding == null)
			{
				if (MimeBasePart.IsAscii(contentString, false))
				{
					encoding = Encoding.ASCII;
				}
				else
				{
					encoding = Encoding.GetEncoding("utf-8");
				}
			}
			contentType.CharSet = encoding.BodyName;
			byte[] bytes = encoding.GetBytes(contentString);
			this.part.SetContent(new MemoryStream(bytes), contentType);
			if (MimeBasePart.ShouldUseBase64Encoding(encoding))
			{
				this.part.TransferEncoding = TransferEncoding.Base64;
				return;
			}
			this.part.TransferEncoding = TransferEncoding.QuotedPrintable;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00075840 File Offset: 0x00073A40
		internal virtual void PrepareForSending(bool allowUnicode)
		{
			this.part.ResetStream();
		}

		/// <summary>Gets the content stream of this attachment.</summary>
		/// <returns>The content stream of this attachment.</returns>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x0007584D File Offset: 0x00073A4D
		public Stream ContentStream
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return this.part.Stream;
			}
		}

		/// <summary>Gets or sets the MIME content ID for this attachment.</summary>
		/// <returns>A <see cref="T:System.String" /> holding the content ID.</returns>
		/// <exception cref="T:System.ArgumentNullException">Attempted to set <see cref="P:System.Net.Mail.AttachmentBase.ContentId" /> to <see langword="null" />.</exception>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x00075874 File Offset: 0x00073A74
		// (set) Token: 0x060016C2 RID: 5826 RVA: 0x000758EC File Offset: 0x00073AEC
		public string ContentId
		{
			get
			{
				string text = this.part.ContentID;
				if (string.IsNullOrEmpty(text))
				{
					text = Guid.NewGuid().ToString();
					this.ContentId = text;
					return text;
				}
				if (text.Length >= 2 && text[0] == '<' && text[text.Length - 1] == '>')
				{
					return text.Substring(1, text.Length - 2);
				}
				return text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.part.ContentID = null;
					return;
				}
				if (value.IndexOfAny(new char[] { '<', '>' }) != -1)
				{
					throw new ArgumentException(SR.GetString("MailHeaderInvalidCID"), "value");
				}
				this.part.ContentID = "<" + value + ">";
			}
		}

		/// <summary>Gets the content type of this attachment.</summary>
		/// <returns>The content type for this attachment.</returns>
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x00075957 File Offset: 0x00073B57
		// (set) Token: 0x060016C4 RID: 5828 RVA: 0x00075964 File Offset: 0x00073B64
		public ContentType ContentType
		{
			get
			{
				return this.part.ContentType;
			}
			set
			{
				this.part.ContentType = value;
			}
		}

		/// <summary>Gets or sets the encoding of this attachment.</summary>
		/// <returns>The encoding for this attachment.</returns>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x00075972 File Offset: 0x00073B72
		// (set) Token: 0x060016C6 RID: 5830 RVA: 0x0007597F File Offset: 0x00073B7F
		public TransferEncoding TransferEncoding
		{
			get
			{
				return this.part.TransferEncoding;
			}
			set
			{
				this.part.TransferEncoding = value;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x00075990 File Offset: 0x00073B90
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x000759B5 File Offset: 0x00073BB5
		internal Uri ContentLocation
		{
			get
			{
				Uri uri;
				if (!Uri.TryCreate(this.part.ContentLocation, UriKind.RelativeOrAbsolute, out uri))
				{
					return null;
				}
				return uri;
			}
			set
			{
				this.part.ContentLocation = ((value == null) ? null : (value.IsAbsoluteUri ? value.AbsoluteUri : value.OriginalString));
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x000759E4 File Offset: 0x00073BE4
		internal MimePart MimePart
		{
			get
			{
				return this.part;
			}
		}

		// Token: 0x04001758 RID: 5976
		internal bool disposed;

		// Token: 0x04001759 RID: 5977
		private MimePart part = new MimePart();
	}
}
