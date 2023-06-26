using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000635 RID: 1589
	internal sealed class VarArgMethod
	{
		// Token: 0x06004A47 RID: 19015 RVA: 0x0010DE81 File Offset: 0x0010C081
		internal VarArgMethod(DynamicMethod dm, SignatureHelper signature)
		{
			this.m_dynamicMethod = dm;
			this.m_signature = signature;
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x0010DE97 File Offset: 0x0010C097
		internal VarArgMethod(RuntimeMethodInfo method, SignatureHelper signature)
		{
			this.m_method = method;
			this.m_signature = signature;
		}

		// Token: 0x04001EA2 RID: 7842
		internal RuntimeMethodInfo m_method;

		// Token: 0x04001EA3 RID: 7843
		internal DynamicMethod m_dynamicMethod;

		// Token: 0x04001EA4 RID: 7844
		internal SignatureHelper m_signature;
	}
}
