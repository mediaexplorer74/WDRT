using System;
using System.Runtime.InteropServices;

namespace System.Management
{
	// Token: 0x02000061 RID: 97
	[InterfaceType(1)]
	[Guid("B7B31DF9-D515-11D3-A11C-00105A1F515A")]
	[ComImport]
	internal interface IWbemShutdown
	{
		// Token: 0x060003EF RID: 1007
		[PreserveSig]
		int Shutdown_([In] int uReason, [In] uint uMaxMilliseconds, [MarshalAs(UnmanagedType.Interface)] [In] IWbemContext pCtx);
	}
}
