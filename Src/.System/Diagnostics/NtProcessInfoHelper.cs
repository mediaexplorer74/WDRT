using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004FA RID: 1274
	internal static class NtProcessInfoHelper
	{
		// Token: 0x06003046 RID: 12358 RVA: 0x000DABEC File Offset: 0x000D8DEC
		private static int GetNewBufferSize(int existingBufferSize, int requiredSize)
		{
			if (requiredSize == 0)
			{
				int num = existingBufferSize * 2;
				if (num < existingBufferSize)
				{
					throw new OutOfMemoryException();
				}
				return num;
			}
			else
			{
				int num2 = requiredSize + 10240;
				if (num2 < requiredSize)
				{
					throw new OutOfMemoryException();
				}
				return num2;
			}
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000DAC20 File Offset: 0x000D8E20
		public static ProcessInfo[] GetProcessInfos(Predicate<int> processIdFilter = null)
		{
			int num = 0;
			GCHandle gchandle = default(GCHandle);
			int num2 = 131072;
			long[] array = Interlocked.Exchange<long[]>(ref NtProcessInfoHelper.CachedBuffer, null);
			ProcessInfo[] processInfos;
			try
			{
				int num3;
				do
				{
					if (array == null)
					{
						array = new long[(num2 + 7) / 8];
					}
					else
					{
						num2 = array.Length * 8;
					}
					gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
					num3 = Microsoft.Win32.NativeMethods.NtQuerySystemInformation(5, gchandle.AddrOfPinnedObject(), num2, out num);
					if (num3 == -1073741820)
					{
						if (gchandle.IsAllocated)
						{
							gchandle.Free();
						}
						array = null;
						num2 = NtProcessInfoHelper.GetNewBufferSize(num2, num);
					}
				}
				while (num3 == -1073741820);
				if (num3 < 0)
				{
					throw new InvalidOperationException(SR.GetString("CouldntGetProcessInfos"), new Win32Exception(num3));
				}
				processInfos = NtProcessInfoHelper.GetProcessInfos(gchandle.AddrOfPinnedObject(), processIdFilter);
			}
			finally
			{
				Interlocked.Exchange<long[]>(ref NtProcessInfoHelper.CachedBuffer, array);
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return processInfos;
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000DAD08 File Offset: 0x000D8F08
		private static ProcessInfo[] GetProcessInfos(IntPtr dataPtr, Predicate<int> processIdFilter)
		{
			Hashtable hashtable = new Hashtable(60);
			long num = 0L;
			for (;;)
			{
				IntPtr intPtr = (IntPtr)((long)dataPtr + num);
				NtProcessInfoHelper.SystemProcessInformation systemProcessInformation = new NtProcessInfoHelper.SystemProcessInformation();
				Marshal.PtrToStructure(intPtr, systemProcessInformation);
				int num2 = systemProcessInformation.UniqueProcessId.ToInt32();
				if (processIdFilter == null || processIdFilter(num2))
				{
					ProcessInfo processInfo = new ProcessInfo();
					processInfo.processId = num2;
					processInfo.handleCount = (int)systemProcessInformation.HandleCount;
					processInfo.sessionId = (int)systemProcessInformation.SessionId;
					processInfo.poolPagedBytes = (long)(ulong)systemProcessInformation.QuotaPagedPoolUsage;
					processInfo.poolNonpagedBytes = (long)(ulong)systemProcessInformation.QuotaNonPagedPoolUsage;
					processInfo.virtualBytes = (long)(ulong)systemProcessInformation.VirtualSize;
					processInfo.virtualBytesPeak = (long)(ulong)systemProcessInformation.PeakVirtualSize;
					processInfo.workingSetPeak = (long)(ulong)systemProcessInformation.PeakWorkingSetSize;
					processInfo.workingSet = (long)(ulong)systemProcessInformation.WorkingSetSize;
					processInfo.pageFileBytesPeak = (long)(ulong)systemProcessInformation.PeakPagefileUsage;
					processInfo.pageFileBytes = (long)(ulong)systemProcessInformation.PagefileUsage;
					processInfo.privateBytes = (long)(ulong)systemProcessInformation.PrivatePageCount;
					processInfo.basePriority = systemProcessInformation.BasePriority;
					if (systemProcessInformation.NamePtr == IntPtr.Zero)
					{
						if (processInfo.processId == NtProcessManager.SystemProcessID)
						{
							processInfo.processName = "System";
						}
						else if (processInfo.processId == 0)
						{
							processInfo.processName = "Idle";
						}
						else
						{
							processInfo.processName = processInfo.processId.ToString(CultureInfo.InvariantCulture);
						}
					}
					else
					{
						string text = NtProcessInfoHelper.GetProcessShortName(Marshal.PtrToStringUni(systemProcessInformation.NamePtr, (int)(systemProcessInformation.NameLength / 2)));
						if (ProcessManager.IsOSOlderThanXP && text.Length == 15)
						{
							if (text.EndsWith(".", StringComparison.OrdinalIgnoreCase))
							{
								text = text.Substring(0, 14);
							}
							else if (text.EndsWith(".e", StringComparison.OrdinalIgnoreCase))
							{
								text = text.Substring(0, 13);
							}
							else if (text.EndsWith(".ex", StringComparison.OrdinalIgnoreCase))
							{
								text = text.Substring(0, 12);
							}
						}
						processInfo.processName = text;
					}
					hashtable[processInfo.processId] = processInfo;
					intPtr = (IntPtr)((long)intPtr + (long)Marshal.SizeOf(systemProcessInformation));
					int num3 = 0;
					while ((long)num3 < (long)((ulong)systemProcessInformation.NumberOfThreads))
					{
						NtProcessInfoHelper.SystemThreadInformation systemThreadInformation = new NtProcessInfoHelper.SystemThreadInformation();
						Marshal.PtrToStructure(intPtr, systemThreadInformation);
						ThreadInfo threadInfo = new ThreadInfo();
						threadInfo.processId = (int)systemThreadInformation.UniqueProcess;
						threadInfo.threadId = (int)systemThreadInformation.UniqueThread;
						threadInfo.basePriority = systemThreadInformation.BasePriority;
						threadInfo.currentPriority = systemThreadInformation.Priority;
						threadInfo.startAddress = systemThreadInformation.StartAddress;
						threadInfo.threadState = (ThreadState)systemThreadInformation.ThreadState;
						threadInfo.threadWaitReason = NtProcessManager.GetThreadWaitReason((int)systemThreadInformation.WaitReason);
						processInfo.threadInfoList.Add(threadInfo);
						intPtr = (IntPtr)((long)intPtr + (long)Marshal.SizeOf(systemThreadInformation));
						num3++;
					}
				}
				if (systemProcessInformation.NextEntryOffset == 0U)
				{
					break;
				}
				num += (long)((ulong)systemProcessInformation.NextEntryOffset);
			}
			ProcessInfo[] array = new ProcessInfo[hashtable.Values.Count];
			hashtable.Values.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000DB060 File Offset: 0x000D9260
		internal static string GetProcessShortName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return string.Empty;
			}
			int num = -1;
			int num2 = -1;
			for (int i = 0; i < name.Length; i++)
			{
				if (name[i] == '\\')
				{
					num = i;
				}
				else if (name[i] == '.')
				{
					num2 = i;
				}
			}
			if (num2 == -1)
			{
				num2 = name.Length - 1;
			}
			else
			{
				string text = name.Substring(num2);
				if (string.Equals(".exe", text, StringComparison.OrdinalIgnoreCase))
				{
					num2--;
				}
				else
				{
					num2 = name.Length - 1;
				}
			}
			if (num == -1)
			{
				num = 0;
			}
			else
			{
				num++;
			}
			return name.Substring(num, num2 - num + 1);
		}

		// Token: 0x04002879 RID: 10361
		private const int DefaultCachedBufferSize = 131072;

		// Token: 0x0400287A RID: 10362
		private static long[] CachedBuffer;

		// Token: 0x02000881 RID: 2177
		[StructLayout(LayoutKind.Sequential)]
		internal class SystemProcessInformation
		{
			// Token: 0x04003748 RID: 14152
			internal uint NextEntryOffset;

			// Token: 0x04003749 RID: 14153
			internal uint NumberOfThreads;

			// Token: 0x0400374A RID: 14154
			private long SpareLi1;

			// Token: 0x0400374B RID: 14155
			private long SpareLi2;

			// Token: 0x0400374C RID: 14156
			private long SpareLi3;

			// Token: 0x0400374D RID: 14157
			private long CreateTime;

			// Token: 0x0400374E RID: 14158
			private long UserTime;

			// Token: 0x0400374F RID: 14159
			private long KernelTime;

			// Token: 0x04003750 RID: 14160
			internal ushort NameLength;

			// Token: 0x04003751 RID: 14161
			internal ushort MaximumNameLength;

			// Token: 0x04003752 RID: 14162
			internal IntPtr NamePtr;

			// Token: 0x04003753 RID: 14163
			internal int BasePriority;

			// Token: 0x04003754 RID: 14164
			internal IntPtr UniqueProcessId;

			// Token: 0x04003755 RID: 14165
			internal IntPtr InheritedFromUniqueProcessId;

			// Token: 0x04003756 RID: 14166
			internal uint HandleCount;

			// Token: 0x04003757 RID: 14167
			internal uint SessionId;

			// Token: 0x04003758 RID: 14168
			internal UIntPtr PageDirectoryBase;

			// Token: 0x04003759 RID: 14169
			internal UIntPtr PeakVirtualSize;

			// Token: 0x0400375A RID: 14170
			internal UIntPtr VirtualSize;

			// Token: 0x0400375B RID: 14171
			internal uint PageFaultCount;

			// Token: 0x0400375C RID: 14172
			internal UIntPtr PeakWorkingSetSize;

			// Token: 0x0400375D RID: 14173
			internal UIntPtr WorkingSetSize;

			// Token: 0x0400375E RID: 14174
			internal UIntPtr QuotaPeakPagedPoolUsage;

			// Token: 0x0400375F RID: 14175
			internal UIntPtr QuotaPagedPoolUsage;

			// Token: 0x04003760 RID: 14176
			internal UIntPtr QuotaPeakNonPagedPoolUsage;

			// Token: 0x04003761 RID: 14177
			internal UIntPtr QuotaNonPagedPoolUsage;

			// Token: 0x04003762 RID: 14178
			internal UIntPtr PagefileUsage;

			// Token: 0x04003763 RID: 14179
			internal UIntPtr PeakPagefileUsage;

			// Token: 0x04003764 RID: 14180
			internal UIntPtr PrivatePageCount;

			// Token: 0x04003765 RID: 14181
			private long ReadOperationCount;

			// Token: 0x04003766 RID: 14182
			private long WriteOperationCount;

			// Token: 0x04003767 RID: 14183
			private long OtherOperationCount;

			// Token: 0x04003768 RID: 14184
			private long ReadTransferCount;

			// Token: 0x04003769 RID: 14185
			private long WriteTransferCount;

			// Token: 0x0400376A RID: 14186
			private long OtherTransferCount;
		}

		// Token: 0x02000882 RID: 2178
		[StructLayout(LayoutKind.Sequential)]
		internal class SystemThreadInformation
		{
			// Token: 0x0400376B RID: 14187
			private long KernelTime;

			// Token: 0x0400376C RID: 14188
			private long UserTime;

			// Token: 0x0400376D RID: 14189
			private long CreateTime;

			// Token: 0x0400376E RID: 14190
			private uint WaitTime;

			// Token: 0x0400376F RID: 14191
			internal IntPtr StartAddress;

			// Token: 0x04003770 RID: 14192
			internal IntPtr UniqueProcess;

			// Token: 0x04003771 RID: 14193
			internal IntPtr UniqueThread;

			// Token: 0x04003772 RID: 14194
			internal int Priority;

			// Token: 0x04003773 RID: 14195
			internal int BasePriority;

			// Token: 0x04003774 RID: 14196
			internal uint ContextSwitches;

			// Token: 0x04003775 RID: 14197
			internal uint ThreadState;

			// Token: 0x04003776 RID: 14198
			internal uint WaitReason;
		}
	}
}
