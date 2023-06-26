using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Provides blocking and bounding capabilities for thread-safe collections that implement <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x020003D0 RID: 976
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(SystemThreadingCollections_BlockingCollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}, Type = {m_collection}")]
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class BlockingCollection<T> : IEnumerable<T>, IEnumerable, ICollection, IDisposable, IReadOnlyCollection<T>
	{
		/// <summary>Gets the bounded capacity of this <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance.</summary>
		/// <returns>The bounded capacity of this collection, or int.MaxValue if no bound was supplied.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x000AD710 File Offset: 0x000AB910
		[global::__DynamicallyInvokable]
		public int BoundedCapacity
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.m_boundedCapacity;
			}
		}

		/// <summary>Gets whether this <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been marked as complete for adding.</summary>
		/// <returns>Whether this collection has been marked as complete for adding.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002543 RID: 9539 RVA: 0x000AD71E File Offset: 0x000AB91E
		[global::__DynamicallyInvokable]
		public bool IsAddingCompleted
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.m_currentAdders == int.MinValue;
			}
		}

		/// <summary>Gets whether this <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been marked as complete for adding and is empty.</summary>
		/// <returns>Whether this collection has been marked as complete for adding and is empty.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002544 RID: 9540 RVA: 0x000AD735 File Offset: 0x000AB935
		[global::__DynamicallyInvokable]
		public bool IsCompleted
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.IsAddingCompleted && this.m_occupiedNodes.CurrentCount == 0;
			}
		}

		/// <summary>Gets the number of items contained in the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" />.</summary>
		/// <returns>The number of items contained in the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x000AD755 File Offset: 0x000AB955
		[global::__DynamicallyInvokable]
		public int Count
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return this.m_occupiedNodes.CurrentCount;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>Always returns <see langword="false" /> to indicate the access is not synchronized.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002546 RID: 9542 RVA: 0x000AD768 File Offset: 0x000AB968
		[global::__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.CheckDisposed();
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. This property is not supported.</summary>
		/// <returns>returns null.</returns>
		/// <exception cref="T:System.NotSupportedException">The SyncRoot property is not supported.</exception>
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x000AD771 File Offset: 0x000AB971
		[global::__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(SR.GetString("ConcurrentCollection_SyncRoot_NotSupported"));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> class without an upper-bound.</summary>
		// Token: 0x06002548 RID: 9544 RVA: 0x000AD782 File Offset: 0x000AB982
		[global::__DynamicallyInvokable]
		public BlockingCollection()
			: this(new ConcurrentQueue<T>())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> class with the specified upper-bound.</summary>
		/// <param name="boundedCapacity">The bounded size of the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="boundedCapacity" /> is not a positive value.</exception>
		// Token: 0x06002549 RID: 9545 RVA: 0x000AD78F File Offset: 0x000AB98F
		[global::__DynamicallyInvokable]
		public BlockingCollection(int boundedCapacity)
			: this(new ConcurrentQueue<T>(), boundedCapacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> class with the specified upper-bound and using the provided <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" /> as its underlying data store.</summary>
		/// <param name="collection">The collection to use as the underlying data store.</param>
		/// <param name="boundedCapacity">The bounded size of the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collection" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="boundedCapacity" /> is not a positive value.</exception>
		/// <exception cref="T:System.ArgumentException">The supplied <paramref name="collection" /> contains more values than is permitted by <paramref name="boundedCapacity" />.</exception>
		// Token: 0x0600254A RID: 9546 RVA: 0x000AD7A0 File Offset: 0x000AB9A0
		[global::__DynamicallyInvokable]
		public BlockingCollection(IProducerConsumerCollection<T> collection, int boundedCapacity)
		{
			if (boundedCapacity < 1)
			{
				throw new ArgumentOutOfRangeException("boundedCapacity", boundedCapacity, SR.GetString("BlockingCollection_ctor_BoundedCapacityRange"));
			}
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			int count = collection.Count;
			if (count > boundedCapacity)
			{
				throw new ArgumentException(SR.GetString("BlockingCollection_ctor_CountMoreThanCapacity"));
			}
			this.Initialize(collection, boundedCapacity, count);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> class without an upper-bound and using the provided <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" /> as its underlying data store.</summary>
		/// <param name="collection">The collection to use as the underlying data store.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collection" /> argument is null.</exception>
		// Token: 0x0600254B RID: 9547 RVA: 0x000AD804 File Offset: 0x000ABA04
		[global::__DynamicallyInvokable]
		public BlockingCollection(IProducerConsumerCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.Initialize(collection, -1, collection.Count);
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000AD828 File Offset: 0x000ABA28
		private void Initialize(IProducerConsumerCollection<T> collection, int boundedCapacity, int collectionCount)
		{
			this.m_collection = collection;
			this.m_boundedCapacity = boundedCapacity;
			this.m_isDisposed = false;
			this.m_ConsumersCancellationTokenSource = new CancellationTokenSource();
			this.m_ProducersCancellationTokenSource = new CancellationTokenSource();
			if (boundedCapacity == -1)
			{
				this.m_freeNodes = null;
			}
			else
			{
				this.m_freeNodes = new SemaphoreSlim(boundedCapacity - collectionCount);
			}
			this.m_occupiedNodes = new SemaphoreSlim(collectionCount);
		}

		/// <summary>Adds the item to the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" />.</summary>
		/// <param name="item">The item to be added to the collection. The value can be a null reference.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been marked as complete with regards to additions.  
		///  -or-  
		///  The underlying collection didn't accept the item.</exception>
		// Token: 0x0600254D RID: 9549 RVA: 0x000AD888 File Offset: 0x000ABA88
		[global::__DynamicallyInvokable]
		public void Add(T item)
		{
			this.TryAddWithNoTimeValidation(item, -1, default(CancellationToken));
		}

		/// <summary>Adds the item to the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" />.</summary>
		/// <param name="item">The item to be added to the collection. The value can be a null reference.</param>
		/// <param name="cancellationToken">A cancellation token to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">If the <see cref="T:System.Threading.CancellationToken" /> is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that owns <paramref name="cancellationToken" /> has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been marked as complete with regards to additions.  
		///  -or-  
		///  The underlying collection didn't accept the item.</exception>
		// Token: 0x0600254E RID: 9550 RVA: 0x000AD8A7 File Offset: 0x000ABAA7
		[global::__DynamicallyInvokable]
		public void Add(T item, CancellationToken cancellationToken)
		{
			this.TryAddWithNoTimeValidation(item, -1, cancellationToken);
		}

		/// <summary>Tries to add the specified item to the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" />.</summary>
		/// <param name="item">The item to be added to the collection.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> could be added; otherwise, <see langword="false" />. If the item is a duplicate, and the underlying collection does not accept duplicate items, then an <see cref="T:System.InvalidOperationException" /> is thrown.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been marked as complete with regards to additions.  
		///  -or-  
		///  The underlying collection didn't accept the item.</exception>
		// Token: 0x0600254F RID: 9551 RVA: 0x000AD8B4 File Offset: 0x000ABAB4
		[global::__DynamicallyInvokable]
		public bool TryAdd(T item)
		{
			return this.TryAddWithNoTimeValidation(item, 0, default(CancellationToken));
		}

		/// <summary>Tries to add the specified item to the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" />.</summary>
		/// <param name="item">The item to be added to the collection.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>true if the <paramref name="item" /> could be added to the collection within the specified time span; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been marked as complete with regards to additions.  
		///  -or-  
		///  The underlying collection didn't accept the item.</exception>
		// Token: 0x06002550 RID: 9552 RVA: 0x000AD8D4 File Offset: 0x000ABAD4
		[global::__DynamicallyInvokable]
		public bool TryAdd(T item, TimeSpan timeout)
		{
			BlockingCollection<T>.ValidateTimeout(timeout);
			return this.TryAddWithNoTimeValidation(item, (int)timeout.TotalMilliseconds, default(CancellationToken));
		}

		/// <summary>Tries to add the specified item to the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> within the specified time period.</summary>
		/// <param name="item">The item to be added to the collection.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <returns>true if the <paramref name="item" /> could be added to the collection within the specified time; otherwise, false. If the item is a duplicate, and the underlying collection does not accept duplicate items, then an <see cref="T:System.InvalidOperationException" /> is thrown.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been marked as complete with regards to additions.  
		///  -or-  
		///  The underlying collection didn't accept the item.</exception>
		// Token: 0x06002551 RID: 9553 RVA: 0x000AD900 File Offset: 0x000ABB00
		[global::__DynamicallyInvokable]
		public bool TryAdd(T item, int millisecondsTimeout)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return this.TryAddWithNoTimeValidation(item, millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Tries to add the specified item to the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> within the specified time period, while observing a cancellation token.</summary>
		/// <param name="item">The item to be added to the collection.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">A cancellation token to observe.</param>
		/// <returns>true if the <paramref name="item" /> could be added to the collection within the specified time; otherwise, false. If the item is a duplicate, and the underlying collection does not accept duplicate items, then an <see cref="T:System.InvalidOperationException" /> is thrown.</returns>
		/// <exception cref="T:System.OperationCanceledException">If the <see cref="T:System.Threading.CancellationToken" /> is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed or the underlying <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been marked as complete with regards to additions.  
		///  -or-  
		///  The underlying collection didn't accept the item.</exception>
		// Token: 0x06002552 RID: 9554 RVA: 0x000AD924 File Offset: 0x000ABB24
		[global::__DynamicallyInvokable]
		public bool TryAdd(T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return this.TryAddWithNoTimeValidation(item, millisecondsTimeout, cancellationToken);
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000AD938 File Offset: 0x000ABB38
		private bool TryAddWithNoTimeValidation(T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.CheckDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), cancellationToken);
			}
			if (this.IsAddingCompleted)
			{
				throw new InvalidOperationException(SR.GetString("BlockingCollection_Completed"));
			}
			bool flag = true;
			if (this.m_freeNodes != null)
			{
				CancellationTokenSource cancellationTokenSource = null;
				try
				{
					flag = this.m_freeNodes.Wait(0);
					if (!flag && millisecondsTimeout != 0)
					{
						cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this.m_ProducersCancellationTokenSource.Token);
						flag = this.m_freeNodes.Wait(millisecondsTimeout, cancellationTokenSource.Token);
					}
				}
				catch (OperationCanceledException)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), cancellationToken);
					}
					throw new InvalidOperationException(SR.GetString("BlockingCollection_Add_ConcurrentCompleteAdd"));
				}
				finally
				{
					if (cancellationTokenSource != null)
					{
						cancellationTokenSource.Dispose();
					}
				}
			}
			if (flag)
			{
				SpinWait spinWait = default(SpinWait);
				for (;;)
				{
					int currentAdders = this.m_currentAdders;
					if ((currentAdders & -2147483648) != 0)
					{
						break;
					}
					if (Interlocked.CompareExchange(ref this.m_currentAdders, currentAdders + 1, currentAdders) == currentAdders)
					{
						goto IL_11D;
					}
					spinWait.SpinOnce();
				}
				spinWait.Reset();
				while (this.m_currentAdders != -2147483648)
				{
					spinWait.SpinOnce();
				}
				throw new InvalidOperationException(SR.GetString("BlockingCollection_Completed"));
				IL_11D:
				try
				{
					bool flag2 = false;
					try
					{
						cancellationToken.ThrowIfCancellationRequested();
						flag2 = this.m_collection.TryAdd(item);
					}
					catch
					{
						if (this.m_freeNodes != null)
						{
							this.m_freeNodes.Release();
						}
						throw;
					}
					if (!flag2)
					{
						throw new InvalidOperationException(SR.GetString("BlockingCollection_Add_Failed"));
					}
					this.m_occupiedNodes.Release();
				}
				finally
				{
					Interlocked.Decrement(ref this.m_currentAdders);
				}
			}
			return flag;
		}

		/// <summary>Removes  an item from the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" />.</summary>
		/// <returns>The item removed from the collection.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying collection was modified outside of this <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance, or the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> is empty and the collection has been marked as complete for adding.</exception>
		// Token: 0x06002554 RID: 9556 RVA: 0x000ADAF8 File Offset: 0x000ABCF8
		[global::__DynamicallyInvokable]
		public T Take()
		{
			T t;
			if (!this.TryTake(out t, -1, CancellationToken.None))
			{
				throw new InvalidOperationException(SR.GetString("BlockingCollection_CantTakeWhenDone"));
			}
			return t;
		}

		/// <summary>Removes an item from the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" />.</summary>
		/// <param name="cancellationToken">Object that can be used to cancel the take operation.</param>
		/// <returns>The item removed from the collection.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created the token was canceled.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying collection was modified outside of this <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance or the BlockingCollection is marked as complete for adding, or the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> is empty.</exception>
		// Token: 0x06002555 RID: 9557 RVA: 0x000ADB28 File Offset: 0x000ABD28
		[global::__DynamicallyInvokable]
		public T Take(CancellationToken cancellationToken)
		{
			T t;
			if (!this.TryTake(out t, -1, cancellationToken))
			{
				throw new InvalidOperationException(SR.GetString("BlockingCollection_CantTakeWhenDone"));
			}
			return t;
		}

		/// <summary>Tries to remove an item from the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" />.</summary>
		/// <param name="item">The item to be removed from the collection.</param>
		/// <returns>
		///   <see langword="true" /> if an item could be removed; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying collection was modified outside of this <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance.</exception>
		// Token: 0x06002556 RID: 9558 RVA: 0x000ADB52 File Offset: 0x000ABD52
		[global::__DynamicallyInvokable]
		public bool TryTake(out T item)
		{
			return this.TryTake(out item, 0, CancellationToken.None);
		}

		/// <summary>Tries to remove an item from the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> in the specified time period.</summary>
		/// <param name="item">The item to be removed from the collection.</param>
		/// <param name="timeout">An object that represents the number of milliseconds to wait, or an object that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> if an item could be removed from the collection within the specified  time; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying collection was modified outside of this <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance.</exception>
		// Token: 0x06002557 RID: 9559 RVA: 0x000ADB61 File Offset: 0x000ABD61
		[global::__DynamicallyInvokable]
		public bool TryTake(out T item, TimeSpan timeout)
		{
			BlockingCollection<T>.ValidateTimeout(timeout);
			return this.TryTakeWithNoTimeValidation(out item, (int)timeout.TotalMilliseconds, CancellationToken.None, null);
		}

		/// <summary>Tries to remove an item from the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> in the specified time period.</summary>
		/// <param name="item">The item to be removed from the collection.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> if an item could be removed from the collection within the specified  time; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying collection was modified outside of this <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance.</exception>
		// Token: 0x06002558 RID: 9560 RVA: 0x000ADB7E File Offset: 0x000ABD7E
		[global::__DynamicallyInvokable]
		public bool TryTake(out T item, int millisecondsTimeout)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return this.TryTakeWithNoTimeValidation(out item, millisecondsTimeout, CancellationToken.None, null);
		}

		/// <summary>Tries to remove an item from the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> in the specified time period while observing a cancellation token.</summary>
		/// <param name="item">The item to be removed from the collection.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">A cancellation token to observe.</param>
		/// <returns>
		///   <see langword="true" /> if an item could be removed from the collection within the specified  time; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed or the underlying <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying collection was modified outside this <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance.</exception>
		// Token: 0x06002559 RID: 9561 RVA: 0x000ADB94 File Offset: 0x000ABD94
		[global::__DynamicallyInvokable]
		public bool TryTake(out T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return this.TryTakeWithNoTimeValidation(out item, millisecondsTimeout, cancellationToken, null);
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000ADBA8 File Offset: 0x000ABDA8
		private bool TryTakeWithNoTimeValidation(out T item, int millisecondsTimeout, CancellationToken cancellationToken, CancellationTokenSource combinedTokenSource)
		{
			this.CheckDisposed();
			item = default(T);
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), cancellationToken);
			}
			if (this.IsCompleted)
			{
				return false;
			}
			bool flag = false;
			CancellationTokenSource cancellationTokenSource = combinedTokenSource;
			try
			{
				flag = this.m_occupiedNodes.Wait(0);
				if (!flag && millisecondsTimeout != 0)
				{
					if (combinedTokenSource == null)
					{
						cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this.m_ConsumersCancellationTokenSource.Token);
					}
					flag = this.m_occupiedNodes.Wait(millisecondsTimeout, cancellationTokenSource.Token);
				}
			}
			catch (OperationCanceledException)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), cancellationToken);
				}
				return false;
			}
			finally
			{
				if (cancellationTokenSource != null && combinedTokenSource == null)
				{
					cancellationTokenSource.Dispose();
				}
			}
			if (flag)
			{
				bool flag2 = false;
				bool flag3 = true;
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					flag2 = this.m_collection.TryTake(out item);
					flag3 = false;
					if (!flag2)
					{
						throw new InvalidOperationException(SR.GetString("BlockingCollection_Take_CollectionModified"));
					}
				}
				finally
				{
					if (flag2)
					{
						if (this.m_freeNodes != null)
						{
							this.m_freeNodes.Release();
						}
					}
					else if (flag3)
					{
						this.m_occupiedNodes.Release();
					}
					if (this.IsCompleted)
					{
						this.CancelWaitingConsumers();
					}
				}
			}
			return flag;
		}

		/// <summary>Adds the specified item to any one of the specified <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances.</summary>
		/// <param name="collections">The array of collections.</param>
		/// <param name="item">The item to be added to one of the collections.</param>
		/// <returns>The index of the collection in the <paramref name="collections" /> array to which the item was added.</returns>
		/// <exception cref="T:System.ObjectDisposedException">At least one of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collections" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The count of <paramref name="collections" /> is greater than the maximum size of 62 for STA and 63 for MTA.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="collections" /> argument is a 0-length array or contains a null element, or at least one of collections has been marked as complete for adding.</exception>
		/// <exception cref="T:System.InvalidOperationException">At least one underlying collection didn't accept the item.</exception>
		// Token: 0x0600255B RID: 9563 RVA: 0x000ADCF0 File Offset: 0x000ABEF0
		[global::__DynamicallyInvokable]
		public static int AddToAny(BlockingCollection<T>[] collections, T item)
		{
			return BlockingCollection<T>.TryAddToAny(collections, item, -1, CancellationToken.None);
		}

		/// <summary>Adds the specified item to any one of the specified <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances.</summary>
		/// <param name="collections">The array of collections.</param>
		/// <param name="item">The item to be added to one of the collections.</param>
		/// <param name="cancellationToken">A cancellation token to observe.</param>
		/// <returns>The index of the collection in the <paramref name="collections" /> array to which the item was added.</returns>
		/// <exception cref="T:System.OperationCanceledException">If the <see cref="T:System.Threading.CancellationToken" /> is canceled.</exception>
		/// <exception cref="T:System.InvalidOperationException">At least one underlying collection didn't accept the item.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collections" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The count of <paramref name="collections" /> is greater than the maximum size of 62 for STA and 63 for MTA.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="collections" /> argument is a 0-length array or contains a null element, or at least one of collections has been marked as complete for adding.</exception>
		/// <exception cref="T:System.ObjectDisposedException">At least one of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances has been disposed, or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x0600255C RID: 9564 RVA: 0x000ADCFF File Offset: 0x000ABEFF
		[global::__DynamicallyInvokable]
		public static int AddToAny(BlockingCollection<T>[] collections, T item, CancellationToken cancellationToken)
		{
			return BlockingCollection<T>.TryAddToAny(collections, item, -1, cancellationToken);
		}

		/// <summary>Tries to add the specified item to any one of the specified <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances.</summary>
		/// <param name="collections">The array of collections.</param>
		/// <param name="item">The item to be added to one of the collections.</param>
		/// <returns>The index of the collection in the <paramref name="collections" /> array to which the item was added, or -1 if the item could not be added.</returns>
		/// <exception cref="T:System.ObjectDisposedException">At least one of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collections" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The count of <paramref name="collections" /> is greater than the maximum size of 62 for STA and 63 for MTA.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="collections" /> argument is a 0-length array or contains a null element, or at least one of collections has been marked as complete for adding.</exception>
		/// <exception cref="T:System.InvalidOperationException">At least one underlying collection didn't accept the item.</exception>
		// Token: 0x0600255D RID: 9565 RVA: 0x000ADD0A File Offset: 0x000ABF0A
		[global::__DynamicallyInvokable]
		public static int TryAddToAny(BlockingCollection<T>[] collections, T item)
		{
			return BlockingCollection<T>.TryAddToAny(collections, item, 0, CancellationToken.None);
		}

		/// <summary>Tries to add the specified item to any one of the specified <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances while observing the specified cancellation token.</summary>
		/// <param name="collections">The array of collections.</param>
		/// <param name="item">The item to be added to one of the collections.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>The index of the collection in the <paramref name="collections" /> array to which the item was added, or -1 if the item could not be added.</returns>
		/// <exception cref="T:System.ObjectDisposedException">At least one of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collections" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.  
		/// -or-  
		/// The count of <paramref name="collections" /> is greater than the maximum size of 62 for STA and 63 for MTA.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="collections" /> argument is a 0-length array or contains a null element, or at least one of collections has been marked as complete for adding.</exception>
		/// <exception cref="T:System.InvalidOperationException">At least one underlying collection didn't accept the item.</exception>
		// Token: 0x0600255E RID: 9566 RVA: 0x000ADD19 File Offset: 0x000ABF19
		[global::__DynamicallyInvokable]
		public static int TryAddToAny(BlockingCollection<T>[] collections, T item, TimeSpan timeout)
		{
			BlockingCollection<T>.ValidateTimeout(timeout);
			return BlockingCollection<T>.TryAddToAnyCore(collections, item, (int)timeout.TotalMilliseconds, CancellationToken.None);
		}

		/// <summary>Tries to add the specified item to any one of the specified <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances.</summary>
		/// <param name="collections">The array of collections.</param>
		/// <param name="item">The item to be added to one of the collections.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <returns>The index of the collection in the <paramref name="collections" /> array to which the item was added, or -1 if the item could not be added.</returns>
		/// <exception cref="T:System.ObjectDisposedException">At least one of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collections" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.  
		/// -or-  
		/// The count of <paramref name="collections" /> is greater than the maximum size of 62 for STA and 63 for MTA.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="collections" /> argument is a 0-length array or contains a null element, or at least one of collections has been marked as complete for adding.</exception>
		/// <exception cref="T:System.InvalidOperationException">At least one underlying collection didn't accept the item.</exception>
		// Token: 0x0600255F RID: 9567 RVA: 0x000ADD35 File Offset: 0x000ABF35
		[global::__DynamicallyInvokable]
		public static int TryAddToAny(BlockingCollection<T>[] collections, T item, int millisecondsTimeout)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return BlockingCollection<T>.TryAddToAnyCore(collections, item, millisecondsTimeout, CancellationToken.None);
		}

		/// <summary>Tries to add the specified item to any one of the specified <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances.</summary>
		/// <param name="collections">The array of collections.</param>
		/// <param name="item">The item to be added to one of the collections.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">A cancellation token to observe.</param>
		/// <returns>The index of the collection in the <paramref name="collections" /> array to which the item was added, or -1 if the item could not be added.</returns>
		/// <exception cref="T:System.OperationCanceledException">If the <see cref="T:System.Threading.CancellationToken" /> is canceled.</exception>
		/// <exception cref="T:System.InvalidOperationException">At least one underlying collection didn't accept the item.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collections" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.  
		/// -or-  
		/// The count of <paramref name="collections" /> is greater than the maximum size of 62 for STA and 63 for MTA.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="collections" /> argument is a 0-length array or contains a null element, or at least one of collections has been marked as complete for adding.</exception>
		/// <exception cref="T:System.ObjectDisposedException">At least one of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances has been disposed.</exception>
		// Token: 0x06002560 RID: 9568 RVA: 0x000ADD4A File Offset: 0x000ABF4A
		[global::__DynamicallyInvokable]
		public static int TryAddToAny(BlockingCollection<T>[] collections, T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return BlockingCollection<T>.TryAddToAnyCore(collections, item, millisecondsTimeout, cancellationToken);
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000ADD5C File Offset: 0x000ABF5C
		private static int TryAddToAnyCore(BlockingCollection<T>[] collections, T item, int millisecondsTimeout, CancellationToken externalCancellationToken)
		{
			BlockingCollection<T>.ValidateCollectionsArray(collections, true);
			int num = millisecondsTimeout;
			uint num2 = 0U;
			if (millisecondsTimeout != -1)
			{
				num2 = (uint)Environment.TickCount;
			}
			int num3 = BlockingCollection<T>.TryAddToAnyFast(collections, item);
			if (num3 > -1)
			{
				return num3;
			}
			CancellationToken[] array;
			List<WaitHandle> handles = BlockingCollection<T>.GetHandles(collections, externalCancellationToken, true, out array);
			while (millisecondsTimeout == -1 || num >= 0)
			{
				num3 = -1;
				using (CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(array))
				{
					handles.Add(cancellationTokenSource.Token.WaitHandle);
					num3 = WaitHandle.WaitAny(handles.ToArray(), num, false);
					handles.RemoveAt(handles.Count - 1);
					if (cancellationTokenSource.IsCancellationRequested)
					{
						if (externalCancellationToken.IsCancellationRequested)
						{
							throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), externalCancellationToken);
						}
						throw new ArgumentException(SR.GetString("BlockingCollection_CantAddAnyWhenCompleted"), "collections");
					}
				}
				if (num3 == 258)
				{
					return -1;
				}
				if (collections[num3].TryAdd(item))
				{
					return num3;
				}
				if (millisecondsTimeout != -1)
				{
					num = BlockingCollection<T>.UpdateTimeOut(num2, millisecondsTimeout);
				}
			}
			return -1;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000ADE68 File Offset: 0x000AC068
		private static int TryAddToAnyFast(BlockingCollection<T>[] collections, T item)
		{
			for (int i = 0; i < collections.Length; i++)
			{
				if (collections[i].m_freeNodes == null)
				{
					collections[i].TryAdd(item);
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000ADE9C File Offset: 0x000AC09C
		private static List<WaitHandle> GetHandles(BlockingCollection<T>[] collections, CancellationToken externalCancellationToken, bool isAddOperation, out CancellationToken[] cancellationTokens)
		{
			List<WaitHandle> list = new List<WaitHandle>(collections.Length + 1);
			List<CancellationToken> list2 = new List<CancellationToken>(collections.Length + 1);
			list2.Add(externalCancellationToken);
			if (isAddOperation)
			{
				for (int i = 0; i < collections.Length; i++)
				{
					if (collections[i].m_freeNodes != null)
					{
						list.Add(collections[i].m_freeNodes.AvailableWaitHandle);
						list2.Add(collections[i].m_ProducersCancellationTokenSource.Token);
					}
				}
			}
			else
			{
				for (int j = 0; j < collections.Length; j++)
				{
					if (!collections[j].IsCompleted)
					{
						list.Add(collections[j].m_occupiedNodes.AvailableWaitHandle);
						list2.Add(collections[j].m_ConsumersCancellationTokenSource.Token);
					}
				}
			}
			cancellationTokens = list2.ToArray();
			return list;
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000ADF50 File Offset: 0x000AC150
		private static int UpdateTimeOut(uint startTime, int originalWaitMillisecondsTimeout)
		{
			if (originalWaitMillisecondsTimeout == 0)
			{
				return 0;
			}
			uint num = (uint)(Environment.TickCount - (int)startTime);
			if (num > 2147483647U)
			{
				return 0;
			}
			int num2 = originalWaitMillisecondsTimeout - (int)num;
			if (num2 <= 0)
			{
				return 0;
			}
			return num2;
		}

		/// <summary>Takes an item from any one of the specified <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances.</summary>
		/// <param name="collections">The array of collections.</param>
		/// <param name="item">The item removed from one of the collections.</param>
		/// <returns>The index of the collection in the <paramref name="collections" /> array from which the item was removed.</returns>
		/// <exception cref="T:System.ObjectDisposedException">At least one of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collections" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The count of <paramref name="collections" /> is greater than the maximum size of 62 for STA and 63 for MTA.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="collections" /> argument is a 0-length array or contains a null element or <see cref="M:System.Collections.Concurrent.BlockingCollection`1.CompleteAdding" /> has been called on the collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">At least one of the underlying collections was modified outside of its <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance.</exception>
		// Token: 0x06002565 RID: 9573 RVA: 0x000ADF7F File Offset: 0x000AC17F
		[global::__DynamicallyInvokable]
		public static int TakeFromAny(BlockingCollection<T>[] collections, out T item)
		{
			return BlockingCollection<T>.TakeFromAny(collections, out item, CancellationToken.None);
		}

		/// <summary>Takes an item from any one of the specified <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances while observing the specified cancellation token.</summary>
		/// <param name="collections">The array of collections.</param>
		/// <param name="item">The item removed from one of the collections.</param>
		/// <param name="cancellationToken">A cancellation token to observe.</param>
		/// <returns>The index of the collection in the <paramref name="collections" /> array from which the item was removed.</returns>
		/// <exception cref="T:System.OperationCanceledException">If the <see cref="T:System.Threading.CancellationToken" /> is canceled.</exception>
		/// <exception cref="T:System.InvalidOperationException">At least one of the underlying collections was modified outside of its <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collections" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The count of <paramref name="collections" /> is greater than the maximum size of 62 for STA and 63 for MTA.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="collections" /> argument is a 0-length array or contains a null element, or <see cref="M:System.Collections.Concurrent.BlockingCollection`1.CompleteAdding" /> has been called on the collection.</exception>
		/// <exception cref="T:System.ObjectDisposedException">At least one of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances has been disposed.</exception>
		// Token: 0x06002566 RID: 9574 RVA: 0x000ADF90 File Offset: 0x000AC190
		[global::__DynamicallyInvokable]
		public static int TakeFromAny(BlockingCollection<T>[] collections, out T item, CancellationToken cancellationToken)
		{
			return BlockingCollection<T>.TryTakeFromAnyCore(collections, out item, -1, true, cancellationToken);
		}

		/// <summary>Tries to remove an item from any one of the specified <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances.</summary>
		/// <param name="collections">The array of collections.</param>
		/// <param name="item">The item removed from one of the collections.</param>
		/// <returns>The index of the collection in the <paramref name="collections" /> array from which the item was removed, or -1 if an item could not be removed.</returns>
		/// <exception cref="T:System.ObjectDisposedException">At least one of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collections" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The count of <paramref name="collections" /> is greater than the maximum size of 62 for STA and 63 for MTA.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="collections" /> argument is a 0-length array or contains a null element.</exception>
		/// <exception cref="T:System.InvalidOperationException">At least one of the underlying collections was modified outside of its <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance.</exception>
		// Token: 0x06002567 RID: 9575 RVA: 0x000ADFA9 File Offset: 0x000AC1A9
		[global::__DynamicallyInvokable]
		public static int TryTakeFromAny(BlockingCollection<T>[] collections, out T item)
		{
			return BlockingCollection<T>.TryTakeFromAny(collections, out item, 0);
		}

		/// <summary>Tries to remove an item from any one of the specified <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances.</summary>
		/// <param name="collections">The array of collections.</param>
		/// <param name="item">The item removed from one of the collections.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>The index of the collection in the <paramref name="collections" /> array from which the item was removed, or -1 if an item could not be removed.</returns>
		/// <exception cref="T:System.ObjectDisposedException">At least one of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collections" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.  
		/// -or-  
		/// The count of <paramref name="collections" /> is greater than the maximum size of 62 for STA and 63 for MTA.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="collections" /> argument is a 0-length array or contains a null element.</exception>
		/// <exception cref="T:System.InvalidOperationException">At least one of the underlying collections was modified outside of its <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance.</exception>
		// Token: 0x06002568 RID: 9576 RVA: 0x000ADFB3 File Offset: 0x000AC1B3
		[global::__DynamicallyInvokable]
		public static int TryTakeFromAny(BlockingCollection<T>[] collections, out T item, TimeSpan timeout)
		{
			BlockingCollection<T>.ValidateTimeout(timeout);
			return BlockingCollection<T>.TryTakeFromAnyCore(collections, out item, (int)timeout.TotalMilliseconds, false, CancellationToken.None);
		}

		/// <summary>Tries to remove an item from any one of the specified <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances.</summary>
		/// <param name="collections">The array of collections.</param>
		/// <param name="item">The item removed from one of the collections.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <returns>The index of the collection in the <paramref name="collections" /> array from which the item was removed, or -1 if an item could not be removed.</returns>
		/// <exception cref="T:System.ObjectDisposedException">At least one of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collections" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.  
		/// -or-  
		/// The count of <paramref name="collections" /> is greater than the maximum size of 62 for STA and 63 for MTA.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="collections" /> argument is a 0-length array or contains a null element.</exception>
		/// <exception cref="T:System.InvalidOperationException">At least one of the underlying collections was modified outside of its <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance.</exception>
		// Token: 0x06002569 RID: 9577 RVA: 0x000ADFD0 File Offset: 0x000AC1D0
		[global::__DynamicallyInvokable]
		public static int TryTakeFromAny(BlockingCollection<T>[] collections, out T item, int millisecondsTimeout)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return BlockingCollection<T>.TryTakeFromAnyCore(collections, out item, millisecondsTimeout, false, CancellationToken.None);
		}

		/// <summary>Tries to remove an item from any one of the specified <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances.</summary>
		/// <param name="collections">The array of collections.</param>
		/// <param name="item">The item removed from one of the collections.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">A cancellation token to observe.</param>
		/// <returns>The index of the collection in the <paramref name="collections" /> array from which the item was removed, or -1 if an item could not be removed.</returns>
		/// <exception cref="T:System.OperationCanceledException">If the <see cref="T:System.Threading.CancellationToken" /> is canceled.</exception>
		/// <exception cref="T:System.InvalidOperationException">At least one of the underlying collections was modified outside of its <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collections" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.  
		/// -or-  
		/// The count of <paramref name="collections" /> is greater than the maximum size of 62 for STA and 63 for MTA.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="collections" /> argument is a 0-length array or contains a null element.</exception>
		/// <exception cref="T:System.ObjectDisposedException">At least one of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances has been disposed.</exception>
		// Token: 0x0600256A RID: 9578 RVA: 0x000ADFE6 File Offset: 0x000AC1E6
		[global::__DynamicallyInvokable]
		public static int TryTakeFromAny(BlockingCollection<T>[] collections, out T item, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			BlockingCollection<T>.ValidateMillisecondsTimeout(millisecondsTimeout);
			return BlockingCollection<T>.TryTakeFromAnyCore(collections, out item, millisecondsTimeout, false, cancellationToken);
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000ADFF8 File Offset: 0x000AC1F8
		private static int TryTakeFromAnyCore(BlockingCollection<T>[] collections, out T item, int millisecondsTimeout, bool isTakeOperation, CancellationToken externalCancellationToken)
		{
			BlockingCollection<T>.ValidateCollectionsArray(collections, false);
			for (int i = 0; i < collections.Length; i++)
			{
				if (!collections[i].IsCompleted && collections[i].m_occupiedNodes.CurrentCount > 0 && collections[i].TryTake(out item))
				{
					return i;
				}
			}
			return BlockingCollection<T>.TryTakeFromAnyCoreSlow(collections, out item, millisecondsTimeout, isTakeOperation, externalCancellationToken);
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000AE04C File Offset: 0x000AC24C
		private static int TryTakeFromAnyCoreSlow(BlockingCollection<T>[] collections, out T item, int millisecondsTimeout, bool isTakeOperation, CancellationToken externalCancellationToken)
		{
			int num = millisecondsTimeout;
			uint num2 = 0U;
			if (millisecondsTimeout != -1)
			{
				num2 = (uint)Environment.TickCount;
			}
			while (millisecondsTimeout == -1 || num >= 0)
			{
				CancellationToken[] array;
				List<WaitHandle> handles = BlockingCollection<T>.GetHandles(collections, externalCancellationToken, false, out array);
				if (handles.Count == 0 && isTakeOperation)
				{
					throw new ArgumentException(SR.GetString("BlockingCollection_CantTakeAnyWhenAllDone"), "collections");
				}
				if (handles.Count != 0)
				{
					using (CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(array))
					{
						handles.Add(cancellationTokenSource.Token.WaitHandle);
						int num3 = WaitHandle.WaitAny(handles.ToArray(), num, false);
						if (cancellationTokenSource.IsCancellationRequested && externalCancellationToken.IsCancellationRequested)
						{
							throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), externalCancellationToken);
						}
						if (!cancellationTokenSource.IsCancellationRequested)
						{
							if (num3 == 258)
							{
								break;
							}
							if (collections.Length != handles.Count - 1)
							{
								for (int i = 0; i < collections.Length; i++)
								{
									if (collections[i].m_occupiedNodes.AvailableWaitHandle == handles[num3])
									{
										num3 = i;
										break;
									}
								}
							}
							if (collections[num3].TryTake(out item))
							{
								return num3;
							}
						}
					}
					if (millisecondsTimeout != -1)
					{
						num = BlockingCollection<T>.UpdateTimeOut(num2, millisecondsTimeout);
						continue;
					}
					continue;
				}
				break;
			}
			item = default(T);
			return -1;
		}

		/// <summary>Marks the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instances as not accepting any more additions.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		// Token: 0x0600256D RID: 9581 RVA: 0x000AE1A0 File Offset: 0x000AC3A0
		[global::__DynamicallyInvokable]
		public void CompleteAdding()
		{
			this.CheckDisposed();
			if (this.IsAddingCompleted)
			{
				return;
			}
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int currentAdders = this.m_currentAdders;
				if ((currentAdders & -2147483648) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_currentAdders, currentAdders | -2147483648, currentAdders) == currentAdders)
				{
					goto Block_4;
				}
				spinWait.SpinOnce();
			}
			spinWait.Reset();
			while (this.m_currentAdders != -2147483648)
			{
				spinWait.SpinOnce();
			}
			return;
			Block_4:
			spinWait.Reset();
			while (this.m_currentAdders != -2147483648)
			{
				spinWait.SpinOnce();
			}
			if (this.Count == 0)
			{
				this.CancelWaitingConsumers();
			}
			this.CancelWaitingProducers();
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000AE24B File Offset: 0x000AC44B
		private void CancelWaitingConsumers()
		{
			this.m_ConsumersCancellationTokenSource.Cancel();
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000AE258 File Offset: 0x000AC458
		private void CancelWaitingProducers()
		{
			this.m_ProducersCancellationTokenSource.Cancel();
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> class.</summary>
		// Token: 0x06002570 RID: 9584 RVA: 0x000AE265 File Offset: 0x000AC465
		[global::__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases resources used by the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance.</summary>
		/// <param name="disposing">Whether being disposed explicitly (true) or due to a finalizer (false).</param>
		// Token: 0x06002571 RID: 9585 RVA: 0x000AE274 File Offset: 0x000AC474
		[global::__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_isDisposed)
			{
				if (this.m_freeNodes != null)
				{
					this.m_freeNodes.Dispose();
				}
				this.m_occupiedNodes.Dispose();
				this.m_isDisposed = true;
			}
		}

		/// <summary>Copies the items from the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance into a new array.</summary>
		/// <returns>An array containing copies of the elements of the collection.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		// Token: 0x06002572 RID: 9586 RVA: 0x000AE2A3 File Offset: 0x000AC4A3
		[global::__DynamicallyInvokable]
		public T[] ToArray()
		{
			this.CheckDisposed();
			return this.m_collection.ToArray();
		}

		/// <summary>Copies all of the items in the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> argument is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> argument is equal to or greater than the length of the <paramref name="array" />.  
		///  The destination array is too small to hold all of the BlockingCcollection elements.  
		///  The array rank doesn't match.  
		///  The array type is incompatible with the type of the BlockingCollection elements.</exception>
		// Token: 0x06002573 RID: 9587 RVA: 0x000AE2B6 File Offset: 0x000AC4B6
		[global::__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Copies all of the items in the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> instance. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> argument is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> argument is equal to or greater than the length of the <paramref name="array" />, the array is multidimensional, or the type parameter for the collection cannot be cast automatically to the type of the destination array.</exception>
		// Token: 0x06002574 RID: 9588 RVA: 0x000AE2C0 File Offset: 0x000AC4C0
		[global::__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			this.CheckDisposed();
			T[] array2 = this.m_collection.ToArray();
			try
			{
				Array.Copy(array2, 0, array, index, array2.Length);
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentNullException("array");
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.GetString("BlockingCollection_CopyTo_NonNegative"));
			}
			catch (ArgumentException)
			{
				throw new ArgumentException(SR.GetString("BlockingCollection_CopyTo_TooManyElems"), "index");
			}
			catch (RankException)
			{
				throw new ArgumentException(SR.GetString("BlockingCollection_CopyTo_MultiDim"), "array");
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(SR.GetString("BlockingCollection_CopyTo_IncorrectType"), "array");
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException(SR.GetString("BlockingCollection_CopyTo_IncorrectType"), "array");
			}
		}

		/// <summary>Provides a consuming <see cref="T:System.Collections.Generic.IEnumerator`1" /> for items in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that removes and returns items from the collection.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		// Token: 0x06002575 RID: 9589 RVA: 0x000AE3BC File Offset: 0x000AC5BC
		[global::__DynamicallyInvokable]
		public IEnumerable<T> GetConsumingEnumerable()
		{
			return this.GetConsumingEnumerable(CancellationToken.None);
		}

		/// <summary>Provides a consuming <see cref="T:System.Collections.Generic.IEnumerable`1" /> for items in the collection.</summary>
		/// <param name="cancellationToken">A cancellation token to observe.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that removes and returns items from the collection.</returns>
		/// <exception cref="T:System.OperationCanceledException">If the <see cref="T:System.Threading.CancellationToken" /> is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed</exception>
		// Token: 0x06002576 RID: 9590 RVA: 0x000AE3C9 File Offset: 0x000AC5C9
		[global::__DynamicallyInvokable]
		public IEnumerable<T> GetConsumingEnumerable(CancellationToken cancellationToken)
		{
			CancellationTokenSource linkedTokenSource = null;
			try
			{
				linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this.m_ConsumersCancellationTokenSource.Token);
				while (!this.IsCompleted)
				{
					T t;
					if (this.TryTakeWithNoTimeValidation(out t, -1, cancellationToken, linkedTokenSource))
					{
						yield return t;
					}
				}
			}
			finally
			{
				if (linkedTokenSource != null)
				{
					linkedTokenSource.Dispose();
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000AE3E0 File Offset: 0x000AC5E0
		[global::__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			this.CheckDisposed();
			return this.m_collection.GetEnumerator();
		}

		/// <summary>Provides an <see cref="T:System.Collections.IEnumerator" /> for items in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the items in the collection.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> has been disposed.</exception>
		// Token: 0x06002578 RID: 9592 RVA: 0x000AE3F3 File Offset: 0x000AC5F3
		[global::__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000AE3FC File Offset: 0x000AC5FC
		private static void ValidateCollectionsArray(BlockingCollection<T>[] collections, bool isAddOperation)
		{
			if (collections == null)
			{
				throw new ArgumentNullException("collections");
			}
			if (collections.Length < 1)
			{
				throw new ArgumentException(SR.GetString("BlockingCollection_ValidateCollectionsArray_ZeroSize"), "collections");
			}
			if ((!BlockingCollection<T>.IsSTAThread && collections.Length > 63) || (BlockingCollection<T>.IsSTAThread && collections.Length > 62))
			{
				throw new ArgumentOutOfRangeException("collections", SR.GetString("BlockingCollection_ValidateCollectionsArray_LargeSize"));
			}
			for (int i = 0; i < collections.Length; i++)
			{
				if (collections[i] == null)
				{
					throw new ArgumentException(SR.GetString("BlockingCollection_ValidateCollectionsArray_NullElems"), "collections");
				}
				if (collections[i].m_isDisposed)
				{
					throw new ObjectDisposedException("collections", SR.GetString("BlockingCollection_ValidateCollectionsArray_DispElems"));
				}
				if (isAddOperation && collections[i].IsAddingCompleted)
				{
					throw new ArgumentException(SR.GetString("BlockingCollection_CantAddAnyWhenCompleted"), "collections");
				}
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x000AE4CC File Offset: 0x000AC6CC
		private static bool IsSTAThread
		{
			get
			{
				return Thread.CurrentThread.GetApartmentState() == ApartmentState.STA;
			}
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000AE4DC File Offset: 0x000AC6DC
		private static void ValidateTimeout(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if ((num < 0L || num > 2147483647L) && num != -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, string.Format(CultureInfo.InvariantCulture, SR.GetString("BlockingCollection_TimeoutInvalid"), new object[] { int.MaxValue }));
			}
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000AE540 File Offset: 0x000AC740
		private static void ValidateMillisecondsTimeout(int millisecondsTimeout)
		{
			if (millisecondsTimeout < 0 && millisecondsTimeout != -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, string.Format(CultureInfo.InvariantCulture, SR.GetString("BlockingCollection_TimeoutInvalid"), new object[] { int.MaxValue }));
			}
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x000AE58D File Offset: 0x000AC78D
		private void CheckDisposed()
		{
			if (this.m_isDisposed)
			{
				throw new ObjectDisposedException("BlockingCollection", SR.GetString("BlockingCollection_Disposed"));
			}
		}

		// Token: 0x0400203A RID: 8250
		private IProducerConsumerCollection<T> m_collection;

		// Token: 0x0400203B RID: 8251
		private int m_boundedCapacity;

		// Token: 0x0400203C RID: 8252
		private const int NON_BOUNDED = -1;

		// Token: 0x0400203D RID: 8253
		private SemaphoreSlim m_freeNodes;

		// Token: 0x0400203E RID: 8254
		private SemaphoreSlim m_occupiedNodes;

		// Token: 0x0400203F RID: 8255
		private bool m_isDisposed;

		// Token: 0x04002040 RID: 8256
		private CancellationTokenSource m_ConsumersCancellationTokenSource;

		// Token: 0x04002041 RID: 8257
		private CancellationTokenSource m_ProducersCancellationTokenSource;

		// Token: 0x04002042 RID: 8258
		private volatile int m_currentAdders;

		// Token: 0x04002043 RID: 8259
		private const int COMPLETE_ADDING_ON_MASK = -2147483648;
	}
}
