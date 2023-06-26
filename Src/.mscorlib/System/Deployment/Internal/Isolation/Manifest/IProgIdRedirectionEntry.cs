using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006E9 RID: 1769
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("54F198EC-A63A-45ea-A984-452F68D9B35B")]
	[ComImport]
	internal interface IProgIdRedirectionEntry
	{
		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x060050C2 RID: 20674
		ProgIdRedirectionEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060050C3 RID: 20675
		string ProgId
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060050C4 RID: 20676
		Guid RedirectedGuid
		{
			[SecurityCritical]
			get;
		}
	}
}
