using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies that a type contains an unmanaged array that might potentially overflow. This class cannot be inherited.</summary>
	// Token: 0x020008BF RID: 2239
	[AttributeUsage(AttributeTargets.Struct)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class UnsafeValueTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.UnsafeValueTypeAttribute" /> class.</summary>
		// Token: 0x06005DD1 RID: 24017 RVA: 0x0014AFE0 File Offset: 0x001491E0
		[__DynamicallyInvokable]
		public UnsafeValueTypeAttribute()
		{
		}
	}
}
