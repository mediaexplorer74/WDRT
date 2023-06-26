using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Represents a nongeneric collection of key/value pairs.</summary>
	// Token: 0x0200049A RID: 1178
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IDictionary : ICollection, IEnumerable
	{
		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <param name="key">The key of the element to get or set.</param>
		/// <returns>The element with the specified key, or <see langword="null" /> if the key does not exist.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.IDictionary" /> object is read-only.  
		///  -or-  
		///  The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</exception>
		// Token: 0x17000870 RID: 2160
		[__DynamicallyInvokable]
		object this[object key]
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.</returns>
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060038C6 RID: 14534
		[__DynamicallyInvokable]
		ICollection Keys
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.IDictionary" /> object.</returns>
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060038C7 RID: 14535
		[__DynamicallyInvokable]
		ICollection Values
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> object contains an element with the specified key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060038C8 RID: 14536
		[__DynamicallyInvokable]
		bool Contains(object key);

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" /> object.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.IDictionary" /> has a fixed size.</exception>
		// Token: 0x060038C9 RID: 14537
		[__DynamicallyInvokable]
		void Add(object key, object value);

		/// <summary>Removes all elements from the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> object is read-only.</exception>
		// Token: 0x060038CA RID: 14538
		[__DynamicallyInvokable]
		void Clear();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> object is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060038CB RID: 14539
		[__DynamicallyInvokable]
		bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> object has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060038CC RID: 14540
		[__DynamicallyInvokable]
		bool IsFixedSize
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.IDictionary" /> object.</returns>
		// Token: 0x060038CD RID: 14541
		[__DynamicallyInvokable]
		IDictionaryEnumerator GetEnumerator();

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> object is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.IDictionary" /> has a fixed size.</exception>
		// Token: 0x060038CE RID: 14542
		[__DynamicallyInvokable]
		void Remove(object key);
	}
}
