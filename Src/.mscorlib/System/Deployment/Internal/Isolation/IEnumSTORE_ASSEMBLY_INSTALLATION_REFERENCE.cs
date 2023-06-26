using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200067E RID: 1662
	[Guid("d8b1aacb-5142-4abb-bcc1-e9dc9052a89e")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE
	{
		// Token: 0x06004F47 RID: 20295
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] StoreApplicationReference[] rgelt);

		// Token: 0x06004F48 RID: 20296
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004F49 RID: 20297
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F4A RID: 20298
		[SecurityCritical]
		IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE Clone();
	}
}
