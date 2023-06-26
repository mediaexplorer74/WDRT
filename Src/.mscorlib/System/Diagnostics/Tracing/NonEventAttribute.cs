using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Identifies a method that is not generating an event.</summary>
	// Token: 0x02000426 RID: 1062
	[AttributeUsage(AttributeTargets.Method)]
	[__DynamicallyInvokable]
	public sealed class NonEventAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.NonEventAttribute" /> class.</summary>
		// Token: 0x0600355D RID: 13661 RVA: 0x000CFF42 File Offset: 0x000CE142
		[__DynamicallyInvokable]
		public NonEventAttribute()
		{
		}
	}
}
