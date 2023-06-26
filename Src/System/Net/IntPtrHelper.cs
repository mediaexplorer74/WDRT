using System;

namespace System.Net
{
	// Token: 0x02000113 RID: 275
	internal static class IntPtrHelper
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x0003CBC8 File Offset: 0x0003ADC8
		internal static IntPtr Add(IntPtr a, int b)
		{
			return (IntPtr)((long)a + (long)b);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0003CBD8 File Offset: 0x0003ADD8
		internal static long Subtract(IntPtr a, IntPtr b)
		{
			return (long)a - (long)b;
		}
	}
}
