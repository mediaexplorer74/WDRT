using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200046D RID: 1133
	internal sealed class EnumByteTypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036D1 RID: 14033 RVA: 0x000D4E8A File Offset: 0x000D308A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.UInt8));
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000D4E9A File Offset: 0x000D309A
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<byte>.Cast<EnumType>(value));
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000D4EAD File Offset: 0x000D30AD
		public override object GetData(object value)
		{
			return value;
		}
	}
}
