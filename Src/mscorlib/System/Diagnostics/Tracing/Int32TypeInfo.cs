using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000456 RID: 1110
	internal sealed class Int32TypeInfo : TraceLoggingTypeInfo<int>
	{
		// Token: 0x0600368C RID: 13964 RVA: 0x000D4AEF File Offset: 0x000D2CEF
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000D4AFF File Offset: 0x000D2CFF
		public override void WriteData(TraceLoggingDataCollector collector, ref int value)
		{
			collector.AddScalar(value);
		}
	}
}
