using System;

namespace System.IO.Compression
{
	// Token: 0x0200042D RID: 1069
	internal class GZipDecoder : IFileFormatReader
	{
		// Token: 0x06002806 RID: 10246 RVA: 0x000B7D2C File Offset: 0x000B5F2C
		public GZipDecoder()
		{
			this.Reset();
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000B7D3A File Offset: 0x000B5F3A
		public void Reset()
		{
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingID1;
			this.gzipFooterSubstate = GZipDecoder.GzipHeaderState.ReadingCRC;
			this.expectedCrc32 = 0U;
			this.expectedOutputStreamSizeModulo = 0U;
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x000B7D5C File Offset: 0x000B5F5C
		public bool ReadHeader(InputBuffer input)
		{
			int num;
			switch (this.gzipHeaderSubstate)
			{
			case GZipDecoder.GzipHeaderState.ReadingID1:
				num = input.GetBits(8);
				if (num < 0)
				{
					return false;
				}
				if (num != 31)
				{
					throw new InvalidDataException(SR.GetString("CorruptedGZipHeader"));
				}
				this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingID2;
				break;
			case GZipDecoder.GzipHeaderState.ReadingID2:
				break;
			case GZipDecoder.GzipHeaderState.ReadingCM:
				goto IL_A5;
			case GZipDecoder.GzipHeaderState.ReadingFLG:
				goto IL_CE;
			case GZipDecoder.GzipHeaderState.ReadingMMTime:
				goto IL_F1;
			case GZipDecoder.GzipHeaderState.ReadingXFL:
				goto IL_128;
			case GZipDecoder.GzipHeaderState.ReadingOS:
				goto IL_13D;
			case GZipDecoder.GzipHeaderState.ReadingXLen1:
				goto IL_152;
			case GZipDecoder.GzipHeaderState.ReadingXLen2:
				goto IL_17B;
			case GZipDecoder.GzipHeaderState.ReadingXLenData:
				goto IL_1A8;
			case GZipDecoder.GzipHeaderState.ReadingFileName:
				goto IL_1E5;
			case GZipDecoder.GzipHeaderState.ReadingComment:
				goto IL_212;
			case GZipDecoder.GzipHeaderState.ReadingCRC16Part1:
				goto IL_240;
			case GZipDecoder.GzipHeaderState.ReadingCRC16Part2:
				goto IL_26A;
			case GZipDecoder.GzipHeaderState.Done:
				return true;
			default:
				throw new InvalidDataException(SR.GetString("UnknownState"));
			}
			num = input.GetBits(8);
			if (num < 0)
			{
				return false;
			}
			if (num != 139)
			{
				throw new InvalidDataException(SR.GetString("CorruptedGZipHeader"));
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingCM;
			IL_A5:
			num = input.GetBits(8);
			if (num < 0)
			{
				return false;
			}
			if (num != 8)
			{
				throw new InvalidDataException(SR.GetString("UnknownCompressionMode"));
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingFLG;
			IL_CE:
			num = input.GetBits(8);
			if (num < 0)
			{
				return false;
			}
			this.gzip_header_flag = num;
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingMMTime;
			this.loopCounter = 0;
			IL_F1:
			while (this.loopCounter < 4)
			{
				num = input.GetBits(8);
				if (num < 0)
				{
					return false;
				}
				this.loopCounter++;
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingXFL;
			this.loopCounter = 0;
			IL_128:
			num = input.GetBits(8);
			if (num < 0)
			{
				return false;
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingOS;
			IL_13D:
			num = input.GetBits(8);
			if (num < 0)
			{
				return false;
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingXLen1;
			IL_152:
			if ((this.gzip_header_flag & 4) == 0)
			{
				goto IL_1E5;
			}
			num = input.GetBits(8);
			if (num < 0)
			{
				return false;
			}
			this.gzip_header_xlen = num;
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingXLen2;
			IL_17B:
			num = input.GetBits(8);
			if (num < 0)
			{
				return false;
			}
			this.gzip_header_xlen |= num << 8;
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingXLenData;
			this.loopCounter = 0;
			IL_1A8:
			while (this.loopCounter < this.gzip_header_xlen)
			{
				num = input.GetBits(8);
				if (num < 0)
				{
					return false;
				}
				this.loopCounter++;
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingFileName;
			this.loopCounter = 0;
			IL_1E5:
			if ((this.gzip_header_flag & 8) == 0)
			{
				this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingComment;
			}
			else
			{
				for (;;)
				{
					num = input.GetBits(8);
					if (num < 0)
					{
						break;
					}
					if (num == 0)
					{
						goto Block_20;
					}
				}
				return false;
				Block_20:
				this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingComment;
			}
			IL_212:
			if ((this.gzip_header_flag & 16) == 0)
			{
				this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingCRC16Part1;
			}
			else
			{
				for (;;)
				{
					num = input.GetBits(8);
					if (num < 0)
					{
						break;
					}
					if (num == 0)
					{
						goto Block_23;
					}
				}
				return false;
				Block_23:
				this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingCRC16Part1;
			}
			IL_240:
			if ((this.gzip_header_flag & 2) == 0)
			{
				this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.Done;
				return true;
			}
			num = input.GetBits(8);
			if (num < 0)
			{
				return false;
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingCRC16Part2;
			IL_26A:
			num = input.GetBits(8);
			if (num < 0)
			{
				return false;
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.Done;
			return true;
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x000B7FFC File Offset: 0x000B61FC
		public bool ReadFooter(InputBuffer input)
		{
			input.SkipToByteBoundary();
			if (this.gzipFooterSubstate == GZipDecoder.GzipHeaderState.ReadingCRC)
			{
				while (this.loopCounter < 4)
				{
					int bits = input.GetBits(8);
					if (bits < 0)
					{
						return false;
					}
					this.expectedCrc32 |= (uint)((uint)bits << 8 * this.loopCounter);
					this.loopCounter++;
				}
				this.gzipFooterSubstate = GZipDecoder.GzipHeaderState.ReadingFileSize;
				this.loopCounter = 0;
			}
			if (this.gzipFooterSubstate == GZipDecoder.GzipHeaderState.ReadingFileSize)
			{
				if (this.loopCounter == 0)
				{
					this.expectedOutputStreamSizeModulo = 0U;
				}
				while (this.loopCounter < 4)
				{
					int bits2 = input.GetBits(8);
					if (bits2 < 0)
					{
						return false;
					}
					this.expectedOutputStreamSizeModulo |= (uint)((uint)bits2 << 8 * this.loopCounter);
					this.loopCounter++;
				}
			}
			return true;
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000B80C4 File Offset: 0x000B62C4
		public void UpdateWithBytesRead(byte[] buffer, int offset, int copied)
		{
			this.actualCrc32 = Crc32Helper.UpdateCrc32(this.actualCrc32, buffer, offset, copied);
			long num = this.actualStreamSizeModulo + (long)((ulong)copied);
			if (num >= 4294967296L)
			{
				num %= 4294967296L;
			}
			this.actualStreamSizeModulo = num;
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000B810E File Offset: 0x000B630E
		public void Validate()
		{
			if (this.expectedCrc32 != this.actualCrc32)
			{
				throw new InvalidDataException(SR.GetString("InvalidCRC"));
			}
			if (this.actualStreamSizeModulo != (long)((ulong)this.expectedOutputStreamSizeModulo))
			{
				throw new InvalidDataException(SR.GetString("InvalidStreamSize"));
			}
		}

		// Token: 0x040021BC RID: 8636
		private GZipDecoder.GzipHeaderState gzipHeaderSubstate;

		// Token: 0x040021BD RID: 8637
		private GZipDecoder.GzipHeaderState gzipFooterSubstate;

		// Token: 0x040021BE RID: 8638
		private int gzip_header_flag;

		// Token: 0x040021BF RID: 8639
		private int gzip_header_xlen;

		// Token: 0x040021C0 RID: 8640
		private uint expectedCrc32;

		// Token: 0x040021C1 RID: 8641
		private uint expectedOutputStreamSizeModulo;

		// Token: 0x040021C2 RID: 8642
		private int loopCounter;

		// Token: 0x040021C3 RID: 8643
		private uint actualCrc32;

		// Token: 0x040021C4 RID: 8644
		private long actualStreamSizeModulo;

		// Token: 0x02000829 RID: 2089
		internal enum GzipHeaderState
		{
			// Token: 0x040035B5 RID: 13749
			ReadingID1,
			// Token: 0x040035B6 RID: 13750
			ReadingID2,
			// Token: 0x040035B7 RID: 13751
			ReadingCM,
			// Token: 0x040035B8 RID: 13752
			ReadingFLG,
			// Token: 0x040035B9 RID: 13753
			ReadingMMTime,
			// Token: 0x040035BA RID: 13754
			ReadingXFL,
			// Token: 0x040035BB RID: 13755
			ReadingOS,
			// Token: 0x040035BC RID: 13756
			ReadingXLen1,
			// Token: 0x040035BD RID: 13757
			ReadingXLen2,
			// Token: 0x040035BE RID: 13758
			ReadingXLenData,
			// Token: 0x040035BF RID: 13759
			ReadingFileName,
			// Token: 0x040035C0 RID: 13760
			ReadingComment,
			// Token: 0x040035C1 RID: 13761
			ReadingCRC16Part1,
			// Token: 0x040035C2 RID: 13762
			ReadingCRC16Part2,
			// Token: 0x040035C3 RID: 13763
			Done,
			// Token: 0x040035C4 RID: 13764
			ReadingCRC,
			// Token: 0x040035C5 RID: 13765
			ReadingFileSize
		}

		// Token: 0x0200082A RID: 2090
		[Flags]
		internal enum GZipOptionalHeaderFlags
		{
			// Token: 0x040035C7 RID: 13767
			CRCFlag = 2,
			// Token: 0x040035C8 RID: 13768
			ExtraFieldsFlag = 4,
			// Token: 0x040035C9 RID: 13769
			FileNameFlag = 8,
			// Token: 0x040035CA RID: 13770
			CommentFlag = 16
		}
	}
}
