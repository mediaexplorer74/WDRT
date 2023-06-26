using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044D RID: 1101
	internal class ClassPropertyWriter<ContainerType, ValueType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x06003672 RID: 13938 RVA: 0x000D48F2 File Offset: 0x000D2AF2
		public ClassPropertyWriter(PropertyAnalysis property)
		{
			this.valueTypeInfo = (TraceLoggingTypeInfo<ValueType>)property.typeInfo;
			this.getter = (ClassPropertyWriter<ContainerType, ValueType>.Getter)Statics.CreateDelegate(typeof(ClassPropertyWriter<ContainerType, ValueType>.Getter), property.getterInfo);
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000D492C File Offset: 0x000D2B2C
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			ValueType valueType = ((container == null) ? default(ValueType) : this.getter(container));
			this.valueTypeInfo.WriteData(collector, ref valueType);
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000D4974 File Offset: 0x000D2B74
		public override object GetData(ContainerType container)
		{
			return (container == null) ? default(ValueType) : this.getter(container);
		}

		// Token: 0x0400185C RID: 6236
		private readonly TraceLoggingTypeInfo<ValueType> valueTypeInfo;

		// Token: 0x0400185D RID: 6237
		private readonly ClassPropertyWriter<ContainerType, ValueType>.Getter getter;

		// Token: 0x02000B99 RID: 2969
		// (Invoke) Token: 0x06006CBA RID: 27834
		private delegate ValueType Getter(ContainerType container);
	}
}
