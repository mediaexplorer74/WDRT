using System;

namespace Microsoft.Data.OData.Query.Metadata
{
	// Token: 0x020000A0 RID: 160
	internal sealed class ODataQueryEdmTypeAnnotation
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000BE22 File Offset: 0x0000A022
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0000BE2A File Offset: 0x0000A02A
		public bool CanReflectOnInstanceType { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000BE33 File Offset: 0x0000A033
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000BE3B File Offset: 0x0000A03B
		public Type InstanceType { get; set; }
	}
}
