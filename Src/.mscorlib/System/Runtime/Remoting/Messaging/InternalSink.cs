using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200087F RID: 2175
	[Serializable]
	internal class InternalSink
	{
		// Token: 0x06005C8F RID: 23695 RVA: 0x00145E10 File Offset: 0x00144010
		[SecurityCritical]
		internal static IMessage ValidateMessage(IMessage reqMsg)
		{
			IMessage message = null;
			if (reqMsg == null)
			{
				message = new ReturnMessage(new ArgumentNullException("reqMsg"), null);
			}
			return message;
		}

		// Token: 0x06005C90 RID: 23696 RVA: 0x00145E34 File Offset: 0x00144034
		[SecurityCritical]
		internal static IMessage DisallowAsyncActivation(IMessage reqMsg)
		{
			if (reqMsg is IConstructionCallMessage)
			{
				return new ReturnMessage(new RemotingException(Environment.GetResourceString("Remoting_Activation_AsyncUnsupported")), null);
			}
			return null;
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x00145E58 File Offset: 0x00144058
		[SecurityCritical]
		internal static Identity GetIdentity(IMessage reqMsg)
		{
			Identity identity = null;
			if (reqMsg is IInternalMessage)
			{
				identity = ((IInternalMessage)reqMsg).IdentityObject;
			}
			else if (reqMsg is InternalMessageWrapper)
			{
				identity = (Identity)((InternalMessageWrapper)reqMsg).GetIdentityObject();
			}
			if (identity == null)
			{
				string uri = InternalSink.GetURI(reqMsg);
				identity = IdentityHolder.ResolveIdentity(uri);
				if (identity == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Remoting_ServerObjectNotFound", new object[] { uri }));
				}
			}
			return identity;
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x00145EC8 File Offset: 0x001440C8
		[SecurityCritical]
		internal static ServerIdentity GetServerIdentity(IMessage reqMsg)
		{
			ServerIdentity serverIdentity = null;
			bool flag = false;
			IInternalMessage internalMessage = reqMsg as IInternalMessage;
			if (internalMessage != null)
			{
				serverIdentity = ((IInternalMessage)reqMsg).ServerIdentityObject;
				flag = true;
			}
			else if (reqMsg is InternalMessageWrapper)
			{
				serverIdentity = (ServerIdentity)((InternalMessageWrapper)reqMsg).GetServerIdentityObject();
			}
			if (serverIdentity == null)
			{
				string uri = InternalSink.GetURI(reqMsg);
				Identity identity = IdentityHolder.ResolveIdentity(uri);
				if (identity is ServerIdentity)
				{
					serverIdentity = (ServerIdentity)identity;
					if (flag)
					{
						internalMessage.ServerIdentityObject = serverIdentity;
					}
				}
			}
			return serverIdentity;
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x00145F3C File Offset: 0x0014413C
		[SecurityCritical]
		internal static string GetURI(IMessage msg)
		{
			string text = null;
			IMethodMessage methodMessage = msg as IMethodMessage;
			if (methodMessage != null)
			{
				text = methodMessage.Uri;
			}
			else
			{
				IDictionary properties = msg.Properties;
				if (properties != null)
				{
					text = (string)properties["__Uri"];
				}
			}
			return text;
		}
	}
}
