using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200047B RID: 1147
	internal sealed class DecimalTypeInfo : TraceLoggingTypeInfo<decimal>
	{
		// Token: 0x06003704 RID: 14084 RVA: 0x000D518C File Offset: 0x000D338C
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Double, format));
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x000D519D File Offset: 0x000D339D
		public override void WriteData(TraceLoggingDataCollector collector, ref decimal value)
		{
			collector.AddScalar((double)value);
		}
	}
}
