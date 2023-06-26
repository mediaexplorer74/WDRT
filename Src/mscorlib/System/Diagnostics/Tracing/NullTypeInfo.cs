using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000450 RID: 1104
	internal sealed class NullTypeInfo<DataType> : TraceLoggingTypeInfo<DataType>
	{
		// Token: 0x06003679 RID: 13945 RVA: 0x000D4A2A File Offset: 0x000D2C2A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddGroup(name);
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000D4A34 File Offset: 0x000D2C34
		public override void WriteData(TraceLoggingDataCollector collector, ref DataType value)
		{
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000D4A36 File Offset: 0x000D2C36
		public override object GetData(object value)
		{
			return null;
		}
	}
}
