using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a thread-safe collection of key/value pairs that can be accessed by multiple threads concurrently.</summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	// Token: 0x020004AC RID: 1196
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(Mscorlib_DictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x06003947 RID: 14663 RVA: 0x000DC3CC File Offset: 0x000DA5CC
		private static bool IsValueWriteAtomic()
		{
			Type typeFromHandle = typeof(TValue);
			if (typeFromHandle.IsClass)
			{
				return true;
			}
			switch (Type.GetTypeCode(typeFromHandle))
			{
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Single:
				return true;
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Double:
				return IntPtr.Size == 8;
			default:
				return false;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that is empty, has the default concurrency level, has the default initial capacity, and uses the default comparer for the key type.</summary>
		// Token: 0x06003948 RID: 14664 RVA: 0x000DC43B File Offset: 0x000DA63B
		[__DynamicallyInvokable]
		public ConcurrentDictionary()
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, EqualityComparer<TKey>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that is empty, has the specified concurrency level and capacity, and uses the default comparer for the key type.</summary>
		/// <param name="concurrencyLevel">The estimated number of threads that will update the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> concurrently.</param>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="concurrencyLevel" /> is less than 1.  
		/// -or-  
		/// <paramref name="capacity" /> is less than 0.</exception>
		// Token: 0x06003949 RID: 14665 RVA: 0x000DC450 File Offset: 0x000DA650
		[__DynamicallyInvokable]
		public ConcurrentDictionary(int concurrencyLevel, int capacity)
			: this(concurrencyLevel, capacity, false, EqualityComparer<TKey>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IEnumerable`1" />, has the default concurrency level, has the default initial capacity, and uses the default comparer for the key type.</summary>
		/// <param name="collection">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> or any of its keys is  <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collection" /> contains one or more duplicate keys.</exception>
		// Token: 0x0600394A RID: 14666 RVA: 0x000DC460 File Offset: 0x000DA660
		[__DynamicallyInvokable]
		public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
			: this(collection, EqualityComparer<TKey>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that is empty, has the default concurrency level and capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="comparer">The equality comparison implementation to use when comparing keys.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is <see langword="null" />.</exception>
		// Token: 0x0600394B RID: 14667 RVA: 0x000DC46E File Offset: 0x000DA66E
		[__DynamicallyInvokable]
		public ConcurrentDictionary(IEqualityComparer<TKey> comparer)
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.IEnumerable" /> has the default concurrency level, has the default initial capacity, and uses the specified  <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="collection">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> or <paramref name="comparer" /> is <see langword="null" />.</exception>
		// Token: 0x0600394C RID: 14668 RVA: 0x000DC47F File Offset: 0x000DA67F
		[__DynamicallyInvokable]
		public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
			: this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.IEnumerable" />, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="concurrencyLevel">The estimated number of threads that will update the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> concurrently.</param>
		/// <param name="collection">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> or <paramref name="comparer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="concurrencyLevel" /> is less than 1.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collection" /> contains one or more duplicate keys.</exception>
		// Token: 0x0600394D RID: 14669 RVA: 0x000DC49D File Offset: 0x000DA69D
		[__DynamicallyInvokable]
		public ConcurrentDictionary(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
			: this(concurrencyLevel, 31, false, comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000DC4D0 File Offset: 0x000DA6D0
		private void InitializeFromCollection(IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
			{
				if (keyValuePair.Key == null)
				{
					throw new ArgumentNullException("key");
				}
				TValue tvalue;
				if (!this.TryAddInternal(keyValuePair.Key, keyValuePair.Value, false, false, out tvalue))
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_SourceContainsDuplicateKeys"));
				}
			}
			if (this.m_budget == 0)
			{
				this.m_budget = this.m_tables.m_buckets.Length / this.m_tables.m_locks.Length;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that is empty, has the specified concurrency level, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="concurrencyLevel">The estimated number of threads that will update the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> concurrently.</param>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="concurrencyLevel" /> or <paramref name="capacity" /> is less than 1.</exception>
		// Token: 0x0600394F RID: 14671 RVA: 0x000DC584 File Offset: 0x000DA784
		[__DynamicallyInvokable]
		public ConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer)
			: this(concurrencyLevel, capacity, false, comparer)
		{
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x000DC590 File Offset: 0x000DA790
		internal ConcurrentDictionary(int concurrencyLevel, int capacity, bool growLockArray, IEqualityComparer<TKey> comparer)
		{
			if (concurrencyLevel < 1)
			{
				throw new ArgumentOutOfRangeException("concurrencyLevel", this.GetResource("ConcurrentDictionary_ConcurrencyLevelMustBePositive"));
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", this.GetResource("ConcurrentDictionary_CapacityMustNotBeNegative"));
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			if (capacity < concurrencyLevel)
			{
				capacity = concurrencyLevel;
			}
			object[] array = new object[concurrencyLevel];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new object();
			}
			int[] array2 = new int[array.Length];
			ConcurrentDictionary<TKey, TValue>.Node[] array3 = new ConcurrentDictionary<TKey, TValue>.Node[capacity];
			this.m_tables = new ConcurrentDictionary<TKey, TValue>.Tables(array3, array, array2, comparer);
			this.m_growLockArray = growLockArray;
			this.m_budget = array3.Length / array.Length;
		}

		/// <summary>Attempts to add the specified key and value to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be  <see langword="null" /> for reference types.</param>
		/// <returns>
		///   <see langword="true" /> if the key/value pair was added to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> successfully; <see langword="false" /> if the key already exists.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06003951 RID: 14673 RVA: 0x000DC640 File Offset: 0x000DA840
		[__DynamicallyInvokable]
		public bool TryAdd(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue tvalue;
			return this.TryAddInternal(key, value, false, true, out tvalue);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> contains the specified key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06003952 RID: 14674 RVA: 0x000DC66C File Offset: 0x000DA86C
		[__DynamicallyInvokable]
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		/// <summary>Attempts to remove and return the value that has the specified key from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <param name="key">The key of the element to remove and return.</param>
		/// <param name="value">When this method returns, contains the object removed from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, or the default value of  the <see langword="TValue" /> type if <paramref name="key" /> does not exist.</param>
		/// <returns>
		///   <see langword="true" /> if the object was removed successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  <see langword="null" />.</exception>
		// Token: 0x06003953 RID: 14675 RVA: 0x000DC698 File Offset: 0x000DA898
		[__DynamicallyInvokable]
		public bool TryRemove(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.TryRemoveInternal(key, out value, false, default(TValue));
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000DC6CC File Offset: 0x000DA8CC
		private bool TryRemoveInternal(TKey key, out TValue value, bool matchValue, TValue oldValue)
		{
			for (;;)
			{
				ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
				IEqualityComparer<TKey> comparer = tables.m_comparer;
				int num;
				int num2;
				this.GetBucketAndLockNo(comparer.GetHashCode(key), out num, out num2, tables.m_buckets.Length, tables.m_locks.Length);
				object obj = tables.m_locks[num2];
				lock (obj)
				{
					if (tables != this.m_tables)
					{
						continue;
					}
					ConcurrentDictionary<TKey, TValue>.Node node = null;
					ConcurrentDictionary<TKey, TValue>.Node node2 = tables.m_buckets[num];
					while (node2 != null)
					{
						if (comparer.Equals(node2.m_key, key))
						{
							if (matchValue && !EqualityComparer<TValue>.Default.Equals(oldValue, node2.m_value))
							{
								value = default(TValue);
								return false;
							}
							if (node == null)
							{
								Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables.m_buckets[num], node2.m_next);
							}
							else
							{
								node.m_next = node2.m_next;
							}
							value = node2.m_value;
							tables.m_countPerLock[num2]--;
							return true;
						}
						else
						{
							node = node2;
							node2 = node2.m_next;
						}
					}
				}
				break;
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Attempts to get the value associated with the specified key from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, contains the object from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> that has the specified key, or the default value of the type if the operation failed.</param>
		/// <returns>
		///   <see langword="true" /> if the key was found in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  <see langword="null" />.</exception>
		// Token: 0x06003955 RID: 14677 RVA: 0x000DC814 File Offset: 0x000DAA14
		[__DynamicallyInvokable]
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
			IEqualityComparer<TKey> comparer = tables.m_comparer;
			int num;
			int num2;
			this.GetBucketAndLockNo(comparer.GetHashCode(key), out num, out num2, tables.m_buckets.Length, tables.m_locks.Length);
			for (ConcurrentDictionary<TKey, TValue>.Node node = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref tables.m_buckets[num]); node != null; node = node.m_next)
			{
				if (comparer.Equals(node.m_key, key))
				{
					value = node.m_value;
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Updates the value associated with <paramref name="key" /> to <paramref name="newValue" /> if the existing value with <paramref name="key" /> is equal to <paramref name="comparisonValue" />.</summary>
		/// <param name="key">The key of the value that is compared with <paramref name="comparisonValue" /> and possibly replaced.</param>
		/// <param name="newValue">The value that replaces the value of the element that has the specified <paramref name="key" /> if the comparison results in equality.</param>
		/// <param name="comparisonValue">The value that is compared with the value of the element that has the specified <paramref name="key" />.</param>
		/// <returns>
		///   <see langword="true" /> if the value with <paramref name="key" /> was equal to <paramref name="comparisonValue" /> and was replaced with <paramref name="newValue" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06003956 RID: 14678 RVA: 0x000DC8B0 File Offset: 0x000DAAB0
		[__DynamicallyInvokable]
		public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			bool flag2;
			for (;;)
			{
				ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
				IEqualityComparer<TKey> comparer = tables.m_comparer;
				int hashCode = comparer.GetHashCode(key);
				int num;
				int num2;
				this.GetBucketAndLockNo(hashCode, out num, out num2, tables.m_buckets.Length, tables.m_locks.Length);
				object obj = tables.m_locks[num2];
				lock (obj)
				{
					if (tables != this.m_tables)
					{
						continue;
					}
					ConcurrentDictionary<TKey, TValue>.Node node = null;
					ConcurrentDictionary<TKey, TValue>.Node node2 = tables.m_buckets[num];
					while (node2 != null)
					{
						if (comparer.Equals(node2.m_key, key))
						{
							if (@default.Equals(node2.m_value, comparisonValue))
							{
								if (ConcurrentDictionary<TKey, TValue>.s_isValueWriteAtomic)
								{
									node2.m_value = newValue;
								}
								else
								{
									ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2.m_key, newValue, hashCode, node2.m_next);
									if (node == null)
									{
										tables.m_buckets[num] = node3;
									}
									else
									{
										node.m_next = node3;
									}
								}
								return true;
							}
							return false;
						}
						else
						{
							node = node2;
							node2 = node2.m_next;
						}
					}
					flag2 = false;
				}
				break;
			}
			return flag2;
		}

		/// <summary>Removes all keys and values from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		// Token: 0x06003957 RID: 14679 RVA: 0x000DC9F4 File Offset: 0x000DABF4
		[__DynamicallyInvokable]
		public void Clear()
		{
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				ConcurrentDictionary<TKey, TValue>.Tables tables = new ConcurrentDictionary<TKey, TValue>.Tables(new ConcurrentDictionary<TKey, TValue>.Node[31], this.m_tables.m_locks, new int[this.m_tables.m_countPerLock.Length], this.m_tables.m_comparer);
				this.m_tables = tables;
				this.m_budget = Math.Max(1, tables.m_buckets.Length / tables.m_locks.Length);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000DCA8C File Offset: 0x000DAC8C
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", this.GetResource("ConcurrentDictionary_IndexIsNegative"));
			}
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				int num2 = 0;
				int num3 = 0;
				while (num3 < this.m_tables.m_locks.Length && num2 >= 0)
				{
					num2 += this.m_tables.m_countPerLock[num3];
					num3++;
				}
				if (array.Length - num2 < index || num2 < 0)
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
				}
				this.CopyToPairs(array, index);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		/// <summary>Copies the key and value pairs stored in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> to a new array.</summary>
		/// <returns>A new array containing a snapshot of key and value pairs copied from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		// Token: 0x06003959 RID: 14681 RVA: 0x000DCB40 File Offset: 0x000DAD40
		[__DynamicallyInvokable]
		public KeyValuePair<TKey, TValue>[] ToArray()
		{
			int num = 0;
			checked
			{
				KeyValuePair<TKey, TValue>[] array;
				try
				{
					this.AcquireAllLocks(ref num);
					int num2 = 0;
					for (int i = 0; i < this.m_tables.m_locks.Length; i++)
					{
						num2 += this.m_tables.m_countPerLock[i];
					}
					if (num2 == 0)
					{
						array = Array.Empty<KeyValuePair<TKey, TValue>>();
					}
					else
					{
						KeyValuePair<TKey, TValue>[] array2 = new KeyValuePair<TKey, TValue>[num2];
						this.CopyToPairs(array2, 0);
						array = array2;
					}
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
				return array;
			}
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000DCBC4 File Offset: 0x000DADC4
		private void CopyToPairs(KeyValuePair<TKey, TValue>[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this.m_tables.m_buckets)
			{
				while (node != null)
				{
					array[index] = new KeyValuePair<TKey, TValue>(node.m_key, node.m_value);
					index++;
					node = node.m_next;
				}
			}
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x000DCC1C File Offset: 0x000DAE1C
		private void CopyToEntries(DictionaryEntry[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this.m_tables.m_buckets)
			{
				while (node != null)
				{
					array[index] = new DictionaryEntry(node.m_key, node.m_value);
					index++;
					node = node.m_next;
				}
			}
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000DCC80 File Offset: 0x000DAE80
		private void CopyToObjects(object[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this.m_tables.m_buckets)
			{
				while (node != null)
				{
					array[index] = new KeyValuePair<TKey, TValue>(node.m_key, node.m_value);
					index++;
					node = node.m_next;
				}
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		// Token: 0x0600395D RID: 14685 RVA: 0x000DCCD9 File Offset: 0x000DAED9
		[__DynamicallyInvokable]
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			ConcurrentDictionary<TKey, TValue>.Node[] buckets = this.m_tables.m_buckets;
			int num;
			for (int i = 0; i < buckets.Length; i = num + 1)
			{
				ConcurrentDictionary<TKey, TValue>.Node current;
				for (current = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref buckets[i]); current != null; current = current.m_next)
				{
					yield return new KeyValuePair<TKey, TValue>(current.m_key, current.m_value);
				}
				current = null;
				num = i;
			}
			yield break;
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000DCCE8 File Offset: 0x000DAEE8
		private bool TryAddInternal(TKey key, TValue value, bool updateIfExists, bool acquireLock, out TValue resultingValue)
		{
			ConcurrentDictionary<TKey, TValue>.Tables tables;
			IEqualityComparer<TKey> comparer;
			bool flag;
			bool flag3;
			for (;;)
			{
				tables = this.m_tables;
				comparer = tables.m_comparer;
				int hashCode = comparer.GetHashCode(key);
				int num;
				int num2;
				this.GetBucketAndLockNo(hashCode, out num, out num2, tables.m_buckets.Length, tables.m_locks.Length);
				flag = false;
				bool flag2 = false;
				flag3 = false;
				try
				{
					if (acquireLock)
					{
						Monitor.Enter(tables.m_locks[num2], ref flag2);
					}
					if (tables != this.m_tables)
					{
						continue;
					}
					int num3 = 0;
					ConcurrentDictionary<TKey, TValue>.Node node = null;
					for (ConcurrentDictionary<TKey, TValue>.Node node2 = tables.m_buckets[num]; node2 != null; node2 = node2.m_next)
					{
						if (comparer.Equals(node2.m_key, key))
						{
							if (updateIfExists)
							{
								if (ConcurrentDictionary<TKey, TValue>.s_isValueWriteAtomic)
								{
									node2.m_value = value;
								}
								else
								{
									ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2.m_key, value, hashCode, node2.m_next);
									if (node == null)
									{
										tables.m_buckets[num] = node3;
									}
									else
									{
										node.m_next = node3;
									}
								}
								resultingValue = value;
							}
							else
							{
								resultingValue = node2.m_value;
							}
							return false;
						}
						node = node2;
						num3++;
					}
					if (num3 > 100 && HashHelpers.IsWellKnownEqualityComparer(comparer))
					{
						flag = true;
						flag3 = true;
					}
					Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables.m_buckets[num], new ConcurrentDictionary<TKey, TValue>.Node(key, value, hashCode, tables.m_buckets[num]));
					checked
					{
						tables.m_countPerLock[num2]++;
						if (tables.m_countPerLock[num2] > this.m_budget)
						{
							flag = true;
						}
					}
				}
				finally
				{
					if (flag2)
					{
						Monitor.Exit(tables.m_locks[num2]);
					}
				}
				break;
			}
			if (flag)
			{
				if (flag3)
				{
					this.GrowTable(tables, (IEqualityComparer<TKey>)HashHelpers.GetRandomizedEqualityComparer(comparer), true, this.m_keyRehashCount);
				}
				else
				{
					this.GrowTable(tables, tables.m_comparer, false, this.m_keyRehashCount);
				}
			}
			resultingValue = value;
			return true;
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <param name="key">The key of the value to get or set.</param>
		/// <returns>The value of the key/value pair at the specified index.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  <see langword="null" />.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x1700088E RID: 2190
		[__DynamicallyInvokable]
		public TValue this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				TValue tvalue;
				if (!this.TryGetValue(key, out tvalue))
				{
					throw new KeyNotFoundException();
				}
				return tvalue;
			}
			[__DynamicallyInvokable]
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				TValue tvalue;
				this.TryAddInternal(key, value, true, true, out tvalue);
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06003961 RID: 14689 RVA: 0x000DCF20 File Offset: 0x000DB120
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				int num = 0;
				int countInternal;
				try
				{
					this.AcquireAllLocks(ref num);
					countInternal = this.GetCountInternal();
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
				return countInternal;
			}
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x000DCF5C File Offset: 0x000DB15C
		private int GetCountInternal()
		{
			int num = 0;
			for (int i = 0; i < this.m_tables.m_countPerLock.Length; i++)
			{
				num += this.m_tables.m_countPerLock[i];
			}
			return num;
		}

		/// <summary>Adds a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> by using the specified function if the key does not already exist. Returns the new value, or the existing value if the key exists.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="valueFactory">The function used to generate a value for the key.</param>
		/// <returns>The value for the key. This will be either the existing value for the key if the key is already in the dictionary, or the new value if the key was not in the dictionary.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> or <paramref name="valueFactory" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06003963 RID: 14691 RVA: 0x000DCF9C File Offset: 0x000DB19C
		[__DynamicallyInvokable]
		public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			TValue tvalue;
			if (this.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			this.TryAddInternal(key, valueFactory(key), false, true, out tvalue);
			return tvalue;
		}

		/// <summary>Adds a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> if the key does not already exist. Returns the new value, or the existing value if the key exists.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value to be added, if the key does not already exist.</param>
		/// <returns>The value for the key. This will be either the existing value for the key if the key is already in the dictionary, or the new value if the key was not in the dictionary.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06003964 RID: 14692 RVA: 0x000DCFEC File Offset: 0x000DB1EC
		[__DynamicallyInvokable]
		public TValue GetOrAdd(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue tvalue;
			this.TryAddInternal(key, value, false, true, out tvalue);
			return tvalue;
		}

		/// <summary>Adds a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> by using the specified function and an argument if the key does not already exist, or returns the existing value if the key exists.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="valueFactory">The function used to generate a value for the key.</param>
		/// <param name="factoryArgument">An argument value to pass into <paramref name="valueFactory" />.</param>
		/// <typeparam name="TArg">The type of an argument to pass into <paramref name="valueFactory" />.</typeparam>
		/// <returns>The value for the key. This will be either the existing value for the key if the key is already in the dictionary, or the new value if the key was not in the dictionary.</returns>
		// Token: 0x06003965 RID: 14693 RVA: 0x000DD01C File Offset: 0x000DB21C
		public TValue GetOrAdd<TArg>(TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			TValue tvalue;
			if (!this.TryGetValue(key, out tvalue))
			{
				this.TryAddInternal(key, valueFactory(key, factoryArgument), false, true, out tvalue);
			}
			return tvalue;
		}

		/// <summary>Uses the specified functions and argument to add a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> if the key does not already exist, or to update a key/value pair in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> if the key already exists.</summary>
		/// <param name="key">The key to be added or whose value should be updated.</param>
		/// <param name="addValueFactory">The function used to generate a value for an absent key.</param>
		/// <param name="updateValueFactory">The function used to generate a new value for an existing key based on the key's existing value.</param>
		/// <param name="factoryArgument">An argument to pass into <paramref name="addValueFactory" /> and <paramref name="updateValueFactory" />.</param>
		/// <typeparam name="TArg">The type of an argument to pass into <paramref name="addValueFactory" /> and <paramref name="updateValueFactory" />.</typeparam>
		/// <returns>The new value for the key. This will be either be the result of <paramref name="addValueFactory" /> (if the key was absent) or the result of <paramref name="updateValueFactory" /> (if the key was present).</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" />, <paramref name="addValueFactory" />, or <paramref name="updateValueFactory" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x06003966 RID: 14694 RVA: 0x000DD06C File Offset: 0x000DB26C
		public TValue AddOrUpdate<TArg>(TKey key, Func<TKey, TArg, TValue> addValueFactory, Func<TKey, TValue, TArg, TValue> updateValueFactory, TArg factoryArgument)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (addValueFactory == null)
			{
				throw new ArgumentNullException("addValueFactory");
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				TValue tvalue3;
				if (this.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue, factoryArgument);
					if (this.TryUpdate(key, tvalue2, tvalue))
					{
						break;
					}
				}
				else if (this.TryAddInternal(key, addValueFactory(key, factoryArgument), false, true, out tvalue3))
				{
					return tvalue3;
				}
			}
			return tvalue2;
		}

		/// <summary>Uses the specified functions to add a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> if the key does not already exist, or to update a key/value pair in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> if the key already exists.</summary>
		/// <param name="key">The key to be added or whose value should be updated</param>
		/// <param name="addValueFactory">The function used to generate a value for an absent key</param>
		/// <param name="updateValueFactory">The function used to generate a new value for an existing key based on the key's existing value</param>
		/// <returns>The new value for the key. This will be either be the result of <paramref name="addValueFactory" /> (if the key was absent) or the result of <paramref name="updateValueFactory" /> (if the key was present).</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" />, <paramref name="addValueFactory" />, or <paramref name="updateValueFactory" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06003967 RID: 14695 RVA: 0x000DD0E4 File Offset: 0x000DB2E4
		[__DynamicallyInvokable]
		public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (addValueFactory == null)
			{
				throw new ArgumentNullException("addValueFactory");
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				if (this.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue);
					if (this.TryUpdate(key, tvalue2, tvalue))
					{
						break;
					}
				}
				else
				{
					tvalue2 = addValueFactory(key);
					TValue tvalue3;
					if (this.TryAddInternal(key, tvalue2, false, true, out tvalue3))
					{
						return tvalue3;
					}
				}
			}
			return tvalue2;
		}

		/// <summary>Adds a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> if the key does not already exist, or updates a key/value pair in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> by using the specified function if the key already exists.</summary>
		/// <param name="key">The key to be added or whose value should be updated</param>
		/// <param name="addValue">The value to be added for an absent key</param>
		/// <param name="updateValueFactory">The function used to generate a new value for an existing key based on the key's existing value</param>
		/// <returns>The new value for the key. This will be either be <paramref name="addValue" /> (if the key was absent) or the result of <paramref name="updateValueFactory" /> (if the key was present).</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> or <paramref name="updateValueFactory" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06003968 RID: 14696 RVA: 0x000DD158 File Offset: 0x000DB358
		[__DynamicallyInvokable]
		public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				TValue tvalue3;
				if (this.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue);
					if (this.TryUpdate(key, tvalue2, tvalue))
					{
						break;
					}
				}
				else if (this.TryAddInternal(key, addValue, false, true, out tvalue3))
				{
					return tvalue3;
				}
			}
			return tvalue2;
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06003969 RID: 14697 RVA: 0x000DD1B8 File Offset: 0x000DB3B8
		[__DynamicallyInvokable]
		public bool IsEmpty
		{
			[__DynamicallyInvokable]
			get
			{
				int num = 0;
				try
				{
					this.AcquireAllLocks(ref num);
					for (int i = 0; i < this.m_tables.m_countPerLock.Length; i++)
					{
						if (this.m_tables.m_countPerLock[i] != 0)
						{
							return false;
						}
					}
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
				return true;
			}
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000DD220 File Offset: 0x000DB420
		[__DynamicallyInvokable]
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			if (!this.TryAdd(key, value))
			{
				throw new ArgumentException(this.GetResource("ConcurrentDictionary_KeyAlreadyExisted"));
			}
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000DD240 File Offset: 0x000DB440
		[__DynamicallyInvokable]
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			TValue tvalue;
			return this.TryRemove(key, out tvalue);
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A collection of keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600396C RID: 14700 RVA: 0x000DD256 File Offset: 0x000DB456
		[__DynamicallyInvokable]
		public ICollection<TKey> Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600396D RID: 14701 RVA: 0x000DD25E File Offset: 0x000DB45E
		[__DynamicallyInvokable]
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetKeys();
			}
		}

		/// <summary>Gets a collection that contains the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A collection that contains the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600396E RID: 14702 RVA: 0x000DD266 File Offset: 0x000DB466
		[__DynamicallyInvokable]
		public ICollection<TValue> Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600396F RID: 14703 RVA: 0x000DD26E File Offset: 0x000DB46E
		[__DynamicallyInvokable]
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000DD276 File Offset: 0x000DB476
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			((IDictionary<TKey, TValue>)this).Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000DD28C File Offset: 0x000DB48C
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			TValue tvalue;
			return this.TryGetValue(keyValuePair.Key, out tvalue) && EqualityComparer<TValue>.Default.Equals(tvalue, keyValuePair.Value);
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06003972 RID: 14706 RVA: 0x000DD2BE File Offset: 0x000DB4BE
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000DD2C4 File Offset: 0x000DB4C4
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			if (keyValuePair.Key == null)
			{
				throw new ArgumentNullException(this.GetResource("ConcurrentDictionary_ItemKeyIsNull"));
			}
			TValue tvalue;
			return this.TryRemoveInternal(keyValuePair.Key, out tvalue, true, keyValuePair.Value);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		// Token: 0x06003974 RID: 14708 RVA: 0x000DD307 File Offset: 0x000DB507
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Adds the specified key and value to the dictionary.</summary>
		/// <param name="key">The object to use as the key.</param>
		/// <param name="value">The object to use as the value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type  of the <see cref="T:System.Collections.Generic.Dictionary`2" />.  
		/// -or-  
		/// <paramref name="value" /> is of a type that is not assignable to the type of values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.  
		/// -or-  
		/// A value with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06003975 RID: 14709 RVA: 0x000DD310 File Offset: 0x000DB510
		[__DynamicallyInvokable]
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!(key is TKey))
			{
				throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
			}
			TValue tvalue;
			try
			{
				tvalue = (TValue)((object)value);
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
			}
			((IDictionary<TKey, TValue>)this).Add((TKey)((object)key), tvalue);
		}

		/// <summary>Gets a value that indicates the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06003976 RID: 14710 RVA: 0x000DD380 File Offset: 0x000DB580
		[__DynamicallyInvokable]
		bool IDictionary.Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Provides a <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Generic.IDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
		// Token: 0x06003977 RID: 14711 RVA: 0x000DD3A6 File Offset: 0x000DB5A6
		[__DynamicallyInvokable]
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new ConcurrentDictionary<TKey, TValue>.DictionaryEnumerator(this);
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.IDictionary`2" /> has a fixed size; otherwise, <see langword="false" />. For <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06003978 RID: 14712 RVA: 0x000DD3AE File Offset: 0x000DB5AE
		[__DynamicallyInvokable]
		bool IDictionary.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only; otherwise, <see langword="false" />. For <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06003979 RID: 14713 RVA: 0x000DD3B1 File Offset: 0x000DB5B1
		[__DynamicallyInvokable]
		bool IDictionary.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the keys of the  <see cref="T:System.Collections.Generic.IDictionary`2" />.</summary>
		/// <returns>An interface that contains the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x0600397A RID: 14714 RVA: 0x000DD3B4 File Offset: 0x000DB5B4
		[__DynamicallyInvokable]
		ICollection IDictionary.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetKeys();
			}
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x0600397B RID: 14715 RVA: 0x000DD3BC File Offset: 0x000DB5BC
		[__DynamicallyInvokable]
		void IDictionary.Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key is TKey)
			{
				TValue tvalue;
				this.TryRemove((TKey)((object)key), out tvalue);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An interface that contains the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600397C RID: 14716 RVA: 0x000DD3EE File Offset: 0x000DB5EE
		[__DynamicallyInvokable]
		ICollection IDictionary.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetValues();
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <param name="key">The key of the value to get or set.</param>
		/// <returns>The value associated with the specified key, or  <see langword="null" /> if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type or the value type of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</exception>
		// Token: 0x1700089A RID: 2202
		[__DynamicallyInvokable]
		object IDictionary.this[object key]
		{
			[__DynamicallyInvokable]
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				TValue tvalue;
				if (key is TKey && this.TryGetValue((TKey)((object)key), out tvalue))
				{
					return tvalue;
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (!(key is TKey))
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
				}
				if (!(value is TValue))
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
				}
				this[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" />.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x0600397F RID: 14719 RVA: 0x000DD494 File Offset: 0x000DB694
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", this.GetResource("ConcurrentDictionary_IndexIsNegative"));
			}
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
				int num2 = 0;
				int num3 = 0;
				while (num3 < tables.m_locks.Length && num2 >= 0)
				{
					num2 += tables.m_countPerLock[num3];
					num3++;
				}
				if (array.Length - num2 < index || num2 < 0)
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
				}
				KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
				if (array2 != null)
				{
					this.CopyToPairs(array2, index);
				}
				else
				{
					DictionaryEntry[] array3 = array as DictionaryEntry[];
					if (array3 != null)
					{
						this.CopyToEntries(array3, index);
					}
					else
					{
						object[] array4 = array as object[];
						if (array4 == null)
						{
							throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayIncorrectType"), "array");
						}
						this.CopyToObjects(array4, index);
					}
				}
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot.</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />. For <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> this property always returns <see langword="false" />.</returns>
		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06003980 RID: 14720 RVA: 0x000DD598 File Offset: 0x000DB798
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. This property is not supported.</summary>
		/// <returns>Always returns null.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06003981 RID: 14721 RVA: 0x000DD59B File Offset: 0x000DB79B
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported"));
			}
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000DD5AC File Offset: 0x000DB7AC
		private void GrowTable(ConcurrentDictionary<TKey, TValue>.Tables tables, IEqualityComparer<TKey> newComparer, bool regenerateHashKeys, int rehashCount)
		{
			int num = 0;
			try
			{
				this.AcquireLocks(0, 1, ref num);
				if (regenerateHashKeys && rehashCount == this.m_keyRehashCount)
				{
					tables = this.m_tables;
				}
				else
				{
					if (tables != this.m_tables)
					{
						return;
					}
					long num2 = 0L;
					for (int i = 0; i < tables.m_countPerLock.Length; i++)
					{
						num2 += (long)tables.m_countPerLock[i];
					}
					if (num2 < (long)(tables.m_buckets.Length / 4))
					{
						this.m_budget = 2 * this.m_budget;
						if (this.m_budget < 0)
						{
							this.m_budget = int.MaxValue;
						}
						return;
					}
				}
				int num3 = 0;
				bool flag = false;
				object[] array;
				checked
				{
					try
					{
						num3 = tables.m_buckets.Length * 2 + 1;
						while (num3 % 3 == 0 || num3 % 5 == 0 || num3 % 7 == 0)
						{
							num3 += 2;
						}
						if (num3 > 2146435071)
						{
							flag = true;
						}
					}
					catch (OverflowException)
					{
						flag = true;
					}
					if (flag)
					{
						num3 = 2146435071;
						this.m_budget = int.MaxValue;
					}
					this.AcquireLocks(1, tables.m_locks.Length, ref num);
					array = tables.m_locks;
				}
				if (this.m_growLockArray && tables.m_locks.Length < 1024)
				{
					array = new object[tables.m_locks.Length * 2];
					Array.Copy(tables.m_locks, array, tables.m_locks.Length);
					for (int j = tables.m_locks.Length; j < array.Length; j++)
					{
						array[j] = new object();
					}
				}
				ConcurrentDictionary<TKey, TValue>.Node[] array2 = new ConcurrentDictionary<TKey, TValue>.Node[num3];
				int[] array3 = new int[array.Length];
				for (int k = 0; k < tables.m_buckets.Length; k++)
				{
					checked
					{
						ConcurrentDictionary<TKey, TValue>.Node next;
						for (ConcurrentDictionary<TKey, TValue>.Node node = tables.m_buckets[k]; node != null; node = next)
						{
							next = node.m_next;
							int num4 = node.m_hashcode;
							if (regenerateHashKeys)
							{
								num4 = newComparer.GetHashCode(node.m_key);
							}
							int num5;
							int num6;
							this.GetBucketAndLockNo(num4, out num5, out num6, array2.Length, array.Length);
							array2[num5] = new ConcurrentDictionary<TKey, TValue>.Node(node.m_key, node.m_value, num4, array2[num5]);
							array3[num6]++;
						}
					}
				}
				if (regenerateHashKeys)
				{
					this.m_keyRehashCount++;
				}
				this.m_budget = Math.Max(1, array2.Length / array.Length);
				this.m_tables = new ConcurrentDictionary<TKey, TValue>.Tables(array2, array, array3, newComparer);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000DD834 File Offset: 0x000DBA34
		private void GetBucketAndLockNo(int hashcode, out int bucketNo, out int lockNo, int bucketCount, int lockCount)
		{
			bucketNo = (hashcode & int.MaxValue) % bucketCount;
			lockNo = bucketNo % lockCount;
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06003984 RID: 14724 RVA: 0x000DD849 File Offset: 0x000DBA49
		private static int DefaultConcurrencyLevel
		{
			get
			{
				return PlatformHelper.ProcessorCount;
			}
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000DD850 File Offset: 0x000DBA50
		private void AcquireAllLocks(ref int locksAcquired)
		{
			if (CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentDictionary_AcquiringAllLocks(this.m_tables.m_buckets.Length);
			}
			this.AcquireLocks(0, 1, ref locksAcquired);
			this.AcquireLocks(1, this.m_tables.m_locks.Length, ref locksAcquired);
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000DD8A4 File Offset: 0x000DBAA4
		private void AcquireLocks(int fromInclusive, int toExclusive, ref int locksAcquired)
		{
			object[] locks = this.m_tables.m_locks;
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				bool flag = false;
				try
				{
					Monitor.Enter(locks[i], ref flag);
				}
				finally
				{
					if (flag)
					{
						locksAcquired++;
					}
				}
			}
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000DD8F4 File Offset: 0x000DBAF4
		private void ReleaseLocks(int fromInclusive, int toExclusive)
		{
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				Monitor.Exit(this.m_tables.m_locks[i]);
			}
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000DD924 File Offset: 0x000DBB24
		private ReadOnlyCollection<TKey> GetKeys()
		{
			int num = 0;
			ReadOnlyCollection<TKey> readOnlyCollection;
			try
			{
				this.AcquireAllLocks(ref num);
				int countInternal = this.GetCountInternal();
				if (countInternal < 0)
				{
					throw new OutOfMemoryException();
				}
				List<TKey> list = new List<TKey>(countInternal);
				for (int i = 0; i < this.m_tables.m_buckets.Length; i++)
				{
					for (ConcurrentDictionary<TKey, TValue>.Node node = this.m_tables.m_buckets[i]; node != null; node = node.m_next)
					{
						list.Add(node.m_key);
					}
				}
				readOnlyCollection = new ReadOnlyCollection<TKey>(list);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
			return readOnlyCollection;
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000DD9C4 File Offset: 0x000DBBC4
		private ReadOnlyCollection<TValue> GetValues()
		{
			int num = 0;
			ReadOnlyCollection<TValue> readOnlyCollection;
			try
			{
				this.AcquireAllLocks(ref num);
				int countInternal = this.GetCountInternal();
				if (countInternal < 0)
				{
					throw new OutOfMemoryException();
				}
				List<TValue> list = new List<TValue>(countInternal);
				for (int i = 0; i < this.m_tables.m_buckets.Length; i++)
				{
					for (ConcurrentDictionary<TKey, TValue>.Node node = this.m_tables.m_buckets[i]; node != null; node = node.m_next)
					{
						list.Add(node.m_value);
					}
				}
				readOnlyCollection = new ReadOnlyCollection<TValue>(list);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
			return readOnlyCollection;
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x000DDA64 File Offset: 0x000DBC64
		[Conditional("DEBUG")]
		private void Assert(bool condition)
		{
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000DDA66 File Offset: 0x000DBC66
		private string GetResource(string key)
		{
			return Environment.GetResourceString(key);
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000DDA70 File Offset: 0x000DBC70
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
			this.m_serializationArray = this.ToArray();
			this.m_serializationConcurrencyLevel = tables.m_locks.Length;
			this.m_serializationCapacity = tables.m_buckets.Length;
			this.m_comparer = (IEqualityComparer<TKey>)HashHelpers.GetEqualityComparerForSerialization(tables.m_comparer);
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x000DDAC4 File Offset: 0x000DBCC4
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			KeyValuePair<TKey, TValue>[] serializationArray = this.m_serializationArray;
			ConcurrentDictionary<TKey, TValue>.Node[] array = new ConcurrentDictionary<TKey, TValue>.Node[this.m_serializationCapacity];
			int[] array2 = new int[this.m_serializationConcurrencyLevel];
			object[] array3 = new object[this.m_serializationConcurrencyLevel];
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i] = new object();
			}
			this.m_tables = new ConcurrentDictionary<TKey, TValue>.Tables(array, array3, array2, this.m_comparer);
			this.InitializeFromCollection(serializationArray);
			this.m_serializationArray = null;
		}

		// Token: 0x04001923 RID: 6435
		[NonSerialized]
		private volatile ConcurrentDictionary<TKey, TValue>.Tables m_tables;

		// Token: 0x04001924 RID: 6436
		internal IEqualityComparer<TKey> m_comparer;

		// Token: 0x04001925 RID: 6437
		[NonSerialized]
		private readonly bool m_growLockArray;

		// Token: 0x04001926 RID: 6438
		[OptionalField]
		private int m_keyRehashCount;

		// Token: 0x04001927 RID: 6439
		[NonSerialized]
		private int m_budget;

		// Token: 0x04001928 RID: 6440
		private KeyValuePair<TKey, TValue>[] m_serializationArray;

		// Token: 0x04001929 RID: 6441
		private int m_serializationConcurrencyLevel;

		// Token: 0x0400192A RID: 6442
		private int m_serializationCapacity;

		// Token: 0x0400192B RID: 6443
		private const int DEFAULT_CAPACITY = 31;

		// Token: 0x0400192C RID: 6444
		private const int MAX_LOCK_NUMBER = 1024;

		// Token: 0x0400192D RID: 6445
		private static readonly bool s_isValueWriteAtomic = ConcurrentDictionary<TKey, TValue>.IsValueWriteAtomic();

		// Token: 0x02000BBE RID: 3006
		private class Tables
		{
			// Token: 0x06006E87 RID: 28295 RVA: 0x0017E879 File Offset: 0x0017CA79
			internal Tables(ConcurrentDictionary<TKey, TValue>.Node[] buckets, object[] locks, int[] countPerLock, IEqualityComparer<TKey> comparer)
			{
				this.m_buckets = buckets;
				this.m_locks = locks;
				this.m_countPerLock = countPerLock;
				this.m_comparer = comparer;
			}

			// Token: 0x04003594 RID: 13716
			internal readonly ConcurrentDictionary<TKey, TValue>.Node[] m_buckets;

			// Token: 0x04003595 RID: 13717
			internal readonly object[] m_locks;

			// Token: 0x04003596 RID: 13718
			internal volatile int[] m_countPerLock;

			// Token: 0x04003597 RID: 13719
			internal readonly IEqualityComparer<TKey> m_comparer;
		}

		// Token: 0x02000BBF RID: 3007
		private class Node
		{
			// Token: 0x06006E88 RID: 28296 RVA: 0x0017E8A0 File Offset: 0x0017CAA0
			internal Node(TKey key, TValue value, int hashcode, ConcurrentDictionary<TKey, TValue>.Node next)
			{
				this.m_key = key;
				this.m_value = value;
				this.m_next = next;
				this.m_hashcode = hashcode;
			}

			// Token: 0x04003598 RID: 13720
			internal TKey m_key;

			// Token: 0x04003599 RID: 13721
			internal TValue m_value;

			// Token: 0x0400359A RID: 13722
			internal volatile ConcurrentDictionary<TKey, TValue>.Node m_next;

			// Token: 0x0400359B RID: 13723
			internal int m_hashcode;
		}

		// Token: 0x02000BC0 RID: 3008
		private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006E89 RID: 28297 RVA: 0x0017E8C7 File Offset: 0x0017CAC7
			internal DictionaryEnumerator(ConcurrentDictionary<TKey, TValue> dictionary)
			{
				this.m_enumerator = dictionary.GetEnumerator();
			}

			// Token: 0x170012DD RID: 4829
			// (get) Token: 0x06006E8A RID: 28298 RVA: 0x0017E8DC File Offset: 0x0017CADC
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this.m_enumerator.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x170012DE RID: 4830
			// (get) Token: 0x06006E8B RID: 28299 RVA: 0x0017E920 File Offset: 0x0017CB20
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x170012DF RID: 4831
			// (get) Token: 0x06006E8C RID: 28300 RVA: 0x0017E948 File Offset: 0x0017CB48
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x170012E0 RID: 4832
			// (get) Token: 0x06006E8D RID: 28301 RVA: 0x0017E96D File Offset: 0x0017CB6D
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06006E8E RID: 28302 RVA: 0x0017E97A File Offset: 0x0017CB7A
			public bool MoveNext()
			{
				return this.m_enumerator.MoveNext();
			}

			// Token: 0x06006E8F RID: 28303 RVA: 0x0017E987 File Offset: 0x0017CB87
			public void Reset()
			{
				this.m_enumerator.Reset();
			}

			// Token: 0x0400359C RID: 13724
			private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;
		}
	}
}
