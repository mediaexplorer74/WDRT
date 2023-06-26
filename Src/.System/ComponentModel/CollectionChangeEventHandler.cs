using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that handles the <see cref="E:System.Data.DataColumnCollection.CollectionChanged" /> event raised when adding elements to or removing elements from a collection.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
	// Token: 0x02000526 RID: 1318
	// (Invoke) Token: 0x060031E7 RID: 12775
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void CollectionChangeEventHandler(object sender, CollectionChangeEventArgs e);
}
