using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Distinguishes a compiler-generated element from a user-generated element. This class cannot be inherited.</summary>
	// Token: 0x020008AB RID: 2219
	[AttributeUsage(AttributeTargets.All, Inherited = true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class CompilerGeneratedAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.CompilerGeneratedAttribute" /> class.</summary>
		// Token: 0x06005DAF RID: 23983 RVA: 0x0014AB8D File Offset: 0x00148D8D
		[__DynamicallyInvokable]
		public CompilerGeneratedAttribute()
		{
		}
	}
}
