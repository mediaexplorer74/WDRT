using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000891 RID: 2193
	internal class ActivationListener : MarshalByRefObject, IActivator
	{
		// Token: 0x06005D22 RID: 23842 RVA: 0x00147F3C File Offset: 0x0014613C
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06005D23 RID: 23843 RVA: 0x00147F3F File Offset: 0x0014613F
		// (set) Token: 0x06005D24 RID: 23844 RVA: 0x00147F42 File Offset: 0x00146142
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

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06005D25 RID: 23845 RVA: 0x00147F49 File Offset: 0x00146149
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x06005D26 RID: 23846 RVA: 0x00147F50 File Offset: 0x00146150
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null || RemotingServices.IsTransparentProxy(ctorMsg))
			{
				throw new ArgumentNullException("ctorMsg");
			}
			ctorMsg.Properties["Permission"] = "allowed";
			string activationTypeName = ctorMsg.ActivationTypeName;
			if (!RemotingConfigHandler.IsActivationAllowed(activationTypeName))
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_PermissionDenied"), ctorMsg.ActivationTypeName));
			}
			Type activationType = ctorMsg.ActivationType;
			if (activationType == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), ctorMsg.ActivationTypeName));
			}
			return ActivationServices.GetActivator().Activate(ctorMsg);
		}
	}
}
