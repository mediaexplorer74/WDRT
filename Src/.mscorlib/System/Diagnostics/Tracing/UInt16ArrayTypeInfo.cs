using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000463 RID: 1123
	internal sealed class UInt16ArrayTypeInfo : TraceLoggingTypeInfo<ushort[]>
	{
		// Token: 0x060036B3 RID: 14003 RVA: 0x000D4D26 File Offset: 0x000D2F26
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x000D4D36 File Offset: 0x000D2F36
		public override void WriteData(TraceLoggingDataCollector collector, ref ushort[] value)
		{
			collector.AddArray(value);
		}
	}
}
