using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000467 RID: 1127
	internal sealed class UInt64ArrayTypeInfo : TraceLoggingTypeInfo<ulong[]>
	{
		// Token: 0x060036BF RID: 14015 RVA: 0x000D4DAF File Offset: 0x000D2FAF
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000D4DC0 File Offset: 0x000D2FC0
		public override void WriteData(TraceLoggingDataCollector collector, ref ulong[] value)
		{
			collector.AddArray(value);
		}
	}
}
