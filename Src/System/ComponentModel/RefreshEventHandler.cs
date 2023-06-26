using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that handles the <see cref="E:System.ComponentModel.TypeDescriptor.Refreshed" /> event raised when a <see cref="T:System.Type" /> or component is changed during design time.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.RefreshEventArgs" /> that contains the component or <see cref="T:System.Type" /> that changed.</param>
	// Token: 0x020005A5 RID: 1445
	// (Invoke) Token: 0x060035F4 RID: 13812
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void RefreshEventHandler(RefreshEventArgs e);
}
