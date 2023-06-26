using System;

namespace System.IO.Compression
{
	// Token: 0x02000434 RID: 1076
	internal class InputBuffer
	{
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600283F RID: 10303 RVA: 0x000B93BC File Offset: 0x000B75BC
		public int AvailableBits
		{
			get
			{
				return this.bitsInBuffer;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002840 RID: 10304 RVA: 0x000B93C4 File Offset: 0x000B75C4
		public int AvailableBytes
		{
			get
			{
				return this.end - this.start + this.bitsInBuffer / 8;
			}
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000B93DC File Offset: 0x000B75DC
		public bool EnsureBitsAvailable(int count)
		{
			if (this.bitsInBuffer < count)
			{
				if (this.NeedsInput())
				{
					return false;
				}
				uint num = this.bitBuffer;
				byte[] array = this.buffer;
				int num2 = this.start;
				this.start = num2 + 1;
				this.bitBuffer = num | (array[num2] << (this.bitsInBuffer & 31));
				this.bitsInBuffer += 8;
				if (this.bitsInBuffer < count)
				{
					if (this.NeedsInput())
					{
						return false;
					}
					uint num3 = this.bitBuffer;
					byte[] array2 = this.buffer;
					num2 = this.start;
					this.start = num2 + 1;
					this.bitBuffer = num3 | (array2[num2] << (this.bitsInBuffer & 31));
					this.bitsInBuffer += 8;
				}
			}
			return true;
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000B9490 File Offset: 0x000B7690
		public uint TryLoad16Bits()
		{
			if (this.bitsInBuffer < 8)
			{
				if (this.start < this.end)
				{
					uint num = this.bitBuffer;
					byte[] array = this.buffer;
					int num2 = this.start;
					this.start = num2 + 1;
					this.bitBuffer = num | (array[num2] << (this.bitsInBuffer & 31));
					this.bitsInBuffer += 8;
				}
				if (this.start < this.end)
				{
					uint num3 = this.bitBuffer;
					byte[] array2 = this.buffer;
					int num2 = this.start;
					this.start = num2 + 1;
					this.bitBuffer = num3 | (array2[num2] << (this.bitsInBuffer & 31));
					this.bitsInBuffer += 8;
				}
			}
			else if (this.bitsInBuffer < 16 && this.start < this.end)
			{
				uint num4 = this.bitBuffer;
				byte[] array3 = this.buffer;
				int num2 = this.start;
				this.start = num2 + 1;
				this.bitBuffer = num4 | (array3[num2] << (this.bitsInBuffer & 31));
				this.bitsInBuffer += 8;
			}
			return this.bitBuffer;
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000B959F File Offset: 0x000B779F
		private uint GetBitMask(int count)
		{
			return (1U << count) - 1U;
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000B95AC File Offset: 0x000B77AC
		public int GetBits(int count)
		{
			if (!this.EnsureBitsAvailable(count))
			{
				return -1;
			}
			int num = (int)(this.bitBuffer & this.GetBitMask(count));
			this.bitBuffer >>= count;
			this.bitsInBuffer -= count;
			return num;
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000B95F4 File Offset: 0x000B77F4
		public int CopyTo(byte[] output, int offset, int length)
		{
			int num = 0;
			while (this.bitsInBuffer > 0 && length > 0)
			{
				output[offset++] = (byte)this.bitBuffer;
				this.bitBuffer >>= 8;
				this.bitsInBuffer -= 8;
				length--;
				num++;
			}
			if (length == 0)
			{
				return num;
			}
			int num2 = this.end - this.start;
			if (length > num2)
			{
				length = num2;
			}
			Array.Copy(this.buffer, this.start, output, offset, length);
			this.start += length;
			return num + length;
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000B9685 File Offset: 0x000B7885
		public bool NeedsInput()
		{
			return this.start == this.end;
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000B9695 File Offset: 0x000B7895
		public void SetInput(byte[] buffer, int offset, int length)
		{
			this.buffer = buffer;
			this.start = offset;
			this.end = offset + length;
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000B96AE File Offset: 0x000B78AE
		public void SkipBits(int n)
		{
			this.bitBuffer >>= n;
			this.bitsInBuffer -= n;
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000B96CF File Offset: 0x000B78CF
		public void SkipToByteBoundary()
		{
			this.bitBuffer >>= this.bitsInBuffer % 8;
			this.bitsInBuffer -= this.bitsInBuffer % 8;
		}

		// Token: 0x04002212 RID: 8722
		private byte[] buffer;

		// Token: 0x04002213 RID: 8723
		private int start;

		// Token: 0x04002214 RID: 8724
		private int end;

		// Token: 0x04002215 RID: 8725
		private uint bitBuffer;

		// Token: 0x04002216 RID: 8726
		private int bitsInBuffer;
	}
}
