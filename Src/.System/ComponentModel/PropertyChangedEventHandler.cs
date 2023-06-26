using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" /> event raised when a property is changed on a component.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data.</param>
	// Token: 0x02000598 RID: 1432
	// (Invoke) Token: 0x0600351A RID: 13594
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
}
