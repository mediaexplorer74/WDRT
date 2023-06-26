using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004F6 RID: 1270
	internal class MainWindowFinder
	{
		// Token: 0x0600301E RID: 12318 RVA: 0x000D94FC File Offset: 0x000D76FC
		public IntPtr FindMainWindow(int processId)
		{
			this.bestHandle = (IntPtr)0;
			this.processId = processId;
			Microsoft.Win32.NativeMethods.EnumThreadWindowsCallback enumThreadWindowsCallback = new Microsoft.Win32.NativeMethods.EnumThreadWindowsCallback(this.EnumWindowsCallback);
			Microsoft.Win32.NativeMethods.EnumWindows(enumThreadWindowsCallback, IntPtr.Zero);
			GC.KeepAlive(enumThreadWindowsCallback);
			return this.bestHandle;
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000D9541 File Offset: 0x000D7741
		private bool IsMainWindow(IntPtr handle)
		{
			return !(Microsoft.Win32.NativeMethods.GetWindow(new HandleRef(this, handle), 4) != (IntPtr)0) && Microsoft.Win32.NativeMethods.IsWindowVisible(new HandleRef(this, handle));
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000D9570 File Offset: 0x000D7770
		private bool EnumWindowsCallback(IntPtr handle, IntPtr extraParameter)
		{
			int num;
			Microsoft.Win32.NativeMethods.GetWindowThreadProcessId(new HandleRef(this, handle), out num);
			if (num == this.processId && this.IsMainWindow(handle))
			{
				this.bestHandle = handle;
				return false;
			}
			return true;
		}

		// Token: 0x04002872 RID: 10354
		private IntPtr bestHandle;

		// Token: 0x04002873 RID: 10355
		private int processId;
	}
}
