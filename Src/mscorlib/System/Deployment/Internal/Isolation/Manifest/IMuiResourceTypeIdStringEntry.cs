using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006CB RID: 1739
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("11df5cad-c183-479b-9a44-3842b71639ce")]
	[ComImport]
	internal interface IMuiResourceTypeIdStringEntry
	{
		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06005074 RID: 20596
		MuiResourceTypeIdStringEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06005075 RID: 20597
		object StringIds
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06005076 RID: 20598
		object IntegerIds
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
