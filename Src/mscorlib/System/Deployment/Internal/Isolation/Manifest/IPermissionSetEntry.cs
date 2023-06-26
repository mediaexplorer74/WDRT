using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006FE RID: 1790
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("EBE5A1ED-FEBC-42c4-A9E1-E087C6E36635")]
	[ComImport]
	internal interface IPermissionSetEntry
	{
		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x060050EF RID: 20719
		PermissionSetEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x060050F0 RID: 20720
		string Id
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x060050F1 RID: 20721
		string XmlSegment
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}
	}
}
