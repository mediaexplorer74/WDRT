using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Specifies whether to search the current directory, or the current directory and all subdirectories.</summary>
	// Token: 0x0200017C RID: 380
	[ComVisible(true)]
	[Serializable]
	public enum SearchOption
	{
		/// <summary>Includes only the current directory in a search operation.</summary>
		// Token: 0x04000821 RID: 2081
		TopDirectoryOnly,
		/// <summary>Includes the current directory and all its subdirectories in a search operation. This option includes reparse points such as mounted drives and symbolic links in the search.</summary>
		// Token: 0x04000822 RID: 2082
		AllDirectories
	}
}
