using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Defines constants for read, write, or read/write access to a file.</summary>
	// Token: 0x02000183 RID: 387
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FileAccess
	{
		/// <summary>Read access to the file. Data can be read from the file. Combine with <see langword="Write" /> for read/write access.</summary>
		// Token: 0x04000831 RID: 2097
		Read = 1,
		/// <summary>Write access to the file. Data can be written to the file. Combine with <see langword="Read" /> for read/write access.</summary>
		// Token: 0x04000832 RID: 2098
		Write = 2,
		/// <summary>Read and write access to the file. Data can be written to and read from the file.</summary>
		// Token: 0x04000833 RID: 2099
		ReadWrite = 3
	}
}
