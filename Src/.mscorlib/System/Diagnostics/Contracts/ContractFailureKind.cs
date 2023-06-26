using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies the type of contract that failed.</summary>
	// Token: 0x02000414 RID: 1044
	[__DynamicallyInvokable]
	public enum ContractFailureKind
	{
		/// <summary>A <see cref="Overload:System.Diagnostics.Contracts.Contract.Requires" /> contract failed.</summary>
		// Token: 0x04001719 RID: 5913
		[__DynamicallyInvokable]
		Precondition,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Ensures" /> contract failed.</summary>
		// Token: 0x0400171A RID: 5914
		[__DynamicallyInvokable]
		Postcondition,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.EnsuresOnThrow" /> contract failed.</summary>
		// Token: 0x0400171B RID: 5915
		[__DynamicallyInvokable]
		PostconditionOnException,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Invariant" /> contract failed.</summary>
		// Token: 0x0400171C RID: 5916
		[__DynamicallyInvokable]
		Invariant,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Assert" /> contract failed.</summary>
		// Token: 0x0400171D RID: 5917
		[__DynamicallyInvokable]
		Assert,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Assume" /> contract failed.</summary>
		// Token: 0x0400171E RID: 5918
		[__DynamicallyInvokable]
		Assume
	}
}
