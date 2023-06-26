using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Allows you to obtain the method or property name of the caller to the method.</summary>
	// Token: 0x020008E7 RID: 2279
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class CallerMemberNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.CallerMemberNameAttribute" /> class.</summary>
		// Token: 0x06005E1A RID: 24090 RVA: 0x0014BD1D File Offset: 0x00149F1D
		[__DynamicallyInvokable]
		public CallerMemberNameAttribute()
		{
		}
	}
}
