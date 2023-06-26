using System;

namespace System.Net.Mime
{
	/// <summary>Specifies the media type information for an email message attachment.</summary>
	// Token: 0x02000248 RID: 584
	public static class MediaTypeNames
	{
		/// <summary>Specifies the type of text data in an email message attachment.</summary>
		// Token: 0x02000792 RID: 1938
		public static class Text
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in plain text format.</summary>
			// Token: 0x04003361 RID: 13153
			public const string Plain = "text/plain";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in HTML format.</summary>
			// Token: 0x04003362 RID: 13154
			public const string Html = "text/html";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in XML format.</summary>
			// Token: 0x04003363 RID: 13155
			public const string Xml = "text/xml";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in Rich Text Format (RTF).</summary>
			// Token: 0x04003364 RID: 13156
			public const string RichText = "text/richtext";
		}

		/// <summary>Specifies the kind of application data in an email message attachment.</summary>
		// Token: 0x02000793 RID: 1939
		public static class Application
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is a SOAP document.</summary>
			// Token: 0x04003365 RID: 13157
			public const string Soap = "application/soap+xml";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is not interpreted.</summary>
			// Token: 0x04003366 RID: 13158
			public const string Octet = "application/octet-stream";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is in Rich Text Format (RTF).</summary>
			// Token: 0x04003367 RID: 13159
			public const string Rtf = "application/rtf";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is in Portable Document Format (PDF).</summary>
			// Token: 0x04003368 RID: 13160
			public const string Pdf = "application/pdf";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is compressed.</summary>
			// Token: 0x04003369 RID: 13161
			public const string Zip = "application/zip";
		}

		/// <summary>Specifies the type of image data in an email message attachment.</summary>
		// Token: 0x02000794 RID: 1940
		public static class Image
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Graphics Interchange Format (GIF).</summary>
			// Token: 0x0400336A RID: 13162
			public const string Gif = "image/gif";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Tagged Image File Format (TIFF).</summary>
			// Token: 0x0400336B RID: 13163
			public const string Tiff = "image/tiff";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Joint Photographic Experts Group (JPEG) format.</summary>
			// Token: 0x0400336C RID: 13164
			public const string Jpeg = "image/jpeg";
		}
	}
}
