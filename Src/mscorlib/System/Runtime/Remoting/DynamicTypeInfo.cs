using System;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007B7 RID: 1975
	[Serializable]
	internal class DynamicTypeInfo : TypeInfo
	{
		// Token: 0x060055B1 RID: 21937 RVA: 0x001317DF File Offset: 0x0012F9DF
		[SecurityCritical]
		internal DynamicTypeInfo(RuntimeType typeOfObj)
			: base(typeOfObj)
		{
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x001317E8 File Offset: 0x0012F9E8
		[SecurityCritical]
		public override bool CanCastTo(Type castType, object o)
		{
			return ((MarshalByRefObject)o).IsInstanceOfType(castType);
		}
	}
}
