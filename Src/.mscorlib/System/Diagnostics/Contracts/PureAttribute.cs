using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Indicates that a type or method is pure, that is, it does not make any visible state changes.</summary>
	// Token: 0x02000408 RID: 1032
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = false, Inherited = true)]
	[__DynamicallyInvokable]
	public sealed class PureAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.PureAttribute" /> class.</summary>
		// Token: 0x06003405 RID: 13317 RVA: 0x000C7EDF File Offset: 0x000C60DF
		[__DynamicallyInvokable]
		public PureAttribute()
		{
		}
	}
}
