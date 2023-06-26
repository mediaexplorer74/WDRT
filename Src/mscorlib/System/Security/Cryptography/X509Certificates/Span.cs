using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D2 RID: 722
	internal struct Span<T>
	{
		// Token: 0x0600259E RID: 9630 RVA: 0x0008A7B8 File Offset: 0x000889B8
		public Span(ArraySegment<T> segment)
		{
			this._Segment = segment;
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x0008A7C4 File Offset: 0x000889C4
		public Span(T[] array, int offset, int count)
		{
			this = new Span<T>((array != null || offset != 0 || count != 0) ? new ArraySegment<T>(array, offset, count) : default(ArraySegment<T>));
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x0008A7F4 File Offset: 0x000889F4
		public Span(T[] array)
		{
			this = new Span<T>((array != null) ? new ArraySegment<T>(array) : default(ArraySegment<T>));
		}

		// Token: 0x170004A5 RID: 1189
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
			set
			{
				if (index < 0 || index >= this._Segment.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this._Segment.Array[index + this._Segment.Offset] = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x0008A894 File Offset: 0x00088A94
		public bool IsEmpty
		{
			get
			{
				return this._Segment.Count == 0;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x0008A8A4 File Offset: 0x00088AA4
		public int Length
		{
			get
			{
				return this._Segment.Count;
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x0008A8B1 File Offset: 0x00088AB1
		public Span<T> Slice(int start)
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new Span<T>(this._Segment.Array, this._Segment.Offset + start, this._Segment.Count - start);
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x0008A8E7 File Offset: 0x00088AE7
		public Span<T> Slice(int start, int length)
		{
			if (start < 0 || length > this._Segment.Count - start)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new Span<T>(this._Segment.Array, this._Segment.Offset + start, length);
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x0008A924 File Offset: 0x00088B24
		public void Fill(T value)
		{
			for (int i = this._Segment.Offset; i < this._Segment.Count - this._Segment.Offset; i++)
			{
				this._Segment.Array[i] = value;
			}
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x0008A970 File Offset: 0x00088B70
		public void Clear()
		{
			for (int i = this._Segment.Offset; i < this._Segment.Count - this._Segment.Offset; i++)
			{
				this._Segment.Array[i] = default(T);
			}
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x0008A9C4 File Offset: 0x00088BC4
		public T[] ToArray()
		{
			T[] array = new T[this._Segment.Count];
			if (!this.IsEmpty)
			{
				Array.Copy(this._Segment.Array, this._Segment.Offset, array, 0, this._Segment.Count);
			}
			return array;
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x0008AA14 File Offset: 0x00088C14
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

		// Token: 0x060025AB RID: 9643 RVA: 0x0008AA80 File Offset: 0x00088C80
		public bool Overlaps(ReadOnlySpan<T> destination, out int elementOffset)
		{
			return this.Overlaps(destination, out elementOffset);
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x0008AAA2 File Offset: 0x00088CA2
		public ArraySegment<T> DangerousGetArraySegment()
		{
			return this._Segment;
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x0008AAAA File Offset: 0x00088CAA
		public static implicit operator Span<T>(T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x0008AAB2 File Offset: 0x00088CB2
		public static implicit operator ReadOnlySpan<T>(Span<T> span)
		{
			return new ReadOnlySpan<T>(span._Segment);
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x0008AABF File Offset: 0x00088CBF
		public T[] DangerousGetArrayForPinning()
		{
			return this._Segment.Array;
		}

		// Token: 0x04000E2D RID: 3629
		public static readonly Span<T> Empty;

		// Token: 0x04000E2E RID: 3630
		private ArraySegment<T> _Segment;
	}
}
