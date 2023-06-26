using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D3 RID: 723
	internal struct ReadOnlyMemory<T>
	{
		// Token: 0x060025B1 RID: 9649 RVA: 0x0008AACE File Offset: 0x00088CCE
		public ReadOnlyMemory(ArraySegment<T> segment)
		{
			this._Segment = segment;
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x0008AAD8 File Offset: 0x00088CD8
		public ReadOnlyMemory(T[] array, int offset, int count)
		{
			this = new ReadOnlyMemory<T>((array != null || offset != 0 || count != 0) ? new ArraySegment<T>(array, offset, count) : default(ArraySegment<T>));
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x0008AB08 File Offset: 0x00088D08
		public ReadOnlyMemory(T[] array)
		{
			this = new ReadOnlyMemory<T>((array != null) ? new ArraySegment<T>(array) : default(ArraySegment<T>));
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x0008AB30 File Offset: 0x00088D30
		public bool IsEmpty
		{
			get
			{
				return this._Segment.Count == 0;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x0008AB50 File Offset: 0x00088D50
		public int Length
		{
			get
			{
				return this._Segment.Count;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x0008AB6B File Offset: 0x00088D6B
		public ReadOnlySpan<T> Span
		{
			get
			{
				return new ReadOnlySpan<T>(this._Segment);
			}
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x0008AB78 File Offset: 0x00088D78
		public ReadOnlyMemory<T> Slice(int start)
		{
			if (start < 0)
			{
				throw new InvalidOperationException();
			}
			return new ReadOnlyMemory<T>(this._Segment.Array, this._Segment.Offset + start, this._Segment.Count - start);
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x0008ABC4 File Offset: 0x00088DC4
		public ReadOnlyMemory<T> Slice(int start, int length)
		{
			if (start < 0)
			{
				throw new InvalidOperationException();
			}
			if (length > this._Segment.Count - start)
			{
				throw new InvalidOperationException();
			}
			return new ReadOnlyMemory<T>(this._Segment.Array, this._Segment.Offset + start, length);
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x0008AC18 File Offset: 0x00088E18
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

		// Token: 0x060025BA RID: 9658 RVA: 0x0008AC8C File Offset: 0x00088E8C
		public static implicit operator ReadOnlyMemory<T>(T[] array)
		{
			return new ReadOnlyMemory<T>(array);
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x0008AC94 File Offset: 0x00088E94
		public static implicit operator ArraySegment<T>(ReadOnlyMemory<T> memory)
		{
			return memory._Segment;
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x0008AC9C File Offset: 0x00088E9C
		public static implicit operator ReadOnlyMemory<T>(ArraySegment<T> segment)
		{
			return new ReadOnlyMemory<T>(segment);
		}

		// Token: 0x04000E2F RID: 3631
		private readonly ArraySegment<T> _Segment;
	}
}
