using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the <see langword="IPersistFile" /> interface, with functionality from <see langword="IPersist" />.</summary>
	// Token: 0x02000A30 RID: 2608
	[Guid("0000010b-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IPersistFile
	{
		/// <summary>Retrieves the class identifier (CLSID) of an object.</summary>
		/// <param name="pClassID">When this method returns, contains a reference to the CLSID. This parameter is passed uninitialized.</param>
		// Token: 0x06006662 RID: 26210
		[__DynamicallyInvokable]
		void GetClassID(out Guid pClassID);

		/// <summary>Checks an object for changes since it was last saved to its current file.</summary>
		/// <returns>
		///   <see langword="S_OK" /> if the file has changed since it was last saved; <see langword="S_FALSE" /> if the file has not changed since it was last saved.</returns>
		// Token: 0x06006663 RID: 26211
		[__DynamicallyInvokable]
		[PreserveSig]
		int IsDirty();

		/// <summary>Opens the specified file and initializes an object from the file contents.</summary>
		/// <param name="pszFileName">A zero-terminated string containing the absolute path of the file to open.</param>
		/// <param name="dwMode">A combination of values from the <see langword="STGM" /> enumeration to indicate the access mode in which to open <paramref name="pszFileName" />.</param>
		// Token: 0x06006664 RID: 26212
		[__DynamicallyInvokable]
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		/// <summary>Saves a copy of the object into the specified file.</summary>
		/// <param name="pszFileName">A zero-terminated string containing the absolute path of the file to which the object is saved.</param>
		/// <param name="fRemember">
		///   <see langword="true" /> to used the <paramref name="pszFileName" /> parameter as the current working file; otherwise <see langword="false" />.</param>
		// Token: 0x06006665 RID: 26213
		[__DynamicallyInvokable]
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		/// <summary>Notifies the object that it can write to its file.</summary>
		/// <param name="pszFileName">The absolute path of the file where the object was previously saved.</param>
		// Token: 0x06006666 RID: 26214
		[__DynamicallyInvokable]
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		/// <summary>Retrieves either the absolute path to the current working file of the object or, if there is no current working file, the default file name prompt of the object.</summary>
		/// <param name="ppszFileName">When this method returns, contains the address of a pointer to a zero-terminated string containing the path for the current file, or the default file name prompt (such as *.txt). This parameter is passed uninitialized.</param>
		// Token: 0x06006667 RID: 26215
		[__DynamicallyInvokable]
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}
