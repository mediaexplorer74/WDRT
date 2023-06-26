using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000689 RID: 1673
	[Guid("19be1967-b2fc-4dc1-9627-f3cb6305d2a7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_CATEGORY_SUBCATEGORY
	{
		// Token: 0x06004F82 RID: 20354
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_CATEGORY_SUBCATEGORY[] rgElements);

		// Token: 0x06004F83 RID: 20355
		[SecurityCritical]
		void Skip([In] uint ulElements);

		// Token: 0x06004F84 RID: 20356
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F85 RID: 20357
		[SecurityCritical]
		IEnumSTORE_CATEGORY_SUBCATEGORY Clone();
	}
}
