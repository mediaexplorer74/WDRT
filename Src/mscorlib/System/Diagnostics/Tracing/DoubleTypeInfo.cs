using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045C RID: 1116
	internal sealed class DoubleTypeInfo : TraceLoggingTypeInfo<double>
	{
		// Token: 0x0600369E RID: 13982 RVA: 0x000D4BC5 File Offset: 0x000D2DC5
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Double));
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000D4BD6 File Offset: 0x000D2DD6
		public override void WriteData(TraceLoggingDataCollector collector, ref double value)
		{
			collector.AddScalar(value);
		}
	}
}
