using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200047D RID: 1149
	internal sealed class NullableTypeInfo<T> : TraceLoggingTypeInfo<T?> where T : struct
	{
		// Token: 0x0600370B RID: 14091 RVA: 0x000D52AF File Offset: 0x000D34AF
		public NullableTypeInfo(List<Type> recursionCheck)
		{
			this.valueInfo = TraceLoggingTypeInfo<T>.GetInstance(recursionCheck);
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x000D52C4 File Offset: 0x000D34C4
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			traceLoggingMetadataCollector.AddScalar("HasValue", TraceLoggingDataType.Boolean8);
			this.valueInfo.WriteMetadata(traceLoggingMetadataCollector, "Value", format);
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000D52FC File Offset: 0x000D34FC
		public override void WriteData(TraceLoggingDataCollector collector, ref T? value)
		{
			bool flag = value != null;
			collector.AddScalar(flag);
			T t = (flag ? value.Value : default(T));
			this.valueInfo.WriteData(collector, ref t);
		}

		// Token: 0x04001866 RID: 6246
		private readonly TraceLoggingTypeInfo<T> valueInfo;
	}
}
