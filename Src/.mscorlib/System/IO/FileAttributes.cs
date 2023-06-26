using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Provides attributes for files and directories.</summary>
	// Token: 0x02000196 RID: 406
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FileAttributes
	{
		/// <summary>The file is read-only.</summary>
		// Token: 0x040008A3 RID: 2211
		ReadOnly = 1,
		/// <summary>The file is hidden, and thus is not included in an ordinary directory listing.</summary>
		// Token: 0x040008A4 RID: 2212
		Hidden = 2,
		/// <summary>The file is a system file. That is, the file is part of the operating system or is used exclusively by the operating system.</summary>
		// Token: 0x040008A5 RID: 2213
		System = 4,
		/// <summary>The file is a directory.</summary>
		// Token: 0x040008A6 RID: 2214
		Directory = 16,
		/// <summary>The file is a candidate for backup or removal.</summary>
		// Token: 0x040008A7 RID: 2215
		Archive = 32,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x040008A8 RID: 2216
		Device = 64,
		/// <summary>The file is a standard file that has no special attributes. This attribute is valid only if it is used alone.</summary>
		// Token: 0x040008A9 RID: 2217
		Normal = 128,
		/// <summary>The file is temporary. A temporary file contains data that is needed while an application is executing but is not needed after the application is finished. File systems try to keep all the data in memory for quicker access rather than flushing the data back to mass storage. A temporary file should be deleted by the application as soon as it is no longer needed.</summary>
		// Token: 0x040008AA RID: 2218
		Temporary = 256,
		/// <summary>The file is a sparse file. Sparse files are typically large files whose data consists of mostly zeros.</summary>
		// Token: 0x040008AB RID: 2219
		SparseFile = 512,
		/// <summary>The file contains a reparse point, which is a block of user-defined data associated with a file or a directory.</summary>
		// Token: 0x040008AC RID: 2220
		ReparsePoint = 1024,
		/// <summary>The file is compressed.</summary>
		// Token: 0x040008AD RID: 2221
		Compressed = 2048,
		/// <summary>The file is offline. The data of the file is not immediately available.</summary>
		// Token: 0x040008AE RID: 2222
		Offline = 4096,
		/// <summary>The file will not be indexed by the operating system's content indexing service.</summary>
		// Token: 0x040008AF RID: 2223
		NotContentIndexed = 8192,
		/// <summary>The file or directory is encrypted. For a file, this means that all data in the file is encrypted. For a directory, this means that encryption is the default for newly created files and directories.</summary>
		// Token: 0x040008B0 RID: 2224
		Encrypted = 16384,
		/// <summary>The file or directory includes data integrity support. When this value is applied to a file, all data streams in the file have integrity support. When this value is applied to a directory, all new files and subdirectories within that directory, by default, include integrity support.</summary>
		// Token: 0x040008B1 RID: 2225
		[ComVisible(false)]
		IntegrityStream = 32768,
		/// <summary>The file or directory is excluded from the data integrity scan. When this value is applied to a directory, by default, all new files and subdirectories within that directory are excluded from data integrity.</summary>
		// Token: 0x040008B2 RID: 2226
		[ComVisible(false)]
		NoScrubData = 131072
	}
}
