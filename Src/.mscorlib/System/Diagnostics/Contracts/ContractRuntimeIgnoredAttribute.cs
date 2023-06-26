using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Identifies a member that has no run-time behavior.</summary>
	// Token: 0x0200040D RID: 1037
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	[__DynamicallyInvokable]
	public sealed class ContractRuntimeIgnoredAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractRuntimeIgnoredAttribute" /> class.</summary>
		// Token: 0x0600340C RID: 13324 RVA: 0x000C7F25 File Offset: 0x000C6125
		[__DynamicallyInvokable]
		public ContractRuntimeIgnoredAttribute()
		{
		}
	}
}
