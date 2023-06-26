using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A18 RID: 2584
	[Guid("913337e9-11a1-4345-a3a2-4e7f956e222d")]
	[ComImport]
	internal interface IVector_Raw<T> : IIterable<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060065ED RID: 26093
		T GetAt(uint index);

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x060065EE RID: 26094
		uint Size { get; }

		// Token: 0x060065EF RID: 26095
		IVectorView<T> GetView();

		// Token: 0x060065F0 RID: 26096
		bool IndexOf(T value, out uint index);

		// Token: 0x060065F1 RID: 26097
		void SetAt(uint index, T value);

		// Token: 0x060065F2 RID: 26098
		void InsertAt(uint index, T value);

		// Token: 0x060065F3 RID: 26099
		void RemoveAt(uint index);

		// Token: 0x060065F4 RID: 26100
		void Append(T value);

		// Token: 0x060065F5 RID: 26101
		void RemoveAtEnd();

		// Token: 0x060065F6 RID: 26102
		void Clear();

		// Token: 0x060065F7 RID: 26103
		uint GetMany(uint startIndex, [Out] T[] items);

		// Token: 0x060065F8 RID: 26104
		void ReplaceAll(T[] items);
	}
}
