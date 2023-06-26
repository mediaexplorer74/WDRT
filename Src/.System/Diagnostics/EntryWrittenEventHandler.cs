using System;

namespace System.Diagnostics
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event of an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> that contains the event data.</param>
	// Token: 0x020004C8 RID: 1224
	// (Invoke) Token: 0x06002D9C RID: 11676
	public delegate void EntryWrittenEventHandler(object sender, EntryWrittenEventArgs e);
}
