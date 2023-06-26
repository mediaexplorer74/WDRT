using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.WebBrowser.ProgressChanged" /> event.</summary>
	// Token: 0x0200043D RID: 1085
	public class WebBrowserProgressChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowserProgressChangedEventArgs" /> class.</summary>
		/// <param name="currentProgress">The number of bytes that are loaded already.</param>
		/// <param name="maximumProgress">The total number of bytes to be loaded.</param>
		// Token: 0x06004B81 RID: 19329 RVA: 0x0013A507 File Offset: 0x00138707
		public WebBrowserProgressChangedEventArgs(long currentProgress, long maximumProgress)
		{
			this.currentProgress = currentProgress;
			this.maximumProgress = maximumProgress;
		}

		/// <summary>Gets the number of bytes that have been downloaded.</summary>
		/// <returns>The number of bytes that have been loaded or -1 to indicate that the download has completed.</returns>
		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06004B82 RID: 19330 RVA: 0x0013A51D File Offset: 0x0013871D
		public long CurrentProgress
		{
			get
			{
				return this.currentProgress;
			}
		}

		/// <summary>Gets the total number of bytes in the document being loaded.</summary>
		/// <returns>The total number of bytes to be loaded.</returns>
		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06004B83 RID: 19331 RVA: 0x0013A525 File Offset: 0x00138725
		public long MaximumProgress
		{
			get
			{
				return this.maximumProgress;
			}
		}

		// Token: 0x0400282C RID: 10284
		private long currentProgress;

		// Token: 0x0400282D RID: 10285
		private long maximumProgress;
	}
}
