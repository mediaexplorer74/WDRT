using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the <see langword="ITypeLib" /> interface.</summary>
	// Token: 0x02000A50 RID: 2640
	[Guid("00020402-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface ITypeLib
	{
		/// <summary>Returns the number of type descriptions in the type library.</summary>
		/// <returns>The number of type descriptions in the type library.</returns>
		// Token: 0x0600669B RID: 26267
		[__DynamicallyInvokable]
		[PreserveSig]
		int GetTypeInfoCount();

		/// <summary>Retrieves the specified type description in the library.</summary>
		/// <param name="index">The index of the <see langword="ITypeInfo" /> interface to return.</param>
		/// <param name="ppTI">When this method returns, contains an <see langword="ITypeInfo" /> describing the type referenced by <paramref name="index" />. This parameter is passed uninitialized.</param>
		// Token: 0x0600669C RID: 26268
		[__DynamicallyInvokable]
		void GetTypeInfo(int index, out ITypeInfo ppTI);

		/// <summary>Retrieves the type of a type description.</summary>
		/// <param name="index">The index of the type description within the type library.</param>
		/// <param name="pTKind">When this method returns, contains a reference to the <see langword="TYPEKIND" /> enumeration for the type description. This parameter is passed uninitialized.</param>
		// Token: 0x0600669D RID: 26269
		[__DynamicallyInvokable]
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		/// <summary>Retrieves the type description that corresponds to the specified GUID.</summary>
		/// <param name="guid">The IID of the interface or CLSID of the class whose type info is requested.</param>
		/// <param name="ppTInfo">When this method returns, contains the requested <see langword="ITypeInfo" /> interface. This parameter is passed uninitialized.</param>
		// Token: 0x0600669E RID: 26270
		[__DynamicallyInvokable]
		void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

		/// <summary>Retrieves the structure that contains the library's attributes.</summary>
		/// <param name="ppTLibAttr">When this method returns, contains a structure that contains the library's attributes. This parameter is passed uninitialized.</param>
		// Token: 0x0600669F RID: 26271
		void GetLibAttr(out IntPtr ppTLibAttr);

		/// <summary>Enables a client compiler to bind to a library's types, variables, constants, and global functions.</summary>
		/// <param name="ppTComp">When this method returns, contains an instance of a <see langword="ITypeComp" /> instance for this <see langword="ITypeLib" />. This parameter is passed uninitialized.</param>
		// Token: 0x060066A0 RID: 26272
		[__DynamicallyInvokable]
		void GetTypeComp(out ITypeComp ppTComp);

		/// <summary>Retrieves the library's documentation string, the complete Help file name and path, and the context identifier for the library Help topic in the Help file.</summary>
		/// <param name="index">The index of the type description whose documentation is to be returned.</param>
		/// <param name="strName">When this method returns, contains a string that represents the name of the specified item. This parameter is passed uninitialized.</param>
		/// <param name="strDocString">When this method returns, contains a string that represents the documentation string for the specified item. This parameter is passed uninitialized.</param>
		/// <param name="dwHelpContext">When this method returns, contains the Help context identifier associated with the specified item. This parameter is passed uninitialized.</param>
		/// <param name="strHelpFile">When this method returns, contains a string that represents the fully qualified name of the Help file. This parameter is passed uninitialized.</param>
		// Token: 0x060066A1 RID: 26273
		[__DynamicallyInvokable]
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		/// <summary>Indicates whether a passed-in string contains the name of a type or member described in the library.</summary>
		/// <param name="szNameBuf">The string to test. This is an in/out parameter.</param>
		/// <param name="lHashVal">The hash value of <paramref name="szNameBuf" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="szNameBuf" /> was found in the type library; otherwise, <see langword="false" />.</returns>
		// Token: 0x060066A2 RID: 26274
		[__DynamicallyInvokable]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		/// <summary>Finds occurrences of a type description in a type library.</summary>
		/// <param name="szNameBuf">The name to search for. This is an in/out parameter.</param>
		/// <param name="lHashVal">A hash value to speed up the search, computed by the <see langword="LHashValOfNameSys" /> function. If <paramref name="lHashVal" /> is 0, a value is computed.</param>
		/// <param name="ppTInfo">When this method returns, contains an array of pointers to the type descriptions that contain the name specified in <paramref name="szNameBuf" />. This parameter is passed uninitialized.</param>
		/// <param name="rgMemId">An array of the <see langword="MEMBERID" /> 's of the found items; <paramref name="rgMemId" /> [i] is the <see langword="MEMBERID" /> that indexes into the type description specified by <paramref name="ppTInfo" /> [i]. Cannot be <see langword="null" />.</param>
		/// <param name="pcFound">On entry, indicates how many instances to look for. For example, <paramref name="pcFound" /> = 1 can be called to find the first occurrence. The search stops when one instance is found.  
		///  On exit, indicates the number of instances that were found. If the <see langword="in" /> and <see langword="out" /> values of <paramref name="pcFound" /> are identical, there might be more type descriptions that contain the name.</param>
		// Token: 0x060066A3 RID: 26275
		[__DynamicallyInvokable]
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		/// <summary>Releases the <see cref="T:System.Runtime.InteropServices.TYPELIBATTR" /> structure originally obtained from the <see cref="M:System.Runtime.InteropServices.ComTypes.ITypeLib.GetLibAttr(System.IntPtr@)" /> method.</summary>
		/// <param name="pTLibAttr">The <see langword="TLIBATTR" /> structure to release.</param>
		// Token: 0x060066A4 RID: 26276
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);
	}
}
