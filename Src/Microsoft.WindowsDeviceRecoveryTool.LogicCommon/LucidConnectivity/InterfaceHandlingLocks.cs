using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.LucidConnectivity
{
	// Token: 0x0200002B RID: 43
	internal class InterfaceHandlingLocks
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x00009F6B File Offset: 0x0000816B
		public InterfaceHandlingLocks()
		{
			this.syncObject = new object();
			this.locks = new Dictionary<string, ManualResetEventSlim>();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00009F8C File Offset: 0x0000818C
		public void CreateLock(string id)
		{
			object obj = this.syncObject;
			lock (obj)
			{
				id = this.ConvertId(id);
				bool flag2 = this.locks.ContainsKey(id);
				if (flag2)
				{
					this.locks[id] = new ManualResetEventSlim(true);
				}
				else
				{
					this.locks.Add(id, new ManualResetEventSlim(true));
				}
				Tracer<InterfaceHandlingLocks>.WriteInformation("*** CREATE_LOCK: Thread {0} created interface lock for '{1}' ***", new object[]
				{
					Thread.CurrentThread.ManagedThreadId.ToString("X4"),
					id
				});
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A040 File Offset: 0x00008240
		public bool Lock(string id)
		{
			object obj = this.syncObject;
			bool flag3;
			lock (obj)
			{
				id = this.ConvertId(id);
				Tracer<InterfaceHandlingLocks>.WriteInformation("*** LOCK: Interface handling for '{0}' locked by thread {1} ***", new object[]
				{
					id,
					Thread.CurrentThread.ManagedThreadId.ToString("X4")
				});
				bool flag2 = this.locks.ContainsKey(id);
				if (flag2)
				{
					this.locks[id].Reset();
					flag3 = true;
				}
				else
				{
					flag3 = false;
				}
			}
			return flag3;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A0E4 File Offset: 0x000082E4
		public bool Unlock(string id)
		{
			object obj = this.syncObject;
			bool flag3;
			lock (obj)
			{
				id = this.ConvertId(id);
				Tracer<InterfaceHandlingLocks>.WriteInformation("*** UNLOCK: Interface handling for '{0}' unlocked by thread {1} ***", new object[]
				{
					id,
					Thread.CurrentThread.ManagedThreadId.ToString("X4")
				});
				bool flag2 = this.locks.ContainsKey(id);
				if (flag2)
				{
					this.locks[id].Set();
					flag3 = true;
				}
				else
				{
					flag3 = false;
				}
			}
			return flag3;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A188 File Offset: 0x00008388
		public void Discard(string id)
		{
			object obj = this.syncObject;
			lock (obj)
			{
				id = this.ConvertId(id);
				bool flag2 = this.locks.ContainsKey(id);
				if (flag2)
				{
					this.locks.Remove(id);
					Tracer<InterfaceHandlingLocks>.WriteInformation("*** DISCARD_LOCK: Lock '{0}' discarded by thread {1} ***", new object[]
					{
						id,
						Thread.CurrentThread.ManagedThreadId.ToString("X4")
					});
				}
				else
				{
					Tracer<InterfaceHandlingLocks>.WriteWarning("*** DISCARD_LOCK: Lock '{0}' could not be discarded by thread {1} ***", new object[]
					{
						id,
						Thread.CurrentThread.ManagedThreadId.ToString("X4")
					});
				}
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A254 File Offset: 0x00008454
		public bool Wait(string id, int timeoutMs)
		{
			id = this.ConvertId(id);
			Tracer<InterfaceHandlingLocks>.WriteInformation("*** WAIT: Thread {0} is waiting for unlocking of '{1}' ***", new object[]
			{
				Thread.CurrentThread.ManagedThreadId.ToString("X4"),
				id
			});
			bool flag = this.locks.ContainsKey(id);
			bool flag4;
			if (flag)
			{
				bool flag2 = this.locks[id].Wait(timeoutMs);
				bool flag3 = flag2;
				if (flag3)
				{
					Tracer<InterfaceHandlingLocks>.WriteInformation("*** SIGNAL: Thread {0} is allowed to continue handling interface(s) for '{1}' ***", new object[]
					{
						Thread.CurrentThread.ManagedThreadId.ToString("X4"),
						id
					});
				}
				else
				{
					Tracer<InterfaceHandlingLocks>.WriteWarning("*** NO_SIGNAL: Waiting for unlocking of '{0}' timed out ***", new object[] { id });
				}
				flag4 = flag2;
			}
			else
			{
				Tracer<InterfaceHandlingLocks>.WriteWarning("*** NO_LOCK: No interface lock found for '{0}' ***", new object[] { id });
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000A330 File Offset: 0x00008530
		private string ConvertId(string id)
		{
			return id.ToUpperInvariant();
		}

		// Token: 0x0400012C RID: 300
		private readonly object syncObject;

		// Token: 0x0400012D RID: 301
		private readonly Dictionary<string, ManualResetEventSlim> locks;
	}
}
