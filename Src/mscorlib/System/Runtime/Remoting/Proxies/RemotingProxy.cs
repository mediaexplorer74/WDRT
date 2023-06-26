using System;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x02000800 RID: 2048
	[SecurityCritical]
	internal class RemotingProxy : RealProxy, IRemotingTypeInfo
	{
		// Token: 0x0600586F RID: 22639 RVA: 0x00138E52 File Offset: 0x00137052
		public RemotingProxy(Type serverType)
			: base(serverType)
		{
		}

		// Token: 0x06005870 RID: 22640 RVA: 0x00138E5B File Offset: 0x0013705B
		private RemotingProxy()
		{
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06005871 RID: 22641 RVA: 0x00138E63 File Offset: 0x00137063
		// (set) Token: 0x06005872 RID: 22642 RVA: 0x00138E6B File Offset: 0x0013706B
		internal int CtorThread
		{
			get
			{
				return this._ctorThread;
			}
			set
			{
				this._ctorThread = value;
			}
		}

		// Token: 0x06005873 RID: 22643 RVA: 0x00138E74 File Offset: 0x00137074
		internal static IMessage CallProcessMessage(IMessageSink ms, IMessage reqMsg, ArrayWithSize proxySinks, Thread currentThread, Context currentContext, bool bSkippingContextChain)
		{
			if (proxySinks != null)
			{
				DynamicPropertyHolder.NotifyDynamicSinks(reqMsg, proxySinks, true, true, false);
			}
			bool flag = false;
			if (bSkippingContextChain)
			{
				flag = currentContext.NotifyDynamicSinks(reqMsg, true, true, false, true);
				ChannelServices.NotifyProfiler(reqMsg, RemotingProfilerEvent.ClientSend);
			}
			if (ms == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_NoChannelSink"));
			}
			IMessage message = ms.SyncProcessMessage(reqMsg);
			if (bSkippingContextChain)
			{
				ChannelServices.NotifyProfiler(message, RemotingProfilerEvent.ClientReceive);
				if (flag)
				{
					currentContext.NotifyDynamicSinks(message, true, false, false, true);
				}
			}
			IMethodReturnMessage methodReturnMessage = message as IMethodReturnMessage;
			if (message == null || methodReturnMessage == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			}
			if (proxySinks != null)
			{
				DynamicPropertyHolder.NotifyDynamicSinks(message, proxySinks, true, false, false);
			}
			return message;
		}

		// Token: 0x06005874 RID: 22644 RVA: 0x00138F0C File Offset: 0x0013710C
		[SecurityCritical]
		public override IMessage Invoke(IMessage reqMsg)
		{
			IConstructionCallMessage constructionCallMessage = reqMsg as IConstructionCallMessage;
			if (constructionCallMessage != null)
			{
				return this.InternalActivate(constructionCallMessage);
			}
			if (!base.Initialized)
			{
				if (this.CtorThread != Thread.CurrentThread.GetHashCode())
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_InvalidCall"));
				}
				ServerIdentity serverIdentity = this.IdentityObject as ServerIdentity;
				RemotingServices.Wrap((ContextBoundObject)base.UnwrappedServerObject);
			}
			int num = 0;
			Message message = reqMsg as Message;
			if (message != null)
			{
				num = message.GetCallType();
			}
			return this.InternalInvoke((IMethodCallMessage)reqMsg, false, num);
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x00138F98 File Offset: 0x00137198
		internal virtual IMessage InternalInvoke(IMethodCallMessage reqMcmMsg, bool useDispatchMessage, int callType)
		{
			Message message = reqMcmMsg as Message;
			if (message == null && callType != 0)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_InvalidCallType"));
			}
			IMessage message2 = null;
			Thread currentThread = Thread.CurrentThread;
			LogicalCallContext logicalCallContext = currentThread.GetMutableExecutionContext().LogicalCallContext;
			Identity identityObject = this.IdentityObject;
			ServerIdentity serverIdentity = identityObject as ServerIdentity;
			if (serverIdentity != null && identityObject.IsFullyDisconnected())
			{
				throw new ArgumentException(Environment.GetResourceString("Remoting_ServerObjectNotFound", new object[] { reqMcmMsg.Uri }));
			}
			MethodBase methodBase = reqMcmMsg.MethodBase;
			if (RemotingProxy._getTypeMethod == methodBase)
			{
				Type proxiedType = base.GetProxiedType();
				return new ReturnMessage(proxiedType, null, 0, logicalCallContext, reqMcmMsg);
			}
			if (RemotingProxy._getHashCodeMethod == methodBase)
			{
				int hashCode = identityObject.GetHashCode();
				return new ReturnMessage(hashCode, null, 0, logicalCallContext, reqMcmMsg);
			}
			if (identityObject.ChannelSink == null)
			{
				IMessageSink messageSink = null;
				IMessageSink messageSink2 = null;
				if (!identityObject.ObjectRef.IsObjRefLite())
				{
					RemotingServices.CreateEnvoyAndChannelSinks(null, identityObject.ObjectRef, out messageSink, out messageSink2);
				}
				else
				{
					RemotingServices.CreateEnvoyAndChannelSinks(identityObject.ObjURI, null, out messageSink, out messageSink2);
				}
				RemotingServices.SetEnvoyAndChannelSinks(identityObject, messageSink, messageSink2);
				if (identityObject.ChannelSink == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_NoChannelSink"));
				}
			}
			IInternalMessage internalMessage = (IInternalMessage)reqMcmMsg;
			internalMessage.IdentityObject = identityObject;
			if (serverIdentity != null)
			{
				internalMessage.ServerIdentityObject = serverIdentity;
			}
			else
			{
				internalMessage.SetURI(identityObject.URI);
			}
			switch (callType)
			{
			case 0:
			{
				bool flag = false;
				Context currentContextInternal = currentThread.GetCurrentContextInternal();
				IMessageSink messageSink3 = identityObject.EnvoyChain;
				if (currentContextInternal.IsDefaultContext && messageSink3 is EnvoyTerminatorSink)
				{
					flag = true;
					messageSink3 = identityObject.ChannelSink;
				}
				message2 = RemotingProxy.CallProcessMessage(messageSink3, reqMcmMsg, identityObject.ProxySideDynamicSinks, currentThread, currentContextInternal, flag);
				break;
			}
			case 1:
			case 9:
			{
				logicalCallContext = (LogicalCallContext)logicalCallContext.Clone();
				internalMessage.SetCallContext(logicalCallContext);
				AsyncResult asyncResult = new AsyncResult(message);
				this.InternalInvokeAsync(asyncResult, message, useDispatchMessage, callType);
				message2 = new ReturnMessage(asyncResult, null, 0, null, message);
				break;
			}
			case 2:
				message2 = RealProxy.EndInvokeHelper(message, true);
				break;
			case 8:
				logicalCallContext = (LogicalCallContext)logicalCallContext.Clone();
				internalMessage.SetCallContext(logicalCallContext);
				this.InternalInvokeAsync(null, message, useDispatchMessage, callType);
				message2 = new ReturnMessage(null, null, 0, null, reqMcmMsg);
				break;
			case 10:
				message2 = new ReturnMessage(null, null, 0, null, reqMcmMsg);
				break;
			}
			return message2;
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x001391F4 File Offset: 0x001373F4
		internal void InternalInvokeAsync(IMessageSink ar, Message reqMsg, bool useDispatchMessage, int callType)
		{
			Identity identityObject = this.IdentityObject;
			ServerIdentity serverIdentity = identityObject as ServerIdentity;
			MethodCall methodCall = new MethodCall(reqMsg);
			IInternalMessage internalMessage = methodCall;
			internalMessage.IdentityObject = identityObject;
			if (serverIdentity != null)
			{
				internalMessage.ServerIdentityObject = serverIdentity;
			}
			if (useDispatchMessage)
			{
				IMessageCtrl messageCtrl = ChannelServices.AsyncDispatchMessage(methodCall, ((callType & 8) != 0) ? null : ar);
			}
			else
			{
				if (identityObject.EnvoyChain == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Remoting_Proxy_InvalidState"));
				}
				IMessageCtrl messageCtrl = identityObject.EnvoyChain.AsyncProcessMessage(methodCall, ((callType & 8) != 0) ? null : ar);
			}
			if ((callType & 1) != 0 && (callType & 8) != 0)
			{
				ar.SyncProcessMessage(null);
			}
		}

		// Token: 0x06005877 RID: 22647 RVA: 0x0013928C File Offset: 0x0013748C
		private IConstructionReturnMessage InternalActivate(IConstructionCallMessage ctorMsg)
		{
			this.CtorThread = Thread.CurrentThread.GetHashCode();
			IConstructionReturnMessage constructionReturnMessage = ActivationServices.Activate(this, ctorMsg);
			base.Initialized = true;
			return constructionReturnMessage;
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x001392BC File Offset: 0x001374BC
		private static void Invoke(object NotUsed, ref MessageData msgData)
		{
			Message message = new Message();
			message.InitFields(msgData);
			object thisPtr = message.GetThisPtr();
			Delegate @delegate;
			if ((@delegate = thisPtr as Delegate) == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
			RemotingProxy remotingProxy = (RemotingProxy)RemotingServices.GetRealProxy(@delegate.Target);
			if (remotingProxy != null)
			{
				remotingProxy.InternalInvoke(message, true, message.GetCallType());
				return;
			}
			int callType = message.GetCallType();
			if (callType <= 2)
			{
				if (callType != 1)
				{
					if (callType != 2)
					{
						return;
					}
					RealProxy.EndInvokeHelper(message, false);
					return;
				}
			}
			else if (callType != 9)
			{
				return;
			}
			message.Properties[Message.CallContextKey] = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Clone();
			AsyncResult asyncResult = new AsyncResult(message);
			AgileAsyncWorkerItem agileAsyncWorkerItem = new AgileAsyncWorkerItem(message, ((callType & 8) != 0) ? null : asyncResult, @delegate.Target);
			ThreadPool.QueueUserWorkItem(new WaitCallback(AgileAsyncWorkerItem.ThreadPoolCallBack), agileAsyncWorkerItem);
			if ((callType & 8) != 0)
			{
				asyncResult.SyncProcessMessage(null);
			}
			message.PropagateOutParameters(null, asyncResult);
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06005879 RID: 22649 RVA: 0x001393C5 File Offset: 0x001375C5
		// (set) Token: 0x0600587A RID: 22650 RVA: 0x001393CD File Offset: 0x001375CD
		internal ConstructorCallMessage ConstructorMessage
		{
			get
			{
				return this._ccm;
			}
			set
			{
				this._ccm = value;
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x0600587B RID: 22651 RVA: 0x001393D6 File Offset: 0x001375D6
		// (set) Token: 0x0600587C RID: 22652 RVA: 0x001393E3 File Offset: 0x001375E3
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				return base.GetProxiedType().FullName;
			}
			[SecurityCritical]
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x001393EC File Offset: 0x001375EC
		[SecurityCritical]
		public override IntPtr GetCOMIUnknown(bool fIsBeingMarshalled)
		{
			IntPtr intPtr = IntPtr.Zero;
			object transparentProxy = this.GetTransparentProxy();
			bool flag = RemotingServices.IsObjectOutOfProcess(transparentProxy);
			if (flag)
			{
				if (fIsBeingMarshalled)
				{
					intPtr = MarshalByRefObject.GetComIUnknown((MarshalByRefObject)transparentProxy);
				}
				else
				{
					intPtr = MarshalByRefObject.GetComIUnknown((MarshalByRefObject)transparentProxy);
				}
			}
			else
			{
				bool flag2 = RemotingServices.IsObjectOutOfAppDomain(transparentProxy);
				if (flag2)
				{
					intPtr = ((MarshalByRefObject)transparentProxy).GetComIUnknown(fIsBeingMarshalled);
				}
				else
				{
					intPtr = MarshalByRefObject.GetComIUnknown((MarshalByRefObject)transparentProxy);
				}
			}
			return intPtr;
		}

		// Token: 0x0600587E RID: 22654 RVA: 0x00139455 File Offset: 0x00137655
		[SecurityCritical]
		public override void SetCOMIUnknown(IntPtr i)
		{
		}

		// Token: 0x0600587F RID: 22655 RVA: 0x00139458 File Offset: 0x00137658
		[SecurityCritical]
		public bool CanCastTo(Type castType, object o)
		{
			if (castType == null)
			{
				throw new ArgumentNullException("castType");
			}
			RuntimeType runtimeType = castType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			bool flag = false;
			if (runtimeType == RemotingProxy.s_typeofObject || runtimeType == RemotingProxy.s_typeofMarshalByRefObject)
			{
				return true;
			}
			ObjRef objectRef = this.IdentityObject.ObjectRef;
			if (objectRef != null)
			{
				object transparentProxy = this.GetTransparentProxy();
				IRemotingTypeInfo typeInfo = objectRef.TypeInfo;
				if (typeInfo != null)
				{
					flag = typeInfo.CanCastTo(runtimeType, transparentProxy);
					if (!flag && typeInfo.GetType() == typeof(TypeInfo) && objectRef.IsWellKnown())
					{
						flag = this.CanCastToWK(runtimeType);
					}
				}
				else if (objectRef.IsObjRefLite())
				{
					flag = MarshalByRefObject.CanCastToXmlTypeHelper(runtimeType, (MarshalByRefObject)o);
				}
			}
			else
			{
				flag = this.CanCastToWK(runtimeType);
			}
			return flag;
		}

		// Token: 0x06005880 RID: 22656 RVA: 0x00139530 File Offset: 0x00137730
		private bool CanCastToWK(Type castType)
		{
			bool flag = false;
			if (castType.IsClass)
			{
				flag = base.GetProxiedType().IsAssignableFrom(castType);
			}
			else if (!(this.IdentityObject is ServerIdentity))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x04002850 RID: 10320
		private static MethodInfo _getTypeMethod = typeof(object).GetMethod("GetType");

		// Token: 0x04002851 RID: 10321
		private static MethodInfo _getHashCodeMethod = typeof(object).GetMethod("GetHashCode");

		// Token: 0x04002852 RID: 10322
		private static RuntimeType s_typeofObject = (RuntimeType)typeof(object);

		// Token: 0x04002853 RID: 10323
		private static RuntimeType s_typeofMarshalByRefObject = (RuntimeType)typeof(MarshalByRefObject);

		// Token: 0x04002854 RID: 10324
		private ConstructorCallMessage _ccm;

		// Token: 0x04002855 RID: 10325
		private int _ctorThread;
	}
}
