using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045A RID: 1114
	internal sealed class IntPtrTypeInfo : TraceLoggingTypeInfo<IntPtr>
	{
		// Token: 0x06003698 RID: 13976 RVA: 0x000D4B79 File Offset: 0x000D2D79
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.FormatPtr(format, Statics.IntPtrType));
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000D4B8D File Offset: 0x000D2D8D
		public override void WriteData(TraceLoggingDataCollector collector, ref IntPtr value)
		{
			collector.AddScalar(value);
		}
	}
}
