using System;

namespace System.Net
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Net.WebClient.DownloadStringCompleted" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Net.DownloadStringCompletedEventArgs" /> that contains event data.</param>
	// Token: 0x0200016D RID: 365
	// (Invoke) Token: 0x06000DD1 RID: 3537
	public delegate void DownloadStringCompletedEventHandler(object sender, DownloadStringCompletedEventArgs e);
}
