using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.WebBrowser.DocumentCompleted" /> event.</summary>
	// Token: 0x02000436 RID: 1078
	public class WebBrowserDocumentCompletedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowserDocumentCompletedEventArgs" /> class.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the location of the document that was loaded.</param>
		// Token: 0x06004B65 RID: 19301 RVA: 0x0013A261 File Offset: 0x00138461
		public WebBrowserDocumentCompletedEventArgs(Uri url)
		{
			this.url = url;
		}

		/// <summary>Gets the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control has navigated.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the location of the document that was loaded.</returns>
		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06004B66 RID: 19302 RVA: 0x0013A270 File Offset: 0x00138470
		public Uri Url
		{
			get
			{
				WebBrowser.EnsureUrlConnectPermission(this.url);
				return this.url;
			}
		}

		// Token: 0x04002816 RID: 10262
		private Uri url;
	}
}
