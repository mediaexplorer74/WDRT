using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a first-in, first-out collection of objects.</summary>
	// Token: 0x0200048D RID: 1165
	[DebuggerTypeProxy(typeof(Queue.QueueDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	[Serializable]
	public class Queue : ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the default initial capacity, and uses the default growth factor.</summary>
		// Token: 0x060037D2 RID: 14290 RVA: 0x000D7491 File Offset: 0x000D5691
		public Queue()
			: this(32, 2f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the specified initial capacity, and uses the default growth factor.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Queue" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x060037D3 RID: 14291 RVA: 0x000D74A0 File Offset: 0x000D56A0
		public Queue(int capacity)
			: this(capacity, 2f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the specified initial capacity, and uses the specified growth factor.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Queue" /> can contain.</param>
		/// <param name="growFactor">The factor by which the capacity of the <see cref="T:System.Collections.Queue" /> is expanded.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.  
		/// -or-  
		/// <paramref name="growFactor" /> is less than 1.0 or greater than 10.0.</exception>
		// Token: 0x060037D4 RID: 14292 RVA: 0x000D74B0 File Offset: 0x000D56B0
		public Queue(int capacity, float growFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if ((double)growFactor < 1.0 || (double)growFactor > 10.0)
			{
				throw new ArgumentOutOfRangeException("growFactor", Environment.GetResourceString("ArgumentOutOfRange_QueueGrowFactor", new object[] { 1, 10 }));
			}
			this._array = new object[capacity];
			this._head = 0;
			this._tail = 0;
			this._size = 0;
			this._growFactor = (int)(growFactor * 100f);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that contains elements copied from the specified collection, has the same initial capacity as the number of elements copied, and uses the default growth factor.</summary>
		/// <param name="col">The <see cref="T:System.Collections.ICollection" /> to copy elements from.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is <see langword="null" />.</exception>
		// Token: 0x060037D5 RID: 14293 RVA: 0x000D7554 File Offset: 0x000D5754
		public Queue(ICollection col)
			: this((col == null) ? 32 : col.Count)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Enqueue(obj);
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Queue" />.</returns>
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060037D6 RID: 14294 RVA: 0x000D759F File Offset: 0x000D579F
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.Queue" />.</returns>
		// Token: 0x060037D7 RID: 14295 RVA: 0x000D75A8 File Offset: 0x000D57A8
		public virtual object Clone()
		{
			Queue queue = new Queue(this._size);
			queue._size = this._size;
			int num = this._size;
			int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
			Array.Copy(this._array, this._head, queue._array, 0, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, queue._array, this._array.Length - this._head, num);
			}
			queue._version = this._version;
			return queue;
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Queue" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.Queue" /> is synchronized (thread safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x000D7649 File Offset: 0x000D5849
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Queue" />.</returns>
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060037D9 RID: 14297 RVA: 0x000D764C File Offset: 0x000D584C
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Queue" />.</summary>
		// Token: 0x060037DA RID: 14298 RVA: 0x000D7670 File Offset: 0x000D5870
		public virtual void Clear()
		{
			if (this._head < this._tail)
			{
				Array.Clear(this._array, this._head, this._size);
			}
			else
			{
				Array.Clear(this._array, this._head, this._array.Length - this._head);
				Array.Clear(this._array, 0, this._tail);
			}
			this._head = 0;
			this._tail = 0;
			this._size = 0;
			this._version++;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Queue" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Queue" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.Queue" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Collections.Queue" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060037DB RID: 14299 RVA: 0x000D76FC File Offset: 0x000D58FC
		public virtual void CopyTo(Array array, int index)
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
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			int length = array.Length;
			if (length - index < this._size)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int num = this._size;
			if (num == 0)
			{
				return;
			}
			int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
			Array.Copy(this._array, this._head, array, index, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, array, index + this._array.Length - this._head, num);
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <param name="obj">The object to add to the <see cref="T:System.Collections.Queue" />. The value can be <see langword="null" />.</param>
		// Token: 0x060037DC RID: 14300 RVA: 0x000D77D8 File Offset: 0x000D59D8
		public virtual void Enqueue(object obj)
		{
			if (this._size == this._array.Length)
			{
				int num = (int)((long)this._array.Length * (long)this._growFactor / 100L);
				if (num < this._array.Length + 4)
				{
					num = this._array.Length + 4;
				}
				this.SetCapacity(num);
			}
			this._array[this._tail] = obj;
			this._tail = (this._tail + 1) % this._array.Length;
			this._size++;
			this._version++;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Queue" />.</returns>
		// Token: 0x060037DD RID: 14301 RVA: 0x000D786C File Offset: 0x000D5A6C
		public virtual IEnumerator GetEnumerator()
		{
			return new Queue.QueueEnumerator(this);
		}

		/// <summary>Removes and returns the object at the beginning of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>The object that is removed from the beginning of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty.</exception>
		// Token: 0x060037DE RID: 14302 RVA: 0x000D7874 File Offset: 0x000D5A74
		public virtual object Dequeue()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyQueue"));
			}
			object obj = this._array[this._head];
			this._array[this._head] = null;
			this._head = (this._head + 1) % this._array.Length;
			this._size--;
			this._version++;
			return obj;
		}

		/// <summary>Returns the object at the beginning of the <see cref="T:System.Collections.Queue" /> without removing it.</summary>
		/// <returns>The object at the beginning of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty.</exception>
		// Token: 0x060037DF RID: 14303 RVA: 0x000D78E9 File Offset: 0x000D5AE9
		public virtual object Peek()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyQueue"));
			}
			return this._array[this._head];
		}

		/// <summary>Returns a new <see cref="T:System.Collections.Queue" /> that wraps the original queue, and is thread safe.</summary>
		/// <param name="queue">The <see cref="T:System.Collections.Queue" /> to synchronize.</param>
		/// <returns>A <see cref="T:System.Collections.Queue" /> wrapper that is synchronized (thread safe).</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="queue" /> is <see langword="null" />.</exception>
		// Token: 0x060037E0 RID: 14304 RVA: 0x000D7910 File Offset: 0x000D5B10
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Queue Synchronized(Queue queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			return new Queue.SynchronizedQueue(queue);
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Queue" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Queue" />. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is found in the <see cref="T:System.Collections.Queue" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060037E1 RID: 14305 RVA: 0x000D7928 File Offset: 0x000D5B28
		public virtual bool Contains(object obj)
		{
			int num = this._head;
			int size = this._size;
			while (size-- > 0)
			{
				if (obj == null)
				{
					if (this._array[num] == null)
					{
						return true;
					}
				}
				else if (this._array[num] != null && this._array[num].Equals(obj))
				{
					return true;
				}
				num = (num + 1) % this._array.Length;
			}
			return false;
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x000D7986 File Offset: 0x000D5B86
		internal object GetElement(int i)
		{
			return this._array[(this._head + i) % this._array.Length];
		}

		/// <summary>Copies the <see cref="T:System.Collections.Queue" /> elements to a new array.</summary>
		/// <returns>A new array containing elements copied from the <see cref="T:System.Collections.Queue" />.</returns>
		// Token: 0x060037E3 RID: 14307 RVA: 0x000D79A0 File Offset: 0x000D5BA0
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			if (this._size == 0)
			{
				return array;
			}
			if (this._head < this._tail)
			{
				Array.Copy(this._array, this._head, array, 0, this._size);
			}
			else
			{
				Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
				Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
			}
			return array;
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000D7A34 File Offset: 0x000D5C34
		private void SetCapacity(int capacity)
		{
			object[] array = new object[capacity];
			if (this._size > 0)
			{
				if (this._head < this._tail)
				{
					Array.Copy(this._array, this._head, array, 0, this._size);
				}
				else
				{
					Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
					Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
				}
			}
			this._array = array;
			this._head = 0;
			this._tail = ((this._size == capacity) ? 0 : this._size);
			this._version++;
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Queue" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Queue" /> is read-only.</exception>
		// Token: 0x060037E5 RID: 14309 RVA: 0x000D7AF2 File Offset: 0x000D5CF2
		public virtual void TrimToSize()
		{
			this.SetCapacity(this._size);
		}

		// Token: 0x040018C3 RID: 6339
		private object[] _array;

		// Token: 0x040018C4 RID: 6340
		private int _head;

		// Token: 0x040018C5 RID: 6341
		private int _tail;

		// Token: 0x040018C6 RID: 6342
		private int _size;

		// Token: 0x040018C7 RID: 6343
		private int _growFactor;

		// Token: 0x040018C8 RID: 6344
		private int _version;

		// Token: 0x040018C9 RID: 6345
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040018CA RID: 6346
		private const int _MinimumGrow = 4;

		// Token: 0x040018CB RID: 6347
		private const int _ShrinkThreshold = 32;

		// Token: 0x02000B9B RID: 2971
		[Serializable]
		private class SynchronizedQueue : Queue
		{
			// Token: 0x06006CC3 RID: 27843 RVA: 0x0017987B File Offset: 0x00177A7B
			internal SynchronizedQueue(Queue q)
			{
				this._q = q;
				this.root = this._q.SyncRoot;
			}

			// Token: 0x17001262 RID: 4706
			// (get) Token: 0x06006CC4 RID: 27844 RVA: 0x0017989B File Offset: 0x00177A9B
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001263 RID: 4707
			// (get) Token: 0x06006CC5 RID: 27845 RVA: 0x0017989E File Offset: 0x00177A9E
			public override object SyncRoot
			{
				get
				{
					return this.root;
				}
			}

			// Token: 0x17001264 RID: 4708
			// (get) Token: 0x06006CC6 RID: 27846 RVA: 0x001798A8 File Offset: 0x00177AA8
			public override int Count
			{
				get
				{
					object obj = this.root;
					int count;
					lock (obj)
					{
						count = this._q.Count;
					}
					return count;
				}
			}

			// Token: 0x06006CC7 RID: 27847 RVA: 0x001798F0 File Offset: 0x00177AF0
			public override void Clear()
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.Clear();
				}
			}

			// Token: 0x06006CC8 RID: 27848 RVA: 0x00179938 File Offset: 0x00177B38
			public override object Clone()
			{
				object obj = this.root;
				object obj2;
				lock (obj)
				{
					obj2 = new Queue.SynchronizedQueue((Queue)this._q.Clone());
				}
				return obj2;
			}

			// Token: 0x06006CC9 RID: 27849 RVA: 0x0017998C File Offset: 0x00177B8C
			public override bool Contains(object obj)
			{
				object obj2 = this.root;
				bool flag2;
				lock (obj2)
				{
					flag2 = this._q.Contains(obj);
				}
				return flag2;
			}

			// Token: 0x06006CCA RID: 27850 RVA: 0x001799D4 File Offset: 0x00177BD4
			public override void CopyTo(Array array, int arrayIndex)
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06006CCB RID: 27851 RVA: 0x00179A1C File Offset: 0x00177C1C
			public override void Enqueue(object value)
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.Enqueue(value);
				}
			}

			// Token: 0x06006CCC RID: 27852 RVA: 0x00179A64 File Offset: 0x00177C64
			public override object Dequeue()
			{
				object obj = this.root;
				object obj2;
				lock (obj)
				{
					obj2 = this._q.Dequeue();
				}
				return obj2;
			}

			// Token: 0x06006CCD RID: 27853 RVA: 0x00179AAC File Offset: 0x00177CAC
			public override IEnumerator GetEnumerator()
			{
				object obj = this.root;
				IEnumerator enumerator;
				lock (obj)
				{
					enumerator = this._q.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006CCE RID: 27854 RVA: 0x00179AF4 File Offset: 0x00177CF4
			public override object Peek()
			{
				object obj = this.root;
				object obj2;
				lock (obj)
				{
					obj2 = this._q.Peek();
				}
				return obj2;
			}

			// Token: 0x06006CCF RID: 27855 RVA: 0x00179B3C File Offset: 0x00177D3C
			public override object[] ToArray()
			{
				object obj = this.root;
				object[] array;
				lock (obj)
				{
					array = this._q.ToArray();
				}
				return array;
			}

			// Token: 0x06006CD0 RID: 27856 RVA: 0x00179B84 File Offset: 0x00177D84
			public override void TrimToSize()
			{
				object obj = this.root;
				lock (obj)
				{
					this._q.TrimToSize();
				}
			}

			// Token: 0x04003537 RID: 13623
			private Queue _q;

			// Token: 0x04003538 RID: 13624
			private object root;
		}

		// Token: 0x02000B9C RID: 2972
		[Serializable]
		private class QueueEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06006CD1 RID: 27857 RVA: 0x00179BCC File Offset: 0x00177DCC
			internal QueueEnumerator(Queue q)
			{
				this._q = q;
				this._version = this._q._version;
				this._index = 0;
				this.currentElement = this._q._array;
				if (this._q._size == 0)
				{
					this._index = -1;
				}
			}

			// Token: 0x06006CD2 RID: 27858 RVA: 0x00179C23 File Offset: 0x00177E23
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006CD3 RID: 27859 RVA: 0x00179C2C File Offset: 0x00177E2C
			public virtual bool MoveNext()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this._index < 0)
				{
					this.currentElement = this._q._array;
					return false;
				}
				this.currentElement = this._q.GetElement(this._index);
				this._index++;
				if (this._index == this._q._size)
				{
					this._index = -1;
				}
				return true;
			}

			// Token: 0x17001265 RID: 4709
			// (get) Token: 0x06006CD4 RID: 27860 RVA: 0x00179CB8 File Offset: 0x00177EB8
			public virtual object Current
			{
				get
				{
					if (this.currentElement != this._q._array)
					{
						return this.currentElement;
					}
					if (this._index == 0)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
				}
			}

			// Token: 0x06006CD5 RID: 27861 RVA: 0x00179D08 File Offset: 0x00177F08
			public virtual void Reset()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this._q._size == 0)
				{
					this._index = -1;
				}
				else
				{
					this._index = 0;
				}
				this.currentElement = this._q._array;
			}

			// Token: 0x04003539 RID: 13625
			private Queue _q;

			// Token: 0x0400353A RID: 13626
			private int _index;

			// Token: 0x0400353B RID: 13627
			private int _version;

			// Token: 0x0400353C RID: 13628
			private object currentElement;
		}

		// Token: 0x02000B9D RID: 2973
		internal class QueueDebugView
		{
			// Token: 0x06006CD6 RID: 27862 RVA: 0x00179D66 File Offset: 0x00177F66
			public QueueDebugView(Queue queue)
			{
				if (queue == null)
				{
					throw new ArgumentNullException("queue");
				}
				this.queue = queue;
			}

			// Token: 0x17001266 RID: 4710
			// (get) Token: 0x06006CD7 RID: 27863 RVA: 0x00179D83 File Offset: 0x00177F83
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this.queue.ToArray();
				}
			}

			// Token: 0x0400353D RID: 13629
			private Queue queue;
		}
	}
}
