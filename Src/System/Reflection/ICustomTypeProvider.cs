using System;

namespace System.Reflection
{
	/// <summary>Represents an object that provides a custom type.</summary>
	// Token: 0x020003F8 RID: 1016
	public interface ICustomTypeProvider
	{
		/// <summary>Gets the custom type provided by this object.</summary>
		/// <returns>The custom type.</returns>
		// Token: 0x06002643 RID: 9795
		Type GetCustomType();
	}
}
