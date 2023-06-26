using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000683 RID: 1667
	[Guid("a5c637bf-6eaa-4e5f-b535-55299657e33e")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_ASSEMBLY
	{
		// Token: 0x06004F61 RID: 20321
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_ASSEMBLY[] rgelt);

		// Token: 0x06004F62 RID: 20322
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004F63 RID: 20323
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F64 RID: 20324
		[SecurityCritical]
		IEnumSTORE_ASSEMBLY Clone();
	}
}
