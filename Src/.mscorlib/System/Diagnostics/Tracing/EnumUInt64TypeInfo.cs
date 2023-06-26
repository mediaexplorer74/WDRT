using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000474 RID: 1140
	internal sealed class EnumUInt64TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036ED RID: 14061 RVA: 0x000D4FCD File Offset: 0x000D31CD
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000D4FDE File Offset: 0x000D31DE
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<ulong>.Cast<EnumType>(value));
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000D4FF1 File Offset: 0x000D31F1
		public override object GetData(object value)
		{
			return value;
		}
	}
}
