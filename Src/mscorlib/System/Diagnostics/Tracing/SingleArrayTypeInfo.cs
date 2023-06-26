using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200046C RID: 1132
	internal sealed class SingleArrayTypeInfo : TraceLoggingTypeInfo<float[]>
	{
		// Token: 0x060036CE RID: 14030 RVA: 0x000D4E67 File Offset: 0x000D3067
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.Float));
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000D4E78 File Offset: 0x000D3078
		public override void WriteData(TraceLoggingDataCollector collector, ref float[] value)
		{
			collector.AddArray(value);
		}
	}
}
