using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000466 RID: 1126
	internal sealed class Int64ArrayTypeInfo : TraceLoggingTypeInfo<long[]>
	{
		// Token: 0x060036BC RID: 14012 RVA: 0x000D4D8C File Offset: 0x000D2F8C
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x000D4D9D File Offset: 0x000D2F9D
		public override void WriteData(TraceLoggingDataCollector collector, ref long[] value)
		{
			collector.AddArray(value);
		}
	}
}
