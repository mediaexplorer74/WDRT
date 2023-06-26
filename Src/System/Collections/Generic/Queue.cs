using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a first-in, first-out collection of objects.</summary>
	/// <typeparam name="T">Specifies the type of elements in the queue.</typeparam>
	// Token: 0x020003C4 RID: 964
	[DebuggerTypeProxy(typeof(System_QueueDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(false)]
	[global::__DynamicallyInvokable]
	[Serializable]
	public class Queue<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Queue`1" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x0600243E RID: 9278 RVA: 0x000A99E3 File Offset: 0x000A7BE3
		[global::__DynamicallyInvokable]
		public Queue()
		{
			this._array = Queue<T>._emptyArray;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Queue`1" /> class that is empty and has the specified initial capacity.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Queue`1" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x0600243F RID: 9279 RVA: 0x000A99F6 File Offset: 0x000A7BF6
		[global::__DynamicallyInvokable]
		public Queue(int capacity)
		{
			if (capacity < 0)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.capacity, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNumRequired);
			}
			this._array = new T[capacity];
			this._head = 0;
			this._tail = 0;
			this._size = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Queue`1" /> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.</summary>
		/// <param name="collection">The collection whose elements are copied to the new <see cref="T:System.Collections.Generic.Queue`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is <see langword="null" />.</exception>
		// Token: 0x06002440 RID: 9280 RVA: 0x000A9A2C File Offset: 0x000A7C2C
		[global::__DynamicallyInvokable]
		public Queue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.collection);
			}
			this._array = new T[4];
			this._size = 0;
			this._version = 0;
			foreach (T t in collection)
			{
				this.Enqueue(t);
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x000A9A9C File Offset: 0x000A7C9C
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
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.Queue`1" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x000A9AA4 File Offset: 0x000A7CA4
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
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Queue`1" />, this property always returns the current instance.</returns>
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x000A9AA7 File Offset: 0x000A7CA7
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

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		// Token: 0x06002444 RID: 9284 RVA: 0x000A9ACC File Offset: 0x000A7CCC
		[global::__DynamicallyInvokable]
		public void Clear()
		{
			if (this._head < this._tail)
			{
				Array.Clear(this._array, this._head, this._size);
			}
			else
			{
				Array.Clear(this._array, this._head, this._array.Length - this._head);
				Array.Clear(this._array, 0, this._tail);
			}
			this._head = 0;
			this._tail = 0;
			this._size = 0;
			this._version++;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Generic.Queue`1" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Queue`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Queue`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06002445 RID: 9285 RVA: 0x000A9B58 File Offset: 0x000A7D58
		[global::__DynamicallyInvokable]
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.array);
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.arrayIndex, System.ExceptionResource.ArgumentOutOfRange_Index);
			}
			int num = array.Length;
			if (num - arrayIndex < this._size)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidOffLen);
			}
			int num2 = ((num - arrayIndex < this._size) ? (num - arrayIndex) : this._size);
			if (num2 == 0)
			{
				return;
			}
			int num3 = ((this._array.Length - this._head < num2) ? (this._array.Length - this._head) : num2);
			Array.Copy(this._array, this._head, array, arrayIndex, num3);
			num2 -= num3;
			if (num2 > 0)
			{
				Array.Copy(this._array, 0, array, arrayIndex + this._array.Length - this._head, num2);
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// <paramref name="array" /> does not have zero-based indexing.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.  
		/// -or-  
		/// The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06002446 RID: 9286 RVA: 0x000A9C14 File Offset: 0x000A7E14
		[global::__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
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
			int length = array.Length;
			if (index < 0 || index > length)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.index, System.ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (length - index < this._size)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidOffLen);
			}
			int num = ((length - index < this._size) ? (length - index) : this._size);
			if (num == 0)
			{
				return;
			}
			try
			{
				int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
				Array.Copy(this._array, this._head, array, index, num2);
				num -= num2;
				if (num > 0)
				{
					Array.Copy(this._array, 0, array, index + this._array.Length - this._head, num);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.Queue`1" />. The value can be <see langword="null" /> for reference types.</param>
		// Token: 0x06002447 RID: 9287 RVA: 0x000A9D0C File Offset: 0x000A7F0C
		[global::__DynamicallyInvokable]
		public void Enqueue(T item)
		{
			if (this._size == this._array.Length)
			{
				int num = (int)((long)this._array.Length * 200L / 100L);
				if (num < this._array.Length + 4)
				{
					num = this._array.Length + 4;
				}
				this.SetCapacity(num);
			}
			this._array[this._tail] = item;
			this._tail = (this._tail + 1) % this._array.Length;
			this._size++;
			this._version++;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.Queue`1.Enumerator" /> for the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		// Token: 0x06002448 RID: 9288 RVA: 0x000A9DA3 File Offset: 0x000A7FA3
		[global::__DynamicallyInvokable]
		public Queue<T>.Enumerator GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000A9DAB File Offset: 0x000A7FAB
		[global::__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x0600244A RID: 9290 RVA: 0x000A9DB8 File Offset: 0x000A7FB8
		[global::__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		/// <summary>Removes and returns the object at the beginning of the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <returns>The object that is removed from the beginning of the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.Queue`1" /> is empty.</exception>
		// Token: 0x0600244B RID: 9291 RVA: 0x000A9DC8 File Offset: 0x000A7FC8
		[global::__DynamicallyInvokable]
		public T Dequeue()
		{
			if (this._size == 0)
			{
				System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EmptyQueue);
			}
			T t = this._array[this._head];
			this._array[this._head] = default(T);
			this._head = (this._head + 1) % this._array.Length;
			this._size--;
			this._version++;
			return t;
		}

		/// <summary>Returns the object at the beginning of the <see cref="T:System.Collections.Generic.Queue`1" /> without removing it.</summary>
		/// <returns>The object at the beginning of the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.Queue`1" /> is empty.</exception>
		// Token: 0x0600244C RID: 9292 RVA: 0x000A9E44 File Offset: 0x000A8044
		[global::__DynamicallyInvokable]
		public T Peek()
		{
			if (this._size == 0)
			{
				System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EmptyQueue);
			}
			return this._array[this._head];
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.Queue`1" />. The value can be <see langword="null" /> for reference types.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.Queue`1" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600244D RID: 9293 RVA: 0x000A9E68 File Offset: 0x000A8068
		[global::__DynamicallyInvokable]
		public bool Contains(T item)
		{
			int num = this._head;
			int size = this._size;
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			while (size-- > 0)
			{
				if (item == null)
				{
					if (this._array[num] == null)
					{
						return true;
					}
				}
				else if (this._array[num] != null && @default.Equals(this._array[num], item))
				{
					return true;
				}
				num = (num + 1) % this._array.Length;
			}
			return false;
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000A9EE8 File Offset: 0x000A80E8
		internal T GetElement(int i)
		{
			return this._array[(this._head + i) % this._array.Length];
		}

		/// <summary>Copies the <see cref="T:System.Collections.Generic.Queue`1" /> elements to a new array.</summary>
		/// <returns>A new array containing elements copied from the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		// Token: 0x0600244F RID: 9295 RVA: 0x000A9F08 File Offset: 0x000A8108
		[global::__DynamicallyInvokable]
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			if (this._size == 0)
			{
				return array;
			}
			if (this._head < this._tail)
			{
				Array.Copy(this._array, this._head, array, 0, this._size);
			}
			else
			{
				Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
				Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
			}
			return array;
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000A9F9C File Offset: 0x000A819C
		private void SetCapacity(int capacity)
		{
			T[] array = new T[capacity];
			if (this._size > 0)
			{
				if (this._head < this._tail)
				{
					Array.Copy(this._array, this._head, array, 0, this._size);
				}
				else
				{
					Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
					Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
				}
			}
			this._array = array;
			this._head = 0;
			this._tail = ((this._size == capacity) ? 0 : this._size);
			this._version++;
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Generic.Queue`1" />, if that number is less than 90 percent of current capacity.</summary>
		// Token: 0x06002451 RID: 9297 RVA: 0x000AA05C File Offset: 0x000A825C
		[global::__DynamicallyInvokable]
		public void TrimExcess()
		{
			int num = (int)((double)this._array.Length * 0.9);
			if (this._size < num)
			{
				this.SetCapacity(this._size);
			}
		}

		// Token: 0x04001FF8 RID: 8184
		private T[] _array;

		// Token: 0x04001FF9 RID: 8185
		private int _head;

		// Token: 0x04001FFA RID: 8186
		private int _tail;

		// Token: 0x04001FFB RID: 8187
		private int _size;

		// Token: 0x04001FFC RID: 8188
		private int _version;

		// Token: 0x04001FFD RID: 8189
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04001FFE RID: 8190
		private const int _MinimumGrow = 4;

		// Token: 0x04001FFF RID: 8191
		private const int _ShrinkThreshold = 32;

		// Token: 0x04002000 RID: 8192
		private const int _GrowFactor = 200;

		// Token: 0x04002001 RID: 8193
		private const int _DefaultCapacity = 4;

		// Token: 0x04002002 RID: 8194
		private static T[] _emptyArray = new T[0];

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <typeparam name="T" />
		// Token: 0x020007F2 RID: 2034
		[global::__DynamicallyInvokable]
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x0600440C RID: 17420 RVA: 0x0011DB93 File Offset: 0x0011BD93
			internal Enumerator(Queue<T> q)
			{
				this._q = q;
				this._version = this._q._version;
				this._index = -1;
				this._currentElement = default(T);
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Queue`1.Enumerator" />.</summary>
			// Token: 0x0600440D RID: 17421 RVA: 0x0011DBC0 File Offset: 0x0011BDC0
			[global::__DynamicallyInvokable]
			public void Dispose()
			{
				this._index = -2;
				this._currentElement = default(T);
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x0600440E RID: 17422 RVA: 0x0011DBD8 File Offset: 0x0011BDD8
			[global::__DynamicallyInvokable]
			public bool MoveNext()
			{
				if (this._version != this._q._version)
				{
					System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				if (this._index == -2)
				{
					return false;
				}
				this._index++;
				if (this._index == this._q._size)
				{
					this._index = -2;
					this._currentElement = default(T);
					return false;
				}
				this._currentElement = this._q.GetElement(this._index);
				return true;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.Queue`1" /> at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x17000F6E RID: 3950
			// (get) Token: 0x0600440F RID: 17423 RVA: 0x0011DC5A File Offset: 0x0011BE5A
			[global::__DynamicallyInvokable]
			public T Current
			{
				[global::__DynamicallyInvokable]
				get
				{
					if (this._index < 0)
					{
						if (this._index == -1)
						{
							System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumNotStarted);
						}
						else
						{
							System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumEnded);
						}
					}
					return this._currentElement;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x17000F6F RID: 3951
			// (get) Token: 0x06004410 RID: 17424 RVA: 0x0011DC84 File Offset: 0x0011BE84
			[global::__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[global::__DynamicallyInvokable]
				get
				{
					if (this._index < 0)
					{
						if (this._index == -1)
						{
							System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumNotStarted);
						}
						else
						{
							System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumEnded);
						}
					}
					return this._currentElement;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x06004411 RID: 17425 RVA: 0x0011DCB3 File Offset: 0x0011BEB3
			[global::__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				if (this._version != this._q._version)
				{
					System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this._index = -1;
				this._currentElement = default(T);
			}

			// Token: 0x040034FD RID: 13565
			private Queue<T> _q;

			// Token: 0x040034FE RID: 13566
			private int _index;

			// Token: 0x040034FF RID: 13567
			private int _version;

			// Token: 0x04003500 RID: 13568
			private T _currentElement;
		}
	}
}
