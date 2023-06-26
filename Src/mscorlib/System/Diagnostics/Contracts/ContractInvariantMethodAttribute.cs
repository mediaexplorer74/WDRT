using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Marks a method as being the invariant method for a class.</summary>
	// Token: 0x0200040B RID: 1035
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class ContractInvariantMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractInvariantMethodAttribute" /> class.</summary>
		// Token: 0x0600340A RID: 13322 RVA: 0x000C7F15 File Offset: 0x000C6115
		[__DynamicallyInvokable]
		public ContractInvariantMethodAttribute()
		{
		}
	}
}
