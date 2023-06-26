using System;
using System.Collections;
using System.Collections.Generic;

namespace System
{
	/// <summary>Delimits a section of a one-dimensional array.</summary>
	/// <typeparam name="T">The type of the elements in the array segment.</typeparam>
	// Token: 0x02000057 RID: 87
	[__DynamicallyInvokable]
	[Serializable]
	public struct ArraySegment<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArraySegment`1" /> structure that delimits all the elements in the specified array.</summary>
		/// <param name="array">The array to wrap.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		// Token: 0x0600031A RID: 794 RVA: 0x00007D1E File Offset: 0x00005F1E
		[__DynamicallyInvokable]
		public ArraySegment(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this._array = array;
			this._offset = 0;
			this._count = array.Length;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArraySegment`1" /> structure that delimits the specified range of the elements in the specified array.</summary>
		/// <param name="array">The array containing the range of elements to delimit.</param>
		/// <param name="offset">The zero-based index of the first element in the range.</param>
		/// <param name="count">The number of elements in the range.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> and <paramref name="count" /> do not specify a valid range in <paramref name="array" />.</exception>
		// Token: 0x0600031B RID: 795 RVA: 0x00007D48 File Offset: 0x00005F48
		[__DynamicallyInvokable]
		public ArraySegment(T[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			this._array = array;
			this._offset = offset;
			this._count = count;
		}

		/// <summary>Gets the original array containing the range of elements that the array segment delimits.</summary>
		/// <returns>The original array that was passed to the constructor, and that contains the range delimited by the <see cref="T:System.ArraySegment`1" />.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00007DC2 File Offset: 0x00005FC2
		[__DynamicallyInvokable]
		public T[] Array
		{
			[__DynamicallyInvokable]
			get
			{
				return this._array;
			}
		}

		/// <summary>Gets the position of the first element in the range delimited by the array segment, relative to the start of the original array.</summary>
		/// <returns>The position of the first element in the range delimited by the <see cref="T:System.ArraySegment`1" />, relative to the start of the original array.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00007DCA File Offset: 0x00005FCA
		[__DynamicallyInvokable]
		public int Offset
		{
			[__DynamicallyInvokable]
			get
			{
				return this._offset;
			}
		}

		/// <summary>Gets the number of elements in the range delimited by the array segment.</summary>
		/// <returns>The number of elements in the range delimited by the <see cref="T:System.ArraySegment`1" />.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00007DD2 File Offset: 0x00005FD2
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this._count;
			}
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600031F RID: 799 RVA: 0x00007DDA File Offset: 0x00005FDA
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this._array != null)
			{
				return this._array.GetHashCode() ^ this._offset ^ this._count;
			}
			return 0;
		}

		/// <summary>Determines whether the specified object is equal to the current instance.</summary>
		/// <param name="obj">The object to be compared with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is a <see cref="T:System.ArraySegment`1" /> structure and is equal to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000320 RID: 800 RVA: 0x00007DFF File Offset: 0x00005FFF
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is ArraySegment<T> && this.Equals((ArraySegment<T>)obj);
		}

		/// <summary>Determines whether the specified <see cref="T:System.ArraySegment`1" /> structure is equal to the current instance.</summary>
		/// <param name="obj">The structure to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ArraySegment`1" /> structure is equal to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000321 RID: 801 RVA: 0x00007E17 File Offset: 0x00006017
		[__DynamicallyInvokable]
		public bool Equals(ArraySegment<T> obj)
		{
			return obj._array == this._array && obj._offset == this._offset && obj._count == this._count;
		}

		/// <summary>Indicates whether two <see cref="T:System.ArraySegment`1" /> structures are equal.</summary>
		/// <param name="a">The  structure on the left side of the equality operator.</param>
		/// <param name="b">The structure on the right side of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000322 RID: 802 RVA: 0x00007E45 File Offset: 0x00006045
		[__DynamicallyInvokable]
		public static bool operator ==(ArraySegment<T> a, ArraySegment<T> b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.ArraySegment`1" /> structures are unequal.</summary>
		/// <param name="a">The structure on the left side of the inequality operator.</param>
		/// <param name="b">The structure on the right side of the inequality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000323 RID: 803 RVA: 0x00007E4F File Offset: 0x0000604F
		[__DynamicallyInvokable]
		public static bool operator !=(ArraySegment<T> a, ArraySegment<T> b)
		{
			return !(a == b);
		}

		// Token: 0x1700003B RID: 59
		[__DynamicallyInvokable]
		T IList<T>.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._array == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
				}
				if (index < 0 || index >= this._count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._array[this._offset + index];
			}
			[__DynamicallyInvokable]
			set
			{
				if (this._array == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
				}
				if (index < 0 || index >= this._count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this._array[this._offset + index] = value;
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00007F00 File Offset: 0x00006100
		[__DynamicallyInvokable]
		int IList<T>.IndexOf(T item)
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			int num = System.Array.IndexOf<T>(this._array, item, this._offset, this._count);
			if (num < 0)
			{
				return -1;
			}
			return num - this._offset;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00007F4C File Offset: 0x0000614C
		[__DynamicallyInvokable]
		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00007F53 File Offset: 0x00006153
		[__DynamicallyInvokable]
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700003C RID: 60
		[__DynamicallyInvokable]
		T IReadOnlyList<T>.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._array == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
				}
				if (index < 0 || index >= this._count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._array[this._offset + index];
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00007FAC File Offset: 0x000061AC
		[__DynamicallyInvokable]
		bool ICollection<T>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00007FAF File Offset: 0x000061AF
		[__DynamicallyInvokable]
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00007FB6 File Offset: 0x000061B6
		[__DynamicallyInvokable]
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00007FC0 File Offset: 0x000061C0
		[__DynamicallyInvokable]
		bool ICollection<T>.Contains(T item)
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			int num = System.Array.IndexOf<T>(this._array, item, this._offset, this._count);
			return num >= 0;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00008005 File Offset: 0x00006205
		[__DynamicallyInvokable]
		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			System.Array.Copy(this._array, this._offset, array, arrayIndex, this._count);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008038 File Offset: 0x00006238
		[__DynamicallyInvokable]
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000803F File Offset: 0x0000623F
		[__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			return new ArraySegment<T>.ArraySegmentEnumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through an array segment.</summary>
		/// <returns>An enumerator that can be used to iterate through the array segment.</returns>
		// Token: 0x06000331 RID: 817 RVA: 0x00008064 File Offset: 0x00006264
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			return new ArraySegment<T>.ArraySegmentEnumerator(this);
		}

		// Token: 0x040001F0 RID: 496
		private T[] _array;

		// Token: 0x040001F1 RID: 497
		private int _offset;

		// Token: 0x040001F2 RID: 498
		private int _count;

		// Token: 0x02000AC4 RID: 2756
		[Serializable]
		private sealed class ArraySegmentEnumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060069DC RID: 27100 RVA: 0x0016E248 File Offset: 0x0016C448
			internal ArraySegmentEnumerator(ArraySegment<T> arraySegment)
			{
				this._array = arraySegment._array;
				this._start = arraySegment._offset;
				this._end = this._start + arraySegment._count;
				this._current = this._start - 1;
			}

			// Token: 0x060069DD RID: 27101 RVA: 0x0016E294 File Offset: 0x0016C494
			public bool MoveNext()
			{
				if (this._current < this._end)
				{
					this._current++;
					return this._current < this._end;
				}
				return false;
			}

			// Token: 0x170011EB RID: 4587
			// (get) Token: 0x060069DE RID: 27102 RVA: 0x0016E2C4 File Offset: 0x0016C4C4
			public T Current
			{
				get
				{
					if (this._current < this._start)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._current >= this._end)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this._array[this._current];
				}
			}

			// Token: 0x170011EC RID: 4588
			// (get) Token: 0x060069DF RID: 27103 RVA: 0x0016E31E File Offset: 0x0016C51E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060069E0 RID: 27104 RVA: 0x0016E32B File Offset: 0x0016C52B
			void IEnumerator.Reset()
			{
				this._current = this._start - 1;
			}

			// Token: 0x060069E1 RID: 27105 RVA: 0x0016E33B File Offset: 0x0016C53B
			public void Dispose()
			{
			}

			// Token: 0x040030E2 RID: 12514
			private T[] _array;

			// Token: 0x040030E3 RID: 12515
			private int _start;

			// Token: 0x040030E4 RID: 12516
			private int _end;

			// Token: 0x040030E5 RID: 12517
			private int _current;
		}
	}
}
