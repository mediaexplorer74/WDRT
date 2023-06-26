using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A1B RID: 2587
	[Guid("346dd6e7-976e-4bc3-815d-ece243bc0f33")]
	[ComImport]
	internal interface IBindableVectorView : IBindableIterable
	{
		// Token: 0x06006607 RID: 26119
		object GetAt(uint index);

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06006608 RID: 26120
		uint Size { get; }

		// Token: 0x06006609 RID: 26121
		bool IndexOf(object value, out uint index);
	}
}
