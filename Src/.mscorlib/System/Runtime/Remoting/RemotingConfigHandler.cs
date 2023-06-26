using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Runtime.Remoting
{
	// Token: 0x020007AD RID: 1965
	internal static class RemotingConfigHandler
	{
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06005537 RID: 21815 RVA: 0x0012FBCB File Offset: 0x0012DDCB
		// (set) Token: 0x06005538 RID: 21816 RVA: 0x0012FBF0 File Offset: 0x0012DDF0
		internal static string ApplicationName
		{
			get
			{
				if (RemotingConfigHandler._applicationName == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Config_NoAppName"));
				}
				return RemotingConfigHandler._applicationName;
			}
			set
			{
				if (RemotingConfigHandler._applicationName != null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_AppNameSet"), RemotingConfigHandler._applicationName));
				}
				RemotingConfigHandler._applicationName = value;
				char[] array = new char[] { '/' };
				if (RemotingConfigHandler._applicationName.StartsWith("/", StringComparison.Ordinal))
				{
					RemotingConfigHandler._applicationName = RemotingConfigHandler._applicationName.TrimStart(array);
				}
				if (RemotingConfigHandler._applicationName.EndsWith("/", StringComparison.Ordinal))
				{
					RemotingConfigHandler._applicationName = RemotingConfigHandler._applicationName.TrimEnd(array);
				}
			}
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x0012FC8B File Offset: 0x0012DE8B
		internal static bool HasApplicationNameBeenSet()
		{
			return RemotingConfigHandler._applicationName != null;
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x0600553A RID: 21818 RVA: 0x0012FC97 File Offset: 0x0012DE97
		internal static bool UrlObjRefMode
		{
			get
			{
				return RemotingConfigHandler._bUrlObjRefMode;
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x0600553B RID: 21819 RVA: 0x0012FCA0 File Offset: 0x0012DEA0
		// (set) Token: 0x0600553C RID: 21820 RVA: 0x0012FCA9 File Offset: 0x0012DEA9
		internal static CustomErrorsModes CustomErrorsMode
		{
			get
			{
				return RemotingConfigHandler._errorMode;
			}
			set
			{
				if (RemotingConfigHandler._errorsModeSet)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Config_ErrorsModeSet"));
				}
				RemotingConfigHandler._errorMode = value;
				RemotingConfigHandler._errorsModeSet = true;
			}
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x0012FCD4 File Offset: 0x0012DED4
		[SecurityCritical]
		internal static IMessageSink FindDelayLoadChannelForCreateMessageSink(string url, object data, out string objectURI)
		{
			RemotingConfigHandler.LoadMachineConfigIfNecessary();
			objectURI = null;
			foreach (object obj in RemotingConfigHandler._delayLoadChannelConfigQueue)
			{
				DelayLoadClientChannelEntry delayLoadClientChannelEntry = (DelayLoadClientChannelEntry)obj;
				IChannelSender channel = delayLoadClientChannelEntry.Channel;
				if (channel != null)
				{
					IMessageSink messageSink = channel.CreateMessageSink(url, data, out objectURI);
					if (messageSink != null)
					{
						delayLoadClientChannelEntry.RegisterChannel();
						return messageSink;
					}
				}
			}
			return null;
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x0012FD58 File Offset: 0x0012DF58
		[SecurityCritical]
		private static void LoadMachineConfigIfNecessary()
		{
			if (!RemotingConfigHandler._bMachineConfigLoaded)
			{
				RemotingConfigHandler.RemotingConfigInfo info = RemotingConfigHandler.Info;
				lock (info)
				{
					if (!RemotingConfigHandler._bMachineConfigLoaded)
					{
						RemotingXmlConfigFileData remotingXmlConfigFileData = RemotingXmlConfigFileParser.ParseDefaultConfiguration();
						if (remotingXmlConfigFileData != null)
						{
							RemotingConfigHandler.ConfigureRemoting(remotingXmlConfigFileData, false);
						}
						string machineDirectory = Config.MachineDirectory;
						string text = machineDirectory + "machine.config";
						new FileIOPermission(FileIOPermissionAccess.Read, text).Assert();
						remotingXmlConfigFileData = RemotingConfigHandler.LoadConfigurationFromXmlFile(text);
						if (remotingXmlConfigFileData != null)
						{
							RemotingConfigHandler.ConfigureRemoting(remotingXmlConfigFileData, false);
						}
						RemotingConfigHandler._bMachineConfigLoaded = true;
					}
				}
			}
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x0012FDF0 File Offset: 0x0012DFF0
		[SecurityCritical]
		internal static void DoConfiguration(string filename, bool ensureSecurity)
		{
			RemotingConfigHandler.LoadMachineConfigIfNecessary();
			RemotingXmlConfigFileData remotingXmlConfigFileData = RemotingConfigHandler.LoadConfigurationFromXmlFile(filename);
			if (remotingXmlConfigFileData != null)
			{
				RemotingConfigHandler.ConfigureRemoting(remotingXmlConfigFileData, ensureSecurity);
			}
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x0012FE14 File Offset: 0x0012E014
		private static RemotingXmlConfigFileData LoadConfigurationFromXmlFile(string filename)
		{
			RemotingXmlConfigFileData remotingXmlConfigFileData;
			try
			{
				if (filename != null)
				{
					remotingXmlConfigFileData = RemotingXmlConfigFileParser.ParseConfigFile(filename);
				}
				else
				{
					remotingXmlConfigFileData = null;
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex.InnerException as FileNotFoundException;
				if (ex2 != null)
				{
					ex = ex2;
				}
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_ReadFailure"), filename, ex));
			}
			return remotingXmlConfigFileData;
		}

		// Token: 0x06005541 RID: 21825 RVA: 0x0012FE70 File Offset: 0x0012E070
		[SecurityCritical]
		private static void ConfigureRemoting(RemotingXmlConfigFileData configData, bool ensureSecurity)
		{
			try
			{
				string applicationName = configData.ApplicationName;
				if (applicationName != null)
				{
					RemotingConfigHandler.ApplicationName = applicationName;
				}
				if (configData.CustomErrors != null)
				{
					RemotingConfigHandler._errorMode = configData.CustomErrors.Mode;
				}
				RemotingConfigHandler.ConfigureChannels(configData, ensureSecurity);
				if (configData.Lifetime != null)
				{
					if (configData.Lifetime.IsLeaseTimeSet)
					{
						LifetimeServices.LeaseTime = configData.Lifetime.LeaseTime;
					}
					if (configData.Lifetime.IsRenewOnCallTimeSet)
					{
						LifetimeServices.RenewOnCallTime = configData.Lifetime.RenewOnCallTime;
					}
					if (configData.Lifetime.IsSponsorshipTimeoutSet)
					{
						LifetimeServices.SponsorshipTimeout = configData.Lifetime.SponsorshipTimeout;
					}
					if (configData.Lifetime.IsLeaseManagerPollTimeSet)
					{
						LifetimeServices.LeaseManagerPollTime = configData.Lifetime.LeaseManagerPollTime;
					}
				}
				RemotingConfigHandler._bUrlObjRefMode = configData.UrlObjRefMode;
				RemotingConfigHandler.Info.StoreRemoteAppEntries(configData);
				RemotingConfigHandler.Info.StoreActivatedExports(configData);
				RemotingConfigHandler.Info.StoreInteropEntries(configData);
				RemotingConfigHandler.Info.StoreWellKnownExports(configData);
				if (configData.ServerActivatedEntries.Count > 0)
				{
					ActivationServices.StartListeningForRemoteRequests();
				}
			}
			catch (Exception ex)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_ConfigurationFailure"), ex));
			}
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x0012FFA4 File Offset: 0x0012E1A4
		[SecurityCritical]
		private static void ConfigureChannels(RemotingXmlConfigFileData configData, bool ensureSecurity)
		{
			RemotingServices.RegisterWellKnownChannels();
			foreach (object obj in configData.ChannelEntries)
			{
				RemotingXmlConfigFileData.ChannelEntry channelEntry = (RemotingXmlConfigFileData.ChannelEntry)obj;
				if (!channelEntry.DelayLoad)
				{
					IChannel channel = RemotingConfigHandler.CreateChannelFromConfigEntry(channelEntry);
					ChannelServices.RegisterChannel(channel, ensureSecurity);
				}
				else
				{
					RemotingConfigHandler._delayLoadChannelConfigQueue.Enqueue(new DelayLoadClientChannelEntry(channelEntry, ensureSecurity));
				}
			}
		}

		// Token: 0x06005543 RID: 21827 RVA: 0x00130028 File Offset: 0x0012E228
		[SecurityCritical]
		internal static IChannel CreateChannelFromConfigEntry(RemotingXmlConfigFileData.ChannelEntry entry)
		{
			Type type = RemotingConfigHandler.RemotingConfigInfo.LoadType(entry.TypeName, entry.AssemblyName);
			bool flag = typeof(IChannelReceiver).IsAssignableFrom(type);
			bool flag2 = typeof(IChannelSender).IsAssignableFrom(type);
			IClientChannelSinkProvider clientChannelSinkProvider = null;
			IServerChannelSinkProvider serverChannelSinkProvider = null;
			if (entry.ClientSinkProviders.Count > 0)
			{
				clientChannelSinkProvider = RemotingConfigHandler.CreateClientChannelSinkProviderChain(entry.ClientSinkProviders);
			}
			if (entry.ServerSinkProviders.Count > 0)
			{
				serverChannelSinkProvider = RemotingConfigHandler.CreateServerChannelSinkProviderChain(entry.ServerSinkProviders);
			}
			object[] array;
			if (flag && flag2)
			{
				array = new object[] { entry.Properties, clientChannelSinkProvider, serverChannelSinkProvider };
			}
			else if (flag)
			{
				array = new object[] { entry.Properties, serverChannelSinkProvider };
			}
			else
			{
				if (!flag2)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_InvalidChannelType"), type.FullName));
				}
				array = new object[] { entry.Properties, clientChannelSinkProvider };
			}
			IChannel channel = null;
			try
			{
				channel = (IChannel)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, array, null, null);
			}
			catch (MissingMethodException)
			{
				string text = null;
				if (flag && flag2)
				{
					text = "MyChannel(IDictionary properties, IClientChannelSinkProvider clientSinkProvider, IServerChannelSinkProvider serverSinkProvider)";
				}
				else if (flag)
				{
					text = "MyChannel(IDictionary properties, IServerChannelSinkProvider serverSinkProvider)";
				}
				else if (flag2)
				{
					text = "MyChannel(IDictionary properties, IClientChannelSinkProvider clientSinkProvider)";
				}
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_ChannelMissingCtor"), type.FullName, text));
			}
			return channel;
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x00130198 File Offset: 0x0012E398
		[SecurityCritical]
		private static IClientChannelSinkProvider CreateClientChannelSinkProviderChain(ArrayList entries)
		{
			IClientChannelSinkProvider clientChannelSinkProvider = null;
			IClientChannelSinkProvider clientChannelSinkProvider2 = null;
			foreach (object obj in entries)
			{
				RemotingXmlConfigFileData.SinkProviderEntry sinkProviderEntry = (RemotingXmlConfigFileData.SinkProviderEntry)obj;
				if (clientChannelSinkProvider == null)
				{
					clientChannelSinkProvider = (IClientChannelSinkProvider)RemotingConfigHandler.CreateChannelSinkProvider(sinkProviderEntry, false);
					clientChannelSinkProvider2 = clientChannelSinkProvider;
				}
				else
				{
					clientChannelSinkProvider2.Next = (IClientChannelSinkProvider)RemotingConfigHandler.CreateChannelSinkProvider(sinkProviderEntry, false);
					clientChannelSinkProvider2 = clientChannelSinkProvider2.Next;
				}
			}
			return clientChannelSinkProvider;
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x0013021C File Offset: 0x0012E41C
		[SecurityCritical]
		private static IServerChannelSinkProvider CreateServerChannelSinkProviderChain(ArrayList entries)
		{
			IServerChannelSinkProvider serverChannelSinkProvider = null;
			IServerChannelSinkProvider serverChannelSinkProvider2 = null;
			foreach (object obj in entries)
			{
				RemotingXmlConfigFileData.SinkProviderEntry sinkProviderEntry = (RemotingXmlConfigFileData.SinkProviderEntry)obj;
				if (serverChannelSinkProvider == null)
				{
					serverChannelSinkProvider = (IServerChannelSinkProvider)RemotingConfigHandler.CreateChannelSinkProvider(sinkProviderEntry, true);
					serverChannelSinkProvider2 = serverChannelSinkProvider;
				}
				else
				{
					serverChannelSinkProvider2.Next = (IServerChannelSinkProvider)RemotingConfigHandler.CreateChannelSinkProvider(sinkProviderEntry, true);
					serverChannelSinkProvider2 = serverChannelSinkProvider2.Next;
				}
			}
			return serverChannelSinkProvider;
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x001302A0 File Offset: 0x0012E4A0
		[SecurityCritical]
		private static object CreateChannelSinkProvider(RemotingXmlConfigFileData.SinkProviderEntry entry, bool bServer)
		{
			object obj = null;
			Type type = RemotingConfigHandler.RemotingConfigInfo.LoadType(entry.TypeName, entry.AssemblyName);
			if (bServer)
			{
				if (!typeof(IServerChannelSinkProvider).IsAssignableFrom(type))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_InvalidSinkProviderType"), type.FullName, "IServerChannelSinkProvider"));
				}
			}
			else if (!typeof(IClientChannelSinkProvider).IsAssignableFrom(type))
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_InvalidSinkProviderType"), type.FullName, "IClientChannelSinkProvider"));
			}
			if (entry.IsFormatter && ((bServer && !typeof(IServerFormatterSinkProvider).IsAssignableFrom(type)) || (!bServer && !typeof(IClientFormatterSinkProvider).IsAssignableFrom(type))))
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_SinkProviderNotFormatter"), type.FullName));
			}
			object[] array = new object[] { entry.Properties, entry.ProviderData };
			try
			{
				obj = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, array, null, null);
			}
			catch (MissingMethodException)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_SinkProviderMissingCtor"), type.FullName, "MySinkProvider(IDictionary properties, ICollection providerData)"));
			}
			return obj;
		}

		// Token: 0x06005547 RID: 21831 RVA: 0x001303E8 File Offset: 0x0012E5E8
		[SecurityCritical]
		internal static ActivatedClientTypeEntry IsRemotelyActivatedClientType(RuntimeType svrType)
		{
			RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(svrType);
			string simpleAssemblyName = reflectionCachedData.SimpleAssemblyName;
			ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfigHandler.Info.QueryRemoteActivate(svrType.FullName, simpleAssemblyName);
			if (activatedClientTypeEntry == null)
			{
				string assemblyName = reflectionCachedData.AssemblyName;
				activatedClientTypeEntry = RemotingConfigHandler.Info.QueryRemoteActivate(svrType.FullName, assemblyName);
				if (activatedClientTypeEntry == null)
				{
					activatedClientTypeEntry = RemotingConfigHandler.Info.QueryRemoteActivate(svrType.Name, simpleAssemblyName);
				}
			}
			return activatedClientTypeEntry;
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x00130447 File Offset: 0x0012E647
		internal static ActivatedClientTypeEntry IsRemotelyActivatedClientType(string typeName, string assemblyName)
		{
			return RemotingConfigHandler.Info.QueryRemoteActivate(typeName, assemblyName);
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x00130458 File Offset: 0x0012E658
		[SecurityCritical]
		internal static WellKnownClientTypeEntry IsWellKnownClientType(RuntimeType svrType)
		{
			RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(svrType);
			string simpleAssemblyName = reflectionCachedData.SimpleAssemblyName;
			WellKnownClientTypeEntry wellKnownClientTypeEntry = RemotingConfigHandler.Info.QueryConnect(svrType.FullName, simpleAssemblyName);
			if (wellKnownClientTypeEntry == null)
			{
				wellKnownClientTypeEntry = RemotingConfigHandler.Info.QueryConnect(svrType.Name, simpleAssemblyName);
			}
			return wellKnownClientTypeEntry;
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x0013049B File Offset: 0x0012E69B
		internal static WellKnownClientTypeEntry IsWellKnownClientType(string typeName, string assemblyName)
		{
			return RemotingConfigHandler.Info.QueryConnect(typeName, assemblyName);
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x001304AC File Offset: 0x0012E6AC
		private static void ParseGenericType(string typeAssem, int indexStart, out string typeName, out string assemName)
		{
			int length = typeAssem.Length;
			int num = 1;
			int num2 = indexStart;
			while (num > 0 && ++num2 < length - 1)
			{
				if (typeAssem[num2] == '[')
				{
					num++;
				}
				else if (typeAssem[num2] == ']')
				{
					num--;
				}
			}
			if (num > 0 || num2 >= length)
			{
				typeName = null;
				assemName = null;
				return;
			}
			num2 = typeAssem.IndexOf(',', num2);
			if (num2 >= 0 && num2 < length - 1)
			{
				typeName = typeAssem.Substring(0, num2).Trim();
				assemName = typeAssem.Substring(num2 + 1).Trim();
				return;
			}
			typeName = null;
			assemName = null;
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x00130540 File Offset: 0x0012E740
		internal static void ParseType(string typeAssem, out string typeName, out string assemName)
		{
			int num = typeAssem.IndexOf("[");
			if (num >= 0 && num < typeAssem.Length - 1)
			{
				RemotingConfigHandler.ParseGenericType(typeAssem, num, out typeName, out assemName);
				return;
			}
			int num2 = typeAssem.IndexOf(",");
			if (num2 >= 0 && num2 < typeAssem.Length - 1)
			{
				typeName = typeAssem.Substring(0, num2).Trim();
				assemName = typeAssem.Substring(num2 + 1).Trim();
				return;
			}
			typeName = null;
			assemName = null;
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x001305B8 File Offset: 0x0012E7B8
		[SecurityCritical]
		internal static bool IsActivationAllowed(RuntimeType svrType)
		{
			if (svrType == null)
			{
				return false;
			}
			RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(svrType);
			string simpleAssemblyName = reflectionCachedData.SimpleAssemblyName;
			return RemotingConfigHandler.Info.ActivationAllowed(svrType.FullName, simpleAssemblyName);
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x001305F0 File Offset: 0x0012E7F0
		[SecurityCritical]
		internal static bool IsActivationAllowed(string TypeName)
		{
			string text = RemotingServices.InternalGetTypeNameFromQualifiedTypeName(TypeName);
			if (text == null)
			{
				return false;
			}
			string text2;
			string text3;
			RemotingConfigHandler.ParseType(text, out text2, out text3);
			if (text3 == null)
			{
				return false;
			}
			int num = text3.IndexOf(',');
			if (num != -1)
			{
				text3 = text3.Substring(0, num);
			}
			return RemotingConfigHandler.Info.ActivationAllowed(text2, text3);
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x0013063A File Offset: 0x0012E83A
		internal static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
		{
			RemotingConfigHandler.Info.AddActivatedType(entry.TypeName, entry.AssemblyName, entry.ContextAttributes);
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x00130658 File Offset: 0x0012E858
		[SecurityCritical]
		internal static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
		{
			string typeName = entry.TypeName;
			string assemblyName = entry.AssemblyName;
			string objectUri = entry.ObjectUri;
			WellKnownObjectMode mode = entry.Mode;
			RemotingConfigHandler.RemotingConfigInfo info = RemotingConfigHandler.Info;
			lock (info)
			{
				RemotingConfigHandler.Info.AddWellKnownEntry(entry);
			}
		}

		// Token: 0x06005551 RID: 21841 RVA: 0x001306C0 File Offset: 0x0012E8C0
		internal static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
		{
			RemotingConfigHandler.Info.AddActivatedClientType(entry);
		}

		// Token: 0x06005552 RID: 21842 RVA: 0x001306CD File Offset: 0x0012E8CD
		internal static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
		{
			RemotingConfigHandler.Info.AddWellKnownClientType(entry);
		}

		// Token: 0x06005553 RID: 21843 RVA: 0x001306DA File Offset: 0x0012E8DA
		[SecurityCritical]
		internal static Type GetServerTypeForUri(string URI)
		{
			URI = Identity.RemoveAppNameOrAppGuidIfNecessary(URI);
			return RemotingConfigHandler.Info.GetServerTypeForUri(URI);
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x001306EF File Offset: 0x0012E8EF
		internal static ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
		{
			return RemotingConfigHandler.Info.GetRegisteredActivatedServiceTypes();
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x001306FB File Offset: 0x0012E8FB
		internal static WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
		{
			return RemotingConfigHandler.Info.GetRegisteredWellKnownServiceTypes();
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x00130707 File Offset: 0x0012E907
		internal static ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
		{
			return RemotingConfigHandler.Info.GetRegisteredActivatedClientTypes();
		}

		// Token: 0x06005557 RID: 21847 RVA: 0x00130713 File Offset: 0x0012E913
		internal static WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
		{
			return RemotingConfigHandler.Info.GetRegisteredWellKnownClientTypes();
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x0013071F File Offset: 0x0012E91F
		[SecurityCritical]
		internal static ServerIdentity CreateWellKnownObject(string uri)
		{
			uri = Identity.RemoveAppNameOrAppGuidIfNecessary(uri);
			return RemotingConfigHandler.Info.StartupWellKnownObject(uri);
		}

		// Token: 0x0400273E RID: 10046
		private static volatile string _applicationName;

		// Token: 0x0400273F RID: 10047
		private static volatile CustomErrorsModes _errorMode = CustomErrorsModes.RemoteOnly;

		// Token: 0x04002740 RID: 10048
		private static volatile bool _errorsModeSet = false;

		// Token: 0x04002741 RID: 10049
		private static volatile bool _bMachineConfigLoaded = false;

		// Token: 0x04002742 RID: 10050
		private static volatile bool _bUrlObjRefMode = false;

		// Token: 0x04002743 RID: 10051
		private static Queue _delayLoadChannelConfigQueue = new Queue();

		// Token: 0x04002744 RID: 10052
		public static RemotingConfigHandler.RemotingConfigInfo Info = new RemotingConfigHandler.RemotingConfigInfo();

		// Token: 0x04002745 RID: 10053
		private const string _machineConfigFilename = "machine.config";

		// Token: 0x02000C64 RID: 3172
		internal class RemotingConfigInfo
		{
			// Token: 0x0600709F RID: 28831 RVA: 0x00184E74 File Offset: 0x00183074
			internal RemotingConfigInfo()
			{
				this._remoteTypeInfo = Hashtable.Synchronized(new Hashtable());
				this._exportableClasses = Hashtable.Synchronized(new Hashtable());
				this._remoteAppInfo = Hashtable.Synchronized(new Hashtable());
				this._wellKnownExportInfo = Hashtable.Synchronized(new Hashtable());
			}

			// Token: 0x060070A0 RID: 28832 RVA: 0x00184EC7 File Offset: 0x001830C7
			private string EncodeTypeAndAssemblyNames(string typeName, string assemblyName)
			{
				return typeName + ", " + assemblyName.ToLower(CultureInfo.InvariantCulture);
			}

			// Token: 0x060070A1 RID: 28833 RVA: 0x00184EE0 File Offset: 0x001830E0
			internal void StoreActivatedExports(RemotingXmlConfigFileData configData)
			{
				foreach (object obj in configData.ServerActivatedEntries)
				{
					RemotingXmlConfigFileData.TypeEntry typeEntry = (RemotingXmlConfigFileData.TypeEntry)obj;
					RemotingConfiguration.RegisterActivatedServiceType(new ActivatedServiceTypeEntry(typeEntry.TypeName, typeEntry.AssemblyName)
					{
						ContextAttributes = RemotingConfigHandler.RemotingConfigInfo.CreateContextAttributesFromConfigEntries(typeEntry.ContextAttributes)
					});
				}
			}

			// Token: 0x060070A2 RID: 28834 RVA: 0x00184F5C File Offset: 0x0018315C
			[SecurityCritical]
			internal void StoreInteropEntries(RemotingXmlConfigFileData configData)
			{
				foreach (object obj in configData.InteropXmlElementEntries)
				{
					RemotingXmlConfigFileData.InteropXmlElementEntry interopXmlElementEntry = (RemotingXmlConfigFileData.InteropXmlElementEntry)obj;
					Assembly assembly = Assembly.Load(interopXmlElementEntry.UrtAssemblyName);
					Type type = assembly.GetType(interopXmlElementEntry.UrtTypeName);
					SoapServices.RegisterInteropXmlElement(interopXmlElementEntry.XmlElementName, interopXmlElementEntry.XmlElementNamespace, type);
				}
				foreach (object obj2 in configData.InteropXmlTypeEntries)
				{
					RemotingXmlConfigFileData.InteropXmlTypeEntry interopXmlTypeEntry = (RemotingXmlConfigFileData.InteropXmlTypeEntry)obj2;
					Assembly assembly2 = Assembly.Load(interopXmlTypeEntry.UrtAssemblyName);
					Type type2 = assembly2.GetType(interopXmlTypeEntry.UrtTypeName);
					SoapServices.RegisterInteropXmlType(interopXmlTypeEntry.XmlTypeName, interopXmlTypeEntry.XmlTypeNamespace, type2);
				}
				foreach (object obj3 in configData.PreLoadEntries)
				{
					RemotingXmlConfigFileData.PreLoadEntry preLoadEntry = (RemotingXmlConfigFileData.PreLoadEntry)obj3;
					Assembly assembly3 = Assembly.Load(preLoadEntry.AssemblyName);
					if (preLoadEntry.TypeName != null)
					{
						Type type3 = assembly3.GetType(preLoadEntry.TypeName);
						SoapServices.PreLoad(type3);
					}
					else
					{
						SoapServices.PreLoad(assembly3);
					}
				}
			}

			// Token: 0x060070A3 RID: 28835 RVA: 0x001850D8 File Offset: 0x001832D8
			internal void StoreRemoteAppEntries(RemotingXmlConfigFileData configData)
			{
				char[] array = new char[] { '/' };
				foreach (object obj in configData.RemoteAppEntries)
				{
					RemotingXmlConfigFileData.RemoteAppEntry remoteAppEntry = (RemotingXmlConfigFileData.RemoteAppEntry)obj;
					string text = remoteAppEntry.AppUri;
					if (text != null && !text.EndsWith("/", StringComparison.Ordinal))
					{
						text = text.TrimEnd(array);
					}
					foreach (object obj2 in remoteAppEntry.ActivatedObjects)
					{
						RemotingXmlConfigFileData.TypeEntry typeEntry = (RemotingXmlConfigFileData.TypeEntry)obj2;
						RemotingConfiguration.RegisterActivatedClientType(new ActivatedClientTypeEntry(typeEntry.TypeName, typeEntry.AssemblyName, text)
						{
							ContextAttributes = RemotingConfigHandler.RemotingConfigInfo.CreateContextAttributesFromConfigEntries(typeEntry.ContextAttributes)
						});
					}
					foreach (object obj3 in remoteAppEntry.WellKnownObjects)
					{
						RemotingXmlConfigFileData.ClientWellKnownEntry clientWellKnownEntry = (RemotingXmlConfigFileData.ClientWellKnownEntry)obj3;
						RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(clientWellKnownEntry.TypeName, clientWellKnownEntry.AssemblyName, clientWellKnownEntry.Url)
						{
							ApplicationUrl = text
						});
					}
				}
			}

			// Token: 0x060070A4 RID: 28836 RVA: 0x00185270 File Offset: 0x00183470
			[SecurityCritical]
			internal void StoreWellKnownExports(RemotingXmlConfigFileData configData)
			{
				foreach (object obj in configData.ServerWellKnownEntries)
				{
					RemotingXmlConfigFileData.ServerWellKnownEntry serverWellKnownEntry = (RemotingXmlConfigFileData.ServerWellKnownEntry)obj;
					RemotingConfigHandler.RegisterWellKnownServiceType(new WellKnownServiceTypeEntry(serverWellKnownEntry.TypeName, serverWellKnownEntry.AssemblyName, serverWellKnownEntry.ObjectURI, serverWellKnownEntry.ObjectMode)
					{
						ContextAttributes = null
					});
				}
			}

			// Token: 0x060070A5 RID: 28837 RVA: 0x001852F0 File Offset: 0x001834F0
			private static IContextAttribute[] CreateContextAttributesFromConfigEntries(ArrayList contextAttributes)
			{
				int count = contextAttributes.Count;
				if (count == 0)
				{
					return null;
				}
				IContextAttribute[] array = new IContextAttribute[count];
				int num = 0;
				foreach (object obj in contextAttributes)
				{
					RemotingXmlConfigFileData.ContextAttributeEntry contextAttributeEntry = (RemotingXmlConfigFileData.ContextAttributeEntry)obj;
					Assembly assembly = Assembly.Load(contextAttributeEntry.AssemblyName);
					Hashtable properties = contextAttributeEntry.Properties;
					IContextAttribute contextAttribute;
					if (properties != null && properties.Count > 0)
					{
						object[] array2 = new object[] { properties };
						contextAttribute = (IContextAttribute)Activator.CreateInstance(assembly.GetType(contextAttributeEntry.TypeName, false, false), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, array2, null, null);
					}
					else
					{
						contextAttribute = (IContextAttribute)Activator.CreateInstance(assembly.GetType(contextAttributeEntry.TypeName, false, false), true);
					}
					array[num++] = contextAttribute;
				}
				return array;
			}

			// Token: 0x060070A6 RID: 28838 RVA: 0x001853E4 File Offset: 0x001835E4
			internal bool ActivationAllowed(string typeName, string assemblyName)
			{
				return this._exportableClasses.ContainsKey(this.EncodeTypeAndAssemblyNames(typeName, assemblyName));
			}

			// Token: 0x060070A7 RID: 28839 RVA: 0x001853FC File Offset: 0x001835FC
			internal ActivatedClientTypeEntry QueryRemoteActivate(string typeName, string assemblyName)
			{
				string text = this.EncodeTypeAndAssemblyNames(typeName, assemblyName);
				ActivatedClientTypeEntry activatedClientTypeEntry = this._remoteTypeInfo[text] as ActivatedClientTypeEntry;
				if (activatedClientTypeEntry == null)
				{
					return null;
				}
				if (activatedClientTypeEntry.GetRemoteAppEntry() == null)
				{
					RemoteAppEntry remoteAppEntry = (RemoteAppEntry)this._remoteAppInfo[activatedClientTypeEntry.ApplicationUrl];
					if (remoteAppEntry == null)
					{
						throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_MissingRemoteAppEntry"), activatedClientTypeEntry.ApplicationUrl));
					}
					activatedClientTypeEntry.CacheRemoteAppEntry(remoteAppEntry);
				}
				return activatedClientTypeEntry;
			}

			// Token: 0x060070A8 RID: 28840 RVA: 0x00185474 File Offset: 0x00183674
			internal WellKnownClientTypeEntry QueryConnect(string typeName, string assemblyName)
			{
				string text = this.EncodeTypeAndAssemblyNames(typeName, assemblyName);
				WellKnownClientTypeEntry wellKnownClientTypeEntry = this._remoteTypeInfo[text] as WellKnownClientTypeEntry;
				if (wellKnownClientTypeEntry == null)
				{
					return null;
				}
				return wellKnownClientTypeEntry;
			}

			// Token: 0x060070A9 RID: 28841 RVA: 0x001854A4 File Offset: 0x001836A4
			internal ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
			{
				ActivatedServiceTypeEntry[] array = new ActivatedServiceTypeEntry[this._exportableClasses.Count];
				int num = 0;
				foreach (object obj in this._exportableClasses)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					array[num++] = (ActivatedServiceTypeEntry)dictionaryEntry.Value;
				}
				return array;
			}

			// Token: 0x060070AA RID: 28842 RVA: 0x00185520 File Offset: 0x00183720
			internal WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
			{
				WellKnownServiceTypeEntry[] array = new WellKnownServiceTypeEntry[this._wellKnownExportInfo.Count];
				int num = 0;
				foreach (object obj in this._wellKnownExportInfo)
				{
					WellKnownServiceTypeEntry wellKnownServiceTypeEntry = (WellKnownServiceTypeEntry)((DictionaryEntry)obj).Value;
					WellKnownServiceTypeEntry wellKnownServiceTypeEntry2 = new WellKnownServiceTypeEntry(wellKnownServiceTypeEntry.TypeName, wellKnownServiceTypeEntry.AssemblyName, wellKnownServiceTypeEntry.ObjectUri, wellKnownServiceTypeEntry.Mode);
					wellKnownServiceTypeEntry2.ContextAttributes = wellKnownServiceTypeEntry.ContextAttributes;
					array[num++] = wellKnownServiceTypeEntry2;
				}
				return array;
			}

			// Token: 0x060070AB RID: 28843 RVA: 0x001855D4 File Offset: 0x001837D4
			internal ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
			{
				int num = 0;
				foreach (object obj in this._remoteTypeInfo)
				{
					ActivatedClientTypeEntry activatedClientTypeEntry = ((DictionaryEntry)obj).Value as ActivatedClientTypeEntry;
					if (activatedClientTypeEntry != null)
					{
						num++;
					}
				}
				ActivatedClientTypeEntry[] array = new ActivatedClientTypeEntry[num];
				int num2 = 0;
				foreach (object obj2 in this._remoteTypeInfo)
				{
					ActivatedClientTypeEntry activatedClientTypeEntry2 = ((DictionaryEntry)obj2).Value as ActivatedClientTypeEntry;
					if (activatedClientTypeEntry2 != null)
					{
						string text = null;
						RemoteAppEntry remoteAppEntry = activatedClientTypeEntry2.GetRemoteAppEntry();
						if (remoteAppEntry != null)
						{
							text = remoteAppEntry.GetAppURI();
						}
						ActivatedClientTypeEntry activatedClientTypeEntry3 = new ActivatedClientTypeEntry(activatedClientTypeEntry2.TypeName, activatedClientTypeEntry2.AssemblyName, text);
						activatedClientTypeEntry3.ContextAttributes = activatedClientTypeEntry2.ContextAttributes;
						array[num2++] = activatedClientTypeEntry3;
					}
				}
				return array;
			}

			// Token: 0x060070AC RID: 28844 RVA: 0x001856F0 File Offset: 0x001838F0
			internal WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
			{
				int num = 0;
				foreach (object obj in this._remoteTypeInfo)
				{
					WellKnownClientTypeEntry wellKnownClientTypeEntry = ((DictionaryEntry)obj).Value as WellKnownClientTypeEntry;
					if (wellKnownClientTypeEntry != null)
					{
						num++;
					}
				}
				WellKnownClientTypeEntry[] array = new WellKnownClientTypeEntry[num];
				int num2 = 0;
				foreach (object obj2 in this._remoteTypeInfo)
				{
					WellKnownClientTypeEntry wellKnownClientTypeEntry2 = ((DictionaryEntry)obj2).Value as WellKnownClientTypeEntry;
					if (wellKnownClientTypeEntry2 != null)
					{
						WellKnownClientTypeEntry wellKnownClientTypeEntry3 = new WellKnownClientTypeEntry(wellKnownClientTypeEntry2.TypeName, wellKnownClientTypeEntry2.AssemblyName, wellKnownClientTypeEntry2.ObjectUrl);
						RemoteAppEntry remoteAppEntry = wellKnownClientTypeEntry2.GetRemoteAppEntry();
						if (remoteAppEntry != null)
						{
							wellKnownClientTypeEntry3.ApplicationUrl = remoteAppEntry.GetAppURI();
						}
						array[num2++] = wellKnownClientTypeEntry3;
					}
				}
				return array;
			}

			// Token: 0x060070AD RID: 28845 RVA: 0x00185804 File Offset: 0x00183A04
			internal void AddActivatedType(string typeName, string assemblyName, IContextAttribute[] contextAttributes)
			{
				if (typeName == null)
				{
					throw new ArgumentNullException("typeName");
				}
				if (assemblyName == null)
				{
					throw new ArgumentNullException("assemblyName");
				}
				if (this.CheckForRedirectedClientType(typeName, assemblyName))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_CantUseRedirectedTypeForWellKnownService"), typeName, assemblyName));
				}
				ActivatedServiceTypeEntry activatedServiceTypeEntry = new ActivatedServiceTypeEntry(typeName, assemblyName);
				activatedServiceTypeEntry.ContextAttributes = contextAttributes;
				string text = this.EncodeTypeAndAssemblyNames(typeName, assemblyName);
				this._exportableClasses.Add(text, activatedServiceTypeEntry);
			}

			// Token: 0x060070AE RID: 28846 RVA: 0x00185878 File Offset: 0x00183A78
			private bool CheckForServiceEntryWithType(string typeName, string asmName)
			{
				return this.CheckForWellKnownServiceEntryWithType(typeName, asmName) || this.ActivationAllowed(typeName, asmName);
			}

			// Token: 0x060070AF RID: 28847 RVA: 0x00185890 File Offset: 0x00183A90
			private bool CheckForWellKnownServiceEntryWithType(string typeName, string asmName)
			{
				foreach (object obj in this._wellKnownExportInfo)
				{
					WellKnownServiceTypeEntry wellKnownServiceTypeEntry = (WellKnownServiceTypeEntry)((DictionaryEntry)obj).Value;
					if (typeName == wellKnownServiceTypeEntry.TypeName)
					{
						bool flag = false;
						if (asmName == wellKnownServiceTypeEntry.AssemblyName)
						{
							flag = true;
						}
						else if (string.Compare(wellKnownServiceTypeEntry.AssemblyName, 0, asmName, 0, asmName.Length, StringComparison.OrdinalIgnoreCase) == 0 && wellKnownServiceTypeEntry.AssemblyName[asmName.Length] == ',')
						{
							flag = true;
						}
						if (flag)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x060070B0 RID: 28848 RVA: 0x00185950 File Offset: 0x00183B50
			private bool CheckForRedirectedClientType(string typeName, string asmName)
			{
				int num = asmName.IndexOf(",");
				if (num != -1)
				{
					asmName = asmName.Substring(0, num);
				}
				return this.QueryRemoteActivate(typeName, asmName) != null || this.QueryConnect(typeName, asmName) != null;
			}

			// Token: 0x060070B1 RID: 28849 RVA: 0x00185990 File Offset: 0x00183B90
			internal void AddActivatedClientType(ActivatedClientTypeEntry entry)
			{
				if (this.CheckForRedirectedClientType(entry.TypeName, entry.AssemblyName))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_TypeAlreadyRedirected"), entry.TypeName, entry.AssemblyName));
				}
				if (this.CheckForServiceEntryWithType(entry.TypeName, entry.AssemblyName))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_CantRedirectActivationOfWellKnownService"), entry.TypeName, entry.AssemblyName));
				}
				string applicationUrl = entry.ApplicationUrl;
				RemoteAppEntry remoteAppEntry = (RemoteAppEntry)this._remoteAppInfo[applicationUrl];
				if (remoteAppEntry == null)
				{
					remoteAppEntry = new RemoteAppEntry(applicationUrl, applicationUrl);
					this._remoteAppInfo.Add(applicationUrl, remoteAppEntry);
				}
				if (remoteAppEntry != null)
				{
					entry.CacheRemoteAppEntry(remoteAppEntry);
				}
				string text = this.EncodeTypeAndAssemblyNames(entry.TypeName, entry.AssemblyName);
				this._remoteTypeInfo.Add(text, entry);
			}

			// Token: 0x060070B2 RID: 28850 RVA: 0x00185A6C File Offset: 0x00183C6C
			internal void AddWellKnownClientType(WellKnownClientTypeEntry entry)
			{
				if (this.CheckForRedirectedClientType(entry.TypeName, entry.AssemblyName))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_TypeAlreadyRedirected"), entry.TypeName, entry.AssemblyName));
				}
				if (this.CheckForServiceEntryWithType(entry.TypeName, entry.AssemblyName))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_CantRedirectActivationOfWellKnownService"), entry.TypeName, entry.AssemblyName));
				}
				string applicationUrl = entry.ApplicationUrl;
				RemoteAppEntry remoteAppEntry = null;
				if (applicationUrl != null)
				{
					remoteAppEntry = (RemoteAppEntry)this._remoteAppInfo[applicationUrl];
					if (remoteAppEntry == null)
					{
						remoteAppEntry = new RemoteAppEntry(applicationUrl, applicationUrl);
						this._remoteAppInfo.Add(applicationUrl, remoteAppEntry);
					}
				}
				if (remoteAppEntry != null)
				{
					entry.CacheRemoteAppEntry(remoteAppEntry);
				}
				string text = this.EncodeTypeAndAssemblyNames(entry.TypeName, entry.AssemblyName);
				this._remoteTypeInfo.Add(text, entry);
			}

			// Token: 0x060070B3 RID: 28851 RVA: 0x00185B4D File Offset: 0x00183D4D
			[SecurityCritical]
			internal void AddWellKnownEntry(WellKnownServiceTypeEntry entry)
			{
				this.AddWellKnownEntry(entry, true);
			}

			// Token: 0x060070B4 RID: 28852 RVA: 0x00185B58 File Offset: 0x00183D58
			[SecurityCritical]
			internal void AddWellKnownEntry(WellKnownServiceTypeEntry entry, bool fReplace)
			{
				if (this.CheckForRedirectedClientType(entry.TypeName, entry.AssemblyName))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_CantUseRedirectedTypeForWellKnownService"), entry.TypeName, entry.AssemblyName));
				}
				string text = entry.ObjectUri.ToLower(CultureInfo.InvariantCulture);
				if (fReplace)
				{
					this._wellKnownExportInfo[text] = entry;
					IdentityHolder.RemoveIdentity(entry.ObjectUri);
					return;
				}
				this._wellKnownExportInfo.Add(text, entry);
			}

			// Token: 0x060070B5 RID: 28853 RVA: 0x00185BDC File Offset: 0x00183DDC
			[SecurityCritical]
			internal Type GetServerTypeForUri(string URI)
			{
				Type type = null;
				string text = URI.ToLower(CultureInfo.InvariantCulture);
				WellKnownServiceTypeEntry wellKnownServiceTypeEntry = (WellKnownServiceTypeEntry)this._wellKnownExportInfo[text];
				if (wellKnownServiceTypeEntry != null)
				{
					type = RemotingConfigHandler.RemotingConfigInfo.LoadType(wellKnownServiceTypeEntry.TypeName, wellKnownServiceTypeEntry.AssemblyName);
				}
				return type;
			}

			// Token: 0x060070B6 RID: 28854 RVA: 0x00185C20 File Offset: 0x00183E20
			[SecurityCritical]
			internal ServerIdentity StartupWellKnownObject(string URI)
			{
				string text = URI.ToLower(CultureInfo.InvariantCulture);
				ServerIdentity serverIdentity = null;
				WellKnownServiceTypeEntry wellKnownServiceTypeEntry = (WellKnownServiceTypeEntry)this._wellKnownExportInfo[text];
				if (wellKnownServiceTypeEntry != null)
				{
					serverIdentity = this.StartupWellKnownObject(wellKnownServiceTypeEntry.AssemblyName, wellKnownServiceTypeEntry.TypeName, wellKnownServiceTypeEntry.ObjectUri, wellKnownServiceTypeEntry.Mode);
				}
				return serverIdentity;
			}

			// Token: 0x060070B7 RID: 28855 RVA: 0x00185C70 File Offset: 0x00183E70
			[SecurityCritical]
			internal ServerIdentity StartupWellKnownObject(string asmName, string svrTypeName, string URI, WellKnownObjectMode mode)
			{
				return this.StartupWellKnownObject(asmName, svrTypeName, URI, mode, false);
			}

			// Token: 0x060070B8 RID: 28856 RVA: 0x00185C80 File Offset: 0x00183E80
			[SecurityCritical]
			internal ServerIdentity StartupWellKnownObject(string asmName, string svrTypeName, string URI, WellKnownObjectMode mode, bool fReplace)
			{
				object obj = RemotingConfigHandler.RemotingConfigInfo.s_wkoStartLock;
				ServerIdentity serverIdentity2;
				lock (obj)
				{
					ServerIdentity serverIdentity = null;
					Type type = RemotingConfigHandler.RemotingConfigInfo.LoadType(svrTypeName, asmName);
					if (!type.IsMarshalByRef)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_WellKnown_MustBeMBR", new object[] { svrTypeName }));
					}
					serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity(URI);
					if (serverIdentity != null && serverIdentity.IsRemoteDisconnected())
					{
						IdentityHolder.RemoveIdentity(URI);
						serverIdentity = null;
					}
					if (serverIdentity == null)
					{
						RemotingConfigHandler.RemotingConfigInfo.s_fullTrust.Assert();
						try
						{
							MarshalByRefObject marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(type, true);
							if (RemotingServices.IsClientProxy(marshalByRefObject))
							{
								RemotingServices.MarshalInternal(new RedirectionProxy(marshalByRefObject, type)
								{
									ObjectMode = mode
								}, URI, type, true, true);
								serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity(URI);
								serverIdentity.SetSingletonObjectMode();
							}
							else if (type.IsCOMObject && mode == WellKnownObjectMode.Singleton)
							{
								ComRedirectionProxy comRedirectionProxy = new ComRedirectionProxy(marshalByRefObject, type);
								RemotingServices.MarshalInternal(comRedirectionProxy, URI, type, true, true);
								serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity(URI);
								serverIdentity.SetSingletonObjectMode();
							}
							else
							{
								string objectUri = RemotingServices.GetObjectUri(marshalByRefObject);
								if (objectUri != null)
								{
									throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_WellKnown_CtorCantMarshal"), URI));
								}
								RemotingServices.MarshalInternal(marshalByRefObject, URI, type, true, true);
								serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity(URI);
								if (mode == WellKnownObjectMode.SingleCall)
								{
									serverIdentity.SetSingleCallObjectMode();
								}
								else
								{
									serverIdentity.SetSingletonObjectMode();
								}
							}
						}
						catch
						{
							throw;
						}
						finally
						{
							if (serverIdentity != null)
							{
								serverIdentity.IsInitializing = false;
							}
							CodeAccessPermission.RevertAssert();
						}
					}
					serverIdentity2 = serverIdentity;
				}
				return serverIdentity2;
			}

			// Token: 0x060070B9 RID: 28857 RVA: 0x00185E48 File Offset: 0x00184048
			[SecurityCritical]
			internal static Type LoadType(string typeName, string assemblyName)
			{
				Assembly assembly = null;
				new FileIOPermission(PermissionState.Unrestricted).Assert();
				try
				{
					assembly = Assembly.Load(assemblyName);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (assembly == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_AssemblyLoadFailed", new object[] { assemblyName }));
				}
				Type type = assembly.GetType(typeName, false, false);
				if (type == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_BadType", new object[] { typeName + ", " + assemblyName }));
				}
				return type;
			}

			// Token: 0x040037D0 RID: 14288
			private Hashtable _exportableClasses;

			// Token: 0x040037D1 RID: 14289
			private Hashtable _remoteTypeInfo;

			// Token: 0x040037D2 RID: 14290
			private Hashtable _remoteAppInfo;

			// Token: 0x040037D3 RID: 14291
			private Hashtable _wellKnownExportInfo;

			// Token: 0x040037D4 RID: 14292
			private static char[] SepSpace = new char[] { ' ' };

			// Token: 0x040037D5 RID: 14293
			private static char[] SepPound = new char[] { '#' };

			// Token: 0x040037D6 RID: 14294
			private static char[] SepSemiColon = new char[] { ';' };

			// Token: 0x040037D7 RID: 14295
			private static char[] SepEquals = new char[] { '=' };

			// Token: 0x040037D8 RID: 14296
			private static object s_wkoStartLock = new object();

			// Token: 0x040037D9 RID: 14297
			private static PermissionSet s_fullTrust = new PermissionSet(PermissionState.Unrestricted);
		}
	}
}
