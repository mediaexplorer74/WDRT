using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000454 RID: 1108
	internal sealed class Int16TypeInfo : TraceLoggingTypeInfo<short>
	{
		// Token: 0x06003686 RID: 13958 RVA: 0x000D4AAB File Offset: 0x000D2CAB
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000D4ABB File Offset: 0x000D2CBB
		public override void WriteData(TraceLoggingDataCollector collector, ref short value)
		{
			collector.AddScalar(value);
		}
	}
}
