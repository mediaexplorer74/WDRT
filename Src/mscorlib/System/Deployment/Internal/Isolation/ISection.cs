using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200066C RID: 1644
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("285a8862-c84a-11d7-850f-005cd062464f")]
	[ComImport]
	internal interface ISection
	{
		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06004F34 RID: 20276
		object _NewEnum
		{
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06004F35 RID: 20277
		uint Count { get; }

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06004F36 RID: 20278
		uint SectionID { get; }

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06004F37 RID: 20279
		string SectionName
		{
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}
	}
}
