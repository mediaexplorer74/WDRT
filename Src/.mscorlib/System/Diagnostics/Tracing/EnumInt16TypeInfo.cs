using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200046F RID: 1135
	internal sealed class EnumInt16TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036D9 RID: 14041 RVA: 0x000D4EE6 File Offset: 0x000D30E6
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x000D4EF6 File Offset: 0x000D30F6
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<short>.Cast<EnumType>(value));
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x000D4F09 File Offset: 0x000D3109
		public override object GetData(object value)
		{
			return value;
		}
	}
}
