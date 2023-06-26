using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200066F RID: 1647
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("285a8861-c84a-11d7-850f-005cd062464f")]
	[ComImport]
	internal interface ISectionEntry
	{
		// Token: 0x06004F3B RID: 20283
		object GetField(uint fieldId);

		// Token: 0x06004F3C RID: 20284
		string GetFieldName(uint fieldId);
	}
}
