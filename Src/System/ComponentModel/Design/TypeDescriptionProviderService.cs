using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a type description provider for a specified type.</summary>
	// Token: 0x020005C6 RID: 1478
	public abstract class TypeDescriptionProviderService
	{
		/// <summary>Gets a type description provider for the specified object.</summary>
		/// <param name="instance">The object to get a type description provider for.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that corresponds with <paramref name="instance" />.</returns>
		// Token: 0x0600373B RID: 14139
		public abstract TypeDescriptionProvider GetProvider(object instance);

		/// <summary>Gets a type description provider for the specified type.</summary>
		/// <param name="type">The type to get a type description provider for.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that corresponds with <paramref name="type" />.</returns>
		// Token: 0x0600373C RID: 14140
		public abstract TypeDescriptionProvider GetProvider(Type type);
	}
}
