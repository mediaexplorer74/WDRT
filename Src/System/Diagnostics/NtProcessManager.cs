using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x020004F9 RID: 1273
	internal static class NtProcessManager
	{
		// Token: 0x06003036 RID: 12342 RVA: 0x000D9E40 File Offset: 0x000D8040
		static NtProcessManager()
		{
			NtProcessManager.valueIds.Add("Handle Count", NtProcessManager.ValueId.HandleCount);
			NtProcessManager.valueIds.Add("Pool Paged Bytes", NtProcessManager.ValueId.PoolPagedBytes);
			NtProcessManager.valueIds.Add("Pool Nonpaged Bytes", NtProcessManager.ValueId.PoolNonpagedBytes);
			NtProcessManager.valueIds.Add("Elapsed Time", NtProcessManager.ValueId.ElapsedTime);
			NtProcessManager.valueIds.Add("Virtual Bytes Peak", NtProcessManager.ValueId.VirtualBytesPeak);
			NtProcessManager.valueIds.Add("Virtual Bytes", NtProcessManager.ValueId.VirtualBytes);
			NtProcessManager.valueIds.Add("Private Bytes", NtProcessManager.ValueId.PrivateBytes);
			NtProcessManager.valueIds.Add("Page File Bytes", NtProcessManager.ValueId.PageFileBytes);
			NtProcessManager.valueIds.Add("Page File Bytes Peak", NtProcessManager.ValueId.PageFileBytesPeak);
			NtProcessManager.valueIds.Add("Working Set Peak", NtProcessManager.ValueId.WorkingSetPeak);
			NtProcessManager.valueIds.Add("Working Set", NtProcessManager.ValueId.WorkingSet);
			NtProcessManager.valueIds.Add("ID Thread", NtProcessManager.ValueId.ThreadId);
			NtProcessManager.valueIds.Add("ID Process", NtProcessManager.ValueId.ProcessId);
			NtProcessManager.valueIds.Add("Priority Base", NtProcessManager.ValueId.BasePriority);
			NtProcessManager.valueIds.Add("Priority Current", NtProcessManager.ValueId.CurrentPriority);
			NtProcessManager.valueIds.Add("% User Time", NtProcessManager.ValueId.UserTime);
			NtProcessManager.valueIds.Add("% Privileged Time", NtProcessManager.ValueId.PrivilegedTime);
			NtProcessManager.valueIds.Add("Start Address", NtProcessManager.ValueId.StartAddress);
			NtProcessManager.valueIds.Add("Thread State", NtProcessManager.ValueId.ThreadState);
			NtProcessManager.valueIds.Add("Thread Wait Reason", NtProcessManager.ValueId.ThreadWaitReason);
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x000DA006 File Offset: 0x000D8206
		internal static int SystemProcessID
		{
			get
			{
				if (ProcessManager.IsOSOlderThanXP)
				{
					return 8;
				}
				return 4;
			}
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x000DA014 File Offset: 0x000D8214
		public static int[] GetProcessIds(string machineName, bool isRemoteMachine)
		{
			ProcessInfo[] processInfos = NtProcessManager.GetProcessInfos(machineName, isRemoteMachine);
			int[] array = new int[processInfos.Length];
			for (int i = 0; i < processInfos.Length; i++)
			{
				array[i] = processInfos[i].processId;
			}
			return array;
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000DA04C File Offset: 0x000D824C
		public static int[] GetProcessIds()
		{
			int[] array = new int[256];
			int num;
			while (Microsoft.Win32.NativeMethods.EnumProcesses(array, array.Length * 4, out num))
			{
				if (num != array.Length * 4)
				{
					int[] array2 = new int[num / 4];
					Array.Copy(array, array2, array2.Length);
					return array2;
				}
				array = new int[array.Length * 2];
			}
			throw new Win32Exception();
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000DA0A2 File Offset: 0x000D82A2
		public static ModuleInfo[] GetModuleInfos(int processId)
		{
			return NtProcessManager.GetModuleInfos(processId, false);
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000DA0AC File Offset: 0x000D82AC
		public static ModuleInfo GetFirstModuleInfo(int processId)
		{
			ModuleInfo[] moduleInfos = NtProcessManager.GetModuleInfos(processId, true);
			if (moduleInfos.Length == 0)
			{
				return null;
			}
			return moduleInfos[0];
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000DA0CC File Offset: 0x000D82CC
		private static ModuleInfo[] GetModuleInfos(int processId, bool firstModuleOnly)
		{
			if (processId == NtProcessManager.SystemProcessID || processId == 0)
			{
				throw new Win32Exception(-2147467259, SR.GetString("EnumProcessModuleFailed"));
			}
			Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = Microsoft.Win32.SafeHandles.SafeProcessHandle.InvalidHandle;
			ModuleInfo[] array3;
			try
			{
				safeProcessHandle = ProcessManager.OpenProcess(processId, 1040, true);
				IntPtr[] array = new IntPtr[64];
				GCHandle gchandle = default(GCHandle);
				int num = 0;
				for (;;)
				{
					bool flag = false;
					try
					{
						gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
						flag = Microsoft.Win32.NativeMethods.EnumProcessModules(safeProcessHandle, gchandle.AddrOfPinnedObject(), array.Length * IntPtr.Size, ref num);
						if (!flag)
						{
							bool flag2 = false;
							bool flag3 = false;
							if (!ProcessManager.IsOSOlderThanXP)
							{
								Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle2 = Microsoft.Win32.SafeHandles.SafeProcessHandle.InvalidHandle;
								try
								{
									safeProcessHandle2 = ProcessManager.OpenProcess(Microsoft.Win32.NativeMethods.GetCurrentProcessId(), 1024, true);
									if (!SafeNativeMethods.IsWow64Process(safeProcessHandle2, ref flag2))
									{
										throw new Win32Exception();
									}
									if (!SafeNativeMethods.IsWow64Process(safeProcessHandle, ref flag3))
									{
										throw new Win32Exception();
									}
									if (flag2 && !flag3)
									{
										throw new Win32Exception(299, SR.GetString("EnumProcessModuleFailedDueToWow"));
									}
								}
								finally
								{
									if (safeProcessHandle2 != Microsoft.Win32.SafeHandles.SafeProcessHandle.InvalidHandle)
									{
										safeProcessHandle2.Close();
									}
								}
							}
							for (int i = 0; i < 50; i++)
							{
								flag = Microsoft.Win32.NativeMethods.EnumProcessModules(safeProcessHandle, gchandle.AddrOfPinnedObject(), array.Length * IntPtr.Size, ref num);
								if (flag)
								{
									break;
								}
								Thread.Sleep(1);
							}
						}
					}
					finally
					{
						gchandle.Free();
					}
					if (!flag)
					{
						break;
					}
					num /= IntPtr.Size;
					if (num <= array.Length)
					{
						goto IL_159;
					}
					array = new IntPtr[array.Length * 2];
				}
				throw new Win32Exception();
				IL_159:
				ArrayList arrayList = new ArrayList();
				for (int j = 0; j < num; j++)
				{
					try
					{
						ModuleInfo moduleInfo = new ModuleInfo();
						IntPtr intPtr = array[j];
						Microsoft.Win32.NativeMethods.NtModuleInfo ntModuleInfo = new Microsoft.Win32.NativeMethods.NtModuleInfo();
						if (!Microsoft.Win32.NativeMethods.GetModuleInformation(safeProcessHandle, new HandleRef(null, intPtr), ntModuleInfo, Marshal.SizeOf(ntModuleInfo)))
						{
							throw new Win32Exception();
						}
						moduleInfo.sizeOfImage = ntModuleInfo.SizeOfImage;
						moduleInfo.entryPoint = ntModuleInfo.EntryPoint;
						moduleInfo.baseOfDll = ntModuleInfo.BaseOfDll;
						StringBuilder stringBuilder = new StringBuilder(1024);
						if (Microsoft.Win32.NativeMethods.GetModuleBaseName(safeProcessHandle, new HandleRef(null, intPtr), stringBuilder, stringBuilder.Capacity * 2) == 0)
						{
							throw new Win32Exception();
						}
						moduleInfo.baseName = stringBuilder.ToString();
						StringBuilder stringBuilder2 = new StringBuilder(1024);
						if (Microsoft.Win32.NativeMethods.GetModuleFileNameEx(safeProcessHandle, new HandleRef(null, intPtr), stringBuilder2, stringBuilder2.Capacity * 2) == 0)
						{
							throw new Win32Exception();
						}
						moduleInfo.fileName = stringBuilder2.ToString();
						if (string.Compare(moduleInfo.fileName, "\\SystemRoot\\System32\\smss.exe", StringComparison.OrdinalIgnoreCase) == 0)
						{
							moduleInfo.fileName = Path.Combine(Environment.SystemDirectory, "smss.exe");
						}
						if (moduleInfo.fileName != null && moduleInfo.fileName.Length >= 4 && moduleInfo.fileName.StartsWith("\\\\?\\", StringComparison.Ordinal))
						{
							moduleInfo.fileName = moduleInfo.fileName.Substring(4);
						}
						arrayList.Add(moduleInfo);
					}
					catch (Win32Exception ex)
					{
						if (ex.NativeErrorCode != 6 && ex.NativeErrorCode != 299)
						{
							throw;
						}
					}
					if (firstModuleOnly)
					{
						break;
					}
				}
				ModuleInfo[] array2 = new ModuleInfo[arrayList.Count];
				arrayList.CopyTo(array2, 0);
				array3 = array2;
			}
			finally
			{
				if (!safeProcessHandle.IsInvalid)
				{
					safeProcessHandle.Close();
				}
			}
			return array3;
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000DA458 File Offset: 0x000D8658
		public static int GetProcessIdFromHandle(Microsoft.Win32.SafeHandles.SafeProcessHandle processHandle)
		{
			Microsoft.Win32.NativeMethods.NtProcessBasicInfo ntProcessBasicInfo = new Microsoft.Win32.NativeMethods.NtProcessBasicInfo();
			int num = Microsoft.Win32.NativeMethods.NtQueryInformationProcess(processHandle, 0, ntProcessBasicInfo, Marshal.SizeOf(ntProcessBasicInfo), null);
			if (num != 0)
			{
				throw new InvalidOperationException(SR.GetString("CantGetProcessId"), new Win32Exception(num));
			}
			return ntProcessBasicInfo.UniqueProcessId.ToInt32();
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000DA4A0 File Offset: 0x000D86A0
		public static ProcessInfo[] GetProcessInfos(string machineName, bool isRemoteMachine)
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			ProcessInfo[] processInfos;
			try
			{
				PerformanceCounterLib performanceCounterLib = PerformanceCounterLib.GetPerformanceCounterLib(machineName, new CultureInfo(9));
				processInfos = NtProcessManager.GetProcessInfos(performanceCounterLib);
			}
			catch (Exception ex)
			{
				if (isRemoteMachine)
				{
					throw new InvalidOperationException(SR.GetString("CouldntConnectToRemoteMachine"), ex);
				}
				throw ex;
			}
			return processInfos;
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000DA4FC File Offset: 0x000D86FC
		private static ProcessInfo[] GetProcessInfos(PerformanceCounterLib library)
		{
			ProcessInfo[] array = new ProcessInfo[0];
			int num = 5;
			while (array.Length == 0 && num != 0)
			{
				try
				{
					byte[] performanceData = library.GetPerformanceData("230 232");
					array = NtProcessManager.GetProcessInfos(library, 230, 232, performanceData);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(SR.GetString("CouldntGetProcessInfos"), ex);
				}
				num--;
			}
			if (array.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("ProcessDisabled"));
			}
			return array;
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000DA57C File Offset: 0x000D877C
		private static ProcessInfo[] GetProcessInfos(PerformanceCounterLib library, int processIndex, int threadIndex, byte[] data)
		{
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			GCHandle gchandle = default(GCHandle);
			try
			{
				gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
				IntPtr intPtr = gchandle.AddrOfPinnedObject();
				Microsoft.Win32.NativeMethods.PERF_DATA_BLOCK perf_DATA_BLOCK = new Microsoft.Win32.NativeMethods.PERF_DATA_BLOCK();
				Marshal.PtrToStructure(intPtr, perf_DATA_BLOCK);
				IntPtr intPtr2 = (IntPtr)((long)intPtr + (long)perf_DATA_BLOCK.HeaderLength);
				Microsoft.Win32.NativeMethods.PERF_INSTANCE_DEFINITION perf_INSTANCE_DEFINITION = new Microsoft.Win32.NativeMethods.PERF_INSTANCE_DEFINITION();
				Microsoft.Win32.NativeMethods.PERF_COUNTER_BLOCK perf_COUNTER_BLOCK = new Microsoft.Win32.NativeMethods.PERF_COUNTER_BLOCK();
				for (int i = 0; i < perf_DATA_BLOCK.NumObjectTypes; i++)
				{
					Microsoft.Win32.NativeMethods.PERF_OBJECT_TYPE perf_OBJECT_TYPE = new Microsoft.Win32.NativeMethods.PERF_OBJECT_TYPE();
					Marshal.PtrToStructure(intPtr2, perf_OBJECT_TYPE);
					IntPtr intPtr3 = (IntPtr)((long)intPtr2 + (long)perf_OBJECT_TYPE.DefinitionLength);
					IntPtr intPtr4 = (IntPtr)((long)intPtr2 + (long)perf_OBJECT_TYPE.HeaderLength);
					ArrayList arrayList2 = new ArrayList();
					for (int j = 0; j < perf_OBJECT_TYPE.NumCounters; j++)
					{
						Microsoft.Win32.NativeMethods.PERF_COUNTER_DEFINITION perf_COUNTER_DEFINITION = new Microsoft.Win32.NativeMethods.PERF_COUNTER_DEFINITION();
						Marshal.PtrToStructure(intPtr4, perf_COUNTER_DEFINITION);
						string counterName = library.GetCounterName(perf_COUNTER_DEFINITION.CounterNameTitleIndex);
						if (perf_OBJECT_TYPE.ObjectNameTitleIndex == processIndex)
						{
							perf_COUNTER_DEFINITION.CounterNameTitlePtr = (int)NtProcessManager.GetValueId(counterName);
						}
						else if (perf_OBJECT_TYPE.ObjectNameTitleIndex == threadIndex)
						{
							perf_COUNTER_DEFINITION.CounterNameTitlePtr = (int)NtProcessManager.GetValueId(counterName);
						}
						arrayList2.Add(perf_COUNTER_DEFINITION);
						intPtr4 = (IntPtr)((long)intPtr4 + (long)perf_COUNTER_DEFINITION.ByteLength);
					}
					Microsoft.Win32.NativeMethods.PERF_COUNTER_DEFINITION[] array = new Microsoft.Win32.NativeMethods.PERF_COUNTER_DEFINITION[arrayList2.Count];
					arrayList2.CopyTo(array, 0);
					for (int k = 0; k < perf_OBJECT_TYPE.NumInstances; k++)
					{
						Marshal.PtrToStructure(intPtr3, perf_INSTANCE_DEFINITION);
						IntPtr intPtr5 = (IntPtr)((long)intPtr3 + (long)perf_INSTANCE_DEFINITION.NameOffset);
						string text = Marshal.PtrToStringUni(intPtr5);
						if (!text.Equals("_Total"))
						{
							IntPtr intPtr6 = (IntPtr)((long)intPtr3 + (long)perf_INSTANCE_DEFINITION.ByteLength);
							Marshal.PtrToStructure(intPtr6, perf_COUNTER_BLOCK);
							if (perf_OBJECT_TYPE.ObjectNameTitleIndex == processIndex)
							{
								ProcessInfo processInfo = NtProcessManager.GetProcessInfo(perf_OBJECT_TYPE, (IntPtr)((long)intPtr3 + (long)perf_INSTANCE_DEFINITION.ByteLength), array);
								if ((processInfo.processId != 0 || string.Compare(text, "Idle", StringComparison.OrdinalIgnoreCase) == 0) && hashtable[processInfo.processId] == null)
								{
									string text2 = text;
									if (text2.Length == 15)
									{
										if (text.EndsWith(".", StringComparison.Ordinal))
										{
											text2 = text.Substring(0, 14);
										}
										else if (text.EndsWith(".e", StringComparison.Ordinal))
										{
											text2 = text.Substring(0, 13);
										}
										else if (text.EndsWith(".ex", StringComparison.Ordinal))
										{
											text2 = text.Substring(0, 12);
										}
									}
									processInfo.processName = text2;
									hashtable.Add(processInfo.processId, processInfo);
								}
							}
							else if (perf_OBJECT_TYPE.ObjectNameTitleIndex == threadIndex)
							{
								ThreadInfo threadInfo = NtProcessManager.GetThreadInfo(perf_OBJECT_TYPE, (IntPtr)((long)intPtr3 + (long)perf_INSTANCE_DEFINITION.ByteLength), array);
								if (threadInfo.threadId != 0)
								{
									arrayList.Add(threadInfo);
								}
							}
							intPtr3 = (IntPtr)((long)intPtr3 + (long)perf_INSTANCE_DEFINITION.ByteLength + (long)perf_COUNTER_BLOCK.ByteLength);
						}
					}
					intPtr2 = (IntPtr)((long)intPtr2 + (long)perf_OBJECT_TYPE.TotalByteLength);
				}
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			for (int l = 0; l < arrayList.Count; l++)
			{
				ThreadInfo threadInfo2 = (ThreadInfo)arrayList[l];
				ProcessInfo processInfo2 = (ProcessInfo)hashtable[threadInfo2.processId];
				if (processInfo2 != null)
				{
					processInfo2.threadInfoList.Add(threadInfo2);
				}
			}
			ProcessInfo[] array2 = new ProcessInfo[hashtable.Values.Count];
			hashtable.Values.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000DA950 File Offset: 0x000D8B50
		private static ThreadInfo GetThreadInfo(Microsoft.Win32.NativeMethods.PERF_OBJECT_TYPE type, IntPtr instancePtr, Microsoft.Win32.NativeMethods.PERF_COUNTER_DEFINITION[] counters)
		{
			ThreadInfo threadInfo = new ThreadInfo();
			foreach (Microsoft.Win32.NativeMethods.PERF_COUNTER_DEFINITION perf_COUNTER_DEFINITION in counters)
			{
				long num = NtProcessManager.ReadCounterValue(perf_COUNTER_DEFINITION.CounterType, (IntPtr)((long)instancePtr + (long)perf_COUNTER_DEFINITION.CounterOffset));
				switch (perf_COUNTER_DEFINITION.CounterNameTitlePtr)
				{
				case 11:
					threadInfo.threadId = (int)num;
					break;
				case 12:
					threadInfo.processId = (int)num;
					break;
				case 13:
					threadInfo.basePriority = (int)num;
					break;
				case 14:
					threadInfo.currentPriority = (int)num;
					break;
				case 17:
					threadInfo.startAddress = (IntPtr)num;
					break;
				case 18:
					threadInfo.threadState = (ThreadState)num;
					break;
				case 19:
					threadInfo.threadWaitReason = NtProcessManager.GetThreadWaitReason((int)num);
					break;
				}
			}
			return threadInfo;
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000DAA20 File Offset: 0x000D8C20
		internal static ThreadWaitReason GetThreadWaitReason(int value)
		{
			switch (value)
			{
			case 0:
			case 7:
				return ThreadWaitReason.Executive;
			case 1:
			case 8:
				return ThreadWaitReason.FreePage;
			case 2:
			case 9:
				return ThreadWaitReason.PageIn;
			case 3:
			case 10:
				return ThreadWaitReason.SystemAllocation;
			case 4:
			case 11:
				return ThreadWaitReason.ExecutionDelay;
			case 5:
			case 12:
				return ThreadWaitReason.Suspended;
			case 6:
			case 13:
				return ThreadWaitReason.UserRequest;
			case 14:
				return ThreadWaitReason.EventPairHigh;
			case 15:
				return ThreadWaitReason.EventPairLow;
			case 16:
				return ThreadWaitReason.LpcReceive;
			case 17:
				return ThreadWaitReason.LpcReply;
			case 18:
				return ThreadWaitReason.VirtualMemory;
			case 19:
				return ThreadWaitReason.PageOut;
			default:
				return ThreadWaitReason.Unknown;
			}
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000DAAA8 File Offset: 0x000D8CA8
		private static ProcessInfo GetProcessInfo(Microsoft.Win32.NativeMethods.PERF_OBJECT_TYPE type, IntPtr instancePtr, Microsoft.Win32.NativeMethods.PERF_COUNTER_DEFINITION[] counters)
		{
			ProcessInfo processInfo = new ProcessInfo();
			foreach (Microsoft.Win32.NativeMethods.PERF_COUNTER_DEFINITION perf_COUNTER_DEFINITION in counters)
			{
				long num = NtProcessManager.ReadCounterValue(perf_COUNTER_DEFINITION.CounterType, (IntPtr)((long)instancePtr + (long)perf_COUNTER_DEFINITION.CounterOffset));
				switch (perf_COUNTER_DEFINITION.CounterNameTitlePtr)
				{
				case 0:
					processInfo.handleCount = (int)num;
					break;
				case 1:
					processInfo.poolPagedBytes = num;
					break;
				case 2:
					processInfo.poolNonpagedBytes = num;
					break;
				case 4:
					processInfo.virtualBytesPeak = num;
					break;
				case 5:
					processInfo.virtualBytes = num;
					break;
				case 6:
					processInfo.privateBytes = num;
					break;
				case 7:
					processInfo.pageFileBytes = num;
					break;
				case 8:
					processInfo.pageFileBytesPeak = num;
					break;
				case 9:
					processInfo.workingSetPeak = num;
					break;
				case 10:
					processInfo.workingSet = num;
					break;
				case 12:
					processInfo.processId = (int)num;
					break;
				case 13:
					processInfo.basePriority = (int)num;
					break;
				}
			}
			return processInfo;
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000DABAC File Offset: 0x000D8DAC
		private static NtProcessManager.ValueId GetValueId(string counterName)
		{
			if (counterName != null)
			{
				object obj = NtProcessManager.valueIds[counterName];
				if (obj != null)
				{
					return (NtProcessManager.ValueId)obj;
				}
			}
			return NtProcessManager.ValueId.Unknown;
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000DABD3 File Offset: 0x000D8DD3
		private static long ReadCounterValue(int counterType, IntPtr dataPtr)
		{
			if ((counterType & 256) != 0)
			{
				return Marshal.ReadInt64(dataPtr);
			}
			return (long)Marshal.ReadInt32(dataPtr);
		}

		// Token: 0x04002874 RID: 10356
		private const int ProcessPerfCounterId = 230;

		// Token: 0x04002875 RID: 10357
		private const int ThreadPerfCounterId = 232;

		// Token: 0x04002876 RID: 10358
		private const string PerfCounterQueryString = "230 232";

		// Token: 0x04002877 RID: 10359
		internal const int IdleProcessID = 0;

		// Token: 0x04002878 RID: 10360
		private static Hashtable valueIds = new Hashtable();

		// Token: 0x02000880 RID: 2176
		private enum ValueId
		{
			// Token: 0x04003733 RID: 14131
			Unknown = -1,
			// Token: 0x04003734 RID: 14132
			HandleCount,
			// Token: 0x04003735 RID: 14133
			PoolPagedBytes,
			// Token: 0x04003736 RID: 14134
			PoolNonpagedBytes,
			// Token: 0x04003737 RID: 14135
			ElapsedTime,
			// Token: 0x04003738 RID: 14136
			VirtualBytesPeak,
			// Token: 0x04003739 RID: 14137
			VirtualBytes,
			// Token: 0x0400373A RID: 14138
			PrivateBytes,
			// Token: 0x0400373B RID: 14139
			PageFileBytes,
			// Token: 0x0400373C RID: 14140
			PageFileBytesPeak,
			// Token: 0x0400373D RID: 14141
			WorkingSetPeak,
			// Token: 0x0400373E RID: 14142
			WorkingSet,
			// Token: 0x0400373F RID: 14143
			ThreadId,
			// Token: 0x04003740 RID: 14144
			ProcessId,
			// Token: 0x04003741 RID: 14145
			BasePriority,
			// Token: 0x04003742 RID: 14146
			CurrentPriority,
			// Token: 0x04003743 RID: 14147
			UserTime,
			// Token: 0x04003744 RID: 14148
			PrivilegedTime,
			// Token: 0x04003745 RID: 14149
			StartAddress,
			// Token: 0x04003746 RID: 14150
			ThreadState,
			// Token: 0x04003747 RID: 14151
			ThreadWaitReason
		}
	}
}
