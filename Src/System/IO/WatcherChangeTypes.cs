using System;

namespace System.IO
{
	/// <summary>Changes that might occur to a file or directory.</summary>
	// Token: 0x02000407 RID: 1031
	[Flags]
	public enum WatcherChangeTypes
	{
		/// <summary>The creation of a file or folder.</summary>
		// Token: 0x040020D7 RID: 8407
		Created = 1,
		/// <summary>The deletion of a file or folder.</summary>
		// Token: 0x040020D8 RID: 8408
		Deleted = 2,
		/// <summary>The change of a file or folder. The types of changes include: changes to size, attributes, security settings, last write, and last access time.</summary>
		// Token: 0x040020D9 RID: 8409
		Changed = 4,
		/// <summary>The renaming of a file or folder.</summary>
		// Token: 0x040020DA RID: 8410
		Renamed = 8,
		/// <summary>The creation, deletion, change, or renaming of a file or folder.</summary>
		// Token: 0x040020DB RID: 8411
		All = 15
	}
}
