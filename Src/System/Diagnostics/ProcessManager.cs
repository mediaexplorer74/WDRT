using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x020004F7 RID: 1271
	internal static class ProcessManager
	{
		// Token: 0x06003022 RID: 12322 RVA: 0x000D95B0 File Offset: 0x000D77B0
		static ProcessManager()
		{
			Microsoft.Win32.NativeMethods.LUID luid = default(Microsoft.Win32.NativeMethods.LUID);
			if (!Microsoft.Win32.NativeMethods.LookupPrivilegeValue(null, "SeDebugPrivilege", out luid))
			{
				return;
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				if (Microsoft.Win32.NativeMethods.OpenProcessToken(new HandleRef(null, Microsoft.Win32.NativeMethods.GetCurrentProcess()), 32, out zero))
				{
					Microsoft.Win32.NativeMethods.TokenPrivileges tokenPrivileges = new Microsoft.Win32.NativeMethods.TokenPrivileges();
					tokenPrivileges.PrivilegeCount = 1;
					tokenPrivileges.Luid = luid;
					tokenPrivileges.Attributes = 2;
					Microsoft.Win32.NativeMethods.AdjustTokenPrivileges(new HandleRef(null, zero), false, tokenPrivileges, 0, IntPtr.Zero, IntPtr.Zero);
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					SafeNativeMethods.CloseHandle(zero);
				}
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x000D9650 File Offset: 0x000D7850
		public static bool IsNt
		{
			get
			{
				return Environment.OSVersion.Platform == PlatformID.Win32NT;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06003024 RID: 12324 RVA: 0x000D965F File Offset: 0x000D785F
		public static bool IsOSOlderThanXP
		{
			get
			{
				return Environment.OSVersion.Version.Major < 5 || (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 0);
			}
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000D969C File Offset: 0x000D789C
		public static ProcessInfo GetProcessInfo(int processId, string machineName)
		{
			bool flag = ProcessManager.IsRemoteMachine(machineName);
			if (!flag && ProcessManager.IsNt && Environment.OSVersion.Version.Major >= 5)
			{
				ProcessInfo[] processInfos = NtProcessInfoHelper.GetProcessInfos((int pid) => pid == processId);
				if (processInfos.Length == 1)
				{
					return processInfos[0];
				}
			}
			else
			{
				ProcessInfo[] processInfosCore = ProcessManager.GetProcessInfosCore(machineName, flag);
				foreach (ProcessInfo processInfo in processInfosCore)
				{
					if (processInfo.processId == processId)
					{
						return processInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000D9730 File Offset: 0x000D7930
		public static ProcessInfo[] GetProcessInfos(string machineName)
		{
			bool flag = ProcessManager.IsRemoteMachine(machineName);
			return ProcessManager.GetProcessInfosCore(machineName, flag);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000D974C File Offset: 0x000D794C
		private static ProcessInfo[] GetProcessInfosCore(string machineName, bool isRemoteMachine)
		{
			if (ProcessManager.IsNt)
			{
				if (!isRemoteMachine && Environment.OSVersion.Version.Major >= 5)
				{
					return NtProcessInfoHelper.GetProcessInfos(null);
				}
				return NtProcessManager.GetProcessInfos(machineName, isRemoteMachine);
			}
			else
			{
				if (isRemoteMachine)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinNTRequiredForRemote"));
				}
				return WinProcessManager.GetProcessInfos();
			}
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000D979C File Offset: 0x000D799C
		public static int[] GetProcessIds()
		{
			if (ProcessManager.IsNt)
			{
				return NtProcessManager.GetProcessIds();
			}
			return WinProcessManager.GetProcessIds();
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000D97B0 File Offset: 0x000D79B0
		public static int[] GetProcessIds(string machineName)
		{
			if (!ProcessManager.IsRemoteMachine(machineName))
			{
				return ProcessManager.GetProcessIds();
			}
			if (ProcessManager.IsNt)
			{
				return NtProcessManager.GetProcessIds(machineName, true);
			}
			throw new PlatformNotSupportedException(SR.GetString("WinNTRequiredForRemote"));
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000D97DE File Offset: 0x000D79DE
		public static bool IsProcessRunning(int processId, string machineName)
		{
			return ProcessManager.IsProcessRunning(processId, ProcessManager.GetProcessIds(machineName));
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000D97EC File Offset: 0x000D79EC
		public static bool IsProcessRunning(int processId)
		{
			return ProcessManager.IsProcessRunning(processId, ProcessManager.GetProcessIds());
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000D97FC File Offset: 0x000D79FC
		private static bool IsProcessRunning(int processId, int[] processIds)
		{
			for (int i = 0; i < processIds.Length; i++)
			{
				if (processIds[i] == processId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000D9820 File Offset: 0x000D7A20
		public static int GetProcessIdFromHandle(Microsoft.Win32.SafeHandles.SafeProcessHandle processHandle)
		{
			if (ProcessManager.IsNt)
			{
				return NtProcessManager.GetProcessIdFromHandle(processHandle);
			}
			throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000D9840 File Offset: 0x000D7A40
		public static IntPtr GetMainWindowHandle(int processId)
		{
			MainWindowFinder mainWindowFinder = new MainWindowFinder();
			return mainWindowFinder.FindMainWindow(processId);
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x000D985A File Offset: 0x000D7A5A
		public static ModuleInfo[] GetModuleInfos(int processId)
		{
			if (ProcessManager.IsNt)
			{
				return NtProcessManager.GetModuleInfos(processId);
			}
			return WinProcessManager.GetModuleInfos(processId);
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x000D9870 File Offset: 0x000D7A70
		public static Microsoft.Win32.SafeHandles.SafeProcessHandle OpenProcess(int processId, int access, bool throwIfExited)
		{
			Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = Microsoft.Win32.NativeMethods.OpenProcess(access, false, processId);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!safeProcessHandle.IsInvalid)
			{
				return safeProcessHandle;
			}
			if (processId == 0)
			{
				throw new Win32Exception(5);
			}
			if (ProcessManager.IsProcessRunning(processId))
			{
				throw new Win32Exception(lastWin32Error);
			}
			if (throwIfExited)
			{
				throw new InvalidOperationException(SR.GetString("ProcessHasExited", new object[] { processId.ToString(CultureInfo.CurrentCulture) }));
			}
			return Microsoft.Win32.SafeHandles.SafeProcessHandle.InvalidHandle;
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x000D98DC File Offset: 0x000D7ADC
		public static Microsoft.Win32.SafeHandles.SafeThreadHandle OpenThread(int threadId, int access)
		{
			Microsoft.Win32.SafeHandles.SafeThreadHandle safeThreadHandle2;
			try
			{
				Microsoft.Win32.SafeHandles.SafeThreadHandle safeThreadHandle = Microsoft.Win32.NativeMethods.OpenThread(access, false, threadId);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (safeThreadHandle.IsInvalid)
				{
					if (lastWin32Error == 87)
					{
						throw new InvalidOperationException(SR.GetString("ThreadExited", new object[] { threadId.ToString(CultureInfo.CurrentCulture) }));
					}
					throw new Win32Exception(lastWin32Error);
				}
				else
				{
					safeThreadHandle2 = safeThreadHandle;
				}
			}
			catch (EntryPointNotFoundException ex)
			{
				throw new PlatformNotSupportedException(SR.GetString("Win2000Required"), ex);
			}
			return safeThreadHandle2;
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x000D9958 File Offset: 0x000D7B58
		public static bool IsRemoteMachine(string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if (machineName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "machineName", machineName }));
			}
			string text;
			if (machineName.StartsWith("\\", StringComparison.Ordinal))
			{
				text = machineName.Substring(2);
			}
			else
			{
				text = machineName;
			}
			if (text.Equals("."))
			{
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder(256);
			SafeNativeMethods.GetComputerName(stringBuilder, new int[] { stringBuilder.Capacity });
			string text2 = stringBuilder.ToString();
			return string.Compare(text2, text, StringComparison.OrdinalIgnoreCase) != 0;
		}
	}
}
