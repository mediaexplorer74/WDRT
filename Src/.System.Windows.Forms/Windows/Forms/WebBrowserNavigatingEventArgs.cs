using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.WebBrowser.Navigating" /> event.</summary>
	// Token: 0x0200043B RID: 1083
	public class WebBrowserNavigatingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowserNavigatingEventArgs" /> class.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control is navigating.</param>
		/// <param name="targetFrameName">The name of the Web page frame in which the new document will be loaded.</param>
		// Token: 0x06004B7A RID: 19322 RVA: 0x0013A4CB File Offset: 0x001386CB
		public WebBrowserNavigatingEventArgs(Uri url, string targetFrameName)
		{
			this.url = url;
			this.targetFrameName = targetFrameName;
		}

		/// <summary>Gets the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control is navigating.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control is navigating.</returns>
		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06004B7B RID: 19323 RVA: 0x0013A4E1 File Offset: 0x001386E1
		public Uri Url
		{
			get
			{
				WebBrowser.EnsureUrlConnectPermission(this.url);
				return this.url;
			}
		}

		/// <summary>Gets the name of the Web page frame in which the new document will be loaded.</summary>
		/// <returns>The name of the frame in which the new document will be loaded.</returns>
		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06004B7C RID: 19324 RVA: 0x0013A4F4 File Offset: 0x001386F4
		public string TargetFrameName
		{
			get
			{
				WebBrowser.EnsureUrlConnectPermission(this.url);
				return this.targetFrameName;
			}
		}

		// Token: 0x0400282A RID: 10282
		private Uri url;

		// Token: 0x0400282B RID: 10283
		private string targetFrameName;
	}
}
