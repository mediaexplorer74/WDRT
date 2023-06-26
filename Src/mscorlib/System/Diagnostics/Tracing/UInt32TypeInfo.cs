using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000457 RID: 1111
	internal sealed class UInt32TypeInfo : TraceLoggingTypeInfo<uint>
	{
		// Token: 0x0600368F RID: 13967 RVA: 0x000D4B11 File Offset: 0x000D2D11
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000D4B21 File Offset: 0x000D2D21
		public override void WriteData(TraceLoggingDataCollector collector, ref uint value)
		{
			collector.AddScalar(value);
		}
	}
}
