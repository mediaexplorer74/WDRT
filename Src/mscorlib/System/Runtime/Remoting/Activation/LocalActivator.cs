using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000890 RID: 2192
	[SecurityCritical]
	internal class LocalActivator : ContextAttribute, IActivator
	{
		// Token: 0x06005D19 RID: 23833 RVA: 0x00147BE2 File Offset: 0x00145DE2
		internal LocalActivator()
			: base("RemoteActivationService.rem")
		{
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x00147BF0 File Offset: 0x00145DF0
		[SecurityCritical]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
		{
			if (RemotingConfigHandler.Info == null)
			{
				return true;
			}
			RuntimeType runtimeType = ctorMsg.ActivationType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			WellKnownClientTypeEntry wellKnownClientTypeEntry = RemotingConfigHandler.IsWellKnownClientType(runtimeType);
			string text = ((wellKnownClientTypeEntry == null) ? null : wellKnownClientTypeEntry.ObjectUrl);
			if (text != null)
			{
				ctorMsg.Properties["Connect"] = text;
				return false;
			}
			ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfigHandler.IsRemotelyActivatedClientType(runtimeType);
			string text2 = null;
			if (activatedClientTypeEntry == null)
			{
				object[] callSiteActivationAttributes = ctorMsg.CallSiteActivationAttributes;
				if (callSiteActivationAttributes != null)
				{
					for (int i = 0; i < callSiteActivationAttributes.Length; i++)
					{
						UrlAttribute urlAttribute = callSiteActivationAttributes[i] as UrlAttribute;
						if (urlAttribute != null)
						{
							text2 = urlAttribute.UrlValue;
						}
					}
				}
				if (text2 == null)
				{
					return true;
				}
			}
			else
			{
				text2 = activatedClientTypeEntry.ApplicationUrl;
			}
			string text3;
			if (!text2.EndsWith("/", StringComparison.Ordinal))
			{
				text3 = text2 + "/RemoteActivationService.rem";
			}
			else
			{
				text3 = text2 + "RemoteActivationService.rem";
			}
			ctorMsg.Properties["Remote"] = text3;
			return false;
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x00147CEC File Offset: 0x00145EEC
		[SecurityCritical]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg.Properties.Contains("Remote"))
			{
				string text = (string)ctorMsg.Properties["Remote"];
				AppDomainLevelActivator appDomainLevelActivator = new AppDomainLevelActivator(text);
				IActivator activator = ctorMsg.Activator;
				if (activator.Level < ActivatorLevel.AppDomain)
				{
					appDomainLevelActivator.NextActivator = activator;
					ctorMsg.Activator = appDomainLevelActivator;
					return;
				}
				if (activator.NextActivator == null)
				{
					activator.NextActivator = appDomainLevelActivator;
					return;
				}
				while (activator.NextActivator.Level >= ActivatorLevel.AppDomain)
				{
					activator = activator.NextActivator;
				}
				appDomainLevelActivator.NextActivator = activator.NextActivator;
				activator.NextActivator = appDomainLevelActivator;
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06005D1C RID: 23836 RVA: 0x00147D81 File Offset: 0x00145F81
		// (set) Token: 0x06005D1D RID: 23837 RVA: 0x00147D84 File Offset: 0x00145F84
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

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06005D1E RID: 23838 RVA: 0x00147D8B File Offset: 0x00145F8B
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x06005D1F RID: 23839 RVA: 0x00147D90 File Offset: 0x00145F90
		private static MethodBase GetMethodBase(IConstructionCallMessage msg)
		{
			MethodBase methodBase = msg.MethodBase;
			if (null == methodBase)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), msg.MethodName, msg.TypeName));
			}
			return methodBase;
		}

		// Token: 0x06005D20 RID: 23840 RVA: 0x00147DD4 File Offset: 0x00145FD4
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			if (ctorMsg.Properties.Contains("Remote"))
			{
				return LocalActivator.DoRemoteActivation(ctorMsg);
			}
			if (ctorMsg.Properties.Contains("Permission"))
			{
				Type activationType = ctorMsg.ActivationType;
				object[] array = null;
				if (activationType.IsContextful)
				{
					IList contextProperties = ctorMsg.ContextProperties;
					if (contextProperties != null && contextProperties.Count > 0)
					{
						RemotePropertyHolderAttribute remotePropertyHolderAttribute = new RemotePropertyHolderAttribute(contextProperties);
						array = new object[] { remotePropertyHolderAttribute };
					}
				}
				MethodBase methodBase = LocalActivator.GetMethodBase(ctorMsg);
				RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(methodBase);
				object[] array2 = Message.CoerceArgs(ctorMsg, reflectionCachedData.Parameters);
				object obj = Activator.CreateInstance(activationType, array2, array);
				if (RemotingServices.IsClientProxy(obj))
				{
					RedirectionProxy redirectionProxy = new RedirectionProxy((MarshalByRefObject)obj, activationType);
					RemotingServices.MarshalInternal(redirectionProxy, null, activationType);
					obj = redirectionProxy;
				}
				return ActivationServices.SetupConstructionReply(obj, ctorMsg, null);
			}
			return ctorMsg.Activator.Activate(ctorMsg);
		}

		// Token: 0x06005D21 RID: 23841 RVA: 0x00147EBC File Offset: 0x001460BC
		internal static IConstructionReturnMessage DoRemoteActivation(IConstructionCallMessage ctorMsg)
		{
			IActivator activator = null;
			string text = (string)ctorMsg.Properties["Remote"];
			try
			{
				activator = (IActivator)RemotingServices.Connect(typeof(IActivator), text);
			}
			catch (Exception ex)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_ConnectFailed"), ex));
			}
			ctorMsg.Properties.Remove("Remote");
			return activator.Activate(ctorMsg);
		}
	}
}
