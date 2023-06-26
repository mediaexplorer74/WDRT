using System;
using System.Collections.Generic;

namespace System.Collections.Concurrent
{
	/// <summary>Defines methods to manipulate thread-safe collections intended for producer/consumer usage. This interface provides a unified representation for producer/consumer collections so that higher level abstractions such as <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> can use the collection as the underlying storage mechanism.</summary>
	/// <typeparam name="T">Specifies the type of elements in the collection.</typeparam>
	// Token: 0x020004A9 RID: 1193
	[__DynamicallyInvokable]
	public interface IProducerConsumerCollection<T> : IEnumerable<T>, IEnumerable, ICollection
	{
		/// <summary>Copies the elements of the <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" /> to an <see cref="T:System.Array" />, starting at a specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />.  
		///  The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- The number of elements in the collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x0600393A RID: 14650
		[__DynamicallyInvokable]
		void CopyTo(T[] array, int index);

		/// <summary>Attempts to add an object to the <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />.</summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />.</param>
		/// <returns>true if the object was added successfully; otherwise, false.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="item" /> was invalid for this collection.</exception>
		// Token: 0x0600393B RID: 14651
		[__DynamicallyInvokable]
		bool TryAdd(T item);

		/// <summary>Attempts to remove and return an object from the <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />.</summary>
		/// <param name="item">When this method returns, if the object was removed and returned successfully, <paramref name="item" /> contains the removed object. If no object was available to be removed, the value is unspecified.</param>
		/// <returns>true if an object was removed and returned successfully; otherwise, false.</returns>
		// Token: 0x0600393C RID: 14652
		[__DynamicallyInvokable]
		bool TryTake(out T item);

		/// <summary>Copies the elements contained in the <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" /> to a new array.</summary>
		/// <returns>A new array containing the elements copied from the <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />.</returns>
		// Token: 0x0600393D RID: 14653
		[__DynamicallyInvokable]
		T[] ToArray();
	}
}
