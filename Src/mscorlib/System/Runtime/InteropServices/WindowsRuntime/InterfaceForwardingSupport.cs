using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A0B RID: 2571
	[Flags]
	internal enum InterfaceForwardingSupport
	{
		// Token: 0x04002D43 RID: 11587
		None = 0,
		// Token: 0x04002D44 RID: 11588
		IBindableVector = 1,
		// Token: 0x04002D45 RID: 11589
		IVector = 2,
		// Token: 0x04002D46 RID: 11590
		IBindableVectorView = 4,
		// Token: 0x04002D47 RID: 11591
		IVectorView = 8,
		// Token: 0x04002D48 RID: 11592
		IBindableIterableOrIIterable = 16
	}
}
