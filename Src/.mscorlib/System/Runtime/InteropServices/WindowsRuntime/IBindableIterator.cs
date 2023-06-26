using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A16 RID: 2582
	[Guid("6a1d6c07-076d-49f2-8314-f52c9c9a8331")]
	[ComImport]
	internal interface IBindableIterator
	{
		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x060065DE RID: 26078
		object Current { get; }

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x060065DF RID: 26079
		bool HasCurrent { get; }

		// Token: 0x060065E0 RID: 26080
		bool MoveNext();
	}
}
