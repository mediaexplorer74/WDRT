using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a basic, component site-specific, key-value pair dictionary through a service that a designer can use to store user-defined data.</summary>
	// Token: 0x020005EB RID: 1515
	public interface IDictionaryService
	{
		/// <summary>Gets the key corresponding to the specified value.</summary>
		/// <param name="value">The value to look up in the dictionary.</param>
		/// <returns>The associated key, or <see langword="null" /> if no key exists.</returns>
		// Token: 0x0600380D RID: 14349
		object GetKey(object value);

		/// <summary>Gets the value corresponding to the specified key.</summary>
		/// <param name="key">The key to look up the value for.</param>
		/// <returns>The associated value, or <see langword="null" /> if no value exists.</returns>
		// Token: 0x0600380E RID: 14350
		object GetValue(object key);

		/// <summary>Sets the specified key-value pair.</summary>
		/// <param name="key">An object to use as the key to associate the value with.</param>
		/// <param name="value">The value to store.</param>
		// Token: 0x0600380F RID: 14351
		void SetValue(object key, object value);
	}
}
