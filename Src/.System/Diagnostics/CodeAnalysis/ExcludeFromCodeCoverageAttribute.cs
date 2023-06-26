using System;

namespace System.Diagnostics.CodeAnalysis
{
	/// <summary>Specifies that the attributed code should be excluded from code coverage information.</summary>
	// Token: 0x0200050A RID: 1290
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event, Inherited = false, AllowMultiple = false)]
	public sealed class ExcludeFromCodeCoverageAttribute : Attribute
	{
	}
}
