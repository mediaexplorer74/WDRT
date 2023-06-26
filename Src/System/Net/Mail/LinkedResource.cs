using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an embedded external resource in an email attachment, such as an image in an HTML attachment.</summary>
	// Token: 0x02000268 RID: 616
	public class LinkedResource : AttachmentBase
	{
		// Token: 0x06001715 RID: 5909 RVA: 0x00076460 File Offset: 0x00074660
		internal LinkedResource()
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> using the specified file name.</summary>
		/// <param name="fileName">The file name holding the content for this embedded resource.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		// Token: 0x06001716 RID: 5910 RVA: 0x00076468 File Offset: 0x00074668
		public LinkedResource(string fileName)
			: base(fileName)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the specified file name and media type.</summary>
		/// <param name="fileName">The file name that holds the content for this embedded resource.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		// Token: 0x06001717 RID: 5911 RVA: 0x00076471 File Offset: 0x00074671
		public LinkedResource(string fileName, string mediaType)
			: base(fileName, mediaType)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the specified file name and content type.</summary>
		/// <param name="fileName">The file name that holds the content for this embedded resource.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		// Token: 0x06001718 RID: 5912 RVA: 0x0007647B File Offset: 0x0007467B
		public LinkedResource(string fileName, ContentType contentType)
			: base(fileName, contentType)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> using the supplied <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this embedded resource.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		// Token: 0x06001719 RID: 5913 RVA: 0x00076485 File Offset: 0x00074685
		public LinkedResource(Stream contentStream)
			: base(contentStream)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the specified <see cref="T:System.IO.Stream" /> and media type.</summary>
		/// <param name="contentStream">A stream that contains the content for this embedded resource.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		// Token: 0x0600171A RID: 5914 RVA: 0x0007648E File Offset: 0x0007468E
		public LinkedResource(Stream contentStream, string mediaType)
			: base(contentStream, mediaType)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the values supplied by <see cref="T:System.IO.Stream" /> and <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this embedded resource.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		// Token: 0x0600171B RID: 5915 RVA: 0x00076498 File Offset: 0x00074698
		public LinkedResource(Stream contentStream, ContentType contentType)
			: base(contentStream, contentType)
		{
		}

		/// <summary>Gets or sets a URI that the resource must match.</summary>
		/// <returns>If <see cref="P:System.Net.Mail.LinkedResource.ContentLink" /> is a relative URI, the recipient of the message must resolve it.</returns>
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x000764A2 File Offset: 0x000746A2
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x000764AA File Offset: 0x000746AA
		public Uri ContentLink
		{
			get
			{
				return base.ContentLocation;
			}
			set
			{
				base.ContentLocation = value;
			}
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.LinkedResource" /> object from a string to be included in an email attachment as an embedded resource. The default media type is plain text, and the default content type is ASCII.</summary>
		/// <param name="content">A string that contains the embedded resource to be included in the email attachment.</param>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResource" /> object that contains the embedded resource to be included in the email attachment.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified content string is null.</exception>
		// Token: 0x0600171E RID: 5918 RVA: 0x000764B4 File Offset: 0x000746B4
		public static LinkedResource CreateLinkedResourceFromString(string content)
		{
			LinkedResource linkedResource = new LinkedResource();
			linkedResource.SetContentFromString(content, null, string.Empty);
			return linkedResource;
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.LinkedResource" /> object from a string to be included in an email attachment as an embedded resource, with the specified content type, and media type.</summary>
		/// <param name="content">A string that contains the embedded resource to be included in the email attachment.</param>
		/// <param name="contentEncoding">The type of the content.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResource" /> object that contains the embedded resource to be included in the email attachment.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified content string is null.</exception>
		// Token: 0x0600171F RID: 5919 RVA: 0x000764D8 File Offset: 0x000746D8
		public static LinkedResource CreateLinkedResourceFromString(string content, Encoding contentEncoding, string mediaType)
		{
			LinkedResource linkedResource = new LinkedResource();
			linkedResource.SetContentFromString(content, contentEncoding, mediaType);
			return linkedResource;
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.LinkedResource" /> object from a string to be included in an email attachment as an embedded resource, with the specified content type, and media type as plain text.</summary>
		/// <param name="content">A string that contains the embedded resource to be included in the email attachment.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResource" /> object that contains the embedded resource to be included in the email attachment.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified content string is null.</exception>
		// Token: 0x06001720 RID: 5920 RVA: 0x000764F8 File Offset: 0x000746F8
		public static LinkedResource CreateLinkedResourceFromString(string content, ContentType contentType)
		{
			LinkedResource linkedResource = new LinkedResource();
			linkedResource.SetContentFromString(content, contentType);
			return linkedResource;
		}
	}
}
