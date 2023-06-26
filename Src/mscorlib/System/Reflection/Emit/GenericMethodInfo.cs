using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000633 RID: 1587
	internal sealed class GenericMethodInfo
	{
		// Token: 0x06004A45 RID: 19013 RVA: 0x0010DE55 File Offset: 0x0010C055
		internal GenericMethodInfo(RuntimeMethodHandle methodHandle, RuntimeTypeHandle context)
		{
			this.m_methodHandle = methodHandle;
			this.m_context = context;
		}

		// Token: 0x04001E9E RID: 7838
		internal RuntimeMethodHandle m_methodHandle;

		// Token: 0x04001E9F RID: 7839
		internal RuntimeTypeHandle m_context;
	}
}
