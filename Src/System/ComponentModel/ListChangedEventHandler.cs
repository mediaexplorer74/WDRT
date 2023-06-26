using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event of the <see cref="T:System.ComponentModel.IBindingList" /> class.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data.</param>
	// Token: 0x02000585 RID: 1413
	// (Invoke) Token: 0x06003426 RID: 13350
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void ListChangedEventHandler(object sender, ListChangedEventArgs e);
}
