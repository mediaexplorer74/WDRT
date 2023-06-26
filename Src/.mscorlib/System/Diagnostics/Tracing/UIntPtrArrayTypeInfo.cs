using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000469 RID: 1129
	internal sealed class UIntPtrArrayTypeInfo : TraceLoggingTypeInfo<UIntPtr[]>
	{
		// Token: 0x060036C5 RID: 14021 RVA: 0x000D4DF8 File Offset: 0x000D2FF8
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.FormatPtr(format, Statics.UIntPtrType));
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000D4E0C File Offset: 0x000D300C
		public override void WriteData(TraceLoggingDataCollector collector, ref UIntPtr[] value)
		{
			collector.AddArray(value);
		}
	}
}
