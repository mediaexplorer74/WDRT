using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000460 RID: 1120
	internal sealed class ByteArrayTypeInfo : TraceLoggingTypeInfo<byte[]>
	{
		// Token: 0x060036AA RID: 13994 RVA: 0x000D4C58 File Offset: 0x000D2E58
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			switch (format)
			{
			case EventFieldFormat.String:
				collector.AddBinary(name, TraceLoggingDataType.CountedMbcsString);
				return;
			case EventFieldFormat.Boolean:
				collector.AddArray(name, TraceLoggingDataType.Boolean8);
				return;
			case EventFieldFormat.Hexadecimal:
				collector.AddArray(name, TraceLoggingDataType.HexInt8);
				return;
			default:
				if (format == EventFieldFormat.Xml)
				{
					collector.AddBinary(name, TraceLoggingDataType.CountedMbcsXml);
					return;
				}
				if (format != EventFieldFormat.Json)
				{
					collector.AddBinary(name, Statics.MakeDataType(TraceLoggingDataType.Binary, format));
					return;
				}
				collector.AddBinary(name, TraceLoggingDataType.CountedMbcsJson);
				return;
			}
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000D4CD0 File Offset: 0x000D2ED0
		public override void WriteData(TraceLoggingDataCollector collector, ref byte[] value)
		{
			collector.AddBinary(value);
		}
	}
}
