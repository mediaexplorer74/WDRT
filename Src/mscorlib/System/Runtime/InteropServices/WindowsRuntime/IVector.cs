using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A17 RID: 2583
	[Guid("913337e9-11a1-4345-a3a2-4e7f956e222d")]
	[ComImport]
	internal interface IVector<T> : IIterable<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060065E1 RID: 26081
		T GetAt(uint index);

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x060065E2 RID: 26082
		uint Size { get; }

		// Token: 0x060065E3 RID: 26083
		IReadOnlyList<T> GetView();

		// Token: 0x060065E4 RID: 26084
		bool IndexOf(T value, out uint index);

		// Token: 0x060065E5 RID: 26085
		void SetAt(uint index, T value);

		// Token: 0x060065E6 RID: 26086
		void InsertAt(uint index, T value);

		// Token: 0x060065E7 RID: 26087
		void RemoveAt(uint index);

		// Token: 0x060065E8 RID: 26088
		void Append(T value);

		// Token: 0x060065E9 RID: 26089
		void RemoveAtEnd();

		// Token: 0x060065EA RID: 26090
		void Clear();

		// Token: 0x060065EB RID: 26091
		uint GetMany(uint startIndex, [Out] T[] items);

		// Token: 0x060065EC RID: 26092
		void ReplaceAll(T[] items);
	}
}
