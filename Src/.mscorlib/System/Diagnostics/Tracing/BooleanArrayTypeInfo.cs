using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045F RID: 1119
	internal sealed class BooleanArrayTypeInfo : TraceLoggingTypeInfo<bool[]>
	{
		// Token: 0x060036A7 RID: 13991 RVA: 0x000D4C31 File Offset: 0x000D2E31
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format8(format, TraceLoggingDataType.Boolean8));
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000D4C45 File Offset: 0x000D2E45
		public override void WriteData(TraceLoggingDataCollector collector, ref bool[] value)
		{
			collector.AddArray(value);
		}
	}
}
