using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies that a class is a contract for a type.</summary>
	// Token: 0x0200040A RID: 1034
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class ContractClassForAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractClassForAttribute" /> class, specifying the type the current class is a contract for.</summary>
		/// <param name="typeContractsAreFor">The type the current class is a contract for.</param>
		// Token: 0x06003408 RID: 13320 RVA: 0x000C7EFE File Offset: 0x000C60FE
		[__DynamicallyInvokable]
		public ContractClassForAttribute(Type typeContractsAreFor)
		{
			this._typeIAmAContractFor = typeContractsAreFor;
		}

		/// <summary>Gets the type that this code contract applies to.</summary>
		/// <returns>The type that this contract applies to.</returns>
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06003409 RID: 13321 RVA: 0x000C7F0D File Offset: 0x000C610D
		[__DynamicallyInvokable]
		public Type TypeContractsAreFor
		{
			[__DynamicallyInvokable]
			get
			{
				return this._typeIAmAContractFor;
			}
		}

		// Token: 0x04001710 RID: 5904
		private Type _typeIAmAContractFor;
	}
}
