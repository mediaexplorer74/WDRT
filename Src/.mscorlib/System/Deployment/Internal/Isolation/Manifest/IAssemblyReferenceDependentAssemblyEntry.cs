using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006EF RID: 1775
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C31FF59E-CD25-47b8-9EF3-CF4433EB97CC")]
	[ComImport]
	internal interface IAssemblyReferenceDependentAssemblyEntry
	{
		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x060050CE RID: 20686
		AssemblyReferenceDependentAssemblyEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x060050CF RID: 20687
		string Group
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x060050D0 RID: 20688
		string Codebase
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x060050D1 RID: 20689
		ulong Size
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x060050D2 RID: 20690
		object HashValue
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x060050D3 RID: 20691
		uint HashAlgorithm
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x060050D4 RID: 20692
		uint Flags
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x060050D5 RID: 20693
		string ResourceFallbackCulture
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x060050D6 RID: 20694
		string Description
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x060050D7 RID: 20695
		string SupportUrl
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x060050D8 RID: 20696
		ISection HashElements
		{
			[SecurityCritical]
			get;
		}
	}
}
