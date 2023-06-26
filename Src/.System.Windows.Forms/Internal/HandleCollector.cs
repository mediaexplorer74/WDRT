using System;
using System.Threading;

namespace System.Internal
{
	// Token: 0x020000F9 RID: 249
	internal sealed class HandleCollector
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060003E5 RID: 997 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		// (remove) Token: 0x060003E6 RID: 998 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		internal static event HandleChangeEventHandler HandleAdded;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060003E7 RID: 999 RVA: 0x0000C524 File Offset: 0x0000A724
		// (remove) Token: 0x060003E8 RID: 1000 RVA: 0x0000C558 File Offset: 0x0000A758
		internal static event HandleChangeEventHandler HandleRemoved;

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000C58B File Offset: 0x0000A78B
		internal static IntPtr Add(IntPtr handle, int type)
		{
			HandleCollector.handleTypes[type - 1].Add(handle);
			return handle;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		internal static void SuspendCollect()
		{
			object obj = HandleCollector.internalSyncObject;
			lock (obj)
			{
				HandleCollector.suspendCount++;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000C5E8 File Offset: 0x0000A7E8
		internal static void ResumeCollect()
		{
			bool flag = false;
			object obj = HandleCollector.internalSyncObject;
			lock (obj)
			{
				if (HandleCollector.suspendCount > 0)
				{
					HandleCollector.suspendCount--;
				}
				if (HandleCollector.suspendCount == 0)
				{
					for (int i = 0; i < HandleCollector.handleTypeCount; i++)
					{
						HandleCollector.HandleType handleType = HandleCollector.handleTypes[i];
						lock (handleType)
						{
							if (HandleCollector.handleTypes[i].NeedCollection())
							{
								flag = true;
							}
						}
					}
				}
			}
			if (flag)
			{
				GC.Collect();
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000C698 File Offset: 0x0000A898
		internal static int RegisterType(string typeName, int expense, int initialThreshold)
		{
			object obj = HandleCollector.internalSyncObject;
			int num;
			lock (obj)
			{
				if (HandleCollector.handleTypeCount == 0 || HandleCollector.handleTypeCount == HandleCollector.handleTypes.Length)
				{
					HandleCollector.HandleType[] array = new HandleCollector.HandleType[HandleCollector.handleTypeCount + 10];
					if (HandleCollector.handleTypes != null)
					{
						Array.Copy(HandleCollector.handleTypes, 0, array, 0, HandleCollector.handleTypeCount);
					}
					HandleCollector.handleTypes = array;
				}
				HandleCollector.handleTypes[HandleCollector.handleTypeCount++] = new HandleCollector.HandleType(typeName, expense, initialThreshold);
				num = HandleCollector.handleTypeCount;
			}
			return num;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000C738 File Offset: 0x0000A938
		internal static IntPtr Remove(IntPtr handle, int type)
		{
			return HandleCollector.handleTypes[type - 1].Remove(handle);
		}

		// Token: 0x04000428 RID: 1064
		private static HandleCollector.HandleType[] handleTypes;

		// Token: 0x04000429 RID: 1065
		private static int handleTypeCount;

		// Token: 0x0400042A RID: 1066
		private static int suspendCount;

		// Token: 0x0400042D RID: 1069
		private static object internalSyncObject = new object();

		// Token: 0x02000545 RID: 1349
		private class HandleType
		{
			// Token: 0x06005560 RID: 21856 RVA: 0x00166034 File Offset: 0x00164234
			internal HandleType(string name, int expense, int initialThreshHold)
			{
				this.name = name;
				this.initialThreshHold = initialThreshHold;
				this.threshHold = initialThreshHold;
				this.deltaPercent = 100 - expense;
			}

			// Token: 0x06005561 RID: 21857 RVA: 0x0016605C File Offset: 0x0016425C
			internal void Add(IntPtr handle)
			{
				if (handle == IntPtr.Zero)
				{
					return;
				}
				bool flag = false;
				int num = 0;
				lock (this)
				{
					this.handleCount++;
					flag = this.NeedCollection();
					num = this.handleCount;
				}
				object internalSyncObject = HandleCollector.internalSyncObject;
				lock (internalSyncObject)
				{
					if (HandleCollector.HandleAdded != null)
					{
						HandleCollector.HandleAdded(this.name, handle, num);
					}
				}
				if (!flag)
				{
					return;
				}
				if (flag)
				{
					GC.Collect();
					int num2 = (100 - this.deltaPercent) / 4;
					Thread.Sleep(num2);
				}
			}

			// Token: 0x06005562 RID: 21858 RVA: 0x00166128 File Offset: 0x00164328
			internal int GetHandleCount()
			{
				int num;
				lock (this)
				{
					num = this.handleCount;
				}
				return num;
			}

			// Token: 0x06005563 RID: 21859 RVA: 0x00166168 File Offset: 0x00164368
			internal bool NeedCollection()
			{
				if (HandleCollector.suspendCount > 0)
				{
					return false;
				}
				if (this.handleCount > this.threshHold)
				{
					this.threshHold = this.handleCount + this.handleCount * this.deltaPercent / 100;
					return true;
				}
				int num = 100 * this.threshHold / (100 + this.deltaPercent);
				if (num >= this.initialThreshHold && this.handleCount < (int)((float)num * 0.9f))
				{
					this.threshHold = num;
				}
				return false;
			}

			// Token: 0x06005564 RID: 21860 RVA: 0x001661E4 File Offset: 0x001643E4
			internal IntPtr Remove(IntPtr handle)
			{
				if (handle == IntPtr.Zero)
				{
					return handle;
				}
				int num = 0;
				lock (this)
				{
					this.handleCount--;
					if (this.handleCount < 0)
					{
						this.handleCount = 0;
					}
					num = this.handleCount;
				}
				object internalSyncObject = HandleCollector.internalSyncObject;
				lock (internalSyncObject)
				{
					if (HandleCollector.HandleRemoved != null)
					{
						HandleCollector.HandleRemoved(this.name, handle, num);
					}
				}
				return handle;
			}

			// Token: 0x040037FF RID: 14335
			internal readonly string name;

			// Token: 0x04003800 RID: 14336
			private int initialThreshHold;

			// Token: 0x04003801 RID: 14337
			private int threshHold;

			// Token: 0x04003802 RID: 14338
			private int handleCount;

			// Token: 0x04003803 RID: 14339
			private readonly int deltaPercent;
		}
	}
}
