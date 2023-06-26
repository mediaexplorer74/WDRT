using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.CompilerServices
{
	/// <summary>Enables compilers to dynamically attach object fields to managed objects.</summary>
	/// <typeparam name="TKey">The reference type to which the field is attached.</typeparam>
	/// <typeparam name="TValue">The field's type. This must be a reference type.</typeparam>
	// Token: 0x020008E3 RID: 2275
	[ComVisible(false)]
	[__DynamicallyInvokable]
	public sealed class ConditionalWeakTable<TKey, TValue> where TKey : class where TValue : class
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.ConditionalWeakTable`2" /> class.</summary>
		// Token: 0x06005DFF RID: 24063 RVA: 0x0014B252 File Offset: 0x00149452
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public ConditionalWeakTable()
		{
			this._buckets = new int[0];
			this._entries = new ConditionalWeakTable<TKey, TValue>.Entry[0];
			this._freeList = -1;
			this._lock = new object();
			this.Resize();
		}

		/// <summary>Gets the value of the specified key.</summary>
		/// <param name="key">The key that represents an object with an attached property.</param>
		/// <param name="value">When this method returns, contains the attached property value. If <paramref name="key" /> is not found, <paramref name="value" /> contains the default value.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="key" /> is found; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06005E00 RID: 24064 RVA: 0x0014B28C File Offset: 0x0014948C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			object @lock = this._lock;
			bool flag2;
			lock (@lock)
			{
				this.VerifyIntegrity();
				flag2 = this.TryGetValueWorker(key, out value);
			}
			return flag2;
		}

		/// <summary>Adds a key to the table.</summary>
		/// <param name="key">The key to add. <paramref name="key" /> represents the object to which the property is attached.</param>
		/// <param name="value">The key's property value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> already exists.</exception>
		// Token: 0x06005E01 RID: 24065 RVA: 0x0014B2E4 File Offset: 0x001494E4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			object @lock = this._lock;
			lock (@lock)
			{
				this.VerifyIntegrity();
				this._invalid = true;
				int num = this.FindEntry(key);
				if (num != -1)
				{
					this._invalid = false;
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
				}
				this.CreateEntry(key, value);
				this._invalid = false;
			}
		}

		/// <summary>Removes a key and its value from the table.</summary>
		/// <param name="key">The key to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the key is found and removed; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06005E02 RID: 24066 RVA: 0x0014B364 File Offset: 0x00149564
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			object @lock = this._lock;
			bool flag2;
			lock (@lock)
			{
				this.VerifyIntegrity();
				this._invalid = true;
				int num = RuntimeHelpers.GetHashCode(key) & int.MaxValue;
				int num2 = num % this._buckets.Length;
				int num3 = -1;
				for (int num4 = this._buckets[num2]; num4 != -1; num4 = this._entries[num4].next)
				{
					if (this._entries[num4].hashCode == num && this._entries[num4].depHnd.GetPrimary() == key)
					{
						if (num3 == -1)
						{
							this._buckets[num2] = this._entries[num4].next;
						}
						else
						{
							this._entries[num3].next = this._entries[num4].next;
						}
						this._entries[num4].depHnd.Free();
						this._entries[num4].next = this._freeList;
						this._freeList = num4;
						this._invalid = false;
						return true;
					}
					num3 = num4;
				}
				this._invalid = false;
				flag2 = false;
			}
			return flag2;
		}

		/// <summary>Atomically searches for a specified key in the table and returns the corresponding value. If the key does not exist in the table, the method invokes a callback method to create a value that is bound to the specified key.</summary>
		/// <param name="key">The key to search for. <paramref name="key" /> represents the object to which the property is attached.</param>
		/// <param name="createValueCallback">A delegate to a method that can create a value for the given <paramref name="key" />. It has a single parameter of type TKey, and returns a value of type TValue.</param>
		/// <returns>The value attached to <paramref name="key" />, if <paramref name="key" /> already exists in the table; otherwise, the new value returned by the <paramref name="createValueCallback" /> delegate.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> or <paramref name="createValueCallback" /> is <see langword="null" />.</exception>
		// Token: 0x06005E03 RID: 24067 RVA: 0x0014B4E4 File Offset: 0x001496E4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public TValue GetValue(TKey key, ConditionalWeakTable<TKey, TValue>.CreateValueCallback createValueCallback)
		{
			if (createValueCallback == null)
			{
				throw new ArgumentNullException("createValueCallback");
			}
			TValue tvalue;
			if (this.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			TValue tvalue2 = createValueCallback(key);
			object @lock = this._lock;
			TValue tvalue3;
			lock (@lock)
			{
				this.VerifyIntegrity();
				this._invalid = true;
				if (this.TryGetValueWorker(key, out tvalue))
				{
					this._invalid = false;
					tvalue3 = tvalue;
				}
				else
				{
					this.CreateEntry(key, tvalue2);
					this._invalid = false;
					tvalue3 = tvalue2;
				}
			}
			return tvalue3;
		}

		/// <summary>Atomically searches for a specified key in the table and returns the corresponding value. If the key does not exist in the table, the method invokes the default constructor of the class that represents the table's value to create a value that is bound to the specified key.</summary>
		/// <param name="key">The key to search for. <paramref name="key" /> represents the object to which the property is attached.</param>
		/// <returns>The value that corresponds to <paramref name="key" />, if <paramref name="key" /> already exists in the table; otherwise, a new value created by the default constructor of the class defined by the <paramref name="TValue" /> generic type parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.  
		///
		///
		///
		///
		///  The class that represents the table's value does not define a default constructor.</exception>
		// Token: 0x06005E04 RID: 24068 RVA: 0x0014B57C File Offset: 0x0014977C
		[__DynamicallyInvokable]
		public TValue GetOrCreateValue(TKey key)
		{
			return this.GetValue(key, (TKey k) => Activator.CreateInstance<TValue>());
		}

		// Token: 0x06005E05 RID: 24069 RVA: 0x0014B5A4 File Offset: 0x001497A4
		[SecuritySafeCritical]
		[FriendAccessAllowed]
		internal TKey FindEquivalentKeyUnsafe(TKey key, out TValue value)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				for (int i = 0; i < this._buckets.Length; i++)
				{
					for (int num = this._buckets[i]; num != -1; num = this._entries[num].next)
					{
						object obj;
						object obj2;
						this._entries[num].depHnd.GetPrimaryAndSecondary(out obj, out obj2);
						if (object.Equals(obj, key))
						{
							value = (TValue)((object)obj2);
							return (TKey)((object)obj);
						}
					}
				}
			}
			value = default(TValue);
			return default(TKey);
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06005E06 RID: 24070 RVA: 0x0014B668 File Offset: 0x00149868
		internal ICollection<TKey> Keys
		{
			[SecuritySafeCritical]
			get
			{
				List<TKey> list = new List<TKey>();
				object @lock = this._lock;
				lock (@lock)
				{
					for (int i = 0; i < this._buckets.Length; i++)
					{
						for (int num = this._buckets[i]; num != -1; num = this._entries[num].next)
						{
							TKey tkey = (TKey)((object)this._entries[num].depHnd.GetPrimary());
							if (tkey != null)
							{
								list.Add(tkey);
							}
						}
					}
				}
				return list;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06005E07 RID: 24071 RVA: 0x0014B710 File Offset: 0x00149910
		internal ICollection<TValue> Values
		{
			[SecuritySafeCritical]
			get
			{
				List<TValue> list = new List<TValue>();
				object @lock = this._lock;
				lock (@lock)
				{
					for (int i = 0; i < this._buckets.Length; i++)
					{
						for (int num = this._buckets[i]; num != -1; num = this._entries[num].next)
						{
							object obj = null;
							object obj2 = null;
							this._entries[num].depHnd.GetPrimaryAndSecondary(out obj, out obj2);
							if (obj != null)
							{
								list.Add((TValue)((object)obj2));
							}
						}
					}
				}
				return list;
			}
		}

		// Token: 0x06005E08 RID: 24072 RVA: 0x0014B7BC File Offset: 0x001499BC
		[SecuritySafeCritical]
		internal void Clear()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				for (int i = 0; i < this._buckets.Length; i++)
				{
					this._buckets[i] = -1;
				}
				int j;
				for (j = 0; j < this._entries.Length; j++)
				{
					if (this._entries[j].depHnd.IsAllocated)
					{
						this._entries[j].depHnd.Free();
					}
					this._entries[j].next = j - 1;
				}
				this._freeList = j - 1;
			}
		}

		// Token: 0x06005E09 RID: 24073 RVA: 0x0014B870 File Offset: 0x00149A70
		[SecurityCritical]
		private bool TryGetValueWorker(TKey key, out TValue value)
		{
			int num = this.FindEntry(key);
			if (num != -1)
			{
				object obj = null;
				object obj2 = null;
				this._entries[num].depHnd.GetPrimaryAndSecondary(out obj, out obj2);
				if (obj != null)
				{
					value = (TValue)((object)obj2);
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06005E0A RID: 24074 RVA: 0x0014B8C0 File Offset: 0x00149AC0
		[SecurityCritical]
		private void CreateEntry(TKey key, TValue value)
		{
			if (this._freeList == -1)
			{
				this.Resize();
			}
			int num = RuntimeHelpers.GetHashCode(key) & int.MaxValue;
			int num2 = num % this._buckets.Length;
			int freeList = this._freeList;
			this._freeList = this._entries[freeList].next;
			this._entries[freeList].hashCode = num;
			this._entries[freeList].depHnd = new DependentHandle(key, value);
			this._entries[freeList].next = this._buckets[num2];
			this._buckets[num2] = freeList;
		}

		// Token: 0x06005E0B RID: 24075 RVA: 0x0014B970 File Offset: 0x00149B70
		[SecurityCritical]
		private void Resize()
		{
			int num = this._buckets.Length;
			bool flag = false;
			int i;
			for (i = 0; i < this._entries.Length; i++)
			{
				if (this._entries[i].depHnd.IsAllocated && this._entries[i].depHnd.GetPrimary() == null)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				num = HashHelpers.GetPrime((this._buckets.Length == 0) ? 6 : (this._buckets.Length * 2));
			}
			int num2 = -1;
			int[] array = new int[num];
			for (int j = 0; j < num; j++)
			{
				array[j] = -1;
			}
			ConditionalWeakTable<TKey, TValue>.Entry[] array2 = new ConditionalWeakTable<TKey, TValue>.Entry[num];
			for (i = 0; i < this._entries.Length; i++)
			{
				DependentHandle depHnd = this._entries[i].depHnd;
				if (depHnd.IsAllocated && depHnd.GetPrimary() != null)
				{
					int num3 = this._entries[i].hashCode % num;
					array2[i].depHnd = depHnd;
					array2[i].hashCode = this._entries[i].hashCode;
					array2[i].next = array[num3];
					array[num3] = i;
				}
				else
				{
					this._entries[i].depHnd.Free();
					array2[i].depHnd = default(DependentHandle);
					array2[i].next = num2;
					num2 = i;
				}
			}
			while (i != array2.Length)
			{
				array2[i].depHnd = default(DependentHandle);
				array2[i].next = num2;
				num2 = i;
				i++;
			}
			this._buckets = array;
			this._entries = array2;
			this._freeList = num2;
		}

		// Token: 0x06005E0C RID: 24076 RVA: 0x0014BB30 File Offset: 0x00149D30
		[SecurityCritical]
		private int FindEntry(TKey key)
		{
			int num = RuntimeHelpers.GetHashCode(key) & int.MaxValue;
			for (int num2 = this._buckets[num % this._buckets.Length]; num2 != -1; num2 = this._entries[num2].next)
			{
				if (this._entries[num2].hashCode == num && this._entries[num2].depHnd.GetPrimary() == key)
				{
					return num2;
				}
			}
			return -1;
		}

		// Token: 0x06005E0D RID: 24077 RVA: 0x0014BBAE File Offset: 0x00149DAE
		private void VerifyIntegrity()
		{
			if (this._invalid)
			{
				throw new InvalidOperationException(Environment.GetResourceString("CollectionCorrupted"));
			}
		}

		/// <summary>Ensures that resources are freed and other cleanup operations are performed when the garbage collector reclaims the <see cref="T:System.Runtime.CompilerServices.ConditionalWeakTable`2" /> object.</summary>
		// Token: 0x06005E0E RID: 24078 RVA: 0x0014BBC8 File Offset: 0x00149DC8
		[SecuritySafeCritical]
		protected override void Finalize()
		{
			try
			{
				if (!Environment.HasShutdownStarted)
				{
					if (this._lock != null)
					{
						object @lock = this._lock;
						lock (@lock)
						{
							if (!this._invalid)
							{
								ConditionalWeakTable<TKey, TValue>.Entry[] entries = this._entries;
								this._invalid = true;
								this._entries = null;
								this._buckets = null;
								for (int i = 0; i < entries.Length; i++)
								{
									entries[i].depHnd.Free();
								}
							}
						}
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x04002A46 RID: 10822
		private int[] _buckets;

		// Token: 0x04002A47 RID: 10823
		private ConditionalWeakTable<TKey, TValue>.Entry[] _entries;

		// Token: 0x04002A48 RID: 10824
		private int _freeList;

		// Token: 0x04002A49 RID: 10825
		private const int _initialCapacity = 5;

		// Token: 0x04002A4A RID: 10826
		private readonly object _lock;

		// Token: 0x04002A4B RID: 10827
		private bool _invalid;

		/// <summary>Represents a method that creates a non-default value to add as part of a key/value pair to a <see cref="T:System.Runtime.CompilerServices.ConditionalWeakTable`2" /> object.</summary>
		/// <param name="key">The key that belongs to the value to create.</param>
		/// <typeparam name="TKey" />
		/// <typeparam name="TValue" />
		/// <returns>An instance of a reference type that represents the value to attach to the specified key.</returns>
		// Token: 0x02000C86 RID: 3206
		// (Invoke) Token: 0x06007101 RID: 28929
		[__DynamicallyInvokable]
		public delegate TValue CreateValueCallback(TKey key);

		// Token: 0x02000C87 RID: 3207
		private struct Entry
		{
			// Token: 0x04003837 RID: 14391
			public DependentHandle depHnd;

			// Token: 0x04003838 RID: 14392
			public int hashCode;

			// Token: 0x04003839 RID: 14393
			public int next;
		}
	}
}
