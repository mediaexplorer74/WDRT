using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents a method that can handle events which may or may not require further processing after the event handler has returned.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.HandledEventArgs" /> that contains the event data.</param>
	// Token: 0x02000556 RID: 1366
	// (Invoke) Token: 0x0600334D RID: 13133
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void HandledEventHandler(object sender, HandledEventArgs e);
}
