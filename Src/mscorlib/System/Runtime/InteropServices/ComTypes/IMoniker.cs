using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the <see langword="IMoniker" /> interface, with COM functionality from <see langword="IPersist" /> and <see langword="IPersistStream" />.</summary>
	// Token: 0x02000A2F RID: 2607
	[Guid("0000000f-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IMoniker
	{
		/// <summary>Retrieves the class identifier (CLSID) of an object.</summary>
		/// <param name="pClassID">When this method returns, contains the CLSID. This parameter is passed uninitialized.</param>
		// Token: 0x0600664E RID: 26190
		[__DynamicallyInvokable]
		void GetClassID(out Guid pClassID);

		/// <summary>Checks the object for changes since it was last saved.</summary>
		/// <returns>An <see langword="S_OK" /><see langword="HRESULT" /> value if the object has changed; otherwise, an <see langword="S_FALSE" /><see langword="HRESULT" /> value.</returns>
		// Token: 0x0600664F RID: 26191
		[__DynamicallyInvokable]
		[PreserveSig]
		int IsDirty();

		/// <summary>Initializes an object from the stream where it was previously saved.</summary>
		/// <param name="pStm">The stream that the object is loaded from.</param>
		// Token: 0x06006650 RID: 26192
		[__DynamicallyInvokable]
		void Load(IStream pStm);

		/// <summary>Saves an object to the specified stream.</summary>
		/// <param name="pStm">The stream to which the object is saved.</param>
		/// <param name="fClearDirty">
		///   <see langword="true" /> to clear the modified flag after the save is complete; otherwise <see langword="false" /></param>
		// Token: 0x06006651 RID: 26193
		[__DynamicallyInvokable]
		void Save(IStream pStm, [MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

		/// <summary>Returns the size, in bytes, of the stream needed to save the object.</summary>
		/// <param name="pcbSize">When this method returns, contains a <see langword="long" /> value indicating the size, in bytes, of the stream needed to save this object. This parameter is passed uninitialized.</param>
		// Token: 0x06006652 RID: 26194
		[__DynamicallyInvokable]
		void GetSizeMax(out long pcbSize);

		/// <summary>Uses the moniker to bind to the object that it identifies.</summary>
		/// <param name="pbc">A reference to the <see langword="IBindCtx" /> interface on the bind context object used in this binding operation.</param>
		/// <param name="pmkToLeft">A reference to the moniker to the left of the current moniker, if the moniker is part of a composite moniker.</param>
		/// <param name="riidResult">The interface identifier (IID) of the interface that the client intends to use to communicate with the object that the moniker identifies.</param>
		/// <param name="ppvResult">When this method returns, contains a reference to the interface requested by <paramref name="riidResult" />. This parameter is passed uninitialized.</param>
		// Token: 0x06006653 RID: 26195
		[__DynamicallyInvokable]
		void BindToObject(IBindCtx pbc, IMoniker pmkToLeft, [In] ref Guid riidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

		/// <summary>Retrieves an interface pointer to the storage that contains the object identified by the moniker.</summary>
		/// <param name="pbc">A reference to the <see langword="IBindCtx" /> interface on the bind context object used during this binding operation.</param>
		/// <param name="pmkToLeft">A reference to the moniker to the left of the current moniker, if the moniker is part of a composite moniker.</param>
		/// <param name="riid">The interface identifier (IID) of the storage interface requested.</param>
		/// <param name="ppvObj">When this method returns, contains a reference to the interface requested by <paramref name="riid" />. This parameter is passed uninitialized.</param>
		// Token: 0x06006654 RID: 26196
		[__DynamicallyInvokable]
		void BindToStorage(IBindCtx pbc, IMoniker pmkToLeft, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

		/// <summary>Returns a reduced moniker, which is another moniker that refers to the same object as the current moniker but can be bound with equal or greater efficiency.</summary>
		/// <param name="pbc">A reference to the <see langword="IBindCtx" /> interface on the bind context to use in this binding operation.</param>
		/// <param name="dwReduceHowFar">A value that specifies how far the current moniker should be reduced.</param>
		/// <param name="ppmkToLeft">A reference to the moniker to the left of the current moniker.</param>
		/// <param name="ppmkReduced">When this method returns, contains a reference to the reduced form of the current moniker, which can be <see langword="null" /> if an error occurs or if the current moniker is reduced to nothing. This parameter is passed uninitialized.</param>
		// Token: 0x06006655 RID: 26197
		[__DynamicallyInvokable]
		void Reduce(IBindCtx pbc, int dwReduceHowFar, ref IMoniker ppmkToLeft, out IMoniker ppmkReduced);

		/// <summary>Combines the current moniker with another moniker, creating a new composite moniker.</summary>
		/// <param name="pmkRight">A reference to the <see langword="IMoniker" /> interface on a moniker to append to the end of the current moniker.</param>
		/// <param name="fOnlyIfNotGeneric">
		///   <see langword="true" /> to indicate that the caller requires a nongeneric composition. The operation proceeds only if <paramref name="pmkRight" /> is a moniker class that the current moniker can combine with in some way other than forming a generic composite. <see langword="false" /> to indicate that the method can create a generic composite if necessary.</param>
		/// <param name="ppmkComposite">When this method returns, contains a reference to the resulting composite moniker. This parameter is passed uninitialized.</param>
		// Token: 0x06006656 RID: 26198
		[__DynamicallyInvokable]
		void ComposeWith(IMoniker pmkRight, [MarshalAs(UnmanagedType.Bool)] bool fOnlyIfNotGeneric, out IMoniker ppmkComposite);

		/// <summary>Supplies a pointer to an enumerator that can enumerate the components of a composite moniker.</summary>
		/// <param name="fForward">
		///   <see langword="true" /> to enumerate the monikers from left to right. <see langword="false" /> to enumerate from right to left.</param>
		/// <param name="ppenumMoniker">When this method returns, contains a reference to the enumerator object for the moniker. This parameter is passed uninitialized.</param>
		// Token: 0x06006657 RID: 26199
		[__DynamicallyInvokable]
		void Enum([MarshalAs(UnmanagedType.Bool)] bool fForward, out IEnumMoniker ppenumMoniker);

		/// <summary>Compares the current moniker with a specified moniker and indicates whether they are identical.</summary>
		/// <param name="pmkOtherMoniker">A reference to the moniker to use for comparison.</param>
		/// <returns>An <see langword="S_OK" /><see langword="HRESULT" /> value if the monikers are identical; otherwise, an <see langword="S_FALSE" /><see langword="HRESULT" /> value.</returns>
		// Token: 0x06006658 RID: 26200
		[__DynamicallyInvokable]
		[PreserveSig]
		int IsEqual(IMoniker pmkOtherMoniker);

		/// <summary>Calculates a 32-bit integer using the internal state of the moniker.</summary>
		/// <param name="pdwHash">When this method returns, contains the hash value for this moniker. This parameter is passed uninitialized.</param>
		// Token: 0x06006659 RID: 26201
		[__DynamicallyInvokable]
		void Hash(out int pdwHash);

		/// <summary>Determines whether the object that is identified by the current moniker is currently loaded and running.</summary>
		/// <param name="pbc">A reference to the bind context to use in this binding operation.</param>
		/// <param name="pmkToLeft">A reference to the moniker to the left of the current moniker if the current moniker is part of a composite.</param>
		/// <param name="pmkNewlyRunning">A reference to the moniker most recently added to the Running Object Table (ROT).</param>
		/// <returns>An <see langword="S_OK" /><see langword="HRESULT" /> value if the moniker is running; an <see langword="S_FALSE" /><see langword="HRESULT" /> value if the moniker is not running; or an <see langword="E_UNEXPECTED" /><see langword="HRESULT" /> value.</returns>
		// Token: 0x0600665A RID: 26202
		[__DynamicallyInvokable]
		[PreserveSig]
		int IsRunning(IBindCtx pbc, IMoniker pmkToLeft, IMoniker pmkNewlyRunning);

		/// <summary>Provides a number representing the time that the object identified by the current moniker was last changed.</summary>
		/// <param name="pbc">A reference to the bind context to use in this binding operation.</param>
		/// <param name="pmkToLeft">A reference to the moniker to the left of the current moniker, if the moniker is part of a composite moniker.</param>
		/// <param name="pFileTime">When this method returns, contains the time of the last change. This parameter is passed uninitialized.</param>
		// Token: 0x0600665B RID: 26203
		[__DynamicallyInvokable]
		void GetTimeOfLastChange(IBindCtx pbc, IMoniker pmkToLeft, out FILETIME pFileTime);

		/// <summary>Provides a moniker that, when composed to the right of the current moniker or one of similar structure, composes to nothing.</summary>
		/// <param name="ppmk">When this method returns, contains a moniker that is the inverse of the current moniker. This parameter is passed uninitialized.</param>
		// Token: 0x0600665C RID: 26204
		[__DynamicallyInvokable]
		void Inverse(out IMoniker ppmk);

		/// <summary>Creates a new moniker based on the common prefix that this moniker shares with another moniker.</summary>
		/// <param name="pmkOther">A reference to the <see langword="IMoniker" /> interface on another moniker to compare with the current moniker for a common prefix.</param>
		/// <param name="ppmkPrefix">When this method returns, contains the moniker that is the common prefix of the current moniker and <paramref name="pmkOther" />. This parameter is passed uninitialized.</param>
		// Token: 0x0600665D RID: 26205
		[__DynamicallyInvokable]
		void CommonPrefixWith(IMoniker pmkOther, out IMoniker ppmkPrefix);

		/// <summary>Supplies a moniker that, when appended to the current moniker (or one with a similar structure), yields the specified moniker.</summary>
		/// <param name="pmkOther">A reference to the moniker to which a relative path should be taken.</param>
		/// <param name="ppmkRelPath">When this method returns, contains a reference to the relative moniker. This parameter is passed uninitialized.</param>
		// Token: 0x0600665E RID: 26206
		[__DynamicallyInvokable]
		void RelativePathTo(IMoniker pmkOther, out IMoniker ppmkRelPath);

		/// <summary>Gets the display name, which is a user-readable representation of the current moniker.</summary>
		/// <param name="pbc">A reference to the bind context to use in this operation.</param>
		/// <param name="pmkToLeft">A reference to the moniker to the left of the current moniker, if the moniker is part of a composite moniker.</param>
		/// <param name="ppszDisplayName">When this method returns, contains the display name string. This parameter is passed uninitialized.</param>
		// Token: 0x0600665F RID: 26207
		[__DynamicallyInvokable]
		void GetDisplayName(IBindCtx pbc, IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplayName);

		/// <summary>Reads as many characters of the specified display name as the <see cref="M:System.Runtime.InteropServices.ComTypes.IMoniker.ParseDisplayName(System.Runtime.InteropServices.ComTypes.IBindCtx,System.Runtime.InteropServices.ComTypes.IMoniker,System.String,System.Int32@,System.Runtime.InteropServices.ComTypes.IMoniker@)" /> understands and builds a moniker corresponding to the portion read.</summary>
		/// <param name="pbc">A reference to the bind context to use in this binding operation.</param>
		/// <param name="pmkToLeft">A reference to the moniker that has been built from the display name up to this point.</param>
		/// <param name="pszDisplayName">A reference to the string containing the remaining display name to parse.</param>
		/// <param name="pchEaten">When this method returns, contains the number of characters that were consumed in parsing <paramref name="pszDisplayName" />. This parameter is passed uninitialized.</param>
		/// <param name="ppmkOut">When this method returns, contains a reference to the moniker that was built from <paramref name="pszDisplayName" />. This parameter is passed uninitialized.</param>
		// Token: 0x06006660 RID: 26208
		[__DynamicallyInvokable]
		void ParseDisplayName(IBindCtx pbc, IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName, out int pchEaten, out IMoniker ppmkOut);

		/// <summary>Indicates whether this moniker is of one of the system-supplied moniker classes.</summary>
		/// <param name="pdwMksys">When this method returns, contains a pointer to an integer that is one of the values from the <see langword="MKSYS" /> enumeration, and refers to one of the COM moniker classes. This parameter is passed uninitialized.</param>
		/// <returns>An <see langword="S_OK" /><see langword="HRESULT" /> value if the moniker is a system moniker; otherwise, an <see langword="S_FALSE" /><see langword="HRESULT" /> value.</returns>
		// Token: 0x06006661 RID: 26209
		[__DynamicallyInvokable]
		[PreserveSig]
		int IsSystemMoniker(out int pdwMksys);
	}
}
