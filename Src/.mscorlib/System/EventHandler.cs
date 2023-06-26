using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents the method that will handle an event that has no event data.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An object that contains no event data.</param>
	// Token: 0x020000E0 RID: 224
	// (Invoke) Token: 0x06000E77 RID: 3703
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public delegate void EventHandler(object sender, EventArgs e);
}
