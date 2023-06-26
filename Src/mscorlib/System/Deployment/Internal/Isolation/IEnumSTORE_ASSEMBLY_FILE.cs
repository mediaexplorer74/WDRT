using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000685 RID: 1669
	[Guid("a5c6aaa3-03e4-478d-b9f5-2e45908d5e4f")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_ASSEMBLY_FILE
	{
		// Token: 0x06004F6C RID: 20332
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_ASSEMBLY_FILE[] rgelt);

		// Token: 0x06004F6D RID: 20333
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004F6E RID: 20334
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F6F RID: 20335
		[SecurityCritical]
		IEnumSTORE_ASSEMBLY_FILE Clone();
	}
}
