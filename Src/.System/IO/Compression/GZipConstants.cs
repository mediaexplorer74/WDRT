using System;

namespace System.IO.Compression
{
	// Token: 0x0200042F RID: 1071
	internal static class GZipConstants
	{
		// Token: 0x040021C6 RID: 8646
		internal const int CompressionLevel_3 = 3;

		// Token: 0x040021C7 RID: 8647
		internal const int CompressionLevel_10 = 10;

		// Token: 0x040021C8 RID: 8648
		internal const long FileLengthModulo = 4294967296L;

		// Token: 0x040021C9 RID: 8649
		internal const byte ID1 = 31;

		// Token: 0x040021CA RID: 8650
		internal const byte ID2 = 139;

		// Token: 0x040021CB RID: 8651
		internal const byte Deflate = 8;

		// Token: 0x040021CC RID: 8652
		internal const int Xfl_HeaderPos = 8;

		// Token: 0x040021CD RID: 8653
		internal const byte Xfl_FastestAlgorithm = 4;

		// Token: 0x040021CE RID: 8654
		internal const byte Xfl_MaxCompressionSlowestAlgorithm = 2;
	}
}
