using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Describes the command (<see cref="P:System.Diagnostics.Tracing.EventCommandEventArgs.Command" /> property) that is passed to the <see cref="M:System.Diagnostics.Tracing.EventSource.OnEventCommand(System.Diagnostics.Tracing.EventCommandEventArgs)" /> callback.</summary>
	// Token: 0x02000429 RID: 1065
	[__DynamicallyInvokable]
	public enum EventCommand
	{
		/// <summary>Update the event.</summary>
		// Token: 0x040017B4 RID: 6068
		[__DynamicallyInvokable]
		Update,
		/// <summary>Send the manifest.</summary>
		// Token: 0x040017B5 RID: 6069
		[__DynamicallyInvokable]
		SendManifest = -1,
		/// <summary>Enable the event.</summary>
		// Token: 0x040017B6 RID: 6070
		[__DynamicallyInvokable]
		Enable = -2,
		/// <summary>Disable the event.</summary>
		// Token: 0x040017B7 RID: 6071
		[__DynamicallyInvokable]
		Disable = -3
	}
}
