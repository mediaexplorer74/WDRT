using System;

namespace System.IO
{
	/// <summary>Represents the method that will handle the <see cref="E:System.IO.FileSystemWatcher.Error" /> event of a <see cref="T:System.IO.FileSystemWatcher" /> object.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.IO.ErrorEventArgs" /> object that contains the event data.</param>
	// Token: 0x020003FC RID: 1020
	// (Invoke) Token: 0x0600264B RID: 9803
	public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);
}
