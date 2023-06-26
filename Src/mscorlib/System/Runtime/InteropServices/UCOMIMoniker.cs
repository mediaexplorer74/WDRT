using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IMoniker" /> instead.</summary>
	// Token: 0x02000986 RID: 2438
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IMoniker instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("0000000f-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIMoniker
	{
		/// <summary>Retrieves the class identifier (CLSID) of an object.</summary>
		/// <param name="pClassID">On successful return, contains the CLSID.</param>
		// Token: 0x060062BB RID: 25275
		void GetClassID(out Guid pClassID);

		/// <summary>Checks the object for changes since it was last saved.</summary>
		/// <returns>An <see langword="S_OK" /><see langword="HRESULT" /> value if the object has changed; otherwise, an <see langword="S_FALSE" /><see langword="HRESULT" /> value.</returns>
		// Token: 0x060062BC RID: 25276
		[PreserveSig]
		int IsDirty();

		/// <summary>Initializes an object from the stream where it was previously saved.</summary>
		/// <param name="pStm">Stream from which the object is loaded.</param>
		// Token: 0x060062BD RID: 25277
		void Load(UCOMIStream pStm);

		/// <summary>Saves an object to the specified stream.</summary>
		/// <param name="pStm">The stream into which the object is saved.</param>
		/// <param name="fClearDirty">Indicates whether to clear the modified flag after the save is complete.</param>
		// Token: 0x060062BE RID: 25278
		void Save(UCOMIStream pStm, [MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

		/// <summary>Returns the size in bytes of the stream needed to save the object.</summary>
		/// <param name="pcbSize">On successful return, contains a <see langword="long" /> value indicating the size in bytes of the stream needed to save this object.</param>
		// Token: 0x060062BF RID: 25279
		void GetSizeMax(out long pcbSize);

		/// <summary>Uses the moniker to bind to the object it identifies.</summary>
		/// <param name="pbc">A reference to the <see langword="IBindCtx" /> interface on the bind context object used in this binding operation.</param>
		/// <param name="pmkToLeft">A reference to the moniker to the left of this moniker, if the moniker is part of a composite moniker.</param>
		/// <param name="riidResult">The interface identifier (IID) of the interface the client intends to use to communicate with the object that the moniker identifies.</param>
		/// <param name="ppvResult">On successful return, reference to the interface requested by <paramref name="riidResult" />.</param>
		// Token: 0x060062C0 RID: 25280
		void BindToObject(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [In] ref Guid riidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

		/// <summary>Retrieves an interface pointer to the storage that contains the object identified by the moniker.</summary>
		/// <param name="pbc">A reference to the <see langword="IBindCtx" /> interface on the bind context object used during this binding operation.</param>
		/// <param name="pmkToLeft">A reference to the moniker to the left of this moniker, if the moniker is part of a composite moniker.</param>
		/// <param name="riid">The interface identifier (IID) of the storage interface requested.</param>
		/// <param name="ppvObj">On successful return, a reference to the interface requested by <paramref name="riid" />.</param>
		// Token: 0x060062C1 RID: 25281
		void BindToStorage(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

		/// <summary>Returns a reduced moniker which is another moniker that refers to the same object as this moniker but can be bound with equal or greater efficiency.</summary>
		/// <param name="pbc">A reference to the <see langword="IBindCtx" /> interface on the bind context to be used in this binding operation.</param>
		/// <param name="dwReduceHowFar">Specifies how far this moniker should be reduced.</param>
		/// <param name="ppmkToLeft">A reference to the moniker to the left of this moniker.</param>
		/// <param name="ppmkReduced">On successful return, a reference to the reduced form of this moniker, which can be <see langword="null" /> if an error occurs or if this moniker is reduced to nothing.</param>
		// Token: 0x060062C2 RID: 25282
		void Reduce(UCOMIBindCtx pbc, int dwReduceHowFar, ref UCOMIMoniker ppmkToLeft, out UCOMIMoniker ppmkReduced);

		/// <summary>Combines the current moniker with another moniker, creating a new composite moniker.</summary>
		/// <param name="pmkRight">A reference to the <see langword="IMoniker" /> interface on the moniker to compose onto the end of this moniker.</param>
		/// <param name="fOnlyIfNotGeneric">If <see langword="true" />, the caller requires a nongeneric composition, so the operation proceeds only if <paramref name="pmkRight" /> is a moniker class that this moniker can compose with in some way other than forming a generic composite. If <see langword="false" />, the method can create a generic composite if necessary.</param>
		/// <param name="ppmkComposite">On successful return, a reference to the resulting composite moniker.</param>
		// Token: 0x060062C3 RID: 25283
		void ComposeWith(UCOMIMoniker pmkRight, [MarshalAs(UnmanagedType.Bool)] bool fOnlyIfNotGeneric, out UCOMIMoniker ppmkComposite);

		/// <summary>Supplies a pointer to an enumerator that can enumerate the components of a composite moniker.</summary>
		/// <param name="fForward">If <see langword="true" />, enumerates the monikers from left to right. If <see langword="false" />, enumerates from right to left.</param>
		/// <param name="ppenumMoniker">On successful return, references the enumerator object for the moniker.</param>
		// Token: 0x060062C4 RID: 25284
		void Enum([MarshalAs(UnmanagedType.Bool)] bool fForward, out UCOMIEnumMoniker ppenumMoniker);

		/// <summary>Compares this moniker with a specified moniker and indicates whether they are identical.</summary>
		/// <param name="pmkOtherMoniker">A reference to the moniker to be used for comparison.</param>
		// Token: 0x060062C5 RID: 25285
		void IsEqual(UCOMIMoniker pmkOtherMoniker);

		/// <summary>Calculates a 32-bit integer using the internal state of the moniker.</summary>
		/// <param name="pdwHash">On successful return, contains the hash value for this moniker.</param>
		// Token: 0x060062C6 RID: 25286
		void Hash(out int pdwHash);

		/// <summary>Determines whether the object that is identified by this moniker is currently loaded and running.</summary>
		/// <param name="pbc">A reference to the bind context to be used in this binding operation.</param>
		/// <param name="pmkToLeft">A reference to the moniker to the left of this moniker if this moniker is part of a composite.</param>
		/// <param name="pmkNewlyRunning">A reference to the moniker most recently added to the Running Object Table.</param>
		// Token: 0x060062C7 RID: 25287
		void IsRunning(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, UCOMIMoniker pmkNewlyRunning);

		/// <summary>Provides a number representing the time the object identified by this moniker was last changed.</summary>
		/// <param name="pbc">A reference to the bind context to be used in this binding operation.</param>
		/// <param name="pmkToLeft">A reference to the moniker to the left of this moniker, if the moniker is part of a composite moniker.</param>
		/// <param name="pFileTime">On successful return, contains the time of last change.</param>
		// Token: 0x060062C8 RID: 25288
		void GetTimeOfLastChange(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, out FILETIME pFileTime);

		/// <summary>Provides a moniker that, when composed to the right of this moniker or one of similar structure, composes to nothing.</summary>
		/// <param name="ppmk">On successful return, contains a moniker that is the inverse of this moniker.</param>
		// Token: 0x060062C9 RID: 25289
		void Inverse(out UCOMIMoniker ppmk);

		/// <summary>Creates a new moniker based on the common prefix that this moniker shares with another moniker.</summary>
		/// <param name="pmkOther">A reference to the <see langword="IMoniker" /> interface on another moniker to compare with this for a common prefix.</param>
		/// <param name="ppmkPrefix">On successful return, contains the moniker that is the common prefix of this moniker and <paramref name="pmkOther" />.</param>
		// Token: 0x060062CA RID: 25290
		void CommonPrefixWith(UCOMIMoniker pmkOther, out UCOMIMoniker ppmkPrefix);

		/// <summary>Supplies a moniker that, when appended to this moniker (or one with a similar structure), yields the specified moniker.</summary>
		/// <param name="pmkOther">A reference to the moniker to which a relative path should be taken.</param>
		/// <param name="ppmkRelPath">On successful return, reference to the relative moniker.</param>
		// Token: 0x060062CB RID: 25291
		void RelativePathTo(UCOMIMoniker pmkOther, out UCOMIMoniker ppmkRelPath);

		/// <summary>Gets the display name, which is a user-readable representation of this moniker.</summary>
		/// <param name="pbc">A reference to the bind context to use in this operation.</param>
		/// <param name="pmkToLeft">A reference to the moniker to the left of this moniker, if the moniker is part of a composite moniker.</param>
		/// <param name="ppszDisplayName">On successful return, contains the display name string.</param>
		// Token: 0x060062CC RID: 25292
		void GetDisplayName(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplayName);

		/// <summary>Reads as many characters of the specified display name as it understands and builds a moniker corresponding to the portion read.</summary>
		/// <param name="pbc">A reference to the bind context to be used in this binding operation.</param>
		/// <param name="pmkToLeft">A reference to the moniker that has been built out of the display name up to this point.</param>
		/// <param name="pszDisplayName">A reference to the string containing the remaining display name to parse.</param>
		/// <param name="pchEaten">On successful return, contains the number of characters in <paramref name="pszDisplayName" /> that were consumed in this step.</param>
		/// <param name="ppmkOut">Reference to the moniker that was built from <paramref name="pszDisplayName" />.</param>
		// Token: 0x060062CD RID: 25293
		void ParseDisplayName(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName, out int pchEaten, out UCOMIMoniker ppmkOut);

		/// <summary>Indicates whether this moniker is of one of the system-supplied moniker classes.</summary>
		/// <param name="pdwMksys">A pointer to an integer that is one of the values from the <see langword="MKSYS" /> enumeration, and refers to one of the COM moniker classes.</param>
		// Token: 0x060062CE RID: 25294
		void IsSystemMoniker(out int pdwMksys);
	}
}
