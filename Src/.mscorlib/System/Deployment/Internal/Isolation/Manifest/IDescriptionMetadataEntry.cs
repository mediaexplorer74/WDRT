using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000704 RID: 1796
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CB73147E-5FC2-4c31-B4E6-58D13DBE1A08")]
	[ComImport]
	internal interface IDescriptionMetadataEntry
	{
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x060050F7 RID: 20727
		DescriptionMetadataEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x060050F8 RID: 20728
		string Publisher
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x060050F9 RID: 20729
		string Product
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x060050FA RID: 20730
		string SupportUrl
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x060050FB RID: 20731
		string IconFile
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x060050FC RID: 20732
		string ErrorReportUrl
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x060050FD RID: 20733
		string SuiteName
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}
	}
}
