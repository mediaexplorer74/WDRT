using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdding" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdded" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoving" />, and <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoved" /> events raised for component-level events.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentEventArgs" /> that contains the event data.</param>
	// Token: 0x020005D0 RID: 1488
	// (Invoke) Token: 0x06003764 RID: 14180
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void ComponentEventHandler(object sender, ComponentEventArgs e);
}
