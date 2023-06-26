using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000478 RID: 1144
	internal sealed class DateTimeTypeInfo : TraceLoggingTypeInfo<DateTime>
	{
		// Token: 0x060036FB RID: 14075 RVA: 0x000D5089 File Offset: 0x000D3289
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.FileTime, format));
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x000D509C File Offset: 0x000D329C
		public override void WriteData(TraceLoggingDataCollector collector, ref DateTime value)
		{
			long ticks = value.Ticks;
			collector.AddScalar((ticks < 504911232000000000L) ? 0L : (ticks - 504911232000000000L));
		}
	}
}
