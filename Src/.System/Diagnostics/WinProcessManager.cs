using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004F8 RID: 1272
	internal static class WinProcessManager
	{
		// Token: 0x06003033 RID: 12339 RVA: 0x000D99FC File Offset: 0x000D7BFC
		public static int[] GetProcessIds()
		{
			ProcessInfo[] processInfos = WinProcessManager.GetProcessInfos();
			int[] array = new int[processInfos.Length];
			for (int i = 0; i < processInfos.Length; i++)
			{
				array[i] = processInfos[i].processId;
			}
			return array;
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x000D9A34 File Offset: 0x000D7C34
		public static ProcessInfo[] GetProcessInfos()
		{
			IntPtr intPtr = (IntPtr)(-1);
			GCHandle gchandle = default(GCHandle);
			ArrayList arrayList = new ArrayList();
			Hashtable hashtable = new Hashtable();
			try
			{
				intPtr = Microsoft.Win32.NativeMethods.CreateToolhelp32Snapshot(6, 0);
				if (intPtr == (IntPtr)(-1))
				{
					throw new Win32Exception();
				}
				int num = Marshal.SizeOf(typeof(Microsoft.Win32.NativeMethods.WinProcessEntry));
				int num2 = num + 260;
				int[] array = new int[num2 / 4];
				gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				IntPtr intPtr2 = gchandle.AddrOfPinnedObject();
				Marshal.WriteInt32(intPtr2, num2);
				HandleRef handleRef = new HandleRef(null, intPtr);
				if (Microsoft.Win32.NativeMethods.Process32First(handleRef, intPtr2))
				{
					do
					{
						Microsoft.Win32.NativeMethods.WinProcessEntry winProcessEntry = new Microsoft.Win32.NativeMethods.WinProcessEntry();
						Marshal.PtrToStructure(intPtr2, winProcessEntry);
						ProcessInfo processInfo = new ProcessInfo();
						string text = Marshal.PtrToStringAnsi((IntPtr)((long)intPtr2 + (long)num));
						processInfo.processName = Path.ChangeExtension(Path.GetFileName(text), null);
						processInfo.handleCount = winProcessEntry.cntUsage;
						processInfo.processId = winProcessEntry.th32ProcessID;
						processInfo.basePriority = winProcessEntry.pcPriClassBase;
						processInfo.mainModuleId = winProcessEntry.th32ModuleID;
						hashtable.Add(processInfo.processId, processInfo);
						Marshal.WriteInt32(intPtr2, num2);
					}
					while (Microsoft.Win32.NativeMethods.Process32Next(handleRef, intPtr2));
				}
				Microsoft.Win32.NativeMethods.WinThreadEntry winThreadEntry = new Microsoft.Win32.NativeMethods.WinThreadEntry();
				winThreadEntry.dwSize = Marshal.SizeOf(winThreadEntry);
				if (Microsoft.Win32.NativeMethods.Thread32First(handleRef, winThreadEntry))
				{
					do
					{
						arrayList.Add(new ThreadInfo
						{
							threadId = winThreadEntry.th32ThreadID,
							processId = winThreadEntry.th32OwnerProcessID,
							basePriority = winThreadEntry.tpBasePri,
							currentPriority = winThreadEntry.tpBasePri + winThreadEntry.tpDeltaPri
						});
					}
					while (Microsoft.Win32.NativeMethods.Thread32Next(handleRef, winThreadEntry));
				}
				for (int i = 0; i < arrayList.Count; i++)
				{
					ThreadInfo threadInfo = (ThreadInfo)arrayList[i];
					ProcessInfo processInfo2 = (ProcessInfo)hashtable[threadInfo.processId];
					if (processInfo2 != null)
					{
						processInfo2.threadInfoList.Add(threadInfo);
					}
				}
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (intPtr != (IntPtr)(-1))
				{
					SafeNativeMethods.CloseHandle(intPtr);
				}
			}
			ProcessInfo[] array2 = new ProcessInfo[hashtable.Values.Count];
			hashtable.Values.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x000D9CA8 File Offset: 0x000D7EA8
		public static ModuleInfo[] GetModuleInfos(int processId)
		{
			IntPtr intPtr = (IntPtr)(-1);
			GCHandle gchandle = default(GCHandle);
			ArrayList arrayList = new ArrayList();
			try
			{
				intPtr = Microsoft.Win32.NativeMethods.CreateToolhelp32Snapshot(8, processId);
				if (intPtr == (IntPtr)(-1))
				{
					throw new Win32Exception();
				}
				int num = Marshal.SizeOf(typeof(Microsoft.Win32.NativeMethods.WinModuleEntry));
				int num2 = num + 260 + 256;
				int[] array = new int[num2 / 4];
				gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				IntPtr intPtr2 = gchandle.AddrOfPinnedObject();
				Marshal.WriteInt32(intPtr2, num2);
				HandleRef handleRef = new HandleRef(null, intPtr);
				if (Microsoft.Win32.NativeMethods.Module32First(handleRef, intPtr2))
				{
					do
					{
						Microsoft.Win32.NativeMethods.WinModuleEntry winModuleEntry = new Microsoft.Win32.NativeMethods.WinModuleEntry();
						Marshal.PtrToStructure(intPtr2, winModuleEntry);
						arrayList.Add(new ModuleInfo
						{
							baseName = Marshal.PtrToStringAnsi((IntPtr)((long)intPtr2 + (long)num)),
							fileName = Marshal.PtrToStringAnsi((IntPtr)((long)intPtr2 + (long)num + 256L)),
							baseOfDll = winModuleEntry.modBaseAddr,
							sizeOfImage = winModuleEntry.modBaseSize,
							Id = winModuleEntry.th32ModuleID
						});
						Marshal.WriteInt32(intPtr2, num2);
					}
					while (Microsoft.Win32.NativeMethods.Module32Next(handleRef, intPtr2));
				}
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (intPtr != (IntPtr)(-1))
				{
					SafeNativeMethods.CloseHandle(intPtr);
				}
			}
			ModuleInfo[] array2 = new ModuleInfo[arrayList.Count];
			arrayList.CopyTo(array2, 0);
			return array2;
		}
	}
}
