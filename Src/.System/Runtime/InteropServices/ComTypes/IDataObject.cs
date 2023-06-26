using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the <see langword="IDataObject" /> interface.</summary>
	// Token: 0x020003E2 RID: 994
	[Guid("0000010E-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDataObject
	{
		/// <summary>Obtains data from a source data object. The <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> method, which is called by a data consumer, renders the data described in the specified <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure and transfers it through the specified <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure. The caller then assumes responsibility for releasing the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure.</summary>
		/// <param name="format">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use when passing the data. It is possible to specify more than one medium by using the Boolean OR operator, allowing the method to choose the best medium among those specified.</param>
		/// <param name="medium">When this method returns, contains a pointer to the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure that indicates the storage medium containing the returned data through its <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.tymed" /> member, and the responsibility for releasing the medium through the value of its <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member. If <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> is <see langword="null" />, the receiver of the medium is responsible for releasing it; otherwise, <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> points to the <see langword="IUnknown" /> interface on the appropriate object so its <see langword="Release" /> method can be called. The medium must be allocated and filled in by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. This parameter is passed uninitialized.</param>
		// Token: 0x060025FB RID: 9723
		void GetData([In] ref FORMATETC format, out STGMEDIUM medium);

		/// <summary>Obtains data from a source data object. This method, which is called by a data consumer, differs from the <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> method in that the caller must allocate and free the specified storage medium.</summary>
		/// <param name="format">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use when passing the data. Only one medium can be specified in <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" />, and only the following <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> values are valid: <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTORAGE" />, <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTREAM" />, <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_HGLOBAL" />, or <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_FILE" />.</param>
		/// <param name="medium">A <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" />, passed by reference, that defines the storage medium containing the data being transferred. The medium must be allocated by the caller and filled in by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetDataHere(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. The caller must also free the medium. The implementation of this method must always supply a value of <see langword="null" /> for the <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member of the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure that this parameter points to.</param>
		// Token: 0x060025FC RID: 9724
		void GetDataHere([In] ref FORMATETC format, ref STGMEDIUM medium);

		/// <summary>Determines whether the data object is capable of rendering the data described in the <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure. Objects attempting a paste or drop operation can call this method before calling <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> to get an indication of whether the operation may be successful.</summary>
		/// <param name="format">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use for the query.</param>
		/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following:  
		///   Value  
		///
		///   Description  
		///
		///   S_OK  
		///
		///   A subsequent call to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> would probably be successful.  
		///
		///   DV_E_LINDEX  
		///
		///   An invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.  
		///
		///   DV_E_FORMATETC  
		///
		///   An invalid value for the <paramref name="pFormatetc" /> parameter.  
		///
		///   DV_E_TYMED  
		///
		///   An invalid <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.tymed" /> value.  
		///
		///   DV_E_DVASPECT  
		///
		///   An invalid <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.dwAspect" /> value.  
		///
		///   OLE_E_NOTRUNNING  
		///
		///   The application is not running.</returns>
		// Token: 0x060025FD RID: 9725
		[PreserveSig]
		int QueryGetData([In] ref FORMATETC format);

		/// <summary>Provides a standard <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure that is logically equivalent to a more complex structure. Use this method to determine whether two different <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures would return the same data, removing the need for duplicate rendering.</summary>
		/// <param name="formatIn">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device that the caller would like to use to retrieve data in a subsequent call such as <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. The <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> member is not significant in this case and should be ignored.</param>
		/// <param name="formatOut">When this method returns, contains a pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure that contains the most general information possible for a specific rendering, making it canonically equivalent to <c>formatetIn</c>. The caller must allocate this structure and the <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetCanonicalFormatEtc(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.FORMATETC@)" /> method must fill in the data. To retrieve data in a subsequent call such as <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />, the caller uses the supplied value of <paramref name="formatOut" />, unless the value supplied is <see langword="null" />. This value is <see langword="null" /> if the method returns <see langword="DATA_S_SAMEFORMATETC" />. The <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> member is not significant in this case and should be ignored. This parameter is passed uninitialized.</param>
		/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following:  
		///   Value  
		///
		///   Description  
		///
		///   S_OK  
		///
		///   The returned <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure is different from the one that was passed.  
		///
		///   DATA_S_SAMEFORMATETC  
		///
		///   The <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures are the same and <see langword="null" /> is returned in the <paramref name="formatOut" /> parameter.  
		///
		///   DV_E_LINDEX  
		///
		///   There is an invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.  
		///
		///   DV_E_FORMATETC  
		///
		///   There is an invalid value for the <paramref name="pFormatetc" /> parameter.  
		///
		///   OLE_E_NOTRUNNING  
		///
		///   The application is not running.</returns>
		// Token: 0x060025FE RID: 9726
		[PreserveSig]
		int GetCanonicalFormatEtc([In] ref FORMATETC formatIn, out FORMATETC formatOut);

		/// <summary>Transfers data to the object that implements this method. This method is called by an object that contains a data source.</summary>
		/// <param name="formatIn">A <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format used by the data object when interpreting the data contained in the storage medium.</param>
		/// <param name="medium">A <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure, passed by reference, that defines the storage medium in which the data is being passed.</param>
		/// <param name="release">
		///   <see langword="true" /> to specify that the data object called, which implements <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.SetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@,System.Boolean)" />, owns the storage medium after the call returns. This means that the data object must free the medium after it has been used by calling the <see langword="ReleaseStgMedium" /> function. <see langword="false" /> to specify that the caller retains ownership of the storage medium, and the data object called uses the storage medium for the duration of the call only.</param>
		// Token: 0x060025FF RID: 9727
		void SetData([In] ref FORMATETC formatIn, [In] ref STGMEDIUM medium, [MarshalAs(UnmanagedType.Bool)] bool release);

		/// <summary>Creates an object for enumerating the <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures for a data object. These structures are used in calls to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> or <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.SetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@,System.Boolean)" />.</summary>
		/// <param name="direction">One of the <see cref="T:System.Runtime.InteropServices.ComTypes.DATADIR" /> values that specifies the direction of the data.</param>
		/// <returns>This method supports the standard return values E_INVALIDARG and E_OUTOFMEMORY, as well as the following:  
		///   Value  
		///
		///   Description  
		///
		///   S_OK  
		///
		///   The enumerator object was successfully created.  
		///
		///   E_NOTIMPL  
		///
		///   The direction specified by the <paramref name="direction" /> parameter is not supported.  
		///
		///   OLE_S_USEREG  
		///
		///   Requests that OLE enumerate the formats from the registry.</returns>
		// Token: 0x06002600 RID: 9728
		IEnumFORMATETC EnumFormatEtc(DATADIR direction);

		/// <summary>Creates a connection between a data object and an advisory sink. This method is called by an object that supports an advisory sink and enables the advisory sink to be notified of changes in the object's data.</summary>
		/// <param name="pFormatetc">A <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, target device, aspect, and medium that will be used for future notifications.</param>
		/// <param name="advf">One of the <see cref="T:System.Runtime.InteropServices.ComTypes.ADVF" /> values that specifies a group of flags for controlling the advisory connection.</param>
		/// <param name="adviseSink">A pointer to the <see cref="T:System.Runtime.InteropServices.ComTypes.IAdviseSink" /> interface on the advisory sink that will receive the change notification.</param>
		/// <param name="connection">When this method returns, contains a pointer to a DWORD token that identifies this connection. You can use this token later to delete the advisory connection by passing it to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.DUnadvise(System.Int32)" />. If this value is zero, the connection was not established. This parameter is passed uninitialized.</param>
		/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following:  
		///   Value  
		///
		///   Description  
		///
		///   S_OK  
		///
		///   The advisory connection was created.  
		///
		///   E_NOTIMPL  
		///
		///   This method is not implemented on the data object.  
		///
		///   DV_E_LINDEX  
		///
		///   There is an invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.  
		///
		///   DV_E_FORMATETC  
		///
		///   There is an invalid value for the <paramref name="pFormatetc" /> parameter.  
		///
		///   OLE_E_ADVISENOTSUPPORTED  
		///
		///   The data object does not support change notification.</returns>
		// Token: 0x06002601 RID: 9729
		[PreserveSig]
		int DAdvise([In] ref FORMATETC pFormatetc, ADVF advf, IAdviseSink adviseSink, out int connection);

		/// <summary>Destroys a notification connection that had been previously established.</summary>
		/// <param name="connection">A DWORD token that specifies the connection to remove. Use the value returned by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.DAdvise(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.ADVF,System.Runtime.InteropServices.ComTypes.IAdviseSink,System.Int32@)" /> when the connection was originally established.</param>
		// Token: 0x06002602 RID: 9730
		void DUnadvise(int connection);

		/// <summary>Creates an object that can be used to enumerate the current advisory connections.</summary>
		/// <param name="enumAdvise">When this method returns, contains an <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumSTATDATA" /> that receives the interface pointer to the new enumerator object. If the implementation sets <paramref name="enumAdvise" /> to <see langword="null" />, there are no connections to advisory sinks at this time. This parameter is passed uninitialized.</param>
		/// <returns>This method supports the standard return value E_OUTOFMEMORY, as well as the following:  
		///   Value  
		///
		///   Description  
		///
		///   S_OK  
		///
		///   The enumerator object is successfully instantiated or there are no connections.  
		///
		///   OLE_E_ADVISENOTSUPPORTED  
		///
		///   This object does not support advisory notifications.</returns>
		// Token: 0x06002603 RID: 9731
		[PreserveSig]
		int EnumDAdvise(out IEnumSTATDATA enumAdvise);
	}
}
