using System;

namespace System.Reflection
{
	/// <summary>Represents a type that you can reflect over.</summary>
	// Token: 0x020005E9 RID: 1513
	[__DynamicallyInvokable]
	public interface IReflectableType
	{
		/// <summary>Retrieves an object that represents this type.</summary>
		/// <returns>An object that represents this type.</returns>
		// Token: 0x06004669 RID: 18025
		[__DynamicallyInvokable]
		TypeInfo GetTypeInfo();
	}
}
