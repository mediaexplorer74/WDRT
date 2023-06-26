using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200046A RID: 1130
	internal sealed class CharArrayTypeInfo : TraceLoggingTypeInfo<char[]>
	{
		// Token: 0x060036C8 RID: 14024 RVA: 0x000D4E1E File Offset: 0x000D301E
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.Char16));
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000D4E32 File Offset: 0x000D3032
		public override void WriteData(TraceLoggingDataCollector collector, ref char[] value)
		{
			collector.AddArray(value);
		}
	}
}
