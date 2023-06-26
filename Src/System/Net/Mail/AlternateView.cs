using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents the format to view an email message.</summary>
	// Token: 0x02000254 RID: 596
	public class AlternateView : AttachmentBase
	{
		// Token: 0x0600169C RID: 5788 RVA: 0x000751F9 File Offset: 0x000733F9
		internal AlternateView()
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified file name.</summary>
		/// <param name="fileName">The name of the file that contains the content for this alternate view.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred, such as a disk error.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified file handle, such as when access is Write or ReadWrite and the file handle is set for read-only access.</exception>
		// Token: 0x0600169D RID: 5789 RVA: 0x00075201 File Offset: 0x00073401
		public AlternateView(string fileName)
			: base(fileName)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified file name and media type.</summary>
		/// <param name="fileName">The name of the file that contains the content for this alternate view.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred, such as a disk error.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified file handle, such as when access is Write or ReadWrite and the file handle is set for read-only access.</exception>
		// Token: 0x0600169E RID: 5790 RVA: 0x0007520A File Offset: 0x0007340A
		public AlternateView(string fileName, string mediaType)
			: base(fileName, mediaType)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified file name and content type.</summary>
		/// <param name="fileName">The name of the file that contains the content for this alternate view.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred, such as a disk error.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified file handle, such as when access is Write or ReadWrite and the file handle is set for read-only access.</exception>
		// Token: 0x0600169F RID: 5791 RVA: 0x00075214 File Offset: 0x00073414
		public AlternateView(string fileName, ContentType contentType)
			: base(fileName, contentType)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this view.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		// Token: 0x060016A0 RID: 5792 RVA: 0x0007521E File Offset: 0x0007341E
		public AlternateView(Stream contentStream)
			: base(contentStream)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified <see cref="T:System.IO.Stream" /> and media type.</summary>
		/// <param name="contentStream">A stream that contains the content for this attachment.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		// Token: 0x060016A1 RID: 5793 RVA: 0x00075227 File Offset: 0x00073427
		public AlternateView(Stream contentStream, string mediaType)
			: base(contentStream, mediaType)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified <see cref="T:System.IO.Stream" /> and <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this attachment.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		// Token: 0x060016A2 RID: 5794 RVA: 0x00075231 File Offset: 0x00073431
		public AlternateView(Stream contentStream, ContentType contentType)
			: base(contentStream, contentType)
		{
		}

		/// <summary>Gets the set of embedded resources referred to by this attachment.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResourceCollection" /> object that stores the collection of linked resources to be sent as part of an email message.</returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0007523B File Offset: 0x0007343B
		public LinkedResourceCollection LinkedResources
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (this.linkedResources == null)
				{
					this.linkedResources = new LinkedResourceCollection();
				}
				return this.linkedResources;
			}
		}

		/// <summary>Gets or sets the base URI to use for resolving relative URIs in the <see cref="T:System.Net.Mail.AlternateView" />.</summary>
		/// <returns>The base URI to use for resolving relative URIs in the <see cref="T:System.Net.Mail.AlternateView" />.</returns>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x0007526F File Offset: 0x0007346F
		// (set) Token: 0x060016A5 RID: 5797 RVA: 0x00075277 File Offset: 0x00073477
		public Uri BaseUri
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

		/// <summary>Creates a <see cref="T:System.Net.Mail.AlternateView" /> of an email message using the content specified in a <see cref="T:System.String" />.</summary>
		/// <param name="content">The <see cref="T:System.String" /> that contains the content of the email message.</param>
		/// <returns>An <see cref="T:System.Net.Mail.AlternateView" /> object that represents an alternate view of an email message.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="content" /> is null.</exception>
		// Token: 0x060016A6 RID: 5798 RVA: 0x00075280 File Offset: 0x00073480
		public static AlternateView CreateAlternateViewFromString(string content)
		{
			AlternateView alternateView = new AlternateView();
			alternateView.SetContentFromString(content, null, string.Empty);
			return alternateView;
		}

		/// <summary>Creates an <see cref="T:System.Net.Mail.AlternateView" /> of an email message using the content specified in a <see cref="T:System.String" />, the specified text encoding, and MIME media type of the content.</summary>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="contentEncoding">An <see cref="T:System.Text.Encoding" />. This value can be <see langword="null." /></param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <returns>An <see cref="T:System.Net.Mail.AlternateView" /> object that represents an alternate view of an email message.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="content" /> is null.</exception>
		// Token: 0x060016A7 RID: 5799 RVA: 0x000752A4 File Offset: 0x000734A4
		public static AlternateView CreateAlternateViewFromString(string content, Encoding contentEncoding, string mediaType)
		{
			AlternateView alternateView = new AlternateView();
			alternateView.SetContentFromString(content, contentEncoding, mediaType);
			return alternateView;
		}

		/// <summary>Creates an <see cref="T:System.Net.Mail.AlternateView" /> of an email message using the content specified in a <see cref="T:System.String" /> and the specified MIME media type of the content.</summary>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> that describes the data in <paramref name="content" />.</param>
		/// <returns>An <see cref="T:System.Net.Mail.AlternateView" /> object that represents an alternate view of an email message.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="content" /> is null.</exception>
		// Token: 0x060016A8 RID: 5800 RVA: 0x000752C4 File Offset: 0x000734C4
		public static AlternateView CreateAlternateViewFromString(string content, ContentType contentType)
		{
			AlternateView alternateView = new AlternateView();
			alternateView.SetContentFromString(content, contentType);
			return alternateView;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Mail.AlternateView" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060016A9 RID: 5801 RVA: 0x000752E0 File Offset: 0x000734E0
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing && this.linkedResources != null)
			{
				this.linkedResources.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04001756 RID: 5974
		private LinkedResourceCollection linkedResources;
	}
}
