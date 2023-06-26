using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000468 RID: 1128
	internal sealed class IntPtrArrayTypeInfo : TraceLoggingTypeInfo<IntPtr[]>
	{
		// Token: 0x060036C2 RID: 14018 RVA: 0x000D4DD2 File Offset: 0x000D2FD2
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.FormatPtr(format, Statics.IntPtrType));
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x000D4DE6 File Offset: 0x000D2FE6
		public override void WriteData(TraceLoggingDataCollector collector, ref IntPtr[] value)
		{
			collector.AddArray(value);
		}
	}
}
