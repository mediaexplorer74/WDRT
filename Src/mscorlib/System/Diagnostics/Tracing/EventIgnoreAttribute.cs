using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies a property should be ignored when writing an event type with the <see cref="M:System.Diagnostics.Tracing.EventSource.Write``1(System.String,System.Diagnostics.Tracing.EventSourceOptions@,``0@)" /> method.</summary>
	// Token: 0x02000443 RID: 1091
	[AttributeUsage(AttributeTargets.Property)]
	[__DynamicallyInvokable]
	public class EventIgnoreAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventIgnoreAttribute" /> class.</summary>
		// Token: 0x06003624 RID: 13860 RVA: 0x000D3BF5 File Offset: 0x000D1DF5
		[__DynamicallyInvokable]
		public EventIgnoreAttribute()
		{
		}
	}
}
