using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that a method is an extension method, or that a class or assembly contains extension methods.</summary>
	// Token: 0x020008B4 RID: 2228
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
	[__DynamicallyInvokable]
	public sealed class ExtensionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.ExtensionAttribute" /> class.</summary>
		// Token: 0x06005DC0 RID: 24000 RVA: 0x0014AF12 File Offset: 0x00149112
		[__DynamicallyInvokable]
		public ExtensionAttribute()
		{
		}
	}
}
