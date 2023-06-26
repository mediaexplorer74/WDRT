using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000CC RID: 204
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8AD3FC86-AFD3-477a-8FD5-146C291195BB")]
	[ComImport]
	internal interface IEventEntry
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002E3 RID: 739
		EventEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002E4 RID: 740
		uint EventID
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002E5 RID: 741
		uint Level
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002E6 RID: 742
		uint Version
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002E7 RID: 743
		Guid Guid
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002E8 RID: 744
		string SubTypeName
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002E9 RID: 745
		uint SubTypeValue
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002EA RID: 746
		string DisplayName
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002EB RID: 747
		uint EventNameMicrodomIndex
		{
			[SecurityCritical]
			get;
		}
	}
}
