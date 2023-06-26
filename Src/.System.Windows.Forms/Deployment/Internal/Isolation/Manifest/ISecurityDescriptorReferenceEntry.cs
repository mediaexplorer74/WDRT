using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000DE RID: 222
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("a75b74e9-2c00-4ebb-b3f9-62a670aaa07e")]
	[ComImport]
	internal interface ISecurityDescriptorReferenceEntry
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000312 RID: 786
		SecurityDescriptorReferenceEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000313 RID: 787
		string Name
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000314 RID: 788
		string BuildFilter
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}
	}
}
