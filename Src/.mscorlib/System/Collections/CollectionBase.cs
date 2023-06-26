using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Provides the <see langword="abstract" /> base class for a strongly typed collection.</summary>
	// Token: 0x0200048A RID: 1162
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class CollectionBase : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CollectionBase" /> class with the default initial capacity.</summary>
		// Token: 0x0600378F RID: 14223 RVA: 0x000D6E24 File Offset: 0x000D5024
		protected CollectionBase()
		{
			this.list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CollectionBase" /> class with the specified capacity.</summary>
		/// <param name="capacity">The number of elements that the new list can initially store.</param>
		// Token: 0x06003790 RID: 14224 RVA: 0x000D6E37 File Offset: 0x000D5037
		protected CollectionBase(int capacity)
		{
			this.list = new ArrayList(capacity);
		}

		/// <summary>Gets an <see cref="T:System.Collections.ArrayList" /> containing the list of elements in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> representing the <see cref="T:System.Collections.CollectionBase" /> instance itself.  
		///  Retrieving the value of this property is an O(1) operation.</returns>
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x000D6E4B File Offset: 0x000D504B
		protected ArrayList InnerList
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IList" /> containing the list of elements in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> representing the <see cref="T:System.Collections.CollectionBase" /> instance itself.</returns>
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x000D6E66 File Offset: 0x000D5066
		protected IList List
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.CollectionBase" /> can contain.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.CollectionBase" /> can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.CollectionBase.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000D6E69 File Offset: 0x000D5069
		// (set) Token: 0x06003794 RID: 14228 RVA: 0x000D6E76 File Offset: 0x000D5076
		[ComVisible(false)]
		public int Capacity
		{
			get
			{
				return this.InnerList.Capacity;
			}
			set
			{
				this.InnerList.Capacity = value;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.CollectionBase" /> instance. This property cannot be overridden.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.CollectionBase" /> instance.  
		///  Retrieving the value of this property is an O(1) operation.</returns>
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x000D6E84 File Offset: 0x000D5084
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.list != null)
				{
					return this.list.Count;
				}
				return 0;
			}
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.CollectionBase" /> instance. This method cannot be overridden.</summary>
		// Token: 0x06003796 RID: 14230 RVA: 0x000D6E9B File Offset: 0x000D509B
		[__DynamicallyInvokable]
		public void Clear()
		{
			this.OnClear();
			this.InnerList.Clear();
			this.OnClearComplete();
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.CollectionBase" /> instance. This method is not overridable.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		// Token: 0x06003797 RID: 14231 RVA: 0x000D6EB4 File Offset: 0x000D50B4
		[__DynamicallyInvokable]
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			object obj = this.InnerList[index];
			this.OnValidate(obj);
			this.OnRemove(index, obj);
			this.InnerList.RemoveAt(index);
			try
			{
				this.OnRemoveComplete(index, obj);
			}
			catch
			{
				this.InnerList.Insert(index, obj);
				throw;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.CollectionBase" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.CollectionBase" /> is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000D6F38 File Offset: 0x000D5138
		bool IList.IsReadOnly
		{
			get
			{
				return this.InnerList.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.CollectionBase" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.CollectionBase" /> has a fixed size; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000D6F45 File Offset: 0x000D5145
		bool IList.IsFixedSize
		{
			get
			{
				return this.InnerList.IsFixedSize;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.CollectionBase" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.CollectionBase" /> is synchronized (thread safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000D6F52 File Offset: 0x000D5152
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.CollectionBase" />.</returns>
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x000D6F5F File Offset: 0x000D515F
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.CollectionBase" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.CollectionBase" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.CollectionBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.CollectionBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x0600379C RID: 14236 RVA: 0x000D6F6C File Offset: 0x000D516C
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		// Token: 0x1700082B RID: 2091
		object IList.this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				return this.InnerList[index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				this.OnValidate(value);
				object obj = this.InnerList[index];
				this.OnSet(index, obj, value);
				this.InnerList[index] = value;
				try
				{
					this.OnSetComplete(index, obj, value);
				}
				catch
				{
					this.InnerList[index] = obj;
					throw;
				}
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.CollectionBase" /> contains a specific element.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.CollectionBase" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.CollectionBase" /> contains the specified <paramref name="value" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600379F RID: 14239 RVA: 0x000D7030 File Offset: 0x000D5230
		bool IList.Contains(object value)
		{
			return this.InnerList.Contains(value);
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:System.Collections.CollectionBase" />.</param>
		/// <returns>The <see cref="T:System.Collections.CollectionBase" /> index at which the <paramref name="value" /> has been added.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.CollectionBase" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.CollectionBase" /> has a fixed size.</exception>
		// Token: 0x060037A0 RID: 14240 RVA: 0x000D7040 File Offset: 0x000D5240
		int IList.Add(object value)
		{
			this.OnValidate(value);
			this.OnInsert(this.InnerList.Count, value);
			int num = this.InnerList.Add(value);
			try
			{
				this.OnInsertComplete(num, value);
			}
			catch
			{
				this.InnerList.RemoveAt(num);
				throw;
			}
			return num;
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.CollectionBase" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter was not found in the <see cref="T:System.Collections.CollectionBase" /> object.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.CollectionBase" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.CollectionBase" /> has a fixed size.</exception>
		// Token: 0x060037A1 RID: 14241 RVA: 0x000D70A0 File Offset: 0x000D52A0
		void IList.Remove(object value)
		{
			this.OnValidate(value);
			int num = this.InnerList.IndexOf(value);
			if (num < 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RemoveArgNotFound"));
			}
			this.OnRemove(num, value);
			this.InnerList.RemoveAt(num);
			try
			{
				this.OnRemoveComplete(num, value);
			}
			catch
			{
				this.InnerList.Insert(num, value);
				throw;
			}
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.CollectionBase" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.CollectionBase" />, if found; otherwise, -1.</returns>
		// Token: 0x060037A2 RID: 14242 RVA: 0x000D7114 File Offset: 0x000D5314
		int IList.IndexOf(object value)
		{
			return this.InnerList.IndexOf(value);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.CollectionBase" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.CollectionBase" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.CollectionBase" /> has a fixed size.</exception>
		// Token: 0x060037A3 RID: 14243 RVA: 0x000D7124 File Offset: 0x000D5324
		void IList.Insert(int index, object value)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			this.OnValidate(value);
			this.OnInsert(index, value);
			this.InnerList.Insert(index, value);
			try
			{
				this.OnInsertComplete(index, value);
			}
			catch
			{
				this.InnerList.RemoveAt(index);
				throw;
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.CollectionBase" /> instance.</returns>
		// Token: 0x060037A4 RID: 14244 RVA: 0x000D7198 File Offset: 0x000D5398
		[__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		/// <summary>Performs additional custom processes before setting a value in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
		/// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x060037A5 RID: 14245 RVA: 0x000D71A5 File Offset: 0x000D53A5
		protected virtual void OnSet(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes before inserting a new element into the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x060037A6 RID: 14246 RVA: 0x000D71A7 File Offset: 0x000D53A7
		protected virtual void OnInsert(int index, object value)
		{
		}

		/// <summary>Performs additional custom processes when clearing the contents of the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		// Token: 0x060037A7 RID: 14247 RVA: 0x000D71A9 File Offset: 0x000D53A9
		protected virtual void OnClear()
		{
		}

		/// <summary>Performs additional custom processes when removing an element from the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
		/// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
		// Token: 0x060037A8 RID: 14248 RVA: 0x000D71AB File Offset: 0x000D53AB
		protected virtual void OnRemove(int index, object value)
		{
		}

		/// <summary>Performs additional custom processes when validating a value.</summary>
		/// <param name="value">The object to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060037A9 RID: 14249 RVA: 0x000D71AD File Offset: 0x000D53AD
		protected virtual void OnValidate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
		}

		/// <summary>Performs additional custom processes after setting a value in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
		/// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x060037AA RID: 14250 RVA: 0x000D71BD File Offset: 0x000D53BD
		protected virtual void OnSetComplete(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes after inserting a new element into the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x060037AB RID: 14251 RVA: 0x000D71BF File Offset: 0x000D53BF
		protected virtual void OnInsertComplete(int index, object value)
		{
		}

		/// <summary>Performs additional custom processes after clearing the contents of the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		// Token: 0x060037AC RID: 14252 RVA: 0x000D71C1 File Offset: 0x000D53C1
		protected virtual void OnClearComplete()
		{
		}

		/// <summary>Performs additional custom processes after removing an element from the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
		/// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
		// Token: 0x060037AD RID: 14253 RVA: 0x000D71C3 File Offset: 0x000D53C3
		protected virtual void OnRemoveComplete(int index, object value)
		{
		}

		// Token: 0x040018C0 RID: 6336
		private ArrayList list;
	}
}
