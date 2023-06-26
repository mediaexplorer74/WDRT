using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000476 RID: 1142
	internal sealed class GuidTypeInfo : TraceLoggingTypeInfo<Guid>
	{
		// Token: 0x060036F5 RID: 14069 RVA: 0x000D503F File Offset: 0x000D323F
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Guid, format));
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000D5050 File Offset: 0x000D3250
		public override void WriteData(TraceLoggingDataCollector collector, ref Guid value)
		{
			collector.AddScalar(value);
		}
	}
}
