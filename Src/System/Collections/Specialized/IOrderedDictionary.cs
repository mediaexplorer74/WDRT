using System;

namespace System.Collections.Specialized
{
	/// <summary>Represents an indexed collection of key/value pairs.</summary>
	// Token: 0x020003AC RID: 940
	public interface IOrderedDictionary : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ICollection.Count" />.</exception>
		// Token: 0x170008DE RID: 2270
		object this[int index] { get; set; }

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the entire <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection.</returns>
		// Token: 0x06002308 RID: 8968
		IDictionaryEnumerator GetEnumerator();

		/// <summary>Inserts a key/value pair into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which the key/value pair should be inserted.</param>
		/// <param name="key">The object to use as the key of the element to add.</param>
		/// <param name="value">The object to use as the value of the element to add.  The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.ICollection.Count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection has a fixed size.</exception>
		// Token: 0x06002309 RID: 8969
		void Insert(int index, object key, object value);

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ICollection.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection has a fixed size.</exception>
		// Token: 0x0600230A RID: 8970
		void RemoveAt(int index);
	}
}
