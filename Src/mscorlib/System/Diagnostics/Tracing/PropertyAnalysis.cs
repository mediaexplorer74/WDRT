using System;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044E RID: 1102
	internal sealed class PropertyAnalysis
	{
		// Token: 0x06003675 RID: 13941 RVA: 0x000D49A5 File Offset: 0x000D2BA5
		public PropertyAnalysis(string name, MethodInfo getterInfo, TraceLoggingTypeInfo typeInfo, EventFieldAttribute fieldAttribute)
		{
			this.name = name;
			this.getterInfo = getterInfo;
			this.typeInfo = typeInfo;
			this.fieldAttribute = fieldAttribute;
		}

		// Token: 0x0400185E RID: 6238
		internal readonly string name;

		// Token: 0x0400185F RID: 6239
		internal readonly MethodInfo getterInfo;

		// Token: 0x04001860 RID: 6240
		internal readonly TraceLoggingTypeInfo typeInfo;

		// Token: 0x04001861 RID: 6241
		internal readonly EventFieldAttribute fieldAttribute;
	}
}
