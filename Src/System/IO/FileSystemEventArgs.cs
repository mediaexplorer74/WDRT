using System;

namespace System.IO
{
	/// <summary>Provides data for the directory events: <see cref="E:System.IO.FileSystemWatcher.Changed" />, <see cref="E:System.IO.FileSystemWatcher.Created" />, <see cref="E:System.IO.FileSystemWatcher.Deleted" />.</summary>
	// Token: 0x020003FD RID: 1021
	public class FileSystemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values, which represents the kind of change detected in the file system.</param>
		/// <param name="directory">The root directory of the affected file or directory.</param>
		/// <param name="name">The name of the affected file or directory.</param>
		// Token: 0x0600264E RID: 9806 RVA: 0x000B091D File Offset: 0x000AEB1D
		public FileSystemEventArgs(WatcherChangeTypes changeType, string directory, string name)
		{
			this.changeType = changeType;
			this.name = name;
			if (!directory.EndsWith("\\", StringComparison.Ordinal))
			{
				directory += "\\";
			}
			this.fullPath = directory + name;
		}

		/// <summary>Gets the type of directory event that occurred.</summary>
		/// <returns>One of the <see cref="T:System.IO.WatcherChangeTypes" /> values that represents the kind of change detected in the file system.</returns>
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x000B095B File Offset: 0x000AEB5B
		public WatcherChangeTypes ChangeType
		{
			get
			{
				return this.changeType;
			}
		}

		/// <summary>Gets the fully qualifed path of the affected file or directory.</summary>
		/// <returns>The path of the affected file or directory.</returns>
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x000B0963 File Offset: 0x000AEB63
		public string FullPath
		{
			get
			{
				return this.fullPath;
			}
		}

		/// <summary>Gets the name of the affected file or directory.</summary>
		/// <returns>The name of the affected file or directory.</returns>
		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x000B096B File Offset: 0x000AEB6B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040020A1 RID: 8353
		private WatcherChangeTypes changeType;

		// Token: 0x040020A2 RID: 8354
		private string name;

		// Token: 0x040020A3 RID: 8355
		private string fullPath;
	}
}
