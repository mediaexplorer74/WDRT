using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of objects that is maintained in sorted order.</summary>
	/// <typeparam name="T">The type of elements in the set.</typeparam>
	// Token: 0x020003CB RID: 971
	[DebuggerTypeProxy(typeof(SortedSetDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[global::__DynamicallyInvokable]
	[Serializable]
	public class SortedSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICollection, ISerializable, IDeserializationCallback, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class.</summary>
		// Token: 0x060024D3 RID: 9427 RVA: 0x000AB5AB File Offset: 0x000A97AB
		[global::__DynamicallyInvokable]
		public SortedSet()
		{
			this.comparer = Comparer<T>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that uses a specified comparer.</summary>
		/// <param name="comparer">The default comparer to use for comparing objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is <see langword="null" />.</exception>
		// Token: 0x060024D4 RID: 9428 RVA: 0x000AB5BE File Offset: 0x000A97BE
		[global::__DynamicallyInvokable]
		public SortedSet(IComparer<T> comparer)
		{
			if (comparer == null)
			{
				this.comparer = Comparer<T>.Default;
				return;
			}
			this.comparer = comparer;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains elements copied from a specified enumerable collection.</summary>
		/// <param name="collection">The enumerable collection to be copied.</param>
		// Token: 0x060024D5 RID: 9429 RVA: 0x000AB5DC File Offset: 0x000A97DC
		[global::__DynamicallyInvokable]
		public SortedSet(IEnumerable<T> collection)
			: this(collection, Comparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains elements copied from a specified enumerable collection and that uses a specified comparer.</summary>
		/// <param name="collection">The enumerable collection to be copied.</param>
		/// <param name="comparer">The default comparer to use for comparing objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is <see langword="null" />.</exception>
		// Token: 0x060024D6 RID: 9430 RVA: 0x000AB5EC File Offset: 0x000A97EC
		[global::__DynamicallyInvokable]
		public SortedSet(IEnumerable<T> collection, IComparer<T> comparer)
			: this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			SortedSet<T> sortedSet = collection as SortedSet<T>;
			SortedSet<T> sortedSet2 = collection as SortedSet<T>.TreeSubSet;
			if (sortedSet == null || sortedSet2 != null || !SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				List<T> list = new List<T>(collection);
				list.Sort(this.comparer);
				for (int i = 1; i < list.Count; i++)
				{
					if (comparer.Compare(list[i], list[i - 1]) == 0)
					{
						list.RemoveAt(i);
						i--;
					}
				}
				this.root = SortedSet<T>.ConstructRootFromSortedArray(list.ToArray(), 0, list.Count - 1, null);
				this.count = list.Count;
				this.version = 0;
				return;
			}
			if (sortedSet.Count == 0)
			{
				this.count = 0;
				this.version = 0;
				this.root = null;
				return;
			}
			Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(sortedSet.Count) + 2);
			Stack<SortedSet<T>.Node> stack2 = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(sortedSet.Count) + 2);
			SortedSet<T>.Node node = sortedSet.root;
			SortedSet<T>.Node node2 = ((node != null) ? new SortedSet<T>.Node(node.Item, node.IsRed) : null);
			this.root = node2;
			while (node != null)
			{
				stack.Push(node);
				stack2.Push(node2);
				node2.Left = ((node.Left != null) ? new SortedSet<T>.Node(node.Left.Item, node.Left.IsRed) : null);
				node = node.Left;
				node2 = node2.Left;
			}
			while (stack.Count != 0)
			{
				node = stack.Pop();
				node2 = stack2.Pop();
				SortedSet<T>.Node node3 = node.Right;
				SortedSet<T>.Node node4 = null;
				if (node3 != null)
				{
					node4 = new SortedSet<T>.Node(node3.Item, node3.IsRed);
				}
				node2.Right = node4;
				while (node3 != null)
				{
					stack.Push(node3);
					stack2.Push(node4);
					node4.Left = ((node3.Left != null) ? new SortedSet<T>.Node(node3.Left.Item, node3.Left.IsRed) : null);
					node3 = node3.Left;
					node4 = node4.Left;
				}
			}
			this.count = sortedSet.count;
			this.version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains serialized data.</summary>
		/// <param name="info">The object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <param name="context">The structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		// Token: 0x060024D7 RID: 9431 RVA: 0x000AB841 File Offset: 0x000A9A41
		protected SortedSet(SerializationInfo info, StreamingContext context)
		{
			this.siInfo = info;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000AB850 File Offset: 0x000A9A50
		private void AddAllElements(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				if (!this.Contains(t))
				{
					this.Add(t);
				}
			}
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000AB8A4 File Offset: 0x000A9AA4
		private void RemoveAllElements(IEnumerable<T> collection)
		{
			T min = this.Min;
			T max = this.Max;
			foreach (T t in collection)
			{
				if (this.comparer.Compare(t, min) >= 0 && this.comparer.Compare(t, max) <= 0 && this.Contains(t))
				{
					this.Remove(t);
				}
			}
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000AB924 File Offset: 0x000A9B24
		private bool ContainsAllElements(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				if (!this.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000AB978 File Offset: 0x000A9B78
		internal bool InOrderTreeWalk(TreeWalkPredicate<T> action)
		{
			return this.InOrderTreeWalk(action, false);
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000AB984 File Offset: 0x000A9B84
		internal virtual bool InOrderTreeWalk(TreeWalkPredicate<T> action, bool reverse)
		{
			if (this.root == null)
			{
				return true;
			}
			Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(this.Count + 1));
			for (SortedSet<T>.Node node = this.root; node != null; node = (reverse ? node.Right : node.Left))
			{
				stack.Push(node);
			}
			while (stack.Count != 0)
			{
				SortedSet<T>.Node node = stack.Pop();
				if (!action(node))
				{
					return false;
				}
				for (SortedSet<T>.Node node2 = (reverse ? node.Left : node.Right); node2 != null; node2 = (reverse ? node2.Right : node2.Left))
				{
					stack.Push(node2);
				}
			}
			return true;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000ABA24 File Offset: 0x000A9C24
		internal virtual bool BreadthFirstTreeWalk(TreeWalkPredicate<T> action)
		{
			if (this.root == null)
			{
				return true;
			}
			List<SortedSet<T>.Node> list = new List<SortedSet<T>.Node>();
			list.Add(this.root);
			while (list.Count != 0)
			{
				SortedSet<T>.Node node = list[0];
				list.RemoveAt(0);
				if (!action(node))
				{
					return false;
				}
				if (node.Left != null)
				{
					list.Add(node.Left);
				}
				if (node.Right != null)
				{
					list.Add(node.Right);
				}
			}
			return true;
		}

		/// <summary>Gets the number of elements in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</returns>
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x000ABA9A File Offset: 0x000A9C9A
		[global::__DynamicallyInvokable]
		public int Count
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.VersionCheck();
				return this.count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IComparer`1" /> object that is used to order the values in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The comparer that is used to order the values in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</returns>
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x000ABAA8 File Offset: 0x000A9CA8
		[global::__DynamicallyInvokable]
		public IComparer<T> Comparer
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x000ABAB0 File Offset: 0x000A9CB0
		[global::__DynamicallyInvokable]
		bool ICollection<T>.IsReadOnly
		{
			[global::__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x000ABAB3 File Offset: 0x000A9CB3
		[global::__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[global::__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x000ABAB6 File Offset: 0x000A9CB6
		[global::__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000ABAD8 File Offset: 0x000A9CD8
		internal virtual void VersionCheck()
		{
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000ABADA File Offset: 0x000A9CDA
		internal virtual bool IsWithinRange(T item)
		{
			return true;
		}

		/// <summary>Adds an element to the set and returns a value that indicates if it was successfully added.</summary>
		/// <param name="item">The element to add to the set.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> is added to the set; otherwise, <see langword="false" />.</returns>
		// Token: 0x060024E5 RID: 9445 RVA: 0x000ABADD File Offset: 0x000A9CDD
		[global::__DynamicallyInvokable]
		public bool Add(T item)
		{
			return this.AddIfNotPresent(item);
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000ABAE6 File Offset: 0x000A9CE6
		[global::__DynamicallyInvokable]
		void ICollection<T>.Add(T item)
		{
			this.AddIfNotPresent(item);
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000ABAF0 File Offset: 0x000A9CF0
		internal virtual bool AddIfNotPresent(T item)
		{
			if (this.root == null)
			{
				this.root = new SortedSet<T>.Node(item, false);
				this.count = 1;
				this.version++;
				return true;
			}
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node node4 = null;
			this.version++;
			int num = 0;
			while (node != null)
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					this.root.IsRed = false;
					return false;
				}
				if (SortedSet<T>.Is4Node(node))
				{
					SortedSet<T>.Split4Node(node);
					if (SortedSet<T>.IsRed(node2))
					{
						this.InsertionBalance(node, ref node2, node3, node4);
					}
				}
				node4 = node3;
				node3 = node2;
				node2 = node;
				node = ((num < 0) ? node.Left : node.Right);
			}
			SortedSet<T>.Node node5 = new SortedSet<T>.Node(item);
			if (num > 0)
			{
				node2.Right = node5;
			}
			else
			{
				node2.Left = node5;
			}
			if (node2.IsRed)
			{
				this.InsertionBalance(node5, ref node2, node3, node4);
			}
			this.root.IsRed = false;
			this.count++;
			return true;
		}

		/// <summary>Removes a specified item from the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <param name="item">The element to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the element is found and successfully removed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060024E8 RID: 9448 RVA: 0x000ABBFB File Offset: 0x000A9DFB
		[global::__DynamicallyInvokable]
		public bool Remove(T item)
		{
			return this.DoRemove(item);
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000ABC04 File Offset: 0x000A9E04
		internal virtual bool DoRemove(T item)
		{
			if (this.root == null)
			{
				return false;
			}
			this.version++;
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node node4 = null;
			SortedSet<T>.Node node5 = null;
			bool flag = false;
			while (node != null)
			{
				if (SortedSet<T>.Is2Node(node))
				{
					if (node2 == null)
					{
						node.IsRed = true;
					}
					else
					{
						SortedSet<T>.Node node6 = SortedSet<T>.GetSibling(node, node2);
						if (node6.IsRed)
						{
							if (node2.Right == node6)
							{
								SortedSet<T>.RotateLeft(node2);
							}
							else
							{
								SortedSet<T>.RotateRight(node2);
							}
							node2.IsRed = true;
							node6.IsRed = false;
							this.ReplaceChildOfNodeOrRoot(node3, node2, node6);
							node3 = node6;
							if (node2 == node4)
							{
								node5 = node6;
							}
							node6 = ((node2.Left == node) ? node2.Right : node2.Left);
						}
						if (SortedSet<T>.Is2Node(node6))
						{
							SortedSet<T>.Merge2Nodes(node2, node, node6);
						}
						else
						{
							TreeRotation treeRotation = SortedSet<T>.RotationNeeded(node2, node, node6);
							SortedSet<T>.Node node7 = null;
							switch (treeRotation)
							{
							case TreeRotation.LeftRotation:
								node6.Right.IsRed = false;
								node7 = SortedSet<T>.RotateLeft(node2);
								break;
							case TreeRotation.RightRotation:
								node6.Left.IsRed = false;
								node7 = SortedSet<T>.RotateRight(node2);
								break;
							case TreeRotation.RightLeftRotation:
								node7 = SortedSet<T>.RotateRightLeft(node2);
								break;
							case TreeRotation.LeftRightRotation:
								node7 = SortedSet<T>.RotateLeftRight(node2);
								break;
							}
							node7.IsRed = node2.IsRed;
							node2.IsRed = false;
							node.IsRed = true;
							this.ReplaceChildOfNodeOrRoot(node3, node2, node7);
							if (node2 == node4)
							{
								node5 = node7;
							}
						}
					}
				}
				int num = (flag ? (-1) : this.comparer.Compare(item, node.Item));
				if (num == 0)
				{
					flag = true;
					node4 = node;
					node5 = node2;
				}
				node3 = node2;
				node2 = node;
				if (num < 0)
				{
					node = node.Left;
				}
				else
				{
					node = node.Right;
				}
			}
			if (node4 != null)
			{
				this.ReplaceNode(node4, node5, node2, node3);
				this.count--;
			}
			if (this.root != null)
			{
				this.root.IsRed = false;
			}
			return flag;
		}

		/// <summary>Removes all elements from the set.</summary>
		// Token: 0x060024EA RID: 9450 RVA: 0x000ABDEC File Offset: 0x000A9FEC
		[global::__DynamicallyInvokable]
		public virtual void Clear()
		{
			this.root = null;
			this.count = 0;
			this.version++;
		}

		/// <summary>Determines whether the set contains a specific element.</summary>
		/// <param name="item">The element to locate in the set.</param>
		/// <returns>
		///   <see langword="true" /> if the set contains <paramref name="item" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060024EB RID: 9451 RVA: 0x000ABE0A File Offset: 0x000AA00A
		[global::__DynamicallyInvokable]
		public virtual bool Contains(T item)
		{
			return this.FindNode(item) != null;
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the beginning of the target array.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedSet`1" /> exceeds the number of elements that the destination array can contain.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		// Token: 0x060024EC RID: 9452 RVA: 0x000ABE16 File Offset: 0x000AA016
		[global::__DynamicallyInvokable]
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0, this.Count);
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x060024ED RID: 9453 RVA: 0x000ABE26 File Offset: 0x000AA026
		[global::__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			this.CopyTo(array, index, this.Count);
		}

		/// <summary>Copies a specified number of elements from <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <param name="count">The number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		// Token: 0x060024EE RID: 9454 RVA: 0x000ABE38 File Offset: 0x000AA038
		[global::__DynamicallyInvokable]
		public void CopyTo(T[] array, int index, int count)
		{
			if (array == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.array);
			}
			if (index < 0)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.index);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (index > array.Length || count > array.Length - index)
			{
				throw new ArgumentException(SR.GetString("Arg_ArrayPlusOffTooSmall"));
			}
			count += index;
			this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
			{
				if (index >= count)
				{
					return false;
				}
				T[] array2 = array;
				int index2 = index;
				index = index2 + 1;
				array2[index2] = node.Item;
				return true;
			});
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x060024EF RID: 9455 RVA: 0x000ABEFC File Offset: 0x000AA0FC
		[global::__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index < 0)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.arrayIndex, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			object[] objects = array as object[];
			if (objects == null)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
			}
			try
			{
				this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
				{
					object[] objects2 = objects;
					int index2 = index;
					index = index2 + 1;
					objects2[index2] = node.Item;
					return true;
				});
			}
			catch (ArrayTypeMismatchException)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>An enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedSet`1" /> in sorted order.</returns>
		// Token: 0x060024F0 RID: 9456 RVA: 0x000ABFD0 File Offset: 0x000AA1D0
		[global::__DynamicallyInvokable]
		public SortedSet<T>.Enumerator GetEnumerator()
		{
			return new SortedSet<T>.Enumerator(this);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000ABFD8 File Offset: 0x000AA1D8
		[global::__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new SortedSet<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x060024F2 RID: 9458 RVA: 0x000ABFE5 File Offset: 0x000AA1E5
		[global::__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedSet<T>.Enumerator(this);
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000ABFF2 File Offset: 0x000AA1F2
		private static SortedSet<T>.Node GetSibling(SortedSet<T>.Node node, SortedSet<T>.Node parent)
		{
			if (parent.Left == node)
			{
				return parent.Right;
			}
			return parent.Left;
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000AC00C File Offset: 0x000AA20C
		private void InsertionBalance(SortedSet<T>.Node current, ref SortedSet<T>.Node parent, SortedSet<T>.Node grandParent, SortedSet<T>.Node greatGrandParent)
		{
			bool flag = grandParent.Right == parent;
			bool flag2 = parent.Right == current;
			SortedSet<T>.Node node;
			if (flag == flag2)
			{
				node = (flag2 ? SortedSet<T>.RotateLeft(grandParent) : SortedSet<T>.RotateRight(grandParent));
			}
			else
			{
				node = (flag2 ? SortedSet<T>.RotateLeftRight(grandParent) : SortedSet<T>.RotateRightLeft(grandParent));
				parent = greatGrandParent;
			}
			grandParent.IsRed = true;
			node.IsRed = false;
			this.ReplaceChildOfNodeOrRoot(greatGrandParent, grandParent, node);
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000AC075 File Offset: 0x000AA275
		private static bool Is2Node(SortedSet<T>.Node node)
		{
			return SortedSet<T>.IsBlack(node) && SortedSet<T>.IsNullOrBlack(node.Left) && SortedSet<T>.IsNullOrBlack(node.Right);
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000AC099 File Offset: 0x000AA299
		private static bool Is4Node(SortedSet<T>.Node node)
		{
			return SortedSet<T>.IsRed(node.Left) && SortedSet<T>.IsRed(node.Right);
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000AC0B5 File Offset: 0x000AA2B5
		private static bool IsBlack(SortedSet<T>.Node node)
		{
			return node != null && !node.IsRed;
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000AC0C5 File Offset: 0x000AA2C5
		private static bool IsNullOrBlack(SortedSet<T>.Node node)
		{
			return node == null || !node.IsRed;
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000AC0D5 File Offset: 0x000AA2D5
		private static bool IsRed(SortedSet<T>.Node node)
		{
			return node != null && node.IsRed;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000AC0E2 File Offset: 0x000AA2E2
		private static void Merge2Nodes(SortedSet<T>.Node parent, SortedSet<T>.Node child1, SortedSet<T>.Node child2)
		{
			parent.IsRed = false;
			child1.IsRed = true;
			child2.IsRed = true;
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000AC0F9 File Offset: 0x000AA2F9
		private void ReplaceChildOfNodeOrRoot(SortedSet<T>.Node parent, SortedSet<T>.Node child, SortedSet<T>.Node newChild)
		{
			if (parent == null)
			{
				this.root = newChild;
				return;
			}
			if (parent.Left == child)
			{
				parent.Left = newChild;
				return;
			}
			parent.Right = newChild;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000AC120 File Offset: 0x000AA320
		private void ReplaceNode(SortedSet<T>.Node match, SortedSet<T>.Node parentOfMatch, SortedSet<T>.Node succesor, SortedSet<T>.Node parentOfSuccesor)
		{
			if (succesor == match)
			{
				succesor = match.Left;
			}
			else
			{
				if (succesor.Right != null)
				{
					succesor.Right.IsRed = false;
				}
				if (parentOfSuccesor != match)
				{
					parentOfSuccesor.Left = succesor.Right;
					succesor.Right = match.Right;
				}
				succesor.Left = match.Left;
			}
			if (succesor != null)
			{
				succesor.IsRed = match.IsRed;
			}
			this.ReplaceChildOfNodeOrRoot(parentOfMatch, match, succesor);
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000AC194 File Offset: 0x000AA394
		internal virtual SortedSet<T>.Node FindNode(T item)
		{
			int num;
			for (SortedSet<T>.Node node = this.root; node != null; node = ((num < 0) ? node.Left : node.Right))
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					return node;
				}
			}
			return null;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000AC1DC File Offset: 0x000AA3DC
		internal virtual int InternalIndexOf(T item)
		{
			SortedSet<T>.Node node = this.root;
			int num = 0;
			while (node != null)
			{
				int num2 = this.comparer.Compare(item, node.Item);
				if (num2 == 0)
				{
					return num;
				}
				node = ((num2 < 0) ? node.Left : node.Right);
				num = ((num2 < 0) ? (2 * num + 1) : (2 * num + 2));
			}
			return -1;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000AC234 File Offset: 0x000AA434
		internal SortedSet<T>.Node FindRange(T from, T to)
		{
			return this.FindRange(from, to, true, true);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000AC240 File Offset: 0x000AA440
		internal SortedSet<T>.Node FindRange(T from, T to, bool lowerBoundActive, bool upperBoundActive)
		{
			SortedSet<T>.Node node = this.root;
			while (node != null)
			{
				if (lowerBoundActive && this.comparer.Compare(from, node.Item) > 0)
				{
					node = node.Right;
				}
				else
				{
					if (!upperBoundActive || this.comparer.Compare(to, node.Item) >= 0)
					{
						return node;
					}
					node = node.Left;
				}
			}
			return null;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000AC29F File Offset: 0x000AA49F
		internal void UpdateVersion()
		{
			this.version++;
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000AC2B0 File Offset: 0x000AA4B0
		private static SortedSet<T>.Node RotateLeft(SortedSet<T>.Node node)
		{
			SortedSet<T>.Node right = node.Right;
			node.Right = right.Left;
			right.Left = node;
			return right;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000AC2D8 File Offset: 0x000AA4D8
		private static SortedSet<T>.Node RotateLeftRight(SortedSet<T>.Node node)
		{
			SortedSet<T>.Node left = node.Left;
			SortedSet<T>.Node right = left.Right;
			node.Left = right.Right;
			right.Right = node;
			left.Right = right.Left;
			right.Left = left;
			return right;
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000AC31C File Offset: 0x000AA51C
		private static SortedSet<T>.Node RotateRight(SortedSet<T>.Node node)
		{
			SortedSet<T>.Node left = node.Left;
			node.Left = left.Right;
			left.Right = node;
			return left;
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000AC344 File Offset: 0x000AA544
		private static SortedSet<T>.Node RotateRightLeft(SortedSet<T>.Node node)
		{
			SortedSet<T>.Node right = node.Right;
			SortedSet<T>.Node left = right.Left;
			node.Right = left.Left;
			left.Left = node;
			right.Left = left.Right;
			left.Right = right;
			return left;
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000AC386 File Offset: 0x000AA586
		private static TreeRotation RotationNeeded(SortedSet<T>.Node parent, SortedSet<T>.Node current, SortedSet<T>.Node sibling)
		{
			if (SortedSet<T>.IsRed(sibling.Left))
			{
				if (parent.Left == current)
				{
					return TreeRotation.RightLeftRotation;
				}
				return TreeRotation.RightRotation;
			}
			else
			{
				if (parent.Left == current)
				{
					return TreeRotation.LeftRotation;
				}
				return TreeRotation.LeftRightRotation;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEqualityComparer" /> object that can be used to create a collection that contains individual sets.</summary>
		/// <returns>A comparer for creating a collection of sets.</returns>
		// Token: 0x06002507 RID: 9479 RVA: 0x000AC3AE File Offset: 0x000AA5AE
		public static IEqualityComparer<SortedSet<T>> CreateSetComparer()
		{
			return new SortedSetEqualityComparer<T>();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEqualityComparer" /> object, according to a specified comparer, that can be used to create a collection that contains individual sets.</summary>
		/// <param name="memberEqualityComparer">The comparer to use for creating the returned comparer.</param>
		/// <returns>A comparer for creating a collection of sets.</returns>
		// Token: 0x06002508 RID: 9480 RVA: 0x000AC3B5 File Offset: 0x000AA5B5
		public static IEqualityComparer<SortedSet<T>> CreateSetComparer(IEqualityComparer<T> memberEqualityComparer)
		{
			return new SortedSetEqualityComparer<T>(memberEqualityComparer);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000AC3C0 File Offset: 0x000AA5C0
		internal static bool SortedSetEquals(SortedSet<T> set1, SortedSet<T> set2, IComparer<T> comparer)
		{
			if (set1 == null)
			{
				return set2 == null;
			}
			if (set2 == null)
			{
				return false;
			}
			if (SortedSet<T>.AreComparersEqual(set1, set2))
			{
				return set1.Count == set2.Count && set1.SetEquals(set2);
			}
			bool flag = false;
			foreach (T t in set1)
			{
				flag = false;
				foreach (T t2 in set2)
				{
					if (comparer.Compare(t, t2) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000AC48C File Offset: 0x000AA68C
		private static bool AreComparersEqual(SortedSet<T> set1, SortedSet<T> set2)
		{
			return set1.Comparer.Equals(set2.Comparer);
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000AC49F File Offset: 0x000AA69F
		private static void Split4Node(SortedSet<T>.Node node)
		{
			node.IsRed = true;
			node.Left.IsRed = false;
			node.Right.IsRed = false;
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000AC4C0 File Offset: 0x000AA6C0
		internal T[] ToArray()
		{
			T[] array = new T[this.Count];
			this.CopyTo(array);
			return array;
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains all elements that are present in either the current object or the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x0600250D RID: 9485 RVA: 0x000AC4E4 File Offset: 0x000AA6E4
		[global::__DynamicallyInvokable]
		public void UnionWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			SortedSet<T>.TreeSubSet treeSubSet = this as SortedSet<T>.TreeSubSet;
			if (treeSubSet != null)
			{
				this.VersionCheck();
			}
			if (sortedSet != null && treeSubSet == null && this.count == 0)
			{
				SortedSet<T> sortedSet2 = new SortedSet<T>(sortedSet, this.comparer);
				this.root = sortedSet2.root;
				this.count = sortedSet2.count;
				this.version++;
				return;
			}
			if (sortedSet != null && treeSubSet == null && SortedSet<T>.AreComparersEqual(this, sortedSet) && sortedSet.Count > this.Count / 2)
			{
				T[] array = new T[sortedSet.Count + this.Count];
				int num = 0;
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				while (!flag && !flag2)
				{
					int num2 = this.Comparer.Compare(enumerator.Current, enumerator2.Current);
					if (num2 < 0)
					{
						array[num++] = enumerator.Current;
						flag = !enumerator.MoveNext();
					}
					else if (num2 == 0)
					{
						array[num++] = enumerator2.Current;
						flag = !enumerator.MoveNext();
						flag2 = !enumerator2.MoveNext();
					}
					else
					{
						array[num++] = enumerator2.Current;
						flag2 = !enumerator2.MoveNext();
					}
				}
				if (!flag || !flag2)
				{
					SortedSet<T>.Enumerator enumerator3 = (flag ? enumerator2 : enumerator);
					do
					{
						array[num++] = enumerator3.Current;
					}
					while (enumerator3.MoveNext());
				}
				this.root = null;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
				this.version++;
				return;
			}
			this.AddAllElements(other);
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000AC6D0 File Offset: 0x000AA8D0
		private static SortedSet<T>.Node ConstructRootFromSortedArray(T[] arr, int startIndex, int endIndex, SortedSet<T>.Node redNode)
		{
			int num = endIndex - startIndex + 1;
			if (num == 0)
			{
				return null;
			}
			SortedSet<T>.Node node;
			if (num == 1)
			{
				node = new SortedSet<T>.Node(arr[startIndex], false);
				if (redNode != null)
				{
					node.Left = redNode;
				}
			}
			else if (num == 2)
			{
				node = new SortedSet<T>.Node(arr[startIndex], false);
				node.Right = new SortedSet<T>.Node(arr[endIndex], false);
				node.Right.IsRed = true;
				if (redNode != null)
				{
					node.Left = redNode;
				}
			}
			else if (num == 3)
			{
				node = new SortedSet<T>.Node(arr[startIndex + 1], false);
				node.Left = new SortedSet<T>.Node(arr[startIndex], false);
				node.Right = new SortedSet<T>.Node(arr[endIndex], false);
				if (redNode != null)
				{
					node.Left.Left = redNode;
				}
			}
			else
			{
				int num2 = (startIndex + endIndex) / 2;
				node = new SortedSet<T>.Node(arr[num2], false);
				node.Left = SortedSet<T>.ConstructRootFromSortedArray(arr, startIndex, num2 - 1, redNode);
				if (num % 2 == 0)
				{
					node.Right = SortedSet<T>.ConstructRootFromSortedArray(arr, num2 + 2, endIndex, new SortedSet<T>.Node(arr[num2 + 1], true));
				}
				else
				{
					node.Right = SortedSet<T>.ConstructRootFromSortedArray(arr, num2 + 1, endIndex, null);
				}
			}
			return node;
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains only elements that are also in a specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x0600250F RID: 9487 RVA: 0x000AC7FC File Offset: 0x000AA9FC
		[global::__DynamicallyInvokable]
		public virtual void IntersectWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			SortedSet<T>.TreeSubSet treeSubSet = this as SortedSet<T>.TreeSubSet;
			if (treeSubSet != null)
			{
				this.VersionCheck();
			}
			if (sortedSet != null && treeSubSet == null && SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				T[] array = new T[this.Count];
				int num = 0;
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				T max = this.Max;
				T min = this.Min;
				while (!flag && !flag2 && this.Comparer.Compare(enumerator2.Current, max) <= 0)
				{
					int num2 = this.Comparer.Compare(enumerator.Current, enumerator2.Current);
					if (num2 < 0)
					{
						flag = !enumerator.MoveNext();
					}
					else if (num2 == 0)
					{
						array[num++] = enumerator2.Current;
						flag = !enumerator.MoveNext();
						flag2 = !enumerator2.MoveNext();
					}
					else
					{
						flag2 = !enumerator2.MoveNext();
					}
				}
				this.root = null;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
				this.version++;
				return;
			}
			this.IntersectWithEnumerable(other);
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000AC958 File Offset: 0x000AAB58
		internal virtual void IntersectWithEnumerable(IEnumerable<T> other)
		{
			List<T> list = new List<T>(this.Count);
			foreach (T t in other)
			{
				if (this.Contains(t))
				{
					list.Add(t);
					this.Remove(t);
				}
			}
			this.Clear();
			this.AddAllElements(list);
		}

		/// <summary>Removes all elements that are in a specified collection from the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		/// <param name="other">The collection of items to remove from the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06002511 RID: 9489 RVA: 0x000AC9CC File Offset: 0x000AABCC
		[global::__DynamicallyInvokable]
		public void ExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.count == 0)
			{
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				if (this.comparer.Compare(sortedSet.Max, this.Min) < 0 || this.comparer.Compare(sortedSet.Min, this.Max) > 0)
				{
					return;
				}
				T min = this.Min;
				T max = this.Max;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						if (this.comparer.Compare(t, min) >= 0)
						{
							if (this.comparer.Compare(t, max) > 0)
							{
								break;
							}
							this.Remove(t);
						}
					}
					return;
				}
			}
			this.RemoveAllElements(other);
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains only elements that are present either in the current object or in the specified collection, but not both.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06002512 RID: 9490 RVA: 0x000ACAC4 File Offset: 0x000AACC4
		[global::__DynamicallyInvokable]
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				this.UnionWith(other);
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				this.SymmetricExceptWithSameEC(sortedSet);
				return;
			}
			T[] array = new List<T>(other).ToArray();
			Array.Sort<T>(array, this.Comparer);
			this.SymmetricExceptWithSameEC(array);
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000ACB34 File Offset: 0x000AAD34
		internal void SymmetricExceptWithSameEC(ISet<T> other)
		{
			foreach (T t in other)
			{
				if (this.Contains(t))
				{
					this.Remove(t);
				}
				else
				{
					this.Add(t);
				}
			}
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000ACB90 File Offset: 0x000AAD90
		internal void SymmetricExceptWithSameEC(T[] other)
		{
			if (other.Length == 0)
			{
				return;
			}
			T t = other[0];
			for (int i = 0; i < other.Length; i++)
			{
				while (i < other.Length && i != 0 && this.comparer.Compare(other[i], t) == 0)
				{
					i++;
				}
				if (i >= other.Length)
				{
					break;
				}
				if (this.Contains(other[i]))
				{
					this.Remove(other[i]);
				}
				else
				{
					this.Add(other[i]);
				}
				t = other[i];
			}
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a subset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a subset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06002515 RID: 9493 RVA: 0x000ACC18 File Offset: 0x000AAE18
		[SecuritySafeCritical]
		[global::__DynamicallyInvokable]
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				return this.Count <= sortedSet.Count && this.IsSubsetOfSortedSetWithSameEC(sortedSet);
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.uniqueCount == this.Count && elementCount.unfoundCount >= 0;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000ACC90 File Offset: 0x000AAE90
		private bool IsSubsetOfSortedSetWithSameEC(SortedSet<T> asSorted)
		{
			SortedSet<T> viewBetween = asSorted.GetViewBetween(this.Min, this.Max);
			foreach (T t in this)
			{
				if (!viewBetween.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper subset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper subset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06002517 RID: 9495 RVA: 0x000ACCFC File Offset: 0x000AAEFC
		[SecuritySafeCritical]
		[global::__DynamicallyInvokable]
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other is ICollection && this.Count == 0)
			{
				return (other as ICollection).Count > 0;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				return this.Count < sortedSet.Count && this.IsSubsetOfSortedSetWithSameEC(sortedSet);
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.uniqueCount == this.Count && elementCount.unfoundCount > 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a superset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a superset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06002518 RID: 9496 RVA: 0x000ACD84 File Offset: 0x000AAF84
		[global::__DynamicallyInvokable]
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other is ICollection && (other as ICollection).Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet == null || !SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				return this.ContainsAllElements(other);
			}
			if (this.Count < sortedSet.Count)
			{
				return false;
			}
			SortedSet<T> viewBetween = this.GetViewBetween(sortedSet.Min, sortedSet.Max);
			foreach (T t in sortedSet)
			{
				if (!viewBetween.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper superset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper superset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06002519 RID: 9497 RVA: 0x000ACE40 File Offset: 0x000AB040
		[SecuritySafeCritical]
		[global::__DynamicallyInvokable]
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return false;
			}
			if (other is ICollection && (other as ICollection).Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet == null || !SortedSet<T>.AreComparersEqual(sortedSet, this))
			{
				SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
				return elementCount.uniqueCount < this.Count && elementCount.unfoundCount == 0;
			}
			if (sortedSet.Count >= this.Count)
			{
				return false;
			}
			SortedSet<T> viewBetween = this.GetViewBetween(sortedSet.Min, sortedSet.Max);
			foreach (T t in sortedSet)
			{
				if (!viewBetween.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object and the specified collection contain the same elements.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is equal to <paramref name="other" />; otherwise, false.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x0600251A RID: 9498 RVA: 0x000ACF24 File Offset: 0x000AB124
		[SecuritySafeCritical]
		[global::__DynamicallyInvokable]
		public bool SetEquals(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				IEnumerator<T> enumerator = this.GetEnumerator();
				IEnumerator<T> enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				while (!flag && !flag2)
				{
					if (this.Comparer.Compare(enumerator.Current, enumerator2.Current) != 0)
					{
						return false;
					}
					flag = !enumerator.MoveNext();
					flag2 = !enumerator2.MoveNext();
				}
				return flag && flag2;
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
			return elementCount.uniqueCount == this.Count && elementCount.unfoundCount == 0;
		}

		/// <summary>Determines whether the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object and a specified collection share common elements.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object and <paramref name="other" /> share at least one common element; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x0600251B RID: 9499 RVA: 0x000ACFE4 File Offset: 0x000AB1E4
		[global::__DynamicallyInvokable]
		public bool Overlaps(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return false;
			}
			if (other is ICollection<T> && (other as ICollection<T>).Count == 0)
			{
				return false;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && SortedSet<T>.AreComparersEqual(this, sortedSet) && (this.comparer.Compare(this.Min, sortedSet.Max) > 0 || this.comparer.Compare(this.Max, sortedSet.Min) < 0))
			{
				return false;
			}
			foreach (T t in other)
			{
				if (this.Contains(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000AD0B0 File Offset: 0x000AB2B0
		[SecurityCritical]
		private unsafe SortedSet<T>.ElementCount CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
		{
			SortedSet<T>.ElementCount elementCount;
			if (this.Count == 0)
			{
				int num = 0;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						num++;
					}
				}
				elementCount.uniqueCount = 0;
				elementCount.unfoundCount = num;
				return elementCount;
			}
			int num2 = this.Count;
			int num3 = BitHelper.ToIntArrayLength(num2);
			BitHelper bitHelper;
			int num4;
			int num5;
			checked
			{
				if (num3 <= 100)
				{
					int* ptr = stackalloc int[unchecked((UIntPtr)num3) * 4];
					bitHelper = new BitHelper(ptr, num3);
				}
				else
				{
					int[] array = new int[num3];
					bitHelper = new BitHelper(array, num3);
				}
				num4 = 0;
				num5 = 0;
			}
			foreach (T t2 in other)
			{
				int num6 = this.InternalIndexOf(t2);
				if (num6 >= 0)
				{
					if (!bitHelper.IsMarked(num6))
					{
						bitHelper.MarkBit(num6);
						num5++;
					}
				}
				else
				{
					num4++;
					if (returnIfUnfound)
					{
						break;
					}
				}
			}
			elementCount.uniqueCount = num5;
			elementCount.unfoundCount = num4;
			return elementCount;
		}

		/// <summary>Removes all elements that match the conditions defined by the specified predicate from a <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <param name="match">The delegate that defines the conditions of the elements to remove.</param>
		/// <returns>The number of elements that were removed from the <see cref="T:System.Collections.Generic.SortedSet`1" /> collection.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x0600251D RID: 9501 RVA: 0x000AD1D8 File Offset: 0x000AB3D8
		[global::__DynamicallyInvokable]
		public int RemoveWhere(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<T> matches = new List<T>(this.Count);
			this.BreadthFirstTreeWalk(delegate(SortedSet<T>.Node n)
			{
				if (match(n.Item))
				{
					matches.Add(n.Item);
				}
				return true;
			});
			int num = 0;
			for (int i = matches.Count - 1; i >= 0; i--)
			{
				if (this.Remove(matches[i]))
				{
					num++;
				}
			}
			return num;
		}

		/// <summary>Gets the minimum value in the <see cref="T:System.Collections.Generic.SortedSet`1" />, as defined by the comparer.</summary>
		/// <returns>The minimum value in the set.</returns>
		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x000AD25C File Offset: 0x000AB45C
		[global::__DynamicallyInvokable]
		public T Min
		{
			[global::__DynamicallyInvokable]
			get
			{
				T ret = default(T);
				this.InOrderTreeWalk(delegate(SortedSet<T>.Node n)
				{
					ret = n.Item;
					return false;
				});
				return ret;
			}
		}

		/// <summary>Gets the maximum value in the <see cref="T:System.Collections.Generic.SortedSet`1" />, as defined by the comparer.</summary>
		/// <returns>The maximum value in the set.</returns>
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x000AD294 File Offset: 0x000AB494
		[global::__DynamicallyInvokable]
		public T Max
		{
			[global::__DynamicallyInvokable]
			get
			{
				T ret = default(T);
				this.InOrderTreeWalk(delegate(SortedSet<T>.Node n)
				{
					ret = n.Item;
					return false;
				}, true);
				return ret;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.Generic.IEnumerable`1" /> that iterates over the <see cref="T:System.Collections.Generic.SortedSet`1" /> in reverse order.</summary>
		/// <returns>An enumerator that iterates over the <see cref="T:System.Collections.Generic.SortedSet`1" /> in reverse order.</returns>
		// Token: 0x06002520 RID: 9504 RVA: 0x000AD2CD File Offset: 0x000AB4CD
		[global::__DynamicallyInvokable]
		public IEnumerable<T> Reverse()
		{
			SortedSet<T>.Enumerator e = new SortedSet<T>.Enumerator(this, true);
			while (e.MoveNext())
			{
				T t = e.Current;
				yield return t;
			}
			yield break;
		}

		/// <summary>Returns a view of a subset in a <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <param name="lowerValue">The lowest desired value in the view.</param>
		/// <param name="upperValue">The highest desired value in the view.</param>
		/// <returns>A subset view that contains only the values in the specified range.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="lowerValue" /> is more than <paramref name="upperValue" /> according to the comparer.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A tried operation on the view was outside the range specified by <paramref name="lowerValue" /> and <paramref name="upperValue" />.</exception>
		// Token: 0x06002521 RID: 9505 RVA: 0x000AD2DD File Offset: 0x000AB4DD
		[global::__DynamicallyInvokable]
		public virtual SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
		{
			if (this.Comparer.Compare(lowerValue, upperValue) > 0)
			{
				throw new ArgumentException("lowerBound is greater than upperBound");
			}
			return new SortedSet<T>.TreeSubSet(this, lowerValue, upperValue, true, true);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface, and returns the data that you need to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06002522 RID: 9506 RVA: 0x000AD304 File Offset: 0x000AB504
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data that you must have to serialize a <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06002523 RID: 9507 RVA: 0x000AD310 File Offset: 0x000AB510
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.info);
			}
			info.AddValue("Count", this.count);
			info.AddValue("Comparer", this.comparer, typeof(IComparer<T>));
			info.AddValue("Version", this.version);
			if (this.root != null)
			{
				T[] array = new T[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("Items", array, typeof(T[]));
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.IDeserializationCallback" /> interface, and raises the deserialization event when the deserialization is completed.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> instance is invalid.</exception>
		// Token: 0x06002524 RID: 9508 RVA: 0x000AD395 File Offset: 0x000AB595
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(sender);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface, and raises the deserialization event when the deserialization is completed.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is invalid.</exception>
		// Token: 0x06002525 RID: 9509 RVA: 0x000AD3A0 File Offset: 0x000AB5A0
		protected virtual void OnDeserialization(object sender)
		{
			if (this.comparer != null)
			{
				return;
			}
			if (this.siInfo == null)
			{
				System.ThrowHelper.ThrowSerializationException(System.ExceptionResource.Serialization_InvalidOnDeser);
			}
			this.comparer = (IComparer<T>)this.siInfo.GetValue("Comparer", typeof(IComparer<T>));
			int @int = this.siInfo.GetInt32("Count");
			if (@int != 0)
			{
				T[] array = (T[])this.siInfo.GetValue("Items", typeof(T[]));
				if (array == null)
				{
					System.ThrowHelper.ThrowSerializationException(System.ExceptionResource.Serialization_MissingValues);
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.Add(array[i]);
				}
			}
			this.version = this.siInfo.GetInt32("Version");
			if (this.count != @int)
			{
				System.ThrowHelper.ThrowSerializationException(System.ExceptionResource.Serialization_MismatchedCount);
			}
			this.siInfo = null;
		}

		/// <summary>Searches the set for a given value and returns the equal value it finds, if any.</summary>
		/// <param name="equalValue">The value to search for.</param>
		/// <param name="actualValue">The value from the set that the search found, or the default value of T when the search yielded no match.</param>
		/// <returns>A value indicating whether the search was successful.</returns>
		// Token: 0x06002526 RID: 9510 RVA: 0x000AD470 File Offset: 0x000AB670
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			SortedSet<T>.Node node = this.FindNode(equalValue);
			if (node != null)
			{
				actualValue = node.Item;
				return true;
			}
			actualValue = default(T);
			return false;
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000AD4A0 File Offset: 0x000AB6A0
		private static int log2(int value)
		{
			int num = 0;
			while (value > 0)
			{
				num++;
				value >>= 1;
			}
			return num;
		}

		// Token: 0x0400201D RID: 8221
		private SortedSet<T>.Node root;

		// Token: 0x0400201E RID: 8222
		private IComparer<T> comparer;

		// Token: 0x0400201F RID: 8223
		private int count;

		// Token: 0x04002020 RID: 8224
		private int version;

		// Token: 0x04002021 RID: 8225
		private object _syncRoot;

		// Token: 0x04002022 RID: 8226
		private const string ComparerName = "Comparer";

		// Token: 0x04002023 RID: 8227
		private const string CountName = "Count";

		// Token: 0x04002024 RID: 8228
		private const string ItemsName = "Items";

		// Token: 0x04002025 RID: 8229
		private const string VersionName = "Version";

		// Token: 0x04002026 RID: 8230
		private const string TreeName = "Tree";

		// Token: 0x04002027 RID: 8231
		private const string NodeValueName = "Item";

		// Token: 0x04002028 RID: 8232
		private const string EnumStartName = "EnumStarted";

		// Token: 0x04002029 RID: 8233
		private const string ReverseName = "Reverse";

		// Token: 0x0400202A RID: 8234
		private const string EnumVersionName = "EnumVersion";

		// Token: 0x0400202B RID: 8235
		private const string minName = "Min";

		// Token: 0x0400202C RID: 8236
		private const string maxName = "Max";

		// Token: 0x0400202D RID: 8237
		private const string lBoundActiveName = "lBoundActive";

		// Token: 0x0400202E RID: 8238
		private const string uBoundActiveName = "uBoundActive";

		// Token: 0x0400202F RID: 8239
		private SerializationInfo siInfo;

		// Token: 0x04002030 RID: 8240
		internal const int StackAllocThreshold = 100;

		// Token: 0x020007FF RID: 2047
		[Serializable]
		internal sealed class TreeSubSet : SortedSet<T>, ISerializable, IDeserializationCallback
		{
			// Token: 0x0600447E RID: 17534 RVA: 0x0011EBD0 File Offset: 0x0011CDD0
			public TreeSubSet(SortedSet<T> Underlying, T Min, T Max, bool lowerBoundActive, bool upperBoundActive)
				: base(Underlying.Comparer)
			{
				this.underlying = Underlying;
				this.min = Min;
				this.max = Max;
				this.lBoundActive = lowerBoundActive;
				this.uBoundActive = upperBoundActive;
				this.root = this.underlying.FindRange(this.min, this.max, this.lBoundActive, this.uBoundActive);
				this.count = 0;
				this.version = -1;
				this.VersionCheckImpl();
			}

			// Token: 0x0600447F RID: 17535 RVA: 0x0011EC4B File Offset: 0x0011CE4B
			private TreeSubSet()
			{
				this.comparer = null;
			}

			// Token: 0x06004480 RID: 17536 RVA: 0x0011EC5A File Offset: 0x0011CE5A
			private TreeSubSet(SerializationInfo info, StreamingContext context)
			{
				this.siInfo = info;
				this.OnDeserializationImpl(info);
			}

			// Token: 0x06004481 RID: 17537 RVA: 0x0011EC70 File Offset: 0x0011CE70
			internal override bool AddIfNotPresent(T item)
			{
				if (!this.IsWithinRange(item))
				{
					System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.collection);
				}
				bool flag = this.underlying.AddIfNotPresent(item);
				this.VersionCheck();
				return flag;
			}

			// Token: 0x06004482 RID: 17538 RVA: 0x0011ECA0 File Offset: 0x0011CEA0
			public override bool Contains(T item)
			{
				this.VersionCheck();
				return base.Contains(item);
			}

			// Token: 0x06004483 RID: 17539 RVA: 0x0011ECB0 File Offset: 0x0011CEB0
			internal override bool DoRemove(T item)
			{
				if (!this.IsWithinRange(item))
				{
					return false;
				}
				bool flag = this.underlying.Remove(item);
				this.VersionCheck();
				return flag;
			}

			// Token: 0x06004484 RID: 17540 RVA: 0x0011ECDC File Offset: 0x0011CEDC
			public override void Clear()
			{
				if (this.count == 0)
				{
					return;
				}
				List<T> toRemove = new List<T>();
				this.BreadthFirstTreeWalk(delegate(SortedSet<T>.Node n)
				{
					toRemove.Add(n.Item);
					return true;
				});
				while (toRemove.Count != 0)
				{
					this.underlying.Remove(toRemove[toRemove.Count - 1]);
					toRemove.RemoveAt(toRemove.Count - 1);
				}
				this.root = null;
				this.count = 0;
				this.version = this.underlying.version;
			}

			// Token: 0x06004485 RID: 17541 RVA: 0x0011ED80 File Offset: 0x0011CF80
			internal override bool IsWithinRange(T item)
			{
				int num = (this.lBoundActive ? base.Comparer.Compare(this.min, item) : (-1));
				if (num > 0)
				{
					return false;
				}
				num = (this.uBoundActive ? base.Comparer.Compare(this.max, item) : 1);
				return num >= 0;
			}

			// Token: 0x06004486 RID: 17542 RVA: 0x0011EDD8 File Offset: 0x0011CFD8
			internal override bool InOrderTreeWalk(TreeWalkPredicate<T> action, bool reverse)
			{
				this.VersionCheck();
				if (this.root == null)
				{
					return true;
				}
				Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(this.count + 1));
				SortedSet<T>.Node node = this.root;
				while (node != null)
				{
					if (this.IsWithinRange(node.Item))
					{
						stack.Push(node);
						node = (reverse ? node.Right : node.Left);
					}
					else if (this.lBoundActive && base.Comparer.Compare(this.min, node.Item) > 0)
					{
						node = node.Right;
					}
					else
					{
						node = node.Left;
					}
				}
				while (stack.Count != 0)
				{
					node = stack.Pop();
					if (!action(node))
					{
						return false;
					}
					SortedSet<T>.Node node2 = (reverse ? node.Left : node.Right);
					while (node2 != null)
					{
						if (this.IsWithinRange(node2.Item))
						{
							stack.Push(node2);
							node2 = (reverse ? node2.Right : node2.Left);
						}
						else if (this.lBoundActive && base.Comparer.Compare(this.min, node2.Item) > 0)
						{
							node2 = node2.Right;
						}
						else
						{
							node2 = node2.Left;
						}
					}
				}
				return true;
			}

			// Token: 0x06004487 RID: 17543 RVA: 0x0011EF08 File Offset: 0x0011D108
			internal override bool BreadthFirstTreeWalk(TreeWalkPredicate<T> action)
			{
				this.VersionCheck();
				if (this.root == null)
				{
					return true;
				}
				List<SortedSet<T>.Node> list = new List<SortedSet<T>.Node>();
				list.Add(this.root);
				while (list.Count != 0)
				{
					SortedSet<T>.Node node = list[0];
					list.RemoveAt(0);
					if (this.IsWithinRange(node.Item) && !action(node))
					{
						return false;
					}
					if (node.Left != null && (!this.lBoundActive || base.Comparer.Compare(this.min, node.Item) < 0))
					{
						list.Add(node.Left);
					}
					if (node.Right != null && (!this.uBoundActive || base.Comparer.Compare(this.max, node.Item) > 0))
					{
						list.Add(node.Right);
					}
				}
				return true;
			}

			// Token: 0x06004488 RID: 17544 RVA: 0x0011EFDC File Offset: 0x0011D1DC
			internal override SortedSet<T>.Node FindNode(T item)
			{
				if (!this.IsWithinRange(item))
				{
					return null;
				}
				this.VersionCheck();
				return base.FindNode(item);
			}

			// Token: 0x06004489 RID: 17545 RVA: 0x0011EFF8 File Offset: 0x0011D1F8
			internal override int InternalIndexOf(T item)
			{
				int num = -1;
				foreach (T t in this)
				{
					num++;
					if (base.Comparer.Compare(item, t) == 0)
					{
						return num;
					}
				}
				return -1;
			}

			// Token: 0x0600448A RID: 17546 RVA: 0x0011F05C File Offset: 0x0011D25C
			internal override void VersionCheck()
			{
				this.VersionCheckImpl();
			}

			// Token: 0x0600448B RID: 17547 RVA: 0x0011F064 File Offset: 0x0011D264
			private void VersionCheckImpl()
			{
				if (this.version != this.underlying.version)
				{
					this.root = this.underlying.FindRange(this.min, this.max, this.lBoundActive, this.uBoundActive);
					this.version = this.underlying.version;
					this.count = 0;
					base.InOrderTreeWalk(delegate(SortedSet<T>.Node n)
					{
						this.count++;
						return true;
					});
				}
			}

			// Token: 0x0600448C RID: 17548 RVA: 0x0011F0D8 File Offset: 0x0011D2D8
			public override SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
			{
				if (this.lBoundActive && base.Comparer.Compare(this.min, lowerValue) > 0)
				{
					throw new ArgumentOutOfRangeException("lowerValue");
				}
				if (this.uBoundActive && base.Comparer.Compare(this.max, upperValue) < 0)
				{
					throw new ArgumentOutOfRangeException("upperValue");
				}
				return (SortedSet<T>.TreeSubSet)this.underlying.GetViewBetween(lowerValue, upperValue);
			}

			// Token: 0x0600448D RID: 17549 RVA: 0x0011F14C File Offset: 0x0011D34C
			internal override void IntersectWithEnumerable(IEnumerable<T> other)
			{
				List<T> list = new List<T>(base.Count);
				foreach (T t in other)
				{
					if (this.Contains(t))
					{
						list.Add(t);
						base.Remove(t);
					}
				}
				this.Clear();
				base.AddAllElements(list);
			}

			// Token: 0x0600448E RID: 17550 RVA: 0x0011F1C0 File Offset: 0x0011D3C0
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				this.GetObjectData(info, context);
			}

			// Token: 0x0600448F RID: 17551 RVA: 0x0011F1CC File Offset: 0x0011D3CC
			protected override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.info);
				}
				info.AddValue("Max", this.max, typeof(T));
				info.AddValue("Min", this.min, typeof(T));
				info.AddValue("lBoundActive", this.lBoundActive);
				info.AddValue("uBoundActive", this.uBoundActive);
				base.GetObjectData(info, context);
			}

			// Token: 0x06004490 RID: 17552 RVA: 0x0011F24C File Offset: 0x0011D44C
			void IDeserializationCallback.OnDeserialization(object sender)
			{
			}

			// Token: 0x06004491 RID: 17553 RVA: 0x0011F24E File Offset: 0x0011D44E
			protected override void OnDeserialization(object sender)
			{
				this.OnDeserializationImpl(sender);
			}

			// Token: 0x06004492 RID: 17554 RVA: 0x0011F258 File Offset: 0x0011D458
			private void OnDeserializationImpl(object sender)
			{
				if (this.siInfo == null)
				{
					System.ThrowHelper.ThrowSerializationException(System.ExceptionResource.Serialization_InvalidOnDeser);
				}
				this.comparer = (IComparer<T>)this.siInfo.GetValue("Comparer", typeof(IComparer<T>));
				int @int = this.siInfo.GetInt32("Count");
				this.max = (T)((object)this.siInfo.GetValue("Max", typeof(T)));
				this.min = (T)((object)this.siInfo.GetValue("Min", typeof(T)));
				this.lBoundActive = this.siInfo.GetBoolean("lBoundActive");
				this.uBoundActive = this.siInfo.GetBoolean("uBoundActive");
				this.underlying = new SortedSet<T>();
				if (@int != 0)
				{
					T[] array = (T[])this.siInfo.GetValue("Items", typeof(T[]));
					if (array == null)
					{
						System.ThrowHelper.ThrowSerializationException(System.ExceptionResource.Serialization_MissingValues);
					}
					for (int i = 0; i < array.Length; i++)
					{
						this.underlying.Add(array[i]);
					}
				}
				this.underlying.version = this.siInfo.GetInt32("Version");
				this.count = this.underlying.count;
				this.version = this.underlying.version - 1;
				this.VersionCheck();
				if (this.count != @int)
				{
					System.ThrowHelper.ThrowSerializationException(System.ExceptionResource.Serialization_MismatchedCount);
				}
				this.siInfo = null;
			}

			// Token: 0x04003522 RID: 13602
			private SortedSet<T> underlying;

			// Token: 0x04003523 RID: 13603
			private T min;

			// Token: 0x04003524 RID: 13604
			private T max;

			// Token: 0x04003525 RID: 13605
			private bool lBoundActive;

			// Token: 0x04003526 RID: 13606
			private bool uBoundActive;
		}

		// Token: 0x02000800 RID: 2048
		internal class Node
		{
			// Token: 0x06004494 RID: 17556 RVA: 0x0011F3E3 File Offset: 0x0011D5E3
			public Node(T item)
			{
				this.Item = item;
				this.IsRed = true;
			}

			// Token: 0x06004495 RID: 17557 RVA: 0x0011F3F9 File Offset: 0x0011D5F9
			public Node(T item, bool isRed)
			{
				this.Item = item;
				this.IsRed = isRed;
			}

			// Token: 0x04003527 RID: 13607
			public bool IsRed;

			// Token: 0x04003528 RID: 13608
			public T Item;

			// Token: 0x04003529 RID: 13609
			public SortedSet<T>.Node Left;

			// Token: 0x0400352A RID: 13610
			public SortedSet<T>.Node Right;
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		/// <typeparam name="T" />
		// Token: 0x02000801 RID: 2049
		[global::__DynamicallyInvokable]
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator, ISerializable, IDeserializationCallback
		{
			// Token: 0x06004496 RID: 17558 RVA: 0x0011F410 File Offset: 0x0011D610
			internal Enumerator(SortedSet<T> set)
			{
				this.tree = set;
				this.tree.VersionCheck();
				this.version = this.tree.version;
				this.stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(set.Count + 1));
				this.current = null;
				this.reverse = false;
				this.siInfo = null;
				this.Intialize();
			}

			// Token: 0x06004497 RID: 17559 RVA: 0x0011F478 File Offset: 0x0011D678
			internal Enumerator(SortedSet<T> set, bool reverse)
			{
				this.tree = set;
				this.tree.VersionCheck();
				this.version = this.tree.version;
				this.stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(set.Count + 1));
				this.current = null;
				this.reverse = reverse;
				this.siInfo = null;
				this.Intialize();
			}

			// Token: 0x06004498 RID: 17560 RVA: 0x0011F4DD File Offset: 0x0011D6DD
			private Enumerator(SerializationInfo info, StreamingContext context)
			{
				this.tree = null;
				this.version = -1;
				this.current = null;
				this.reverse = false;
				this.stack = null;
				this.siInfo = info;
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</summary>
			/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
			/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="info" /> is <see langword="null" />.</exception>
			// Token: 0x06004499 RID: 17561 RVA: 0x0011F509 File Offset: 0x0011D709
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				this.GetObjectData(info, context);
			}

			// Token: 0x0600449A RID: 17562 RVA: 0x0011F514 File Offset: 0x0011D714
			private void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.info);
				}
				info.AddValue("Tree", this.tree, typeof(SortedSet<T>));
				info.AddValue("EnumVersion", this.version);
				info.AddValue("Reverse", this.reverse);
				info.AddValue("EnumStarted", !this.NotStartedOrEnded);
				info.AddValue("Item", (this.current == null) ? SortedSet<T>.Enumerator.dummyNode.Item : this.current.Item, typeof(T));
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
			/// <param name="sender">The source of the deserialization event.</param>
			/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> instance is invalid.</exception>
			// Token: 0x0600449B RID: 17563 RVA: 0x0011F5B4 File Offset: 0x0011D7B4
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				this.OnDeserialization(sender);
			}

			// Token: 0x0600449C RID: 17564 RVA: 0x0011F5C0 File Offset: 0x0011D7C0
			private void OnDeserialization(object sender)
			{
				if (this.siInfo == null)
				{
					System.ThrowHelper.ThrowSerializationException(System.ExceptionResource.Serialization_InvalidOnDeser);
				}
				this.tree = (SortedSet<T>)this.siInfo.GetValue("Tree", typeof(SortedSet<T>));
				this.version = this.siInfo.GetInt32("EnumVersion");
				this.reverse = this.siInfo.GetBoolean("Reverse");
				bool boolean = this.siInfo.GetBoolean("EnumStarted");
				this.stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(this.tree.Count + 1));
				this.current = null;
				if (boolean)
				{
					T t = (T)((object)this.siInfo.GetValue("Item", typeof(T)));
					this.Intialize();
					while (this.MoveNext() && this.tree.Comparer.Compare(this.Current, t) != 0)
					{
					}
				}
			}

			// Token: 0x0600449D RID: 17565 RVA: 0x0011F6B0 File Offset: 0x0011D8B0
			private void Intialize()
			{
				this.current = null;
				SortedSet<T>.Node node = this.tree.root;
				while (node != null)
				{
					SortedSet<T>.Node node2 = (this.reverse ? node.Right : node.Left);
					SortedSet<T>.Node node3 = (this.reverse ? node.Left : node.Right);
					if (this.tree.IsWithinRange(node.Item))
					{
						this.stack.Push(node);
						node = node2;
					}
					else if (node2 == null || !this.tree.IsWithinRange(node2.Item))
					{
						node = node3;
					}
					else
					{
						node = node2;
					}
				}
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedSet`1" /> collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x0600449E RID: 17566 RVA: 0x0011F748 File Offset: 0x0011D948
			[global::__DynamicallyInvokable]
			public bool MoveNext()
			{
				this.tree.VersionCheck();
				if (this.version != this.tree.version)
				{
					System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				if (this.stack.Count == 0)
				{
					this.current = null;
					return false;
				}
				this.current = this.stack.Pop();
				SortedSet<T>.Node node = (this.reverse ? this.current.Left : this.current.Right);
				while (node != null)
				{
					SortedSet<T>.Node node2 = (this.reverse ? node.Right : node.Left);
					SortedSet<T>.Node node3 = (this.reverse ? node.Left : node.Right);
					if (this.tree.IsWithinRange(node.Item))
					{
						this.stack.Push(node);
						node = node2;
					}
					else if (node3 == null || !this.tree.IsWithinRange(node3.Item))
					{
						node = node2;
					}
					else
					{
						node = node3;
					}
				}
				return true;
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedSet`1.Enumerator" />.</summary>
			// Token: 0x0600449F RID: 17567 RVA: 0x0011F839 File Offset: 0x0011DA39
			[global::__DynamicallyInvokable]
			public void Dispose()
			{
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000F93 RID: 3987
			// (get) Token: 0x060044A0 RID: 17568 RVA: 0x0011F83C File Offset: 0x0011DA3C
			[global::__DynamicallyInvokable]
			public T Current
			{
				[global::__DynamicallyInvokable]
				get
				{
					if (this.current != null)
					{
						return this.current.Item;
					}
					return default(T);
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x17000F94 RID: 3988
			// (get) Token: 0x060044A1 RID: 17569 RVA: 0x0011F866 File Offset: 0x0011DA66
			[global::__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[global::__DynamicallyInvokable]
				get
				{
					if (this.current == null)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.current.Item;
				}
			}

			// Token: 0x17000F95 RID: 3989
			// (get) Token: 0x060044A2 RID: 17570 RVA: 0x0011F887 File Offset: 0x0011DA87
			internal bool NotStartedOrEnded
			{
				get
				{
					return this.current == null;
				}
			}

			// Token: 0x060044A3 RID: 17571 RVA: 0x0011F892 File Offset: 0x0011DA92
			internal void Reset()
			{
				if (this.version != this.tree.version)
				{
					System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.stack.Clear();
				this.Intialize();
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x060044A4 RID: 17572 RVA: 0x0011F8BF File Offset: 0x0011DABF
			[global::__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				this.Reset();
			}

			// Token: 0x0400352B RID: 13611
			private SortedSet<T> tree;

			// Token: 0x0400352C RID: 13612
			private int version;

			// Token: 0x0400352D RID: 13613
			private Stack<SortedSet<T>.Node> stack;

			// Token: 0x0400352E RID: 13614
			private SortedSet<T>.Node current;

			// Token: 0x0400352F RID: 13615
			private static SortedSet<T>.Node dummyNode = new SortedSet<T>.Node(default(T));

			// Token: 0x04003530 RID: 13616
			private bool reverse;

			// Token: 0x04003531 RID: 13617
			private SerializationInfo siInfo;
		}

		// Token: 0x02000802 RID: 2050
		internal struct ElementCount
		{
			// Token: 0x04003532 RID: 13618
			internal int uniqueCount;

			// Token: 0x04003533 RID: 13619
			internal int unfoundCount;
		}
	}
}
