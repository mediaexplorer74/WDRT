using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x02000596 RID: 1430
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class ValueClassMarshaler
	{
		// Token: 0x060042F9 RID: 17145
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertToNative(IntPtr dst, IntPtr src, IntPtr pMT, ref CleanupWorkList pCleanupWorkList);

		// Token: 0x060042FA RID: 17146
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertToManaged(IntPtr dst, IntPtr src, IntPtr pMT);

		// Token: 0x060042FB RID: 17147
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNative(IntPtr dst, IntPtr pMT);
	}
}
