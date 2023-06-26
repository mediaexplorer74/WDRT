using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event of a <see cref="T:System.Net.NetworkInformation.Ping" /> object.</summary>
	/// <param name="sender">The source of the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event.</param>
	/// <param name="e">A <see cref="T:System.Net.NetworkInformation.PingCompletedEventArgs" /> object that contains the event data.</param>
	// Token: 0x020002E8 RID: 744
	// (Invoke) Token: 0x06001A0D RID: 6669
	public delegate void PingCompletedEventHandler(object sender, PingCompletedEventArgs e);
}
