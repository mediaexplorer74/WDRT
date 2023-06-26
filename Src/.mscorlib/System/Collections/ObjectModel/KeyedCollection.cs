using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections.ObjectModel
{
	/// <summary>Provides the abstract base class for a collection whose keys are embedded in the values.</summary>
	/// <typeparam name="TKey">The type of keys in the collection.</typeparam>
	/// <typeparam name="TItem">The type of items in the collection.</typeparam>
	// Token: 0x020004B7 RID: 1207
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(Mscorlib_KeyedCollectionDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class KeyedCollection<TKey, TItem> : Collection<TItem>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the default equality comparer.</summary>
		// Token: 0x06003A2A RID: 14890 RVA: 0x000DEF78 File Offset: 0x000DD178
		[__DynamicallyInvokable]
		protected KeyedCollection()
			: this(null, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the specified equality comparer.</summary>
		/// <param name="comparer">The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface to use when comparing keys, or <see langword="null" /> to use the default equality comparer for the type of the key, obtained from <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.</param>
		// Token: 0x06003A2B RID: 14891 RVA: 0x000DEF82 File Offset: 0x000DD182
		[__DynamicallyInvokable]
		protected KeyedCollection(IEqualityComparer<TKey> comparer)
			: this(comparer, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the specified equality comparer and creates a lookup dictionary when the specified threshold is exceeded.</summary>
		/// <param name="comparer">The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface to use when comparing keys, or <see langword="null" /> to use the default equality comparer for the type of the key, obtained from <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.</param>
		/// <param name="dictionaryCreationThreshold">The number of elements the collection can hold without creating a lookup dictionary (0 creates the lookup dictionary when the first item is added), or -1 to specify that a lookup dictionary is never created.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dictionaryCreationThreshold" /> is less than -1.</exception>
		// Token: 0x06003A2C RID: 14892 RVA: 0x000DEF8C File Offset: 0x000DD18C
		[__DynamicallyInvokable]
		protected KeyedCollection(IEqualityComparer<TKey> comparer, int dictionaryCreationThreshold)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TKey>.Default;
			}
			if (dictionaryCreationThreshold == -1)
			{
				dictionaryCreationThreshold = int.MaxValue;
			}
			if (dictionaryCreationThreshold < -1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.dictionaryCreationThreshold, ExceptionResource.ArgumentOutOfRange_InvalidThreshold);
			}
			this.comparer = comparer;
			this.threshold = dictionaryCreationThreshold;
		}

		/// <summary>Gets the generic equality comparer that is used to determine equality of keys in the collection.</summary>
		/// <returns>The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface that is used to determine equality of keys in the collection.</returns>
		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x000DEFC3 File Offset: 0x000DD1C3
		[__DynamicallyInvokable]
		public IEqualityComparer<TKey> Comparer
		{
			[__DynamicallyInvokable]
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Gets the element with the specified key.</summary>
		/// <param name="key">The key of the element to get.</param>
		/// <returns>The element with the specified key. If an element with the specified key is not found, an exception is thrown.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">An element with the specified key does not exist in the collection.</exception>
		// Token: 0x170008CC RID: 2252
		[__DynamicallyInvokable]
		public TItem this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				if (this.dict != null)
				{
					return this.dict[key];
				}
				foreach (TItem titem in base.Items)
				{
					if (this.comparer.Equals(this.GetKeyForItem(titem), key))
					{
						return titem;
					}
				}
				ThrowHelper.ThrowKeyNotFoundException();
				return default(TItem);
			}
		}

		/// <summary>Determines whether the collection contains an element with the specified key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06003A2F RID: 14895 RVA: 0x000DF060 File Offset: 0x000DD260
		[__DynamicallyInvokable]
		public bool Contains(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.dict != null)
			{
				return this.dict.ContainsKey(key);
			}
			if (key != null)
			{
				foreach (TItem titem in base.Items)
				{
					if (this.comparer.Equals(this.GetKeyForItem(titem), key))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x000DF0F0 File Offset: 0x000DD2F0
		private bool ContainsItem(TItem item)
		{
			TKey keyForItem;
			if (this.dict == null || (keyForItem = this.GetKeyForItem(item)) == null)
			{
				return base.Items.Contains(item);
			}
			TItem titem;
			bool flag = this.dict.TryGetValue(keyForItem, out titem);
			return flag && EqualityComparer<TItem>.Default.Equals(titem, item);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the element is successfully removed; otherwise, <see langword="false" />.  This method also returns <see langword="false" /> if <paramref name="key" /> is not found in the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06003A31 RID: 14897 RVA: 0x000DF144 File Offset: 0x000DD344
		[__DynamicallyInvokable]
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.dict != null)
			{
				return this.dict.ContainsKey(key) && base.Remove(this.dict[key]);
			}
			if (key != null)
			{
				for (int i = 0; i < base.Items.Count; i++)
				{
					if (this.comparer.Equals(this.GetKeyForItem(base.Items[i]), key))
					{
						this.RemoveItem(i);
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Gets the lookup dictionary of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		/// <returns>The lookup dictionary of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />, if it exists; otherwise, <see langword="null" />.</returns>
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06003A32 RID: 14898 RVA: 0x000DF1D2 File Offset: 0x000DD3D2
		[__DynamicallyInvokable]
		protected IDictionary<TKey, TItem> Dictionary
		{
			[__DynamicallyInvokable]
			get
			{
				return this.dict;
			}
		}

		/// <summary>Changes the key associated with the specified element in the lookup dictionary.</summary>
		/// <param name="item">The element to change the key of.</param>
		/// <param name="newKey">The new key for <paramref name="item" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="item" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="item" /> is not found.  
		/// -or-  
		/// <paramref name="key" /> already exists in the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</exception>
		// Token: 0x06003A33 RID: 14899 RVA: 0x000DF1DC File Offset: 0x000DD3DC
		[__DynamicallyInvokable]
		protected void ChangeItemKey(TItem item, TKey newKey)
		{
			if (!this.ContainsItem(item))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_ItemNotExist);
			}
			TKey keyForItem = this.GetKeyForItem(item);
			if (!this.comparer.Equals(keyForItem, newKey))
			{
				if (newKey != null)
				{
					this.AddKey(newKey, item);
				}
				if (keyForItem != null)
				{
					this.RemoveKey(keyForItem);
				}
			}
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		// Token: 0x06003A34 RID: 14900 RVA: 0x000DF22F File Offset: 0x000DD42F
		[__DynamicallyInvokable]
		protected override void ClearItems()
		{
			base.ClearItems();
			if (this.dict != null)
			{
				this.dict.Clear();
			}
			this.keyCount = 0;
		}

		/// <summary>When implemented in a derived class, extracts the key from the specified element.</summary>
		/// <param name="item">The element from which to extract the key.</param>
		/// <returns>The key for the specified element.</returns>
		// Token: 0x06003A35 RID: 14901
		[__DynamicallyInvokable]
		protected abstract TKey GetKeyForItem(TItem item);

		/// <summary>Inserts an element into the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06003A36 RID: 14902 RVA: 0x000DF254 File Offset: 0x000DD454
		[__DynamicallyInvokable]
		protected override void InsertItem(int index, TItem item)
		{
			TKey keyForItem = this.GetKeyForItem(item);
			if (keyForItem != null)
			{
				this.AddKey(keyForItem, item);
			}
			base.InsertItem(index, item);
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		/// <param name="index">The index of the element to remove.</param>
		// Token: 0x06003A37 RID: 14903 RVA: 0x000DF284 File Offset: 0x000DD484
		[__DynamicallyInvokable]
		protected override void RemoveItem(int index)
		{
			TKey keyForItem = this.GetKeyForItem(base.Items[index]);
			if (keyForItem != null)
			{
				this.RemoveKey(keyForItem);
			}
			base.RemoveItem(index);
		}

		/// <summary>Replaces the item at the specified index with the specified item.</summary>
		/// <param name="index">The zero-based index of the item to be replaced.</param>
		/// <param name="item">The new item.</param>
		// Token: 0x06003A38 RID: 14904 RVA: 0x000DF2BC File Offset: 0x000DD4BC
		[__DynamicallyInvokable]
		protected override void SetItem(int index, TItem item)
		{
			TKey keyForItem = this.GetKeyForItem(item);
			TKey keyForItem2 = this.GetKeyForItem(base.Items[index]);
			if (this.comparer.Equals(keyForItem2, keyForItem))
			{
				if (keyForItem != null && this.dict != null)
				{
					this.dict[keyForItem] = item;
				}
			}
			else
			{
				if (keyForItem != null)
				{
					this.AddKey(keyForItem, item);
				}
				if (keyForItem2 != null)
				{
					this.RemoveKey(keyForItem2);
				}
			}
			base.SetItem(index, item);
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x000DF33C File Offset: 0x000DD53C
		private void AddKey(TKey key, TItem item)
		{
			if (this.dict != null)
			{
				this.dict.Add(key, item);
				return;
			}
			if (this.keyCount == this.threshold)
			{
				this.CreateDictionary();
				this.dict.Add(key, item);
				return;
			}
			if (this.Contains(key))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
			}
			this.keyCount++;
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000DF3A0 File Offset: 0x000DD5A0
		private void CreateDictionary()
		{
			this.dict = new Dictionary<TKey, TItem>(this.comparer);
			foreach (TItem titem in base.Items)
			{
				TKey keyForItem = this.GetKeyForItem(titem);
				if (keyForItem != null)
				{
					this.dict.Add(keyForItem, titem);
				}
			}
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000DF414 File Offset: 0x000DD614
		private void RemoveKey(TKey key)
		{
			if (this.dict != null)
			{
				this.dict.Remove(key);
				return;
			}
			this.keyCount--;
		}

		// Token: 0x04001943 RID: 6467
		private const int defaultThreshold = 0;

		// Token: 0x04001944 RID: 6468
		private IEqualityComparer<TKey> comparer;

		// Token: 0x04001945 RID: 6469
		private Dictionary<TKey, TItem> dict;

		// Token: 0x04001946 RID: 6470
		private int keyCount;

		// Token: 0x04001947 RID: 6471
		private int threshold;
	}
}
