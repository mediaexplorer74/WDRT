using System;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000874 RID: 2164
	internal class SmuggledObjRef
	{
		// Token: 0x06005C47 RID: 23623 RVA: 0x001449DC File Offset: 0x00142BDC
		[SecurityCritical]
		public SmuggledObjRef(ObjRef objRef)
		{
			this._objRef = objRef;
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06005C48 RID: 23624 RVA: 0x001449EB File Offset: 0x00142BEB
		public ObjRef ObjRef
		{
			[SecurityCritical]
			get
			{
				return this._objRef;
			}
		}

		// Token: 0x040029A6 RID: 10662
		[SecurityCritical]
		private ObjRef _objRef;
	}
}
