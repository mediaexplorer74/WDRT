using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000018 RID: 24
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("285a8871-c84a-11d7-850f-005cd062464f")]
	[ComImport]
	internal interface ISectionWithStringKey
	{
		// Token: 0x060000B3 RID: 179
		void Lookup([MarshalAs(UnmanagedType.LPWStr)] string wzStringKey, [MarshalAs(UnmanagedType.Interface)] out object ppUnknown);

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B4 RID: 180
		bool IsCaseInsensitive { get; }
	}
}
