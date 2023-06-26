using System;

namespace System.IO.Compression
{
	// Token: 0x02000428 RID: 1064
	internal class FastEncoder
	{
		// Token: 0x060027DC RID: 10204 RVA: 0x000B7307 File Offset: 0x000B5507
		public FastEncoder()
		{
			this.inputWindow = new FastEncoderWindow();
			this.currentMatch = new Match();
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x060027DD RID: 10205 RVA: 0x000B7325 File Offset: 0x000B5525
		internal int BytesInHistory
		{
			get
			{
				return this.inputWindow.BytesAvailable;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x000B7332 File Offset: 0x000B5532
		internal DeflateInput UnprocessedInput
		{
			get
			{
				return this.inputWindow.UnprocessedInput;
			}
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000B733F File Offset: 0x000B553F
		internal void FlushInput()
		{
			this.inputWindow.FlushWindow();
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x000B734C File Offset: 0x000B554C
		internal double LastCompressionRatio
		{
			get
			{
				return this.lastCompressionRatio;
			}
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000B7354 File Offset: 0x000B5554
		internal void GetBlock(DeflateInput input, OutputBuffer output, int maxBytesToCopy)
		{
			FastEncoder.WriteDeflatePreamble(output);
			this.GetCompressedOutput(input, output, maxBytesToCopy);
			this.WriteEndOfBlock(output);
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000B736C File Offset: 0x000B556C
		internal void GetCompressedData(DeflateInput input, OutputBuffer output)
		{
			this.GetCompressedOutput(input, output, -1);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000B7377 File Offset: 0x000B5577
		internal void GetBlockHeader(OutputBuffer output)
		{
			FastEncoder.WriteDeflatePreamble(output);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000B737F File Offset: 0x000B557F
		internal void GetBlockFooter(OutputBuffer output)
		{
			this.WriteEndOfBlock(output);
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000B7388 File Offset: 0x000B5588
		private void GetCompressedOutput(DeflateInput input, OutputBuffer output, int maxBytesToCopy)
		{
			int bytesWritten = output.BytesWritten;
			int num = 0;
			int num2 = this.BytesInHistory + input.Count;
			do
			{
				int num3 = ((input.Count < this.inputWindow.FreeWindowSpace) ? input.Count : this.inputWindow.FreeWindowSpace);
				if (maxBytesToCopy >= 1)
				{
					num3 = Math.Min(num3, maxBytesToCopy - num);
				}
				if (num3 > 0)
				{
					this.inputWindow.CopyBytes(input.Buffer, input.StartIndex, num3);
					input.ConsumeBytes(num3);
					num += num3;
				}
				this.GetCompressedOutput(output);
			}
			while (this.SafeToWriteTo(output) && this.InputAvailable(input) && (maxBytesToCopy < 1 || num < maxBytesToCopy));
			int bytesWritten2 = output.BytesWritten;
			int num4 = bytesWritten2 - bytesWritten;
			int num5 = this.BytesInHistory + input.Count;
			int num6 = num2 - num5;
			if (num4 != 0)
			{
				this.lastCompressionRatio = (double)num4 / (double)num6;
			}
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000B7468 File Offset: 0x000B5668
		private void GetCompressedOutput(OutputBuffer output)
		{
			while (this.inputWindow.BytesAvailable > 0 && this.SafeToWriteTo(output))
			{
				this.inputWindow.GetNextSymbolOrMatch(this.currentMatch);
				if (this.currentMatch.State == MatchState.HasSymbol)
				{
					FastEncoder.WriteChar(this.currentMatch.Symbol, output);
				}
				else if (this.currentMatch.State == MatchState.HasMatch)
				{
					FastEncoder.WriteMatch(this.currentMatch.Length, this.currentMatch.Position, output);
				}
				else
				{
					FastEncoder.WriteChar(this.currentMatch.Symbol, output);
					FastEncoder.WriteMatch(this.currentMatch.Length, this.currentMatch.Position, output);
				}
			}
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000B7520 File Offset: 0x000B5720
		private bool InputAvailable(DeflateInput input)
		{
			return input.Count > 0 || this.BytesInHistory > 0;
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000B7536 File Offset: 0x000B5736
		private bool SafeToWriteTo(OutputBuffer output)
		{
			return output.FreeBytes > 16;
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x000B7544 File Offset: 0x000B5744
		private void WriteEndOfBlock(OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[256];
			int num2 = (int)(num & 31U);
			output.WriteBits(num2, num >> 5);
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000B756C File Offset: 0x000B576C
		internal static void WriteMatch(int matchLen, int matchPos, OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[254 + matchLen];
			int num2 = (int)(num & 31U);
			if (num2 <= 16)
			{
				output.WriteBits(num2, num >> 5);
			}
			else
			{
				output.WriteBits(16, (num >> 5) & 65535U);
				output.WriteBits(num2 - 16, num >> 21);
			}
			num = FastEncoderStatics.FastEncoderDistanceCodeInfo[FastEncoderStatics.GetSlot(matchPos)];
			output.WriteBits((int)(num & 15U), num >> 8);
			int num3 = (int)((num >> 4) & 15U);
			if (num3 != 0)
			{
				output.WriteBits(num3, (uint)(matchPos & (int)FastEncoderStatics.BitMask[num3]));
			}
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000B75F0 File Offset: 0x000B57F0
		internal static void WriteChar(byte b, OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[(int)b];
			output.WriteBits((int)(num & 31U), num >> 5);
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000B7612 File Offset: 0x000B5812
		internal static void WriteDeflatePreamble(OutputBuffer output)
		{
			output.WriteBytes(FastEncoderStatics.FastEncoderTreeStructureData, 0, FastEncoderStatics.FastEncoderTreeStructureData.Length);
			output.WriteBits(9, 34U);
		}

		// Token: 0x04002196 RID: 8598
		private FastEncoderWindow inputWindow;

		// Token: 0x04002197 RID: 8599
		private Match currentMatch;

		// Token: 0x04002198 RID: 8600
		private double lastCompressionRatio;
	}
}
