using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies the event log channel for the event.</summary>
	// Token: 0x02000436 RID: 1078
	[FriendAccessAllowed]
	[__DynamicallyInvokable]
	public enum EventChannel : byte
	{
		/// <summary>No channel specified.</summary>
		// Token: 0x04001805 RID: 6149
		[__DynamicallyInvokable]
		None,
		/// <summary>The administrator log channel.</summary>
		// Token: 0x04001806 RID: 6150
		[__DynamicallyInvokable]
		Admin = 16,
		/// <summary>The operational channel.</summary>
		// Token: 0x04001807 RID: 6151
		[__DynamicallyInvokable]
		Operational,
		/// <summary>The analytic channel.</summary>
		// Token: 0x04001808 RID: 6152
		[__DynamicallyInvokable]
		Analytic,
		/// <summary>The debug channel.</summary>
		// Token: 0x04001809 RID: 6153
		[__DynamicallyInvokable]
		Debug
	}
}
