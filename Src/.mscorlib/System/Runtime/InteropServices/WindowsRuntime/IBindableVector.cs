using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A1A RID: 2586
	[Guid("393de7de-6fd0-4c0d-bb71-47244a113e93")]
	[ComImport]
	internal interface IBindableVector : IBindableIterable
	{
		// Token: 0x060065FD RID: 26109
		object GetAt(uint index);

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x060065FE RID: 26110
		uint Size { get; }

		// Token: 0x060065FF RID: 26111
		IBindableVectorView GetView();

		// Token: 0x06006600 RID: 26112
		bool IndexOf(object value, out uint index);

		// Token: 0x06006601 RID: 26113
		void SetAt(uint index, object value);

		// Token: 0x06006602 RID: 26114
		void InsertAt(uint index, object value);

		// Token: 0x06006603 RID: 26115
		void RemoveAt(uint index);

		// Token: 0x06006604 RID: 26116
		void Append(object value);

		// Token: 0x06006605 RID: 26117
		void RemoveAtEnd();

		// Token: 0x06006606 RID: 26118
		void Clear();
	}
}
