using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A19 RID: 2585
	[Guid("bbe1fa4c-b0e3-4583-baef-1f1b2e483e56")]
	[ComImport]
	internal interface IVectorView<T> : IIterable<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060065F9 RID: 26105
		T GetAt(uint index);

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x060065FA RID: 26106
		uint Size { get; }

		// Token: 0x060065FB RID: 26107
		bool IndexOf(T value, out uint index);

		// Token: 0x060065FC RID: 26108
		uint GetMany(uint startIndex, [Out] T[] items);
	}
}
