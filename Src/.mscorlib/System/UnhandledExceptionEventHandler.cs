using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents the method that will handle the event raised by an exception that is not handled by the application domain.</summary>
	/// <param name="sender">The source of the unhandled exception event.</param>
	/// <param name="e">An UnhandledExceptionEventArgs that contains the event data.</param>
	// Token: 0x02000157 RID: 343
	// (Invoke) Token: 0x06001569 RID: 5481
	[ComVisible(true)]
	[Serializable]
	public delegate void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e);
}
