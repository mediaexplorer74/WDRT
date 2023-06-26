using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D1 RID: 1745
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("397927f5-10f2-4ecb-bfe1-3c264212a193")]
	[ComImport]
	internal interface IMuiResourceMapEntry
	{
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06005082 RID: 20610
		MuiResourceMapEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06005083 RID: 20611
		object ResourceTypeIdInt
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06005084 RID: 20612
		object ResourceTypeIdString
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
