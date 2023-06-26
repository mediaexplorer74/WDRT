using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006F8 RID: 1784
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("70A4ECEE-B195-4c59-85BF-44B6ACA83F07")]
	[ComImport]
	internal interface IResourceTableMappingEntry
	{
		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x060050E4 RID: 20708
		ResourceTableMappingEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x060050E5 RID: 20709
		string id
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x060050E6 RID: 20710
		string FinalStringMapped
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}
	}
}
