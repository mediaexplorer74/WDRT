using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x02000598 RID: 1432
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	[FriendAccessAllowed]
	internal static class InterfaceMarshaler
	{
		// Token: 0x060042FE RID: 17150
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ConvertToNative(object objSrc, IntPtr itfMT, IntPtr classMT, int flags);

		// Token: 0x060042FF RID: 17151
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object ConvertToManaged(IntPtr pUnk, IntPtr itfMT, IntPtr classMT, int flags);

		// Token: 0x06004300 RID: 17152
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall")]
		internal static extern void ClearNative(IntPtr pUnk);

		// Token: 0x06004301 RID: 17153
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object ConvertToManagedWithoutUnboxing(IntPtr pNative);
	}
}
