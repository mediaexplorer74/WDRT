using System;

namespace System.IO.Compression
{
	// Token: 0x02000433 RID: 1075
	internal enum InflaterState
	{
		// Token: 0x040021FB RID: 8699
		ReadingHeader,
		// Token: 0x040021FC RID: 8700
		ReadingBFinal = 2,
		// Token: 0x040021FD RID: 8701
		ReadingBType,
		// Token: 0x040021FE RID: 8702
		ReadingNumLitCodes,
		// Token: 0x040021FF RID: 8703
		ReadingNumDistCodes,
		// Token: 0x04002200 RID: 8704
		ReadingNumCodeLengthCodes,
		// Token: 0x04002201 RID: 8705
		ReadingCodeLengthCodes,
		// Token: 0x04002202 RID: 8706
		ReadingTreeCodesBefore,
		// Token: 0x04002203 RID: 8707
		ReadingTreeCodesAfter,
		// Token: 0x04002204 RID: 8708
		DecodeTop,
		// Token: 0x04002205 RID: 8709
		HaveInitialLength,
		// Token: 0x04002206 RID: 8710
		HaveFullLength,
		// Token: 0x04002207 RID: 8711
		HaveDistCode,
		// Token: 0x04002208 RID: 8712
		UncompressedAligning = 15,
		// Token: 0x04002209 RID: 8713
		UncompressedByte1,
		// Token: 0x0400220A RID: 8714
		UncompressedByte2,
		// Token: 0x0400220B RID: 8715
		UncompressedByte3,
		// Token: 0x0400220C RID: 8716
		UncompressedByte4,
		// Token: 0x0400220D RID: 8717
		DecodingUncompressed,
		// Token: 0x0400220E RID: 8718
		StartReadingFooter,
		// Token: 0x0400220F RID: 8719
		ReadingFooter,
		// Token: 0x04002210 RID: 8720
		VerifyingFooter,
		// Token: 0x04002211 RID: 8721
		Done
	}
}
