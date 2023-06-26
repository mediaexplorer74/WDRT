using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000455 RID: 1109
	internal sealed class UInt16TypeInfo : TraceLoggingTypeInfo<ushort>
	{
		// Token: 0x06003689 RID: 13961 RVA: 0x000D4ACD File Offset: 0x000D2CCD
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000D4ADD File Offset: 0x000D2CDD
		public override void WriteData(TraceLoggingDataCollector collector, ref ushort value)
		{
			collector.AddScalar(value);
		}
	}
}
