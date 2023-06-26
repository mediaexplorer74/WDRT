using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045D RID: 1117
	internal sealed class SingleTypeInfo : TraceLoggingTypeInfo<float>
	{
		// Token: 0x060036A1 RID: 13985 RVA: 0x000D4BE8 File Offset: 0x000D2DE8
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Float));
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000D4BF9 File Offset: 0x000D2DF9
		public override void WriteData(TraceLoggingDataCollector collector, ref float value)
		{
			collector.AddScalar(value);
		}
	}
}
