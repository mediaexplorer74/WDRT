using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event.</summary>
	/// <param name="sender">The source of the event, typically a data container or data-bound collection.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.AddingNewEventArgs" /> that contains the event data.</param>
	// Token: 0x0200050C RID: 1292
	// (Invoke) Token: 0x060030FA RID: 12538
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void AddingNewEventHandler(object sender, AddingNewEventArgs e);
}
