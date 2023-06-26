using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000894 RID: 2196
	[Serializable]
	internal class ConstructionLevelActivator : IActivator
	{
		// Token: 0x06005D34 RID: 23860 RVA: 0x001480DF File Offset: 0x001462DF
		internal ConstructionLevelActivator()
		{
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06005D35 RID: 23861 RVA: 0x001480E7 File Offset: 0x001462E7
		// (set) Token: 0x06005D36 RID: 23862 RVA: 0x001480EA File Offset: 0x001462EA
		public virtual IActivator NextActivator
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06005D37 RID: 23863 RVA: 0x001480F1 File Offset: 0x001462F1
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.Construction;
			}
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x001480F4 File Offset: 0x001462F4
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			ctorMsg.Activator = ctorMsg.Activator.NextActivator;
			return ActivationServices.DoServerContextActivation(ctorMsg);
		}
	}
}
