using System;

namespace System.Net
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Net.WebClient.DownloadProgressChanged" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Net.DownloadProgressChangedEventArgs" /> containing event data.</param>
	// Token: 0x02000179 RID: 377
	// (Invoke) Token: 0x06000DF5 RID: 3573
	public delegate void DownloadProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs e);
}
