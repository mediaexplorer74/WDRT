using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A15 RID: 2581
	[Guid("6a79e863-4300-459a-9966-cbb660963ee1")]
	[ComImport]
	internal interface IIterator<T>
	{
		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x060065DA RID: 26074
		T Current { get; }

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x060065DB RID: 26075
		bool HasCurrent { get; }

		// Token: 0x060065DC RID: 26076
		bool MoveNext();

		// Token: 0x060065DD RID: 26077
		int GetMany([Out] T[] items);
	}
}
