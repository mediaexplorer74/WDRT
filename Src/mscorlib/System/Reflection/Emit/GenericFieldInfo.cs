using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000634 RID: 1588
	internal sealed class GenericFieldInfo
	{
		// Token: 0x06004A46 RID: 19014 RVA: 0x0010DE6B File Offset: 0x0010C06B
		internal GenericFieldInfo(RuntimeFieldHandle fieldHandle, RuntimeTypeHandle context)
		{
			this.m_fieldHandle = fieldHandle;
			this.m_context = context;
		}

		// Token: 0x04001EA0 RID: 7840
		internal RuntimeFieldHandle m_fieldHandle;

		// Token: 0x04001EA1 RID: 7841
		internal RuntimeTypeHandle m_context;
	}
}
