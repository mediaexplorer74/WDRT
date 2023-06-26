using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200068F RID: 1679
	[Guid("9cdaae75-246e-4b00-a26d-b9aec137a3eb")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumIDENTITY_ATTRIBUTE
	{
		// Token: 0x06004FA0 RID: 20384
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IDENTITY_ATTRIBUTE[] rgAttributes);

		// Token: 0x06004FA1 RID: 20385
		[SecurityCritical]
		IntPtr CurrentIntoBuffer([In] IntPtr Available, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] Data);

		// Token: 0x06004FA2 RID: 20386
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004FA3 RID: 20387
		[SecurityCritical]
		void Reset();

		// Token: 0x06004FA4 RID: 20388
		[SecurityCritical]
		IEnumIDENTITY_ATTRIBUTE Clone();
	}
}
