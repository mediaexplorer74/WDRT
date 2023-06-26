using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies the user-defined tag that is placed on fields of user-defined types that are passed as <see cref="T:System.Diagnostics.Tracing.EventSource" /> payloads through the <see cref="T:System.Diagnostics.Tracing.EventFieldAttribute" />.</summary>
	// Token: 0x02000440 RID: 1088
	[Flags]
	[__DynamicallyInvokable]
	public enum EventFieldTags
	{
		/// <summary>Specifies no tag and is equal to zero.</summary>
		// Token: 0x0400182B RID: 6187
		[__DynamicallyInvokable]
		None = 0
	}
}
