using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	internal sealed class MaybeNullAttribute : Attribute
	{
	}
}
