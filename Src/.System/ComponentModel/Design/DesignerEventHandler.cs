using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerCreated" /> and <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerDisposed" /> events that are raised when a document is created or disposed of.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.DesignerEventArgs" /> that contains the event data.</param>
	// Token: 0x020005DE RID: 1502
	// (Invoke) Token: 0x060037BC RID: 14268
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void DesignerEventHandler(object sender, DesignerEventArgs e);
}
