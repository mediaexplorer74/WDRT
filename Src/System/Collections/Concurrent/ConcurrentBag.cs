using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a thread-safe, unordered collection of objects.</summary>
	/// <typeparam name="T">The type of the elements to be stored in the collection.</typeparam>
	// Token: 0x020003D2 RID: 978
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(SystemThreadingCollection_IProducerConsumerCollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class ConcurrentBag<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> class.</summary>
		// Token: 0x06002580 RID: 9600 RVA: 0x000AE5D6 File Offset: 0x000AC7D6
		[global::__DynamicallyInvokable]
		public ConcurrentBag()
		{
			this.Initialize(null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> class that contains elements copied from the specified collection.</summary>
		/// <param name="collection">The collection whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x06002581 RID: 9601 RVA: 0x000AE5E5 File Offset: 0x000AC7E5
		[global::__DynamicallyInvokable]
		public ConcurrentBag(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection", SR.GetString("ConcurrentBag_Ctor_ArgumentNullException"));
			}
			this.Initialize(collection);
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000AE60C File Offset: 0x000AC80C
		private void Initialize(IEnumerable<T> collection)
		{
			this.m_locals = new ThreadLocal<ConcurrentBag<T>.ThreadLocalList>();
			if (collection != null)
			{
				ConcurrentBag<T>.ThreadLocalList threadList = this.GetThreadList(true);
				foreach (T t in collection)
				{
					threadList.Add(t, false);
				}
			}
		}

		/// <summary>Adds an object to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <param name="item">The object to be added to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
		// Token: 0x06002583 RID: 9603 RVA: 0x000AE66C File Offset: 0x000AC86C
		[global::__DynamicallyInvokable]
		public void Add(T item)
		{
			ConcurrentBag<T>.ThreadLocalList threadList = this.GetThreadList(true);
			this.AddInternal(threadList, item);
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000AE68C File Offset: 0x000AC88C
		private void AddInternal(ConcurrentBag<T>.ThreadLocalList list, T item)
		{
			bool flag = false;
			try
			{
				Interlocked.Exchange(ref list.m_currentOp, 1);
				if (list.Count < 2 || this.m_needSync)
				{
					list.m_currentOp = 0;
					Monitor.Enter(list, ref flag);
				}
				list.Add(item, flag);
			}
			finally
			{
				list.m_currentOp = 0;
				if (flag)
				{
					Monitor.Exit(list);
				}
			}
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000AE6F8 File Offset: 0x000AC8F8
		[global::__DynamicallyInvokable]
		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Add(item);
			return true;
		}

		/// <summary>Attempts to remove and return an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <param name="result">When this method returns, <paramref name="result" /> contains the object removed from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> or the default value of <typeparamref name="T" /> if the bag is empty.</param>
		/// <returns>true if an object was removed successfully; otherwise, false.</returns>
		// Token: 0x06002586 RID: 9606 RVA: 0x000AE702 File Offset: 0x000AC902
		[global::__DynamicallyInvokable]
		public bool TryTake(out T result)
		{
			return this.TryTakeOrPeek(out result, true);
		}

		/// <summary>Attempts to return an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> without removing it.</summary>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> or the default value of <typeparamref name="T" /> if the operation failed.</param>
		/// <returns>true if an object was returned successfully; otherwise, false.</returns>
		// Token: 0x06002587 RID: 9607 RVA: 0x000AE70C File Offset: 0x000AC90C
		[global::__DynamicallyInvokable]
		public bool TryPeek(out T result)
		{
			return this.TryTakeOrPeek(out result, false);
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000AE718 File Offset: 0x000AC918
		private bool TryTakeOrPeek(out T result, bool take)
		{
			ConcurrentBag<T>.ThreadLocalList threadList = this.GetThreadList(false);
			if (threadList == null || threadList.Count == 0)
			{
				return this.Steal(out result, take);
			}
			bool flag = false;
			try
			{
				if (take)
				{
					Interlocked.Exchange(ref threadList.m_currentOp, 2);
					if (threadList.Count <= 2 || this.m_needSync)
					{
						threadList.m_currentOp = 0;
						Monitor.Enter(threadList, ref flag);
						if (threadList.Count == 0)
						{
							if (flag)
							{
								try
								{
								}
								finally
								{
									flag = false;
									Monitor.Exit(threadList);
								}
							}
							return this.Steal(out result, true);
						}
					}
					threadList.Remove(out result);
				}
				else if (!threadList.Peek(out result))
				{
					return this.Steal(out result, false);
				}
			}
			finally
			{
				threadList.m_currentOp = 0;
				if (flag)
				{
					Monitor.Exit(threadList);
				}
			}
			return true;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000AE7E8 File Offset: 0x000AC9E8
		private ConcurrentBag<T>.ThreadLocalList GetThreadList(bool forceCreate)
		{
			ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_locals.Value;
			if (threadLocalList != null)
			{
				return threadLocalList;
			}
			if (forceCreate)
			{
				object globalListsLock = this.GlobalListsLock;
				lock (globalListsLock)
				{
					if (this.m_headList == null)
					{
						threadLocalList = new ConcurrentBag<T>.ThreadLocalList(Thread.CurrentThread);
						this.m_headList = threadLocalList;
						this.m_tailList = threadLocalList;
					}
					else
					{
						threadLocalList = this.GetUnownedList();
						if (threadLocalList == null)
						{
							threadLocalList = new ConcurrentBag<T>.ThreadLocalList(Thread.CurrentThread);
							this.m_tailList.m_nextList = threadLocalList;
							this.m_tailList = threadLocalList;
						}
					}
					this.m_locals.Value = threadLocalList;
					return threadLocalList;
				}
			}
			return null;
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000AE8A0 File Offset: 0x000ACAA0
		private ConcurrentBag<T>.ThreadLocalList GetUnownedList()
		{
			for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
			{
				if (threadLocalList.m_ownerThread.ThreadState == System.Threading.ThreadState.Stopped)
				{
					threadLocalList.m_ownerThread = Thread.CurrentThread;
					return threadLocalList;
				}
			}
			return null;
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000AE8E4 File Offset: 0x000ACAE4
		private bool Steal(out T result, bool take)
		{
			if (take)
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentBag_TryTakeSteals();
			}
			else
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentBag_TryPeekSteals();
			}
			List<int> list = new List<int>();
			for (;;)
			{
				list.Clear();
				bool flag = false;
				ConcurrentBag<T>.ThreadLocalList threadLocalList;
				for (threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
				{
					list.Add(threadLocalList.m_version);
					if (threadLocalList.m_head != null && this.TrySteal(threadLocalList, out result, take))
					{
						return true;
					}
				}
				threadLocalList = this.m_headList;
				foreach (int num in list)
				{
					if (num != threadLocalList.m_version)
					{
						flag = true;
						if (threadLocalList.m_head != null && this.TrySteal(threadLocalList, out result, take))
						{
							return true;
						}
					}
					threadLocalList = threadLocalList.m_nextList;
				}
				if (!flag)
				{
					goto Block_6;
				}
			}
			return true;
			Block_6:
			result = default(T);
			return false;
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000AE9DC File Offset: 0x000ACBDC
		private bool TrySteal(ConcurrentBag<T>.ThreadLocalList list, out T result, bool take)
		{
			bool flag2;
			lock (list)
			{
				if (this.CanSteal(list))
				{
					list.Steal(out result, take);
					flag2 = true;
				}
				else
				{
					result = default(T);
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000AEA30 File Offset: 0x000ACC30
		private bool CanSteal(ConcurrentBag<T>.ThreadLocalList list)
		{
			if (list.Count <= 2 && list.m_currentOp != 0)
			{
				SpinWait spinWait = default(SpinWait);
				while (list.m_currentOp != 0)
				{
					spinWait.SpinOnce();
				}
			}
			return list.Count > 0;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- the number of elements in the source <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x0600258E RID: 9614 RVA: 0x000AEA78 File Offset: 0x000ACC78
		[global::__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.GetString("ConcurrentBag_CopyTo_ArgumentNullException"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("ConcurrentBag_CopyTo_ArgumentOutOfRangeException"));
			}
			if (this.m_headList == null)
			{
				return;
			}
			bool flag = false;
			try
			{
				this.FreezeBag(ref flag);
				this.ToList().CopyTo(array, index);
			}
			finally
			{
				this.UnfreezeBag(flag);
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional. -or- <paramref name="array" /> does not have zero-based indexing. -or- <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. -or- The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x0600258F RID: 9615 RVA: 0x000AEAF4 File Offset: 0x000ACCF4
		[global::__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.GetString("ConcurrentBag_CopyTo_ArgumentNullException"));
			}
			bool flag = false;
			try
			{
				this.FreezeBag(ref flag);
				((ICollection)this.ToList()).CopyTo(array, index);
			}
			finally
			{
				this.UnfreezeBag(flag);
			}
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> elements to a new array.</summary>
		/// <returns>A new array containing a snapshot of elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x06002590 RID: 9616 RVA: 0x000AEB4C File Offset: 0x000ACD4C
		[global::__DynamicallyInvokable]
		public T[] ToArray()
		{
			if (this.m_headList == null)
			{
				return new T[0];
			}
			bool flag = false;
			T[] array;
			try
			{
				this.FreezeBag(ref flag);
				array = this.ToList().ToArray();
			}
			finally
			{
				this.UnfreezeBag(flag);
			}
			return array;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>An enumerator for the contents of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x06002591 RID: 9617 RVA: 0x000AEB9C File Offset: 0x000ACD9C
		[global::__DynamicallyInvokable]
		public IEnumerator<T> GetEnumerator()
		{
			if (this.m_headList == null)
			{
				return new List<T>().GetEnumerator();
			}
			bool flag = false;
			IEnumerator<T> enumerator;
			try
			{
				this.FreezeBag(ref flag);
				enumerator = this.ToList().GetEnumerator();
			}
			finally
			{
				this.UnfreezeBag(flag);
			}
			return enumerator;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>An enumerator for the contents of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x06002592 RID: 9618 RVA: 0x000AEBFC File Offset: 0x000ACDFC
		[global::__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000AEC04 File Offset: 0x000ACE04
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.m_serializationArray = this.ToArray();
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000AEC14 File Offset: 0x000ACE14
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.m_locals = new ThreadLocal<ConcurrentBag<T>.ThreadLocalList>();
			ConcurrentBag<T>.ThreadLocalList threadList = this.GetThreadList(true);
			foreach (T t in this.m_serializationArray)
			{
				threadList.Add(t, false);
			}
			this.m_headList = threadList;
			this.m_tailList = threadList;
			this.m_serializationArray = null;
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x000AEC74 File Offset: 0x000ACE74
		[global::__DynamicallyInvokable]
		public int Count
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this.m_headList == null)
				{
					return 0;
				}
				bool flag = false;
				int countInternal;
				try
				{
					this.FreezeBag(ref flag);
					countInternal = this.GetCountInternal();
				}
				finally
				{
					this.UnfreezeBag(flag);
				}
				return countInternal;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x000AECBC File Offset: 0x000ACEBC
		[global::__DynamicallyInvokable]
		public bool IsEmpty
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this.m_headList == null)
				{
					return true;
				}
				bool flag = false;
				bool flag2;
				try
				{
					this.FreezeBag(ref flag);
					for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
					{
						if (threadLocalList.m_head != null)
						{
							return false;
						}
					}
					flag2 = true;
				}
				finally
				{
					this.UnfreezeBag(flag);
				}
				return flag2;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot.</summary>
		/// <returns>Always returns <see langword="false" /> to indicate access is not synchronized.</returns>
		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x000AED24 File Offset: 0x000ACF24
		[global::__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[global::__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. This property is not supported.</summary>
		/// <returns>Returns null  (Nothing in Visual Basic).</returns>
		/// <exception cref="T:System.NotSupportedException">The SyncRoot property is not supported.</exception>
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x000AED27 File Offset: 0x000ACF27
		[global::__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(SR.GetString("ConcurrentCollection_SyncRoot_NotSupported"));
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x000AED38 File Offset: 0x000ACF38
		private object GlobalListsLock
		{
			get
			{
				return this.m_locals;
			}
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000AED40 File Offset: 0x000ACF40
		private void FreezeBag(ref bool lockTaken)
		{
			Monitor.Enter(this.GlobalListsLock, ref lockTaken);
			this.m_needSync = true;
			this.AcquireAllLocks();
			this.WaitAllOperations();
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000AED61 File Offset: 0x000ACF61
		private void UnfreezeBag(bool lockTaken)
		{
			this.ReleaseAllLocks();
			this.m_needSync = false;
			if (lockTaken)
			{
				Monitor.Exit(this.GlobalListsLock);
			}
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000AED80 File Offset: 0x000ACF80
		private void AcquireAllLocks()
		{
			bool flag = false;
			for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
			{
				try
				{
					Monitor.Enter(threadLocalList, ref flag);
				}
				finally
				{
					if (flag)
					{
						threadLocalList.m_lockTaken = true;
						flag = false;
					}
				}
			}
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000AEDD0 File Offset: 0x000ACFD0
		private void ReleaseAllLocks()
		{
			for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
			{
				if (threadLocalList.m_lockTaken)
				{
					threadLocalList.m_lockTaken = false;
					Monitor.Exit(threadLocalList);
				}
			}
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000AEE0C File Offset: 0x000AD00C
		private void WaitAllOperations()
		{
			for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
			{
				if (threadLocalList.m_currentOp != 0)
				{
					SpinWait spinWait = default(SpinWait);
					while (threadLocalList.m_currentOp != 0)
					{
						spinWait.SpinOnce();
					}
				}
			}
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000AEE58 File Offset: 0x000AD058
		private int GetCountInternal()
		{
			int num = 0;
			checked
			{
				for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
				{
					num += threadLocalList.Count;
				}
				return num;
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000AEE88 File Offset: 0x000AD088
		private List<T> ToList()
		{
			List<T> list = new List<T>();
			for (ConcurrentBag<T>.ThreadLocalList threadLocalList = this.m_headList; threadLocalList != null; threadLocalList = threadLocalList.m_nextList)
			{
				for (ConcurrentBag<T>.Node node = threadLocalList.m_head; node != null; node = node.m_next)
				{
					list.Add(node.m_value);
				}
			}
			return list;
		}

		// Token: 0x04002045 RID: 8261
		[NonSerialized]
		private ThreadLocal<ConcurrentBag<T>.ThreadLocalList> m_locals;

		// Token: 0x04002046 RID: 8262
		[NonSerialized]
		private volatile ConcurrentBag<T>.ThreadLocalList m_headList;

		// Token: 0x04002047 RID: 8263
		[NonSerialized]
		private volatile ConcurrentBag<T>.ThreadLocalList m_tailList;

		// Token: 0x04002048 RID: 8264
		[NonSerialized]
		private bool m_needSync;

		// Token: 0x04002049 RID: 8265
		private T[] m_serializationArray;

		// Token: 0x0200080A RID: 2058
		[Serializable]
		internal class Node
		{
			// Token: 0x060044C1 RID: 17601 RVA: 0x0011FC53 File Offset: 0x0011DE53
			public Node(T value)
			{
				this.m_value = value;
			}

			// Token: 0x04003549 RID: 13641
			public readonly T m_value;

			// Token: 0x0400354A RID: 13642
			public ConcurrentBag<T>.Node m_next;

			// Token: 0x0400354B RID: 13643
			public ConcurrentBag<T>.Node m_prev;
		}

		// Token: 0x0200080B RID: 2059
		internal class ThreadLocalList
		{
			// Token: 0x060044C2 RID: 17602 RVA: 0x0011FC62 File Offset: 0x0011DE62
			internal ThreadLocalList(Thread ownerThread)
			{
				this.m_ownerThread = ownerThread;
			}

			// Token: 0x060044C3 RID: 17603 RVA: 0x0011FC74 File Offset: 0x0011DE74
			internal void Add(T item, bool updateCount)
			{
				ConcurrentBag<T>.Node node;
				checked
				{
					this.m_count++;
					node = new ConcurrentBag<T>.Node(item);
				}
				if (this.m_head == null)
				{
					this.m_head = node;
					this.m_tail = node;
					this.m_version++;
				}
				else
				{
					node.m_next = this.m_head;
					this.m_head.m_prev = node;
					this.m_head = node;
				}
				if (updateCount)
				{
					this.m_count -= this.m_stealCount;
					this.m_stealCount = 0;
				}
			}

			// Token: 0x060044C4 RID: 17604 RVA: 0x0011FD08 File Offset: 0x0011DF08
			internal void Remove(out T result)
			{
				ConcurrentBag<T>.Node head = this.m_head;
				this.m_head = this.m_head.m_next;
				if (this.m_head != null)
				{
					this.m_head.m_prev = null;
				}
				else
				{
					this.m_tail = null;
				}
				this.m_count--;
				result = head.m_value;
			}

			// Token: 0x060044C5 RID: 17605 RVA: 0x0011FD70 File Offset: 0x0011DF70
			internal bool Peek(out T result)
			{
				ConcurrentBag<T>.Node head = this.m_head;
				if (head != null)
				{
					result = head.m_value;
					return true;
				}
				result = default(T);
				return false;
			}

			// Token: 0x060044C6 RID: 17606 RVA: 0x0011FDA0 File Offset: 0x0011DFA0
			internal void Steal(out T result, bool remove)
			{
				ConcurrentBag<T>.Node tail = this.m_tail;
				if (remove)
				{
					this.m_tail = this.m_tail.m_prev;
					if (this.m_tail != null)
					{
						this.m_tail.m_next = null;
					}
					else
					{
						this.m_head = null;
					}
					this.m_stealCount++;
				}
				result = tail.m_value;
			}

			// Token: 0x17000F9A RID: 3994
			// (get) Token: 0x060044C7 RID: 17607 RVA: 0x0011FE0B File Offset: 0x0011E00B
			internal int Count
			{
				get
				{
					return this.m_count - this.m_stealCount;
				}
			}

			// Token: 0x0400354C RID: 13644
			internal volatile ConcurrentBag<T>.Node m_head;

			// Token: 0x0400354D RID: 13645
			private volatile ConcurrentBag<T>.Node m_tail;

			// Token: 0x0400354E RID: 13646
			internal volatile int m_currentOp;

			// Token: 0x0400354F RID: 13647
			private int m_count;

			// Token: 0x04003550 RID: 13648
			internal int m_stealCount;

			// Token: 0x04003551 RID: 13649
			internal volatile ConcurrentBag<T>.ThreadLocalList m_nextList;

			// Token: 0x04003552 RID: 13650
			internal bool m_lockTaken;

			// Token: 0x04003553 RID: 13651
			internal Thread m_ownerThread;

			// Token: 0x04003554 RID: 13652
			internal volatile int m_version;
		}

		// Token: 0x0200080C RID: 2060
		internal enum ListOperation
		{
			// Token: 0x04003556 RID: 13654
			None,
			// Token: 0x04003557 RID: 13655
			Add,
			// Token: 0x04003558 RID: 13656
			Take
		}
	}
}
