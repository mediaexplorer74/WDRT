using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000451 RID: 1105
	internal sealed class BooleanTypeInfo : TraceLoggingTypeInfo<bool>
	{
		// Token: 0x0600367D RID: 13949 RVA: 0x000D4A41 File Offset: 0x000D2C41
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Boolean8));
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000D4A55 File Offset: 0x000D2C55
		public override void WriteData(TraceLoggingDataCollector collector, ref bool value)
		{
			collector.AddScalar(value);
		}
	}
}
