using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
	// Token: 0x02000597 RID: 1431
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class DateMarshaler
	{
		// Token: 0x060042FC RID: 17148
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern double ConvertToNative(DateTime managedDate);

		// Token: 0x060042FD RID: 17149
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long ConvertToManaged(double nativeDate);
	}
}
