using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000473 RID: 1139
	internal sealed class EnumInt64TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036E9 RID: 14057 RVA: 0x000D4F9E File Offset: 0x000D319E
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000D4FAF File Offset: 0x000D31AF
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<long>.Cast<EnumType>(value));
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000D4FC2 File Offset: 0x000D31C2
		public override object GetData(object value)
		{
			return value;
		}
	}
}
