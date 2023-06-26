using System;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	/// <summary>Represents a node in a <see cref="T:System.Collections.Generic.LinkedList`1" />. This class cannot be inherited.</summary>
	/// <typeparam name="T">Specifies the element type of the linked list.</typeparam>
	// Token: 0x020003C3 RID: 963
	[ComVisible(false)]
	[global::__DynamicallyInvokable]
	public sealed class LinkedListNode<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> class, containing the specified value.</summary>
		/// <param name="value">The value to contain in the <see cref="T:System.Collections.Generic.LinkedListNode`1" />.</param>
		// Token: 0x06002436 RID: 9270 RVA: 0x000A9949 File Offset: 0x000A7B49
		[global::__DynamicallyInvokable]
		public LinkedListNode(T value)
		{
			this.item = value;
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000A9958 File Offset: 0x000A7B58
		internal LinkedListNode(LinkedList<T> list, T value)
		{
			this.list = list;
			this.item = value;
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.LinkedList`1" /> that the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> belongs to.</summary>
		/// <returns>A reference to the <see cref="T:System.Collections.Generic.LinkedList`1" /> that the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> belongs to, or <see langword="null" /> if the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> is not linked.</returns>
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002438 RID: 9272 RVA: 0x000A996E File Offset: 0x000A7B6E
		[global::__DynamicallyInvokable]
		public LinkedList<T> List
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.list;
			}
		}

		/// <summary>Gets the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or <see langword="null" /> if the current node is the last element (<see cref="P:System.Collections.Generic.LinkedList`1.Last" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002439 RID: 9273 RVA: 0x000A9976 File Offset: 0x000A7B76
		[global::__DynamicallyInvokable]
		public LinkedListNode<T> Next
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this.next != null && this.next != this.list.head)
				{
					return this.next;
				}
				return null;
			}
		}

		/// <summary>Gets the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or <see langword="null" /> if the current node is the first element (<see cref="P:System.Collections.Generic.LinkedList`1.First" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x0600243A RID: 9274 RVA: 0x000A999B File Offset: 0x000A7B9B
		[global::__DynamicallyInvokable]
		public LinkedListNode<T> Previous
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this.prev != null && this != this.list.head)
				{
					return this.prev;
				}
				return null;
			}
		}

		/// <summary>Gets the value contained in the node.</summary>
		/// <returns>The value contained in the node.</returns>
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x0600243B RID: 9275 RVA: 0x000A99BB File Offset: 0x000A7BBB
		// (set) Token: 0x0600243C RID: 9276 RVA: 0x000A99C3 File Offset: 0x000A7BC3
		[global::__DynamicallyInvokable]
		public T Value
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.item;
			}
			[global::__DynamicallyInvokable]
			set
			{
				this.item = value;
			}
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000A99CC File Offset: 0x000A7BCC
		internal void Invalidate()
		{
			this.list = null;
			this.next = null;
			this.prev = null;
		}

		// Token: 0x04001FF4 RID: 8180
		internal LinkedList<T> list;

		// Token: 0x04001FF5 RID: 8181
		internal LinkedListNode<T> next;

		// Token: 0x04001FF6 RID: 8182
		internal LinkedListNode<T> prev;

		// Token: 0x04001FF7 RID: 8183
		internal T item;
	}
}
