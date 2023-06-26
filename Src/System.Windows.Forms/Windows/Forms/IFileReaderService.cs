using System;
using System.IO;

namespace System.Windows.Forms
{
	/// <summary>Defines a method that opens a file from the current directory.</summary>
	// Token: 0x0200028E RID: 654
	public interface IFileReaderService
	{
		/// <summary>Opens a file from the current directory.</summary>
		/// <param name="relativePath">The file to open.</param>
		/// <returns>A stream of the specified file.</returns>
		// Token: 0x060029A5 RID: 10661
		Stream OpenFileFromSource(string relativePath);
	}
}
