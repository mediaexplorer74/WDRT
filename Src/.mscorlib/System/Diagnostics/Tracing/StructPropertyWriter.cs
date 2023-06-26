using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044C RID: 1100
	internal class StructPropertyWriter<ContainerType, ValueType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x0600366F RID: 13935 RVA: 0x000D4844 File Offset: 0x000D2A44
		public StructPropertyWriter(PropertyAnalysis property)
		{
			this.valueTypeInfo = (TraceLoggingTypeInfo<ValueType>)property.typeInfo;
			this.getter = (StructPropertyWriter<ContainerType, ValueType>.Getter)Statics.CreateDelegate(typeof(StructPropertyWriter<ContainerType, ValueType>.Getter), property.getterInfo);
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000D4880 File Offset: 0x000D2A80
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			ValueType valueType = ((container == null) ? default(ValueType) : this.getter(ref container));
			this.valueTypeInfo.WriteData(collector, ref valueType);
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000D48C0 File Offset: 0x000D2AC0
		public override object GetData(ContainerType container)
		{
			return (container == null) ? default(ValueType) : this.getter(ref container);
		}

		// Token: 0x0400185A RID: 6234
		private readonly TraceLoggingTypeInfo<ValueType> valueTypeInfo;

		// Token: 0x0400185B RID: 6235
		private readonly StructPropertyWriter<ContainerType, ValueType>.Getter getter;

		// Token: 0x02000B98 RID: 2968
		// (Invoke) Token: 0x06006CB6 RID: 27830
		private delegate ValueType Getter(ref ContainerType container);
	}
}
