using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045B RID: 1115
	internal sealed class UIntPtrTypeInfo : TraceLoggingTypeInfo<UIntPtr>
	{
		// Token: 0x0600369B RID: 13979 RVA: 0x000D4B9F File Offset: 0x000D2D9F
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.FormatPtr(format, Statics.UIntPtrType));
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x000D4BB3 File Offset: 0x000D2DB3
		public override void WriteData(TraceLoggingDataCollector collector, ref UIntPtr value)
		{
			collector.AddScalar(value);
		}
	}
}
