using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Identifies the level of an event.</summary>
	// Token: 0x02000433 RID: 1075
	[__DynamicallyInvokable]
	public enum EventLevel
	{
		/// <summary>No level filtering is done on the event.</summary>
		// Token: 0x040017F0 RID: 6128
		[__DynamicallyInvokable]
		LogAlways,
		/// <summary>This level corresponds to a critical error, which is a serious error that has caused a major failure.</summary>
		// Token: 0x040017F1 RID: 6129
		[__DynamicallyInvokable]
		Critical,
		/// <summary>This level adds standard errors that signify a problem.</summary>
		// Token: 0x040017F2 RID: 6130
		[__DynamicallyInvokable]
		Error,
		/// <summary>This level adds warning events (for example, events that are published because a disk is nearing full capacity).</summary>
		// Token: 0x040017F3 RID: 6131
		[__DynamicallyInvokable]
		Warning,
		/// <summary>This level adds informational events or messages that are not errors. These events can help trace the progress or state of an application.</summary>
		// Token: 0x040017F4 RID: 6132
		[__DynamicallyInvokable]
		Informational,
		/// <summary>This level adds lengthy events or messages. It causes all events to be logged.</summary>
		// Token: 0x040017F5 RID: 6133
		[__DynamicallyInvokable]
		Verbose
	}
}
