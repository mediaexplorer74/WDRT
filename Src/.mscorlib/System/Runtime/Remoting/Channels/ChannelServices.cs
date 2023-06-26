using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides static methods to aid with remoting channel registration, resolution, and URL discovery. This class cannot be inherited.</summary>
	// Token: 0x02000825 RID: 2085
	[ComVisible(true)]
	public sealed class ChannelServices
	{
		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06005985 RID: 22917 RVA: 0x0013C952 File Offset: 0x0013AB52
		internal static object[] CurrentChannelData
		{
			[SecurityCritical]
			get
			{
				if (ChannelServices.s_currentChannelData == null)
				{
					ChannelServices.RefreshChannelData();
				}
				return ChannelServices.s_currentChannelData;
			}
		}

		// Token: 0x06005986 RID: 22918 RVA: 0x0013C969 File Offset: 0x0013AB69
		private ChannelServices()
		{
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06005987 RID: 22919 RVA: 0x0013C971 File Offset: 0x0013AB71
		// (set) Token: 0x06005988 RID: 22920 RVA: 0x0013C987 File Offset: 0x0013AB87
		private static long remoteCalls
		{
			get
			{
				return Thread.GetDomain().RemotingData.ChannelServicesData.remoteCalls;
			}
			set
			{
				Thread.GetDomain().RemotingData.ChannelServicesData.remoteCalls = value;
			}
		}

		// Token: 0x06005989 RID: 22921
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern Perf_Contexts* GetPrivateContextsPerfCounters();

		/// <summary>Registers a channel with the channel services.</summary>
		/// <param name="chnl">The channel to register.</param>
		/// <param name="ensureSecurity">
		///   <see langword="true" /> ensures that security is enabled; otherwise <see langword="false" />. Setting the value to <see langword="false" /> does not effect the security setting on the TCP or IPC channel.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="chnl" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The channel has already been registered.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the call stack does not have permission to configure remoting types and channels.</exception>
		/// <exception cref="T:System.NotSupportedException">Not supported in Windows 98 for <see cref="T:System.Runtime.Remoting.Channels.Tcp.TcpServerChannel" /> and on all platforms for <see cref="T:System.Runtime.Remoting.Channels.Http.HttpServerChannel" />. Host the service using Internet Information Services (IIS) if you require a secure HTTP channel.</exception>
		// Token: 0x0600598A RID: 22922 RVA: 0x0013C99E File Offset: 0x0013AB9E
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterChannel(IChannel chnl, bool ensureSecurity)
		{
			ChannelServices.RegisterChannelInternal(chnl, ensureSecurity);
		}

		/// <summary>Registers a channel with the channel services. <see cref="M:System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(System.Runtime.Remoting.Channels.IChannel)" /> is obsolete. Please use <see cref="M:System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(System.Runtime.Remoting.Channels.IChannel,System.Boolean)" /> instead.</summary>
		/// <param name="chnl">The channel to register.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="chnl" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The channel has already been registered.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600598B RID: 22923 RVA: 0x0013C9A7 File Offset: 0x0013ABA7
		[SecuritySafeCritical]
		[Obsolete("Use System.Runtime.Remoting.ChannelServices.RegisterChannel(IChannel chnl, bool ensureSecurity) instead.", false)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterChannel(IChannel chnl)
		{
			ChannelServices.RegisterChannelInternal(chnl, false);
		}

		// Token: 0x0600598C RID: 22924 RVA: 0x0013C9B0 File Offset: 0x0013ABB0
		[SecurityCritical]
		internal unsafe static void RegisterChannelInternal(IChannel chnl, bool ensureSecurity)
		{
			if (chnl == null)
			{
				throw new ArgumentNullException("chnl");
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(ChannelServices.s_channelLock, ref flag);
				string channelName = chnl.ChannelName;
				RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
				if (channelName != null && channelName.Length != 0 && -1 != registeredChannelList.FindChannelIndex(chnl.ChannelName))
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_ChannelNameAlreadyRegistered", new object[] { chnl.ChannelName }));
				}
				if (ensureSecurity)
				{
					ISecurableChannel securableChannel = chnl as ISecurableChannel;
					if (securableChannel == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CannotBeSecured", new object[] { chnl.ChannelName ?? chnl.ToString() }));
					}
					securableChannel.IsSecured = ensureSecurity;
				}
				RegisteredChannel[] registeredChannels = registeredChannelList.RegisteredChannels;
				RegisteredChannel[] array;
				if (registeredChannels == null)
				{
					array = new RegisteredChannel[1];
				}
				else
				{
					array = new RegisteredChannel[registeredChannels.Length + 1];
				}
				if (!ChannelServices.unloadHandlerRegistered && !(chnl is CrossAppDomainChannel))
				{
					AppDomain.CurrentDomain.DomainUnload += ChannelServices.UnloadHandler;
					ChannelServices.unloadHandlerRegistered = true;
				}
				int channelPriority = chnl.ChannelPriority;
				int i;
				for (i = 0; i < registeredChannels.Length; i++)
				{
					RegisteredChannel registeredChannel = registeredChannels[i];
					if (channelPriority > registeredChannel.Channel.ChannelPriority)
					{
						array[i] = new RegisteredChannel(chnl);
						break;
					}
					array[i] = registeredChannel;
				}
				if (i == registeredChannels.Length)
				{
					array[registeredChannels.Length] = new RegisteredChannel(chnl);
				}
				else
				{
					while (i < registeredChannels.Length)
					{
						array[i + 1] = registeredChannels[i];
						i++;
					}
				}
				if (ChannelServices.perf_Contexts != null)
				{
					ChannelServices.perf_Contexts->cChannels++;
				}
				ChannelServices.s_registeredChannels = new RegisteredChannelList(array);
				ChannelServices.RefreshChannelData();
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(ChannelServices.s_channelLock);
				}
			}
		}

		/// <summary>Unregisters a particular channel from the registered channels list.</summary>
		/// <param name="chnl">The channel to unregister.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="chnl" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The channel is not registered.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600598D RID: 22925 RVA: 0x0013CB88 File Offset: 0x0013AD88
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public unsafe static void UnregisterChannel(IChannel chnl)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(ChannelServices.s_channelLock, ref flag);
				if (chnl != null)
				{
					RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
					int num = registeredChannelList.FindChannelIndex(chnl);
					if (-1 == num)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_ChannelNotRegistered", new object[] { chnl.ChannelName }));
					}
					RegisteredChannel[] registeredChannels = registeredChannelList.RegisteredChannels;
					RegisteredChannel[] array = new RegisteredChannel[registeredChannels.Length - 1];
					IChannelReceiver channelReceiver = chnl as IChannelReceiver;
					if (channelReceiver != null)
					{
						channelReceiver.StopListening(null);
					}
					int num2 = 0;
					int i = 0;
					while (i < registeredChannels.Length)
					{
						if (i == num)
						{
							i++;
						}
						else
						{
							array[num2] = registeredChannels[i];
							num2++;
							i++;
						}
					}
					if (ChannelServices.perf_Contexts != null)
					{
						ChannelServices.perf_Contexts->cChannels--;
					}
					ChannelServices.s_registeredChannels = new RegisteredChannelList(array);
				}
				ChannelServices.RefreshChannelData();
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(ChannelServices.s_channelLock);
				}
			}
		}

		/// <summary>Gets a list of currently registered channels.</summary>
		/// <returns>An array of all the currently registered channels.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x0600598E RID: 22926 RVA: 0x0013CC8C File Offset: 0x0013AE8C
		public static IChannel[] RegisteredChannels
		{
			[SecurityCritical]
			get
			{
				RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
				int count = registeredChannelList.Count;
				if (count == 0)
				{
					return new IChannel[0];
				}
				int num = count - 1;
				int num2 = 0;
				IChannel[] array = new IChannel[num];
				for (int i = 0; i < count; i++)
				{
					IChannel channel = registeredChannelList.GetChannel(i);
					if (!(channel is CrossAppDomainChannel))
					{
						array[num2++] = channel;
					}
				}
				return array;
			}
		}

		// Token: 0x0600598F RID: 22927 RVA: 0x0013CCF0 File Offset: 0x0013AEF0
		[SecurityCritical]
		internal static IMessageSink CreateMessageSink(string url, object data, out string objectURI)
		{
			IMessageSink messageSink = null;
			objectURI = null;
			RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
			int count = registeredChannelList.Count;
			for (int i = 0; i < count; i++)
			{
				if (registeredChannelList.IsSender(i))
				{
					IChannelSender channelSender = (IChannelSender)registeredChannelList.GetChannel(i);
					messageSink = channelSender.CreateMessageSink(url, data, out objectURI);
					if (messageSink != null)
					{
						break;
					}
				}
			}
			if (objectURI == null)
			{
				objectURI = url;
			}
			return messageSink;
		}

		// Token: 0x06005990 RID: 22928 RVA: 0x0013CD4C File Offset: 0x0013AF4C
		[SecurityCritical]
		internal static IMessageSink CreateMessageSink(object data)
		{
			string text;
			return ChannelServices.CreateMessageSink(null, data, out text);
		}

		/// <summary>Returns a registered channel with the specified name.</summary>
		/// <param name="name">The channel name.</param>
		/// <returns>An interface to a registered channel, or <see langword="null" /> if the channel is not registered.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005991 RID: 22929 RVA: 0x0013CD64 File Offset: 0x0013AF64
		[SecurityCritical]
		public static IChannel GetChannel(string name)
		{
			RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
			int num = registeredChannelList.FindChannelIndex(name);
			if (0 > num)
			{
				return null;
			}
			IChannel channel = registeredChannelList.GetChannel(num);
			if (channel is CrossAppDomainChannel || channel is CrossContextChannel)
			{
				return null;
			}
			return channel;
		}

		/// <summary>Returns an array of all the URLs that can be used to reach the specified object.</summary>
		/// <param name="obj">The object to retrieve the URL array for.</param>
		/// <returns>An array of strings that contains the URLs that can be used to remotely identify the object, or <see langword="null" /> if none were found.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005992 RID: 22930 RVA: 0x0013CDA4 File Offset: 0x0013AFA4
		[SecurityCritical]
		public static string[] GetUrlsForObject(MarshalByRefObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
			int count = registeredChannelList.Count;
			Hashtable hashtable = new Hashtable();
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			if (identity != null)
			{
				string objURI = identity.ObjURI;
				if (objURI != null)
				{
					for (int i = 0; i < count; i++)
					{
						if (registeredChannelList.IsReceiver(i))
						{
							try
							{
								string[] urlsForUri = ((IChannelReceiver)registeredChannelList.GetChannel(i)).GetUrlsForUri(objURI);
								for (int j = 0; j < urlsForUri.Length; j++)
								{
									hashtable.Add(urlsForUri[j], urlsForUri[j]);
								}
							}
							catch (NotSupportedException)
							{
							}
						}
					}
				}
			}
			ICollection keys = hashtable.Keys;
			string[] array = new string[keys.Count];
			int num = 0;
			foreach (object obj2 in keys)
			{
				string text = (string)obj2;
				array[num++] = text;
			}
			return array;
		}

		// Token: 0x06005993 RID: 22931 RVA: 0x0013CEBC File Offset: 0x0013B0BC
		[SecurityCritical]
		internal static IMessageSink GetChannelSinkForProxy(object obj)
		{
			IMessageSink messageSink = null;
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				RemotingProxy remotingProxy = realProxy as RemotingProxy;
				if (remotingProxy != null)
				{
					Identity identityObject = remotingProxy.IdentityObject;
					messageSink = identityObject.ChannelSink;
				}
			}
			return messageSink;
		}

		/// <summary>Returns a <see cref="T:System.Collections.IDictionary" /> of properties for a given proxy.</summary>
		/// <param name="obj">The proxy to retrieve properties for.</param>
		/// <returns>An interface to the dictionary of properties, or <see langword="null" /> if no properties were found.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers that is higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06005994 RID: 22932 RVA: 0x0013CEF4 File Offset: 0x0013B0F4
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static IDictionary GetChannelSinkProperties(object obj)
		{
			IMessageSink channelSinkForProxy = ChannelServices.GetChannelSinkForProxy(obj);
			IClientChannelSink clientChannelSink = channelSinkForProxy as IClientChannelSink;
			if (clientChannelSink != null)
			{
				ArrayList arrayList = new ArrayList();
				do
				{
					IDictionary properties = clientChannelSink.Properties;
					if (properties != null)
					{
						arrayList.Add(properties);
					}
					clientChannelSink = clientChannelSink.NextChannelSink;
				}
				while (clientChannelSink != null);
				return new AggregateDictionary(arrayList);
			}
			IDictionary dictionary = channelSinkForProxy as IDictionary;
			if (dictionary != null)
			{
				return dictionary;
			}
			return null;
		}

		// Token: 0x06005995 RID: 22933 RVA: 0x0013CF4B File Offset: 0x0013B14B
		internal static IMessageSink GetCrossContextChannelSink()
		{
			if (ChannelServices.xCtxChannel == null)
			{
				ChannelServices.xCtxChannel = CrossContextChannel.MessageSink;
			}
			return ChannelServices.xCtxChannel;
		}

		// Token: 0x06005996 RID: 22934 RVA: 0x0013CF69 File Offset: 0x0013B169
		[SecurityCritical]
		internal unsafe static void IncrementRemoteCalls(long cCalls)
		{
			ChannelServices.remoteCalls += cCalls;
			if (ChannelServices.perf_Contexts != null)
			{
				ChannelServices.perf_Contexts->cRemoteCalls += (int)cCalls;
			}
		}

		// Token: 0x06005997 RID: 22935 RVA: 0x0013CF94 File Offset: 0x0013B194
		[SecurityCritical]
		internal static void IncrementRemoteCalls()
		{
			ChannelServices.IncrementRemoteCalls(1L);
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x0013CFA0 File Offset: 0x0013B1A0
		[SecurityCritical]
		internal static void RefreshChannelData()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(ChannelServices.s_channelLock, ref flag);
				ChannelServices.s_currentChannelData = ChannelServices.CollectChannelDataFromChannels();
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(ChannelServices.s_channelLock);
				}
			}
		}

		// Token: 0x06005999 RID: 22937 RVA: 0x0013CFEC File Offset: 0x0013B1EC
		[SecurityCritical]
		private static object[] CollectChannelDataFromChannels()
		{
			RemotingServices.RegisterWellKnownChannels();
			RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
			int count = registeredChannelList.Count;
			int receiverCount = registeredChannelList.ReceiverCount;
			object[] array = new object[receiverCount];
			int num = 0;
			int i = 0;
			int num2 = 0;
			while (i < count)
			{
				IChannel channel = registeredChannelList.GetChannel(i);
				if (channel == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_ChannelNotRegistered", new object[] { "" }));
				}
				if (registeredChannelList.IsReceiver(i))
				{
					object channelData = ((IChannelReceiver)channel).ChannelData;
					array[num2] = channelData;
					if (channelData != null)
					{
						num++;
					}
					num2++;
				}
				i++;
			}
			if (num != receiverCount)
			{
				object[] array2 = new object[num];
				int num3 = 0;
				for (int j = 0; j < receiverCount; j++)
				{
					object obj = array[j];
					if (obj != null)
					{
						array2[num3++] = obj;
					}
				}
				array = array2;
			}
			return array;
		}

		// Token: 0x0600599A RID: 22938 RVA: 0x0013D0C8 File Offset: 0x0013B2C8
		private static bool IsMethodReallyPublic(MethodInfo mi)
		{
			if (!mi.IsPublic || mi.IsStatic)
			{
				return false;
			}
			if (!mi.IsGenericMethod)
			{
				return true;
			}
			foreach (Type type in mi.GetGenericArguments())
			{
				if (!type.IsVisible)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Dispatches incoming remote calls.</summary>
		/// <param name="sinkStack">The stack of server channel sinks that the message already traversed.</param>
		/// <param name="msg">The message to dispatch.</param>
		/// <param name="replyMsg">When this method returns, contains a <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> that holds the reply from the server to the message that is contained in the <paramref name="msg" /> parameter. This parameter is passed uninitialized.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Channels.ServerProcessing" /> that gives the status of the server message processing.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="msg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x0600599B RID: 22939 RVA: 0x0013D118 File Offset: 0x0013B318
		[SecurityCritical]
		public static ServerProcessing DispatchMessage(IServerChannelSinkStack sinkStack, IMessage msg, out IMessage replyMsg)
		{
			ServerProcessing serverProcessing = ServerProcessing.Complete;
			replyMsg = null;
			try
			{
				if (msg == null)
				{
					throw new ArgumentNullException("msg");
				}
				ChannelServices.IncrementRemoteCalls();
				ServerIdentity serverIdentity = ChannelServices.CheckDisconnectedOrCreateWellKnownObject(msg);
				if (serverIdentity.ServerType == typeof(AppDomain))
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_AppDomainsCantBeCalledRemotely"));
				}
				IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
				if (methodCallMessage == null)
				{
					if (!typeof(IMessageSink).IsAssignableFrom(serverIdentity.ServerType))
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_AppDomainsCantBeCalledRemotely"));
					}
					serverProcessing = ServerProcessing.Complete;
					replyMsg = ChannelServices.GetCrossContextChannelSink().SyncProcessMessage(msg);
				}
				else
				{
					MethodInfo methodInfo = (MethodInfo)methodCallMessage.MethodBase;
					if (!ChannelServices.IsMethodReallyPublic(methodInfo) && !RemotingServices.IsMethodAllowedRemotely(methodInfo))
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_NonPublicOrStaticCantBeCalledRemotely"));
					}
					RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(methodInfo);
					if (RemotingServices.IsOneWay(methodInfo))
					{
						serverProcessing = ServerProcessing.OneWay;
						ChannelServices.GetCrossContextChannelSink().AsyncProcessMessage(msg, null);
					}
					else
					{
						serverProcessing = ServerProcessing.Complete;
						if (!serverIdentity.ServerType.IsContextful)
						{
							object[] array = new object[] { msg, serverIdentity.ServerContext };
							replyMsg = (IMessage)CrossContextChannel.SyncProcessMessageCallback(array);
						}
						else
						{
							replyMsg = ChannelServices.GetCrossContextChannelSink().SyncProcessMessage(msg);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (serverProcessing != ServerProcessing.OneWay)
				{
					try
					{
						IMessage message2;
						if (msg == null)
						{
							IMessage message = new ErrorMessage();
							message2 = message;
						}
						else
						{
							message2 = msg;
						}
						IMethodCallMessage methodCallMessage2 = (IMethodCallMessage)message2;
						replyMsg = new ReturnMessage(ex, methodCallMessage2);
						if (msg != null)
						{
							((ReturnMessage)replyMsg).SetLogicalCallContext((LogicalCallContext)msg.Properties[Message.CallContextKey]);
						}
					}
					catch (Exception)
					{
					}
				}
			}
			return serverProcessing;
		}

		/// <summary>Synchronously dispatches the incoming message to the server-side chain(s) based on the URI embedded in the message.</summary>
		/// <param name="msg">The message to dispatch.</param>
		/// <returns>A reply message is returned by the call to the server-side chain.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="msg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x0600599C RID: 22940 RVA: 0x0013D2C8 File Offset: 0x0013B4C8
		[SecurityCritical]
		public static IMessage SyncDispatchMessage(IMessage msg)
		{
			IMessage message = null;
			bool flag = false;
			try
			{
				if (msg == null)
				{
					throw new ArgumentNullException("msg");
				}
				ChannelServices.IncrementRemoteCalls();
				if (!(msg is TransitionCall))
				{
					ChannelServices.CheckDisconnectedOrCreateWellKnownObject(msg);
					MethodBase methodBase = ((IMethodMessage)msg).MethodBase;
					flag = RemotingServices.IsOneWay(methodBase);
				}
				IMessageSink crossContextChannelSink = ChannelServices.GetCrossContextChannelSink();
				if (!flag)
				{
					message = crossContextChannelSink.SyncProcessMessage(msg);
				}
				else
				{
					crossContextChannelSink.AsyncProcessMessage(msg, null);
				}
			}
			catch (Exception ex)
			{
				if (!flag)
				{
					try
					{
						IMessage message3;
						if (msg == null)
						{
							IMessage message2 = new ErrorMessage();
							message3 = message2;
						}
						else
						{
							message3 = msg;
						}
						IMethodCallMessage methodCallMessage = (IMethodCallMessage)message3;
						message = new ReturnMessage(ex, methodCallMessage);
						if (msg != null)
						{
							((ReturnMessage)message).SetLogicalCallContext(methodCallMessage.LogicalCallContext);
						}
					}
					catch (Exception)
					{
					}
				}
			}
			return message;
		}

		/// <summary>Asynchronously dispatches the given message to the server-side chain(s) based on the URI embedded in the message.</summary>
		/// <param name="msg">The message to dispatch.</param>
		/// <param name="replySink">The sink that will process the return message if it is not <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> object used to control the asynchronously dispatched message.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="msg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x0600599D RID: 22941 RVA: 0x0013D38C File Offset: 0x0013B58C
		[SecurityCritical]
		public static IMessageCtrl AsyncDispatchMessage(IMessage msg, IMessageSink replySink)
		{
			IMessageCtrl messageCtrl = null;
			try
			{
				if (msg == null)
				{
					throw new ArgumentNullException("msg");
				}
				ChannelServices.IncrementRemoteCalls();
				if (!(msg is TransitionCall))
				{
					ChannelServices.CheckDisconnectedOrCreateWellKnownObject(msg);
				}
				messageCtrl = ChannelServices.GetCrossContextChannelSink().AsyncProcessMessage(msg, replySink);
			}
			catch (Exception ex)
			{
				if (replySink != null)
				{
					try
					{
						IMethodCallMessage methodCallMessage = (IMethodCallMessage)msg;
						ReturnMessage returnMessage = new ReturnMessage(ex, (IMethodCallMessage)msg);
						if (msg != null)
						{
							returnMessage.SetLogicalCallContext(methodCallMessage.LogicalCallContext);
						}
						replySink.SyncProcessMessage(returnMessage);
					}
					catch (Exception)
					{
					}
				}
			}
			return messageCtrl;
		}

		/// <summary>Creates a channel sink chain for the specified channel.</summary>
		/// <param name="provider">The first provider in the chain of sink providers that will create the channel sink chain.</param>
		/// <param name="channel">The <see cref="T:System.Runtime.Remoting.Channels.IChannelReceiver" /> for which to create the channel sink chain.</param>
		/// <returns>A new channel sink chain for the specified channel.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x0600599E RID: 22942 RVA: 0x0013D420 File Offset: 0x0013B620
		[SecurityCritical]
		public static IServerChannelSink CreateServerChannelSinkChain(IServerChannelSinkProvider provider, IChannelReceiver channel)
		{
			if (provider == null)
			{
				return new DispatchChannelSink();
			}
			IServerChannelSinkProvider serverChannelSinkProvider = provider;
			while (serverChannelSinkProvider.Next != null)
			{
				serverChannelSinkProvider = serverChannelSinkProvider.Next;
			}
			serverChannelSinkProvider.Next = new DispatchChannelSinkProvider();
			IServerChannelSink serverChannelSink = provider.CreateSink(channel);
			serverChannelSinkProvider.Next = null;
			return serverChannelSink;
		}

		// Token: 0x0600599F RID: 22943 RVA: 0x0013D464 File Offset: 0x0013B664
		[SecurityCritical]
		internal static ServerIdentity CheckDisconnectedOrCreateWellKnownObject(IMessage msg)
		{
			ServerIdentity serverIdentity = InternalSink.GetServerIdentity(msg);
			if (serverIdentity == null || serverIdentity.IsRemoteDisconnected())
			{
				string uri = InternalSink.GetURI(msg);
				if (uri != null)
				{
					ServerIdentity serverIdentity2 = RemotingConfigHandler.CreateWellKnownObject(uri);
					if (serverIdentity2 != null)
					{
						serverIdentity = serverIdentity2;
					}
				}
			}
			if (serverIdentity == null || serverIdentity.IsRemoteDisconnected())
			{
				string uri2 = InternalSink.GetURI(msg);
				throw new RemotingException(Environment.GetResourceString("Remoting_Disconnected", new object[] { uri2 }));
			}
			return serverIdentity;
		}

		// Token: 0x060059A0 RID: 22944 RVA: 0x0013D4C6 File Offset: 0x0013B6C6
		[SecurityCritical]
		internal static void UnloadHandler(object sender, EventArgs e)
		{
			ChannelServices.StopListeningOnAllChannels();
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x0013D4D0 File Offset: 0x0013B6D0
		[SecurityCritical]
		private static void StopListeningOnAllChannels()
		{
			try
			{
				RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
				int count = registeredChannelList.Count;
				for (int i = 0; i < count; i++)
				{
					if (registeredChannelList.IsReceiver(i))
					{
						IChannelReceiver channelReceiver = (IChannelReceiver)registeredChannelList.GetChannel(i);
						channelReceiver.StopListening(null);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x0013D52C File Offset: 0x0013B72C
		[SecurityCritical]
		internal static void NotifyProfiler(IMessage msg, RemotingProfilerEvent profilerEvent)
		{
			if (profilerEvent != RemotingProfilerEvent.ClientSend)
			{
				if (profilerEvent != RemotingProfilerEvent.ClientReceive)
				{
					return;
				}
				if (RemotingServices.CORProfilerTrackRemoting())
				{
					Guid guid = Guid.Empty;
					if (RemotingServices.CORProfilerTrackRemotingCookie())
					{
						object obj = msg.Properties["CORProfilerCookie"];
						if (obj != null)
						{
							guid = (Guid)obj;
						}
					}
					RemotingServices.CORProfilerRemotingClientReceivingReply(guid, false);
				}
			}
			else if (RemotingServices.CORProfilerTrackRemoting())
			{
				Guid guid2;
				RemotingServices.CORProfilerRemotingClientSendingMessage(out guid2, false);
				if (RemotingServices.CORProfilerTrackRemotingCookie())
				{
					msg.Properties["CORProfilerCookie"] = guid2;
					return;
				}
			}
		}

		// Token: 0x060059A3 RID: 22947 RVA: 0x0013D5A4 File Offset: 0x0013B7A4
		[SecurityCritical]
		internal static string FindFirstHttpUrlForObject(string objectUri)
		{
			if (objectUri == null)
			{
				return null;
			}
			RegisteredChannelList registeredChannelList = ChannelServices.s_registeredChannels;
			int count = registeredChannelList.Count;
			for (int i = 0; i < count; i++)
			{
				if (registeredChannelList.IsReceiver(i))
				{
					IChannelReceiver channelReceiver = (IChannelReceiver)registeredChannelList.GetChannel(i);
					string fullName = channelReceiver.GetType().FullName;
					if (string.CompareOrdinal(fullName, "System.Runtime.Remoting.Channels.Http.HttpChannel") == 0 || string.CompareOrdinal(fullName, "System.Runtime.Remoting.Channels.Http.HttpServerChannel") == 0)
					{
						string[] urlsForUri = channelReceiver.GetUrlsForUri(objectUri);
						if (urlsForUri != null && urlsForUri.Length != 0)
						{
							return urlsForUri[0];
						}
					}
				}
			}
			return null;
		}

		// Token: 0x040028C8 RID: 10440
		private static volatile object[] s_currentChannelData = null;

		// Token: 0x040028C9 RID: 10441
		private static object s_channelLock = new object();

		// Token: 0x040028CA RID: 10442
		private static volatile RegisteredChannelList s_registeredChannels = new RegisteredChannelList();

		// Token: 0x040028CB RID: 10443
		private static volatile IMessageSink xCtxChannel;

		// Token: 0x040028CC RID: 10444
		[SecurityCritical]
		private unsafe static volatile Perf_Contexts* perf_Contexts = ChannelServices.GetPrivateContextsPerfCounters();

		// Token: 0x040028CD RID: 10445
		private static bool unloadHandlerRegistered = false;
	}
}
