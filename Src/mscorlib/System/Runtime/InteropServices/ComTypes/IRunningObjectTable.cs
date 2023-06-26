using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the <see langword="IRunningObjectTable" /> interface.</summary>
	// Token: 0x02000A32 RID: 2610
	[Guid("00000010-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IRunningObjectTable
	{
		/// <summary>Registers that the supplied object has entered the running state.</summary>
		/// <param name="grfFlags">Specifies whether the Running Object Table's (ROT) reference to <paramref name="punkObject" /> is weak or strong, and controls access to the object through its entry in the ROT.</param>
		/// <param name="punkObject">A reference to the object being registered as running.</param>
		/// <param name="pmkObjectName">A reference to the moniker that identifies <paramref name="punkObject" />.</param>
		/// <returns>A value that can be used to identify this ROT entry in subsequent calls to <see cref="M:System.Runtime.InteropServices.ComTypes.IRunningObjectTable.Revoke(System.Int32)" /> or <see cref="M:System.Runtime.InteropServices.ComTypes.IRunningObjectTable.NoteChangeTime(System.Int32,System.Runtime.InteropServices.ComTypes.FILETIME@)" />.</returns>
		// Token: 0x06006674 RID: 26228
		[__DynamicallyInvokable]
		int Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, IMoniker pmkObjectName);

		/// <summary>Unregisters the specified object from the Running Object Table (ROT).</summary>
		/// <param name="dwRegister">The Running Object Table (ROT) entry to revoke.</param>
		// Token: 0x06006675 RID: 26229
		[__DynamicallyInvokable]
		void Revoke(int dwRegister);

		/// <summary>Determines whether the specified moniker is currently registered in the Running Object Table (ROT).</summary>
		/// <param name="pmkObjectName">A reference to the moniker to search for in the Running Object Table (ROT).</param>
		/// <returns>An <see langword="HRESULT" /> value that indicates the success or failure of the operation.</returns>
		// Token: 0x06006676 RID: 26230
		[__DynamicallyInvokable]
		[PreserveSig]
		int IsRunning(IMoniker pmkObjectName);

		/// <summary>Returns the registered object if the supplied object name is registered as running.</summary>
		/// <param name="pmkObjectName">A reference to the moniker to search for in the Running Object Table (ROT).</param>
		/// <param name="ppunkObject">When this method returns, contains the requested running object. This parameter is passed uninitialized.</param>
		/// <returns>An <see langword="HRESULT" /> value that indicates the success or failure of the operation.</returns>
		// Token: 0x06006677 RID: 26231
		[__DynamicallyInvokable]
		[PreserveSig]
		int GetObject(IMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

		/// <summary>Notes the time that a particular object changed so <see langword="IMoniker::GetTimeOfLastChange" /> can report an appropriate change time.</summary>
		/// <param name="dwRegister">The Running Object Table (ROT) entry of the changed object.</param>
		/// <param name="pfiletime">A reference to the object's last change time.</param>
		// Token: 0x06006678 RID: 26232
		[__DynamicallyInvokable]
		void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

		/// <summary>Searches for this moniker in the Running Object Table (ROT) and reports the recorded time of change, if present.</summary>
		/// <param name="pmkObjectName">A reference to the moniker to search for in the Running Object Table (ROT).</param>
		/// <param name="pfiletime">When this object returns, contains the objects last change time. This parameter is passed uninitialized.</param>
		/// <returns>An <see langword="HRESULT" /> value that indicates the success or failure of the operation.</returns>
		// Token: 0x06006679 RID: 26233
		[__DynamicallyInvokable]
		[PreserveSig]
		int GetTimeOfLastChange(IMoniker pmkObjectName, out FILETIME pfiletime);

		/// <summary>Enumerates the objects currently registered as running.</summary>
		/// <param name="ppenumMoniker">When this method returns, contains the new enumerator for the Running Object Table (ROT). This parameter is passed uninitialized.</param>
		// Token: 0x0600667A RID: 26234
		[__DynamicallyInvokable]
		void EnumRunning(out IEnumMoniker ppenumMoniker);
	}
}
