using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	/// <summary>Implements the <see cref="T:System.Collections.IList" /> interface using an array whose size is dynamically increased as required.</summary>
	// Token: 0x0200048E RID: 1166
	[DebuggerTypeProxy(typeof(ArrayList.ArrayListDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	[Serializable]
	public class ArrayList : IList, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x060037E6 RID: 14310 RVA: 0x000D7B00 File Offset: 0x000D5D00
		internal ArrayList(bool trash)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x060037E7 RID: 14311 RVA: 0x000D7B08 File Offset: 0x000D5D08
		public ArrayList()
		{
			this._items = ArrayList.emptyArray;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that is empty and has the specified initial capacity.</summary>
		/// <param name="capacity">The number of elements that the new list can initially store.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x060037E8 RID: 14312 RVA: 0x000D7B1C File Offset: 0x000D5D1C
		public ArrayList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", new object[] { "capacity" }));
			}
			if (capacity == 0)
			{
				this._items = ArrayList.emptyArray;
				return;
			}
			this._items = new object[capacity];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that contains elements copied from the specified collection and that has the same initial capacity as the number of elements copied.</summary>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements are copied to the new list.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is <see langword="null" />.</exception>
		// Token: 0x060037E9 RID: 14313 RVA: 0x000D7B74 File Offset: 0x000D5D74
		public ArrayList(ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
			}
			int count = c.Count;
			if (count == 0)
			{
				this._items = ArrayList.emptyArray;
				return;
			}
			this._items = new object[count];
			this.AddRange(c);
		}

		/// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.ArrayList" /> can contain.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.ArrayList" /> can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.ArrayList.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060037EA RID: 14314 RVA: 0x000D7BC8 File Offset: 0x000D5DC8
		// (set) Token: 0x060037EB RID: 14315 RVA: 0x000D7BD4 File Offset: 0x000D5DD4
		public virtual int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						object[] array = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = new object[4];
				}
			}
		}

		/// <summary>Gets the number of elements actually contained in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>The number of elements actually contained in the <see cref="T:System.Collections.ArrayList" />.</returns>
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060037EC RID: 14316 RVA: 0x000D7C46 File Offset: 0x000D5E46
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.ArrayList" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.ArrayList" /> has a fixed size; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060037ED RID: 14317 RVA: 0x000D7C4E File Offset: 0x000D5E4E
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.ArrayList" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.ArrayList" /> is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060037EE RID: 14318 RVA: 0x000D7C51 File Offset: 0x000D5E51
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ArrayList" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ArrayList" /> is synchronized (thread safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060037EF RID: 14319 RVA: 0x000D7C54 File Offset: 0x000D5E54
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ArrayList" />.</returns>
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x060037F0 RID: 14320 RVA: 0x000D7C57 File Offset: 0x000D5E57
		public virtual object SyncRoot
		{
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
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		// Token: 0x17000843 RID: 2115
		public virtual object this[int index]
		{
			get
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				return this._items[index];
			}
			set
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				this._items[index] = value;
				this._version++;
			}
		}

		/// <summary>Creates an <see cref="T:System.Collections.ArrayList" /> wrapper for a specific <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to wrap.</param>
		/// <returns>The <see cref="T:System.Collections.ArrayList" /> wrapper around the <see cref="T:System.Collections.IList" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x060037F3 RID: 14323 RVA: 0x000D7CE0 File Offset: 0x000D5EE0
		public static ArrayList Adapter(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.IListWrapper(list);
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <returns>The <see cref="T:System.Collections.ArrayList" /> index at which the <paramref name="value" /> has been added.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x060037F4 RID: 14324 RVA: 0x000D7CF8 File Offset: 0x000D5EF8
		public virtual int Add(object value)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			this._items[this._size] = value;
			this._version++;
			int size = this._size;
			this._size = size + 1;
			return size;
		}

		/// <summary>Adds the elements of an <see cref="T:System.Collections.ICollection" /> to the end of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements should be added to the end of the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be <see langword="null" />, but it can contain elements that are <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x060037F5 RID: 14325 RVA: 0x000D7D50 File Offset: 0x000D5F50
		public virtual void AddRange(ICollection c)
		{
			this.InsertRange(this._size, c);
		}

		/// <summary>Searches a range of elements in the sorted <see cref="T:System.Collections.ArrayList" /> for an element using the specified comparer and returns the zero-based index of the element.</summary>
		/// <param name="index">The zero-based starting index of the range to search.</param>
		/// <param name="count">The length of the range to search.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be <see langword="null" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer that is the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in the <see cref="T:System.Collections.ArrayList" />.  
		/// -or-  
		/// <paramref name="comparer" /> is <see langword="null" /> and neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" /> and <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		// Token: 0x060037F6 RID: 14326 RVA: 0x000D7D60 File Offset: 0x000D5F60
		public virtual int BinarySearch(int index, int count, object value, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			return Array.BinarySearch(this._items, index, count, value, comparer);
		}

		/// <summary>Searches the entire sorted <see cref="T:System.Collections.ArrayList" /> for an element using the default comparer and returns the zero-based index of the element.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x060037F7 RID: 14327 RVA: 0x000D7DCA File Offset: 0x000D5FCA
		public virtual int BinarySearch(object value)
		{
			return this.BinarySearch(0, this.Count, value, null);
		}

		/// <summary>Searches the entire sorted <see cref="T:System.Collections.ArrayList" /> for an element using the specified comparer and returns the zero-based index of the element.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be <see langword="null" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer that is the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparer" /> is <see langword="null" /> and neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is <see langword="null" /> and <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x060037F8 RID: 14328 RVA: 0x000D7DDB File Offset: 0x000D5FDB
		public virtual int BinarySearch(object value, IComparer comparer)
		{
			return this.BinarySearch(0, this.Count, value, comparer);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x060037F9 RID: 14329 RVA: 0x000D7DEC File Offset: 0x000D5FEC
		public virtual void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.ArrayList" />.</returns>
		// Token: 0x060037FA RID: 14330 RVA: 0x000D7E20 File Offset: 0x000D6020
		public virtual object Clone()
		{
			ArrayList arrayList = new ArrayList(this._size);
			arrayList._size = this._size;
			arrayList._version = this._version;
			Array.Copy(this._items, 0, arrayList._items, 0, this._size);
			return arrayList;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="item">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> is found in the <see cref="T:System.Collections.ArrayList" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060037FB RID: 14331 RVA: 0x000D7E6C File Offset: 0x000D606C
		public virtual bool Contains(object item)
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
			for (int j = 0; j < this._size; j++)
			{
				if (this._items[j] != null && this._items[j].Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the beginning of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.ArrayList" /> is greater than the number of elements that the destination <paramref name="array" /> can contain.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060037FC RID: 14332 RVA: 0x000D7EC9 File Offset: 0x000D60C9
		public virtual void CopyTo(Array array)
		{
			this.CopyTo(array, 0);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.ArrayList" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060037FD RID: 14333 RVA: 0x000D7ED3 File Offset: 0x000D60D3
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		/// <summary>Copies a range of elements from the <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="index">The zero-based index in the source <see cref="T:System.Collections.ArrayList" /> at which copying begins.</param>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <param name="count">The number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="arrayIndex" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the <see cref="P:System.Collections.ArrayList.Count" /> of the source <see cref="T:System.Collections.ArrayList" />.  
		/// -or-  
		/// The number of elements from <paramref name="index" /> to the end of the source <see cref="T:System.Collections.ArrayList" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060037FE RID: 14334 RVA: 0x000D7F08 File Offset: 0x000D6108
		public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x000D7F60 File Offset: 0x000D6160
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

		/// <summary>Returns an <see cref="T:System.Collections.IList" /> wrapper with a fixed size.</summary>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to wrap.</param>
		/// <returns>An <see cref="T:System.Collections.IList" /> wrapper with a fixed size.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x06003800 RID: 14336 RVA: 0x000D7FAA File Offset: 0x000D61AA
		public static IList FixedSize(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.FixedSizeList(list);
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> wrapper with a fixed size.</summary>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to wrap.</param>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> wrapper with a fixed size.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x06003801 RID: 14337 RVA: 0x000D7FC0 File Offset: 0x000D61C0
		public static ArrayList FixedSize(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.FixedSizeArrayList(list);
		}

		/// <summary>Returns an enumerator for the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire <see cref="T:System.Collections.ArrayList" />.</returns>
		// Token: 0x06003802 RID: 14338 RVA: 0x000D7FD6 File Offset: 0x000D61D6
		public virtual IEnumerator GetEnumerator()
		{
			return new ArrayList.ArrayListEnumeratorSimple(this);
		}

		/// <summary>Returns an enumerator for a range of elements in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based starting index of the <see cref="T:System.Collections.ArrayList" /> section that the enumerator should refer to.</param>
		/// <param name="count">The number of elements in the <see cref="T:System.Collections.ArrayList" /> section that the enumerator should refer to.</param>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the specified range of elements in the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x06003803 RID: 14339 RVA: 0x000D7FE0 File Offset: 0x000D61E0
		public virtual IEnumerator GetEnumerator(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			return new ArrayList.ArrayListEnumerator(this, index, count);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.ArrayList" />, if found; otherwise, -1.</returns>
		// Token: 0x06003804 RID: 14340 RVA: 0x000D8042 File Offset: 0x000D6242
		public virtual int IndexOf(object value)
		{
			return Array.IndexOf(this._items, value, 0, this._size);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from the specified index to the last element.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from <paramref name="startIndex" /> to the last element, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x06003805 RID: 14341 RVA: 0x000D8057 File Offset: 0x000D6257
		public virtual int IndexOf(object value, int startIndex)
		{
			if (startIndex > this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return Array.IndexOf(this._items, value, startIndex, this._size - startIndex);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that starts at the specified index and contains the specified number of elements.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that starts at <paramref name="startIndex" /> and contains <paramref name="count" /> number of elements, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x06003806 RID: 14342 RVA: 0x000D808C File Offset: 0x000D628C
		public virtual int IndexOf(object value, int startIndex, int count)
		{
			if (startIndex > this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || startIndex > this._size - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			return Array.IndexOf(this._items, value, startIndex, count);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.ArrayList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x06003807 RID: 14343 RVA: 0x000D80EC File Offset: 0x000D62EC
		public virtual void Insert(int index, object value)
		{
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_ArrayListInsert"));
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = value;
			this._size++;
			this._version++;
		}

		/// <summary>Inserts the elements of a collection into the <see cref="T:System.Collections.ArrayList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which the new elements should be inserted.</param>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements should be inserted into the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be <see langword="null" />, but it can contain elements that are <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x06003808 RID: 14344 RVA: 0x000D8184 File Offset: 0x000D6384
		public virtual void InsertRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
			}
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			int count = c.Count;
			if (count > 0)
			{
				this.EnsureCapacity(this._size + count);
				if (index < this._size)
				{
					Array.Copy(this._items, index, this._items, index + count, this._size - index);
				}
				object[] array = new object[count];
				c.CopyTo(array, 0);
				array.CopyTo(this._items, index);
				this._size += count;
				this._version++;
			}
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the last occurrence within the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the entire the <see cref="T:System.Collections.ArrayList" />, if found; otherwise, -1.</returns>
		// Token: 0x06003809 RID: 14345 RVA: 0x000D8242 File Offset: 0x000D6442
		public virtual int LastIndexOf(object value)
		{
			return this.LastIndexOf(value, this._size - 1, this._size);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from the first element to the specified index.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from the first element to <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x0600380A RID: 14346 RVA: 0x000D8259 File Offset: 0x000D6459
		public virtual int LastIndexOf(object value, int startIndex)
		{
			if (startIndex >= this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return this.LastIndexOf(value, startIndex, startIndex + 1);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that contains the specified number of elements and ends at the specified index.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that contains <paramref name="count" /> number of elements and ends at <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x0600380B RID: 14347 RVA: 0x000D8284 File Offset: 0x000D6484
		public virtual int LastIndexOf(object value, int startIndex, int count)
		{
			if (this.Count != 0 && (startIndex < 0 || count < 0))
			{
				throw new ArgumentOutOfRangeException((startIndex < 0) ? "startIndex" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size == 0)
			{
				return -1;
			}
			if (startIndex >= this._size || count > startIndex + 1)
			{
				throw new ArgumentOutOfRangeException((startIndex >= this._size) ? "startIndex" : "count", Environment.GetResourceString("ArgumentOutOfRange_BiggerThanCollection"));
			}
			return Array.LastIndexOf(this._items, value, startIndex, count);
		}

		/// <summary>Returns a read-only <see cref="T:System.Collections.IList" /> wrapper.</summary>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to wrap.</param>
		/// <returns>A read-only <see cref="T:System.Collections.IList" /> wrapper around <paramref name="list" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x0600380C RID: 14348 RVA: 0x000D830D File Offset: 0x000D650D
		public static IList ReadOnly(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.ReadOnlyList(list);
		}

		/// <summary>Returns a read-only <see cref="T:System.Collections.ArrayList" /> wrapper.</summary>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to wrap.</param>
		/// <returns>A read-only <see cref="T:System.Collections.ArrayList" /> wrapper around <paramref name="list" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x0600380D RID: 14349 RVA: 0x000D8323 File Offset: 0x000D6523
		public static ArrayList ReadOnly(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.ReadOnlyArrayList(list);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x0600380E RID: 14350 RVA: 0x000D833C File Offset: 0x000D653C
		public virtual void Remove(object obj)
		{
			int num = this.IndexOf(obj);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x0600380F RID: 14351 RVA: 0x000D835C File Offset: 0x000D655C
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = null;
			this._version++;
		}

		/// <summary>Removes a range of elements from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based starting index of the range of elements to remove.</param>
		/// <param name="count">The number of elements to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x06003810 RID: 14352 RVA: 0x000D83DC File Offset: 0x000D65DC
		public virtual void RemoveRange(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (count > 0)
			{
				int i = this._size;
				this._size -= count;
				if (index < this._size)
				{
					Array.Copy(this._items, index + count, this._items, index, this._size - index);
				}
				while (i > this._size)
				{
					this._items[--i] = null;
				}
				this._version++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> whose elements are copies of the specified value.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to copy multiple times in the new <see cref="T:System.Collections.ArrayList" />. The value can be <see langword="null" />.</param>
		/// <param name="count">The number of times <paramref name="value" /> should be copied.</param>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> with <paramref name="count" /> number of elements, all of which are copies of <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.</exception>
		// Token: 0x06003811 RID: 14353 RVA: 0x000D849C File Offset: 0x000D669C
		public static ArrayList Repeat(object value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			ArrayList arrayList = new ArrayList((count > 4) ? count : 4);
			for (int i = 0; i < count; i++)
			{
				arrayList.Add(value);
			}
			return arrayList;
		}

		/// <summary>Reverses the order of the elements in the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.</exception>
		// Token: 0x06003812 RID: 14354 RVA: 0x000D84E5 File Offset: 0x000D66E5
		public virtual void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		/// <summary>Reverses the order of the elements in the specified range.</summary>
		/// <param name="index">The zero-based starting index of the range to reverse.</param>
		/// <param name="count">The number of elements in the range to reverse.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.</exception>
		// Token: 0x06003813 RID: 14355 RVA: 0x000D84F4 File Offset: 0x000D66F4
		public virtual void Reverse(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			Array.Reverse(this._items, index, count);
			this._version++;
		}

		/// <summary>Copies the elements of a collection over a range of elements in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based <see cref="T:System.Collections.ArrayList" /> index at which to start copying the elements of <paramref name="c" />.</param>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements to copy to the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be <see langword="null" />, but it can contain elements that are <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> plus the number of elements in <paramref name="c" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.</exception>
		// Token: 0x06003814 RID: 14356 RVA: 0x000D856C File Offset: 0x000D676C
		public virtual void SetRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
			}
			int count = c.Count;
			if (index < 0 || index > this._size - count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count > 0)
			{
				c.CopyTo(this._items, index);
				this._version++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> which represents a subset of the elements in the source <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based <see cref="T:System.Collections.ArrayList" /> index at which the range starts.</param>
		/// <param name="count">The number of elements in the range.</param>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> which represents a subset of the elements in the source <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />.</exception>
		// Token: 0x06003815 RID: 14357 RVA: 0x000D85DC File Offset: 0x000D67DC
		public virtual ArrayList GetRange(int index, int count)
		{
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			return new ArrayList.Range(this, index, count);
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.</exception>
		// Token: 0x06003816 RID: 14358 RVA: 0x000D8634 File Offset: 0x000D6834
		public virtual void Sort()
		{
			this.Sort(0, this.Count, Comparer.Default);
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.ArrayList" /> using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic) to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.</exception>
		/// <exception cref="T:System.InvalidOperationException">An error occurred while comparing two elements.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see langword="null" /> is passed for <paramref name="comparer" />, and the elements in the list do not implement <see cref="T:System.IComparable" />.</exception>
		// Token: 0x06003817 RID: 14359 RVA: 0x000D8648 File Offset: 0x000D6848
		public virtual void Sort(IComparer comparer)
		{
			this.Sort(0, this.Count, comparer);
		}

		/// <summary>Sorts the elements in a range of elements in <see cref="T:System.Collections.ArrayList" /> using the specified comparer.</summary>
		/// <param name="index">The zero-based starting index of the range to sort.</param>
		/// <param name="count">The length of the range to sort.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic) to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the <see cref="T:System.Collections.ArrayList" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.</exception>
		/// <exception cref="T:System.InvalidOperationException">An error occurred while comparing two elements.</exception>
		// Token: 0x06003818 RID: 14360 RVA: 0x000D8658 File Offset: 0x000D6858
		public virtual void Sort(int index, int count, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			Array.Sort(this._items, index, count, comparer);
			this._version++;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IList" /> wrapper that is synchronized (thread safe).</summary>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to synchronize.</param>
		/// <returns>An <see cref="T:System.Collections.IList" /> wrapper that is synchronized (thread safe).</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x06003819 RID: 14361 RVA: 0x000D86CE File Offset: 0x000D68CE
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static IList Synchronized(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncIList(list);
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> wrapper that is synchronized (thread safe).</summary>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to synchronize.</param>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> wrapper that is synchronized (thread safe).</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x0600381A RID: 14362 RVA: 0x000D86E4 File Offset: 0x000D68E4
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static ArrayList Synchronized(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncArrayList(list);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ArrayList" /> to a new <see cref="T:System.Object" /> array.</summary>
		/// <returns>An <see cref="T:System.Object" /> array containing copies of the elements of the <see cref="T:System.Collections.ArrayList" />.</returns>
		// Token: 0x0600381B RID: 14363 RVA: 0x000D86FC File Offset: 0x000D68FC
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ArrayList" /> to a new array of the specified element type.</summary>
		/// <param name="type">The element <see cref="T:System.Type" /> of the destination array to create and copy elements to.</param>
		/// <returns>An array of the specified element type containing copies of the elements of the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the specified type.</exception>
		// Token: 0x0600381C RID: 14364 RVA: 0x000D872C File Offset: 0x000D692C
		[SecuritySafeCritical]
		public virtual Array ToArray(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Array array = Array.UnsafeCreateInstance(type, this._size);
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.ArrayList" /> has a fixed size.</exception>
		// Token: 0x0600381D RID: 14365 RVA: 0x000D876F File Offset: 0x000D696F
		public virtual void TrimToSize()
		{
			this.Capacity = this._size;
		}

		// Token: 0x040018CC RID: 6348
		private object[] _items;

		// Token: 0x040018CD RID: 6349
		private int _size;

		// Token: 0x040018CE RID: 6350
		private int _version;

		// Token: 0x040018CF RID: 6351
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040018D0 RID: 6352
		private const int _defaultCapacity = 4;

		// Token: 0x040018D1 RID: 6353
		private static readonly object[] emptyArray = EmptyArray<object>.Value;

		// Token: 0x02000B9E RID: 2974
		[Serializable]
		private class IListWrapper : ArrayList
		{
			// Token: 0x06006CD8 RID: 27864 RVA: 0x00179D90 File Offset: 0x00177F90
			internal IListWrapper(IList list)
			{
				this._list = list;
				this._version = 0;
			}

			// Token: 0x17001267 RID: 4711
			// (get) Token: 0x06006CD9 RID: 27865 RVA: 0x00179DA6 File Offset: 0x00177FA6
			// (set) Token: 0x06006CDA RID: 27866 RVA: 0x00179DB3 File Offset: 0x00177FB3
			public override int Capacity
			{
				get
				{
					return this._list.Count;
				}
				set
				{
					if (value < this.Count)
					{
						throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
					}
				}
			}

			// Token: 0x17001268 RID: 4712
			// (get) Token: 0x06006CDB RID: 27867 RVA: 0x00179DD3 File Offset: 0x00177FD3
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17001269 RID: 4713
			// (get) Token: 0x06006CDC RID: 27868 RVA: 0x00179DE0 File Offset: 0x00177FE0
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x1700126A RID: 4714
			// (get) Token: 0x06006CDD RID: 27869 RVA: 0x00179DED File Offset: 0x00177FED
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x1700126B RID: 4715
			// (get) Token: 0x06006CDE RID: 27870 RVA: 0x00179DFA File Offset: 0x00177FFA
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x1700126C RID: 4716
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
					this._version++;
				}
			}

			// Token: 0x1700126D RID: 4717
			// (get) Token: 0x06006CE1 RID: 27873 RVA: 0x00179E32 File Offset: 0x00178032
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06006CE2 RID: 27874 RVA: 0x00179E40 File Offset: 0x00178040
			public override int Add(object obj)
			{
				int num = this._list.Add(obj);
				this._version++;
				return num;
			}

			// Token: 0x06006CE3 RID: 27875 RVA: 0x00179E69 File Offset: 0x00178069
			public override void AddRange(ICollection c)
			{
				this.InsertRange(this.Count, c);
			}

			// Token: 0x06006CE4 RID: 27876 RVA: 0x00179E78 File Offset: 0x00178078
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				int i = index;
				int num = index + count - 1;
				while (i <= num)
				{
					int num2 = (i + num) / 2;
					int num3 = comparer.Compare(value, this._list[num2]);
					if (num3 == 0)
					{
						return num2;
					}
					if (num3 < 0)
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
				return ~i;
			}

			// Token: 0x06006CE5 RID: 27877 RVA: 0x00179F11 File Offset: 0x00178111
			public override void Clear()
			{
				if (this._list.IsFixedSize)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
				}
				this._list.Clear();
				this._version++;
			}

			// Token: 0x06006CE6 RID: 27878 RVA: 0x00179F49 File Offset: 0x00178149
			public override object Clone()
			{
				return new ArrayList.IListWrapper(this._list);
			}

			// Token: 0x06006CE7 RID: 27879 RVA: 0x00179F56 File Offset: 0x00178156
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06006CE8 RID: 27880 RVA: 0x00179F64 File Offset: 0x00178164
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06006CE9 RID: 27881 RVA: 0x00179F74 File Offset: 0x00178174
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0 || arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "arrayIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (array.Length - arrayIndex < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				for (int i = index; i < index + count; i++)
				{
					array.SetValue(this._list[i], arrayIndex++);
				}
			}

			// Token: 0x06006CEA RID: 27882 RVA: 0x0017A04E File Offset: 0x0017824E
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06006CEB RID: 27883 RVA: 0x0017A05C File Offset: 0x0017825C
			public override IEnumerator GetEnumerator(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				return new ArrayList.IListWrapper.IListWrapperEnumWrapper(this, index, count);
			}

			// Token: 0x06006CEC RID: 27884 RVA: 0x0017A0B9 File Offset: 0x001782B9
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06006CED RID: 27885 RVA: 0x0017A0C7 File Offset: 0x001782C7
			public override int IndexOf(object value, int startIndex)
			{
				return this.IndexOf(value, startIndex, this._list.Count - startIndex);
			}

			// Token: 0x06006CEE RID: 27886 RVA: 0x0017A0E0 File Offset: 0x001782E0
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (count < 0 || startIndex > this.Count - count)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				int num = startIndex + count;
				if (value == null)
				{
					for (int i = startIndex; i < num; i++)
					{
						if (this._list[i] == null)
						{
							return i;
						}
					}
					return -1;
				}
				for (int j = startIndex; j < num; j++)
				{
					if (this._list[j] != null && this._list[j].Equals(value))
					{
						return j;
					}
				}
				return -1;
			}

			// Token: 0x06006CEF RID: 27887 RVA: 0x0017A189 File Offset: 0x00178389
			public override void Insert(int index, object obj)
			{
				this._list.Insert(index, obj);
				this._version++;
			}

			// Token: 0x06006CF0 RID: 27888 RVA: 0x0017A1A8 File Offset: 0x001783A8
			public override void InsertRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
				}
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (c.Count > 0)
				{
					ArrayList arrayList = this._list as ArrayList;
					if (arrayList != null)
					{
						arrayList.InsertRange(index, c);
					}
					else
					{
						foreach (object obj in c)
						{
							this._list.Insert(index++, obj);
						}
					}
					this._version++;
				}
			}

			// Token: 0x06006CF1 RID: 27889 RVA: 0x0017A247 File Offset: 0x00178447
			public override int LastIndexOf(object value)
			{
				return this.LastIndexOf(value, this._list.Count - 1, this._list.Count);
			}

			// Token: 0x06006CF2 RID: 27890 RVA: 0x0017A268 File Offset: 0x00178468
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x06006CF3 RID: 27891 RVA: 0x0017A278 File Offset: 0x00178478
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				if (this._list.Count == 0)
				{
					return -1;
				}
				if (startIndex < 0 || startIndex >= this._list.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (count < 0 || count > startIndex + 1)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				int num = startIndex - count + 1;
				if (value == null)
				{
					for (int i = startIndex; i >= num; i--)
					{
						if (this._list[i] == null)
						{
							return i;
						}
					}
					return -1;
				}
				for (int j = startIndex; j >= num; j--)
				{
					if (this._list[j] != null && this._list[j].Equals(value))
					{
						return j;
					}
				}
				return -1;
			}

			// Token: 0x06006CF4 RID: 27892 RVA: 0x0017A334 File Offset: 0x00178534
			public override void Remove(object value)
			{
				int num = this.IndexOf(value);
				if (num >= 0)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06006CF5 RID: 27893 RVA: 0x0017A354 File Offset: 0x00178554
			public override void RemoveAt(int index)
			{
				this._list.RemoveAt(index);
				this._version++;
			}

			// Token: 0x06006CF6 RID: 27894 RVA: 0x0017A370 File Offset: 0x00178570
			public override void RemoveRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				if (count > 0)
				{
					this._version++;
				}
				while (count > 0)
				{
					this._list.RemoveAt(index);
					count--;
				}
			}

			// Token: 0x06006CF7 RID: 27895 RVA: 0x0017A3F0 File Offset: 0x001785F0
			public override void Reverse(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				int i = index;
				int num = index + count - 1;
				while (i < num)
				{
					object obj = this._list[i];
					this._list[i++] = this._list[num];
					this._list[num--] = obj;
				}
				this._version++;
			}

			// Token: 0x06006CF8 RID: 27896 RVA: 0x0017A49C File Offset: 0x0017869C
			public override void SetRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c", Environment.GetResourceString("ArgumentNull_Collection"));
				}
				if (index < 0 || index > this._list.Count - c.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (c.Count > 0)
				{
					foreach (object obj in c)
					{
						this._list[index++] = obj;
					}
					this._version++;
				}
			}

			// Token: 0x06006CF9 RID: 27897 RVA: 0x0017A530 File Offset: 0x00178730
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06006CFA RID: 27898 RVA: 0x0017A590 File Offset: 0x00178790
			public override void Sort(int index, int count, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				object[] array = new object[count];
				this.CopyTo(index, array, 0, count);
				Array.Sort(array, 0, count, comparer);
				for (int i = 0; i < count; i++)
				{
					this._list[i + index] = array[i];
				}
				this._version++;
			}

			// Token: 0x06006CFB RID: 27899 RVA: 0x0017A62C File Offset: 0x0017882C
			public override object[] ToArray()
			{
				object[] array = new object[this.Count];
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06006CFC RID: 27900 RVA: 0x0017A654 File Offset: 0x00178854
			[SecuritySafeCritical]
			public override Array ToArray(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				Array array = Array.UnsafeCreateInstance(type, this._list.Count);
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06006CFD RID: 27901 RVA: 0x0017A695 File Offset: 0x00178895
			public override void TrimToSize()
			{
			}

			// Token: 0x0400353E RID: 13630
			private IList _list;

			// Token: 0x02000CFE RID: 3326
			[Serializable]
			private sealed class IListWrapperEnumWrapper : IEnumerator, ICloneable
			{
				// Token: 0x06007204 RID: 29188 RVA: 0x0018A0DB File Offset: 0x001882DB
				private IListWrapperEnumWrapper()
				{
				}

				// Token: 0x06007205 RID: 29189 RVA: 0x0018A0E4 File Offset: 0x001882E4
				internal IListWrapperEnumWrapper(ArrayList.IListWrapper listWrapper, int startIndex, int count)
				{
					this._en = listWrapper.GetEnumerator();
					this._initialStartIndex = startIndex;
					this._initialCount = count;
					while (startIndex-- > 0 && this._en.MoveNext())
					{
					}
					this._remaining = count;
					this._firstCall = true;
				}

				// Token: 0x06007206 RID: 29190 RVA: 0x0018A138 File Offset: 0x00188338
				public object Clone()
				{
					return new ArrayList.IListWrapper.IListWrapperEnumWrapper
					{
						_en = (IEnumerator)((ICloneable)this._en).Clone(),
						_initialStartIndex = this._initialStartIndex,
						_initialCount = this._initialCount,
						_remaining = this._remaining,
						_firstCall = this._firstCall
					};
				}

				// Token: 0x06007207 RID: 29191 RVA: 0x0018A198 File Offset: 0x00188398
				public bool MoveNext()
				{
					if (this._firstCall)
					{
						this._firstCall = false;
						int num = this._remaining;
						this._remaining = num - 1;
						return num > 0 && this._en.MoveNext();
					}
					if (this._remaining < 0)
					{
						return false;
					}
					bool flag = this._en.MoveNext();
					if (flag)
					{
						int num = this._remaining;
						this._remaining = num - 1;
						return num > 0;
					}
					return false;
				}

				// Token: 0x17001384 RID: 4996
				// (get) Token: 0x06007208 RID: 29192 RVA: 0x0018A206 File Offset: 0x00188406
				public object Current
				{
					get
					{
						if (this._firstCall)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
						}
						if (this._remaining < 0)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
						}
						return this._en.Current;
					}
				}

				// Token: 0x06007209 RID: 29193 RVA: 0x0018A244 File Offset: 0x00188444
				public void Reset()
				{
					this._en.Reset();
					int initialStartIndex = this._initialStartIndex;
					while (initialStartIndex-- > 0 && this._en.MoveNext())
					{
					}
					this._remaining = this._initialCount;
					this._firstCall = true;
				}

				// Token: 0x04003933 RID: 14643
				private IEnumerator _en;

				// Token: 0x04003934 RID: 14644
				private int _remaining;

				// Token: 0x04003935 RID: 14645
				private int _initialStartIndex;

				// Token: 0x04003936 RID: 14646
				private int _initialCount;

				// Token: 0x04003937 RID: 14647
				private bool _firstCall;
			}
		}

		// Token: 0x02000B9F RID: 2975
		[Serializable]
		private class SyncArrayList : ArrayList
		{
			// Token: 0x06006CFE RID: 27902 RVA: 0x0017A697 File Offset: 0x00178897
			internal SyncArrayList(ArrayList list)
				: base(false)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x1700126E RID: 4718
			// (get) Token: 0x06006CFF RID: 27903 RVA: 0x0017A6B4 File Offset: 0x001788B4
			// (set) Token: 0x06006D00 RID: 27904 RVA: 0x0017A6FC File Offset: 0x001788FC
			public override int Capacity
			{
				get
				{
					object root = this._root;
					int capacity;
					lock (root)
					{
						capacity = this._list.Capacity;
					}
					return capacity;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list.Capacity = value;
					}
				}
			}

			// Token: 0x1700126F RID: 4719
			// (get) Token: 0x06006D01 RID: 27905 RVA: 0x0017A744 File Offset: 0x00178944
			public override int Count
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

			// Token: 0x17001270 RID: 4720
			// (get) Token: 0x06006D02 RID: 27906 RVA: 0x0017A78C File Offset: 0x0017898C
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001271 RID: 4721
			// (get) Token: 0x06006D03 RID: 27907 RVA: 0x0017A799 File Offset: 0x00178999
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17001272 RID: 4722
			// (get) Token: 0x06006D04 RID: 27908 RVA: 0x0017A7A6 File Offset: 0x001789A6
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001273 RID: 4723
			public override object this[int index]
			{
				get
				{
					object root = this._root;
					object obj;
					lock (root)
					{
						obj = this._list[index];
					}
					return obj;
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

			// Token: 0x17001274 RID: 4724
			// (get) Token: 0x06006D07 RID: 27911 RVA: 0x0017A83C File Offset: 0x00178A3C
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x06006D08 RID: 27912 RVA: 0x0017A844 File Offset: 0x00178A44
			public override int Add(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.Add(value);
				}
				return num;
			}

			// Token: 0x06006D09 RID: 27913 RVA: 0x0017A88C File Offset: 0x00178A8C
			public override void AddRange(ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.AddRange(c);
				}
			}

			// Token: 0x06006D0A RID: 27914 RVA: 0x0017A8D4 File Offset: 0x00178AD4
			public override int BinarySearch(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.BinarySearch(value);
				}
				return num;
			}

			// Token: 0x06006D0B RID: 27915 RVA: 0x0017A91C File Offset: 0x00178B1C
			public override int BinarySearch(object value, IComparer comparer)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.BinarySearch(value, comparer);
				}
				return num;
			}

			// Token: 0x06006D0C RID: 27916 RVA: 0x0017A968 File Offset: 0x00178B68
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.BinarySearch(index, count, value, comparer);
				}
				return num;
			}

			// Token: 0x06006D0D RID: 27917 RVA: 0x0017A9B4 File Offset: 0x00178BB4
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06006D0E RID: 27918 RVA: 0x0017A9FC File Offset: 0x00178BFC
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = new ArrayList.SyncArrayList((ArrayList)this._list.Clone());
				}
				return obj;
			}

			// Token: 0x06006D0F RID: 27919 RVA: 0x0017AA50 File Offset: 0x00178C50
			public override bool Contains(object item)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(item);
				}
				return flag2;
			}

			// Token: 0x06006D10 RID: 27920 RVA: 0x0017AA98 File Offset: 0x00178C98
			public override void CopyTo(Array array)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array);
				}
			}

			// Token: 0x06006D11 RID: 27921 RVA: 0x0017AAE0 File Offset: 0x00178CE0
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06006D12 RID: 27922 RVA: 0x0017AB28 File Offset: 0x00178D28
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(index, array, arrayIndex, count);
				}
			}

			// Token: 0x06006D13 RID: 27923 RVA: 0x0017AB74 File Offset: 0x00178D74
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006D14 RID: 27924 RVA: 0x0017ABBC File Offset: 0x00178DBC
			public override IEnumerator GetEnumerator(int index, int count)
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator(index, count);
				}
				return enumerator;
			}

			// Token: 0x06006D15 RID: 27925 RVA: 0x0017AC08 File Offset: 0x00178E08
			public override int IndexOf(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(value);
				}
				return num;
			}

			// Token: 0x06006D16 RID: 27926 RVA: 0x0017AC50 File Offset: 0x00178E50
			public override int IndexOf(object value, int startIndex)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(value, startIndex);
				}
				return num;
			}

			// Token: 0x06006D17 RID: 27927 RVA: 0x0017AC9C File Offset: 0x00178E9C
			public override int IndexOf(object value, int startIndex, int count)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(value, startIndex, count);
				}
				return num;
			}

			// Token: 0x06006D18 RID: 27928 RVA: 0x0017ACE8 File Offset: 0x00178EE8
			public override void Insert(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, value);
				}
			}

			// Token: 0x06006D19 RID: 27929 RVA: 0x0017AD30 File Offset: 0x00178F30
			public override void InsertRange(int index, ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.InsertRange(index, c);
				}
			}

			// Token: 0x06006D1A RID: 27930 RVA: 0x0017AD78 File Offset: 0x00178F78
			public override int LastIndexOf(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.LastIndexOf(value);
				}
				return num;
			}

			// Token: 0x06006D1B RID: 27931 RVA: 0x0017ADC0 File Offset: 0x00178FC0
			public override int LastIndexOf(object value, int startIndex)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.LastIndexOf(value, startIndex);
				}
				return num;
			}

			// Token: 0x06006D1C RID: 27932 RVA: 0x0017AE0C File Offset: 0x0017900C
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.LastIndexOf(value, startIndex, count);
				}
				return num;
			}

			// Token: 0x06006D1D RID: 27933 RVA: 0x0017AE58 File Offset: 0x00179058
			public override void Remove(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(value);
				}
			}

			// Token: 0x06006D1E RID: 27934 RVA: 0x0017AEA0 File Offset: 0x001790A0
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x06006D1F RID: 27935 RVA: 0x0017AEE8 File Offset: 0x001790E8
			public override void RemoveRange(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveRange(index, count);
				}
			}

			// Token: 0x06006D20 RID: 27936 RVA: 0x0017AF30 File Offset: 0x00179130
			public override void Reverse(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Reverse(index, count);
				}
			}

			// Token: 0x06006D21 RID: 27937 RVA: 0x0017AF78 File Offset: 0x00179178
			public override void SetRange(int index, ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.SetRange(index, c);
				}
			}

			// Token: 0x06006D22 RID: 27938 RVA: 0x0017AFC0 File Offset: 0x001791C0
			public override ArrayList GetRange(int index, int count)
			{
				object root = this._root;
				ArrayList range;
				lock (root)
				{
					range = this._list.GetRange(index, count);
				}
				return range;
			}

			// Token: 0x06006D23 RID: 27939 RVA: 0x0017B00C File Offset: 0x0017920C
			public override void Sort()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort();
				}
			}

			// Token: 0x06006D24 RID: 27940 RVA: 0x0017B054 File Offset: 0x00179254
			public override void Sort(IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(comparer);
				}
			}

			// Token: 0x06006D25 RID: 27941 RVA: 0x0017B09C File Offset: 0x0017929C
			public override void Sort(int index, int count, IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(index, count, comparer);
				}
			}

			// Token: 0x06006D26 RID: 27942 RVA: 0x0017B0E4 File Offset: 0x001792E4
			public override object[] ToArray()
			{
				object root = this._root;
				object[] array;
				lock (root)
				{
					array = this._list.ToArray();
				}
				return array;
			}

			// Token: 0x06006D27 RID: 27943 RVA: 0x0017B12C File Offset: 0x0017932C
			public override Array ToArray(Type type)
			{
				object root = this._root;
				Array array;
				lock (root)
				{
					array = this._list.ToArray(type);
				}
				return array;
			}

			// Token: 0x06006D28 RID: 27944 RVA: 0x0017B174 File Offset: 0x00179374
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._list.TrimToSize();
				}
			}

			// Token: 0x0400353F RID: 13631
			private ArrayList _list;

			// Token: 0x04003540 RID: 13632
			private object _root;
		}

		// Token: 0x02000BA0 RID: 2976
		[Serializable]
		private class SyncIList : IList, ICollection, IEnumerable
		{
			// Token: 0x06006D29 RID: 27945 RVA: 0x0017B1BC File Offset: 0x001793BC
			internal SyncIList(IList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x17001275 RID: 4725
			// (get) Token: 0x06006D2A RID: 27946 RVA: 0x0017B1D8 File Offset: 0x001793D8
			public virtual int Count
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

			// Token: 0x17001276 RID: 4726
			// (get) Token: 0x06006D2B RID: 27947 RVA: 0x0017B220 File Offset: 0x00179420
			public virtual bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001277 RID: 4727
			// (get) Token: 0x06006D2C RID: 27948 RVA: 0x0017B22D File Offset: 0x0017942D
			public virtual bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17001278 RID: 4728
			// (get) Token: 0x06006D2D RID: 27949 RVA: 0x0017B23A File Offset: 0x0017943A
			public virtual bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001279 RID: 4729
			public virtual object this[int index]
			{
				get
				{
					object root = this._root;
					object obj;
					lock (root)
					{
						obj = this._list[index];
					}
					return obj;
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

			// Token: 0x1700127A RID: 4730
			// (get) Token: 0x06006D30 RID: 27952 RVA: 0x0017B2D0 File Offset: 0x001794D0
			public virtual object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x06006D31 RID: 27953 RVA: 0x0017B2D8 File Offset: 0x001794D8
			public virtual int Add(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.Add(value);
				}
				return num;
			}

			// Token: 0x06006D32 RID: 27954 RVA: 0x0017B320 File Offset: 0x00179520
			public virtual void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06006D33 RID: 27955 RVA: 0x0017B368 File Offset: 0x00179568
			public virtual bool Contains(object item)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(item);
				}
				return flag2;
			}

			// Token: 0x06006D34 RID: 27956 RVA: 0x0017B3B0 File Offset: 0x001795B0
			public virtual void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06006D35 RID: 27957 RVA: 0x0017B3F8 File Offset: 0x001795F8
			public virtual IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006D36 RID: 27958 RVA: 0x0017B440 File Offset: 0x00179640
			public virtual int IndexOf(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(value);
				}
				return num;
			}

			// Token: 0x06006D37 RID: 27959 RVA: 0x0017B488 File Offset: 0x00179688
			public virtual void Insert(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, value);
				}
			}

			// Token: 0x06006D38 RID: 27960 RVA: 0x0017B4D0 File Offset: 0x001796D0
			public virtual void Remove(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(value);
				}
			}

			// Token: 0x06006D39 RID: 27961 RVA: 0x0017B518 File Offset: 0x00179718
			public virtual void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x04003541 RID: 13633
			private IList _list;

			// Token: 0x04003542 RID: 13634
			private object _root;
		}

		// Token: 0x02000BA1 RID: 2977
		[Serializable]
		private class FixedSizeList : IList, ICollection, IEnumerable
		{
			// Token: 0x06006D3A RID: 27962 RVA: 0x0017B560 File Offset: 0x00179760
			internal FixedSizeList(IList l)
			{
				this._list = l;
			}

			// Token: 0x1700127B RID: 4731
			// (get) Token: 0x06006D3B RID: 27963 RVA: 0x0017B56F File Offset: 0x0017976F
			public virtual int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x1700127C RID: 4732
			// (get) Token: 0x06006D3C RID: 27964 RVA: 0x0017B57C File Offset: 0x0017977C
			public virtual bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x1700127D RID: 4733
			// (get) Token: 0x06006D3D RID: 27965 RVA: 0x0017B589 File Offset: 0x00179789
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700127E RID: 4734
			// (get) Token: 0x06006D3E RID: 27966 RVA: 0x0017B58C File Offset: 0x0017978C
			public virtual bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x1700127F RID: 4735
			public virtual object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
				}
			}

			// Token: 0x17001280 RID: 4736
			// (get) Token: 0x06006D41 RID: 27969 RVA: 0x0017B5B6 File Offset: 0x001797B6
			public virtual object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06006D42 RID: 27970 RVA: 0x0017B5C3 File Offset: 0x001797C3
			public virtual int Add(object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006D43 RID: 27971 RVA: 0x0017B5D4 File Offset: 0x001797D4
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006D44 RID: 27972 RVA: 0x0017B5E5 File Offset: 0x001797E5
			public virtual bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06006D45 RID: 27973 RVA: 0x0017B5F3 File Offset: 0x001797F3
			public virtual void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06006D46 RID: 27974 RVA: 0x0017B602 File Offset: 0x00179802
			public virtual IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06006D47 RID: 27975 RVA: 0x0017B60F File Offset: 0x0017980F
			public virtual int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06006D48 RID: 27976 RVA: 0x0017B61D File Offset: 0x0017981D
			public virtual void Insert(int index, object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006D49 RID: 27977 RVA: 0x0017B62E File Offset: 0x0017982E
			public virtual void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006D4A RID: 27978 RVA: 0x0017B63F File Offset: 0x0017983F
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x04003543 RID: 13635
			private IList _list;
		}

		// Token: 0x02000BA2 RID: 2978
		[Serializable]
		private class FixedSizeArrayList : ArrayList
		{
			// Token: 0x06006D4B RID: 27979 RVA: 0x0017B650 File Offset: 0x00179850
			internal FixedSizeArrayList(ArrayList l)
			{
				this._list = l;
				this._version = this._list._version;
			}

			// Token: 0x17001281 RID: 4737
			// (get) Token: 0x06006D4C RID: 27980 RVA: 0x0017B670 File Offset: 0x00179870
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17001282 RID: 4738
			// (get) Token: 0x06006D4D RID: 27981 RVA: 0x0017B67D File Offset: 0x0017987D
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001283 RID: 4739
			// (get) Token: 0x06006D4E RID: 27982 RVA: 0x0017B68A File Offset: 0x0017988A
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001284 RID: 4740
			// (get) Token: 0x06006D4F RID: 27983 RVA: 0x0017B68D File Offset: 0x0017988D
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17001285 RID: 4741
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
					this._version = this._list._version;
				}
			}

			// Token: 0x17001286 RID: 4742
			// (get) Token: 0x06006D52 RID: 27986 RVA: 0x0017B6C8 File Offset: 0x001798C8
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06006D53 RID: 27987 RVA: 0x0017B6D5 File Offset: 0x001798D5
			public override int Add(object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006D54 RID: 27988 RVA: 0x0017B6E6 File Offset: 0x001798E6
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006D55 RID: 27989 RVA: 0x0017B6F7 File Offset: 0x001798F7
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x17001287 RID: 4743
			// (get) Token: 0x06006D56 RID: 27990 RVA: 0x0017B709 File Offset: 0x00179909
			// (set) Token: 0x06006D57 RID: 27991 RVA: 0x0017B716 File Offset: 0x00179916
			public override int Capacity
			{
				get
				{
					return this._list.Capacity;
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
				}
			}

			// Token: 0x06006D58 RID: 27992 RVA: 0x0017B727 File Offset: 0x00179927
			public override void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006D59 RID: 27993 RVA: 0x0017B738 File Offset: 0x00179938
			public override object Clone()
			{
				return new ArrayList.FixedSizeArrayList(this._list)
				{
					_list = (ArrayList)this._list.Clone()
				};
			}

			// Token: 0x06006D5A RID: 27994 RVA: 0x0017B768 File Offset: 0x00179968
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06006D5B RID: 27995 RVA: 0x0017B776 File Offset: 0x00179976
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06006D5C RID: 27996 RVA: 0x0017B785 File Offset: 0x00179985
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x06006D5D RID: 27997 RVA: 0x0017B797 File Offset: 0x00179997
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06006D5E RID: 27998 RVA: 0x0017B7A4 File Offset: 0x001799A4
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this._list.GetEnumerator(index, count);
			}

			// Token: 0x06006D5F RID: 27999 RVA: 0x0017B7B3 File Offset: 0x001799B3
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06006D60 RID: 28000 RVA: 0x0017B7C1 File Offset: 0x001799C1
			public override int IndexOf(object value, int startIndex)
			{
				return this._list.IndexOf(value, startIndex);
			}

			// Token: 0x06006D61 RID: 28001 RVA: 0x0017B7D0 File Offset: 0x001799D0
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this._list.IndexOf(value, startIndex, count);
			}

			// Token: 0x06006D62 RID: 28002 RVA: 0x0017B7E0 File Offset: 0x001799E0
			public override void Insert(int index, object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006D63 RID: 28003 RVA: 0x0017B7F1 File Offset: 0x001799F1
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006D64 RID: 28004 RVA: 0x0017B802 File Offset: 0x00179A02
			public override int LastIndexOf(object value)
			{
				return this._list.LastIndexOf(value);
			}

			// Token: 0x06006D65 RID: 28005 RVA: 0x0017B810 File Offset: 0x00179A10
			public override int LastIndexOf(object value, int startIndex)
			{
				return this._list.LastIndexOf(value, startIndex);
			}

			// Token: 0x06006D66 RID: 28006 RVA: 0x0017B81F File Offset: 0x00179A1F
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this._list.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x06006D67 RID: 28007 RVA: 0x0017B82F File Offset: 0x00179A2F
			public override void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006D68 RID: 28008 RVA: 0x0017B840 File Offset: 0x00179A40
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006D69 RID: 28009 RVA: 0x0017B851 File Offset: 0x00179A51
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x06006D6A RID: 28010 RVA: 0x0017B862 File Offset: 0x00179A62
			public override void SetRange(int index, ICollection c)
			{
				this._list.SetRange(index, c);
				this._version = this._list._version;
			}

			// Token: 0x06006D6B RID: 28011 RVA: 0x0017B884 File Offset: 0x00179A84
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06006D6C RID: 28012 RVA: 0x0017B8DC File Offset: 0x00179ADC
			public override void Reverse(int index, int count)
			{
				this._list.Reverse(index, count);
				this._version = this._list._version;
			}

			// Token: 0x06006D6D RID: 28013 RVA: 0x0017B8FC File Offset: 0x00179AFC
			public override void Sort(int index, int count, IComparer comparer)
			{
				this._list.Sort(index, count, comparer);
				this._version = this._list._version;
			}

			// Token: 0x06006D6E RID: 28014 RVA: 0x0017B91D File Offset: 0x00179B1D
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x06006D6F RID: 28015 RVA: 0x0017B92A File Offset: 0x00179B2A
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x06006D70 RID: 28016 RVA: 0x0017B938 File Offset: 0x00179B38
			public override void TrimToSize()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
			}

			// Token: 0x04003544 RID: 13636
			private ArrayList _list;
		}

		// Token: 0x02000BA3 RID: 2979
		[Serializable]
		private class ReadOnlyList : IList, ICollection, IEnumerable
		{
			// Token: 0x06006D71 RID: 28017 RVA: 0x0017B949 File Offset: 0x00179B49
			internal ReadOnlyList(IList l)
			{
				this._list = l;
			}

			// Token: 0x17001288 RID: 4744
			// (get) Token: 0x06006D72 RID: 28018 RVA: 0x0017B958 File Offset: 0x00179B58
			public virtual int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17001289 RID: 4745
			// (get) Token: 0x06006D73 RID: 28019 RVA: 0x0017B965 File Offset: 0x00179B65
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700128A RID: 4746
			// (get) Token: 0x06006D74 RID: 28020 RVA: 0x0017B968 File Offset: 0x00179B68
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700128B RID: 4747
			// (get) Token: 0x06006D75 RID: 28021 RVA: 0x0017B96B File Offset: 0x00179B6B
			public virtual bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x1700128C RID: 4748
			public virtual object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
				}
			}

			// Token: 0x1700128D RID: 4749
			// (get) Token: 0x06006D78 RID: 28024 RVA: 0x0017B997 File Offset: 0x00179B97
			public virtual object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06006D79 RID: 28025 RVA: 0x0017B9A4 File Offset: 0x00179BA4
			public virtual int Add(object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006D7A RID: 28026 RVA: 0x0017B9B5 File Offset: 0x00179BB5
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006D7B RID: 28027 RVA: 0x0017B9C6 File Offset: 0x00179BC6
			public virtual bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06006D7C RID: 28028 RVA: 0x0017B9D4 File Offset: 0x00179BD4
			public virtual void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06006D7D RID: 28029 RVA: 0x0017B9E3 File Offset: 0x00179BE3
			public virtual IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06006D7E RID: 28030 RVA: 0x0017B9F0 File Offset: 0x00179BF0
			public virtual int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06006D7F RID: 28031 RVA: 0x0017B9FE File Offset: 0x00179BFE
			public virtual void Insert(int index, object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006D80 RID: 28032 RVA: 0x0017BA0F File Offset: 0x00179C0F
			public virtual void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006D81 RID: 28033 RVA: 0x0017BA20 File Offset: 0x00179C20
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x04003545 RID: 13637
			private IList _list;
		}

		// Token: 0x02000BA4 RID: 2980
		[Serializable]
		private class ReadOnlyArrayList : ArrayList
		{
			// Token: 0x06006D82 RID: 28034 RVA: 0x0017BA31 File Offset: 0x00179C31
			internal ReadOnlyArrayList(ArrayList l)
			{
				this._list = l;
			}

			// Token: 0x1700128E RID: 4750
			// (get) Token: 0x06006D83 RID: 28035 RVA: 0x0017BA40 File Offset: 0x00179C40
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x1700128F RID: 4751
			// (get) Token: 0x06006D84 RID: 28036 RVA: 0x0017BA4D File Offset: 0x00179C4D
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001290 RID: 4752
			// (get) Token: 0x06006D85 RID: 28037 RVA: 0x0017BA50 File Offset: 0x00179C50
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001291 RID: 4753
			// (get) Token: 0x06006D86 RID: 28038 RVA: 0x0017BA53 File Offset: 0x00179C53
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17001292 RID: 4754
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
				}
			}

			// Token: 0x17001293 RID: 4755
			// (get) Token: 0x06006D89 RID: 28041 RVA: 0x0017BA7F File Offset: 0x00179C7F
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06006D8A RID: 28042 RVA: 0x0017BA8C File Offset: 0x00179C8C
			public override int Add(object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006D8B RID: 28043 RVA: 0x0017BA9D File Offset: 0x00179C9D
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006D8C RID: 28044 RVA: 0x0017BAAE File Offset: 0x00179CAE
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x17001294 RID: 4756
			// (get) Token: 0x06006D8D RID: 28045 RVA: 0x0017BAC0 File Offset: 0x00179CC0
			// (set) Token: 0x06006D8E RID: 28046 RVA: 0x0017BACD File Offset: 0x00179CCD
			public override int Capacity
			{
				get
				{
					return this._list.Capacity;
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
				}
			}

			// Token: 0x06006D8F RID: 28047 RVA: 0x0017BADE File Offset: 0x00179CDE
			public override void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006D90 RID: 28048 RVA: 0x0017BAF0 File Offset: 0x00179CF0
			public override object Clone()
			{
				return new ArrayList.ReadOnlyArrayList(this._list)
				{
					_list = (ArrayList)this._list.Clone()
				};
			}

			// Token: 0x06006D91 RID: 28049 RVA: 0x0017BB20 File Offset: 0x00179D20
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06006D92 RID: 28050 RVA: 0x0017BB2E File Offset: 0x00179D2E
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06006D93 RID: 28051 RVA: 0x0017BB3D File Offset: 0x00179D3D
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x06006D94 RID: 28052 RVA: 0x0017BB4F File Offset: 0x00179D4F
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06006D95 RID: 28053 RVA: 0x0017BB5C File Offset: 0x00179D5C
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this._list.GetEnumerator(index, count);
			}

			// Token: 0x06006D96 RID: 28054 RVA: 0x0017BB6B File Offset: 0x00179D6B
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06006D97 RID: 28055 RVA: 0x0017BB79 File Offset: 0x00179D79
			public override int IndexOf(object value, int startIndex)
			{
				return this._list.IndexOf(value, startIndex);
			}

			// Token: 0x06006D98 RID: 28056 RVA: 0x0017BB88 File Offset: 0x00179D88
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this._list.IndexOf(value, startIndex, count);
			}

			// Token: 0x06006D99 RID: 28057 RVA: 0x0017BB98 File Offset: 0x00179D98
			public override void Insert(int index, object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006D9A RID: 28058 RVA: 0x0017BBA9 File Offset: 0x00179DA9
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006D9B RID: 28059 RVA: 0x0017BBBA File Offset: 0x00179DBA
			public override int LastIndexOf(object value)
			{
				return this._list.LastIndexOf(value);
			}

			// Token: 0x06006D9C RID: 28060 RVA: 0x0017BBC8 File Offset: 0x00179DC8
			public override int LastIndexOf(object value, int startIndex)
			{
				return this._list.LastIndexOf(value, startIndex);
			}

			// Token: 0x06006D9D RID: 28061 RVA: 0x0017BBD7 File Offset: 0x00179DD7
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this._list.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x06006D9E RID: 28062 RVA: 0x0017BBE7 File Offset: 0x00179DE7
			public override void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006D9F RID: 28063 RVA: 0x0017BBF8 File Offset: 0x00179DF8
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006DA0 RID: 28064 RVA: 0x0017BC09 File Offset: 0x00179E09
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006DA1 RID: 28065 RVA: 0x0017BC1A File Offset: 0x00179E1A
			public override void SetRange(int index, ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006DA2 RID: 28066 RVA: 0x0017BC2C File Offset: 0x00179E2C
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06006DA3 RID: 28067 RVA: 0x0017BC84 File Offset: 0x00179E84
			public override void Reverse(int index, int count)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006DA4 RID: 28068 RVA: 0x0017BC95 File Offset: 0x00179E95
			public override void Sort(int index, int count, IComparer comparer)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x06006DA5 RID: 28069 RVA: 0x0017BCA6 File Offset: 0x00179EA6
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x06006DA6 RID: 28070 RVA: 0x0017BCB3 File Offset: 0x00179EB3
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x06006DA7 RID: 28071 RVA: 0x0017BCC1 File Offset: 0x00179EC1
			public override void TrimToSize()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
			}

			// Token: 0x04003546 RID: 13638
			private ArrayList _list;
		}

		// Token: 0x02000BA5 RID: 2981
		[Serializable]
		private sealed class ArrayListEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06006DA8 RID: 28072 RVA: 0x0017BCD2 File Offset: 0x00179ED2
			internal ArrayListEnumerator(ArrayList list, int index, int count)
			{
				this.list = list;
				this.startIndex = index;
				this.index = index - 1;
				this.endIndex = this.index + count;
				this.version = list._version;
				this.currentElement = null;
			}

			// Token: 0x06006DA9 RID: 28073 RVA: 0x0017BD12 File Offset: 0x00179F12
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006DAA RID: 28074 RVA: 0x0017BD1C File Offset: 0x00179F1C
			public bool MoveNext()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.index < this.endIndex)
				{
					ArrayList arrayList = this.list;
					int num = this.index + 1;
					this.index = num;
					this.currentElement = arrayList[num];
					return true;
				}
				this.index = this.endIndex + 1;
				return false;
			}

			// Token: 0x17001295 RID: 4757
			// (get) Token: 0x06006DAB RID: 28075 RVA: 0x0017BD90 File Offset: 0x00179F90
			public object Current
			{
				get
				{
					if (this.index < this.startIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this.index > this.endIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this.currentElement;
				}
			}

			// Token: 0x06006DAC RID: 28076 RVA: 0x0017BDDF File Offset: 0x00179FDF
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.index = this.startIndex - 1;
			}

			// Token: 0x04003547 RID: 13639
			private ArrayList list;

			// Token: 0x04003548 RID: 13640
			private int index;

			// Token: 0x04003549 RID: 13641
			private int endIndex;

			// Token: 0x0400354A RID: 13642
			private int version;

			// Token: 0x0400354B RID: 13643
			private object currentElement;

			// Token: 0x0400354C RID: 13644
			private int startIndex;
		}

		// Token: 0x02000BA6 RID: 2982
		[Serializable]
		private class Range : ArrayList
		{
			// Token: 0x06006DAD RID: 28077 RVA: 0x0017BE12 File Offset: 0x0017A012
			internal Range(ArrayList list, int index, int count)
				: base(false)
			{
				this._baseList = list;
				this._baseIndex = index;
				this._baseSize = count;
				this._baseVersion = list._version;
				this._version = list._version;
			}

			// Token: 0x06006DAE RID: 28078 RVA: 0x0017BE48 File Offset: 0x0017A048
			private void InternalUpdateRange()
			{
				if (this._baseVersion != this._baseList._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnderlyingArrayListChanged"));
				}
			}

			// Token: 0x06006DAF RID: 28079 RVA: 0x0017BE6D File Offset: 0x0017A06D
			private void InternalUpdateVersion()
			{
				this._baseVersion++;
				this._version++;
			}

			// Token: 0x06006DB0 RID: 28080 RVA: 0x0017BE8C File Offset: 0x0017A08C
			public override int Add(object value)
			{
				this.InternalUpdateRange();
				this._baseList.Insert(this._baseIndex + this._baseSize, value);
				this.InternalUpdateVersion();
				int baseSize = this._baseSize;
				this._baseSize = baseSize + 1;
				return baseSize;
			}

			// Token: 0x06006DB1 RID: 28081 RVA: 0x0017BED0 File Offset: 0x0017A0D0
			public override void AddRange(ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				this.InternalUpdateRange();
				int count = c.Count;
				if (count > 0)
				{
					this._baseList.InsertRange(this._baseIndex + this._baseSize, c);
					this.InternalUpdateVersion();
					this._baseSize += count;
				}
			}

			// Token: 0x06006DB2 RID: 28082 RVA: 0x0017BF2C File Offset: 0x0017A12C
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				int num = this._baseList.BinarySearch(this._baseIndex + index, count, value, comparer);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return num + this._baseIndex;
			}

			// Token: 0x17001296 RID: 4758
			// (get) Token: 0x06006DB3 RID: 28083 RVA: 0x0017BFAF File Offset: 0x0017A1AF
			// (set) Token: 0x06006DB4 RID: 28084 RVA: 0x0017BFBC File Offset: 0x0017A1BC
			public override int Capacity
			{
				get
				{
					return this._baseList.Capacity;
				}
				set
				{
					if (value < this.Count)
					{
						throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
					}
				}
			}

			// Token: 0x06006DB5 RID: 28085 RVA: 0x0017BFDC File Offset: 0x0017A1DC
			public override void Clear()
			{
				this.InternalUpdateRange();
				if (this._baseSize != 0)
				{
					this._baseList.RemoveRange(this._baseIndex, this._baseSize);
					this.InternalUpdateVersion();
					this._baseSize = 0;
				}
			}

			// Token: 0x06006DB6 RID: 28086 RVA: 0x0017C010 File Offset: 0x0017A210
			public override object Clone()
			{
				this.InternalUpdateRange();
				return new ArrayList.Range(this._baseList, this._baseIndex, this._baseSize)
				{
					_baseList = (ArrayList)this._baseList.Clone()
				};
			}

			// Token: 0x06006DB7 RID: 28087 RVA: 0x0017C054 File Offset: 0x0017A254
			public override bool Contains(object item)
			{
				this.InternalUpdateRange();
				if (item == null)
				{
					for (int i = 0; i < this._baseSize; i++)
					{
						if (this._baseList[this._baseIndex + i] == null)
						{
							return true;
						}
					}
					return false;
				}
				for (int j = 0; j < this._baseSize; j++)
				{
					if (this._baseList[this._baseIndex + j] != null && this._baseList[this._baseIndex + j].Equals(item))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006DB8 RID: 28088 RVA: 0x0017C0D8 File Offset: 0x0017A2D8
			public override void CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (array.Length - index < this._baseSize)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				this._baseList.CopyTo(this._baseIndex, array, index, this._baseSize);
			}

			// Token: 0x06006DB9 RID: 28089 RVA: 0x0017C164 File Offset: 0x0017A364
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
				}
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (array.Length - arrayIndex < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				this._baseList.CopyTo(this._baseIndex + index, array, arrayIndex, count);
			}

			// Token: 0x17001297 RID: 4759
			// (get) Token: 0x06006DBA RID: 28090 RVA: 0x0017C216 File Offset: 0x0017A416
			public override int Count
			{
				get
				{
					this.InternalUpdateRange();
					return this._baseSize;
				}
			}

			// Token: 0x17001298 RID: 4760
			// (get) Token: 0x06006DBB RID: 28091 RVA: 0x0017C224 File Offset: 0x0017A424
			public override bool IsReadOnly
			{
				get
				{
					return this._baseList.IsReadOnly;
				}
			}

			// Token: 0x17001299 RID: 4761
			// (get) Token: 0x06006DBC RID: 28092 RVA: 0x0017C231 File Offset: 0x0017A431
			public override bool IsFixedSize
			{
				get
				{
					return this._baseList.IsFixedSize;
				}
			}

			// Token: 0x1700129A RID: 4762
			// (get) Token: 0x06006DBD RID: 28093 RVA: 0x0017C23E File Offset: 0x0017A43E
			public override bool IsSynchronized
			{
				get
				{
					return this._baseList.IsSynchronized;
				}
			}

			// Token: 0x06006DBE RID: 28094 RVA: 0x0017C24B File Offset: 0x0017A44B
			public override IEnumerator GetEnumerator()
			{
				return this.GetEnumerator(0, this._baseSize);
			}

			// Token: 0x06006DBF RID: 28095 RVA: 0x0017C25C File Offset: 0x0017A45C
			public override IEnumerator GetEnumerator(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				return this._baseList.GetEnumerator(this._baseIndex + index, count);
			}

			// Token: 0x06006DC0 RID: 28096 RVA: 0x0017C2C8 File Offset: 0x0017A4C8
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x1700129B RID: 4763
			// (get) Token: 0x06006DC1 RID: 28097 RVA: 0x0017C326 File Offset: 0x0017A526
			public override object SyncRoot
			{
				get
				{
					return this._baseList.SyncRoot;
				}
			}

			// Token: 0x06006DC2 RID: 28098 RVA: 0x0017C334 File Offset: 0x0017A534
			public override int IndexOf(object value)
			{
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex, this._baseSize);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006DC3 RID: 28099 RVA: 0x0017C370 File Offset: 0x0017A570
			public override int IndexOf(object value, int startIndex)
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (startIndex > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, this._baseSize - startIndex);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006DC4 RID: 28100 RVA: 0x0017C3E8 File Offset: 0x0017A5E8
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (count < 0 || startIndex > this._baseSize - count)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, count);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006DC5 RID: 28101 RVA: 0x0017C468 File Offset: 0x0017A668
			public override void Insert(int index, object value)
			{
				if (index < 0 || index > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				this.InternalUpdateRange();
				this._baseList.Insert(this._baseIndex + index, value);
				this.InternalUpdateVersion();
				this._baseSize++;
			}

			// Token: 0x06006DC6 RID: 28102 RVA: 0x0017C4C8 File Offset: 0x0017A6C8
			public override void InsertRange(int index, ICollection c)
			{
				if (index < 0 || index > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				this.InternalUpdateRange();
				int count = c.Count;
				if (count > 0)
				{
					this._baseList.InsertRange(this._baseIndex + index, c);
					this._baseSize += count;
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06006DC7 RID: 28103 RVA: 0x0017C540 File Offset: 0x0017A740
			public override int LastIndexOf(object value)
			{
				this.InternalUpdateRange();
				int num = this._baseList.LastIndexOf(value, this._baseIndex + this._baseSize - 1, this._baseSize);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006DC8 RID: 28104 RVA: 0x0017C583 File Offset: 0x0017A783
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x06006DC9 RID: 28105 RVA: 0x0017C590 File Offset: 0x0017A790
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				this.InternalUpdateRange();
				if (this._baseSize == 0)
				{
					return -1;
				}
				if (startIndex >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				int num = this._baseList.LastIndexOf(value, this._baseIndex + startIndex, count);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006DCA RID: 28106 RVA: 0x0017C608 File Offset: 0x0017A808
			public override void RemoveAt(int index)
			{
				if (index < 0 || index >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				this.InternalUpdateRange();
				this._baseList.RemoveAt(this._baseIndex + index);
				this.InternalUpdateVersion();
				this._baseSize--;
			}

			// Token: 0x06006DCB RID: 28107 RVA: 0x0017C664 File Offset: 0x0017A864
			public override void RemoveRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				if (count > 0)
				{
					this._baseList.RemoveRange(this._baseIndex + index, count);
					this.InternalUpdateVersion();
					this._baseSize -= count;
				}
			}

			// Token: 0x06006DCC RID: 28108 RVA: 0x0017C6E8 File Offset: 0x0017A8E8
			public override void Reverse(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				this._baseList.Reverse(this._baseIndex + index, count);
				this.InternalUpdateVersion();
			}

			// Token: 0x06006DCD RID: 28109 RVA: 0x0017C758 File Offset: 0x0017A958
			public override void SetRange(int index, ICollection c)
			{
				this.InternalUpdateRange();
				if (index < 0 || index >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				this._baseList.SetRange(this._baseIndex + index, c);
				if (c.Count > 0)
				{
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06006DCE RID: 28110 RVA: 0x0017C7B0 File Offset: 0x0017A9B0
			public override void Sort(int index, int count, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				this.InternalUpdateRange();
				this._baseList.Sort(this._baseIndex + index, count, comparer);
				this.InternalUpdateVersion();
			}

			// Token: 0x1700129C RID: 4764
			public override object this[int index]
			{
				get
				{
					this.InternalUpdateRange();
					if (index < 0 || index >= this._baseSize)
					{
						throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
					}
					return this._baseList[this._baseIndex + index];
				}
				set
				{
					this.InternalUpdateRange();
					if (index < 0 || index >= this._baseSize)
					{
						throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
					}
					this._baseList[this._baseIndex + index] = value;
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06006DD1 RID: 28113 RVA: 0x0017C8B0 File Offset: 0x0017AAB0
			public override object[] ToArray()
			{
				this.InternalUpdateRange();
				object[] array = new object[this._baseSize];
				Array.Copy(this._baseList._items, this._baseIndex, array, 0, this._baseSize);
				return array;
			}

			// Token: 0x06006DD2 RID: 28114 RVA: 0x0017C8F0 File Offset: 0x0017AAF0
			[SecuritySafeCritical]
			public override Array ToArray(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				this.InternalUpdateRange();
				Array array = Array.UnsafeCreateInstance(type, this._baseSize);
				this._baseList.CopyTo(this._baseIndex, array, 0, this._baseSize);
				return array;
			}

			// Token: 0x06006DD3 RID: 28115 RVA: 0x0017C93E File Offset: 0x0017AB3E
			public override void TrimToSize()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RangeCollection"));
			}

			// Token: 0x0400354D RID: 13645
			private ArrayList _baseList;

			// Token: 0x0400354E RID: 13646
			private int _baseIndex;

			// Token: 0x0400354F RID: 13647
			private int _baseSize;

			// Token: 0x04003550 RID: 13648
			private int _baseVersion;
		}

		// Token: 0x02000BA7 RID: 2983
		[Serializable]
		private sealed class ArrayListEnumeratorSimple : IEnumerator, ICloneable
		{
			// Token: 0x06006DD4 RID: 28116 RVA: 0x0017C950 File Offset: 0x0017AB50
			internal ArrayListEnumeratorSimple(ArrayList list)
			{
				this.list = list;
				this.index = -1;
				this.version = list._version;
				this.isArrayList = list.GetType() == typeof(ArrayList);
				this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
			}

			// Token: 0x06006DD5 RID: 28117 RVA: 0x0017C9A3 File Offset: 0x0017ABA3
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006DD6 RID: 28118 RVA: 0x0017C9AC File Offset: 0x0017ABAC
			public bool MoveNext()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.isArrayList)
				{
					if (this.index < this.list._size - 1)
					{
						object[] items = this.list._items;
						int num = this.index + 1;
						this.index = num;
						this.currentElement = items[num];
						return true;
					}
					this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
					this.index = this.list._size;
					return false;
				}
				else
				{
					if (this.index < this.list.Count - 1)
					{
						ArrayList arrayList = this.list;
						int num = this.index + 1;
						this.index = num;
						this.currentElement = arrayList[num];
						return true;
					}
					this.index = this.list.Count;
					this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
					return false;
				}
			}

			// Token: 0x1700129D RID: 4765
			// (get) Token: 0x06006DD7 RID: 28119 RVA: 0x0017CA94 File Offset: 0x0017AC94
			public object Current
			{
				get
				{
					object obj = this.currentElement;
					if (ArrayList.ArrayListEnumeratorSimple.dummyObject != obj)
					{
						return obj;
					}
					if (this.index == -1)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
				}
			}

			// Token: 0x06006DD8 RID: 28120 RVA: 0x0017CADA File Offset: 0x0017ACDA
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
				this.index = -1;
			}

			// Token: 0x04003551 RID: 13649
			private ArrayList list;

			// Token: 0x04003552 RID: 13650
			private int index;

			// Token: 0x04003553 RID: 13651
			private int version;

			// Token: 0x04003554 RID: 13652
			private object currentElement;

			// Token: 0x04003555 RID: 13653
			[NonSerialized]
			private bool isArrayList;

			// Token: 0x04003556 RID: 13654
			private static object dummyObject = new object();
		}

		// Token: 0x02000BA8 RID: 2984
		internal class ArrayListDebugView
		{
			// Token: 0x06006DDA RID: 28122 RVA: 0x0017CB1D File Offset: 0x0017AD1D
			public ArrayListDebugView(ArrayList arrayList)
			{
				if (arrayList == null)
				{
					throw new ArgumentNullException("arrayList");
				}
				this.arrayList = arrayList;
			}

			// Token: 0x1700129E RID: 4766
			// (get) Token: 0x06006DDB RID: 28123 RVA: 0x0017CB3A File Offset: 0x0017AD3A
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this.arrayList.ToArray();
				}
			}

			// Token: 0x04003557 RID: 13655
			private ArrayList arrayList;
		}
	}
}
