using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D7 RID: 1751
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("A2A55FAD-349B-469b-BF12-ADC33D14A937")]
	[ComImport]
	internal interface IFileEntry
	{
		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06005094 RID: 20628
		FileEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06005095 RID: 20629
		string Name
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06005096 RID: 20630
		uint HashAlgorithm
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06005097 RID: 20631
		string LoadFrom
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06005098 RID: 20632
		string SourcePath
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06005099 RID: 20633
		string ImportPath
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x0600509A RID: 20634
		string SourceName
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x0600509B RID: 20635
		string Location
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x0600509C RID: 20636
		object HashValue
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x0600509D RID: 20637
		ulong Size
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600509E RID: 20638
		string Group
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x0600509F RID: 20639
		uint Flags
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x060050A0 RID: 20640
		IMuiResourceMapEntry MuiMapping
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x060050A1 RID: 20641
		uint WritableType
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x060050A2 RID: 20642
		ISection HashElements
		{
			[SecurityCritical]
			get;
		}
	}
}
