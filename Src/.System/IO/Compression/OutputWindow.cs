using System;

namespace System.IO.Compression
{
	// Token: 0x02000437 RID: 1079
	internal class OutputWindow
	{
		// Token: 0x06002861 RID: 10337 RVA: 0x000B99BC File Offset: 0x000B7BBC
		public void Write(byte b)
		{
			byte[] array = this.window;
			int num = this.end;
			this.end = num + 1;
			array[num] = b;
			this.end &= 32767;
			this.bytesUsed++;
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000B9A04 File Offset: 0x000B7C04
		public void WriteLengthDistance(int length, int distance)
		{
			this.bytesUsed += length;
			int num = (this.end - distance) & 32767;
			int num2 = 32768 - length;
			if (num > num2 || this.end >= num2)
			{
				while (length-- > 0)
				{
					byte[] array = this.window;
					int num3 = this.end;
					this.end = num3 + 1;
					array[num3] = this.window[num++];
					this.end &= 32767;
					num &= 32767;
				}
				return;
			}
			if (length <= distance)
			{
				Array.Copy(this.window, num, this.window, this.end, length);
				this.end += length;
				return;
			}
			while (length-- > 0)
			{
				byte[] array2 = this.window;
				int num3 = this.end;
				this.end = num3 + 1;
				array2[num3] = this.window[num++];
			}
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000B9AEC File Offset: 0x000B7CEC
		public int CopyFrom(InputBuffer input, int length)
		{
			length = Math.Min(Math.Min(length, 32768 - this.bytesUsed), input.AvailableBytes);
			int num = 32768 - this.end;
			int num2;
			if (length > num)
			{
				num2 = input.CopyTo(this.window, this.end, num);
				if (num2 == num)
				{
					num2 += input.CopyTo(this.window, 0, length - num);
				}
			}
			else
			{
				num2 = input.CopyTo(this.window, this.end, length);
			}
			this.end = (this.end + num2) & 32767;
			this.bytesUsed += num2;
			return num2;
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x000B9B8D File Offset: 0x000B7D8D
		public int FreeBytes
		{
			get
			{
				return 32768 - this.bytesUsed;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x000B9B9B File Offset: 0x000B7D9B
		public int AvailableBytes
		{
			get
			{
				return this.bytesUsed;
			}
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000B9BA4 File Offset: 0x000B7DA4
		public int CopyTo(byte[] output, int offset, int length)
		{
			int num;
			if (length > this.bytesUsed)
			{
				num = this.end;
				length = this.bytesUsed;
			}
			else
			{
				num = (this.end - this.bytesUsed + length) & 32767;
			}
			int num2 = length;
			int num3 = length - num;
			if (num3 > 0)
			{
				Array.Copy(this.window, 32768 - num3, output, offset, num3);
				offset += num3;
				length = num;
			}
			Array.Copy(this.window, num - length, output, offset, length);
			this.bytesUsed -= num2;
			return num2;
		}

		// Token: 0x0400221F RID: 8735
		private const int WindowSize = 32768;

		// Token: 0x04002220 RID: 8736
		private const int WindowMask = 32767;

		// Token: 0x04002221 RID: 8737
		private byte[] window = new byte[32768];

		// Token: 0x04002222 RID: 8738
		private int end;

		// Token: 0x04002223 RID: 8739
		private int bytesUsed;
	}
}
