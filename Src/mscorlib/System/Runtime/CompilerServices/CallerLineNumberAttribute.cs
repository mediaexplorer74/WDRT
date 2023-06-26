using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Allows you to obtain the line number in the source file at which the method is called.</summary>
	// Token: 0x020008E6 RID: 2278
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class CallerLineNumberAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.CallerLineNumberAttribute" /> class.</summary>
		// Token: 0x06005E19 RID: 24089 RVA: 0x0014BD15 File Offset: 0x00149F15
		[__DynamicallyInvokable]
		public CallerLineNumberAttribute()
		{
		}
	}
}
