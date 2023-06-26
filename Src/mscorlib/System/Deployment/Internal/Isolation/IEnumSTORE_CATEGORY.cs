using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000687 RID: 1671
	[Guid("b840a2f5-a497-4a6d-9038-cd3ec2fbd222")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_CATEGORY
	{
		// Token: 0x06004F77 RID: 20343
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_CATEGORY[] rgElements);

		// Token: 0x06004F78 RID: 20344
		[SecurityCritical]
		void Skip([In] uint ulElements);

		// Token: 0x06004F79 RID: 20345
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F7A RID: 20346
		[SecurityCritical]
		IEnumSTORE_CATEGORY Clone();
	}
}
