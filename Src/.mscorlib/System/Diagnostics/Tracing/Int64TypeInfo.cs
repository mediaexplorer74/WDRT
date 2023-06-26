using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000458 RID: 1112
	internal sealed class Int64TypeInfo : TraceLoggingTypeInfo<long>
	{
		// Token: 0x06003692 RID: 13970 RVA: 0x000D4B33 File Offset: 0x000D2D33
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x000D4B44 File Offset: 0x000D2D44
		public override void WriteData(TraceLoggingDataCollector collector, ref long value)
		{
			collector.AddScalar(value);
		}
	}
}
