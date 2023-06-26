using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Represents the method that handles the <see cref="E:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.ResolveName" /> event of a serialization manager.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.Serialization.ResolveNameEventArgs" /> that contains the event data.</param>
	// Token: 0x02000612 RID: 1554
	// (Invoke) Token: 0x060038D7 RID: 14551
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void ResolveNameEventHandler(object sender, ResolveNameEventArgs e);
}
