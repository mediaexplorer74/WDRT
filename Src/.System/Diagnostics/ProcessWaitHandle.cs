using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x02000501 RID: 1281
	internal class ProcessWaitHandle : WaitHandle
	{
		// Token: 0x060030A5 RID: 12453 RVA: 0x000DBA68 File Offset: 0x000D9C68
		internal ProcessWaitHandle(Microsoft.Win32.SafeHandles.SafeProcessHandle processHandle)
		{
			SafeWaitHandle safeWaitHandle = null;
			if (!Microsoft.Win32.NativeMethods.DuplicateHandle(new HandleRef(this, Microsoft.Win32.NativeMethods.GetCurrentProcess()), processHandle, new HandleRef(this, Microsoft.Win32.NativeMethods.GetCurrentProcess()), out safeWaitHandle, 0, false, 2))
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			base.SafeWaitHandle = safeWaitHandle;
		}
	}
}
