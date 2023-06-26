using System;

namespace System.IO.Compression
{
	// Token: 0x0200041C RID: 1052
	internal class CopyEncoder
	{
		// Token: 0x06002759 RID: 10073 RVA: 0x000B5680 File Offset: 0x000B3880
		public void GetBlock(DeflateInput input, OutputBuffer output, bool isFinal)
		{
			int num = 0;
			if (input != null)
			{
				num = Math.Min(input.Count, output.FreeBytes - 5 - output.BitsInBuffer);
				if (num > 65531)
				{
					num = 65531;
				}
			}
			if (isFinal)
			{
				output.WriteBits(3, 1U);
			}
			else
			{
				output.WriteBits(3, 0U);
			}
			output.FlushBits();
			this.WriteLenNLen((ushort)num, output);
			if (input != null && num > 0)
			{
				output.WriteBytes(input.Buffer, input.StartIndex, num);
				input.ConsumeBytes(num);
			}
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000B5700 File Offset: 0x000B3900
		private void WriteLenNLen(ushort len, OutputBuffer output)
		{
			output.WriteUInt16(len);
			ushort num = ~len;
			output.WriteUInt16(num);
		}

		// Token: 0x04002156 RID: 8534
		private const int PaddingSize = 5;

		// Token: 0x04002157 RID: 8535
		private const int MaxUncompressedBlockSize = 65536;
	}
}
