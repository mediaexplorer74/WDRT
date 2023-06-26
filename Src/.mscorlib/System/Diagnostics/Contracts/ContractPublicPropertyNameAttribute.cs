using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies that a field can be used in method contracts when the field has less visibility than the method.</summary>
	// Token: 0x0200040F RID: 1039
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Field)]
	[__DynamicallyInvokable]
	public sealed class ContractPublicPropertyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractPublicPropertyNameAttribute" /> class.</summary>
		/// <param name="name">The property name to apply to the field.</param>
		// Token: 0x0600340F RID: 13327 RVA: 0x000C7F44 File Offset: 0x000C6144
		[__DynamicallyInvokable]
		public ContractPublicPropertyNameAttribute(string name)
		{
			this._publicName = name;
		}

		/// <summary>Gets the property name to be applied to the field.</summary>
		/// <returns>The property name to be applied to the field.</returns>
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x000C7F53 File Offset: 0x000C6153
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this._publicName;
			}
		}

		// Token: 0x04001712 RID: 5906
		private string _publicName;
	}
}
