using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	/// <summary>Defines the standard operation codes that the event source attaches to events.</summary>
	// Token: 0x02000435 RID: 1077
	[FriendAccessAllowed]
	[__DynamicallyInvokable]
	public enum EventOpcode
	{
		/// <summary>An informational event.</summary>
		// Token: 0x040017F9 RID: 6137
		[__DynamicallyInvokable]
		Info,
		/// <summary>An event that is published when an application starts a new transaction or activity. This operation code can be embedded within another transaction or activity when multiple events that have the <see cref="F:System.Diagnostics.Tracing.EventOpcode.Start" /> code follow each other without an intervening event that has a <see cref="F:System.Diagnostics.Tracing.EventOpcode.Stop" /> code.</summary>
		// Token: 0x040017FA RID: 6138
		[__DynamicallyInvokable]
		Start,
		/// <summary>An event that is published when an activity or a transaction in an application ends. The event corresponds to the last unpaired event that has a <see cref="F:System.Diagnostics.Tracing.EventOpcode.Start" /> operation code.</summary>
		// Token: 0x040017FB RID: 6139
		[__DynamicallyInvokable]
		Stop,
		/// <summary>A trace collection start event.</summary>
		// Token: 0x040017FC RID: 6140
		[__DynamicallyInvokable]
		DataCollectionStart,
		/// <summary>A trace collection stop event.</summary>
		// Token: 0x040017FD RID: 6141
		[__DynamicallyInvokable]
		DataCollectionStop,
		/// <summary>An extension event.</summary>
		// Token: 0x040017FE RID: 6142
		[__DynamicallyInvokable]
		Extension,
		/// <summary>An event that is published after an activity in an application replies to an event.</summary>
		// Token: 0x040017FF RID: 6143
		[__DynamicallyInvokable]
		Reply,
		/// <summary>An event that is published after an activity in an application resumes from a suspended state. The event should follow an event that has the <see cref="F:System.Diagnostics.Tracing.EventOpcode.Suspend" /> operation code.</summary>
		// Token: 0x04001800 RID: 6144
		[__DynamicallyInvokable]
		Resume,
		/// <summary>An event that is published when an activity in an application is suspended.</summary>
		// Token: 0x04001801 RID: 6145
		[__DynamicallyInvokable]
		Suspend,
		/// <summary>An event that is published when one activity in an application transfers data or system resources to another activity.</summary>
		// Token: 0x04001802 RID: 6146
		[__DynamicallyInvokable]
		Send,
		/// <summary>An event that is published when one activity in an application receives data.</summary>
		// Token: 0x04001803 RID: 6147
		[__DynamicallyInvokable]
		Receive = 240
	}
}
