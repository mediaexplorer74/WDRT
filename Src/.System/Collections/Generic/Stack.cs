using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a variable size last-in-first-out (LIFO) collection of instances of the same specified type.</summary>
	/// <typeparam name="T">Specifies the type of elements in the stack.</typeparam>
	// Token: 0x020003C6 RID: 966
	[DebuggerTypeProxy(typeof(System_StackDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(false)]
	[global::__DynamicallyInvokable]
	[Serializable]
	public class Stack<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Stack`1" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x0600248C RID: 9356 RVA: 0x000AAAE7 File Offset: 0x000A8CE7
		[global::__DynamicallyInvokable]
		public Stack()
		{
			this._array = Stack<T>._emptyArray;
			this._size = 0;
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Stack`1" /> class that is empty and has the specified initial capacity or the default initial capacity, whichever is greater.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Stack`1" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x0600248D RID: 9357 RVA: 0x000AAB08 File Offset: 0x000A8D08
		[global::__DynamicallyInvokable]
		public Stack(int capacity)
		{
			if (capacity < 0)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.capacity, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNumRequired);
			}
			this._array = new T[capacity];
			this._size = 0;
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Stack`1" /> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.</summary>
		/// <param name="collection">The collection to copy elements from.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is <see langword="null" />.</exception>
		// Token: 0x0600248E RID: 9358 RVA: 0x000AAB38 File Offset: 0x000A8D38
		[global::__DynamicallyInvokable]
		public Stack(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.collection);
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				this._array = new T[count];
				collection2.CopyTo(this._array, 0);
				this._size = count;
				return;
			}
			this._size = 0;
			this._array = new T[4];
			foreach (T t in collection)
			{
				this.Push(t);
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x000AABD4 File Offset: 0x000A8DD4
		[global::__DynamicallyInvokable]
		public int Count
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.Stack`1" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x000AABDC File Offset: 0x000A8DDC
		[global::__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[global::__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Stack`1" />, this property always returns the current instance.</returns>
		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x000AABDF File Offset: 0x000A8DDF
		[global::__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		// Token: 0x06002492 RID: 9362 RVA: 0x000AAC01 File Offset: 0x000A8E01
		[global::__DynamicallyInvokable]
		public void Clear()
		{
			Array.Clear(this._array, 0, this._size);
			this._size = 0;
			this._version++;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.Stack`1" />. The value can be <see langword="null" /> for reference types.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.Stack`1" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002493 RID: 9363 RVA: 0x000AAC2C File Offset: 0x000A8E2C
		[global::__DynamicallyInvokable]
		public bool Contains(T item)
		{
			int size = this._size;
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			while (size-- > 0)
			{
				if (item == null)
				{
					if (this._array[size] == null)
					{
						return true;
					}
				}
				else if (this._array[size] != null && @default.Equals(this._array[size], item))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Generic.Stack`1" /> to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Stack`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Stack`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06002494 RID: 9364 RVA: 0x000AAC98 File Offset: 0x000A8E98
		[global::__DynamicallyInvokable]
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.array);
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.arrayIndex, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - arrayIndex < this._size)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidOffLen);
			}
			Array.Copy(this._array, 0, array, arrayIndex, this._size);
			Array.Reverse(array, arrayIndex, this._size);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// <paramref name="array" /> does not have zero-based indexing.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.  
		/// -or-  
		/// The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06002495 RID: 9365 RVA: 0x000AACF8 File Offset: 0x000A8EF8
		[global::__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.arrayIndex, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - arrayIndex < this._size)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidOffLen);
			}
			try
			{
				Array.Copy(this._array, 0, array, arrayIndex, this._size);
				Array.Reverse(array, arrayIndex, this._size);
			}
			catch (ArrayTypeMismatchException)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
			}
		}

		/// <summary>Returns an enumerator for the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.Stack`1.Enumerator" /> for the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		// Token: 0x06002496 RID: 9366 RVA: 0x000AAD98 File Offset: 0x000A8F98
		[global::__DynamicallyInvokable]
		public Stack<T>.Enumerator GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000AADA0 File Offset: 0x000A8FA0
		[global::__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06002498 RID: 9368 RVA: 0x000AADAD File Offset: 0x000A8FAD
		[global::__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Generic.Stack`1" />, if that number is less than 90 percent of current capacity.</summary>
		// Token: 0x06002499 RID: 9369 RVA: 0x000AADBC File Offset: 0x000A8FBC
		[global::__DynamicallyInvokable]
		public void TrimExcess()
		{
			int num = (int)((double)this._array.Length * 0.9);
			if (this._size < num)
			{
				T[] array = new T[this._size];
				Array.Copy(this._array, 0, array, 0, this._size);
				this._array = array;
				this._version++;
			}
		}

		/// <summary>Returns the object at the top of the <see cref="T:System.Collections.Generic.Stack`1" /> without removing it.</summary>
		/// <returns>The object at the top of the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.Stack`1" /> is empty.</exception>
		// Token: 0x0600249A RID: 9370 RVA: 0x000AAE1C File Offset: 0x000A901C
		[global::__DynamicallyInvokable]
		public T Peek()
		{
			if (this._size == 0)
			{
				System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EmptyStack);
			}
			return this._array[this._size - 1];
		}

		/// <summary>Removes and returns the object at the top of the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <returns>The object removed from the top of the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.Stack`1" /> is empty.</exception>
		// Token: 0x0600249B RID: 9371 RVA: 0x000AAE40 File Offset: 0x000A9040
		[global::__DynamicallyInvokable]
		public T Pop()
		{
			if (this._size == 0)
			{
				System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EmptyStack);
			}
			this._version++;
			T[] array = this._array;
			int num = this._size - 1;
			this._size = num;
			T t = array[num];
			this._array[this._size] = default(T);
			return t;
		}

		/// <summary>Inserts an object at the top of the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <param name="item">The object to push onto the <see cref="T:System.Collections.Generic.Stack`1" />. The value can be <see langword="null" /> for reference types.</param>
		// Token: 0x0600249C RID: 9372 RVA: 0x000AAEA4 File Offset: 0x000A90A4
		[global::__DynamicallyInvokable]
		public void Push(T item)
		{
			if (this._size == this._array.Length)
			{
				T[] array = new T[(this._array.Length == 0) ? 4 : (2 * this._array.Length)];
				Array.Copy(this._array, 0, array, 0, this._size);
				this._array = array;
			}
			T[] array2 = this._array;
			int size = this._size;
			this._size = size + 1;
			array2[size] = item;
			this._version++;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Generic.Stack`1" /> to a new array.</summary>
		/// <returns>A new array containing copies of the elements of the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		// Token: 0x0600249D RID: 9373 RVA: 0x000AAF24 File Offset: 0x000A9124
		[global::__DynamicallyInvokable]
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			for (int i = 0; i < this._size; i++)
			{
				array[i] = this._array[this._size - i - 1];
			}
			return array;
		}

		// Token: 0x0400200F RID: 8207
		private T[] _array;

		// Token: 0x04002010 RID: 8208
		private int _size;

		// Token: 0x04002011 RID: 8209
		private int _version;

		// Token: 0x04002012 RID: 8210
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002013 RID: 8211
		private const int _defaultCapacity = 4;

		// Token: 0x04002014 RID: 8212
		private static T[] _emptyArray = new T[0];

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <typeparam name="T" />
		// Token: 0x020007F8 RID: 2040
		[global::__DynamicallyInvokable]
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x0600444B RID: 17483 RVA: 0x0011E444 File Offset: 0x0011C644
			internal Enumerator(Stack<T> stack)
			{
				this._stack = stack;
				this._version = this._stack._version;
				this._index = -2;
				this.currentElement = default(T);
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Stack`1.Enumerator" />.</summary>
			// Token: 0x0600444C RID: 17484 RVA: 0x0011E472 File Offset: 0x0011C672
			[global::__DynamicallyInvokable]
			public void Dispose()
			{
				this._index = -1;
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x0600444D RID: 17485 RVA: 0x0011E47C File Offset: 0x0011C67C
			[global::__DynamicallyInvokable]
			public bool MoveNext()
			{
				if (this._version != this._stack._version)
				{
					System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				bool flag;
				if (this._index == -2)
				{
					this._index = this._stack._size - 1;
					flag = this._index >= 0;
					if (flag)
					{
						this.currentElement = this._stack._array[this._index];
					}
					return flag;
				}
				if (this._index == -1)
				{
					return false;
				}
				int num = this._index - 1;
				this._index = num;
				flag = num >= 0;
				if (flag)
				{
					this.currentElement = this._stack._array[this._index];
				}
				else
				{
					this.currentElement = default(T);
				}
				return flag;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.Stack`1" /> at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x17000F83 RID: 3971
			// (get) Token: 0x0600444E RID: 17486 RVA: 0x0011E53F File Offset: 0x0011C73F
			[global::__DynamicallyInvokable]
			public T Current
			{
				[global::__DynamicallyInvokable]
				get
				{
					if (this._index == -2)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumNotStarted);
					}
					if (this._index == -1)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumEnded);
					}
					return this.currentElement;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x17000F84 RID: 3972
			// (get) Token: 0x0600444F RID: 17487 RVA: 0x0011E568 File Offset: 0x0011C768
			[global::__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[global::__DynamicallyInvokable]
				get
				{
					if (this._index == -2)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumNotStarted);
					}
					if (this._index == -1)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumEnded);
					}
					return this.currentElement;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection. This class cannot be inherited.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x06004450 RID: 17488 RVA: 0x0011E596 File Offset: 0x0011C796
			[global::__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				if (this._version != this._stack._version)
				{
					System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this._index = -2;
				this.currentElement = default(T);
			}

			// Token: 0x04003513 RID: 13587
			private Stack<T> _stack;

			// Token: 0x04003514 RID: 13588
			private int _index;

			// Token: 0x04003515 RID: 13589
			private int _version;

			// Token: 0x04003516 RID: 13590
			private T currentElement;
		}
	}
}
