using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies that a separate type contains the code contracts for this type.</summary>
	// Token: 0x02000409 RID: 1033
	[Conditional("CONTRACTS_FULL")]
	[Conditional("DEBUG")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class ContractClassAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractClassAttribute" /> class.</summary>
		/// <param name="typeContainingContracts">The type that contains the code contracts for this type.</param>
		// Token: 0x06003406 RID: 13318 RVA: 0x000C7EE7 File Offset: 0x000C60E7
		[__DynamicallyInvokable]
		public ContractClassAttribute(Type typeContainingContracts)
		{
			this._typeWithContracts = typeContainingContracts;
		}

		/// <summary>Gets the type that contains the code contracts for this type.</summary>
		/// <returns>The type that contains the code contracts for this type.</returns>
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06003407 RID: 13319 RVA: 0x000C7EF6 File Offset: 0x000C60F6
		[__DynamicallyInvokable]
		public Type TypeContainingContracts
		{
			[__DynamicallyInvokable]
			get
			{
				return this._typeWithContracts;
			}
		}

		// Token: 0x0400170F RID: 5903
		private Type _typeWithContracts;
	}
}
