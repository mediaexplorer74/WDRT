using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000471 RID: 1137
	internal sealed class EnumInt32TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036E1 RID: 14049 RVA: 0x000D4F42 File Offset: 0x000D3142
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000D4F52 File Offset: 0x000D3152
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<int>.Cast<EnumType>(value));
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x000D4F65 File Offset: 0x000D3165
		public override object GetData(object value)
		{
			return value;
		}
	}
}
