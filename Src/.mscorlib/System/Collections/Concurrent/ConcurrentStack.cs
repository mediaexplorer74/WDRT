using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a thread-safe last in-first out (LIFO) collection.</summary>
	/// <typeparam name="T">The type of the elements contained in the stack.</typeparam>
	// Token: 0x020004A8 RID: 1192
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<>))]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class ConcurrentStack<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> class.</summary>
		// Token: 0x0600391B RID: 14619 RVA: 0x000DBD63 File Offset: 0x000D9F63
		[__DynamicallyInvokable]
		public ConcurrentStack()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> class that contains elements copied from the specified collection</summary>
		/// <param name="collection">The collection whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collection" /> argument is null.</exception>
		// Token: 0x0600391C RID: 14620 RVA: 0x000DBD6B File Offset: 0x000D9F6B
		[__DynamicallyInvokable]
		public ConcurrentStack(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000DBD88 File Offset: 0x000D9F88
		private void InitializeFromCollection(IEnumerable<T> collection)
		{
			ConcurrentStack<T>.Node node = null;
			foreach (T t in collection)
			{
				node = new ConcurrentStack<T>.Node(t)
				{
					m_next = node
				};
			}
			this.m_head = node;
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000DBDE4 File Offset: 0x000D9FE4
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.m_serializationArray = this.ToArray();
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000DBDF4 File Offset: 0x000D9FF4
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			ConcurrentStack<T>.Node node = null;
			ConcurrentStack<T>.Node node2 = null;
			for (int i = 0; i < this.m_serializationArray.Length; i++)
			{
				ConcurrentStack<T>.Node node3 = new ConcurrentStack<T>.Node(this.m_serializationArray[i]);
				if (node == null)
				{
					node2 = node3;
				}
				else
				{
					node.m_next = node3;
				}
				node = node3;
			}
			this.m_head = node2;
			this.m_serializationArray = null;
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06003920 RID: 14624 RVA: 0x000DBE4A File Offset: 0x000DA04A
		[__DynamicallyInvokable]
		public bool IsEmpty
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_head == null;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</returns>
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x000DBE58 File Offset: 0x000DA058
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				int num = 0;
				for (ConcurrentStack<T>.Node node = this.m_head; node != null; node = node.m_next)
				{
					num++;
				}
				return num;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot.</summary>
		/// <returns>Always returns <see langword="false" /> to indicate access is not synchronized.</returns>
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06003922 RID: 14626 RVA: 0x000DBE81 File Offset: 0x000DA081
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
		/// <returns>Returns null (Nothing in Visual Basic).</returns>
		/// <exception cref="T:System.NotSupportedException">The SyncRoot property is not supported</exception>
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06003923 RID: 14627 RVA: 0x000DBE84 File Offset: 0x000DA084
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported"));
			}
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</summary>
		// Token: 0x06003924 RID: 14628 RVA: 0x000DBE95 File Offset: 0x000DA095
		[__DynamicallyInvokable]
		public void Clear()
		{
			this.m_head = null;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional. -or- <paramref name="array" /> does not have zero-based indexing. -or- <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. -or- The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003925 RID: 14629 RVA: 0x000DBEA0 File Offset: 0x000DA0A0
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			((ICollection)this.ToList()).CopyTo(array, index);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- The number of elements in the source <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06003926 RID: 14630 RVA: 0x000DBEBD File Offset: 0x000DA0BD
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this.ToList().CopyTo(array, index);
		}

		/// <summary>Inserts an object at the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</summary>
		/// <param name="item">The object to push onto the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
		// Token: 0x06003927 RID: 14631 RVA: 0x000DBEDC File Offset: 0x000DA0DC
		[__DynamicallyInvokable]
		public void Push(T item)
		{
			ConcurrentStack<T>.Node node = new ConcurrentStack<T>.Node(item);
			node.m_next = this.m_head;
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, node, node.m_next) == node.m_next)
			{
				return;
			}
			this.PushCore(node, node);
		}

		/// <summary>Inserts multiple objects at the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> atomically.</summary>
		/// <param name="items">The objects to push onto the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="items" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x06003928 RID: 14632 RVA: 0x000DBF21 File Offset: 0x000DA121
		[__DynamicallyInvokable]
		public void PushRange(T[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.PushRange(items, 0, items.Length);
		}

		/// <summary>Inserts multiple objects at the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> atomically.</summary>
		/// <param name="items">The objects to push onto the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</param>
		/// <param name="startIndex">The zero-based offset in <paramref name="items" /> at which to begin inserting elements onto the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</param>
		/// <param name="count">The number of elements to be inserted onto the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="items" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="count" /> is negative. Or <paramref name="startIndex" /> is greater than or equal to the length of <paramref name="items" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> + <paramref name="count" /> is greater than the length of <paramref name="items" />.</exception>
		// Token: 0x06003929 RID: 14633 RVA: 0x000DBF3C File Offset: 0x000DA13C
		[__DynamicallyInvokable]
		public void PushRange(T[] items, int startIndex, int count)
		{
			this.ValidatePushPopRangeInput(items, startIndex, count);
			if (count == 0)
			{
				return;
			}
			ConcurrentStack<T>.Node node2;
			ConcurrentStack<T>.Node node = (node2 = new ConcurrentStack<T>.Node(items[startIndex]));
			for (int i = startIndex + 1; i < startIndex + count; i++)
			{
				node2 = new ConcurrentStack<T>.Node(items[i])
				{
					m_next = node2
				};
			}
			node.m_next = this.m_head;
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, node2, node.m_next) == node.m_next)
			{
				return;
			}
			this.PushCore(node2, node);
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000DBFBC File Offset: 0x000DA1BC
		private void PushCore(ConcurrentStack<T>.Node head, ConcurrentStack<T>.Node tail)
		{
			SpinWait spinWait = default(SpinWait);
			do
			{
				spinWait.SpinOnce();
				tail.m_next = this.m_head;
			}
			while (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, head, tail.m_next) != tail.m_next);
			if (CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPushFailed(spinWait.Count);
			}
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000DC020 File Offset: 0x000DA220
		private void ValidatePushPopRangeInput(T[] items, int startIndex, int count)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ConcurrentStack_PushPopRange_CountOutOfRange"));
			}
			int num = items.Length;
			if (startIndex >= num || startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ConcurrentStack_PushPopRange_StartOutOfRange"));
			}
			if (num - count < startIndex)
			{
				throw new ArgumentException(Environment.GetResourceString("ConcurrentStack_PushPopRange_InvalidCount"));
			}
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000DC08B File Offset: 0x000DA28B
		[__DynamicallyInvokable]
		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Push(item);
			return true;
		}

		/// <summary>Attempts to return an object from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> without removing it.</summary>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> or an unspecified value if the operation failed.</param>
		/// <returns>true if and object was returned successfully; otherwise, false.</returns>
		// Token: 0x0600392D RID: 14637 RVA: 0x000DC098 File Offset: 0x000DA298
		[__DynamicallyInvokable]
		public bool TryPeek(out T result)
		{
			ConcurrentStack<T>.Node head = this.m_head;
			if (head == null)
			{
				result = default(T);
				return false;
			}
			result = head.m_value;
			return true;
		}

		/// <summary>Attempts to pop and return the object at the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</summary>
		/// <param name="result">When this method returns, if the operation was successful, <paramref name="result" /> contains the object removed. If no object was available to be removed, the value is unspecified.</param>
		/// <returns>true if an element was removed and returned from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> successfully; otherwise, false.</returns>
		// Token: 0x0600392E RID: 14638 RVA: 0x000DC0C8 File Offset: 0x000DA2C8
		[__DynamicallyInvokable]
		public bool TryPop(out T result)
		{
			ConcurrentStack<T>.Node head = this.m_head;
			if (head == null)
			{
				result = default(T);
				return false;
			}
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, head.m_next, head) == head)
			{
				result = head.m_value;
				return true;
			}
			return this.TryPopCore(out result);
		}

		/// <summary>Attempts to pop and return multiple objects from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> atomically.</summary>
		/// <param name="items">The <see cref="T:System.Array" /> to which objects popped from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> will be added.</param>
		/// <returns>The number of objects successfully popped from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> and inserted in <paramref name="items" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="items" /> is a null argument (Nothing in Visual Basic).</exception>
		// Token: 0x0600392F RID: 14639 RVA: 0x000DC114 File Offset: 0x000DA314
		[__DynamicallyInvokable]
		public int TryPopRange(T[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			return this.TryPopRange(items, 0, items.Length);
		}

		/// <summary>Attempts to pop and return multiple objects from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> atomically.</summary>
		/// <param name="items">The <see cref="T:System.Array" /> to which objects popped from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> will be added.</param>
		/// <param name="startIndex">The zero-based offset in <paramref name="items" /> at which to begin inserting elements from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</param>
		/// <param name="count">The number of elements to be popped from top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> and inserted into <paramref name="items" />.</param>
		/// <returns>The number of objects successfully popped from the top of the stack and inserted in <paramref name="items" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="items" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="count" /> is negative. Or <paramref name="startIndex" /> is greater than or equal to the length of <paramref name="items" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> + <paramref name="count" /> is greater than the length of <paramref name="items" />.</exception>
		// Token: 0x06003930 RID: 14640 RVA: 0x000DC130 File Offset: 0x000DA330
		[__DynamicallyInvokable]
		public int TryPopRange(T[] items, int startIndex, int count)
		{
			this.ValidatePushPopRangeInput(items, startIndex, count);
			if (count == 0)
			{
				return 0;
			}
			ConcurrentStack<T>.Node node;
			int num = this.TryPopCore(count, out node);
			if (num > 0)
			{
				this.CopyRemovedItems(node, items, startIndex, num);
			}
			return num;
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000DC164 File Offset: 0x000DA364
		private bool TryPopCore(out T result)
		{
			ConcurrentStack<T>.Node node;
			if (this.TryPopCore(1, out node) == 1)
			{
				result = node.m_value;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000DC194 File Offset: 0x000DA394
		private int TryPopCore(int count, out ConcurrentStack<T>.Node poppedHead)
		{
			SpinWait spinWait = default(SpinWait);
			int num = 1;
			Random random = new Random(Environment.TickCount & int.MaxValue);
			ConcurrentStack<T>.Node head;
			int num2;
			for (;;)
			{
				head = this.m_head;
				if (head == null)
				{
					break;
				}
				ConcurrentStack<T>.Node node = head;
				num2 = 1;
				while (num2 < count && node.m_next != null)
				{
					node = node.m_next;
					num2++;
				}
				if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, node.m_next, head) == head)
				{
					goto Block_5;
				}
				for (int i = 0; i < num; i++)
				{
					spinWait.SpinOnce();
				}
				num = (spinWait.NextSpinWillYield ? random.Next(1, 8) : (num * 2));
			}
			if (count == 1 && CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPopFailed(spinWait.Count);
			}
			poppedHead = null;
			return 0;
			Block_5:
			if (count == 1 && CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPopFailed(spinWait.Count);
			}
			poppedHead = head;
			return num2;
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000DC280 File Offset: 0x000DA480
		private void CopyRemovedItems(ConcurrentStack<T>.Node head, T[] collection, int startIndex, int nodesCount)
		{
			ConcurrentStack<T>.Node node = head;
			for (int i = startIndex; i < startIndex + nodesCount; i++)
			{
				collection[i] = node.m_value;
				node = node.m_next;
			}
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000DC2B2 File Offset: 0x000DA4B2
		[__DynamicallyInvokable]
		bool IProducerConsumerCollection<T>.TryTake(out T item)
		{
			return this.TryPop(out item);
		}

		/// <summary>Copies the items stored in the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> to a new array.</summary>
		/// <returns>A new array containing a snapshot of elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</returns>
		// Token: 0x06003935 RID: 14645 RVA: 0x000DC2BB File Offset: 0x000DA4BB
		[__DynamicallyInvokable]
		public T[] ToArray()
		{
			return this.ToList().ToArray();
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000DC2C8 File Offset: 0x000DA4C8
		private List<T> ToList()
		{
			List<T> list = new List<T>();
			for (ConcurrentStack<T>.Node node = this.m_head; node != null; node = node.m_next)
			{
				list.Add(node.m_value);
			}
			return list;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</returns>
		// Token: 0x06003937 RID: 14647 RVA: 0x000DC2FD File Offset: 0x000DA4FD
		[__DynamicallyInvokable]
		public IEnumerator<T> GetEnumerator()
		{
			return this.GetEnumerator(this.m_head);
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000DC30D File Offset: 0x000DA50D
		private IEnumerator<T> GetEnumerator(ConcurrentStack<T>.Node head)
		{
			for (ConcurrentStack<T>.Node current = head; current != null; current = current.m_next)
			{
				yield return current.m_value;
			}
			yield break;
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003939 RID: 14649 RVA: 0x000DC31C File Offset: 0x000DA51C
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x04001918 RID: 6424
		[NonSerialized]
		private volatile ConcurrentStack<T>.Node m_head;

		// Token: 0x04001919 RID: 6425
		private T[] m_serializationArray;

		// Token: 0x0400191A RID: 6426
		private const int BACKOFF_MAX_YIELDS = 8;

		// Token: 0x02000BBC RID: 3004
		private class Node
		{
			// Token: 0x06006E80 RID: 28288 RVA: 0x0017E7C8 File Offset: 0x0017C9C8
			internal Node(T value)
			{
				this.m_value = value;
				this.m_next = null;
			}

			// Token: 0x0400358E RID: 13710
			internal readonly T m_value;

			// Token: 0x0400358F RID: 13711
			internal ConcurrentStack<T>.Node m_next;
		}
	}
}
