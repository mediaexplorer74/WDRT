using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a simple last-in-first-out (LIFO) non-generic collection of objects.</summary>
	// Token: 0x02000490 RID: 1168
	[DebuggerTypeProxy(typeof(Stack.StackDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	[Serializable]
	public class Stack : ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x06003839 RID: 14393 RVA: 0x000D9019 File Offset: 0x000D7219
		public Stack()
		{
			this._array = new object[10];
			this._size = 0;
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that is empty and has the specified initial capacity or the default initial capacity, whichever is greater.</summary>
		/// <param name="initialCapacity">The initial number of elements that the <see cref="T:System.Collections.Stack" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCapacity" /> is less than zero.</exception>
		// Token: 0x0600383A RID: 14394 RVA: 0x000D903C File Offset: 0x000D723C
		public Stack(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (initialCapacity < 10)
			{
				initialCapacity = 10;
			}
			this._array = new object[initialCapacity];
			this._size = 0;
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that contains elements copied from the specified collection and has the same initial capacity as the number of elements copied.</summary>
		/// <param name="col">The <see cref="T:System.Collections.ICollection" /> to copy elements from.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is <see langword="null" />.</exception>
		// Token: 0x0600383B RID: 14395 RVA: 0x000D908C File Offset: 0x000D728C
		public Stack(ICollection col)
			: this((col == null) ? 32 : col.Count)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Push(obj);
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Stack" />.</returns>
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x0600383C RID: 14396 RVA: 0x000D90D7 File Offset: 0x000D72D7
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Stack" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" />, if access to the <see cref="T:System.Collections.Stack" /> is synchronized (thread safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x000D90DF File Offset: 0x000D72DF
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Collections.Stack" />.</returns>
		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x0600383E RID: 14398 RVA: 0x000D90E2 File Offset: 0x000D72E2
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

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Stack" />.</summary>
		// Token: 0x0600383F RID: 14399 RVA: 0x000D9104 File Offset: 0x000D7304
		public virtual void Clear()
		{
			Array.Clear(this._array, 0, this._size);
			this._size = 0;
			this._version++;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.Stack" />.</returns>
		// Token: 0x06003840 RID: 14400 RVA: 0x000D9130 File Offset: 0x000D7330
		public virtual object Clone()
		{
			Stack stack = new Stack(this._size);
			stack._size = this._size;
			Array.Copy(this._array, 0, stack._array, 0, this._size);
			stack._version = this._version;
			return stack;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Stack" />.</summary>
		/// <param name="obj">The object to locate in the <see cref="T:System.Collections.Stack" />. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" />, if <paramref name="obj" /> is found in the <see cref="T:System.Collections.Stack" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003841 RID: 14401 RVA: 0x000D917C File Offset: 0x000D737C
		public virtual bool Contains(object obj)
		{
			int size = this._size;
			while (size-- > 0)
			{
				if (obj == null)
				{
					if (this._array[size] == null)
					{
						return true;
					}
				}
				else if (this._array[size] != null && this._array[size].Equals(obj))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Stack" /> to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Stack" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.Stack" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Stack" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003842 RID: 14402 RVA: 0x000D91C8 File Offset: 0x000D73C8
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
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < this._size)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int i = 0;
			if (array is object[])
			{
				object[] array2 = (object[])array;
				while (i < this._size)
				{
					array2[i + index] = this._array[this._size - i - 1];
					i++;
				}
				return;
			}
			while (i < this._size)
			{
				array.SetValue(this._array[this._size - i - 1], i + index);
				i++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Stack" />.</returns>
		// Token: 0x06003843 RID: 14403 RVA: 0x000D9293 File Offset: 0x000D7493
		public virtual IEnumerator GetEnumerator()
		{
			return new Stack.StackEnumerator(this);
		}

		/// <summary>Returns the object at the top of the <see cref="T:System.Collections.Stack" /> without removing it.</summary>
		/// <returns>The <see cref="T:System.Object" /> at the top of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Stack" /> is empty.</exception>
		// Token: 0x06003844 RID: 14404 RVA: 0x000D929B File Offset: 0x000D749B
		public virtual object Peek()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
			}
			return this._array[this._size - 1];
		}

		/// <summary>Removes and returns the object at the top of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> removed from the top of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Stack" /> is empty.</exception>
		// Token: 0x06003845 RID: 14405 RVA: 0x000D92C4 File Offset: 0x000D74C4
		public virtual object Pop()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
			}
			this._version++;
			object[] array = this._array;
			int num = this._size - 1;
			this._size = num;
			object obj = array[num];
			this._array[this._size] = null;
			return obj;
		}

		/// <summary>Inserts an object at the top of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to push onto the <see cref="T:System.Collections.Stack" />. The value can be <see langword="null" />.</param>
		// Token: 0x06003846 RID: 14406 RVA: 0x000D9320 File Offset: 0x000D7520
		public virtual void Push(object obj)
		{
			if (this._size == this._array.Length)
			{
				object[] array = new object[2 * this._array.Length];
				Array.Copy(this._array, 0, array, 0, this._size);
				this._array = array;
			}
			object[] array2 = this._array;
			int size = this._size;
			this._size = size + 1;
			array2[size] = obj;
			this._version++;
		}

		/// <summary>Returns a synchronized (thread safe) wrapper for the <see cref="T:System.Collections.Stack" />.</summary>
		/// <param name="stack">The <see cref="T:System.Collections.Stack" /> to synchronize.</param>
		/// <returns>A synchronized wrapper around the <see cref="T:System.Collections.Stack" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stack" /> is <see langword="null" />.</exception>
		// Token: 0x06003847 RID: 14407 RVA: 0x000D938F File Offset: 0x000D758F
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Stack Synchronized(Stack stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}
			return new Stack.SyncStack(stack);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Stack" /> to a new array.</summary>
		/// <returns>A new array containing copies of the elements of the <see cref="T:System.Collections.Stack" />.</returns>
		// Token: 0x06003848 RID: 14408 RVA: 0x000D93A8 File Offset: 0x000D75A8
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			for (int i = 0; i < this._size; i++)
			{
				array[i] = this._array[this._size - i - 1];
			}
			return array;
		}

		// Token: 0x040018DA RID: 6362
		private object[] _array;

		// Token: 0x040018DB RID: 6363
		private int _size;

		// Token: 0x040018DC RID: 6364
		private int _version;

		// Token: 0x040018DD RID: 6365
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040018DE RID: 6366
		private const int _defaultCapacity = 10;

		// Token: 0x02000BAA RID: 2986
		[Serializable]
		private class SyncStack : Stack
		{
			// Token: 0x06006DE1 RID: 28129 RVA: 0x0017CC74 File Offset: 0x0017AE74
			internal SyncStack(Stack stack)
			{
				this._s = stack;
				this._root = stack.SyncRoot;
			}

			// Token: 0x170012A0 RID: 4768
			// (get) Token: 0x06006DE2 RID: 28130 RVA: 0x0017CC8F File Offset: 0x0017AE8F
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012A1 RID: 4769
			// (get) Token: 0x06006DE3 RID: 28131 RVA: 0x0017CC92 File Offset: 0x0017AE92
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170012A2 RID: 4770
			// (get) Token: 0x06006DE4 RID: 28132 RVA: 0x0017CC9C File Offset: 0x0017AE9C
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._s.Count;
					}
					return count;
				}
			}

			// Token: 0x06006DE5 RID: 28133 RVA: 0x0017CCE4 File Offset: 0x0017AEE4
			public override bool Contains(object obj)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._s.Contains(obj);
				}
				return flag2;
			}

			// Token: 0x06006DE6 RID: 28134 RVA: 0x0017CD2C File Offset: 0x0017AF2C
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = new Stack.SyncStack((Stack)this._s.Clone());
				}
				return obj;
			}

			// Token: 0x06006DE7 RID: 28135 RVA: 0x0017CD80 File Offset: 0x0017AF80
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._s.Clear();
				}
			}

			// Token: 0x06006DE8 RID: 28136 RVA: 0x0017CDC8 File Offset: 0x0017AFC8
			public override void CopyTo(Array array, int arrayIndex)
			{
				object root = this._root;
				lock (root)
				{
					this._s.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06006DE9 RID: 28137 RVA: 0x0017CE10 File Offset: 0x0017B010
			public override void Push(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._s.Push(value);
				}
			}

			// Token: 0x06006DEA RID: 28138 RVA: 0x0017CE58 File Offset: 0x0017B058
			public override object Pop()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._s.Pop();
				}
				return obj;
			}

			// Token: 0x06006DEB RID: 28139 RVA: 0x0017CEA0 File Offset: 0x0017B0A0
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._s.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006DEC RID: 28140 RVA: 0x0017CEE8 File Offset: 0x0017B0E8
			public override object Peek()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._s.Peek();
				}
				return obj;
			}

			// Token: 0x06006DED RID: 28141 RVA: 0x0017CF30 File Offset: 0x0017B130
			public override object[] ToArray()
			{
				object root = this._root;
				object[] array;
				lock (root)
				{
					array = this._s.ToArray();
				}
				return array;
			}

			// Token: 0x0400355C RID: 13660
			private Stack _s;

			// Token: 0x0400355D RID: 13661
			private object _root;
		}

		// Token: 0x02000BAB RID: 2987
		[Serializable]
		private class StackEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06006DEE RID: 28142 RVA: 0x0017CF78 File Offset: 0x0017B178
			internal StackEnumerator(Stack stack)
			{
				this._stack = stack;
				this._version = this._stack._version;
				this._index = -2;
				this.currentElement = null;
			}

			// Token: 0x06006DEF RID: 28143 RVA: 0x0017CFA7 File Offset: 0x0017B1A7
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006DF0 RID: 28144 RVA: 0x0017CFB0 File Offset: 0x0017B1B0
			public virtual bool MoveNext()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				bool flag;
				if (this._index == -2)
				{
					this._index = this._stack._size - 1;
					flag = this._index >= 0;
					if (flag)
					{
						this.currentElement = this._stack._array[this._index];
					}
					return flag;
				}
				if (this._index == -1)
				{
					return false;
				}
				int num = this._index - 1;
				this._index = num;
				flag = num >= 0;
				if (flag)
				{
					this.currentElement = this._stack._array[this._index];
				}
				else
				{
					this.currentElement = null;
				}
				return flag;
			}

			// Token: 0x170012A3 RID: 4771
			// (get) Token: 0x06006DF1 RID: 28145 RVA: 0x0017D06F File Offset: 0x0017B26F
			public virtual object Current
			{
				get
				{
					if (this._index == -2)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._index == -1)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this.currentElement;
				}
			}

			// Token: 0x06006DF2 RID: 28146 RVA: 0x0017D0AA File Offset: 0x0017B2AA
			public virtual void Reset()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this._index = -2;
				this.currentElement = null;
			}

			// Token: 0x0400355E RID: 13662
			private Stack _stack;

			// Token: 0x0400355F RID: 13663
			private int _index;

			// Token: 0x04003560 RID: 13664
			private int _version;

			// Token: 0x04003561 RID: 13665
			private object currentElement;
		}

		// Token: 0x02000BAC RID: 2988
		internal class StackDebugView
		{
			// Token: 0x06006DF3 RID: 28147 RVA: 0x0017D0DE File Offset: 0x0017B2DE
			public StackDebugView(Stack stack)
			{
				if (stack == null)
				{
					throw new ArgumentNullException("stack");
				}
				this.stack = stack;
			}

			// Token: 0x170012A4 RID: 4772
			// (get) Token: 0x06006DF4 RID: 28148 RVA: 0x0017D0FB File Offset: 0x0017B2FB
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this.stack.ToArray();
				}
			}

			// Token: 0x04003562 RID: 13666
			private Stack stack;
		}
	}
}
