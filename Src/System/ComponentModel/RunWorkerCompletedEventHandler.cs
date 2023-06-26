using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.BackgroundWorker.RunWorkerCompleted" /> event of a <see cref="T:System.ComponentModel.BackgroundWorker" /> class.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.RunWorkerCompletedEventArgs" /> that contains the event data.</param>
	// Token: 0x020005A8 RID: 1448
	// (Invoke) Token: 0x06003601 RID: 13825
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void RunWorkerCompletedEventHandler(object sender, RunWorkerCompletedEventArgs e);
}
