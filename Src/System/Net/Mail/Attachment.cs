using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an attachment to an email.</summary>
	// Token: 0x02000257 RID: 599
	public class Attachment : AttachmentBase
	{
		// Token: 0x060016CA RID: 5834 RVA: 0x000759EC File Offset: 0x00073BEC
		internal Attachment()
		{
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains a file path to use to create this attachment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileName" /> is empty.</exception>
		// Token: 0x060016CB RID: 5835 RVA: 0x00075A04 File Offset: 0x00073C04
		public Attachment(string fileName)
			: base(fileName)
		{
			this.Name = AttachmentBase.ShortNameFromFile(fileName);
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string and MIME type information.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x060016CC RID: 5836 RVA: 0x00075A29 File Offset: 0x00073C29
		public Attachment(string fileName, string mediaType)
			: base(fileName, mediaType)
		{
			this.Name = AttachmentBase.ShortNameFromFile(fileName);
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string and <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains a file path to use to create this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> that describes the data in <paramref name="fileName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x060016CD RID: 5837 RVA: 0x00075A50 File Offset: 0x00073C50
		public Attachment(string fileName, ContentType contentType)
			: base(fileName, contentType)
		{
			if (contentType.Name == null || contentType.Name == string.Empty)
			{
				this.Name = AttachmentBase.ShortNameFromFile(fileName);
			}
			else
			{
				this.Name = contentType.Name;
			}
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream and name.</summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the value for the <see cref="P:System.Net.Mime.ContentType.Name" /> property of the <see cref="T:System.Net.Mime.ContentType" /> associated with this attachment. This value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		// Token: 0x060016CE RID: 5838 RVA: 0x00075AA9 File Offset: 0x00073CA9
		public Attachment(Stream contentStream, string name)
			: base(contentStream, null, null)
		{
			this.Name = name;
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream, name, and MIME type information.</summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the value for the <see cref="P:System.Net.Mime.ContentType.Name" /> property of the <see cref="T:System.Net.Mime.ContentType" /> associated with this attachment. This value can be <see langword="null" />.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x060016CF RID: 5839 RVA: 0x00075ACB File Offset: 0x00073CCB
		public Attachment(Stream contentStream, string name, string mediaType)
			: base(contentStream, null, mediaType)
		{
			this.Name = name;
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream and content type.</summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> that describes the data in <paramref name="contentStream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentType" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="contentStream" /> is <see langword="null" />.</exception>
		// Token: 0x060016D0 RID: 5840 RVA: 0x00075AED File Offset: 0x00073CED
		public Attachment(Stream contentStream, ContentType contentType)
			: base(contentStream, contentType)
		{
			this.Name = contentType.Name;
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00075B14 File Offset: 0x00073D14
		internal void SetContentTypeName(bool allowUnicode)
		{
			if (!allowUnicode && this.name != null && this.name.Length != 0 && !MimeBasePart.IsAscii(this.name, false))
			{
				Encoding encoding = this.NameEncoding;
				if (encoding == null)
				{
					encoding = Encoding.GetEncoding("utf-8");
				}
				base.MimePart.ContentType.Name = MimeBasePart.EncodeHeaderValue(this.name, encoding, MimeBasePart.ShouldUseBase64Encoding(encoding));
				return;
			}
			base.MimePart.ContentType.Name = this.name;
		}

		/// <summary>Gets or sets the MIME content type name value in the content type associated with this attachment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value for the content type <paramref name="name" /> represented by the <see cref="P:System.Net.Mime.ContentType.Name" /> property.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is <see cref="F:System.String.Empty" /> ("").</exception>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x00075B95 File Offset: 0x00073D95
		// (set) Token: 0x060016D3 RID: 5843 RVA: 0x00075BA0 File Offset: 0x00073DA0
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				Encoding encoding = MimeBasePart.DecodeEncoding(value);
				if (encoding != null)
				{
					this.nameEncoding = encoding;
					this.name = MimeBasePart.DecodeHeaderValue(value);
					base.MimePart.ContentType.Name = value;
					return;
				}
				this.name = value;
				this.SetContentTypeName(true);
			}
		}

		/// <summary>Specifies the encoding for the <see cref="T:System.Net.Mail.Attachment" /><see cref="P:System.Net.Mail.Attachment.Name" />.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> value that specifies the type of name encoding. The default value is determined from the name of the attachment.</returns>
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x00075BEA File Offset: 0x00073DEA
		// (set) Token: 0x060016D5 RID: 5845 RVA: 0x00075BF2 File Offset: 0x00073DF2
		public Encoding NameEncoding
		{
			get
			{
				return this.nameEncoding;
			}
			set
			{
				this.nameEncoding = value;
				if (this.name != null && this.name != string.Empty)
				{
					this.SetContentTypeName(true);
				}
			}
		}

		/// <summary>Gets the MIME content disposition for this attachment.</summary>
		/// <returns>A <see cref="T:System.Net.Mime.ContentDisposition" /> that provides the presentation information for this attachment.</returns>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x00075C1C File Offset: 0x00073E1C
		public ContentDisposition ContentDisposition
		{
			get
			{
				return base.MimePart.ContentDisposition;
			}
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00075C29 File Offset: 0x00073E29
		internal override void PrepareForSending(bool allowUnicode)
		{
			if (this.name != null && this.name != string.Empty)
			{
				this.SetContentTypeName(allowUnicode);
			}
			base.PrepareForSending(allowUnicode);
		}

		/// <summary>Creates a mail attachment using the content from the specified string, and the specified MIME content type name.</summary>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="name">The MIME content type name value in the content type associated with this attachment.</param>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		// Token: 0x060016D8 RID: 5848 RVA: 0x00075C54 File Offset: 0x00073E54
		public static Attachment CreateAttachmentFromString(string content, string name)
		{
			Attachment attachment = new Attachment();
			attachment.SetContentFromString(content, null, string.Empty);
			attachment.Name = name;
			return attachment;
		}

		/// <summary>Creates a mail attachment using the content from the specified string, the specified MIME content type name, character encoding, and MIME header information for the attachment.</summary>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="name">The MIME content type name value in the content type associated with this attachment.</param>
		/// <param name="contentEncoding">An <see cref="T:System.Text.Encoding" />. This value can be <see langword="null" />.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be <see langword="null" />.</param>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		// Token: 0x060016D9 RID: 5849 RVA: 0x00075C7C File Offset: 0x00073E7C
		public static Attachment CreateAttachmentFromString(string content, string name, Encoding contentEncoding, string mediaType)
		{
			Attachment attachment = new Attachment();
			attachment.SetContentFromString(content, contentEncoding, mediaType);
			attachment.Name = name;
			return attachment;
		}

		/// <summary>Creates a mail attachment using the content from the specified string, and the specified <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> object that represents the Multipurpose Internet Mail Exchange (MIME) protocol Content-Type header to be used.</param>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		// Token: 0x060016DA RID: 5850 RVA: 0x00075CA0 File Offset: 0x00073EA0
		public static Attachment CreateAttachmentFromString(string content, ContentType contentType)
		{
			Attachment attachment = new Attachment();
			attachment.SetContentFromString(content, contentType);
			attachment.Name = contentType.Name;
			return attachment;
		}

		// Token: 0x0400175A RID: 5978
		private string name;

		// Token: 0x0400175B RID: 5979
		private Encoding nameEncoding;
	}
}
