using System;

namespace System.Net
{
	// Token: 0x0200014A RID: 330
	internal class SplitWritesState
	{
		// Token: 0x06000B67 RID: 2919 RVA: 0x0003E39F File Offset: 0x0003C59F
		internal SplitWritesState(BufferOffsetSize[] buffers)
		{
			this._UserBuffers = buffers;
			this._LastBufferConsumed = 0;
			this._Index = 0;
			this._RealBuffers = null;
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0003E3C4 File Offset: 0x0003C5C4
		internal bool IsDone
		{
			get
			{
				if (this._LastBufferConsumed != 0)
				{
					return false;
				}
				for (int i = this._Index; i < this._UserBuffers.Length; i++)
				{
					if (this._UserBuffers[i].Size != 0)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0003E408 File Offset: 0x0003C608
		internal BufferOffsetSize[] GetNextBuffers()
		{
			int i = this._Index;
			int num = 0;
			int num2 = 0;
			int num3 = this._LastBufferConsumed;
			while (this._Index < this._UserBuffers.Length)
			{
				num2 = this._UserBuffers[this._Index].Size - this._LastBufferConsumed;
				num += num2;
				if (num > 65536)
				{
					num2 -= num - 65536;
					num = 65536;
					break;
				}
				num2 = 0;
				this._LastBufferConsumed = 0;
				this._Index++;
			}
			if (num == 0)
			{
				return null;
			}
			if (num3 == 0 && i == 0 && this._Index == this._UserBuffers.Length)
			{
				return this._UserBuffers;
			}
			int num4 = ((num2 == 0) ? (this._Index - i) : (this._Index - i + 1));
			if (this._RealBuffers == null || this._RealBuffers.Length != num4)
			{
				this._RealBuffers = new BufferOffsetSize[num4];
			}
			int num5 = 0;
			while (i < this._Index)
			{
				this._RealBuffers[num5++] = new BufferOffsetSize(this._UserBuffers[i].Buffer, this._UserBuffers[i].Offset + num3, this._UserBuffers[i].Size - num3, false);
				num3 = 0;
				i++;
			}
			if (num2 != 0)
			{
				this._RealBuffers[num5] = new BufferOffsetSize(this._UserBuffers[i].Buffer, this._UserBuffers[i].Offset + this._LastBufferConsumed, num2, false);
				if ((this._LastBufferConsumed += num2) == this._UserBuffers[this._Index].Size)
				{
					this._Index++;
					this._LastBufferConsumed = 0;
				}
			}
			return this._RealBuffers;
		}

		// Token: 0x040010E5 RID: 4325
		private const int c_SplitEncryptedBuffersSize = 65536;

		// Token: 0x040010E6 RID: 4326
		private BufferOffsetSize[] _UserBuffers;

		// Token: 0x040010E7 RID: 4327
		private int _Index;

		// Token: 0x040010E8 RID: 4328
		private int _LastBufferConsumed;

		// Token: 0x040010E9 RID: 4329
		private BufferOffsetSize[] _RealBuffers;
	}
}
