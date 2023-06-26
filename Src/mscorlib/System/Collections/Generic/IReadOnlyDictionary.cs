using System;

namespace System.Collections.Generic
{
	/// <summary>Represents a generic read-only collection of key/value pairs.</summary>
	/// <typeparam name="TKey">The type of keys in the read-only dictionary.</typeparam>
	/// <typeparam name="TValue">The type of values in the read-only dictionary.</typeparam>
	// Token: 0x020004D7 RID: 1239
	[__DynamicallyInvokable]
	public interface IReadOnlyDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		/// <summary>Determines whether the read-only dictionary contains an element that has the specified key.</summary>
		/// <param name="key">The key to locate.</param>
		/// <returns>
		///   <see langword="true" /> if the read-only dictionary contains an element that has the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06003AF6 RID: 15094
		[__DynamicallyInvokable]
		bool ContainsKey(TKey key);

		/// <summary>Gets the value that is associated with the specified key.</summary>
		/// <param name="key">The key to locate.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the object that implements the <see cref="T:System.Collections.Generic.IReadOnlyDictionary`2" /> interface contains an element that has the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06003AF7 RID: 15095
		[__DynamicallyInvokable]
		bool TryGetValue(TKey key, out TValue value);

		/// <summary>Gets the element that has the specified key in the read-only dictionary.</summary>
		/// <param name="key">The key to locate.</param>
		/// <returns>The element that has the specified key in the read-only dictionary.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> is not found.</exception>
		// Token: 0x170008EF RID: 2287
		[__DynamicallyInvokable]
		TValue this[TKey key]
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets an enumerable collection that contains the keys in the read-only dictionary.</summary>
		/// <returns>An enumerable collection that contains the keys in the read-only dictionary.</returns>
		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06003AF9 RID: 15097
		[__DynamicallyInvokable]
		IEnumerable<TKey> Keys
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets an enumerable collection that contains the values in the read-only dictionary.</summary>
		/// <returns>An enumerable collection that contains the values in the read-only dictionary.</returns>
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06003AFA RID: 15098
		[__DynamicallyInvokable]
		IEnumerable<TValue> Values
		{
			[__DynamicallyInvokable]
			get;
		}
	}
}
