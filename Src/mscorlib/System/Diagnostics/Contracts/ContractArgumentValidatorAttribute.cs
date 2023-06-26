using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Enables the factoring of legacy <see langword="if-then-throw" /> code into separate methods for reuse, and provides full control over thrown exceptions and arguments.</summary>
	// Token: 0x02000410 RID: 1040
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[Conditional("CONTRACTS_FULL")]
	[__DynamicallyInvokable]
	public sealed class ContractArgumentValidatorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractArgumentValidatorAttribute" /> class.</summary>
		// Token: 0x06003411 RID: 13329 RVA: 0x000C7F5B File Offset: 0x000C615B
		[__DynamicallyInvokable]
		public ContractArgumentValidatorAttribute()
		{
		}
	}
}
