using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the reason a thread is waiting.</summary>
	// Token: 0x02000509 RID: 1289
	public enum ThreadWaitReason
	{
		/// <summary>The thread is waiting for the scheduler.</summary>
		// Token: 0x040028DF RID: 10463
		Executive,
		/// <summary>The thread is waiting for a free virtual memory page.</summary>
		// Token: 0x040028E0 RID: 10464
		FreePage,
		/// <summary>The thread is waiting for a virtual memory page to arrive in memory.</summary>
		// Token: 0x040028E1 RID: 10465
		PageIn,
		/// <summary>The thread is waiting for system allocation.</summary>
		// Token: 0x040028E2 RID: 10466
		SystemAllocation,
		/// <summary>Thread execution is delayed.</summary>
		// Token: 0x040028E3 RID: 10467
		ExecutionDelay,
		/// <summary>Thread execution is suspended.</summary>
		// Token: 0x040028E4 RID: 10468
		Suspended,
		/// <summary>The thread is waiting for a user request.</summary>
		// Token: 0x040028E5 RID: 10469
		UserRequest,
		/// <summary>The thread is waiting for event pair high.</summary>
		// Token: 0x040028E6 RID: 10470
		EventPairHigh,
		/// <summary>The thread is waiting for event pair low.</summary>
		// Token: 0x040028E7 RID: 10471
		EventPairLow,
		/// <summary>The thread is waiting for a local procedure call to arrive.</summary>
		// Token: 0x040028E8 RID: 10472
		LpcReceive,
		/// <summary>The thread is waiting for reply to a local procedure call to arrive.</summary>
		// Token: 0x040028E9 RID: 10473
		LpcReply,
		/// <summary>The thread is waiting for the system to allocate virtual memory.</summary>
		// Token: 0x040028EA RID: 10474
		VirtualMemory,
		/// <summary>The thread is waiting for a virtual memory page to be written to disk.</summary>
		// Token: 0x040028EB RID: 10475
		PageOut,
		/// <summary>The thread is waiting for an unknown reason.</summary>
		// Token: 0x040028EC RID: 10476
		Unknown
	}
}
