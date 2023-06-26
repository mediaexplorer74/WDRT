using System;

namespace System.IO
{
	/// <summary>Represents the method that will handle the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event of a <see cref="T:System.IO.FileSystemWatcher" /> class.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.IO.RenamedEventArgs" /> that contains the event data.</param>
	// Token: 0x02000405 RID: 1029
	// (Invoke) Token: 0x06002696 RID: 9878
	public delegate void RenamedEventHandler(object sender, RenamedEventArgs e);
}
