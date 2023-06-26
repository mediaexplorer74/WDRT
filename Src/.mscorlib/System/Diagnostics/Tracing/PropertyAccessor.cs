using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044A RID: 1098
	internal abstract class PropertyAccessor<ContainerType>
	{
		// Token: 0x06003668 RID: 13928
		public abstract void Write(TraceLoggingDataCollector collector, ref ContainerType value);

		// Token: 0x06003669 RID: 13929
		public abstract object GetData(ContainerType value);

		// Token: 0x0600366A RID: 13930 RVA: 0x000D4740 File Offset: 0x000D2940
		public static PropertyAccessor<ContainerType> Create(PropertyAnalysis property)
		{
			Type returnType = property.getterInfo.ReturnType;
			if (!Statics.IsValueType(typeof(ContainerType)))
			{
				if (returnType == typeof(int))
				{
					return new ClassPropertyWriter<ContainerType, int>(property);
				}
				if (returnType == typeof(long))
				{
					return new ClassPropertyWriter<ContainerType, long>(property);
				}
				if (returnType == typeof(string))
				{
					return new ClassPropertyWriter<ContainerType, string>(property);
				}
			}
			return new NonGenericProperytWriter<ContainerType>(property);
		}
	}
}
