using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000452 RID: 1106
	internal sealed class ByteTypeInfo : TraceLoggingTypeInfo<byte>
	{
		// Token: 0x06003680 RID: 13952 RVA: 0x000D4A67 File Offset: 0x000D2C67
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.UInt8));
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000D4A77 File Offset: 0x000D2C77
		public override void WriteData(TraceLoggingDataCollector collector, ref byte value)
		{
			collector.AddScalar(value);
		}
	}
}
