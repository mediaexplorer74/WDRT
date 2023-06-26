using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200046B RID: 1131
	internal sealed class DoubleArrayTypeInfo : TraceLoggingTypeInfo<double[]>
	{
		// Token: 0x060036CB RID: 14027 RVA: 0x000D4E44 File Offset: 0x000D3044
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.Double));
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000D4E55 File Offset: 0x000D3055
		public override void WriteData(TraceLoggingDataCollector collector, ref double[] value)
		{
			collector.AddArray(value);
		}
	}
}
