using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000475 RID: 1141
	internal sealed class StringTypeInfo : TraceLoggingTypeInfo<string>
	{
		// Token: 0x060036F1 RID: 14065 RVA: 0x000D4FFC File Offset: 0x000D31FC
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddBinary(name, Statics.MakeDataType(TraceLoggingDataType.CountedUtf16String, format));
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000D500D File Offset: 0x000D320D
		public override void WriteData(TraceLoggingDataCollector collector, ref string value)
		{
			collector.AddBinary(value);
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000D5018 File Offset: 0x000D3218
		public override object GetData(object value)
		{
			object obj = base.GetData(value);
			if (obj == null)
			{
				obj = "";
			}
			return obj;
		}
	}
}
