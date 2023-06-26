using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000472 RID: 1138
	internal sealed class EnumUInt32TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036E5 RID: 14053 RVA: 0x000D4F70 File Offset: 0x000D3170
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000D4F80 File Offset: 0x000D3180
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<uint>.Cast<EnumType>(value));
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000D4F93 File Offset: 0x000D3193
		public override object GetData(object value)
		{
			return value;
		}
	}
}
