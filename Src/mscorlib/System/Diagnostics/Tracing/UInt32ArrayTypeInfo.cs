using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000465 RID: 1125
	internal sealed class UInt32ArrayTypeInfo : TraceLoggingTypeInfo<uint[]>
	{
		// Token: 0x060036B9 RID: 14009 RVA: 0x000D4D6A File Offset: 0x000D2F6A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x000D4D7A File Offset: 0x000D2F7A
		public override void WriteData(TraceLoggingDataCollector collector, ref uint[] value)
		{
			collector.AddArray(value);
		}
	}
}
