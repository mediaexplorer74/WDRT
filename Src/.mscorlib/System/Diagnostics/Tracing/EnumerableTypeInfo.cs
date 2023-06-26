using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200043D RID: 1085
	internal sealed class EnumerableTypeInfo<IterableType, ElementType> : TraceLoggingTypeInfo<IterableType> where IterableType : IEnumerable<ElementType>
	{
		// Token: 0x0600360B RID: 13835 RVA: 0x000D3A09 File Offset: 0x000D1C09
		public EnumerableTypeInfo(TraceLoggingTypeInfo<ElementType> elementInfo)
		{
			this.elementInfo = elementInfo;
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000D3A18 File Offset: 0x000D1C18
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.BeginBufferedArray();
			this.elementInfo.WriteMetadata(collector, name, format);
			collector.EndBufferedArray();
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x000D3A34 File Offset: 0x000D1C34
		public override void WriteData(TraceLoggingDataCollector collector, ref IterableType value)
		{
			int num = collector.BeginBufferedArray();
			int num2 = 0;
			if (value != null)
			{
				foreach (ElementType elementType in value)
				{
					ElementType elementType2 = elementType;
					this.elementInfo.WriteData(collector, ref elementType2);
					num2++;
				}
			}
			collector.EndBufferedArray(num, num2);
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000D3AB0 File Offset: 0x000D1CB0
		public override object GetData(object value)
		{
			IterableType iterableType = (IterableType)((object)value);
			List<object> list = new List<object>();
			foreach (ElementType elementType in iterableType)
			{
				list.Add(this.elementInfo.GetData(elementType));
			}
			return list.ToArray();
		}

		// Token: 0x04001823 RID: 6179
		private readonly TraceLoggingTypeInfo<ElementType> elementInfo;
	}
}
