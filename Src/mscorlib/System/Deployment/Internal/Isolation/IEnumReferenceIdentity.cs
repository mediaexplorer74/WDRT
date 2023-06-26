using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000691 RID: 1681
	[Guid("b30352cf-23da-4577-9b3f-b4e6573be53b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumReferenceIdentity
	{
		// Token: 0x06004FA9 RID: 20393
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IReferenceIdentity[] ReferenceIdentity);

		// Token: 0x06004FAA RID: 20394
		[SecurityCritical]
		void Skip(uint celt);

		// Token: 0x06004FAB RID: 20395
		[SecurityCritical]
		void Reset();

		// Token: 0x06004FAC RID: 20396
		[SecurityCritical]
		IEnumReferenceIdentity Clone();
	}
}
