using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200053A RID: 1338
	[FriendAccessAllowed]
	[EventSource(Name = "System.Threading.SynchronizationEventSource", Guid = "EC631D38-466B-4290-9306-834971BA0217", LocalizationResources = "mscorlib")]
	internal sealed class CdsSyncEtwBCLProvider : EventSource
	{
		// Token: 0x06003EF3 RID: 16115 RVA: 0x000EB5A5 File Offset: 0x000E97A5
		private CdsSyncEtwBCLProvider()
		{
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x000EB5AD File Offset: 0x000E97AD
		[Event(1, Level = EventLevel.Warning)]
		public void SpinLock_FastPathFailed(int ownerID)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(1, ownerID);
			}
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x000EB5C2 File Offset: 0x000E97C2
		[Event(2, Level = EventLevel.Informational)]
		public void SpinWait_NextSpinWillYield()
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				base.WriteEvent(2);
			}
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x000EB5D8 File Offset: 0x000E97D8
		[SecuritySafeCritical]
		[Event(3, Level = EventLevel.Verbose, Version = 1)]
		public unsafe void Barrier_PhaseFinished(bool currentSense, long phaseNum)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				EventSource.EventData* ptr;
				int num;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
					num = (currentSense ? 1 : 0);
					ptr->Size = 4;
				}
				ptr->DataPointer = (IntPtr)((void*)(&num));
				ptr[1].Size = 8;
				ptr[1].DataPointer = (IntPtr)((void*)(&phaseNum));
				base.WriteEventCore(3, 2, ptr);
			}
		}

		// Token: 0x04001A75 RID: 6773
		public static CdsSyncEtwBCLProvider Log = new CdsSyncEtwBCLProvider();

		// Token: 0x04001A76 RID: 6774
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04001A77 RID: 6775
		private const int SPINLOCK_FASTPATHFAILED_ID = 1;

		// Token: 0x04001A78 RID: 6776
		private const int SPINWAIT_NEXTSPINWILLYIELD_ID = 2;

		// Token: 0x04001A79 RID: 6777
		private const int BARRIER_PHASEFINISHED_ID = 3;
	}
}
