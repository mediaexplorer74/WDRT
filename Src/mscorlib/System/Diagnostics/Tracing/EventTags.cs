using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies the tracking of activity start and stop events. You should only use the lower 24 bits. For more information, see <see cref="T:System.Diagnostics.Tracing.EventSourceOptions" /> and <see cref="M:System.Diagnostics.Tracing.EventSource.Write(System.String,System.Diagnostics.Tracing.EventSourceOptions)" />.</summary>
	// Token: 0x02000481 RID: 1153
	[Flags]
	[__DynamicallyInvokable]
	public enum EventTags
	{
		/// <summary>Specifies no tag and is equal to zero.</summary>
		// Token: 0x0400189D RID: 6301
		[__DynamicallyInvokable]
		None = 0
	}
}
