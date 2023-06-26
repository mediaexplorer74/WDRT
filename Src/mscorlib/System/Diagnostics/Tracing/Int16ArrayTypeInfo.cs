using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000462 RID: 1122
	internal sealed class Int16ArrayTypeInfo : TraceLoggingTypeInfo<short[]>
	{
		// Token: 0x060036B0 RID: 14000 RVA: 0x000D4D04 File Offset: 0x000D2F04
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000D4D14 File Offset: 0x000D2F14
		public override void WriteData(TraceLoggingDataCollector collector, ref short[] value)
		{
			collector.AddArray(value);
		}
	}
}
