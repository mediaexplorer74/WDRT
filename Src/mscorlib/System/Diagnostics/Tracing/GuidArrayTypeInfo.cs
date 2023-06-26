using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000477 RID: 1143
	internal sealed class GuidArrayTypeInfo : TraceLoggingTypeInfo<Guid[]>
	{
		// Token: 0x060036F8 RID: 14072 RVA: 0x000D5066 File Offset: 0x000D3266
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.MakeDataType(TraceLoggingDataType.Guid, format));
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000D5077 File Offset: 0x000D3277
		public override void WriteData(TraceLoggingDataCollector collector, ref Guid[] value)
		{
			collector.AddArray(value);
		}
	}
}
