using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000895 RID: 2197
	internal class RemotePropertyHolderAttribute : IContextAttribute
	{
		// Token: 0x06005D39 RID: 23865 RVA: 0x0014810D File Offset: 0x0014630D
		internal RemotePropertyHolderAttribute(IList cp)
		{
			this._cp = cp;
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x0014811C File Offset: 0x0014631C
		[SecurityCritical]
		[ComVisible(true)]
		public virtual bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return false;
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x00148120 File Offset: 0x00146320
		[SecurityCritical]
		[ComVisible(true)]
		public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			for (int i = 0; i < this._cp.Count; i++)
			{
				ctorMsg.ContextProperties.Add(this._cp[i]);
			}
		}

		// Token: 0x040029F9 RID: 10745
		private IList _cp;
	}
}
