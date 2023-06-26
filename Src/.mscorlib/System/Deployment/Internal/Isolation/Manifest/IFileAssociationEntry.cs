using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006DA RID: 1754
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0C66F299-E08E-48c5-9264-7CCBEB4D5CBB")]
	[ComImport]
	internal interface IFileAssociationEntry
	{
		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x060050A4 RID: 20644
		FileAssociationEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x060050A5 RID: 20645
		string Extension
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x060050A6 RID: 20646
		string Description
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x060050A7 RID: 20647
		string ProgID
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x060050A8 RID: 20648
		string DefaultIcon
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x060050A9 RID: 20649
		string Parameter
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}
	}
}
