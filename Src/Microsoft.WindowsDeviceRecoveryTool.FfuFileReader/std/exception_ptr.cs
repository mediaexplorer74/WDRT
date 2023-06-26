using System;
using System.Runtime.CompilerServices;

namespace std
{
	// Token: 0x020001A5 RID: 421
	[NativeCppClass]
	internal struct exception_ptr
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x00001454 File Offset: 0x00000854
		public unsafe static void <MarshalCopy>(exception_ptr* A_0, exception_ptr* A_1)
		{
			if (A_0 != null)
			{
				<Module>.__ExceptionPtrCopy((void*)A_0, (void*)A_1);
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00001474 File Offset: 0x00000874
		public unsafe static void <MarshalDestroy>(exception_ptr* A_0)
		{
			<Module>.__ExceptionPtrDestroy((void*)A_0);
		}
	}
}
