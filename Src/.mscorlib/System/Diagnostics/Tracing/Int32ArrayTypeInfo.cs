using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000464 RID: 1124
	internal sealed class Int32ArrayTypeInfo : TraceLoggingTypeInfo<int[]>
	{
		// Token: 0x060036B6 RID: 14006 RVA: 0x000D4D48 File Offset: 0x000D2F48
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x000D4D58 File Offset: 0x000D2F58
		public override void WriteData(TraceLoggingDataCollector collector, ref int[] value)
		{
			collector.AddArray(value);
		}
	}
}
