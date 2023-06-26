using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Represents the method that will handle the <see cref="E:Microsoft.Win32.SystemEvents.SessionSwitch" /> event.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:Microsoft.Win32.SessionSwitchEventArgs" /> indicating the type of the session change event.</param>
	// Token: 0x0200001C RID: 28
	// (Invoke) Token: 0x060001C4 RID: 452
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public delegate void SessionSwitchEventHandler(object sender, SessionSwitchEventArgs e);
}
