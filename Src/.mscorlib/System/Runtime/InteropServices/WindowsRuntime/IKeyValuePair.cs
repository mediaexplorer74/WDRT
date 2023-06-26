using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A1E RID: 2590
	[Guid("02b51929-c1c4-4a7e-8940-0312b5c18500")]
	[ComImport]
	internal interface IKeyValuePair<K, V>
	{
		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06006615 RID: 26133
		K Key { get; }

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06006616 RID: 26134
		V Value { get; }
	}
}
