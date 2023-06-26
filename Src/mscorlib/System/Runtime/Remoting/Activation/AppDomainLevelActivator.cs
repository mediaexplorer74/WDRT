using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000892 RID: 2194
	[Serializable]
	internal class AppDomainLevelActivator : IActivator
	{
		// Token: 0x06005D28 RID: 23848 RVA: 0x00147FFA File Offset: 0x001461FA
		internal AppDomainLevelActivator(string remActivatorURL)
		{
			this.m_RemActivatorURL = remActivatorURL;
		}

		// Token: 0x06005D29 RID: 23849 RVA: 0x00148009 File Offset: 0x00146209
		internal AppDomainLevelActivator(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_NextActivator = (IActivator)info.GetValue("m_NextActivator", typeof(IActivator));
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06005D2A RID: 23850 RVA: 0x0014803F File Offset: 0x0014623F
		// (set) Token: 0x06005D2B RID: 23851 RVA: 0x00148047 File Offset: 0x00146247
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

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06005D2C RID: 23852 RVA: 0x00148050 File Offset: 0x00146250
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x00148054 File Offset: 0x00146254
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			ctorMsg.Activator = this.m_NextActivator;
			return ActivationServices.GetActivator().Activate(ctorMsg);
		}

		// Token: 0x040029F6 RID: 10742
		private IActivator m_NextActivator;

		// Token: 0x040029F7 RID: 10743
		private string m_RemActivatorURL;
	}
}
