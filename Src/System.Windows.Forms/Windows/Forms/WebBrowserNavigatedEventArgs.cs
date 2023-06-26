using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.WebBrowser.Navigated" /> event.</summary>
	// Token: 0x02000439 RID: 1081
	public class WebBrowserNavigatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowserNavigatedEventArgs" /> class.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control has navigated.</param>
		// Token: 0x06004B74 RID: 19316 RVA: 0x0013A4A9 File Offset: 0x001386A9
		public WebBrowserNavigatedEventArgs(Uri url)
		{
			this.url = url;
		}

		/// <summary>Gets the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control has navigated.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control has navigated.</returns>
		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06004B75 RID: 19317 RVA: 0x0013A4B8 File Offset: 0x001386B8
		public Uri Url
		{
			get
			{
				WebBrowser.EnsureUrlConnectPermission(this.url);
				return this.url;
			}
		}

		// Token: 0x04002829 RID: 10281
		private Uri url;
	}
}
