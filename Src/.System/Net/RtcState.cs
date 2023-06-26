using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200010A RID: 266
	[FriendAccessAllowed]
	internal class RtcState
	{
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0003BBFE File Offset: 0x00039DFE
		internal bool IsAborted
		{
			get
			{
				return this.isAborted != 0;
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0003BC09 File Offset: 0x00039E09
		internal RtcState()
		{
			this.connectComplete = new ManualResetEvent(false);
			this.flushComplete = new ManualResetEvent(false);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0003BC29 File Offset: 0x00039E29
		internal void Abort()
		{
			Interlocked.Exchange(ref this.isAborted, 1);
			this.connectComplete.Set();
			this.flushComplete.Set();
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0003BC50 File Offset: 0x00039E50
		internal bool IsEnabled()
		{
			RtcState.ControlChannelTriggerStatus controlChannelTriggerStatus = (RtcState.ControlChannelTriggerStatus)BitConverter.ToInt32(this.outputData, 0);
			return this.result == 0 && (controlChannelTriggerStatus == RtcState.ControlChannelTriggerStatus.SoftwareSlotAllocated || controlChannelTriggerStatus == RtcState.ControlChannelTriggerStatus.HardwareSlotAllocated);
		}

		// Token: 0x04000F2F RID: 3887
		internal byte[] inputData;

		// Token: 0x04000F30 RID: 3888
		internal byte[] outputData;

		// Token: 0x04000F31 RID: 3889
		internal ManualResetEvent connectComplete;

		// Token: 0x04000F32 RID: 3890
		internal ManualResetEvent flushComplete;

		// Token: 0x04000F33 RID: 3891
		internal int result;

		// Token: 0x04000F34 RID: 3892
		private int isAborted;

		// Token: 0x02000708 RID: 1800
		[FriendAccessAllowed]
		internal enum ControlChannelTriggerStatus
		{
			// Token: 0x040030D3 RID: 12499
			Invalid,
			// Token: 0x040030D4 RID: 12500
			SoftwareSlotAllocated,
			// Token: 0x040030D5 RID: 12501
			HardwareSlotAllocated,
			// Token: 0x040030D6 RID: 12502
			PolicyError,
			// Token: 0x040030D7 RID: 12503
			SystemError,
			// Token: 0x040030D8 RID: 12504
			TransportDisconnected,
			// Token: 0x040030D9 RID: 12505
			ServiceUnavailable
		}
	}
}
