using System;

namespace System.Net.NetworkInformation
{
	/// <summary>References one or more methods to be called when the availability of the network changes.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains data about the event.</param>
	// Token: 0x020002DE RID: 734
	// (Invoke) Token: 0x060019DB RID: 6619
	public delegate void NetworkAvailabilityChangedEventHandler(object sender, NetworkAvailabilityEventArgs e);
}
