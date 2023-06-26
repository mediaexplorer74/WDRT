using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000485 RID: 1157
	internal abstract class TraceLoggingTypeInfo<DataType> : TraceLoggingTypeInfo
	{
		// Token: 0x06003779 RID: 14201 RVA: 0x000D6951 File Offset: 0x000D4B51
		protected TraceLoggingTypeInfo()
			: base(typeof(DataType))
		{
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000D6963 File Offset: 0x000D4B63
		protected TraceLoggingTypeInfo(string name, EventLevel level, EventOpcode opcode, EventKeywords keywords, EventTags tags)
			: base(typeof(DataType), name, level, opcode, keywords, tags)
		{
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600377B RID: 14203 RVA: 0x000D697C File Offset: 0x000D4B7C
		public static TraceLoggingTypeInfo<DataType> Instance
		{
			get
			{
				return TraceLoggingTypeInfo<DataType>.instance ?? TraceLoggingTypeInfo<DataType>.InitInstance();
			}
		}

		// Token: 0x0600377C RID: 14204
		public abstract void WriteData(TraceLoggingDataCollector collector, ref DataType value);

		// Token: 0x0600377D RID: 14205 RVA: 0x000D698C File Offset: 0x000D4B8C
		public override void WriteObjectData(TraceLoggingDataCollector collector, object value)
		{
			DataType dataType = ((value == null) ? default(DataType) : ((DataType)((object)value)));
			this.WriteData(collector, ref dataType);
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000D69B8 File Offset: 0x000D4BB8
		internal static TraceLoggingTypeInfo<DataType> GetInstance(List<Type> recursionCheck)
		{
			if (TraceLoggingTypeInfo<DataType>.instance == null)
			{
				int count = recursionCheck.Count;
				TraceLoggingTypeInfo<DataType> traceLoggingTypeInfo = Statics.CreateDefaultTypeInfo<DataType>(recursionCheck);
				Interlocked.CompareExchange<TraceLoggingTypeInfo<DataType>>(ref TraceLoggingTypeInfo<DataType>.instance, traceLoggingTypeInfo, null);
				recursionCheck.RemoveRange(count, recursionCheck.Count - count);
			}
			return TraceLoggingTypeInfo<DataType>.instance;
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000D69FB File Offset: 0x000D4BFB
		private static TraceLoggingTypeInfo<DataType> InitInstance()
		{
			return TraceLoggingTypeInfo<DataType>.GetInstance(new List<Type>());
		}

		// Token: 0x040018B3 RID: 6323
		private static TraceLoggingTypeInfo<DataType> instance;
	}
}
