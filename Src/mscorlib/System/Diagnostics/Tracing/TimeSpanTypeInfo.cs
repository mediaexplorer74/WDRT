using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200047A RID: 1146
	internal sealed class TimeSpanTypeInfo : TraceLoggingTypeInfo<TimeSpan>
	{
		// Token: 0x06003701 RID: 14081 RVA: 0x000D5165 File Offset: 0x000D3365
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Int64, format));
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000D5176 File Offset: 0x000D3376
		public override void WriteData(TraceLoggingDataCollector collector, ref TimeSpan value)
		{
			collector.AddScalar(value.Ticks);
		}
	}
}
