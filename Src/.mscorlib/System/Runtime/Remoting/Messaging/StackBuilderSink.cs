using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200087E RID: 2174
	[Serializable]
	internal class StackBuilderSink : IMessageSink
	{
		// Token: 0x06005C80 RID: 23680 RVA: 0x001457C8 File Offset: 0x001439C8
		public StackBuilderSink(MarshalByRefObject server)
		{
			this._server = server;
		}

		// Token: 0x06005C81 RID: 23681 RVA: 0x001457D7 File Offset: 0x001439D7
		public StackBuilderSink(object server)
		{
			this._server = server;
			if (this._server == null)
			{
				this._bStatic = true;
			}
		}

		// Token: 0x06005C82 RID: 23682 RVA: 0x001457F8 File Offset: 0x001439F8
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage message = InternalSink.ValidateMessage(msg);
			if (message != null)
			{
				return message;
			}
			IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
			LogicalCallContext logicalCallContext = null;
			LogicalCallContext logicalCallContext2 = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			object data = logicalCallContext2.GetData("__xADCall");
			bool flag = false;
			IMessage message2;
			try
			{
				object server = this._server;
				StackBuilderSink.VerifyIsOkToCallMethod(server, methodCallMessage);
				LogicalCallContext logicalCallContext3;
				if (methodCallMessage != null)
				{
					logicalCallContext3 = methodCallMessage.LogicalCallContext;
				}
				else
				{
					logicalCallContext3 = (LogicalCallContext)msg.Properties["__CallContext"];
				}
				logicalCallContext = CallContext.SetLogicalCallContext(logicalCallContext3);
				flag = true;
				logicalCallContext3.PropagateIncomingHeadersToCallContext(msg);
				StackBuilderSink.PreserveThreadPrincipalIfNecessary(logicalCallContext3, logicalCallContext);
				if (this.IsOKToStackBlt(methodCallMessage, server) && ((Message)methodCallMessage).Dispatch(server))
				{
					message2 = new StackBasedReturnMessage();
					((StackBasedReturnMessage)message2).InitFields((Message)methodCallMessage);
					LogicalCallContext logicalCallContext4 = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
					logicalCallContext4.PropagateOutgoingHeadersToMessage(message2);
					((StackBasedReturnMessage)message2).SetLogicalCallContext(logicalCallContext4);
				}
				else
				{
					MethodBase methodBase = StackBuilderSink.GetMethodBase(methodCallMessage);
					object[] array = null;
					RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(methodBase);
					object[] array2 = Message.CoerceArgs(methodCallMessage, reflectionCachedData.Parameters);
					object obj = this.PrivateProcessMessage(methodBase.MethodHandle, array2, server, out array);
					this.CopyNonByrefOutArgsFromOriginalArgs(reflectionCachedData, array2, ref array);
					LogicalCallContext logicalCallContext5 = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
					if (data != null && (bool)data && logicalCallContext5 != null)
					{
						logicalCallContext5.RemovePrincipalIfNotSerializable();
					}
					message2 = new ReturnMessage(obj, array, (array == null) ? 0 : array.Length, logicalCallContext5, methodCallMessage);
					logicalCallContext5.PropagateOutgoingHeadersToMessage(message2);
					CallContext.SetLogicalCallContext(logicalCallContext);
				}
			}
			catch (Exception ex)
			{
				message2 = new ReturnMessage(ex, methodCallMessage);
				((ReturnMessage)message2).SetLogicalCallContext(methodCallMessage.LogicalCallContext);
				if (flag)
				{
					CallContext.SetLogicalCallContext(logicalCallContext);
				}
			}
			return message2;
		}

		// Token: 0x06005C83 RID: 23683 RVA: 0x001459CC File Offset: 0x00143BCC
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)msg;
			IMessageCtrl messageCtrl = null;
			IMessage message = null;
			LogicalCallContext logicalCallContext = null;
			bool flag = false;
			try
			{
				try
				{
					LogicalCallContext logicalCallContext2 = (LogicalCallContext)methodCallMessage.Properties[Message.CallContextKey];
					object server = this._server;
					StackBuilderSink.VerifyIsOkToCallMethod(server, methodCallMessage);
					logicalCallContext = CallContext.SetLogicalCallContext(logicalCallContext2);
					flag = true;
					logicalCallContext2.PropagateIncomingHeadersToCallContext(msg);
					StackBuilderSink.PreserveThreadPrincipalIfNecessary(logicalCallContext2, logicalCallContext);
					ServerChannelSinkStack serverChannelSinkStack = msg.Properties["__SinkStack"] as ServerChannelSinkStack;
					if (serverChannelSinkStack != null)
					{
						serverChannelSinkStack.ServerObject = server;
					}
					MethodBase methodBase = StackBuilderSink.GetMethodBase(methodCallMessage);
					object[] array = null;
					RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(methodBase);
					object[] array2 = Message.CoerceArgs(methodCallMessage, reflectionCachedData.Parameters);
					object obj = this.PrivateProcessMessage(methodBase.MethodHandle, array2, server, out array);
					this.CopyNonByrefOutArgsFromOriginalArgs(reflectionCachedData, array2, ref array);
					if (replySink != null)
					{
						LogicalCallContext logicalCallContext3 = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
						if (logicalCallContext3 != null)
						{
							logicalCallContext3.RemovePrincipalIfNotSerializable();
						}
						message = new ReturnMessage(obj, array, (array == null) ? 0 : array.Length, logicalCallContext3, methodCallMessage);
						logicalCallContext3.PropagateOutgoingHeadersToMessage(message);
					}
				}
				catch (Exception ex)
				{
					if (replySink != null)
					{
						message = new ReturnMessage(ex, methodCallMessage);
						((ReturnMessage)message).SetLogicalCallContext((LogicalCallContext)methodCallMessage.Properties[Message.CallContextKey]);
					}
				}
				finally
				{
					if (replySink != null)
					{
						replySink.SyncProcessMessage(message);
					}
				}
			}
			finally
			{
				if (flag)
				{
					CallContext.SetLogicalCallContext(logicalCallContext);
				}
			}
			return messageCtrl;
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06005C84 RID: 23684 RVA: 0x00145B70 File Offset: 0x00143D70
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x06005C85 RID: 23685 RVA: 0x00145B74 File Offset: 0x00143D74
		[SecurityCritical]
		internal bool IsOKToStackBlt(IMethodMessage mcMsg, object server)
		{
			bool flag = false;
			Message message = mcMsg as Message;
			if (message != null)
			{
				IInternalMessage internalMessage = message;
				if (message.GetFramePtr() != IntPtr.Zero && message.GetThisPtr() == server && (internalMessage.IdentityObject == null || (internalMessage.IdentityObject != null && internalMessage.IdentityObject == internalMessage.ServerIdentityObject)))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06005C86 RID: 23686 RVA: 0x00145BCC File Offset: 0x00143DCC
		[SecurityCritical]
		private static MethodBase GetMethodBase(IMethodMessage msg)
		{
			MethodBase methodBase = msg.MethodBase;
			if (null == methodBase)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), msg.MethodName, msg.TypeName));
			}
			return methodBase;
		}

		// Token: 0x06005C87 RID: 23687 RVA: 0x00145C10 File Offset: 0x00143E10
		[SecurityCritical]
		private static void VerifyIsOkToCallMethod(object server, IMethodMessage msg)
		{
			bool flag = false;
			MarshalByRefObject marshalByRefObject = server as MarshalByRefObject;
			if (marshalByRefObject != null)
			{
				bool flag2;
				Identity identity = MarshalByRefObject.GetIdentity(marshalByRefObject, out flag2);
				if (identity != null)
				{
					ServerIdentity serverIdentity = identity as ServerIdentity;
					if (serverIdentity != null && serverIdentity.MarshaledAsSpecificType)
					{
						Type serverType = serverIdentity.ServerType;
						if (serverType != null)
						{
							MethodBase methodBase = StackBuilderSink.GetMethodBase(msg);
							RuntimeType runtimeType = (RuntimeType)methodBase.DeclaringType;
							if (runtimeType != serverType && !runtimeType.IsAssignableFrom(serverType))
							{
								throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_InvalidCallingType"), methodBase.DeclaringType.FullName, serverType.FullName));
							}
							if (runtimeType.IsInterface)
							{
								StackBuilderSink.VerifyNotIRemoteDispatch(runtimeType);
							}
							flag = true;
						}
					}
				}
				if (!flag)
				{
					MethodBase methodBase2 = StackBuilderSink.GetMethodBase(msg);
					RuntimeType runtimeType2 = (RuntimeType)methodBase2.ReflectedType;
					if (!runtimeType2.IsInterface)
					{
						if (!runtimeType2.IsInstanceOfType(marshalByRefObject))
						{
							throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_InvalidCallingType"), runtimeType2.FullName, marshalByRefObject.GetType().FullName));
						}
					}
					else
					{
						StackBuilderSink.VerifyNotIRemoteDispatch(runtimeType2);
					}
				}
			}
		}

		// Token: 0x06005C88 RID: 23688 RVA: 0x00145D30 File Offset: 0x00143F30
		[SecurityCritical]
		private static void VerifyNotIRemoteDispatch(RuntimeType reflectedType)
		{
			if (reflectedType.FullName.Equals(StackBuilderSink.sIRemoteDispatch) && reflectedType.GetRuntimeAssembly().GetSimpleName().Equals(StackBuilderSink.sIRemoteDispatchAssembly))
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_CantInvokeIRemoteDispatch"));
			}
		}

		// Token: 0x06005C89 RID: 23689 RVA: 0x00145D6C File Offset: 0x00143F6C
		internal void CopyNonByrefOutArgsFromOriginalArgs(RemotingMethodCachedData methodCache, object[] args, ref object[] marshalResponseArgs)
		{
			int[] nonRefOutArgMap = methodCache.NonRefOutArgMap;
			if (nonRefOutArgMap.Length != 0)
			{
				if (marshalResponseArgs == null)
				{
					marshalResponseArgs = new object[methodCache.Parameters.Length];
				}
				foreach (int num in nonRefOutArgMap)
				{
					marshalResponseArgs[num] = args[num];
				}
			}
		}

		// Token: 0x06005C8A RID: 23690 RVA: 0x00145DB4 File Offset: 0x00143FB4
		[SecurityCritical]
		internal static void PreserveThreadPrincipalIfNecessary(LogicalCallContext messageCallContext, LogicalCallContext threadCallContext)
		{
			if (threadCallContext != null && messageCallContext.Principal == null)
			{
				IPrincipal principal = threadCallContext.Principal;
				if (principal != null)
				{
					messageCallContext.Principal = principal;
				}
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06005C8B RID: 23691 RVA: 0x00145DDD File Offset: 0x00143FDD
		internal object ServerObject
		{
			get
			{
				return this._server;
			}
		}

		// Token: 0x06005C8C RID: 23692
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object _PrivateProcessMessage(IntPtr md, object[] args, object server, out object[] outArgs);

		// Token: 0x06005C8D RID: 23693 RVA: 0x00145DE5 File Offset: 0x00143FE5
		[SecurityCritical]
		public object PrivateProcessMessage(RuntimeMethodHandle md, object[] args, object server, out object[] outArgs)
		{
			return this._PrivateProcessMessage(md.Value, args, server, out outArgs);
		}

		// Token: 0x040029CB RID: 10699
		private object _server;

		// Token: 0x040029CC RID: 10700
		private static string sIRemoteDispatch = "System.EnterpriseServices.IRemoteDispatch";

		// Token: 0x040029CD RID: 10701
		private static string sIRemoteDispatchAssembly = "System.EnterpriseServices";

		// Token: 0x040029CE RID: 10702
		private bool _bStatic;
	}
}
