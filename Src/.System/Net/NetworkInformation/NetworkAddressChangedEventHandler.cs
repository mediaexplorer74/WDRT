using System;

namespace System.Net.NetworkInformation
{
	/// <summary>References one or more methods to be called when the address of a network interface changes.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains data about the event.</param>
	// Token: 0x020002DD RID: 733
	// (Invoke) Token: 0x060019D7 RID: 6615
	[global::__DynamicallyInvokable]
	public delegate void NetworkAddressChangedEventHandler(object sender, EventArgs e);
}
