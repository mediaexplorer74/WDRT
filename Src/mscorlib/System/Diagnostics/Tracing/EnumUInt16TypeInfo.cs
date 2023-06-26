using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000470 RID: 1136
	internal sealed class EnumUInt16TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036DD RID: 14045 RVA: 0x000D4F14 File Offset: 0x000D3114
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x000D4F24 File Offset: 0x000D3124
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<ushort>.Cast<EnumType>(value));
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000D4F37 File Offset: 0x000D3137
		public override object GetData(object value)
		{
			return value;
		}
	}
}
