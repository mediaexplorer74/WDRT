using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000430 RID: 1072
	internal struct ManifestEnvelope
	{
		// Token: 0x040017E7 RID: 6119
		public const int MaxChunkSize = 65280;

		// Token: 0x040017E8 RID: 6120
		public ManifestEnvelope.ManifestFormats Format;

		// Token: 0x040017E9 RID: 6121
		public byte MajorVersion;

		// Token: 0x040017EA RID: 6122
		public byte MinorVersion;

		// Token: 0x040017EB RID: 6123
		public byte Magic;

		// Token: 0x040017EC RID: 6124
		public ushort TotalChunks;

		// Token: 0x040017ED RID: 6125
		public ushort ChunkNumber;

		// Token: 0x02000B90 RID: 2960
		public enum ManifestFormats : byte
		{
			// Token: 0x0400351F RID: 13599
			SimpleXmlFormat = 1
		}
	}
}
