using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event. This class cannot be inherited.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.DoWorkEventArgs" /> that contains the event data.</param>
	// Token: 0x02000549 RID: 1353
	// (Invoke) Token: 0x060032D6 RID: 13014
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void DoWorkEventHandler(object sender, DoWorkEventArgs e);
}
