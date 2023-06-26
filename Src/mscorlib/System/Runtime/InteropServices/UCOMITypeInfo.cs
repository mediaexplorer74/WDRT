using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeInfo" /> instead.</summary>
	// Token: 0x020009A2 RID: 2466
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeInfo instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00020401-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMITypeInfo
	{
		/// <summary>Retrieves a <see cref="T:System.Runtime.InteropServices.TYPEATTR" /> structure that contains the attributes of the type description.</summary>
		/// <param name="ppTypeAttr">On successful return, a reference to the structure that contains the attributes of this type description.</param>
		// Token: 0x060062F5 RID: 25333
		void GetTypeAttr(out IntPtr ppTypeAttr);

		/// <summary>Retrieves the <see langword="ITypeComp" /> interface for the type description, which enables a client compiler to bind to the type description's members.</summary>
		/// <param name="ppTComp">On successful return, a reference to the <see langword="UCOMITypeComp" /> of the containing type library.</param>
		// Token: 0x060062F6 RID: 25334
		void GetTypeComp(out UCOMITypeComp ppTComp);

		/// <summary>Retrieves the <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> structure that contains information about a specified function.</summary>
		/// <param name="index">Index of the function description to return.</param>
		/// <param name="ppFuncDesc">Reference to a <see langword="FUNCDESC" /> that describes the specified function.</param>
		// Token: 0x060062F7 RID: 25335
		void GetFuncDesc(int index, out IntPtr ppFuncDesc);

		/// <summary>Retrieves a <see langword="VARDESC" /> structure that describes the specified variable.</summary>
		/// <param name="index">Index of the variable description to return.</param>
		/// <param name="ppVarDesc">On successful return, a reference to the <see langword="VARDESC" /> that describes the specified variable.</param>
		// Token: 0x060062F8 RID: 25336
		void GetVarDesc(int index, out IntPtr ppVarDesc);

		/// <summary>Retrieves the variable with the specified member ID (or the name of the property or method and its parameters) that correspond to the specified function ID.</summary>
		/// <param name="memid">The ID of the member whose name (or names) is to be returned.</param>
		/// <param name="rgBstrNames">On succesful return, contains the name (or names) associated with the member.</param>
		/// <param name="cMaxNames">Length of the <paramref name="rgBstrNames" /> array.</param>
		/// <param name="pcNames">On succesful return, the number of names in the <paramref name="rgBstrNames" /> array.</param>
		// Token: 0x060062F9 RID: 25337
		void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

		/// <summary>If a type description describes a COM class, it retrieves the type description of the implemented interface types.</summary>
		/// <param name="index">Index of the implemented type whose handle is returned.</param>
		/// <param name="href">Reference to a handle for the implemented interface.</param>
		// Token: 0x060062FA RID: 25338
		void GetRefTypeOfImplType(int index, out int href);

		/// <summary>Retrieves the <see cref="T:System.Runtime.InteropServices.IMPLTYPEFLAGS" /> value for one implemented interface or base interface in a type description.</summary>
		/// <param name="index">Index of the implemented interface or base interface.</param>
		/// <param name="pImplTypeFlags">On successful return, a reference to the <see langword="IMPLTYPEFLAGS" /> enumeration.</param>
		// Token: 0x060062FB RID: 25339
		void GetImplTypeFlags(int index, out int pImplTypeFlags);

		/// <summary>Maps between member names and member IDs, and parameter names and parameter IDs.</summary>
		/// <param name="rgszNames">On succesful return, an array of names to map.</param>
		/// <param name="cNames">Count of names to map.</param>
		/// <param name="pMemId">Reference to an array in which name mappings are placed.</param>
		// Token: 0x060062FC RID: 25340
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] [In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] int[] pMemId);

		/// <summary>Invokes a method, or accesses a property of an object, that implements the interface described by the type description.</summary>
		/// <param name="pvInstance">Reference to the interface described by this type description.</param>
		/// <param name="memid">Identifies the interface member.</param>
		/// <param name="wFlags">Flags describing the context of the invoke call.</param>
		/// <param name="pDispParams">Reference to a structure that contains an array of arguments, an array of DISPIDs for named arguments, and counts of the number of elements in each array.</param>
		/// <param name="pVarResult">Reference to the location at which the result is to be stored. If <paramref name="wFlags" /> specifies <see langword="DISPATCH_PROPERTYPUT" /> or <see langword="DISPATCH_PROPERTYPUTREF" />, <paramref name="pVarResult" /> is ignored. Set to <see langword="null" /> if no result is desired.</param>
		/// <param name="pExcepInfo">Points to an exception information structure, which is filled in only if <see langword="DISP_E_EXCEPTION" /> is returned.</param>
		/// <param name="puArgErr">If <see langword="Invoke" /> returns <see langword="DISP_E_TYPEMISMATCH" />, <paramref name="puArgErr" /> indicates the index within rgvarg of the argument with incorrect type. If more than one argument returns an error, <paramref name="puArgErr" /> indicates only the first argument with an error.</param>
		// Token: 0x060062FD RID: 25341
		void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, out object pVarResult, out EXCEPINFO pExcepInfo, out int puArgErr);

		/// <summary>Retrieves the documentation string, the complete Help file name and path, and the context ID for the Help topic for a specified type description.</summary>
		/// <param name="index">ID of the member whose documentation is to be returned.</param>
		/// <param name="strName">On successful return, the name of the item method.</param>
		/// <param name="strDocString">On successful return, the documentation string for the specified item.</param>
		/// <param name="dwHelpContext">On successful return, a reference to the Help context associated with the specified item.</param>
		/// <param name="strHelpFile">On successful return, the fully qualified name of the Help file.</param>
		// Token: 0x060062FE RID: 25342
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		/// <summary>Retrieves a description or specification of an entry point for a function in a DLL.</summary>
		/// <param name="memid">ID of the member function whose DLL entry description is to be returned.</param>
		/// <param name="invKind">Specifies the kind of member identified by <paramref name="memid" />.</param>
		/// <param name="pBstrDllName">If not <see langword="null" />, the function sets <paramref name="pBstrDllName" /> to a <see langword="BSTR" /> that contains the name of the DLL.</param>
		/// <param name="pBstrName">If not <see langword="null" />, the function sets lpbstrName to a <see langword="BSTR" /> that contains the name of the entry point.</param>
		/// <param name="pwOrdinal">If not <see langword="null" />, and the function is defined by an ordinal, then lpwOrdinal is set to point to the ordinal.</param>
		// Token: 0x060062FF RID: 25343
		void GetDllEntry(int memid, INVOKEKIND invKind, out string pBstrDllName, out string pBstrName, out short pwOrdinal);

		/// <summary>If a type description references other type descriptions, it retrieves the referenced type descriptions.</summary>
		/// <param name="hRef">Handle to the referenced type description to return.</param>
		/// <param name="ppTI">On successful return, the referenced type description.</param>
		// Token: 0x06006300 RID: 25344
		void GetRefTypeInfo(int hRef, out UCOMITypeInfo ppTI);

		/// <summary>Retrieves the addresses of static functions or variables, such as those defined in a DLL.</summary>
		/// <param name="memid">Member ID of the <see langword="static" /> member's address to retrieve.</param>
		/// <param name="invKind">Specifies whether the member is a property, and if so, what kind.</param>
		/// <param name="ppv">On successful return, a reference to the <see langword="static" /> member.</param>
		// Token: 0x06006301 RID: 25345
		void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

		/// <summary>Creates a new instance of a type that describes a component class (coclass).</summary>
		/// <param name="pUnkOuter">Object which acts as the controlling <see langword="IUnknown" />.</param>
		/// <param name="riid">The IID of the interface that the caller will use to communicate with the resulting object.</param>
		/// <param name="ppvObj">On successful return, a reference to the created object.</param>
		// Token: 0x06006302 RID: 25346
		void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		/// <summary>Retrieves marshaling information.</summary>
		/// <param name="memid">The member ID that indicates which marshaling information is needed.</param>
		/// <param name="pBstrMops">A reference to the opcode string used in marshaling the fields of the structure described by the referenced type description, or returns <see langword="null" /> if there is no information to return.</param>
		// Token: 0x06006303 RID: 25347
		void GetMops(int memid, out string pBstrMops);

		/// <summary>Retrieves the type library that contains this type description and its index within that type library.</summary>
		/// <param name="ppTLB">On successful return, a reference to the containing type library.</param>
		/// <param name="pIndex">On successful return, a reference to the index of the type description within the containing type library.</param>
		// Token: 0x06006304 RID: 25348
		void GetContainingTypeLib(out UCOMITypeLib ppTLB, out int pIndex);

		/// <summary>Releases a <see cref="T:System.Runtime.InteropServices.TYPEATTR" /> previously returned by <see cref="M:System.Runtime.InteropServices.UCOMITypeInfo.GetTypeAttr(System.IntPtr@)" />.</summary>
		/// <param name="pTypeAttr">Reference to the <see langword="TYPEATTR" /> to release.</param>
		// Token: 0x06006305 RID: 25349
		void ReleaseTypeAttr(IntPtr pTypeAttr);

		/// <summary>Releases a <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> previously returned by <see cref="M:System.Runtime.InteropServices.UCOMITypeInfo.GetFuncDesc(System.Int32,System.IntPtr@)" />.</summary>
		/// <param name="pFuncDesc">Reference to the <see langword="FUNCDESC" /> to release.</param>
		// Token: 0x06006306 RID: 25350
		void ReleaseFuncDesc(IntPtr pFuncDesc);

		/// <summary>Releases a <see langword="VARDESC" /> previously returned by <see cref="M:System.Runtime.InteropServices.UCOMITypeInfo.GetVarDesc(System.Int32,System.IntPtr@)" />.</summary>
		/// <param name="pVarDesc">Reference to the <see langword="VARDESC" /> to release.</param>
		// Token: 0x06006307 RID: 25351
		void ReleaseVarDesc(IntPtr pVarDesc);
	}
}
