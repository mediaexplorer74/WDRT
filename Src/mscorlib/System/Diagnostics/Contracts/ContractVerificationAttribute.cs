using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Instructs analysis tools to assume the correctness of an assembly, type, or member without performing static verification.</summary>
	// Token: 0x0200040E RID: 1038
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	[__DynamicallyInvokable]
	public sealed class ContractVerificationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractVerificationAttribute" /> class.</summary>
		/// <param name="value">
		///   <see langword="true" /> to require verification; otherwise, <see langword="false" />.</param>
		// Token: 0x0600340D RID: 13325 RVA: 0x000C7F2D File Offset: 0x000C612D
		[__DynamicallyInvokable]
		public ContractVerificationAttribute(bool value)
		{
			this._value = value;
		}

		/// <summary>Gets the value that indicates whether to verify the contract of the target.</summary>
		/// <returns>
		///   <see langword="true" /> if verification is required; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x0600340E RID: 13326 RVA: 0x000C7F3C File Offset: 0x000C613C
		[__DynamicallyInvokable]
		public bool Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._value;
			}
		}

		// Token: 0x04001711 RID: 5905
		private bool _value;
	}
}
