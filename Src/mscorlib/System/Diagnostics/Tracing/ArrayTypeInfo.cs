using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000438 RID: 1080
	internal sealed class ArrayTypeInfo<ElementType> : TraceLoggingTypeInfo<ElementType[]>
	{
		// Token: 0x060035F1 RID: 13809 RVA: 0x000D33A7 File Offset: 0x000D15A7
		public ArrayTypeInfo(TraceLoggingTypeInfo<ElementType> elementInfo)
		{
			this.elementInfo = elementInfo;
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000D33B6 File Offset: 0x000D15B6
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.BeginBufferedArray();
			this.elementInfo.WriteMetadata(collector, name, format);
			collector.EndBufferedArray();
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000D33D4 File Offset: 0x000D15D4
		public override void WriteData(TraceLoggingDataCollector collector, ref ElementType[] value)
		{
			int num = collector.BeginBufferedArray();
			int num2 = 0;
			if (value != null)
			{
				num2 = value.Length;
				for (int i = 0; i < value.Length; i++)
				{
					this.elementInfo.WriteData(collector, ref value[i]);
				}
			}
			collector.EndBufferedArray(num, num2);
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000D3420 File Offset: 0x000D1620
		public override object GetData(object value)
		{
			ElementType[] array = (ElementType[])value;
			object[] array2 = new object[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.elementInfo.GetData(array[i]);
			}
			return array2;
		}

		// Token: 0x04001815 RID: 6165
		private readonly TraceLoggingTypeInfo<ElementType> elementInfo;
	}
}
