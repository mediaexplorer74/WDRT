using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Contains constants for controlling the kind of access other <see cref="T:System.IO.FileStream" /> objects can have to the same file.</summary>
	// Token: 0x0200018A RID: 394
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FileShare
	{
		/// <summary>Declines sharing of the current file. Any request to open the file (by this process or another process) will fail until the file is closed.</summary>
		// Token: 0x04000850 RID: 2128
		None = 0,
		/// <summary>Allows subsequent opening of the file for reading. If this flag is not specified, any request to open the file for reading (by this process or another process) will fail until the file is closed. However, even if this flag is specified, additional permissions might still be needed to access the file.</summary>
		// Token: 0x04000851 RID: 2129
		Read = 1,
		/// <summary>Allows subsequent opening of the file for writing. If this flag is not specified, any request to open the file for writing (by this process or another process) will fail until the file is closed. However, even if this flag is specified, additional permissions might still be needed to access the file.</summary>
		// Token: 0x04000852 RID: 2130
		Write = 2,
		/// <summary>Allows subsequent opening of the file for reading or writing. If this flag is not specified, any request to open the file for reading or writing (by this process or another process) will fail until the file is closed. However, even if this flag is specified, additional permissions might still be needed to access the file.</summary>
		// Token: 0x04000853 RID: 2131
		ReadWrite = 3,
		/// <summary>Allows subsequent deleting of a file.</summary>
		// Token: 0x04000854 RID: 2132
		Delete = 4,
		/// <summary>Makes the file handle inheritable by child processes. This is not directly supported by Win32.</summary>
		// Token: 0x04000855 RID: 2133
		Inheritable = 16
	}
}
