using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A12 RID: 2578
	[FriendAccessAllowed]
	internal static class WindowsRuntimeBufferHelper
	{
		// Token: 0x060065D6 RID: 26070
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("QCall")]
		private unsafe static extern void StoreOverlappedPtrInCCW(ObjectHandleOnStack windowsRuntimeBuffer, NativeOverlapped* overlapped);

		// Token: 0x060065D7 RID: 26071 RVA: 0x0015B391 File Offset: 0x00159591
		[FriendAccessAllowed]
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal unsafe static void StoreOverlappedInCCW(object windowsRuntimeBuffer, NativeOverlapped* overlapped)
		{
			WindowsRuntimeBufferHelper.StoreOverlappedPtrInCCW(JitHelpers.GetObjectHandleOnStack<object>(ref windowsRuntimeBuffer), overlapped);
		}
	}
}
