using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000461 RID: 1121
	internal sealed class SByteArrayTypeInfo : TraceLoggingTypeInfo<sbyte[]>
	{
		// Token: 0x060036AD RID: 13997 RVA: 0x000D4CE2 File Offset: 0x000D2EE2
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x000D4CF2 File Offset: 0x000D2EF2
		public override void WriteData(TraceLoggingDataCollector collector, ref sbyte[] value)
		{
			collector.AddArray(value);
		}
	}
}
