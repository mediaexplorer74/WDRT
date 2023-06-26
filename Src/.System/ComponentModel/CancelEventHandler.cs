using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that handles a cancelable event.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
	// Token: 0x02000521 RID: 1313
	// (Invoke) Token: 0x060031C7 RID: 12743
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void CancelEventHandler(object sender, CancelEventArgs e);
}
