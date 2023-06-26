using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D1 RID: 721
	internal struct ReadOnlySpan<T>
	{
		// Token: 0x0600258E RID: 9614 RVA: 0x0008A513 File Offset: 0x00088713
		public ReadOnlySpan(ArraySegment<T> segment)
		{
			this._Segment = segment;
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x0008A51C File Offset: 0x0008871C
		public ReadOnlySpan(T[] array, int offset, int count)
		{
			this = new ReadOnlySpan<T>((array != null || offset != 0 || count != 0) ? new ArraySegment<T>(array, offset, count) : default(ArraySegment<T>));
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x0008A54C File Offset: 0x0008874C
		public ReadOnlySpan(T[] array)
		{
			this = new ReadOnlySpan<T>((array != null) ? new ArraySegment<T>(array) : default(ArraySegment<T>));
		}

		// Token: 0x170004A1 RID: 1185
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this._Segment.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._Segment.Array[index + this._Segment.Offset];
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06002592 RID: 9618 RVA: 0x0008A5AF File Offset: 0x000887AF
		public bool IsEmpty
		{
			get
			{
				return this._Segment.Count == 0;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x0008A5BF File Offset: 0x000887BF
		public bool IsNull
		{
			get
			{
				return this._Segment.Array == null;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06002594 RID: 9620 RVA: 0x0008A5CF File Offset: 0x000887CF
		public int Length
		{
			get
			{
				return this._Segment.Count;
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x0008A5DC File Offset: 0x000887DC
		public ReadOnlySpan<T> Slice(int start)
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new ReadOnlySpan<T>(this._Segment.Array, this._Segment.Offset + start, this._Segment.Count - start);
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x0008A612 File Offset: 0x00088812
		public ReadOnlySpan<T> Slice(int start, int length)
		{
			if (start < 0)
			{
				throw new InvalidOperationException();
			}
			if (length > this._Segment.Count - start)
			{
				throw new InvalidOperationException();
			}
			return new ReadOnlySpan<T>(this._Segment.Array, this._Segment.Offset + start, length);
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x0008A654 File Offset: 0x00088854
		public T[] ToArray()
		{
			T[] array = new T[this._Segment.Count];
			if (!this.IsEmpty)
			{
				Array.Copy(this._Segment.Array, this._Segment.Offset, array, 0, this._Segment.Count);
			}
			return array;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x0008A6A4 File Offset: 0x000888A4
		public void CopyTo(Span<T> destination)
		{
			if (destination.Length < this.Length)
			{
				throw new InvalidOperationException("Destination too short");
			}
			if (!this.IsEmpty)
			{
				ArraySegment<T> arraySegment = destination.DangerousGetArraySegment();
				Array.Copy(this._Segment.Array, this._Segment.Offset, arraySegment.Array, arraySegment.Offset, this._Segment.Count);
			}
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0008A710 File Offset: 0x00088910
		public bool Overlaps(ReadOnlySpan<T> destination)
		{
			int num;
			return this.Overlaps(destination, out num);
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x0008A728 File Offset: 0x00088928
		public bool Overlaps(ReadOnlySpan<T> destination, out int elementOffset)
		{
			elementOffset = 0;
			if (this.IsEmpty || destination.IsEmpty)
			{
				return false;
			}
			if (this._Segment.Array != destination._Segment.Array)
			{
				return false;
			}
			elementOffset = destination._Segment.Offset - this._Segment.Offset;
			if (elementOffset >= this._Segment.Count || elementOffset <= -destination._Segment.Count)
			{
				elementOffset = 0;
				return false;
			}
			return true;
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x0008A7A6 File Offset: 0x000889A6
		public ArraySegment<T> DangerousGetArraySegment()
		{
			return this._Segment;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x0008A7AE File Offset: 0x000889AE
		public static implicit operator ReadOnlySpan<T>(T[] array)
		{
			return new ReadOnlySpan<T>(array);
		}

		// Token: 0x04000E2B RID: 3627
		public static readonly Span<T> Empty;

		// Token: 0x04000E2C RID: 3628
		private ArraySegment<T> _Segment;
	}
}
