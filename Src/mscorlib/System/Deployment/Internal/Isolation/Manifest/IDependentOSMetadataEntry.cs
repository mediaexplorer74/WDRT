using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200070A RID: 1802
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CF168CF4-4E8F-4d92-9D2A-60E5CA21CF85")]
	[ComImport]
	internal interface IDependentOSMetadataEntry
	{
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06005106 RID: 20742
		DependentOSMetadataEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06005107 RID: 20743
		string SupportUrl
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06005108 RID: 20744
		string Description
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06005109 RID: 20745
		ushort MajorVersion
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x0600510A RID: 20746
		ushort MinorVersion
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x0600510B RID: 20747
		ushort BuildNumber
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x0600510C RID: 20748
		byte ServicePackMajor
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x0600510D RID: 20749
		byte ServicePackMinor
		{
			[SecurityCritical]
			get;
		}
	}
}
