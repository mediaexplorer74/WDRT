using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200068D RID: 1677
	[Guid("6eaf5ace-7917-4f3c-b129-e046a9704766")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IReferenceIdentity
	{
		// Token: 0x06004F98 RID: 20376
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string GetAttribute([MarshalAs(UnmanagedType.LPWStr)] [In] string Namespace, [MarshalAs(UnmanagedType.LPWStr)] [In] string Name);

		// Token: 0x06004F99 RID: 20377
		[SecurityCritical]
		void SetAttribute([MarshalAs(UnmanagedType.LPWStr)] [In] string Namespace, [MarshalAs(UnmanagedType.LPWStr)] [In] string Name, [MarshalAs(UnmanagedType.LPWStr)] [In] string Value);

		// Token: 0x06004F9A RID: 20378
		[SecurityCritical]
		IEnumIDENTITY_ATTRIBUTE EnumAttributes();

		// Token: 0x06004F9B RID: 20379
		[SecurityCritical]
		IReferenceIdentity Clone([In] IntPtr cDeltas, [MarshalAs(UnmanagedType.LPArray)] [In] IDENTITY_ATTRIBUTE[] Deltas);
	}
}
