using System;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044B RID: 1099
	internal class NonGenericProperytWriter<ContainerType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x0600366C RID: 13932 RVA: 0x000D47C3 File Offset: 0x000D29C3
		public NonGenericProperytWriter(PropertyAnalysis property)
		{
			this.getterInfo = property.getterInfo;
			this.typeInfo = property.typeInfo;
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000D47E4 File Offset: 0x000D29E4
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			object obj = ((container == null) ? null : this.getterInfo.Invoke(container, null));
			this.typeInfo.WriteObjectData(collector, obj);
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000D4826 File Offset: 0x000D2A26
		public override object GetData(ContainerType container)
		{
			if (container != null)
			{
				return this.getterInfo.Invoke(container, null);
			}
			return null;
		}

		// Token: 0x04001858 RID: 6232
		private readonly TraceLoggingTypeInfo typeInfo;

		// Token: 0x04001859 RID: 6233
		private readonly MethodInfo getterInfo;
	}
}
