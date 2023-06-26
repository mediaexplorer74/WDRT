using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006E6 RID: 1766
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("3903B11B-FBE8-477c-825F-DB828B5FD174")]
	[ComImport]
	internal interface ICOMServerEntry
	{
		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060050B8 RID: 20664
		COMServerEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060050B9 RID: 20665
		Guid Clsid
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x060050BA RID: 20666
		uint Flags
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x060050BB RID: 20667
		Guid ConfiguredGuid
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x060050BC RID: 20668
		Guid ImplementedClsid
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x060050BD RID: 20669
		Guid TypeLibrary
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x060050BE RID: 20670
		uint ThreadingModel
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060050BF RID: 20671
		string RuntimeVersion
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060050C0 RID: 20672
		string HostFile
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}
	}
}
