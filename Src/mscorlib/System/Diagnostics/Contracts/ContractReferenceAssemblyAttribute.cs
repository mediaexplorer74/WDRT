using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies that an assembly is a reference assembly that contains contracts.</summary>
	// Token: 0x0200040C RID: 1036
	[AttributeUsage(AttributeTargets.Assembly)]
	[__DynamicallyInvokable]
	public sealed class ContractReferenceAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractReferenceAssemblyAttribute" /> class.</summary>
		// Token: 0x0600340B RID: 13323 RVA: 0x000C7F1D File Offset: 0x000C611D
		[__DynamicallyInvokable]
		public ContractReferenceAssemblyAttribute()
		{
		}
	}
}
