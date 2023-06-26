using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006DD RID: 1757
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("DA0C3B27-6B6B-4b80-A8F8-6CE14F4BC0A4")]
	[ComImport]
	internal interface ICategoryMembershipDataEntry
	{
		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x060050AB RID: 20651
		CategoryMembershipDataEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x060050AC RID: 20652
		uint index
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060050AD RID: 20653
		string Xml
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x060050AE RID: 20654
		string Description
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}
	}
}
