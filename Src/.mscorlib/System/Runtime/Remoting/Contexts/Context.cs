using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Defines an environment for the objects that are resident inside it and for which a policy can be enforced.</summary>
	// Token: 0x02000807 RID: 2055
	[ComVisible(true)]
	public class Context
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> class.</summary>
		// Token: 0x0600589B RID: 22683 RVA: 0x00139A0B File Offset: 0x00137C0B
		[SecurityCritical]
		public Context()
			: this(0)
		{
		}

		// Token: 0x0600589C RID: 22684 RVA: 0x00139A14 File Offset: 0x00137C14
		[SecurityCritical]
		private Context(int flags)
		{
			this._ctxFlags = flags;
			if ((this._ctxFlags & 1) != 0)
			{
				this._ctxID = 0;
			}
			else
			{
				this._ctxID = Interlocked.Increment(ref Context._ctxIDCounter);
			}
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			if (remotingData != null)
			{
				IContextProperty[] appDomainContextProperties = remotingData.AppDomainContextProperties;
				if (appDomainContextProperties != null)
				{
					for (int i = 0; i < appDomainContextProperties.Length; i++)
					{
						this.SetProperty(appDomainContextProperties[i]);
					}
				}
			}
			if ((this._ctxFlags & 1) != 0)
			{
				this.Freeze();
			}
			this.SetupInternalContext((this._ctxFlags & 1) == 1);
		}

		// Token: 0x0600589D RID: 22685
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetupInternalContext(bool bDefault);

		/// <summary>Cleans up the backing objects for the nondefault contexts.</summary>
		// Token: 0x0600589E RID: 22686 RVA: 0x00139AA4 File Offset: 0x00137CA4
		[SecuritySafeCritical]
		~Context()
		{
			if (this._internalContext != IntPtr.Zero && (this._ctxFlags & 1) == 0)
			{
				this.CleanupInternalContext();
			}
		}

		// Token: 0x0600589F RID: 22687
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CleanupInternalContext();

		/// <summary>Gets the context ID for the current context.</summary>
		/// <returns>The context ID for the current context.</returns>
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x060058A0 RID: 22688 RVA: 0x00139AEC File Offset: 0x00137CEC
		public virtual int ContextID
		{
			[SecurityCritical]
			get
			{
				return this._ctxID;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x060058A1 RID: 22689 RVA: 0x00139AF4 File Offset: 0x00137CF4
		internal virtual IntPtr InternalContextID
		{
			get
			{
				return this._internalContext;
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x060058A2 RID: 22690 RVA: 0x00139AFC File Offset: 0x00137CFC
		internal virtual AppDomain AppDomain
		{
			get
			{
				return this._appDomain;
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x060058A3 RID: 22691 RVA: 0x00139B04 File Offset: 0x00137D04
		internal bool IsDefaultContext
		{
			get
			{
				return this._ctxID == 0;
			}
		}

		/// <summary>Gets the default context for the current application domain.</summary>
		/// <returns>The default context for the <see cref="T:System.AppDomain" /> namespace.</returns>
		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x060058A4 RID: 22692 RVA: 0x00139B0F File Offset: 0x00137D0F
		public static Context DefaultContext
		{
			[SecurityCritical]
			get
			{
				return Thread.GetDomain().GetDefaultContext();
			}
		}

		// Token: 0x060058A5 RID: 22693 RVA: 0x00139B1B File Offset: 0x00137D1B
		[SecurityCritical]
		internal static Context CreateDefaultContext()
		{
			return new Context(1);
		}

		/// <summary>Returns a specific context property, specified by name.</summary>
		/// <param name="name">The name of the property.</param>
		/// <returns>The specified context property.</returns>
		// Token: 0x060058A6 RID: 22694 RVA: 0x00139B24 File Offset: 0x00137D24
		[SecurityCritical]
		public virtual IContextProperty GetProperty(string name)
		{
			if (this._ctxProps == null || name == null)
			{
				return null;
			}
			IContextProperty contextProperty = null;
			for (int i = 0; i < this._numCtxProps; i++)
			{
				if (this._ctxProps[i].Name.Equals(name))
				{
					contextProperty = this._ctxProps[i];
					break;
				}
			}
			return contextProperty;
		}

		/// <summary>Sets a specific context property by name.</summary>
		/// <param name="prop">The actual context property.</param>
		/// <exception cref="T:System.InvalidOperationException">The context is frozen.</exception>
		/// <exception cref="T:System.ArgumentNullException">The property or the property name is <see langword="null" />.</exception>
		// Token: 0x060058A7 RID: 22695 RVA: 0x00139B74 File Offset: 0x00137D74
		[SecurityCritical]
		public virtual void SetProperty(IContextProperty prop)
		{
			if (prop == null || prop.Name == null)
			{
				throw new ArgumentNullException((prop == null) ? "prop" : "property name");
			}
			if ((this._ctxFlags & 2) != 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AddContextFrozen"));
			}
			lock (this)
			{
				Context.CheckPropertyNameClash(prop.Name, this._ctxProps, this._numCtxProps);
				if (this._ctxProps == null || this._numCtxProps == this._ctxProps.Length)
				{
					this._ctxProps = Context.GrowPropertiesArray(this._ctxProps);
				}
				IContextProperty[] ctxProps = this._ctxProps;
				int numCtxProps = this._numCtxProps;
				this._numCtxProps = numCtxProps + 1;
				ctxProps[numCtxProps] = prop;
			}
		}

		// Token: 0x060058A8 RID: 22696 RVA: 0x00139C3C File Offset: 0x00137E3C
		[SecurityCritical]
		internal virtual void InternalFreeze()
		{
			this._ctxFlags |= 2;
			for (int i = 0; i < this._numCtxProps; i++)
			{
				this._ctxProps[i].Freeze(this);
			}
		}

		/// <summary>Freezes the context, making it impossible to add or remove context properties from the current context.</summary>
		/// <exception cref="T:System.InvalidOperationException">The context is already frozen.</exception>
		// Token: 0x060058A9 RID: 22697 RVA: 0x00139C78 File Offset: 0x00137E78
		[SecurityCritical]
		public virtual void Freeze()
		{
			lock (this)
			{
				if ((this._ctxFlags & 2) != 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ContextAlreadyFrozen"));
				}
				this.InternalFreeze();
			}
		}

		// Token: 0x060058AA RID: 22698 RVA: 0x00139CD0 File Offset: 0x00137ED0
		internal virtual void SetThreadPoolAware()
		{
			this._ctxFlags |= 4;
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x060058AB RID: 22699 RVA: 0x00139CE0 File Offset: 0x00137EE0
		internal virtual bool IsThreadPoolAware
		{
			get
			{
				return (this._ctxFlags & 4) == 4;
			}
		}

		/// <summary>Gets the array of the current context properties.</summary>
		/// <returns>The current context properties array; otherwise, <see langword="null" /> if the context does not have any properties attributed to it.</returns>
		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x060058AC RID: 22700 RVA: 0x00139CF0 File Offset: 0x00137EF0
		public virtual IContextProperty[] ContextProperties
		{
			[SecurityCritical]
			get
			{
				if (this._ctxProps == null)
				{
					return null;
				}
				IContextProperty[] array2;
				lock (this)
				{
					IContextProperty[] array = new IContextProperty[this._numCtxProps];
					Array.Copy(this._ctxProps, array, this._numCtxProps);
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x060058AD RID: 22701 RVA: 0x00139D50 File Offset: 0x00137F50
		[SecurityCritical]
		internal static void CheckPropertyNameClash(string name, IContextProperty[] props, int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (props[i].Name.Equals(name))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DuplicatePropertyName"));
				}
			}
		}

		// Token: 0x060058AE RID: 22702 RVA: 0x00139D8C File Offset: 0x00137F8C
		internal static IContextProperty[] GrowPropertiesArray(IContextProperty[] props)
		{
			int num = ((props != null) ? props.Length : 0) + 8;
			IContextProperty[] array = new IContextProperty[num];
			if (props != null)
			{
				Array.Copy(props, array, props.Length);
			}
			return array;
		}

		// Token: 0x060058AF RID: 22703 RVA: 0x00139DBC File Offset: 0x00137FBC
		[SecurityCritical]
		internal virtual IMessageSink GetServerContextChain()
		{
			if (this._serverContextChain == null)
			{
				IMessageSink messageSink = ServerContextTerminatorSink.MessageSink;
				int numCtxProps = this._numCtxProps;
				while (numCtxProps-- > 0)
				{
					object obj = this._ctxProps[numCtxProps];
					IContributeServerContextSink contributeServerContextSink = obj as IContributeServerContextSink;
					if (contributeServerContextSink != null)
					{
						messageSink = contributeServerContextSink.GetServerContextSink(messageSink);
						if (messageSink == null)
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
						}
					}
				}
				lock (this)
				{
					if (this._serverContextChain == null)
					{
						this._serverContextChain = messageSink;
					}
				}
			}
			return this._serverContextChain;
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x00139E5C File Offset: 0x0013805C
		[SecurityCritical]
		internal virtual IMessageSink GetClientContextChain()
		{
			if (this._clientContextChain == null)
			{
				IMessageSink messageSink = ClientContextTerminatorSink.MessageSink;
				for (int i = 0; i < this._numCtxProps; i++)
				{
					object obj = this._ctxProps[i];
					IContributeClientContextSink contributeClientContextSink = obj as IContributeClientContextSink;
					if (contributeClientContextSink != null)
					{
						messageSink = contributeClientContextSink.GetClientContextSink(messageSink);
						if (messageSink == null)
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
						}
					}
				}
				lock (this)
				{
					if (this._clientContextChain == null)
					{
						this._clientContextChain = messageSink;
					}
				}
			}
			return this._clientContextChain;
		}

		// Token: 0x060058B1 RID: 22705 RVA: 0x00139EFC File Offset: 0x001380FC
		[SecurityCritical]
		internal virtual IMessageSink CreateServerObjectChain(MarshalByRefObject serverObj)
		{
			IMessageSink messageSink = new ServerObjectTerminatorSink(serverObj);
			int numCtxProps = this._numCtxProps;
			while (numCtxProps-- > 0)
			{
				object obj = this._ctxProps[numCtxProps];
				IContributeObjectSink contributeObjectSink = obj as IContributeObjectSink;
				if (contributeObjectSink != null)
				{
					messageSink = contributeObjectSink.GetObjectSink(serverObj, messageSink);
					if (messageSink == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
					}
				}
			}
			return messageSink;
		}

		// Token: 0x060058B2 RID: 22706 RVA: 0x00139F54 File Offset: 0x00138154
		[SecurityCritical]
		internal virtual IMessageSink CreateEnvoyChain(MarshalByRefObject objectOrProxy)
		{
			IMessageSink messageSink = EnvoyTerminatorSink.MessageSink;
			for (int i = 0; i < this._numCtxProps; i++)
			{
				object obj = this._ctxProps[i];
				IContributeEnvoySink contributeEnvoySink = obj as IContributeEnvoySink;
				if (contributeEnvoySink != null)
				{
					messageSink = contributeEnvoySink.GetEnvoySink(objectOrProxy, messageSink);
					if (messageSink == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
					}
				}
			}
			return messageSink;
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x00139FB0 File Offset: 0x001381B0
		[SecurityCritical]
		internal IMessage NotifyActivatorProperties(IMessage msg, bool bServerSide)
		{
			IMessage message = null;
			try
			{
				int numCtxProps = this._numCtxProps;
				while (numCtxProps-- != 0)
				{
					object obj = this._ctxProps[numCtxProps];
					IContextPropertyActivator contextPropertyActivator = obj as IContextPropertyActivator;
					if (contextPropertyActivator != null)
					{
						IConstructionCallMessage constructionCallMessage = msg as IConstructionCallMessage;
						if (constructionCallMessage != null)
						{
							if (!bServerSide)
							{
								contextPropertyActivator.CollectFromClientContext(constructionCallMessage);
							}
							else
							{
								contextPropertyActivator.DeliverClientContextToServerContext(constructionCallMessage);
							}
						}
						else if (bServerSide)
						{
							contextPropertyActivator.CollectFromServerContext((IConstructionReturnMessage)msg);
						}
						else
						{
							contextPropertyActivator.DeliverServerContextToClientContext((IConstructionReturnMessage)msg);
						}
					}
				}
			}
			catch (Exception ex)
			{
				IMethodCallMessage methodCallMessage;
				if (msg is IConstructionCallMessage)
				{
					methodCallMessage = (IMethodCallMessage)msg;
				}
				else
				{
					methodCallMessage = new ErrorMessage();
				}
				message = new ReturnMessage(ex, methodCallMessage);
				if (msg != null)
				{
					((ReturnMessage)message).SetLogicalCallContext((LogicalCallContext)msg.Properties[Message.CallContextKey]);
				}
			}
			return message;
		}

		/// <summary>Returns a <see cref="T:System.String" /> class representation of the current context.</summary>
		/// <returns>A <see cref="T:System.String" /> class representation of the current context.</returns>
		// Token: 0x060058B4 RID: 22708 RVA: 0x0013A088 File Offset: 0x00138288
		public override string ToString()
		{
			return "ContextID: " + this._ctxID.ToString();
		}

		/// <summary>Executes code in another context.</summary>
		/// <param name="deleg">The delegate used to request the callback.</param>
		// Token: 0x060058B5 RID: 22709 RVA: 0x0013A0A0 File Offset: 0x001382A0
		[SecurityCritical]
		public void DoCallBack(CrossContextDelegate deleg)
		{
			if (deleg == null)
			{
				throw new ArgumentNullException("deleg");
			}
			if ((this._ctxFlags & 2) == 0)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_ContextNotFrozenForCallBack"));
			}
			Context currentContext = Thread.CurrentContext;
			if (currentContext == this)
			{
				deleg();
				return;
			}
			currentContext.DoCallBackGeneric(this.InternalContextID, deleg);
			GC.KeepAlive(this);
		}

		// Token: 0x060058B6 RID: 22710 RVA: 0x0013A0FC File Offset: 0x001382FC
		[SecurityCritical]
		internal static void DoCallBackFromEE(IntPtr targetCtxID, IntPtr privateData, int targetDomainID)
		{
			if (targetDomainID == 0)
			{
				CallBackHelper callBackHelper = new CallBackHelper(privateData, true, targetDomainID);
				CrossContextDelegate crossContextDelegate = new CrossContextDelegate(callBackHelper.Func);
				Thread.CurrentContext.DoCallBackGeneric(targetCtxID, crossContextDelegate);
				return;
			}
			TransitionCall transitionCall = new TransitionCall(targetCtxID, privateData, targetDomainID);
			Message.PropagateCallContextFromThreadToMessage(transitionCall);
			IMessage message = Thread.CurrentContext.GetClientContextChain().SyncProcessMessage(transitionCall);
			Message.PropagateCallContextFromMessageToThread(message);
			IMethodReturnMessage methodReturnMessage = message as IMethodReturnMessage;
			if (methodReturnMessage != null && methodReturnMessage.Exception != null)
			{
				throw methodReturnMessage.Exception;
			}
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x0013A174 File Offset: 0x00138374
		[SecurityCritical]
		internal void DoCallBackGeneric(IntPtr targetCtxID, CrossContextDelegate deleg)
		{
			TransitionCall transitionCall = new TransitionCall(targetCtxID, deleg);
			Message.PropagateCallContextFromThreadToMessage(transitionCall);
			IMessage message = this.GetClientContextChain().SyncProcessMessage(transitionCall);
			if (message != null)
			{
				Message.PropagateCallContextFromMessageToThread(message);
			}
			IMethodReturnMessage methodReturnMessage = message as IMethodReturnMessage;
			if (methodReturnMessage != null && methodReturnMessage.Exception != null)
			{
				throw methodReturnMessage.Exception;
			}
		}

		// Token: 0x060058B8 RID: 22712
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ExecuteCallBackInEE(IntPtr privateData);

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x060058B9 RID: 22713 RVA: 0x0013A1C0 File Offset: 0x001383C0
		private LocalDataStore MyLocalStore
		{
			get
			{
				if (this._localDataStore == null)
				{
					LocalDataStoreMgr localDataStoreMgr = Context._localDataStoreMgr;
					lock (localDataStoreMgr)
					{
						if (this._localDataStore == null)
						{
							this._localDataStore = Context._localDataStoreMgr.CreateLocalDataStore();
						}
					}
				}
				return this._localDataStore.Store;
			}
		}

		/// <summary>Allocates an unnamed data slot.</summary>
		/// <returns>A local data slot.</returns>
		// Token: 0x060058BA RID: 22714 RVA: 0x0013A22C File Offset: 0x0013842C
		[SecurityCritical]
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return Context._localDataStoreMgr.AllocateDataSlot();
		}

		/// <summary>Allocates a named data slot.</summary>
		/// <param name="name">The required name for the data slot.</param>
		/// <returns>A local data slot object.</returns>
		// Token: 0x060058BB RID: 22715 RVA: 0x0013A238 File Offset: 0x00138438
		[SecurityCritical]
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			return Context._localDataStoreMgr.AllocateNamedDataSlot(name);
		}

		/// <summary>Looks up a named data slot.</summary>
		/// <param name="name">The data slot name.</param>
		/// <returns>A local data slot.</returns>
		// Token: 0x060058BC RID: 22716 RVA: 0x0013A245 File Offset: 0x00138445
		[SecurityCritical]
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			return Context._localDataStoreMgr.GetNamedDataSlot(name);
		}

		/// <summary>Frees a named data slot on all the contexts.</summary>
		/// <param name="name">The name of the data slot to free.</param>
		// Token: 0x060058BD RID: 22717 RVA: 0x0013A252 File Offset: 0x00138452
		[SecurityCritical]
		public static void FreeNamedDataSlot(string name)
		{
			Context._localDataStoreMgr.FreeNamedDataSlot(name);
		}

		/// <summary>Sets the data in the specified slot on the current context.</summary>
		/// <param name="slot">The data slot where the data is to be added.</param>
		/// <param name="data">The data that is to be added.</param>
		// Token: 0x060058BE RID: 22718 RVA: 0x0013A25F File Offset: 0x0013845F
		[SecurityCritical]
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			Thread.CurrentContext.MyLocalStore.SetData(slot, data);
		}

		/// <summary>Retrieves the value from the specified slot on the current context.</summary>
		/// <param name="slot">The data slot that contains the data.</param>
		/// <returns>The data associated with <paramref name="slot" />.</returns>
		// Token: 0x060058BF RID: 22719 RVA: 0x0013A272 File Offset: 0x00138472
		[SecurityCritical]
		public static object GetData(LocalDataStoreSlot slot)
		{
			return Thread.CurrentContext.MyLocalStore.GetData(slot);
		}

		// Token: 0x060058C0 RID: 22720 RVA: 0x0013A284 File Offset: 0x00138484
		private int ReserveSlot()
		{
			if (this._ctxStatics == null)
			{
				this._ctxStatics = new object[8];
				this._ctxStatics[0] = null;
				this._ctxStaticsFreeIndex = 1;
				this._ctxStaticsCurrentBucket = 0;
			}
			if (this._ctxStaticsFreeIndex == 8)
			{
				object[] array = new object[8];
				object[] array2 = this._ctxStatics;
				while (array2[0] != null)
				{
					array2 = (object[])array2[0];
				}
				array2[0] = array;
				this._ctxStaticsFreeIndex = 1;
				this._ctxStaticsCurrentBucket++;
			}
			int ctxStaticsFreeIndex = this._ctxStaticsFreeIndex;
			this._ctxStaticsFreeIndex = ctxStaticsFreeIndex + 1;
			return ctxStaticsFreeIndex | (this._ctxStaticsCurrentBucket << 16);
		}

		/// <summary>Registers a dynamic property implementing the <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> interface with the remoting service.</summary>
		/// <param name="prop">The dynamic property to register.</param>
		/// <param name="obj">The object/proxy for which the property is registered.</param>
		/// <param name="ctx">The context for which the property is registered.</param>
		/// <returns>
		///   <see langword="true" /> if the property was successfully registered; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="prop" /> or its name is <see langword="null" />, or it is not dynamic (it does not implement <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" />).</exception>
		/// <exception cref="T:System.ArgumentException">Both an object as well as a context are specified (both <paramref name="obj" /> and <paramref name="ctx" /> are not <see langword="null" />).</exception>
		// Token: 0x060058C1 RID: 22721 RVA: 0x0013A318 File Offset: 0x00138518
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static bool RegisterDynamicProperty(IDynamicProperty prop, ContextBoundObject obj, Context ctx)
		{
			if (prop == null || prop.Name == null || !(prop is IContributeDynamicSink))
			{
				throw new ArgumentNullException("prop");
			}
			if (obj != null && ctx != null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NonNullObjAndCtx"));
			}
			bool flag;
			if (obj != null)
			{
				flag = IdentityHolder.AddDynamicProperty(obj, prop);
			}
			else
			{
				flag = Context.AddDynamicProperty(ctx, prop);
			}
			return flag;
		}

		/// <summary>Unregisters a dynamic property implementing the <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> interface.</summary>
		/// <param name="name">The name of the dynamic property to unregister.</param>
		/// <param name="obj">The object/proxy for which the property is registered.</param>
		/// <param name="ctx">The context for which the property is registered.</param>
		/// <returns>
		///   <see langword="true" /> if the object was successfully unregistered; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">Both an object as well as a context are specified (both <paramref name="obj" /> and <paramref name="ctx" /> are not <see langword="null" />).</exception>
		// Token: 0x060058C2 RID: 22722 RVA: 0x0013A374 File Offset: 0x00138574
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static bool UnregisterDynamicProperty(string name, ContextBoundObject obj, Context ctx)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (obj != null && ctx != null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NonNullObjAndCtx"));
			}
			bool flag;
			if (obj != null)
			{
				flag = IdentityHolder.RemoveDynamicProperty(obj, name);
			}
			else
			{
				flag = Context.RemoveDynamicProperty(ctx, name);
			}
			return flag;
		}

		// Token: 0x060058C3 RID: 22723 RVA: 0x0013A3BD File Offset: 0x001385BD
		[SecurityCritical]
		internal static bool AddDynamicProperty(Context ctx, IDynamicProperty prop)
		{
			if (ctx != null)
			{
				return ctx.AddPerContextDynamicProperty(prop);
			}
			return Context.AddGlobalDynamicProperty(prop);
		}

		// Token: 0x060058C4 RID: 22724 RVA: 0x0013A3D0 File Offset: 0x001385D0
		[SecurityCritical]
		private bool AddPerContextDynamicProperty(IDynamicProperty prop)
		{
			if (this._dphCtx == null)
			{
				DynamicPropertyHolder dynamicPropertyHolder = new DynamicPropertyHolder();
				lock (this)
				{
					if (this._dphCtx == null)
					{
						this._dphCtx = dynamicPropertyHolder;
					}
				}
			}
			return this._dphCtx.AddDynamicProperty(prop);
		}

		// Token: 0x060058C5 RID: 22725 RVA: 0x0013A430 File Offset: 0x00138630
		[SecurityCritical]
		private static bool AddGlobalDynamicProperty(IDynamicProperty prop)
		{
			return Context._dphGlobal.AddDynamicProperty(prop);
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x0013A43D File Offset: 0x0013863D
		[SecurityCritical]
		internal static bool RemoveDynamicProperty(Context ctx, string name)
		{
			if (ctx != null)
			{
				return ctx.RemovePerContextDynamicProperty(name);
			}
			return Context.RemoveGlobalDynamicProperty(name);
		}

		// Token: 0x060058C7 RID: 22727 RVA: 0x0013A450 File Offset: 0x00138650
		[SecurityCritical]
		private bool RemovePerContextDynamicProperty(string name)
		{
			if (this._dphCtx == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Contexts_NoProperty"), name));
			}
			return this._dphCtx.RemoveDynamicProperty(name);
		}

		// Token: 0x060058C8 RID: 22728 RVA: 0x0013A481 File Offset: 0x00138681
		[SecurityCritical]
		private static bool RemoveGlobalDynamicProperty(string name)
		{
			return Context._dphGlobal.RemoveDynamicProperty(name);
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x060058C9 RID: 22729 RVA: 0x0013A48E File Offset: 0x0013868E
		internal virtual IDynamicProperty[] PerContextDynamicProperties
		{
			get
			{
				if (this._dphCtx == null)
				{
					return null;
				}
				return this._dphCtx.DynamicProperties;
			}
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x060058CA RID: 22730 RVA: 0x0013A4A5 File Offset: 0x001386A5
		internal static ArrayWithSize GlobalDynamicSinks
		{
			[SecurityCritical]
			get
			{
				return Context._dphGlobal.DynamicSinks;
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x060058CB RID: 22731 RVA: 0x0013A4B1 File Offset: 0x001386B1
		internal virtual ArrayWithSize DynamicSinks
		{
			[SecurityCritical]
			get
			{
				if (this._dphCtx == null)
				{
					return null;
				}
				return this._dphCtx.DynamicSinks;
			}
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x0013A4C8 File Offset: 0x001386C8
		[SecurityCritical]
		internal virtual bool NotifyDynamicSinks(IMessage msg, bool bCliSide, bool bStart, bool bAsync, bool bNotifyGlobals)
		{
			bool flag = false;
			if (bNotifyGlobals && Context._dphGlobal.DynamicProperties != null)
			{
				ArrayWithSize globalDynamicSinks = Context.GlobalDynamicSinks;
				if (globalDynamicSinks != null)
				{
					DynamicPropertyHolder.NotifyDynamicSinks(msg, globalDynamicSinks, bCliSide, bStart, bAsync);
					flag = true;
				}
			}
			ArrayWithSize dynamicSinks = this.DynamicSinks;
			if (dynamicSinks != null)
			{
				DynamicPropertyHolder.NotifyDynamicSinks(msg, dynamicSinks, bCliSide, bStart, bAsync);
				flag = true;
			}
			return flag;
		}

		// Token: 0x04002861 RID: 10337
		internal const int CTX_DEFAULT_CONTEXT = 1;

		// Token: 0x04002862 RID: 10338
		internal const int CTX_FROZEN = 2;

		// Token: 0x04002863 RID: 10339
		internal const int CTX_THREADPOOL_AWARE = 4;

		// Token: 0x04002864 RID: 10340
		private const int GROW_BY = 8;

		// Token: 0x04002865 RID: 10341
		private const int STATICS_BUCKET_SIZE = 8;

		// Token: 0x04002866 RID: 10342
		private IContextProperty[] _ctxProps;

		// Token: 0x04002867 RID: 10343
		private DynamicPropertyHolder _dphCtx;

		// Token: 0x04002868 RID: 10344
		private volatile LocalDataStoreHolder _localDataStore;

		// Token: 0x04002869 RID: 10345
		private IMessageSink _serverContextChain;

		// Token: 0x0400286A RID: 10346
		private IMessageSink _clientContextChain;

		// Token: 0x0400286B RID: 10347
		private AppDomain _appDomain;

		// Token: 0x0400286C RID: 10348
		private object[] _ctxStatics;

		// Token: 0x0400286D RID: 10349
		private IntPtr _internalContext;

		// Token: 0x0400286E RID: 10350
		private int _ctxID;

		// Token: 0x0400286F RID: 10351
		private int _ctxFlags;

		// Token: 0x04002870 RID: 10352
		private int _numCtxProps;

		// Token: 0x04002871 RID: 10353
		private int _ctxStaticsCurrentBucket;

		// Token: 0x04002872 RID: 10354
		private int _ctxStaticsFreeIndex;

		// Token: 0x04002873 RID: 10355
		private static DynamicPropertyHolder _dphGlobal = new DynamicPropertyHolder();

		// Token: 0x04002874 RID: 10356
		private static LocalDataStoreMgr _localDataStoreMgr = new LocalDataStoreMgr();

		// Token: 0x04002875 RID: 10357
		private static int _ctxIDCounter = 0;
	}
}
