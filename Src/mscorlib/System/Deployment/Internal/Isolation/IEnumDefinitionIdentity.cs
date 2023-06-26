using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000690 RID: 1680
	[Guid("f3549d9c-fc73-4793-9c00-1cd204254c0c")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumDefinitionIdentity
	{
		// Token: 0x06004FA5 RID: 20389
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IDefinitionIdentity[] DefinitionIdentity);

		// Token: 0x06004FA6 RID: 20390
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004FA7 RID: 20391
		[SecurityCritical]
		void Reset();

		// Token: 0x06004FA8 RID: 20392
		[SecurityCritical]
		IEnumDefinitionIdentity Clone();
	}
}
