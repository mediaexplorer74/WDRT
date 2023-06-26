using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a strongly typed list of objects that can be accessed by index. Provides methods to search, sort, and manipulate lists.</summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	// Token: 0x020004DA RID: 1242
	[DebuggerTypeProxy(typeof(Mscorlib_CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public class List<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.List`1" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x06003B03 RID: 15107 RVA: 0x000E12E1 File Offset: 0x000DF4E1
		[__DynamicallyInvokable]
		public List()
		{
			this._items = List<T>._emptyArray;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.List`1" /> class that is empty and has the specified initial capacity.</summary>
		/// <param name="capacity">The number of elements that the new list can initially store.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than 0.</exception>
		// Token: 0x06003B04 RID: 15108 RVA: 0x000E12F4 File Offset: 0x000DF4F4
		[__DynamicallyInvokable]
		public List(int capacity)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (capacity == 0)
			{
				this._items = List<T>._emptyArray;
				return;
			}
			this._items = new T[capacity];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.List`1" /> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.</summary>
		/// <param name="collection">The collection whose elements are copied to the new list.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is <see langword="null" />.</exception>
		// Token: 0x06003B05 RID: 15109 RVA: 0x000E1324 File Offset: 0x000DF524
		[__DynamicallyInvokable]
		public List(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 == null)
			{
				this._size = 0;
				this._items = List<T>._emptyArray;
				foreach (T t in collection)
				{
					this.Add(t);
				}
				return;
			}
			int count = collection2.Count;
			if (count == 0)
			{
				this._items = List<T>._emptyArray;
				return;
			}
			this._items = new T[count];
			collection2.CopyTo(this._items, 0);
			this._size = count;
		}

		/// <summary>Gets or sets the total number of elements the internal data structure can hold without resizing.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.Generic.List`1" /> can contain before resizing is required.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.Generic.List`1.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.Generic.List`1.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06003B06 RID: 15110 RVA: 0x000E13CC File Offset: 0x000DF5CC
		// (set) Token: 0x06003B07 RID: 15111 RVA: 0x000E13D8 File Offset: 0x000DF5D8
		[__DynamicallyInvokable]
		public int Capacity
		{
			[__DynamicallyInvokable]
			get
			{
				return this._items.Length;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						T[] array = new T[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = List<T>._emptyArray;
				}
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.List`1" />.</returns>
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06003B08 RID: 15112 RVA: 0x000E143D File Offset: 0x000DF63D
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.List`1" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06003B09 RID: 15113 RVA: 0x000E1445 File Offset: 0x000DF645
		[__DynamicallyInvokable]
		bool IList.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06003B0A RID: 15114 RVA: 0x000E1448 File Offset: 0x000DF648
		[__DynamicallyInvokable]
		bool ICollection<T>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.List`1" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x000E144B File Offset: 0x000DF64B
		[__DynamicallyInvokable]
		bool IList.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.List`1" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06003B0C RID: 15116 RVA: 0x000E144E File Offset: 0x000DF64E
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.List`1" />, this property always returns the current instance.</returns>
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x000E1451 File Offset: 0x000DF651
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Generic.List`1.Count" />.</exception>
		// Token: 0x170008FB RID: 2299
		[__DynamicallyInvokable]
		public T this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				if (index >= this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				return this._items[index];
			}
			[__DynamicallyInvokable]
			set
			{
				if (index >= this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x000E14BC File Offset: 0x000DF6BC
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property is set and <paramref name="value" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x170008FC RID: 2300
		[__DynamicallyInvokable]
		object IList.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this[index];
			}
			[__DynamicallyInvokable]
			set
			{
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
				try
				{
					this[index] = (T)((object)value);
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
				}
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="item">The object to be added to the end of the <see cref="T:System.Collections.Generic.List`1" />. The value can be <see langword="null" /> for reference types.</param>
		// Token: 0x06003B13 RID: 15123 RVA: 0x000E1540 File Offset: 0x000DF740
		[__DynamicallyInvokable]
		public void Add(T item)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			T[] items = this._items;
			int size = this._size;
			this._size = size + 1;
			items[size] = item;
			this._version++;
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="item">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="item" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x06003B14 RID: 15124 RVA: 0x000E1598 File Offset: 0x000DF798
		[__DynamicallyInvokable]
		int IList.Add(object item)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
			try
			{
				this.Add((T)((object)item));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof(T));
			}
			return this.Count - 1;
		}

		/// <summary>Adds the elements of the specified collection to the end of the <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="collection">The collection whose elements should be added to the end of the <see cref="T:System.Collections.Generic.List`1" />. The collection itself cannot be <see langword="null" />, but it can contain elements that are <see langword="null" />, if type <paramref name="T" /> is a reference type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is <see langword="null" />.</exception>
		// Token: 0x06003B15 RID: 15125 RVA: 0x000E15E8 File Offset: 0x000DF7E8
		[__DynamicallyInvokable]
		public void AddRange(IEnumerable<T> collection)
		{
			this.InsertRange(this._size, collection);
		}

		/// <summary>Returns a read-only <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> wrapper for the current collection.</summary>
		/// <returns>An object that acts as a read-only wrapper around the current <see cref="T:System.Collections.Generic.List`1" />.</returns>
		// Token: 0x06003B16 RID: 15126 RVA: 0x000E15F7 File Offset: 0x000DF7F7
		[__DynamicallyInvokable]
		public ReadOnlyCollection<T> AsReadOnly()
		{
			return new ReadOnlyCollection<T>(this);
		}

		/// <summary>Searches a range of elements in the sorted <see cref="T:System.Collections.Generic.List`1" /> for an element using the specified comparer and returns the zero-based index of the element.</summary>
		/// <param name="index">The zero-based starting index of the range to search.</param>
		/// <param name="count">The length of the range to search.</param>
		/// <param name="item">The object to locate. The value can be <see langword="null" /> for reference types.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing elements, or <see langword="null" /> to use the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" />.</param>
		/// <returns>The zero-based index of <paramref name="item" /> in the sorted <see cref="T:System.Collections.Generic.List`1" />, if <paramref name="item" /> is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than <paramref name="item" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.Generic.List`1.Count" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in the <see cref="T:System.Collections.Generic.List`1" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" /> cannot find an implementation of the <see cref="T:System.IComparable`1" /> generic interface or the <see cref="T:System.IComparable" /> interface for type <paramref name="T" />.</exception>
		// Token: 0x06003B17 RID: 15127 RVA: 0x000E15FF File Offset: 0x000DF7FF
		[__DynamicallyInvokable]
		public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			return Array.BinarySearch<T>(this._items, index, count, item, comparer);
		}

		/// <summary>Searches the entire sorted <see cref="T:System.Collections.Generic.List`1" /> for an element using the default comparer and returns the zero-based index of the element.</summary>
		/// <param name="item">The object to locate. The value can be <see langword="null" /> for reference types.</param>
		/// <returns>The zero-based index of <paramref name="item" /> in the sorted <see cref="T:System.Collections.Generic.List`1" />, if <paramref name="item" /> is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than <paramref name="item" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.Generic.List`1.Count" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" /> cannot find an implementation of the <see cref="T:System.IComparable`1" /> generic interface or the <see cref="T:System.IComparable" /> interface for type <paramref name="T" />.</exception>
		// Token: 0x06003B18 RID: 15128 RVA: 0x000E163B File Offset: 0x000DF83B
		[__DynamicallyInvokable]
		public int BinarySearch(T item)
		{
			return this.BinarySearch(0, this.Count, item, null);
		}

		/// <summary>Searches the entire sorted <see cref="T:System.Collections.Generic.List`1" /> for an element using the specified comparer and returns the zero-based index of the element.</summary>
		/// <param name="item">The object to locate. The value can be <see langword="null" /> for reference types.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing elements.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" />.</param>
		/// <returns>The zero-based index of <paramref name="item" /> in the sorted <see cref="T:System.Collections.Generic.List`1" />, if <paramref name="item" /> is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than <paramref name="item" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.Generic.List`1.Count" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" /> cannot find an implementation of the <see cref="T:System.IComparable`1" /> generic interface or the <see cref="T:System.IComparable" /> interface for type <paramref name="T" />.</exception>
		// Token: 0x06003B19 RID: 15129 RVA: 0x000E164C File Offset: 0x000DF84C
		[__DynamicallyInvokable]
		public int BinarySearch(T item, IComparer<T> comparer)
		{
			return this.BinarySearch(0, this.Count, item, comparer);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Generic.List`1" />.</summary>
		// Token: 0x06003B1A RID: 15130 RVA: 0x000E165D File Offset: 0x000DF85D
		[__DynamicallyInvokable]
		public void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1" />. The value can be <see langword="null" /> for reference types.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.List`1" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003B1B RID: 15131 RVA: 0x000E1690 File Offset: 0x000DF890
		[__DynamicallyInvokable]
		public bool Contains(T item)
		{
			if (item == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int j = 0; j < this._size; j++)
			{
				if (@default.Equals(this._items[j], item))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <param name="item">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003B1C RID: 15132 RVA: 0x000E16FC File Offset: 0x000DF8FC
		[__DynamicallyInvokable]
		bool IList.Contains(object item)
		{
			return List<T>.IsCompatibleObject(item) && this.Contains((T)((object)item));
		}

		/// <summary>Converts the elements in the current <see cref="T:System.Collections.Generic.List`1" /> to another type, and returns a list containing the converted elements.</summary>
		/// <param name="converter">A <see cref="T:System.Converter`2" /> delegate that converts each element from one type to another type.</param>
		/// <typeparam name="TOutput">The type of the elements of the target array.</typeparam>
		/// <returns>A <see cref="T:System.Collections.Generic.List`1" /> of the target type containing the converted elements from the current <see cref="T:System.Collections.Generic.List`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="converter" /> is <see langword="null" />.</exception>
		// Token: 0x06003B1D RID: 15133 RVA: 0x000E1714 File Offset: 0x000DF914
		public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
		{
			if (converter == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.converter);
			}
			List<TOutput> list = new List<TOutput>(this._size);
			for (int i = 0; i < this._size; i++)
			{
				list._items[i] = converter(this._items[i]);
			}
			list._size = this._size;
			return list;
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Generic.List`1" /> to a compatible one-dimensional array, starting at the beginning of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.List`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.List`1" /> is greater than the number of elements that the destination <paramref name="array" /> can contain.</exception>
		// Token: 0x06003B1E RID: 15134 RVA: 0x000E1773 File Offset: 0x000DF973
		[__DynamicallyInvokable]
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// <paramref name="array" /> does not have zero-based indexing.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.  
		/// -or-  
		/// The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003B1F RID: 15135 RVA: 0x000E1780 File Offset: 0x000DF980
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			try
			{
				Array.Copy(this._items, 0, array, arrayIndex, this._size);
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		/// <summary>Copies a range of elements from the <see cref="T:System.Collections.Generic.List`1" /> to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="index">The zero-based index in the source <see cref="T:System.Collections.Generic.List`1" /> at which copying begins.</param>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.List`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <param name="count">The number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="arrayIndex" /> is less than 0.  
		/// -or-  
		/// <paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the <see cref="P:System.Collections.Generic.List`1.Count" /> of the source <see cref="T:System.Collections.Generic.List`1" />.  
		/// -or-  
		/// The number of elements from <paramref name="index" /> to the end of the source <see cref="T:System.Collections.Generic.List`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06003B20 RID: 15136 RVA: 0x000E17D0 File Offset: 0x000DF9D0
		[__DynamicallyInvokable]
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Generic.List`1" /> to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.List`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.List`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06003B21 RID: 15137 RVA: 0x000E17F5 File Offset: 0x000DF9F5
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x000E180C File Offset: 0x000DFA0C
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = ((this._items.Length == 0) ? 4 : (this._items.Length * 2));
				if (num > 2146435071)
				{
					num = 2146435071;
				}
				if (num < min)
				{
					num = min;
				}
				this.Capacity = num;
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.List`1" /> contains elements that match the conditions defined by the specified predicate.</summary>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the elements to search for.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.List`1" /> contains one or more elements that match the conditions defined by the specified predicate; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x06003B23 RID: 15139 RVA: 0x000E1856 File Offset: 0x000DFA56
		[__DynamicallyInvokable]
		public bool Exists(Predicate<T> match)
		{
			return this.FindIndex(match) != -1;
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the element to search for.</param>
		/// <returns>The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type <paramref name="T" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x06003B24 RID: 15140 RVA: 0x000E1868 File Offset: 0x000DFA68
		[__DynamicallyInvokable]
		public T Find(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < this._size; i++)
			{
				if (match(this._items[i]))
				{
					return this._items[i];
				}
			}
			return default(T);
		}

		/// <summary>Retrieves all the elements that match the conditions defined by the specified predicate.</summary>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the elements to search for.</param>
		/// <returns>A <see cref="T:System.Collections.Generic.List`1" /> containing all the elements that match the conditions defined by the specified predicate, if found; otherwise, an empty <see cref="T:System.Collections.Generic.List`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x06003B25 RID: 15141 RVA: 0x000E18BC File Offset: 0x000DFABC
		[__DynamicallyInvokable]
		public List<T> FindAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			List<T> list = new List<T>();
			for (int i = 0; i < this._size; i++)
			{
				if (match(this._items[i]))
				{
					list.Add(this._items[i]);
				}
			}
			return list;
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the element to search for.</param>
		/// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x06003B26 RID: 15142 RVA: 0x000E1910 File Offset: 0x000DFB10
		[__DynamicallyInvokable]
		public int FindIndex(Predicate<T> match)
		{
			return this.FindIndex(0, this._size, match);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1" /> that extends from the specified index to the last element.</summary>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the element to search for.</param>
		/// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1" />.</exception>
		// Token: 0x06003B27 RID: 15143 RVA: 0x000E1920 File Offset: 0x000DFB20
		[__DynamicallyInvokable]
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			return this.FindIndex(startIndex, this._size - startIndex, match);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1" /> that starts at the specified index and contains the specified number of elements.</summary>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the element to search for.</param>
		/// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1" />.  
		/// -or-  
		/// <paramref name="count" /> is less than 0.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.Generic.List`1" />.</exception>
		// Token: 0x06003B28 RID: 15144 RVA: 0x000E1934 File Offset: 0x000DFB34
		[__DynamicallyInvokable]
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			if (startIndex > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (count < 0 || startIndex > this._size - count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
			}
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (match(this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the last occurrence within the entire <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the element to search for.</param>
		/// <returns>The last element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type <paramref name="T" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x06003B29 RID: 15145 RVA: 0x000E199C File Offset: 0x000DFB9C
		[__DynamicallyInvokable]
		public T FindLast(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = this._size - 1; i >= 0; i--)
			{
				if (match(this._items[i]))
				{
					return this._items[i];
				}
			}
			return default(T);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the entire <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the element to search for.</param>
		/// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x06003B2A RID: 15146 RVA: 0x000E19EF File Offset: 0x000DFBEF
		[__DynamicallyInvokable]
		public int FindLastIndex(Predicate<T> match)
		{
			return this.FindLastIndex(this._size - 1, this._size, match);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1" /> that extends from the first element to the specified index.</summary>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the element to search for.</param>
		/// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1" />.</exception>
		// Token: 0x06003B2B RID: 15147 RVA: 0x000E1A06 File Offset: 0x000DFC06
		[__DynamicallyInvokable]
		public int FindLastIndex(int startIndex, Predicate<T> match)
		{
			return this.FindLastIndex(startIndex, startIndex + 1, match);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1" /> that contains the specified number of elements and ends at the specified index.</summary>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the element to search for.</param>
		/// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1" />.  
		/// -or-  
		/// <paramref name="count" /> is less than 0.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.Generic.List`1" />.</exception>
		// Token: 0x06003B2C RID: 15148 RVA: 0x000E1A14 File Offset: 0x000DFC14
		[__DynamicallyInvokable]
		public int FindLastIndex(int startIndex, int count, Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			if (this._size == 0)
			{
				if (startIndex != -1)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
				}
			}
			else if (startIndex >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
			}
			int num = startIndex - count;
			for (int i = startIndex; i > num; i--)
			{
				if (match(this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Performs the specified action on each element of the <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the <see cref="T:System.Collections.Generic.List`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="action" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
		// Token: 0x06003B2D RID: 15149 RVA: 0x000E1A90 File Offset: 0x000DFC90
		[__DynamicallyInvokable]
		public void ForEach(Action<T> action)
		{
			if (action == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int version = this._version;
			int num = 0;
			while (num < this._size && (version == this._version || !BinaryCompatibility.TargetsAtLeast_Desktop_V4_5))
			{
				action(this._items[num]);
				num++;
			}
			if (version != this._version && BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.List`1.Enumerator" /> for the <see cref="T:System.Collections.Generic.List`1" />.</returns>
		// Token: 0x06003B2E RID: 15150 RVA: 0x000E1AF7 File Offset: 0x000DFCF7
		[__DynamicallyInvokable]
		public List<T>.Enumerator GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000E1AFF File Offset: 0x000DFCFF
		[__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003B30 RID: 15152 RVA: 0x000E1B0C File Offset: 0x000DFD0C
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		/// <summary>Creates a shallow copy of a range of elements in the source <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="index">The zero-based <see cref="T:System.Collections.Generic.List`1" /> index at which the range starts.</param>
		/// <param name="count">The number of elements in the range.</param>
		/// <returns>A shallow copy of a range of elements in the source <see cref="T:System.Collections.Generic.List`1" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.Generic.List`1" />.</exception>
		// Token: 0x06003B31 RID: 15153 RVA: 0x000E1B1C File Offset: 0x000DFD1C
		[__DynamicallyInvokable]
		public List<T> GetRange(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			List<T> list = new List<T>(count);
			Array.Copy(this._items, index, list._items, 0, count);
			list._size = count;
			return list;
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1" />. The value can be <see langword="null" /> for reference types.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="item" /> within the entire <see cref="T:System.Collections.Generic.List`1" />, if found; otherwise, -1.</returns>
		// Token: 0x06003B32 RID: 15154 RVA: 0x000E1B76 File Offset: 0x000DFD76
		[__DynamicallyInvokable]
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The index of <paramref name="item" /> if found in the list; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="item" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x06003B33 RID: 15155 RVA: 0x000E1B8B File Offset: 0x000DFD8B
		[__DynamicallyInvokable]
		int IList.IndexOf(object item)
		{
			if (List<T>.IsCompatibleObject(item))
			{
				return this.IndexOf((T)((object)item));
			}
			return -1;
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1" /> that extends from the specified index to the last element.</summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1" />. The value can be <see langword="null" /> for reference types.</param>
		/// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="item" /> within the range of elements in the <see cref="T:System.Collections.Generic.List`1" /> that extends from <paramref name="index" /> to the last element, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1" />.</exception>
		// Token: 0x06003B34 RID: 15156 RVA: 0x000E1BA3 File Offset: 0x000DFDA3
		[__DynamicallyInvokable]
		public int IndexOf(T item, int index)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			return Array.IndexOf<T>(this._items, item, index, this._size - index);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1" /> that starts at the specified index and contains the specified number of elements.</summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1" />. The value can be <see langword="null" /> for reference types.</param>
		/// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="item" /> within the range of elements in the <see cref="T:System.Collections.Generic.List`1" /> that starts at <paramref name="index" /> and contains <paramref name="count" /> number of elements, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1" />.  
		/// -or-  
		/// <paramref name="count" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.Generic.List`1" />.</exception>
		// Token: 0x06003B35 RID: 15157 RVA: 0x000E1BCC File Offset: 0x000DFDCC
		[__DynamicallyInvokable]
		public int IndexOf(T item, int index, int count)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (count < 0 || index > this._size - count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
			}
			return Array.IndexOf<T>(this._items, item, index, count);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.Generic.List`1" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert. The value can be <see langword="null" /> for reference types.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.Generic.List`1.Count" />.</exception>
		// Token: 0x06003B36 RID: 15158 RVA: 0x000E1C08 File Offset: 0x000DFE08
		[__DynamicallyInvokable]
		public void Insert(int index, T item)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = item;
			this._size++;
			this._version++;
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert into the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="item" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x06003B37 RID: 15159 RVA: 0x000E1C94 File Offset: 0x000DFE94
		[__DynamicallyInvokable]
		void IList.Insert(int index, object item)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
			try
			{
				this.Insert(index, (T)((object)item));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof(T));
			}
		}

		/// <summary>Inserts the elements of a collection into the <see cref="T:System.Collections.Generic.List`1" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which the new elements should be inserted.</param>
		/// <param name="collection">The collection whose elements should be inserted into the <see cref="T:System.Collections.Generic.List`1" />. The collection itself cannot be <see langword="null" />, but it can contain elements that are <see langword="null" />, if type <paramref name="T" /> is a reference type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.Generic.List`1.Count" />.</exception>
		// Token: 0x06003B38 RID: 15160 RVA: 0x000E1CDC File Offset: 0x000DFEDC
		[__DynamicallyInvokable]
		public void InsertRange(int index, IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count > 0)
				{
					this.EnsureCapacity(this._size + count);
					if (index < this._size)
					{
						Array.Copy(this._items, index, this._items, index + count, this._size - index);
					}
					if (this == collection2)
					{
						Array.Copy(this._items, 0, this._items, index, index);
						Array.Copy(this._items, index + count, this._items, index * 2, this._size - index);
					}
					else
					{
						T[] array = new T[count];
						collection2.CopyTo(array, 0);
						array.CopyTo(this._items, index);
					}
					this._size += count;
				}
			}
			else
			{
				foreach (T t in collection)
				{
					this.Insert(index++, t);
				}
			}
			this._version++;
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the entire <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1" />. The value can be <see langword="null" /> for reference types.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="item" /> within the entire the <see cref="T:System.Collections.Generic.List`1" />, if found; otherwise, -1.</returns>
		// Token: 0x06003B39 RID: 15161 RVA: 0x000E1E08 File Offset: 0x000E0008
		[__DynamicallyInvokable]
		public int LastIndexOf(T item)
		{
			if (this._size == 0)
			{
				return -1;
			}
			return this.LastIndexOf(item, this._size - 1, this._size);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1" /> that extends from the first element to the specified index.</summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1" />. The value can be <see langword="null" /> for reference types.</param>
		/// <param name="index">The zero-based starting index of the backward search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="item" /> within the range of elements in the <see cref="T:System.Collections.Generic.List`1" /> that extends from the first element to <paramref name="index" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1" />.</exception>
		// Token: 0x06003B3A RID: 15162 RVA: 0x000E1E29 File Offset: 0x000E0029
		[__DynamicallyInvokable]
		public int LastIndexOf(T item, int index)
		{
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			return this.LastIndexOf(item, index, index + 1);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.Generic.List`1" /> that contains the specified number of elements and ends at the specified index.</summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1" />. The value can be <see langword="null" /> for reference types.</param>
		/// <param name="index">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="item" /> within the range of elements in the <see cref="T:System.Collections.Generic.List`1" /> that contains <paramref name="count" /> number of elements and ends at <paramref name="index" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.Generic.List`1" />.  
		/// -or-  
		/// <paramref name="count" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.Generic.List`1" />.</exception>
		// Token: 0x06003B3B RID: 15163 RVA: 0x000E1E48 File Offset: 0x000E0048
		[__DynamicallyInvokable]
		public int LastIndexOf(T item, int index, int count)
		{
			if (this.Count != 0 && index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this.Count != 0 && count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size == 0)
			{
				return -1;
			}
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
			}
			if (count > index + 1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
			}
			return Array.LastIndexOf<T>(this._items, item, index, count);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.List`1" />. The value can be <see langword="null" /> for reference types.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> is successfully removed; otherwise, <see langword="false" />.  This method also returns <see langword="false" /> if <paramref name="item" /> was not found in the <see cref="T:System.Collections.Generic.List`1" />.</returns>
		// Token: 0x06003B3C RID: 15164 RVA: 0x000E1EB8 File Offset: 0x000E00B8
		[__DynamicallyInvokable]
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="item">The object to remove from the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="item" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x06003B3D RID: 15165 RVA: 0x000E1EDB File Offset: 0x000E00DB
		[__DynamicallyInvokable]
		void IList.Remove(object item)
		{
			if (List<T>.IsCompatibleObject(item))
			{
				this.Remove((T)((object)item));
			}
		}

		/// <summary>Removes all the elements that match the conditions defined by the specified predicate.</summary>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the elements to remove.</param>
		/// <returns>The number of elements removed from the <see cref="T:System.Collections.Generic.List`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x06003B3E RID: 15166 RVA: 0x000E1EF4 File Offset: 0x000E00F4
		[__DynamicallyInvokable]
		public int RemoveAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int num = 0;
			while (num < this._size && !match(this._items[num]))
			{
				num++;
			}
			if (num >= this._size)
			{
				return 0;
			}
			int i = num + 1;
			while (i < this._size)
			{
				while (i < this._size && match(this._items[i]))
				{
					i++;
				}
				if (i < this._size)
				{
					this._items[num++] = this._items[i++];
				}
			}
			Array.Clear(this._items, num, this._size - num);
			int num2 = this._size - num;
			this._size = num;
			this._version++;
			return num2;
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Generic.List`1.Count" />.</exception>
		// Token: 0x06003B3F RID: 15167 RVA: 0x000E1FC8 File Offset: 0x000E01C8
		[__DynamicallyInvokable]
		public void RemoveAt(int index)
		{
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = default(T);
			this._version++;
		}

		/// <summary>Removes a range of elements from the <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <param name="index">The zero-based starting index of the range of elements to remove.</param>
		/// <param name="count">The number of elements to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.Generic.List`1" />.</exception>
		// Token: 0x06003B40 RID: 15168 RVA: 0x000E2040 File Offset: 0x000E0240
		[__DynamicallyInvokable]
		public void RemoveRange(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (count > 0)
			{
				int size = this._size;
				this._size -= count;
				if (index < this._size)
				{
					Array.Copy(this._items, index + count, this._items, index, this._size - index);
				}
				Array.Clear(this._items, this._size, count);
				this._version++;
			}
		}

		/// <summary>Reverses the order of the elements in the entire <see cref="T:System.Collections.Generic.List`1" />.</summary>
		// Token: 0x06003B41 RID: 15169 RVA: 0x000E20D6 File Offset: 0x000E02D6
		[__DynamicallyInvokable]
		public void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		/// <summary>Reverses the order of the elements in the specified range.</summary>
		/// <param name="index">The zero-based starting index of the range to reverse.</param>
		/// <param name="count">The number of elements in the range to reverse.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.Generic.List`1" />.</exception>
		// Token: 0x06003B42 RID: 15170 RVA: 0x000E20E8 File Offset: 0x000E02E8
		[__DynamicallyInvokable]
		public void Reverse(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			Array.Reverse(this._items, index, count);
			this._version++;
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.Generic.List`1" /> using the default comparer.</summary>
		/// <exception cref="T:System.InvalidOperationException">The default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" /> cannot find an implementation of the <see cref="T:System.IComparable`1" /> generic interface or the <see cref="T:System.IComparable" /> interface for type <paramref name="T" />.</exception>
		// Token: 0x06003B43 RID: 15171 RVA: 0x000E213A File Offset: 0x000E033A
		[__DynamicallyInvokable]
		public void Sort()
		{
			this.Sort(0, this.Count, null);
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.Generic.List`1" /> using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing elements, or <see langword="null" /> to use the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" />.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" /> cannot find implementation of the <see cref="T:System.IComparable`1" /> generic interface or the <see cref="T:System.IComparable" /> interface for type <paramref name="T" />.</exception>
		/// <exception cref="T:System.ArgumentException">The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		// Token: 0x06003B44 RID: 15172 RVA: 0x000E214A File Offset: 0x000E034A
		[__DynamicallyInvokable]
		public void Sort(IComparer<T> comparer)
		{
			this.Sort(0, this.Count, comparer);
		}

		/// <summary>Sorts the elements in a range of elements in <see cref="T:System.Collections.Generic.List`1" /> using the specified comparer.</summary>
		/// <param name="index">The zero-based starting index of the range to sort.</param>
		/// <param name="count">The length of the range to sort.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing elements, or <see langword="null" /> to use the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the <see cref="T:System.Collections.Generic.List`1" />.  
		/// -or-  
		/// The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" />, and the default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default" /> cannot find implementation of the <see cref="T:System.IComparable`1" /> generic interface or the <see cref="T:System.IComparable" /> interface for type <paramref name="T" />.</exception>
		// Token: 0x06003B45 RID: 15173 RVA: 0x000E215C File Offset: 0x000E035C
		[__DynamicallyInvokable]
		public void Sort(int index, int count, IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			Array.Sort<T>(this._items, index, count, comparer);
			this._version++;
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.Generic.List`1" /> using the specified <see cref="T:System.Comparison`1" />.</summary>
		/// <param name="comparison">The <see cref="T:System.Comparison`1" /> to use when comparing elements.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparison" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The implementation of <paramref name="comparison" /> caused an error during the sort. For example, <paramref name="comparison" /> might not return 0 when comparing an item with itself.</exception>
		// Token: 0x06003B46 RID: 15174 RVA: 0x000E21B0 File Offset: 0x000E03B0
		[__DynamicallyInvokable]
		public void Sort(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			if (this._size > 0)
			{
				IComparer<T> comparer = new Array.FunctorComparer<T>(comparison);
				Array.Sort<T>(this._items, 0, this._size, comparer);
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.List`1" /> to a new array.</summary>
		/// <returns>An array containing copies of the elements of the <see cref="T:System.Collections.Generic.List`1" />.</returns>
		// Token: 0x06003B47 RID: 15175 RVA: 0x000E21EC File Offset: 0x000E03EC
		[__DynamicallyInvokable]
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Generic.List`1" />, if that number is less than a threshold value.</summary>
		// Token: 0x06003B48 RID: 15176 RVA: 0x000E221C File Offset: 0x000E041C
		[__DynamicallyInvokable]
		public void TrimExcess()
		{
			int num = (int)((double)this._items.Length * 0.9);
			if (this._size < num)
			{
				this.Capacity = this._size;
			}
		}

		/// <summary>Determines whether every element in the <see cref="T:System.Collections.Generic.List`1" /> matches the conditions defined by the specified predicate.</summary>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions to check against the elements.</param>
		/// <returns>
		///   <see langword="true" /> if every element in the <see cref="T:System.Collections.Generic.List`1" /> matches the conditions defined by the specified predicate; otherwise, <see langword="false" />. If the list has no elements, the return value is <see langword="true" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x06003B49 RID: 15177 RVA: 0x000E2254 File Offset: 0x000E0454
		[__DynamicallyInvokable]
		public bool TrueForAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < this._size; i++)
			{
				if (!match(this._items[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x000E2292 File Offset: 0x000E0492
		internal static IList<T> Synchronized(List<T> list)
		{
			return new List<T>.SynchronizedList(list);
		}

		// Token: 0x04001962 RID: 6498
		private const int _defaultCapacity = 4;

		// Token: 0x04001963 RID: 6499
		private T[] _items;

		// Token: 0x04001964 RID: 6500
		private int _size;

		// Token: 0x04001965 RID: 6501
		private int _version;

		// Token: 0x04001966 RID: 6502
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04001967 RID: 6503
		private static readonly T[] _emptyArray = new T[0];

		// Token: 0x02000BDE RID: 3038
		[Serializable]
		internal class SynchronizedList : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x06006F3E RID: 28478 RVA: 0x001803D8 File Offset: 0x0017E5D8
			internal SynchronizedList(List<T> list)
			{
				this._list = list;
				this._root = ((ICollection)list).SyncRoot;
			}

			// Token: 0x17001316 RID: 4886
			// (get) Token: 0x06006F3F RID: 28479 RVA: 0x001803F4 File Offset: 0x0017E5F4
			public int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x17001317 RID: 4887
			// (get) Token: 0x06006F40 RID: 28480 RVA: 0x0018043C File Offset: 0x0017E63C
			public bool IsReadOnly
			{
				get
				{
					return ((ICollection<T>)this._list).IsReadOnly;
				}
			}

			// Token: 0x06006F41 RID: 28481 RVA: 0x0018044C File Offset: 0x0017E64C
			public void Add(T item)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Add(item);
				}
			}

			// Token: 0x06006F42 RID: 28482 RVA: 0x00180494 File Offset: 0x0017E694
			public void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06006F43 RID: 28483 RVA: 0x001804DC File Offset: 0x0017E6DC
			public bool Contains(T item)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(item);
				}
				return flag2;
			}

			// Token: 0x06006F44 RID: 28484 RVA: 0x00180524 File Offset: 0x0017E724
			public void CopyTo(T[] array, int arrayIndex)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06006F45 RID: 28485 RVA: 0x0018056C File Offset: 0x0017E76C
			public bool Remove(T item)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Remove(item);
				}
				return flag2;
			}

			// Token: 0x06006F46 RID: 28486 RVA: 0x001805B4 File Offset: 0x0017E7B4
			IEnumerator IEnumerable.GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006F47 RID: 28487 RVA: 0x00180600 File Offset: 0x0017E800
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				object root = this._root;
				IEnumerator<T> enumerator;
				lock (root)
				{
					enumerator = ((IEnumerable<T>)this._list).GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x17001318 RID: 4888
			public T this[int index]
			{
				get
				{
					object root = this._root;
					T t;
					lock (root)
					{
						t = this._list[index];
					}
					return t;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[index] = value;
					}
				}
			}

			// Token: 0x06006F4A RID: 28490 RVA: 0x001806D8 File Offset: 0x0017E8D8
			public int IndexOf(T item)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(item);
				}
				return num;
			}

			// Token: 0x06006F4B RID: 28491 RVA: 0x00180720 File Offset: 0x0017E920
			public void Insert(int index, T item)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, item);
				}
			}

			// Token: 0x06006F4C RID: 28492 RVA: 0x00180768 File Offset: 0x0017E968
			public void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x040035F5 RID: 13813
			private List<T> _list;

			// Token: 0x040035F6 RID: 13814
			private object _root;
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.List`1" />.</summary>
		/// <typeparam name="T" />
		// Token: 0x02000BDF RID: 3039
		[__DynamicallyInvokable]
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06006F4D RID: 28493 RVA: 0x001807B0 File Offset: 0x0017E9B0
			internal Enumerator(List<T> list)
			{
				this.list = list;
				this.index = 0;
				this.version = list._version;
				this.current = default(T);
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.List`1.Enumerator" />.</summary>
			// Token: 0x06006F4E RID: 28494 RVA: 0x001807D8 File Offset: 0x0017E9D8
			[__DynamicallyInvokable]
			public void Dispose()
			{
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.List`1" />.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x06006F4F RID: 28495 RVA: 0x001807DC File Offset: 0x0017E9DC
			[__DynamicallyInvokable]
			public bool MoveNext()
			{
				List<T> list = this.list;
				if (this.version == list._version && this.index < list._size)
				{
					this.current = list._items[this.index];
					this.index++;
					return true;
				}
				return this.MoveNextRare();
			}

			// Token: 0x06006F50 RID: 28496 RVA: 0x00180839 File Offset: 0x0017EA39
			private bool MoveNextRare()
			{
				if (this.version != this.list._version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = this.list._size + 1;
				this.current = default(T);
				return false;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.List`1" /> at the current position of the enumerator.</returns>
			// Token: 0x17001319 RID: 4889
			// (get) Token: 0x06006F51 RID: 28497 RVA: 0x00180875 File Offset: 0x0017EA75
			[__DynamicallyInvokable]
			public T Current
			{
				[__DynamicallyInvokable]
				get
				{
					return this.current;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.List`1" /> at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x1700131A RID: 4890
			// (get) Token: 0x06006F52 RID: 28498 RVA: 0x0018087D File Offset: 0x0017EA7D
			[__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.index == 0 || this.index == this.list._size + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x06006F53 RID: 28499 RVA: 0x001808AE File Offset: 0x0017EAAE
			[__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				if (this.version != this.list._version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = 0;
				this.current = default(T);
			}

			// Token: 0x040035F7 RID: 13815
			private List<T> list;

			// Token: 0x040035F8 RID: 13816
			private int index;

			// Token: 0x040035F9 RID: 13817
			private int version;

			// Token: 0x040035FA RID: 13818
			private T current;
		}
	}
}
