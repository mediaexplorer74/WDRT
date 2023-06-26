using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the <see langword="STGMEDIUM" /> structure.</summary>
	// Token: 0x020003E6 RID: 998
	[global::__DynamicallyInvokable]
	public struct STGMEDIUM
	{
		/// <summary>Specifies the type of storage medium. The marshaling and unmarshaling routines use this value to determine which union member was used. This value must be one of the elements of the <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> enumeration.</summary>
		// Token: 0x04002087 RID: 8327
		[global::__DynamicallyInvokable]
		public TYMED tymed;

		/// <summary>Represents a handle, string, or interface pointer that the receiving process can use to access the data being transferred.</summary>
		// Token: 0x04002088 RID: 8328
		public IntPtr unionmember;

		/// <summary>Represents a pointer to an interface instance that allows the sending process to control the way the storage is released when the receiving process calls the <see langword="ReleaseStgMedium" /> function. If <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> is <see langword="null" />, <see langword="ReleaseStgMedium" /> uses default procedures to release the storage; otherwise, <see langword="ReleaseStgMedium" /> uses the specified <see langword="IUnknown" /> interface.</summary>
		// Token: 0x04002089 RID: 8329
		[global::__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.IUnknown)]
		public object pUnkForRelease;
	}
}
