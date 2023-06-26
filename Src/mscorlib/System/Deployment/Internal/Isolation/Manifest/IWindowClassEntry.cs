using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006F5 RID: 1781
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8AD3FC86-AFD3-477a-8FD5-146C291195BA")]
	[ComImport]
	internal interface IWindowClassEntry
	{
		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x060050DF RID: 20703
		WindowClassEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x060050E0 RID: 20704
		string ClassName
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x060050E1 RID: 20705
		string HostDll
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x060050E2 RID: 20706
		bool fVersioned
		{
			[SecurityCritical]
			get;
		}
	}
}
