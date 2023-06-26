using System;
using System.Security.Permissions;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
	// Token: 0x02000404 RID: 1028
	public class RenamedEventArgs : FileSystemEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.RenamedEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values.</param>
		/// <param name="directory">The name of the affected file or directory.</param>
		/// <param name="name">The name of the affected file or directory.</param>
		/// <param name="oldName">The old name of the affected file or directory.</param>
		// Token: 0x06002692 RID: 9874 RVA: 0x000B1BE5 File Offset: 0x000AFDE5
		public RenamedEventArgs(WatcherChangeTypes changeType, string directory, string name, string oldName)
			: base(changeType, directory, name)
		{
			if (!directory.EndsWith("\\", StringComparison.Ordinal))
			{
				directory += "\\";
			}
			this.oldName = oldName;
			this.oldFullPath = directory + oldName;
		}

		/// <summary>Gets the previous fully qualified path of the affected file or directory.</summary>
		/// <returns>The previous fully qualified path of the affected file or directory.</returns>
		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x000B1C21 File Offset: 0x000AFE21
		public string OldFullPath
		{
			get
			{
				new FileIOPermission(FileIOPermissionAccess.Read, Path.GetPathRoot(this.oldFullPath)).Demand();
				return this.oldFullPath;
			}
		}

		/// <summary>Gets the old name of the affected file or directory.</summary>
		/// <returns>The previous name of the affected file or directory.</returns>
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x000B1C3F File Offset: 0x000AFE3F
		public string OldName
		{
			get
			{
				return this.oldName;
			}
		}

		// Token: 0x040020CF RID: 8399
		private string oldName;

		// Token: 0x040020D0 RID: 8400
		private string oldFullPath;
	}
}
