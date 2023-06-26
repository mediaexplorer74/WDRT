using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Represents a generalized Clipboard format.</summary>
	// Token: 0x020003E0 RID: 992
	[global::__DynamicallyInvokable]
	public struct FORMATETC
	{
		/// <summary>Specifies the particular clipboard format of interest.</summary>
		// Token: 0x0400207E RID: 8318
		[global::__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.U2)]
		public short cfFormat;

		/// <summary>Specifies a pointer to a <see langword="DVTARGETDEVICE" /> structure containing information about the target device that the data is being composed for.</summary>
		// Token: 0x0400207F RID: 8319
		public IntPtr ptd;

		/// <summary>Specifies one of the <see cref="T:System.Runtime.InteropServices.ComTypes.DVASPECT" /> enumeration constants that indicates how much detail should be contained in the rendering.</summary>
		// Token: 0x04002080 RID: 8320
		[global::__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.U4)]
		public DVASPECT dwAspect;

		/// <summary>Specifies part of the aspect when the data must be split across page boundaries.</summary>
		// Token: 0x04002081 RID: 8321
		[global::__DynamicallyInvokable]
		public int lindex;

		/// <summary>Specifies one of the <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> enumeration constants, which indicates the type of storage medium used to transfer the object's data.</summary>
		// Token: 0x04002082 RID: 8322
		[global::__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.U4)]
		public TYMED tymed;
	}
}
