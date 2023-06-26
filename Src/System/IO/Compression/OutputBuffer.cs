using System;

namespace System.IO.Compression
{
	// Token: 0x02000436 RID: 1078
	internal class OutputBuffer
	{
		// Token: 0x06002854 RID: 10324 RVA: 0x000B9752 File Offset: 0x000B7952
		internal void UpdateBuffer(byte[] output)
		{
			this.byteBuffer = output;
			this.pos = 0;
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x000B9762 File Offset: 0x000B7962
		internal int BytesWritten
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x000B976A File Offset: 0x000B796A
		internal int FreeBytes
		{
			get
			{
				return this.byteBuffer.Length - this.pos;
			}
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000B977C File Offset: 0x000B797C
		internal void WriteUInt16(ushort value)
		{
			byte[] array = this.byteBuffer;
			int num = this.pos;
			this.pos = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.byteBuffer;
			num = this.pos;
			this.pos = num + 1;
			array2[num] = (byte)(value >> 8);
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000B97C0 File Offset: 0x000B79C0
		internal void WriteBits(int n, uint bits)
		{
			this.bitBuf |= bits << this.bitCount;
			this.bitCount += n;
			if (this.bitCount >= 16)
			{
				byte[] array = this.byteBuffer;
				int num = this.pos;
				this.pos = num + 1;
				array[num] = (byte)this.bitBuf;
				byte[] array2 = this.byteBuffer;
				num = this.pos;
				this.pos = num + 1;
				array2[num] = (byte)(this.bitBuf >> 8);
				this.bitCount -= 16;
				this.bitBuf >>= 16;
			}
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000B985C File Offset: 0x000B7A5C
		internal void FlushBits()
		{
			while (this.bitCount >= 8)
			{
				byte[] array = this.byteBuffer;
				int num = this.pos;
				this.pos = num + 1;
				array[num] = (byte)this.bitBuf;
				this.bitCount -= 8;
				this.bitBuf >>= 8;
			}
			if (this.bitCount > 0)
			{
				byte[] array2 = this.byteBuffer;
				int num = this.pos;
				this.pos = num + 1;
				array2[num] = (byte)this.bitBuf;
				this.bitBuf = 0U;
				this.bitCount = 0;
			}
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000B98E5 File Offset: 0x000B7AE5
		internal void WriteBytes(byte[] byteArray, int offset, int count)
		{
			if (this.bitCount == 0)
			{
				Array.Copy(byteArray, offset, this.byteBuffer, this.pos, count);
				this.pos += count;
				return;
			}
			this.WriteBytesUnaligned(byteArray, offset, count);
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000B991C File Offset: 0x000B7B1C
		private void WriteBytesUnaligned(byte[] byteArray, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				byte b = byteArray[offset + i];
				this.WriteByteUnaligned(b);
			}
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x000B9942 File Offset: 0x000B7B42
		private void WriteByteUnaligned(byte b)
		{
			this.WriteBits(8, (uint)b);
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x000B994C File Offset: 0x000B7B4C
		internal int BitsInBuffer
		{
			get
			{
				return this.bitCount / 8 + 1;
			}
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000B9958 File Offset: 0x000B7B58
		internal OutputBuffer.BufferState DumpState()
		{
			OutputBuffer.BufferState bufferState;
			bufferState.pos = this.pos;
			bufferState.bitBuf = this.bitBuf;
			bufferState.bitCount = this.bitCount;
			return bufferState;
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000B998D File Offset: 0x000B7B8D
		internal void RestoreState(OutputBuffer.BufferState state)
		{
			this.pos = state.pos;
			this.bitBuf = state.bitBuf;
			this.bitCount = state.bitCount;
		}

		// Token: 0x0400221B RID: 8731
		private byte[] byteBuffer;

		// Token: 0x0400221C RID: 8732
		private int pos;

		// Token: 0x0400221D RID: 8733
		private uint bitBuf;

		// Token: 0x0400221E RID: 8734
		private int bitCount;

		// Token: 0x0200082B RID: 2091
		internal struct BufferState
		{
			// Token: 0x040035CB RID: 13771
			internal int pos;

			// Token: 0x040035CC RID: 13772
			internal uint bitBuf;

			// Token: 0x040035CD RID: 13773
			internal int bitCount;
		}
	}
}
