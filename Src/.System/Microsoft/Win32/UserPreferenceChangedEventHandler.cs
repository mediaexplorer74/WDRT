using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Represents the method that will handle the <see cref="E:Microsoft.Win32.SystemEvents.UserPreferenceChanged" /> event.</summary>
	/// <param name="sender">The source of the event. When this event is raised by the <see cref="T:Microsoft.Win32.SystemEvents" /> class, this object is always <see langword="null" />.</param>
	/// <param name="e">A <see cref="T:Microsoft.Win32.UserPreferenceChangedEventArgs" /> that contains the event data.</param>
	// Token: 0x02000024 RID: 36
	// (Invoke) Token: 0x0600026B RID: 619
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public delegate void UserPreferenceChangedEventHandler(object sender, UserPreferenceChangedEventArgs e);
}
