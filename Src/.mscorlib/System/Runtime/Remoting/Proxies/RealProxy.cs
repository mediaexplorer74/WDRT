using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Proxies
{
	/// <summary>Provides base functionality for proxies.</summary>
	// Token: 0x020007FF RID: 2047
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public abstract class RealProxy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> class that represents a remote object of the specified <see cref="T:System.Type" />.</summary>
		/// <param name="classToProxy">The <see cref="T:System.Type" /> of the remote object for which to create a proxy.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="classToProxy" /> is not an interface, and is not derived from <see cref="T:System.MarshalByRefObject" />.</exception>
		// Token: 0x06005849 RID: 22601 RVA: 0x00138370 File Offset: 0x00136570
		[SecurityCritical]
		protected RealProxy(Type classToProxy)
			: this(classToProxy, (IntPtr)0, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> class.</summary>
		/// <param name="classToProxy">The <see cref="T:System.Type" /> of the remote object for which to create a proxy.</param>
		/// <param name="stub">A stub to associate with the new proxy instance.</param>
		/// <param name="stubData">The stub data to set for the specified stub and the new proxy instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="classToProxy" /> is not an interface, and is not derived from <see cref="T:System.MarshalByRefObject" />.</exception>
		// Token: 0x0600584A RID: 22602 RVA: 0x00138380 File Offset: 0x00136580
		[SecurityCritical]
		protected RealProxy(Type classToProxy, IntPtr stub, object stubData)
		{
			if (!classToProxy.IsMarshalByRef && !classToProxy.IsInterface)
			{
				throw new ArgumentException(Environment.GetResourceString("Remoting_Proxy_ProxyTypeIsNotMBR"));
			}
			if ((IntPtr)0 == stub)
			{
				stub = RealProxy._defaultStub;
				stubData = RealProxy._defaultStubData;
			}
			this._tp = null;
			if (stubData == null)
			{
				throw new ArgumentNullException("stubdata");
			}
			this._tp = RemotingServices.CreateTransparentProxy(this, classToProxy, stub, stubData);
			RemotingProxy remotingProxy = this as RemotingProxy;
			if (remotingProxy != null)
			{
				this._flags |= RealProxyFlags.RemotingProxy;
			}
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x0013840B File Offset: 0x0013660B
		internal bool IsRemotingProxy()
		{
			return (this._flags & RealProxyFlags.RemotingProxy) == RealProxyFlags.RemotingProxy;
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x0600584C RID: 22604 RVA: 0x00138418 File Offset: 0x00136618
		// (set) Token: 0x0600584D RID: 22605 RVA: 0x00138425 File Offset: 0x00136625
		internal bool Initialized
		{
			get
			{
				return (this._flags & RealProxyFlags.Initialized) == RealProxyFlags.Initialized;
			}
			set
			{
				if (value)
				{
					this._flags |= RealProxyFlags.Initialized;
					return;
				}
				this._flags &= ~RealProxyFlags.Initialized;
			}
		}

		/// <summary>Initializes a new instance of the object <see cref="T:System.Type" /> of the remote object that the current instance of <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> represents with the specified <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.</summary>
		/// <param name="ctorMsg">A construction call message that contains the constructor parameters for the new instance of the remote object that is represented by the current <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" />. Can be <see langword="null" />.</param>
		/// <returns>The result of the construction request.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have UnmanagedCode permission.</exception>
		// Token: 0x0600584E RID: 22606 RVA: 0x00138448 File Offset: 0x00136648
		[SecurityCritical]
		[ComVisible(true)]
		public IConstructionReturnMessage InitializeServerObject(IConstructionCallMessage ctorMsg)
		{
			IConstructionReturnMessage constructionReturnMessage = null;
			if (this._serverObject == null)
			{
				Type proxiedType = this.GetProxiedType();
				if (ctorMsg != null && ctorMsg.ActivationType != proxiedType)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Proxy_BadTypeForActivation"), proxiedType.FullName, ctorMsg.ActivationType));
				}
				this._serverObject = RemotingServices.AllocateUninitializedObject(proxiedType);
				this.SetContextForDefaultStub();
				MarshalByRefObject marshalByRefObject = (MarshalByRefObject)this.GetTransparentProxy();
				IMethodReturnMessage methodReturnMessage = null;
				Exception ex = null;
				if (ctorMsg != null)
				{
					methodReturnMessage = RemotingServices.ExecuteMessage(marshalByRefObject, ctorMsg);
					ex = methodReturnMessage.Exception;
				}
				else
				{
					try
					{
						RemotingServices.CallDefaultCtor(marshalByRefObject);
					}
					catch (Exception ex2)
					{
						ex = ex2;
					}
				}
				if (ex == null)
				{
					object[] array = ((methodReturnMessage == null) ? null : methodReturnMessage.OutArgs);
					int num = ((array == null) ? 0 : array.Length);
					LogicalCallContext logicalCallContext = ((methodReturnMessage == null) ? null : methodReturnMessage.LogicalCallContext);
					constructionReturnMessage = new ConstructorReturnMessage(marshalByRefObject, array, num, logicalCallContext, ctorMsg);
					this.SetupIdentity();
					if (this.IsRemotingProxy())
					{
						((RemotingProxy)this).Initialized = true;
					}
				}
				else
				{
					constructionReturnMessage = new ConstructorReturnMessage(ex, ctorMsg);
				}
			}
			return constructionReturnMessage;
		}

		/// <summary>Returns the server object that is represented by the current proxy instance.</summary>
		/// <returns>The server object that is represented by the current proxy instance.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have UnmanagedCode permission.</exception>
		// Token: 0x0600584F RID: 22607 RVA: 0x0013855C File Offset: 0x0013675C
		[SecurityCritical]
		protected MarshalByRefObject GetUnwrappedServer()
		{
			return this.UnwrappedServerObject;
		}

		/// <summary>Detaches the current proxy instance from the remote server object that it represents.</summary>
		/// <returns>The detached server object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have UnmanagedCode permission.</exception>
		// Token: 0x06005850 RID: 22608 RVA: 0x00138564 File Offset: 0x00136764
		[SecurityCritical]
		protected MarshalByRefObject DetachServer()
		{
			object transparentProxy = this.GetTransparentProxy();
			if (transparentProxy != null)
			{
				RemotingServices.ResetInterfaceCache(transparentProxy);
			}
			MarshalByRefObject serverObject = this._serverObject;
			this._serverObject = null;
			serverObject.__ResetServerIdentity();
			return serverObject;
		}

		/// <summary>Attaches the current proxy instance to the specified remote <see cref="T:System.MarshalByRefObject" />.</summary>
		/// <param name="s">The <see cref="T:System.MarshalByRefObject" /> that the current proxy instance represents.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have UnmanagedCode permission.</exception>
		// Token: 0x06005851 RID: 22609 RVA: 0x00138598 File Offset: 0x00136798
		[SecurityCritical]
		protected void AttachServer(MarshalByRefObject s)
		{
			object transparentProxy = this.GetTransparentProxy();
			if (transparentProxy != null)
			{
				RemotingServices.ResetInterfaceCache(transparentProxy);
			}
			this.AttachServerHelper(s);
		}

		// Token: 0x06005852 RID: 22610 RVA: 0x001385BC File Offset: 0x001367BC
		[SecurityCritical]
		private void SetupIdentity()
		{
			if (this._identity == null)
			{
				this._identity = IdentityHolder.FindOrCreateServerIdentity(this._serverObject, null, 0);
				((Identity)this._identity).RaceSetTransparentProxy(this.GetTransparentProxy());
			}
		}

		// Token: 0x06005853 RID: 22611 RVA: 0x001385F0 File Offset: 0x001367F0
		[SecurityCritical]
		private void SetContextForDefaultStub()
		{
			if (this.GetStub() == RealProxy._defaultStub)
			{
				object stubData = RealProxy.GetStubData(this);
				if (stubData is IntPtr && ((IntPtr)stubData).Equals(RealProxy._defaultStubValue))
				{
					RealProxy.SetStubData(this, Thread.CurrentContext.InternalContextID);
				}
			}
		}

		// Token: 0x06005854 RID: 22612 RVA: 0x00138650 File Offset: 0x00136850
		[SecurityCritical]
		internal bool DoContextsMatch()
		{
			bool flag = false;
			if (this.GetStub() == RealProxy._defaultStub)
			{
				object stubData = RealProxy.GetStubData(this);
				if (stubData is IntPtr && ((IntPtr)stubData).Equals(Thread.CurrentContext.InternalContextID))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06005855 RID: 22613 RVA: 0x001386A2 File Offset: 0x001368A2
		[SecurityCritical]
		internal void AttachServerHelper(MarshalByRefObject s)
		{
			if (s == null || this._serverObject != null)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentNull_Generic"), "s");
			}
			this._serverObject = s;
			this.SetupIdentity();
		}

		// Token: 0x06005856 RID: 22614
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetStub();

		/// <summary>Sets the stub data for the specified proxy.</summary>
		/// <param name="rp">The proxy for which to set stub data.</param>
		/// <param name="stubData">The new stub data.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have UnmanagedCode permission.</exception>
		// Token: 0x06005857 RID: 22615
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStubData(RealProxy rp, object stubData);

		// Token: 0x06005858 RID: 22616 RVA: 0x001386D1 File Offset: 0x001368D1
		internal void SetSrvInfo(GCHandle srvIdentity, int domainID)
		{
			this._srvIdentity = srvIdentity;
			this._domainID = domainID;
		}

		/// <summary>Retrieves stub data that is stored for the specified proxy.</summary>
		/// <param name="rp">The proxy for which stub data is requested.</param>
		/// <returns>Stub data for the specified proxy.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have UnmanagedCode permission.</exception>
		// Token: 0x06005859 RID: 22617
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object GetStubData(RealProxy rp);

		// Token: 0x0600585A RID: 22618
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDefaultStub();

		/// <summary>Returns the <see cref="T:System.Type" /> of the object that the current instance of <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> represents.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object that the current instance of <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> represents.</returns>
		// Token: 0x0600585B RID: 22619
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Type GetProxiedType();

		/// <summary>When overridden in a derived class, invokes the method that is specified in the provided <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> on the remote object that is represented by the current instance.</summary>
		/// <param name="msg">A <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> that contains a <see cref="T:System.Collections.IDictionary" /> of information about the method call.</param>
		/// <returns>The message returned by the invoked method, containing the return value and any <see langword="out" /> or <see langword="ref" /> parameters.</returns>
		// Token: 0x0600585C RID: 22620
		public abstract IMessage Invoke(IMessage msg);

		/// <summary>Creates an <see cref="T:System.Runtime.Remoting.ObjRef" /> for the specified object type, and registers it with the remoting infrastructure as a client-activated object.</summary>
		/// <param name="requestedType">The object type that an <see cref="T:System.Runtime.Remoting.ObjRef" /> is created for.</param>
		/// <returns>A new instance of <see cref="T:System.Runtime.Remoting.ObjRef" /> that is created for the specified type.</returns>
		// Token: 0x0600585D RID: 22621 RVA: 0x001386E1 File Offset: 0x001368E1
		[SecurityCritical]
		public virtual ObjRef CreateObjRef(Type requestedType)
		{
			if (this._identity == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_NoIdentityEntry"));
			}
			return new ObjRef((MarshalByRefObject)this.GetTransparentProxy(), requestedType);
		}

		/// <summary>Adds the transparent proxy of the object represented by the current instance of <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> to the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> into which the transparent proxy is serialized.</param>
		/// <param name="context">The source and destination of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> or <paramref name="context" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have SerializationFormatter permission.</exception>
		// Token: 0x0600585E RID: 22622 RVA: 0x0013870C File Offset: 0x0013690C
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			object transparentProxy = this.GetTransparentProxy();
			RemotingServices.GetObjectData(transparentProxy, info, context);
		}

		// Token: 0x0600585F RID: 22623 RVA: 0x00138728 File Offset: 0x00136928
		[SecurityCritical]
		private static void HandleReturnMessage(IMessage reqMsg, IMessage retMsg)
		{
			IMethodReturnMessage methodReturnMessage = retMsg as IMethodReturnMessage;
			if (retMsg == null || methodReturnMessage == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			}
			Exception exception = methodReturnMessage.Exception;
			if (exception != null)
			{
				throw exception.PrepForRemoting();
			}
			if (!(retMsg is StackBasedReturnMessage))
			{
				if (reqMsg is Message)
				{
					RealProxy.PropagateOutParameters(reqMsg, methodReturnMessage.Args, methodReturnMessage.ReturnValue);
					return;
				}
				if (reqMsg is ConstructorCallMessage)
				{
					RealProxy.PropagateOutParameters(reqMsg, methodReturnMessage.Args, null);
				}
			}
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x0013879C File Offset: 0x0013699C
		[SecurityCritical]
		internal static void PropagateOutParameters(IMessage msg, object[] outArgs, object returnValue)
		{
			Message message = msg as Message;
			if (message == null)
			{
				ConstructorCallMessage constructorCallMessage = msg as ConstructorCallMessage;
				if (constructorCallMessage != null)
				{
					message = constructorCallMessage.GetMessage();
				}
			}
			if (message == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Remoting_Proxy_ExpectedOriginalMessage"));
			}
			MethodBase methodBase = message.GetMethodBase();
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(methodBase);
			if (outArgs != null && outArgs.Length != 0)
			{
				object[] args = message.Args;
				ParameterInfo[] parameters = reflectionCachedData.Parameters;
				foreach (int num in reflectionCachedData.MarshalRequestArgMap)
				{
					ParameterInfo parameterInfo = parameters[num];
					if (parameterInfo.IsIn && parameterInfo.ParameterType.IsByRef && !parameterInfo.IsOut)
					{
						outArgs[num] = args[num];
					}
				}
				if (reflectionCachedData.NonRefOutArgMap.Length != 0)
				{
					foreach (int num2 in reflectionCachedData.NonRefOutArgMap)
					{
						Array array = args[num2] as Array;
						if (array != null)
						{
							Array.Copy((Array)outArgs[num2], array, array.Length);
						}
					}
				}
				int[] outRefArgMap = reflectionCachedData.OutRefArgMap;
				if (outRefArgMap.Length != 0)
				{
					foreach (int num3 in outRefArgMap)
					{
						RealProxy.ValidateReturnArg(outArgs[num3], parameters[num3].ParameterType);
					}
				}
			}
			int callType = message.GetCallType();
			if ((callType & 15) != 1)
			{
				Type returnType = reflectionCachedData.ReturnType;
				if (returnType != null)
				{
					RealProxy.ValidateReturnArg(returnValue, returnType);
				}
			}
			message.PropagateOutParameters(outArgs, returnValue);
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x00138918 File Offset: 0x00136B18
		private static void ValidateReturnArg(object arg, Type paramType)
		{
			if (paramType.IsByRef)
			{
				paramType = paramType.GetElementType();
			}
			if (paramType.IsValueType)
			{
				if (arg == null)
				{
					if (!paramType.IsGenericType || !(paramType.GetGenericTypeDefinition() == typeof(Nullable<>)))
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_ReturnValueTypeCannotBeNull"));
					}
				}
				else if (!paramType.IsInstanceOfType(arg))
				{
					throw new InvalidCastException(Environment.GetResourceString("Remoting_Proxy_BadReturnType"));
				}
			}
			else if (arg != null && !paramType.IsInstanceOfType(arg))
			{
				throw new InvalidCastException(Environment.GetResourceString("Remoting_Proxy_BadReturnType"));
			}
		}

		// Token: 0x06005862 RID: 22626 RVA: 0x001389A4 File Offset: 0x00136BA4
		[SecurityCritical]
		internal static IMessage EndInvokeHelper(Message reqMsg, bool bProxyCase)
		{
			AsyncResult asyncResult = reqMsg.GetAsyncResult() as AsyncResult;
			IMessage message = null;
			if (asyncResult == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadAsyncResult"));
			}
			if (asyncResult.AsyncDelegate != reqMsg.GetThisPtr())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MismatchedAsyncResult"));
			}
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne(-1, Thread.CurrentContext.IsThreadPoolAware);
			}
			AsyncResult asyncResult2 = asyncResult;
			lock (asyncResult2)
			{
				if (asyncResult.EndInvokeCalled)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EndInvokeCalledMultiple"));
				}
				asyncResult.EndInvokeCalled = true;
				IMethodReturnMessage methodReturnMessage = (IMethodReturnMessage)asyncResult.GetReplyMessage();
				if (!bProxyCase)
				{
					Exception exception = methodReturnMessage.Exception;
					if (exception != null)
					{
						throw exception.PrepForRemoting();
					}
					reqMsg.PropagateOutParameters(methodReturnMessage.Args, methodReturnMessage.ReturnValue);
				}
				else
				{
					message = methodReturnMessage;
				}
				Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Merge(methodReturnMessage.LogicalCallContext);
			}
			return message;
		}

		/// <summary>Requests an unmanaged reference to the object represented by the current proxy instance.</summary>
		/// <param name="fIsMarshalled">
		///   <see langword="true" /> if the object reference is requested for marshaling to a remote location; <see langword="false" /> if the object reference is requested for communication with unmanaged objects in the current process through COM.</param>
		/// <returns>A pointer to a COM Callable Wrapper if the object reference is requested for communication with unmanaged objects in the current process through COM, or a pointer to a cached or newly generated <see langword="IUnknown" /> COM interface if the object reference is requested for marshaling to a remote location.</returns>
		// Token: 0x06005863 RID: 22627 RVA: 0x00138AB0 File Offset: 0x00136CB0
		[SecurityCritical]
		public virtual IntPtr GetCOMIUnknown(bool fIsMarshalled)
		{
			return MarshalByRefObject.GetComIUnknown((MarshalByRefObject)this.GetTransparentProxy());
		}

		/// <summary>Stores an unmanaged proxy of the object that is represented by the current instance.</summary>
		/// <param name="i">A pointer to the <see langword="IUnknown" /> interface for the object that is represented by the current proxy instance.</param>
		// Token: 0x06005864 RID: 22628 RVA: 0x00138AC2 File Offset: 0x00136CC2
		public virtual void SetCOMIUnknown(IntPtr i)
		{
		}

		/// <summary>Requests a COM interface with the specified ID.</summary>
		/// <param name="iid">A reference to the requested interface.</param>
		/// <returns>A pointer to the requested interface.</returns>
		// Token: 0x06005865 RID: 22629 RVA: 0x00138AC4 File Offset: 0x00136CC4
		public virtual IntPtr SupportsInterface(ref Guid iid)
		{
			return IntPtr.Zero;
		}

		/// <summary>Returns the transparent proxy for the current instance of <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" />.</summary>
		/// <returns>The transparent proxy for the current proxy instance.</returns>
		// Token: 0x06005866 RID: 22630 RVA: 0x00138ACB File Offset: 0x00136CCB
		public virtual object GetTransparentProxy()
		{
			return this._tp;
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06005867 RID: 22631 RVA: 0x00138AD3 File Offset: 0x00136CD3
		internal MarshalByRefObject UnwrappedServerObject
		{
			get
			{
				return this._serverObject;
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06005868 RID: 22632 RVA: 0x00138ADB File Offset: 0x00136CDB
		// (set) Token: 0x06005869 RID: 22633 RVA: 0x00138AE8 File Offset: 0x00136CE8
		internal virtual Identity IdentityObject
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return (Identity)this._identity;
			}
			set
			{
				this._identity = value;
			}
		}

		// Token: 0x0600586A RID: 22634 RVA: 0x00138AF4 File Offset: 0x00136CF4
		[SecurityCritical]
		private void PrivateInvoke(ref MessageData msgData, int type)
		{
			IMessage message = null;
			IMessage message2 = null;
			int num = -1;
			RemotingProxy remotingProxy = null;
			if (1 == type)
			{
				Message message3 = new Message();
				message3.InitFields(msgData);
				message = message3;
				num = message3.GetCallType();
			}
			else if (2 == type)
			{
				num = 0;
				remotingProxy = this as RemotingProxy;
				bool flag = false;
				ConstructorCallMessage constructorCallMessage;
				if (!this.IsRemotingProxy())
				{
					constructorCallMessage = new ConstructorCallMessage(null, null, null, (RuntimeType)this.GetProxiedType());
				}
				else
				{
					constructorCallMessage = remotingProxy.ConstructorMessage;
					Identity identityObject = remotingProxy.IdentityObject;
					if (identityObject != null)
					{
						flag = identityObject.IsWellKnown();
					}
				}
				if (constructorCallMessage == null || flag)
				{
					constructorCallMessage = new ConstructorCallMessage(null, null, null, (RuntimeType)this.GetProxiedType());
					constructorCallMessage.SetFrame(msgData);
					message = constructorCallMessage;
					if (flag)
					{
						remotingProxy.ConstructorMessage = null;
						if (constructorCallMessage.ArgCount != 0)
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Activation_WellKnownCTOR"));
						}
					}
					message2 = new ConstructorReturnMessage((MarshalByRefObject)this.GetTransparentProxy(), null, 0, null, constructorCallMessage);
				}
				else
				{
					constructorCallMessage.SetFrame(msgData);
					message = constructorCallMessage;
				}
			}
			ChannelServices.IncrementRemoteCalls();
			if (!this.IsRemotingProxy() && (num & 2) == 2)
			{
				Message message4 = message as Message;
				message2 = RealProxy.EndInvokeHelper(message4, true);
			}
			if (message2 == null)
			{
				Thread currentThread = Thread.CurrentThread;
				LogicalCallContext logicalCallContext = currentThread.GetMutableExecutionContext().LogicalCallContext;
				this.SetCallContextInMessage(message, num, logicalCallContext);
				logicalCallContext.PropagateOutgoingHeadersToMessage(message);
				message2 = this.Invoke(message);
				this.ReturnCallContextToThread(currentThread, message2, num, logicalCallContext);
				Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.PropagateIncomingHeadersToCallContext(message2);
			}
			if (!this.IsRemotingProxy() && (num & 1) == 1)
			{
				Message message5 = message as Message;
				AsyncResult asyncResult = new AsyncResult(message5);
				asyncResult.SyncProcessMessage(message2);
				message2 = new ReturnMessage(asyncResult, null, 0, null, message5);
			}
			RealProxy.HandleReturnMessage(message, message2);
			if (2 == type)
			{
				IConstructionReturnMessage constructionReturnMessage = message2 as IConstructionReturnMessage;
				if (constructionReturnMessage == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_BadReturnTypeForActivation"));
				}
				ConstructorReturnMessage constructorReturnMessage = constructionReturnMessage as ConstructorReturnMessage;
				MarshalByRefObject marshalByRefObject;
				if (constructorReturnMessage != null)
				{
					marshalByRefObject = (MarshalByRefObject)constructorReturnMessage.GetObject();
					if (marshalByRefObject == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Activation_NullReturnValue"));
					}
				}
				else
				{
					marshalByRefObject = (MarshalByRefObject)RemotingServices.InternalUnmarshal((ObjRef)constructionReturnMessage.ReturnValue, this.GetTransparentProxy(), true);
					if (marshalByRefObject == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Activation_NullFromInternalUnmarshal"));
					}
				}
				if (marshalByRefObject != (MarshalByRefObject)this.GetTransparentProxy())
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Activation_InconsistentState"));
				}
				if (this.IsRemotingProxy())
				{
					remotingProxy.ConstructorMessage = null;
				}
			}
		}

		// Token: 0x0600586B RID: 22635 RVA: 0x00138D74 File Offset: 0x00136F74
		private void SetCallContextInMessage(IMessage reqMsg, int msgFlags, LogicalCallContext cctx)
		{
			Message message = reqMsg as Message;
			if (msgFlags == 0)
			{
				if (message != null)
				{
					message.SetLogicalCallContext(cctx);
					return;
				}
				((ConstructorCallMessage)reqMsg).SetLogicalCallContext(cctx);
			}
		}

		// Token: 0x0600586C RID: 22636 RVA: 0x00138DA4 File Offset: 0x00136FA4
		[SecurityCritical]
		private void ReturnCallContextToThread(Thread currentThread, IMessage retMsg, int msgFlags, LogicalCallContext currCtx)
		{
			if (msgFlags == 0)
			{
				if (retMsg == null)
				{
					return;
				}
				IMethodReturnMessage methodReturnMessage = retMsg as IMethodReturnMessage;
				if (methodReturnMessage == null)
				{
					return;
				}
				LogicalCallContext logicalCallContext = methodReturnMessage.LogicalCallContext;
				if (logicalCallContext == null)
				{
					currentThread.GetMutableExecutionContext().LogicalCallContext = currCtx;
					return;
				}
				if (!(methodReturnMessage is StackBasedReturnMessage))
				{
					ExecutionContext mutableExecutionContext = currentThread.GetMutableExecutionContext();
					LogicalCallContext logicalCallContext2 = mutableExecutionContext.LogicalCallContext;
					mutableExecutionContext.LogicalCallContext = logicalCallContext;
					if (logicalCallContext2 != logicalCallContext)
					{
						IPrincipal principal = logicalCallContext2.Principal;
						if (principal != null)
						{
							logicalCallContext.Principal = principal;
						}
					}
				}
			}
		}

		// Token: 0x0600586D RID: 22637 RVA: 0x00138E10 File Offset: 0x00137010
		[SecurityCritical]
		internal virtual void Wrap()
		{
			ServerIdentity serverIdentity = this._identity as ServerIdentity;
			if (serverIdentity != null && this is RemotingProxy)
			{
				RealProxy.SetStubData(this, serverIdentity.ServerContext.InternalContextID);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> class with default values.</summary>
		// Token: 0x0600586E RID: 22638 RVA: 0x00138E4A File Offset: 0x0013704A
		protected RealProxy()
		{
		}

		// Token: 0x04002846 RID: 10310
		private object _tp;

		// Token: 0x04002847 RID: 10311
		private object _identity;

		// Token: 0x04002848 RID: 10312
		private MarshalByRefObject _serverObject;

		// Token: 0x04002849 RID: 10313
		private RealProxyFlags _flags;

		// Token: 0x0400284A RID: 10314
		internal GCHandle _srvIdentity;

		// Token: 0x0400284B RID: 10315
		internal int _optFlags;

		// Token: 0x0400284C RID: 10316
		internal int _domainID;

		// Token: 0x0400284D RID: 10317
		private static IntPtr _defaultStub = RealProxy.GetDefaultStub();

		// Token: 0x0400284E RID: 10318
		private static IntPtr _defaultStubValue = new IntPtr(-1);

		// Token: 0x0400284F RID: 10319
		private static object _defaultStubData = RealProxy._defaultStubValue;
	}
}
