using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045E RID: 1118
	internal sealed class CharTypeInfo : TraceLoggingTypeInfo<char>
	{
		// Token: 0x060036A4 RID: 13988 RVA: 0x000D4C0B File Offset: 0x000D2E0B
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Char16));
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000D4C1F File Offset: 0x000D2E1F
		public override void WriteData(TraceLoggingDataCollector collector, ref char value)
		{
			collector.AddScalar(value);
		}
	}
}
