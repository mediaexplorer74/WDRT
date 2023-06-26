using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000893 RID: 2195
	[Serializable]
	internal class ContextLevelActivator : IActivator
	{
		// Token: 0x06005D2E RID: 23854 RVA: 0x0014806D File Offset: 0x0014626D
		internal ContextLevelActivator()
		{
			this.m_NextActivator = null;
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x0014807C File Offset: 0x0014627C
		internal ContextLevelActivator(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_NextActivator = (IActivator)info.GetValue("m_NextActivator", typeof(IActivator));
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06005D30 RID: 23856 RVA: 0x001480B2 File Offset: 0x001462B2
		// (set) Token: 0x06005D31 RID: 23857 RVA: 0x001480BA File Offset: 0x001462BA
		public virtual IActivator NextActivator
		{
			[SecurityCritical]
			get
			{
				return this.m_NextActivator;
			}
			[SecurityCritical]
			set
			{
				this.m_NextActivator = value;
			}
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06005D32 RID: 23858 RVA: 0x001480C3 File Offset: 0x001462C3
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.Context;
			}
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x001480C6 File Offset: 0x001462C6
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			ctorMsg.Activator = ctorMsg.Activator.NextActivator;
			return ActivationServices.DoCrossContextActivation(ctorMsg);
		}

		// Token: 0x040029F8 RID: 10744
		private IActivator m_NextActivator;
	}
}
