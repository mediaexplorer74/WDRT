using System;

namespace System.IO.Compression
{
	// Token: 0x02000421 RID: 1057
	internal class DeflaterManaged : IDeflater, IDisposable
	{
		// Token: 0x06002771 RID: 10097 RVA: 0x000B5812 File Offset: 0x000B3A12
		internal DeflaterManaged()
		{
			this.deflateEncoder = new FastEncoder();
			this.copyEncoder = new CopyEncoder();
			this.input = new DeflateInput();
			this.output = new OutputBuffer();
			this.processingState = DeflaterManaged.DeflaterState.NotStarted;
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000B584D File Offset: 0x000B3A4D
		private bool NeedsInput()
		{
			return ((IDeflater)this).NeedsInput();
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000B5855 File Offset: 0x000B3A55
		bool IDeflater.NeedsInput()
		{
			return this.input.Count == 0 && this.deflateEncoder.BytesInHistory == 0;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000B5874 File Offset: 0x000B3A74
		void IDeflater.SetInput(byte[] inputBuffer, int startIndex, int count)
		{
			this.input.Buffer = inputBuffer;
			this.input.Count = count;
			this.input.StartIndex = startIndex;
			if (count > 0 && count < 256)
			{
				DeflaterManaged.DeflaterState deflaterState = this.processingState;
				if (deflaterState != DeflaterManaged.DeflaterState.NotStarted)
				{
					if (deflaterState == DeflaterManaged.DeflaterState.CompressThenCheck)
					{
						this.processingState = DeflaterManaged.DeflaterState.HandlingSmallData;
						return;
					}
					if (deflaterState != DeflaterManaged.DeflaterState.CheckingForIncompressible)
					{
						return;
					}
				}
				this.processingState = DeflaterManaged.DeflaterState.StartingSmallData;
				return;
			}
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000B58D4 File Offset: 0x000B3AD4
		int IDeflater.GetDeflateOutput(byte[] outputBuffer)
		{
			this.output.UpdateBuffer(outputBuffer);
			switch (this.processingState)
			{
			case DeflaterManaged.DeflaterState.NotStarted:
			{
				DeflateInput.InputState inputState = this.input.DumpState();
				OutputBuffer.BufferState bufferState = this.output.DumpState();
				this.deflateEncoder.GetBlockHeader(this.output);
				this.deflateEncoder.GetCompressedData(this.input, this.output);
				if (!this.UseCompressed(this.deflateEncoder.LastCompressionRatio))
				{
					this.input.RestoreState(inputState);
					this.output.RestoreState(bufferState);
					this.copyEncoder.GetBlock(this.input, this.output, false);
					this.FlushInputWindows();
					this.processingState = DeflaterManaged.DeflaterState.CheckingForIncompressible;
					goto IL_23A;
				}
				this.processingState = DeflaterManaged.DeflaterState.CompressThenCheck;
				goto IL_23A;
			}
			case DeflaterManaged.DeflaterState.SlowDownForIncompressible1:
				this.deflateEncoder.GetBlockFooter(this.output);
				this.processingState = DeflaterManaged.DeflaterState.SlowDownForIncompressible2;
				break;
			case DeflaterManaged.DeflaterState.SlowDownForIncompressible2:
				break;
			case DeflaterManaged.DeflaterState.StartingSmallData:
				this.deflateEncoder.GetBlockHeader(this.output);
				this.processingState = DeflaterManaged.DeflaterState.HandlingSmallData;
				goto IL_223;
			case DeflaterManaged.DeflaterState.CompressThenCheck:
				this.deflateEncoder.GetCompressedData(this.input, this.output);
				if (!this.UseCompressed(this.deflateEncoder.LastCompressionRatio))
				{
					this.processingState = DeflaterManaged.DeflaterState.SlowDownForIncompressible1;
					this.inputFromHistory = this.deflateEncoder.UnprocessedInput;
					goto IL_23A;
				}
				goto IL_23A;
			case DeflaterManaged.DeflaterState.CheckingForIncompressible:
			{
				DeflateInput.InputState inputState2 = this.input.DumpState();
				OutputBuffer.BufferState bufferState2 = this.output.DumpState();
				this.deflateEncoder.GetBlock(this.input, this.output, 8072);
				if (!this.UseCompressed(this.deflateEncoder.LastCompressionRatio))
				{
					this.input.RestoreState(inputState2);
					this.output.RestoreState(bufferState2);
					this.copyEncoder.GetBlock(this.input, this.output, false);
					this.FlushInputWindows();
					goto IL_23A;
				}
				goto IL_23A;
			}
			case DeflaterManaged.DeflaterState.HandlingSmallData:
				goto IL_223;
			default:
				goto IL_23A;
			}
			if (this.inputFromHistory.Count > 0)
			{
				this.copyEncoder.GetBlock(this.inputFromHistory, this.output, false);
			}
			if (this.inputFromHistory.Count == 0)
			{
				this.deflateEncoder.FlushInput();
				this.processingState = DeflaterManaged.DeflaterState.CheckingForIncompressible;
				goto IL_23A;
			}
			goto IL_23A;
			IL_223:
			this.deflateEncoder.GetCompressedData(this.input, this.output);
			IL_23A:
			return this.output.BytesWritten;
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000B5B28 File Offset: 0x000B3D28
		bool IDeflater.Finish(byte[] outputBuffer, out int bytesRead)
		{
			if (this.processingState == DeflaterManaged.DeflaterState.NotStarted)
			{
				bytesRead = 0;
				return true;
			}
			this.output.UpdateBuffer(outputBuffer);
			if (this.processingState == DeflaterManaged.DeflaterState.CompressThenCheck || this.processingState == DeflaterManaged.DeflaterState.HandlingSmallData || this.processingState == DeflaterManaged.DeflaterState.SlowDownForIncompressible1)
			{
				this.deflateEncoder.GetBlockFooter(this.output);
			}
			this.WriteFinal();
			bytesRead = this.output.BytesWritten;
			return true;
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000B5B8E File Offset: 0x000B3D8E
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000B5B90 File Offset: 0x000B3D90
		protected void Dispose(bool disposing)
		{
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000B5B92 File Offset: 0x000B3D92
		private bool UseCompressed(double ratio)
		{
			return ratio <= 1.0;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000B5BA3 File Offset: 0x000B3DA3
		private void FlushInputWindows()
		{
			this.deflateEncoder.FlushInput();
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000B5BB0 File Offset: 0x000B3DB0
		private void WriteFinal()
		{
			this.copyEncoder.GetBlock(null, this.output, true);
		}

		// Token: 0x0400215C RID: 8540
		private const int MinBlockSize = 256;

		// Token: 0x0400215D RID: 8541
		private const int MaxHeaderFooterGoo = 120;

		// Token: 0x0400215E RID: 8542
		private const int CleanCopySize = 8072;

		// Token: 0x0400215F RID: 8543
		private const double BadCompressionThreshold = 1.0;

		// Token: 0x04002160 RID: 8544
		private FastEncoder deflateEncoder;

		// Token: 0x04002161 RID: 8545
		private CopyEncoder copyEncoder;

		// Token: 0x04002162 RID: 8546
		private DeflateInput input;

		// Token: 0x04002163 RID: 8547
		private OutputBuffer output;

		// Token: 0x04002164 RID: 8548
		private DeflaterManaged.DeflaterState processingState;

		// Token: 0x04002165 RID: 8549
		private DeflateInput inputFromHistory;

		// Token: 0x02000816 RID: 2070
		private enum DeflaterState
		{
			// Token: 0x0400357A RID: 13690
			NotStarted,
			// Token: 0x0400357B RID: 13691
			SlowDownForIncompressible1,
			// Token: 0x0400357C RID: 13692
			SlowDownForIncompressible2,
			// Token: 0x0400357D RID: 13693
			StartingSmallData,
			// Token: 0x0400357E RID: 13694
			CompressThenCheck,
			// Token: 0x0400357F RID: 13695
			CheckingForIncompressible,
			// Token: 0x04003580 RID: 13696
			HandlingSmallData
		}
	}
}
