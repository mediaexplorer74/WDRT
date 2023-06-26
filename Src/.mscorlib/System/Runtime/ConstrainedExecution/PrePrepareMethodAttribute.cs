using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Instructs the native image generation service to prepare a method for inclusion in a constrained execution region (CER).</summary>
	// Token: 0x0200072B RID: 1835
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	public sealed class PrePrepareMethodAttribute : Attribute
	{
	}
}
