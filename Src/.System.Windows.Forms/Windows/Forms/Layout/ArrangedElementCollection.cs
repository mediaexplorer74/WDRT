using System;
using System.Collections;

namespace System.Windows.Forms.Layout
{
	/// <summary>Represents a collection of objects.</summary>
	// Token: 0x020004C6 RID: 1222
	public class ArrangedElementCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x0600501D RID: 20509 RVA: 0x0014D244 File Offset: 0x0014B444
		internal ArrangedElementCollection()
		{
			this._innerList = new ArrayList(4);
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x0014D258 File Offset: 0x0014B458
		internal ArrangedElementCollection(ArrayList innerList)
		{
			this._innerList = innerList;
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x0014D267 File Offset: 0x0014B467
		private ArrangedElementCollection(int size)
		{
			this._innerList = new ArrayList(size);
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06005020 RID: 20512 RVA: 0x0014D27B File Offset: 0x0014B47B
		internal ArrayList InnerList
		{
			get
			{
				return this._innerList;
			}
		}

		// Token: 0x17001383 RID: 4995
		internal virtual IArrangedElement this[int index]
		{
			get
			{
				return (IArrangedElement)this.InnerList[index];
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> instances are equal.</summary>
		/// <param name="obj">The <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> to compare with the current <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> is equal to the current <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005022 RID: 20514 RVA: 0x0014D298 File Offset: 0x0014B498
		public override bool Equals(object obj)
		{
			ArrangedElementCollection arrangedElementCollection = obj as ArrangedElementCollection;
			if (arrangedElementCollection == null || this.Count != arrangedElementCollection.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (this.InnerList[i] != arrangedElementCollection.InnerList[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />.</returns>
		// Token: 0x06005023 RID: 20515 RVA: 0x0014D2ED File Offset: 0x0014B4ED
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x0014D2F8 File Offset: 0x0014B4F8
		internal void MoveElement(IArrangedElement element, int fromIndex, int toIndex)
		{
			int num = toIndex - fromIndex;
			if (num == -1 || num == 1)
			{
				this.InnerList[fromIndex] = this.InnerList[toIndex];
			}
			else
			{
				int num2;
				int num3;
				if (num > 0)
				{
					num2 = fromIndex + 1;
					num3 = fromIndex;
				}
				else
				{
					num2 = toIndex;
					num3 = toIndex + 1;
					num = -num;
				}
				ArrangedElementCollection.Copy(this, num2, this, num3, num);
			}
			this.InnerList[toIndex] = element;
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x0014D35C File Offset: 0x0014B55C
		private static void Copy(ArrangedElementCollection sourceList, int sourceIndex, ArrangedElementCollection destinationList, int destinationIndex, int length)
		{
			if (sourceIndex < destinationIndex)
			{
				sourceIndex += length;
				destinationIndex += length;
				while (length > 0)
				{
					destinationList.InnerList[--destinationIndex] = sourceList.InnerList[--sourceIndex];
					length--;
				}
				return;
			}
			while (length > 0)
			{
				destinationList.InnerList[destinationIndex++] = sourceList.InnerList[sourceIndex++];
				length--;
			}
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Clear" /> method.</summary>
		// Token: 0x06005026 RID: 20518 RVA: 0x0014D3D6 File Offset: 0x0014B5D6
		void IList.Clear()
		{
			this.InnerList.Clear();
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.IList.IsFixedSize" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06005027 RID: 20519 RVA: 0x0011C9DC File Offset: 0x0011ABDC
		bool IList.IsFixedSize
		{
			get
			{
				return this.InnerList.IsFixedSize;
			}
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Contains(System.Object)" /> method.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005028 RID: 20520 RVA: 0x0011C768 File Offset: 0x0011A968
		bool IList.Contains(object value)
		{
			return this.InnerList.Contains(value);
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06005029 RID: 20521 RVA: 0x0014D3E3 File Offset: 0x0014B5E3
		public virtual bool IsReadOnly
		{
			get
			{
				return this.InnerList.IsReadOnly;
			}
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" /> method.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		// Token: 0x0600502A RID: 20522 RVA: 0x0014D3F0 File Offset: 0x0014B5F0
		void IList.RemoveAt(int index)
		{
			this.InnerList.RemoveAt(index);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600502B RID: 20523 RVA: 0x0014D3FE File Offset: 0x0014B5FE
		void IList.Remove(object value)
		{
			this.InnerList.Remove(value);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Add(System.Object)" /> method.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		// Token: 0x0600502C RID: 20524 RVA: 0x0014D40C File Offset: 0x0014B60C
		int IList.Add(object value)
		{
			return this.InnerList.Add(value);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		// Token: 0x0600502D RID: 20525 RVA: 0x0011CACC File Offset: 0x0011ACCC
		int IList.IndexOf(object value)
		{
			return this.InnerList.IndexOf(value);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600502E RID: 20526 RVA: 0x0000A337 File Offset: 0x00008537
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.IList.Item(System.Int32)" /> property.</summary>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <returns>The element at the specified index.</returns>
		// Token: 0x17001386 RID: 4998
		object IList.this[int index]
		{
			get
			{
				return this.InnerList[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements currently contained in the collection.</returns>
		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x06005031 RID: 20529 RVA: 0x0014D41A File Offset: 0x0014B61A
		public virtual int Count
		{
			get
			{
				return this.InnerList.Count;
			}
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.SyncRoot" /> property.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />.</returns>
		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x06005032 RID: 20530 RVA: 0x0014D427 File Offset: 0x0014B627
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		/// <summary>Copies the entire contents of this collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source element cannot be cast automatically to the type of <paramref name="array" />.</exception>
		// Token: 0x06005033 RID: 20531 RVA: 0x0011CCA9 File Offset: 0x0011AEA9
		public void CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.IsSynchronized" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x06005034 RID: 20532 RVA: 0x0014D434 File Offset: 0x0014B634
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		/// <summary>Returns an enumerator for the entire collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire collection.</returns>
		// Token: 0x06005035 RID: 20533 RVA: 0x0014D441 File Offset: 0x0014B641
		public virtual IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		// Token: 0x0400346E RID: 13422
		internal static ArrangedElementCollection Empty = new ArrangedElementCollection(0);

		// Token: 0x0400346F RID: 13423
		private ArrayList _innerList;
	}
}
