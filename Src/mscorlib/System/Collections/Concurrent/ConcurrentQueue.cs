using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a thread-safe first in-first out (FIFO) collection.</summary>
	/// <typeparam name="T">The type of the elements contained in the queue.</typeparam>
	// Token: 0x020004AD RID: 1197
	[ComVisible(false)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<>))]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class ConcurrentQueue<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> class.</summary>
		// Token: 0x0600398F RID: 14735 RVA: 0x000DDB48 File Offset: 0x000DBD48
		[__DynamicallyInvokable]
		public ConcurrentQueue()
		{
			this.m_head = (this.m_tail = new ConcurrentQueue<T>.Segment(0L, this));
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x000DDB78 File Offset: 0x000DBD78
		private void InitializeFromCollection(IEnumerable<T> collection)
		{
			ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(0L, this);
			this.m_head = segment;
			int num = 0;
			foreach (T t in collection)
			{
				segment.UnsafeAdd(t);
				num++;
				if (num >= 32)
				{
					segment = segment.UnsafeGrow();
					num = 0;
				}
			}
			this.m_tail = segment;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> class that contains elements copied from the specified collection</summary>
		/// <param name="collection">The collection whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collection" /> argument is null.</exception>
		// Token: 0x06003991 RID: 14737 RVA: 0x000DDBF0 File Offset: 0x000DBDF0
		[__DynamicallyInvokable]
		public ConcurrentQueue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x000DDC0D File Offset: 0x000DBE0D
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.m_serializationArray = this.ToArray();
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x000DDC1B File Offset: 0x000DBE1B
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.InitializeFromCollection(this.m_serializationArray);
			this.m_serializationArray = null;
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
		// Token: 0x06003994 RID: 14740 RVA: 0x000DDC30 File Offset: 0x000DBE30
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			((ICollection)this.ToList()).CopyTo(array, index);
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot.</summary>
		/// <returns>Always returns <see langword="false" /> to indicate access is not synchronized.</returns>
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06003995 RID: 14741 RVA: 0x000DDC4D File Offset: 0x000DBE4D
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
		/// <returns>Returns null  (Nothing in Visual Basic).</returns>
		/// <exception cref="T:System.NotSupportedException">The SyncRoot property is not supported.</exception>
		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06003996 RID: 14742 RVA: 0x000DDC50 File Offset: 0x000DBE50
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported"));
			}
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003997 RID: 14743 RVA: 0x000DDC61 File Offset: 0x000DBE61
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x000DDC69 File Offset: 0x000DBE69
		[__DynamicallyInvokable]
		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Enqueue(item);
			return true;
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000DDC73 File Offset: 0x000DBE73
		[__DynamicallyInvokable]
		bool IProducerConsumerCollection<T>.TryTake(out T item)
		{
			return this.TryDequeue(out item);
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600399A RID: 14746 RVA: 0x000DDC7C File Offset: 0x000DBE7C
		[__DynamicallyInvokable]
		public bool IsEmpty
		{
			[__DynamicallyInvokable]
			get
			{
				ConcurrentQueue<T>.Segment segment = this.m_head;
				if (!segment.IsEmpty)
				{
					return false;
				}
				if (segment.Next == null)
				{
					return true;
				}
				SpinWait spinWait = default(SpinWait);
				while (segment.IsEmpty)
				{
					if (segment.Next == null)
					{
						return true;
					}
					spinWait.SpinOnce();
					segment = this.m_head;
				}
				return false;
			}
		}

		/// <summary>Copies the elements stored in the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> to a new array.</summary>
		/// <returns>A new array containing a snapshot of elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</returns>
		// Token: 0x0600399B RID: 14747 RVA: 0x000DDCD3 File Offset: 0x000DBED3
		[__DynamicallyInvokable]
		public T[] ToArray()
		{
			return this.ToList().ToArray();
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000DDCE0 File Offset: 0x000DBEE0
		private List<T> ToList()
		{
			Interlocked.Increment(ref this.m_numSnapshotTakers);
			List<T> list = new List<T>();
			try
			{
				ConcurrentQueue<T>.Segment segment;
				ConcurrentQueue<T>.Segment segment2;
				int num;
				int num2;
				this.GetHeadTailPositions(out segment, out segment2, out num, out num2);
				if (segment == segment2)
				{
					segment.AddToList(list, num, num2);
				}
				else
				{
					segment.AddToList(list, num, 31);
					for (ConcurrentQueue<T>.Segment segment3 = segment.Next; segment3 != segment2; segment3 = segment3.Next)
					{
						segment3.AddToList(list, 0, 31);
					}
					segment2.AddToList(list, 0, num2);
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.m_numSnapshotTakers);
			}
			return list;
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x000DDD74 File Offset: 0x000DBF74
		private void GetHeadTailPositions(out ConcurrentQueue<T>.Segment head, out ConcurrentQueue<T>.Segment tail, out int headLow, out int tailHigh)
		{
			head = this.m_head;
			tail = this.m_tail;
			headLow = head.Low;
			tailHigh = tail.High;
			SpinWait spinWait = default(SpinWait);
			while (head != this.m_head || tail != this.m_tail || headLow != head.Low || tailHigh != tail.High || head.m_index > tail.m_index)
			{
				spinWait.SpinOnce();
				head = this.m_head;
				tail = this.m_tail;
				headLow = head.Low;
				tailHigh = tail.High;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</returns>
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000DDE20 File Offset: 0x000DC020
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				ConcurrentQueue<T>.Segment segment;
				ConcurrentQueue<T>.Segment segment2;
				int num;
				int num2;
				this.GetHeadTailPositions(out segment, out segment2, out num, out num2);
				if (segment == segment2)
				{
					return num2 - num + 1;
				}
				int num3 = 32 - num;
				num3 += 32 * (int)(segment2.m_index - segment.m_index - 1L);
				return num3 + (num2 + 1);
			}
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- The number of elements in the source <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x0600399F RID: 14751 RVA: 0x000DDE6E File Offset: 0x000DC06E
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this.ToList().CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</summary>
		/// <returns>An enumerator for the contents of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</returns>
		// Token: 0x060039A0 RID: 14752 RVA: 0x000DDE8C File Offset: 0x000DC08C
		[__DynamicallyInvokable]
		public IEnumerator<T> GetEnumerator()
		{
			Interlocked.Increment(ref this.m_numSnapshotTakers);
			ConcurrentQueue<T>.Segment segment;
			ConcurrentQueue<T>.Segment segment2;
			int num;
			int num2;
			this.GetHeadTailPositions(out segment, out segment2, out num, out num2);
			return this.GetEnumerator(segment, segment2, num, num2);
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000DDEBD File Offset: 0x000DC0BD
		private IEnumerator<T> GetEnumerator(ConcurrentQueue<T>.Segment head, ConcurrentQueue<T>.Segment tail, int headLow, int tailHigh)
		{
			try
			{
				SpinWait spin = default(SpinWait);
				if (head == tail)
				{
					int num;
					for (int i = headLow; i <= tailHigh; i = num + 1)
					{
						spin.Reset();
						while (!head.m_state[i].m_value)
						{
							spin.SpinOnce();
						}
						yield return head.m_array[i];
						num = i;
					}
				}
				else
				{
					int num;
					for (int i = headLow; i < 32; i = num + 1)
					{
						spin.Reset();
						while (!head.m_state[i].m_value)
						{
							spin.SpinOnce();
						}
						yield return head.m_array[i];
						num = i;
					}
					ConcurrentQueue<T>.Segment curr;
					for (curr = head.Next; curr != tail; curr = curr.Next)
					{
						for (int i = 0; i < 32; i = num + 1)
						{
							spin.Reset();
							while (!curr.m_state[i].m_value)
							{
								spin.SpinOnce();
							}
							yield return curr.m_array[i];
							num = i;
						}
					}
					for (int i = 0; i <= tailHigh; i = num + 1)
					{
						spin.Reset();
						while (!tail.m_state[i].m_value)
						{
							spin.SpinOnce();
						}
						yield return tail.m_array[i];
						num = i;
					}
					curr = null;
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.m_numSnapshotTakers);
			}
			yield break;
			yield break;
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.</summary>
		/// <param name="item">The object to add to the end of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
		// Token: 0x060039A2 RID: 14754 RVA: 0x000DDEEC File Offset: 0x000DC0EC
		[__DynamicallyInvokable]
		public void Enqueue(T item)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				ConcurrentQueue<T>.Segment tail = this.m_tail;
				if (tail.TryAppend(item))
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		/// <summary>Tries to remove and return the object at the beginning of the concurrent queue.</summary>
		/// <param name="result">When this method returns, if the operation was successful, <paramref name="result" /> contains the object removed. If no object was available to be removed, the value is unspecified.</param>
		/// <returns>
		///   <see langword="true" /> if an element was removed and returned from the beginning of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x060039A3 RID: 14755 RVA: 0x000DDF1C File Offset: 0x000DC11C
		[__DynamicallyInvokable]
		public bool TryDequeue(out T result)
		{
			while (!this.IsEmpty)
			{
				ConcurrentQueue<T>.Segment head = this.m_head;
				if (head.TryRemove(out result))
				{
					return true;
				}
			}
			result = default(T);
			return false;
		}

		/// <summary>Tries to return an object from the beginning of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> without removing it.</summary>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object from the beginning of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> or an unspecified value if the operation failed.</param>
		/// <returns>
		///   <see langword="true" /> if an object was returned successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x060039A4 RID: 14756 RVA: 0x000DDF50 File Offset: 0x000DC150
		[__DynamicallyInvokable]
		public bool TryPeek(out T result)
		{
			Interlocked.Increment(ref this.m_numSnapshotTakers);
			while (!this.IsEmpty)
			{
				ConcurrentQueue<T>.Segment head = this.m_head;
				if (head.TryPeek(out result))
				{
					Interlocked.Decrement(ref this.m_numSnapshotTakers);
					return true;
				}
			}
			result = default(T);
			Interlocked.Decrement(ref this.m_numSnapshotTakers);
			return false;
		}

		// Token: 0x0400192E RID: 6446
		[NonSerialized]
		private volatile ConcurrentQueue<T>.Segment m_head;

		// Token: 0x0400192F RID: 6447
		[NonSerialized]
		private volatile ConcurrentQueue<T>.Segment m_tail;

		// Token: 0x04001930 RID: 6448
		private T[] m_serializationArray;

		// Token: 0x04001931 RID: 6449
		private const int SEGMENT_SIZE = 32;

		// Token: 0x04001932 RID: 6450
		[NonSerialized]
		internal volatile int m_numSnapshotTakers;

		// Token: 0x02000BC2 RID: 3010
		private class Segment
		{
			// Token: 0x06006E96 RID: 28310 RVA: 0x0017EAA3 File Offset: 0x0017CCA3
			internal Segment(long index, ConcurrentQueue<T> source)
			{
				this.m_array = new T[32];
				this.m_state = new VolatileBool[32];
				this.m_high = -1;
				this.m_index = index;
				this.m_source = source;
			}

			// Token: 0x170012E3 RID: 4835
			// (get) Token: 0x06006E97 RID: 28311 RVA: 0x0017EAE2 File Offset: 0x0017CCE2
			internal ConcurrentQueue<T>.Segment Next
			{
				get
				{
					return this.m_next;
				}
			}

			// Token: 0x170012E4 RID: 4836
			// (get) Token: 0x06006E98 RID: 28312 RVA: 0x0017EAEC File Offset: 0x0017CCEC
			internal bool IsEmpty
			{
				get
				{
					return this.Low > this.High;
				}
			}

			// Token: 0x06006E99 RID: 28313 RVA: 0x0017EAFC File Offset: 0x0017CCFC
			internal void UnsafeAdd(T value)
			{
				this.m_high++;
				this.m_array[this.m_high] = value;
				this.m_state[this.m_high].m_value = true;
			}

			// Token: 0x06006E9A RID: 28314 RVA: 0x0017EB50 File Offset: 0x0017CD50
			internal ConcurrentQueue<T>.Segment UnsafeGrow()
			{
				ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(this.m_index + 1L, this.m_source);
				this.m_next = segment;
				return segment;
			}

			// Token: 0x06006E9B RID: 28315 RVA: 0x0017EB80 File Offset: 0x0017CD80
			internal void Grow()
			{
				ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(this.m_index + 1L, this.m_source);
				this.m_next = segment;
				this.m_source.m_tail = this.m_next;
			}

			// Token: 0x06006E9C RID: 28316 RVA: 0x0017EBC4 File Offset: 0x0017CDC4
			internal bool TryAppend(T value)
			{
				if (this.m_high >= 31)
				{
					return false;
				}
				int num = 32;
				try
				{
				}
				finally
				{
					num = Interlocked.Increment(ref this.m_high);
					if (num <= 31)
					{
						this.m_array[num] = value;
						this.m_state[num].m_value = true;
					}
					if (num == 31)
					{
						this.Grow();
					}
				}
				return num <= 31;
			}

			// Token: 0x06006E9D RID: 28317 RVA: 0x0017EC40 File Offset: 0x0017CE40
			internal bool TryRemove(out T result)
			{
				SpinWait spinWait = default(SpinWait);
				int i = this.Low;
				int num = this.High;
				while (i <= num)
				{
					if (Interlocked.CompareExchange(ref this.m_low, i + 1, i) == i)
					{
						SpinWait spinWait2 = default(SpinWait);
						while (!this.m_state[i].m_value)
						{
							spinWait2.SpinOnce();
						}
						result = this.m_array[i];
						if (this.m_source.m_numSnapshotTakers <= 0)
						{
							this.m_array[i] = default(T);
						}
						if (i + 1 >= 32)
						{
							spinWait2 = default(SpinWait);
							while (this.m_next == null)
							{
								spinWait2.SpinOnce();
							}
							this.m_source.m_head = this.m_next;
						}
						return true;
					}
					spinWait.SpinOnce();
					i = this.Low;
					num = this.High;
				}
				result = default(T);
				return false;
			}

			// Token: 0x06006E9E RID: 28318 RVA: 0x0017ED44 File Offset: 0x0017CF44
			internal bool TryPeek(out T result)
			{
				result = default(T);
				int low = this.Low;
				if (low > this.High)
				{
					return false;
				}
				SpinWait spinWait = default(SpinWait);
				while (!this.m_state[low].m_value)
				{
					spinWait.SpinOnce();
				}
				result = this.m_array[low];
				return true;
			}

			// Token: 0x06006E9F RID: 28319 RVA: 0x0017EDA8 File Offset: 0x0017CFA8
			internal void AddToList(List<T> list, int start, int end)
			{
				for (int i = start; i <= end; i++)
				{
					SpinWait spinWait = default(SpinWait);
					while (!this.m_state[i].m_value)
					{
						spinWait.SpinOnce();
					}
					list.Add(this.m_array[i]);
				}
			}

			// Token: 0x170012E5 RID: 4837
			// (get) Token: 0x06006EA0 RID: 28320 RVA: 0x0017EDFD File Offset: 0x0017CFFD
			internal int Low
			{
				get
				{
					return Math.Min(this.m_low, 32);
				}
			}

			// Token: 0x170012E6 RID: 4838
			// (get) Token: 0x06006EA1 RID: 28321 RVA: 0x0017EE0E File Offset: 0x0017D00E
			internal int High
			{
				get
				{
					return Math.Min(this.m_high, 31);
				}
			}

			// Token: 0x040035A3 RID: 13731
			internal volatile T[] m_array;

			// Token: 0x040035A4 RID: 13732
			internal volatile VolatileBool[] m_state;

			// Token: 0x040035A5 RID: 13733
			private volatile ConcurrentQueue<T>.Segment m_next;

			// Token: 0x040035A6 RID: 13734
			internal readonly long m_index;

			// Token: 0x040035A7 RID: 13735
			private volatile int m_low;

			// Token: 0x040035A8 RID: 13736
			private volatile int m_high;

			// Token: 0x040035A9 RID: 13737
			private volatile ConcurrentQueue<T> m_source;
		}
	}
}
