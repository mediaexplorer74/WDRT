using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000453 RID: 1107
	internal sealed class SByteTypeInfo : TraceLoggingTypeInfo<sbyte>
	{
		// Token: 0x06003683 RID: 13955 RVA: 0x000D4A89 File Offset: 0x000D2C89
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000D4A99 File Offset: 0x000D2C99
		public override void WriteData(TraceLoggingDataCollector collector, ref sbyte value)
		{
			collector.AddScalar(value);
		}
	}
}
