using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000459 RID: 1113
	internal sealed class UInt64TypeInfo : TraceLoggingTypeInfo<ulong>
	{
		// Token: 0x06003695 RID: 13973 RVA: 0x000D4B56 File Offset: 0x000D2D56
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000D4B67 File Offset: 0x000D2D67
		public override void WriteData(TraceLoggingDataCollector collector, ref ulong value)
		{
			collector.AddScalar(value);
		}
	}
}
