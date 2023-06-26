using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200046E RID: 1134
	internal sealed class EnumSByteTypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036D5 RID: 14037 RVA: 0x000D4EB8 File Offset: 0x000D30B8
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000D4EC8 File Offset: 0x000D30C8
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<sbyte>.Cast<EnumType>(value));
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000D4EDB File Offset: 0x000D30DB
		public override object GetData(object value)
		{
			return value;
		}
	}
}
