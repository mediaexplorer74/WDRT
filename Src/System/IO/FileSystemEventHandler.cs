using System;

namespace System.IO
{
	/// <summary>Represents the method that will handle the <see cref="E:System.IO.FileSystemWatcher.Changed" />, <see cref="E:System.IO.FileSystemWatcher.Created" />, or <see cref="E:System.IO.FileSystemWatcher.Deleted" /> event of a <see cref="T:System.IO.FileSystemWatcher" /> class.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data.</param>
	// Token: 0x020003FE RID: 1022
	// (Invoke) Token: 0x06002653 RID: 9811
	public delegate void FileSystemEventHandler(object sender, FileSystemEventArgs e);
}
