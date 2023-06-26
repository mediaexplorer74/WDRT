using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000671 RID: 1649
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("285a8860-c84a-11d7-850f-005cd062464f")]
	[ComImport]
	internal interface ICDF
	{
		// Token: 0x06004F41 RID: 20289
		ISection GetRootSection(uint SectionId);

		// Token: 0x06004F42 RID: 20290
		ISectionEntry GetRootSectionEntry(uint SectionId);

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06004F43 RID: 20291
		object _NewEnum
		{
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06004F44 RID: 20292
		uint Count { get; }

		// Token: 0x06004F45 RID: 20293
		object GetItem(uint SectionId);
	}
}
